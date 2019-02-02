using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.AccountViewModels;
using WebApplication2.Services;
using DNTPersianUtils.Core;

namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public ApiController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender
            , ILogger<AccountController> logger,
            ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            _config = configuration;

        }

        [TempData]
        public string ErrorMessage { get; set; }
        [HttpPost]
        public ActionResult ReturnUrl()
        {
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> UserStatus()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "jti").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var date = DateTime.Now.ToShortPersianDateString();
            var check = _context.EntranceExits.Where(d => d.ApplicationUserId == user.Id && d.EntranceDate == date && d.ExitTime == null).Any();
            return Ok(check);

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = await _userManager.FindByEmailAsync(model.UserName);


            if (user != null)
            {
                var checkPwd = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                // var checkPwd = _signInManager.CheckPasswordSignInAsync(user, model.authUserRequest);
                if (checkPwd.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddYears(1),
                    signingCredentials: creds);
                    var t = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { token = t });
                }
            }

            return BadRequest("Could not create token");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return await Login(new LoginViewModel()
                    {
                        UserName = model.Email,
                        Password = model.Password,
                        RememberMe = false
                    });

                }
                ModelState.AddModelError("result", "result");
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);

        }



        [HttpPost]
        public async Task<IActionResult> RegisterEntrance()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "jti").Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var date = DateTime.Now.ToShortPersianDateString();
            var entrytime = DateTime.Now.ToString("HH:mm");
            var check = _context.EntranceExits.Where(d => d.ApplicationUserId == user.Id && d.EntranceDate == date && d.ExitTime == null).FirstOrDefault();
            if (check == null || check.Id == 0)
            {
                check = new EntranceExit();
                check.EntranceDate = date;
                check.EntranceTime = entrytime;
                check.ApplicationUserId = user.Id;
                _context.Add(check);

                await _context.SaveChangesAsync();
                return Ok("با موفقیت ثبت شد");
            }
            else
            {
                try
                {
                    check.ExitTime = entrytime;
                    _context.Update(check);
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(HomeController.Index), "Home");
                    return Ok("با موفقیت ثبت شد");

                }
                catch (Exception)
                { }
            }

            return BadRequest();

        }
        /// <summary>
        /// کوئری ورود و خروج افراد 
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] dateModel dateModel)
        {

            string DateFrom = dateModel.startDate.ToPersianDateTimeString("yyyy/MM/dd"); string DateTo = dateModel.endDate.ToPersianDateTimeString("yyyy/MM/dd");
            var userId = User.Claims.FirstOrDefault(x => x.Type == "jti").Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (DateFrom != null && DateTo != null)
            {
                var applicationDbContext = _context.EntranceExits.Include(e => e.ApplicationUser)
                     .Where(s => s.EntranceDate.CompareTo(DateFrom) >= 0 && s.EntranceDate.CompareTo(DateTo) <= 0).OrderBy(e => e.ApplicationUser.Email).ToList();
                if (user.UserName != "mm.940601@gmail.com")
                {
                    applicationDbContext = applicationDbContext.Where(x => x.ApplicationUserId == userId).ToList();
                }
                if (applicationDbContext != null)
                {
                    var entranceExitList = new List<EntranceExitViewModel>();
                    foreach (var item in applicationDbContext)
                    {
                        var time1 = item.EntranceTime;
                        var time2 = item.ExitTime;
                        string def = "";
                        if (time2 == null)
                        {
                            def = "0";
                        }
                        else
                        {
                            var time1dt = Convert.ToDateTime(time1);
                            var time2dt = Convert.ToDateTime(time2);
                            def = time2dt.Subtract(time1dt).ToString();
                        }
                        EntranceExitViewModel entranceExitListdish = new EntranceExitViewModel()
                        {
                            Username = item.ApplicationUser.UserName,
                            EntranceDate = item.EntranceDate,
                            FirstName = item.ApplicationUser.FirstName,
                            LastName = item.ApplicationUser.LastName,
                            EntranceTime = item.EntranceTime,
                            ExitTime = item.ExitTime,
                            Id = item.Id,
                            workTime = def
                        };
                        entranceExitList.Add(entranceExitListdish);
                    }

                    return Ok(entranceExitList);
                }
                return BadRequest();
            }

            return BadRequest();
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }

    public class dateModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}

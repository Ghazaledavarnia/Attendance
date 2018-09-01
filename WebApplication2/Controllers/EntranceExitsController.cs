using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.AccountViewModels;

namespace WebApplication2.Controllers
{
    public class EntranceExitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EntranceExitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EntranceExits
        public IActionResult Index()
        {
            return View();

            //return View(await applicationDbContext.ToListAsync());
        }
        ///_context.EntranceExits.Include(e => e.ApplicationUser)
        /// <summary>
        /// کوئری ورود و خروج افراد 
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Filter(string DateFrom,string DateTo)
        {
        
            if (DateFrom!=null && DateTo!=null)
            { 
           // CultureInfo info = new CultureInfo("fa-Ir");
                //var applicationDbContext = _context.EntranceExits.Include(e => e.ApplicationUser)
                //    .Where(
                //    s =>
                //    DateTime.ParseExact(s.EntranceDate, "yyyy/mm/dd", info) >= 
                //    DateTime.ParseExact(DateFrom, "yyyy/mm/dd", info)
                //&& DateTime.ParseExact(s.EntranceDate, "yyyy/mm/dd", info) <= 
                //DateTime.ParseExact(DateTo, "yyyy/mm/dd", info)
                //)
                //.ToList();
                var applicationDbContext = _context.EntranceExits.Include(e => e.ApplicationUser)
                    .Where(s => s.EntranceDate.CompareTo(DateFrom) >= 0 && s.EntranceDate.CompareTo(DateTo) <= 0).OrderBy(e => e.ApplicationUser.Email).ToList();
                if (applicationDbContext!=null)
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

             return PartialView("_ReportGrid", entranceExitList);
                }
                return BadRequest();
            }

            return BadRequest();
        }
        // GET: EntranceExits/Details/5
        public IActionResult Details()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var entranceExitlist = await _context.EntranceExits
            //    .Include(e => e.ApplicationUser)
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //var dish = new EntranceExitViewModel();
            //dish.EntranceDate = entranceExitlist.EntranceDate;
            //dish.EntranceTime = entranceExitlist.EntranceTime;
            //dish.ExitTime = entranceExitlist.ExitTime;
            //dish.FirstName = entranceExitlist.ApplicationUser.FirstName;
            //dish.LastName = entranceExitlist.ApplicationUser.LastName;
            //if (entranceExitlist == null)
            //{
            //    return NotFound();
            //}
            return View();
        }
        ///_context.EntranceExits.Include(e => e.ApplicationUser)
        /// <summary>
        /// کوئری ورود و خروج هر شخص 
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FilterPerson(string DateFrom, string DateTo)
        {
            var user = await _userManager.GetUserAsync(User);
            if (DateFrom != null && DateTo != null)
            {
                var check = user.UserName;
                if(check!= "mm.940601@gmail.com")
                { 
                  var applicationDbContext = _context.EntranceExits.Include(e => e.ApplicationUser)
                    .Where(s => s.EntranceDate.CompareTo(DateFrom) >= 0 && s.EntranceDate.CompareTo(DateTo) <= 0 && s.ApplicationUserId==user.Id).ToList();
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

                        return PartialView("_ReportGridPerson", entranceExitList);
                    }
                    return BadRequest();
                }
                else
                {
                    var applicationDbContext = _context.EntranceExits.Include(e => e.ApplicationUser)
                       .Where(s => s.EntranceDate.CompareTo(DateFrom) >= 0 && s.EntranceDate.CompareTo(DateTo) <= 0).OrderBy(e => e.ApplicationUser.Email).ToList();
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

                        return PartialView("_ReportGrid", entranceExitList);
                    }

                }
                return BadRequest();
            }

            return BadRequest();
        }
        // GET: EntranceExits/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUseres, "Id", "Id");
            return View();
        }

        // POST: EntranceExits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntranceDate,EntranceTime,ExitTime,ApplicationUserId")] EntranceExit entranceExit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entranceExit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUseres, "Id", "Id", entranceExit.ApplicationUserId);
            return View(entranceExit);
        }

        // GET: EntranceExits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entranceExit = await _context.EntranceExits.SingleOrDefaultAsync(m => m.Id == id);
            if (entranceExit == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUseres, "Id", "Id", entranceExit.ApplicationUserId);
            return View(entranceExit);
        }

        // POST: EntranceExits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EntranceDate,EntranceTime,ExitTime,ApplicationUserId")] EntranceExit entranceExit)
        {
            if (id != entranceExit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entranceExit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntranceExitExists(entranceExit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUseres, "Id", "Id", entranceExit.ApplicationUserId);
            return View(entranceExit);
        }

        // GET: EntranceExits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entranceExit = await _context.EntranceExits
                .Include(e => e.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (entranceExit == null)
            {
                return NotFound();
            }

            return View(entranceExit);
        }

        // POST: EntranceExits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entranceExit = await _context.EntranceExits.SingleOrDefaultAsync(m => m.Id == id);
            _context.EntranceExits.Remove(entranceExit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntranceExitExists(int id)
        {
            return _context.EntranceExits.Any(e => e.Id == id);
        }
    }
}

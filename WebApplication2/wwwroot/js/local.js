//import { debug } from "util";

function Comma(el) {
    Num = el.val();
    Num += '';
    Num = Num.replace(/,/g, '');
    x = Num.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    el.val(x1 + x2)
    //return x1 + x2;
}

var SelectResearch = function () {
    //var sel = $('select')
    $('select').selectpicker({
        //style: 'btn-info',
        size: 4
    });

}
var phonRegExp = /09(0[0-9]|1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}/
function datePickerCol(args) {

    args.element.kendoDatePicker({
        format: "yyyy/MM/dd"
    });

}

var InputNumber = function () {
    $("input.number").focus(function () { $(this).select(); });
    $('input.number').keydown(function (e) {
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode === 8 || e.keyCode === 46) {
            var tk = 2;
        }
        else {
            e.preventDefault();
        }
    });
    $('input.number2').keydown(function (e) {

        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode === 8 || e.keyCode === 46) {
            var tk = 2;
        }
        else {
            e.preventDefault();
        }
    });

    $('input.number').keyup(function (event) {
        //
        // skip for arrow keys
        if (event.which >= 37 && event.which <= 40) {
            event.preventDefault();
        }
        var $this = $(this);
        var num = $this.val().replace(/,/g, '');

        $this.val(num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    });
    $('input.number').keypress(function (event) {

        // skip for arrow keys
        if (event.which >= 37 && event.which <= 40) {
            event.preventDefault();
        }
        var $this = $(this);
        var num = $this.val().replace(/,/g, '');

        $this.val(num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    });
    $('input.number').blur(function () {

        var $this = $(this);
        var num = $this.val().replace(/,/g, '');

        if (num.length > 1 && toNumSep(num) === 0) {
            $this.val(0);
        }
        else
            $this.val(num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    });

    $("input.number3").focus(function () { $(this).select(); });
    $('input.number3').keydown(function (e) {
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode === 8 || e.keyCode === 46 || e.keyCode === 109) {
            var tk = 2;
        }
        else {
            e.preventDefault();
        }
    });
    $('input.number3').keypress(function (event) {
        var $this = $(this);        
        var ng = "";
        var val = $this.val();
        if (val[0] === "-") {
            ng = "-";
            val = val.substring(1, val.length);
        }
        if (event.which === 45) {
            if (ng === "-") {
                ng = "";
            } else {
                ng = "-";
            }
        }
        var num = val.replace(/,/g, '');
        if (num.length === 0) {
            ng = "";
        }
        $this.val(ng+num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    });
    $('input.number3').keyup(function (event) {
        var $this = $(this);
        var val = $this.val();
        $this.css("direction", "ltr").css("text-align", "right");


        if (event.which === 109) {
            
            $this.val(val.substring(0, val.length - 1));
            //event.preventDefault();
        }
        else {
        
            var num = val.replace(/,/g, '');
            $this.val(num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
        }
    });


    var allInputNumber = $('input.number');
    allInputNumber.each(function () {
        $(this).val(NumberSeprat($(this).val()))
    });

}

function NumberSeprat(val) {

    //val = val.toString();
    //var num = val.replace(/,/g, '');
    //return num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");

    val = val.toString();
    var ng = "";
    if (val[0] === "-") {
        ng = "-";
        val = val.substring(1, val.length);
    }
    var num = val.replace(/,/g, '');
    if (num.length === 0) {
        ng = "";
    }
    return(ng + num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
}

//ارسال جهت 
var pdfFileName = null;
var pdfDownload = null;
function exportPdf(grid, reportCat, reportTitle, urlDetails) {
    pdfFileName = reportCat;
    pdfDownload = null;
    var Colomns = [];
    grid.columns.forEach(function (item) {
        Colomns.push({
            Field: item.field,
            Title: item.title,
            Hidden: item.hidden
        });
    });

    var model = {
        colomns: Colomns,
        reportCat: reportCat,
        reportTitle: reportTitle,
        urlDetails: urlDetails
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: 'General/ExportPdf/',
        contentType: "application/json"
    }).done(function (res) {

        pdfDownload = document.body.appendChild(
            document.createElement("a")
        );
        pdfDownload.download = pdfFileName + ".pdf";
        pdfDownload.href = "data:text/plain;base64," + res;
        pdfDownload.setAttribute('class', 'Displaynone')
        pdfDownload.innerHTML = "download";
        pdfDownload.click();
        //console.log('res', res);
        // Do something with the result :)
    }).fail(function (e) {

        alert('File download failed!');
    });

}

function retunDateNow() {
    var t = new JalaliDate();
    var arr = t.jalalidate
    if (arr[1] === 12) {
        arr[1] = 0;
    }
    arr[1] += 1;
    arr[0] = arr[0].toString();
    arr[1] = arr[1] < 10 ? "0" + arr[1].toString() : arr[1].toString();
    arr[2] = arr[2] < 10 ? "0" + arr[2].toString() : arr[2].toString();
    return arr.join('/');
}

function datePickerPosition(name) {

    if (name !== "") {
        //    var off = $('#' + name ).offset();
        var t = $('#' + name + '_dateview');
        //    var w = ($('#pnlShowSharjbook').width() - off.left) - 183
        //    var tp = off.top - $('html').scrollTop()
        //    t.css('top',  tp + 'px').css('transform', 'translateY(0px)').css('left', '-' + w + 'px');
        //    //$('#' + name ).parent().parent()[0]
        //    $('#' + name).parent().parent().after(t);

        var tp = $('#Header_BillDate').offset().top - $('html').scrollTop();

        t.css('top', tp + 'px')
        $('.' + name).after(t);
        //t.css('position', 'relative!important')
    }
}

var customOptions = {
    placeholder: "روز / ماه / سال"
    , twodigit: false
    , closeAfterSelect: false
    , nextButtonIcon: "../images/icon/next.png" //"fa fa-arrow-circle-right"
    , previousButtonIcon: "../images/icon/previous.png"
    , buttonsColor: "blue"
    , forceFarsiDigits: true
    , markToday: true
    , markHolidays: true
    , highlightSelectedDay: true
    , sync: true
    , gotoToday: true
}

function toNum(val) {
    try {
        val = parseInt(val);
    } catch (e) {
        val = 0;
    }
    return val;
}

function toNumSep(val) {
    try {
        val = val.replace(/,/g, '');
        val = val === "" ? 0 : val
        val = parseInt(val);
    } catch (e) {
        val = 0;
    }
    return val;
}

function billTypeManager(id, role) {

    var btn = role === "Manager" ? "<button type='button' title='حذف' onClick='BillTypeDelete($(this))' data-apNo='" + id + "'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button>" : "";
    return btn;
}
function costTypeManager(id, role) {

    var btn = role === "Manager" ? "<button type='button' title='حذف' onClick='CostTypeDelete($(this))' data-apNo='" + id + "'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button>" : "";
    // var btnv = role == "Manager" ? "<button type='button' title='ویرایش نوع قبض' data-toggle='modal' data-target='\\#costTypeInfoModal'  data-apNo='" + id + "' class=' k-icon k-i-edit innerButton'>" : "";
    return btn;
}
function incomeManager(id, role) {

    var btn = role === "Manager" ? "<button type='button' title='حذف' onClick='IncomeDelete($(this))' data-apNo='" + id + "'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button> " : "";
    return btn;
}
function invoceManager(id, role) {

    var btn = role === "Manager" ? " <button type='button' title='حذف' onClick='InvoiceDelete($(this))' data-apNo='" + id + "'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button>" : "";
    return btn;
}
function receiptManager(id, role) {
   
    var btn = role === "Manager" ? " <button type='button' title='حذف' onClick='ReceiptDelete($(this))' data-apNo='" + id + "'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button>" : "";
    return btn;
}

function billManager(id, role) {

    var btn = role === "Manager" ? " <button type='button' title='حذف' onClick='BillDelete($(this))' data-apNo='" + id +"'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button>" : "";
    return btn;
}

function personManager(id, role) {

    var btn = role === "Manager" ? " <button type='button' title='حذف' onClick='deletePersonModal($(this))' data-apNo='" + id + "'  class='k-button k-button-icontext k-grid-first'><i class='material-icons'>delete</i></button>" : "";
    return btn;
}

function billanManager(id, role, Status) {
    
    if (Status.toString() === "true") {
        Status = " checked='checked' ";
    } else {
        Status = "";

    }
    var idStr = "";
    var forStr = "";

    if (role === "Manager") {
        idStr = "id='Status_" + id + "'";
        forStr = "for='Status_" + id + "'";
    }
    var action = role === "Manager" ?
        " onclick='StatusClick($(this))' " :
        " disabled ";
    var btn = "<input  type='checkbox' " + action + " class='filled-in chk-col-black' " + idStr + " " + Status + "  > <label " + forStr+"></label>";
    return btn;

    //
}

function DisabledItems(id) {
    $('#' + id +' input').attr("disabled", "disabled");
    //$('#' + id + 'select').attr("disabled", "disabled");
    var dl = $('#' + id + ' [data-role="dropdownlist"]').data("kendoDropDownList");
    try {
        dl.enable(false);
    } catch (e) {
        var tk = 2;
    } 
    
}


//var PeymentErrorList = [
//    { id=0, err = 'تراكنش با موفقيت انجام شد ' },
//    { id=11, err = 'شماره كارت نامعتبر است ' },
//    { id=12, err = 'موجودي كافي نيست ' },
//    { id=13, err = 'رمز نادرست است ' },
//    { id=14, err = 'تعداد دفعات وارد كردن رمز بيش از حد مجاز است ' },
//    { id=15, err = 'كارت نامعتبر است ' },
//    { id=16, err = 'دفعات برداشت وجه بيش از حد مجاز است ' },
//    { id=17, err = 'كاربر از انجام تراكنش منصرف شده است ' },
//    { id=18, err = 'تاريخ انقضاي كارت گذشته است ' },
//    { id=19, err = 'مبلغ برداشت وجه بيش از حد مجاز است ' },
//    { id=111, err = 'صادر كننده كارت نامعتبر است ' },
//    { id=112, err = 'خطاي سوييچ صادر كننده كارت ' },
//    { id=113, err = 'پاسخي از صادر كننده كارت دريافت نشد ' },
//    { id=114, err = 'دارنده كارت مجاز به انجام اين تراكنش نيست ' },
//    { id=21, err = 'پذيرنده نامعتبر است ' },
//    { id=23, err = 'خطاي امنيتي رخ داده است ' },
//    { id=24, err = 'اطلاعات كاربري پذيرنده نامعتبر است ' },
//    { id=25, err = 'مبلغ نامعتبر است ' },
//    { id=31, err = 'پاسخ نامعتبر است ' },
//    { id=32, err = 'فرمت اطلاعات وارد شده صحيح نمي باشد ' },
//    { id=33, err = 'حساب نامعتبر است ' },
//    { id=34, err = 'خطاي سيستمي ' },
//    { id=35, err = 'تاريخ نامعتبر است ' },
//    { id=41, err = 'شماره درخواست تكراري است ' },
//    { id=42, err = 'تراكنش Saleيافت نشد ' },
//    { id=43, err = 'قبلا درخواست Verifyداده شده است ' },
//    { id=44, err = 'درخواست Verfiyيافت نشد ' },
//    { id=45, err = 'تراكنش Settleشده است ' },
//    { id=46, err = 'تراكنش Settleنشده است ' },
//    { id=47, err = 'تراكنش Settleيافت نشد ' },
//    { id=48, err = 'تراكنش Reverseشده است ' },
//    { id=49, err = 'تراكنش Refundيافت نشد ' },
//    { id=412, err = 'شناسه قبض نادرست است ' },
//    { id=413, err = 'شناسه پرداخت نادرست است ' },
//    { id=414, err = 'سازمان صادر كننده قبض نامعتبر است ' },
//    { id=415, err = 'زمان جلسه كاري به پايان رسيده است ' },
//    { id=416, err = 'خطا در ثبت اطلاعات ' },
//    { id=417, err = 'شناسه پرداخت كننده نامعتبر است ' },
//    { id=418, err = 'اشكال در تعريف اطلاعات مشتري ' },
//    { id=419, err = 'تعداد دفعات ورود اطلاعات از حد مجاز گذشته است ' },
//    { id=421, err = 'IPنامعتبر است ' },
//    { id=51, err = 'تراكنش تكراري است ' },
//    { id=54, err = 'تراكنش مرجع موجود نيست ' },
//    { id=55, err = 'تراكنش نامعتبر است ' },
//    { id=6, err = 'خطا در واريز ' }

//    ];
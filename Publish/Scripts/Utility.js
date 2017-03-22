var ajaxLoadingDiv = "<div class='ajaxLoader' style='position:fixed; top:0px; right:45% '><div id='divLoading' ><span id='spanLoading'>Loading...</span></div></div>";

function getErrorMessage(message) {
    return "<label id='lblMsg' style='font-size:12pt;font-weight:bold;color:Red;'>" + message + "</label>";
}

function getSuccessMessage(message) {
    return "<label id='lblMsg' style='font-size:12pt;font-weight:bold;color:Green;'>" + message + "</label>";
}

function setTitle(title) {
    $('#lblCaption').html(title);
}

function executeAction(formName, actionURL, targetDiv) {
    targetDiv = "#" + targetDiv;
    //var ajaxLoading = "<img id='ajax-loader' src='../../Content/ajax-loader.gif' align='left' height='28' width='28'>";
    var ajaxLoading = ajaxLoadingDiv;
    // $(targetDiv).append(ajaxLoading);

    var url = actionURL;
    var form = $("#" + formName);
    var serializedForm = form.serialize();


    $.post(url, serializedForm, function (result) { $(targetDiv).html(result); }, "html");

    return false;
}

function executeCustomAction(data, actionURL, targetDiv) {


    targetDiv = "#" + targetDiv;

    //var ajaxLoading = "<img id='ajax-loader' src='../../Content/ajax-loader.gif' align='left' height='28' width='28'>";
    //$(targetDiv).append("<p>" + ajaxLoading + " Loading...</p>");

    var ajaxLoading = "<div style='position:fixed; top:0px; right:45% '><div id='divLoading' ><span id='spanLoading'>Loading...</span></div></div>";
    //$(targetDiv).append(ajaxLoading);

    var url = actionURL;

    $.post(url, data, function (result) { $(targetDiv).html(result); }, "html");

    return false;
}

function executeCustomActionGet(data, actionURL, targetDiv) {


    targetDiv = "#" + targetDiv;

    //var ajaxLoading = "<img id='ajax-loader' src='../../Content/ajax-loader.gif' align='left' height='28' width='28'>";
    //$(targetDiv).append("<p>" + ajaxLoading + " Loading...</p>");

    var ajaxLoading = "<div style='position:fixed; top:0px; right:45% '><div id='divLoading' ><span id='spanLoading'>Loading...</span></div></div>";
    //$(targetDiv).append(ajaxLoading);

    var url = actionURL;

    $.post(url, data, function (result) { alert(result); $(targetDiv).html(result); }, "html");

    return false;
}


function FormatTextBox() {
    initIntegerFields();
    initDoubleFields();
    initMetacharactersFilter();
    //$("input.double,input.doubleNR").format({ precision: 2, allow_negative: true, autofix: true });
    $("input.date,input.dateNR").keypress(function fn() { allowValidDate(event, this.value); });
    $("input.readonly").attr("readonly", "true");
}

function fnDisableEditMode() {
    $("input[type='text' ],  textarea").attr('disabled', 'disabled');
    $("input[type!='submit' ],  textarea").css('background-color', '#F2F5F8');
    $("input[type='file' ]").attr('disabled', 'disabled');
    $(":checkbox,select,:radio").attr('disabled', 'disabled');
    $(":submit,:button").hide();
    $("label.mandatory").hide();
    $("img[src *='datetimePicker']").hide();
    $("input[type ='image']").hide();
    $(".ui-datepicker-trigger").hide();// hide datepicker
    //$("input[id$='btnUpdate']").show().attr('disabled', '');

}

function fnRemoveDisableMode(Id) {
    $("#"+Id).show();
    $("#" + Id).removeAttr('disabled');
    $("#"+Id).css('background-color', '');
    $("#" + Id).removeAttr('readonly');
    $("#" + Id).removeClass('read-only');
}

function fnEnableDisableMode(Id) {
    $("#" + Id).attr('disabled', true);
    $("#" + Id).addClass('read-only');
}

function fnDisableEditModeWithoutBtn() {
    $("input[type!='submit' ],  textarea").attr('readonly', 'true');
    $(":checkbox,select,:radio").attr('disabled', 'disabled');

    $("label.mandatory").hide();
    $("img[src *='datetimePicker']").hide();
}

// Move cursor to the first control of a page
function focusFirstControl() {
    $(function() {
        $("form:not(.filter) :input:visible:enabled:not([readonly]):first").focus();
    });
}

//Initializing integer fields
function initIntegerFields() {

    $("input.integer, input.integerNR").keydown(function (event) {
        // Allow backspace, delete, >, <, enter, tab, etc.
        if ((event.keyCode == null) || (event.keyCode == 0) || (event.keyCode == 8) || (event.keyCode == 9) || (event.keyCode == 13) || (event.keyCode == 27) || (event.keyCode == 33) || (event.keyCode == 34) || (event.keyCode == 35) || (event.keyCode == 36) || (event.keyCode == 37) || (event.keyCode == 38) || (event.keyCode == 39) || (event.keyCode == 40) || (event.keyCode == 46)) {
        }
        else {
            // Ensure that it is a number and stop the keypress
            if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105)) {
            }
            else {
                event.preventDefault();
            }
        }
    });
}

//Initializing Double fields
function initDoubleFields() {

    $("input.double,input.doubleNR,input.doubleNZ").keydown(function(event) {
        // Allow backspace, delete, >, <, enter, tab, etc.

        if ((event.keyCode == null) || (event.keyCode == 0) || (event.keyCode == 8) || (event.keyCode == 9) || (event.keyCode == 13) || (event.keyCode == 27) || (event.keyCode == 33) || (event.keyCode == 34) || (event.keyCode == 35) || (event.keyCode == 36) || (event.keyCode == 37) || (event.keyCode == 38) || (event.keyCode == 39) || (event.keyCode == 40) || (event.keyCode == 46) || (event.keyCode == 190) || (event.keyCode == 110)) {

            if ((event.keyCode == 190) || (event.keyCode == 110)) {
                var fieldValue = this.value;
                var fieldArray = fieldValue.split( /\./ );
                if (fieldArray.length > 1) {
                    event.preventDefault();
                }
            }
        } else {
            // Ensure that it is a number and stop the keypress
            if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105)) {
            } else {
                event.preventDefault();
            }
        }
    });




    //Initializing integer fields
    //    function initDoubleFieldsNEG() {

    //    $("input.doubleNEG").keydown(function (event) {
    //        // Allow backspace, delete, >, <, enter, tab, etc.
    //        
    //        if ((event.keyCode == null) || (event.keyCode == 0) || (event.keyCode == 8) || (event.keyCode == 9) || (event.keyCode == 13) || (event.keyCode == 27) || (event.keyCode == 33) || (event.keyCode == 34) || (event.keyCode == 35) || (event.keyCode == 36) || (event.keyCode == 37) || (event.keyCode == 38) || (event.keyCode == 39) || (event.keyCode == 40) || (event.keyCode == 46) || (event.keyCode == 190) || (event.keyCode == 110)) {

    //            if ((event.keyCode == 190) || (event.keyCode == 110)) {
    //                var fieldValue = this.value;
    //                var fieldArray = fieldValue.split(/\./);
    //                if (fieldArray.length > 1) {
    //                    event.preventDefault();
    //                }
    //            }
    //        }
    //        else {
    //            // Ensure that it is a number and stop the keypress
    //            if (event.keyCode >= 45 || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105)) {
    //            }
    //            else {
    //                event.preventDefault();
    //            }
    //        }
    //    });


    $("input.double,input.doubleNR").click(function(event) {
        this.select();
    });
}


//Initializing Double fields
function initMetacharactersFilter() {

    $("input.filter,input.filterNR").keyup(function(e) {
        // Allow backspace, delete, >, <, enter, tab, etc.

        var keynum;
        var keychar;
        var numcheck;
        // For Internet Explorer
        if (window.event) {
            keynum = e.keyCode;
        }
        // For Netscape/Firefox/Opera
        else if (e.which) {
            keynum = e.which;
        }

        keychar = String.fromCharCode(keynum);
        //List of special characters you want to restrict
        if (keychar == "'" || keychar == "`" || keychar == "!" || keychar == "@" || keychar == "#" || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&" || keychar == "*" || keychar == "(" || keychar == ")" || keychar == "-" || keychar == "_" || keychar == "+" || keychar == "=" || keychar == "/" || keychar == "~" || keychar == "<" || keychar == ">" || keychar == "," || keychar == ";" || keychar == ":" || keychar == "|" || keychar == "?" || keychar == "{" || keychar == "}" || keychar == "[" || keychar == "]" || keychar == "¬" || keychar == "£" || keychar == '"' || keychar == "\\") {
            e.preventDefault();
        } else {
            return true;
        }

    });
}
function allowOnlyNumbers(evt) {

    var keyCode = "";

    if (window.event) {
        keyCode = window.event.keyCode;
        evt = window.event;
    }
    else if (evt) {
        keyCode = evt.which;
    }
    else {
        return true;
    }

    if ((keyCode == null) || (keyCode == 0) || (keyCode == 8) || (keyCode == 9) || (keyCode == 13) || (keyCode == 27)) {
        return true;
    }

    if ((keyCode < 48 || keyCode > 57)) {
        evt.returnValue = false;
        return false;
    }
}

//-- check decimal value --//
function allowDecimalValue(evt, fieldValue) {

    var keyCode = "";
    if (window.event) {
        keyCode = window.event.keyCode;
        evt = window.event;
    }
    else if (evt) keyCode = evt.which;
    else return true;

    fieldArray = fieldValue.split(""); //take every character of field value in array

    //if((keyCode==null) || (keyCode==0) || (keyCode==8) || (keyCode==9) || (keyCode==13) || (keyCode==27) )

    if ((keyCode > 47 && keyCode < 58) || (keyCode == 45) || (keyCode == 46)) {
        if ((keyCode == 46) || (keyCode == 45)) {
            for (var x = 0; x <= fieldValue.length; x++) {
                if ((keyCode == 46) && (fieldArray[x] == ".")) {
                    evt.returnValue = false;
                    return false;
                }
                else if ((keyCode == 45) && (fieldArray[x] == "-")) {
                    evt.returnValue = false;
                    return false;
                }
            }
        }
        return true;
    }
    else {
        evt.returnValue = false;
        return false;
    }
}

function limitText(limitField, limitNum) {
    if (limitField.value.length > limitNum) {
        limitField.value = limitField.value.substring(0, limitNum);
    }
}

//-- checks the length of a text field --//
function checkFieldLength(evt, fieldValue, maxLen) {
    var keyCode = "";
    if (window.event) {
        keyCode = window.event.keyCode;
        evt = window.event;
    }

    if (fieldValue.length >= maxLen) {
        evt.returnValue = false;
        return false;
    }
    //    else if(fieldValue.length >25) // chk the max length of a word
    //    {
    //          strArray = fieldValue.split(" ");
    //          if(strArray[strArray.length -1].length >= 26)
    //          {    
    //              if(keyCode != 32 && keyCode != 13 && keyCode != 10)
    //              {
    //                    evt.returnValue = false;
    //                    return false;
    //              }                
    //          }          
    //    }
    else return true;
}

//-- checks valid key for a date field --//
function allowValidDate(evt, fieldValue) {
    var keyCode = "";
    if (window.event) {
        keyCode = window.event.keyCode;
        evt = window.event;
    }
    else if (evt) keyCode = evt.which;
    else return true;
    dateArray = fieldValue.split(""); //take every character of field value in array

    if (fieldValue.length == 0) {
        if ((keyCode < 48 || keyCode > 51))// (0-3)
        {
            evt.returnValue = false;
            return false;
        }
    }
    if (fieldValue.length == 1) {
        // If first digit of date is '3' then second digit should be (0-1)  
        if (dateArray[0] == "3") {
            if (keyCode < 48 || keyCode > 49)// (0-1)
            {
                evt.returnValue = false;
                return false;
            }
        }
        // If first digit of date is '0' then second digit should not be '0'
        if (dateArray[0] == "0") {
            if (keyCode == 48)// (0)
            {
                evt.returnValue = false;
                return false;
            }
        }
        else if ((keyCode < 48 || keyCode > 57))//(0-9)
        {
            evt.returnValue = false;
            return false;
        }
    }
    if (fieldValue.length == 2) {
        if ((keyCode != 45))// (-)
        {
            evt.returnValue = false;
            return false;
        }
    }
    if (fieldValue.length == 3) {
        if ((keyCode < 48 || keyCode > 49))//(0-1)
        {
            evt.returnValue = false;
            return false;
        }
    }
    if (fieldValue.length == 4) {
        // If first digit of month is '1' then second digit should be (0-2) 
        if (dateArray[3] == "1") {
            if (keyCode < 48 || keyCode > 50)// (0-1)
            {
                evt.returnValue = false;
                return false;
            }
        }
        // If first digit of month is '0' then second digit should not be '0' 
        if (dateArray[3] == "0") {
            if (keyCode == 48)// (0-1)
            {
                evt.returnValue = false;
                return false;
            }
        }
        if ((keyCode < 48 || keyCode > 57))//(0-9)
        {
            evt.returnValue = false;
            return false;
        }
    }
    if (fieldValue.length == 5) {
        if ((keyCode != 45))// (-)
        {
            evt.returnValue = false;
            return false;
        }
    }
    if ((fieldValue.length > 5) && (fieldValue.length < 10)) {
        if ((keyCode < 48 || keyCode > 57))//(0-9)
        {
            evt.returnValue = false;
            return false;
        }
    }
    // check if year is not less then 0001
    if (fieldValue.length == 9) {
        if (dateArray[6] == "0" && dateArray[7] == "0" && dateArray[8] == "0") {
            if (keyCode == 48) {
                evt.returnValue = false;
                return false;
            }
        }
    }
    // check if field length is greater than 9
    if (fieldValue.length > 9) {
        evt.returnValue = false;
        return false;
    }

    else return true;
}

function CheckAnyValidDate(date)// dd-mm-yyyy 
{
    var dateValid = true;

    try {
        var dateSeperator = "";
        //See what the character is that seperates the date parts...
        if (date.indexOf("-") > 0) {
            dateSeperator = "-";
        }

        else {
            //if it's not one of the ones listed above, then we don't have a valid date...
            dateValid = false;
        }

        var dateArray = new Array(5);
        dateArray = date.split(dateSeperator);

        if (dateArray[0].length > 2) {
            dateValid = false;
        }
        if (dateArray[1].length > 2) {
            dateValid = false;
        }
        if (dateArray[2].length != 4) {
            dateValid = false;
        }

        //If any of the parts aren't numbers, then we don't have a date
        if (isNaN(dateArray[0]) || isNaN(dateArray[1]) || isNaN(dateArray[2])) {
            dateValid = false;
        }

        var iDate = parseInt(dateArray[0], 10);
        var iMonth = parseInt(dateArray[1], 10);
        var iYear = parseInt(dateArray[2], 10);

        //If the year is before 1900 it's not valid...
        if (iYear < 1900) {
            dateValid = false;
        }
        //Make sure our month is in range...
        else if (iMonth < 0 || iMonth > 12) {
            dateValid = false;
        }
        //Jan, Mar, May, July, Aug, Oct and Dec must have between 1 and 31 days...
        else if ((iMonth == 1 || iMonth == 3 || iMonth == 5 || iMonth == 7 || iMonth == 8 || iMonth == 10 || iMonth == 12)
				   && (iDate < 0 || iDate > 31)) {
            dateValid = false;
        }
        //April, June, Sept, and Nov must have between 1 and 30 days...
        else if ((iMonth == 2 || iMonth == 6 || iMonth == 9 || iMonth == 11)
				   && (iDate < 0 || iDate > 30)) {
            dateValid = false;
        }
        //Feb must have 28 days unless it's a leap year. If the year is evenly divisable by 4, then we're in a leap year
        //NOTE: that this will fail for the year 2100, since 2100 is not a leap century
        //			(even though we really don't have to worry about this yet)
        else if ((iMonth == 2) && (iYear % 4 == 0) && (iDate < 0 || iDate > 29)) {
            dateValid = false;
        }
        //Now we handle non-leap year Feb's...
        else if ((iMonth == 2) && (iYear % 4 != 0) && (iDate < 0 || iDate > 28)) {
            dateValid = false;
        }

    }
    catch (e) {
        dateValid = false;
    }
    return dateValid;
}

function CheckValidCurrentDate(date, currentyear, currentmonth, currentday)// check valid current date: dd-mm-yyyy
{

    var dateValid = true;

    try {
        var dateSeperator = "";
        //See what the character is that seperates the date parts...
        if (date.indexOf("-") > 0) {
            dateSeperator = "-";
        }
        else {
            //if it's not one of the ones listed above, then we don't have a valid date...
            dateValid = false;
        }

        var dateArray = new Array(5);
        dateArray = date.split(dateSeperator);

        if (dateArray[0].length > 2) {
            dateValid = false;
        }
        if (dateArray[1].length > 2) {
            dateValid = false;
        }
        if (dateArray[2].length != 4) {
            dateValid = false;
        }

        //If any of the parts aren't numbers, then we don't have a date
        if (isNaN(dateArray[0]) || isNaN(dateArray[1]) || isNaN(dateArray[2])) {
            dateValid = false;
        }

        var iDate = parseInt(dateArray[0], 10);
        var iMonth = parseInt(dateArray[1], 10);
        var iYear = parseInt(dateArray[2], 10);

        //If the year is before 1900 or later than the current date, it's not valid...
        if (iYear < 1900 || iYear > currentyear) {
            dateValid = false;
        }
        //Make sure our month is in range...
        else if (iMonth < 0 || iMonth > 12) {
            dateValid = false;
        }
        //Jan, Mar, May, July, Aug, Oct and Dec must have between 1 and 31 days...
        else if ((iMonth == 1 || iMonth == 3 || iMonth == 5 || iMonth == 7 || iMonth == 8 || iMonth == 10 || iMonth == 12)
				   && (iDate < 0 || iDate > 31)) {
            dateValid = false;
        }
        //April, June, Sept, and Nov must have between 1 and 30 days...
        else if ((iMonth == 2 || iMonth == 6 || iMonth == 9 || iMonth == 11)
				   && (iDate < 0 || iDate > 30)) {
            dateValid = false;
        }
        //Feb must have 28 days unless it's a leap year. If the year is evenly divisable by 4, then we're in a leap year
        //NOTE: that this will fail for the year 2100, since 2100 is not a leap century
        //			(even though we really don't have to worry about this yet)
        else if ((iMonth == 2) && (iYear % 4 == 0) && (iDate < 0 || iDate > 29)) {
            dateValid = false;
        }
        //Now we handle non-leap year Feb's...
        else if ((iMonth == 2) && (iYear % 4 != 0) && (iDate < 0 || iDate > 28)) {
            dateValid = false;
        }
        else {
            //Now see if we can create a Date object with our date parts. If so, then we have a valid date...
            //			var validDate:Date = new Date(iYear, iMonth, iDate);
            //check for current date
            if ((iYear == currentyear && iMonth > currentmonth) || (iYear == currentyear && iMonth == currentmonth && iDate > currentday)) {
                dateValid = false;
            }
        }
    }
    catch (e) {
        dateValid = false;
    }
    return dateValid;
}


//adds comma to a currency type value
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function ThousandSeparator(Value, digit) {
    decimalDigits = 2;
    if (digit != null) {
        decimalDigits = digit;
    }

    // Separator Length. Here this is thousand separator 
    var separatorLength = 3;
    var OriginalValue = Value;
    var TempValue = "" + OriginalValue;
    var NewValue = "";

    // Store digits after decimal 
    var pStr;

    // store digits before decimal 
    var dStr;

    // Add decimal point if it is not there 
    if (TempValue.indexOf(".") == -1) {
        TempValue += ".";
    }

    dStr = TempValue.substr(0, TempValue.indexOf("."));

    pStr = TempValue.substr(TempValue.indexOf(".")); // Add "0″ for remaining digits after decimal point 
    while (pStr.length - 1 < decimalDigits) { pStr += "0";
    }

    if (pStr == '.')
        pStr = "";

    if (dStr.length > separatorLength) {
        // Logic of separation 
        while (dStr.length > separatorLength) {
            NewValue = "," + dStr.substr(dStr.length - separatorLength) + NewValue;
            dStr = dStr.substr(0, dStr.length - separatorLength);
            separatorLength = 2;
        }
        NewValue = dStr + NewValue;
    }
    else {

        NewValue = dStr;
    }
    // Add decimal part 
    NewValue = NewValue + pStr;
    // Show Final value 

    var x = "";
    x = NewValue.substr(NewValue.indexOf("."));
    var y = NewValue.replace(x, "");
    if (x.length > decimalDigits + 1)
        x = x.substr(0, decimalDigits + 1);
    y = y.replace("-,", "-");
    return NewValue = y + x;
}

//Disables the controls for view mode
function disablePageControls() {

    var url = window.location;
    url = url + "";
    var a = url.indexOf("view");
    if (a > 0) {
        $("input[type!='submit' ],  textarea").attr('readonly', 'true');
        $(":checkbox,select,:radio,:button,:submit").attr('disabled', 'disabled');

        $("label.mandatory").hide();
        $("input[type ='image']").hide();

        $("input[id$='btnBack']").show().attr('disabled', '');
    }
}

function GetAjaxCall(strFormName, strUrl, strDiv) {
    executeAction(strFormName, strUrl, strDiv);
    return false;
}

function GetAjaxCallWithAction(strUrl, strDiv, act) {
    $.ajax({
        url: strUrl,
        type: act,
        dataType: 'text',
        timeout: 5000,
        error: function () {
            alert('System is unable to load data please try again.');
        },
        success: function (result) {
            $('#' + strDiv).html(result);

        }
    });

    return false;
}

function ismaxlengthPop(obj) {
    var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : "";
    if (obj.getAttribute && obj.value.length > mlength)
        obj.value = obj.value.substring(0, mlength);
}

function preventSubmitOnEnter(theForm) {
    $(theForm).bind("keypress", function (e) {
        if (e.keyCode == 13) {
            var $targ = $(e.target);

            if (!$targ.is("textarea") && !$targ.is(":button,:submit")) {
                var focusNext = false;
                $(this).find(":input:visible:not([disabled],[readonly]), a").each(function () {
                    if (this === e.target) {
                        //focusNext = true;
                        focusNext = false; //Preventing TAB will be decided later.
                    }
                    else if (focusNext) {
                        $(this).focus();
                        return false;
                    }
                });

                return false;
            }

        }
    });
}

function roundNumber(num, dec) {
    var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
    return result;
}

function MathRound(val, position) {
    var temp = Math.pow(10, position) * 1.0;
    var returnVal = Math.round(temp * val) / temp;
    return returnVal;
}

function replaceCharacters(origString, inChar, outChar) {
    var newString = origString.split(inChar);
    newString = newString.join(outChar);
    return newString;
}

//Rasel 09 Oct 2012
function getNextElement(field) {
    var form = $(field.form).find(":input");
    for (var e = 0; e < form.length; e++) {
        if (field == form[e])
            break;
    }
    return form[++e % form.length];
} 


$.fn.fillSelect = function (data) {
    return this.clearSelect().each(function () {
        if (this.tagName == 'SELECT') {
            var dropdownList = this;
            $.each(data, function (index, optionData) {
                var option = new Option(optionData.Text, optionData.Value, optionData.Selected, optionData.Selected);
                //                    if (optionData.Selected == true) {
                //                        alert('dfdf');
                //                        option.attr('selected', 'selected');
                //                    }
                if ($.browser.msie) {
                    dropdownList.add(option);
                }
                else {
                    dropdownList.add(option, null);
                }
            });
        }
    });
};
$.fn.clearSelect = function () {
    return this.each(function () {
        if (this.tagName == 'SELECT')
            this.options.length = 0;
    });
};

function dateToDDMMYY(date) {
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
   // return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
    return '' + (d <= 9 ? '0' + d : d) + '-' + (m <= 9 ? '0' + m : m) + '-' + y;
}


function JSON2CSV(objArray) {
    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '';
    var line = '';

    for (var i = 0; i < array.length; i++) {
        var line = '';
        
        for (var index in array[i]) {
            var value = array[i][index] + "";
            value = value.replace( /\,/g , ';');  //->replaces all occurance //value.replace(',', ';'); -> replaces only 1st occurance
            line += value + ',';
        }

        line = line.slice(0, -1);
        str += line + '\r\n';
    }
    return str;
}

/* //Original function
function JSON2CSV(objArray) {
    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '';
    var line = '';

    if ($("#labels").is(':checked')) {

        var head = array[0];
        if ($("#quote").is(':checked')) {
            for (var index in array[0]) {
                var value = index + "";
                line += '"' + value.replace(/"/g, '""') + '",';
            }
        } else {
            for (var index in array[0]) {
                line += index + ',';
            }
        }

        line = line.slice(0, -1);
        str += line + '\r\n';
    }

    for (var i = 0; i < array.length; i++) {
        var line = '';

        if ($("#quote").is(':checked')) {
            for (var index in array[i]) {
                var value = array[i][index] + "";
                line += '"' + value.replace(/"/g, '""') + '",';
            }
        } else {
            for (var index in array[i]) {
                line += array[i][index] + ',';
            }
        }

        line = line.slice(0, -1);
        str += line + '\r\n';
    }
    return str;
}
//Modified function
function JSON2CSV(objArray, isLabelCheck = false, isQouteCheck = false) {
    isLabelCheck = typeof a !== 'undefined' ? isLabelCheck : false;
    isQouteCheck = typeof a !== 'undefined' ? isQouteCheck : false;

    //console.log(objArray);
    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '';
    var line = '';
    
    if (isLabelCheck) { //if ($("#labels").is(':checked')) {
        console.log(array);
        var head = array[0];
        if (isQouteCheck) { //if ($("#quote").is(':checked')) {
            for (var index in array[0]) {
                var value = index + "";
                line += '"' + value.replace(/"/g, '""') + '",';
            }
        } else {
            for (var index in array[0]) {
                line += index + ',';
            }
        }

        line = line.slice(0, -1);
        str += line + '\r\n';
    }

    for (var i = 0; i < array.length; i++) {
        var line = '';

        if (isQouteCheck) { //if ($("#quote").is(':checked')) {
            for (var index in array[i]) {
                var value = array[i][index] + "";
                line += '"' + value.replace(/"/g, '""') + '",';
            }
        } else {
            for (var index in array[i]) {
                //does not Handle comma values
                //line += array[i][index] + ',';

                //Handles comma values
                var value = array[i][index] + "";
                value = value.replace( /\,/g , ';');  //->replaces all occurance //value.replace(',', ';'); -> replaces only 1st occurance
                line += value + ',';
            }
        }

        line = line.slice(0, -1);
        str += line + '\r\n';
    }
    return str;
}
*/


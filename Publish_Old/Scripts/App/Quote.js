//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Quote Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddQuoteForm() {
    $('#QuoteNo').val('');
    $('#Organization').val('');
    $('#CustomerId').val('');
    $('#CusContactId').val('');
    $('#ArticleCode').val('');
    $('#QuoteDetailsKendoGrid').data('kendoGrid').dataSource.data([]);

}

function AddQuoteSuccess() {
    if ($("#updateTargetId").html() == "True") {
        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddQuoteForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Quote Success Function
function EditQuoteSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editQuoteDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddQuoteForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Quote Success Function
function DeleteQuoteSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteQuoteDialog').dialog('close');

        SuccessMessage("Information has been deleted successfully");

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------


//-----------------------------------------------------
//start Refresh Kendo Grid Funtion
function dtKendoGridRefresh() {
    //Get Quote Grid
    var dGrid = $('#QuoteKendoGrid').data('kendoGrid');
    dGrid.dataSource.read();
}
//end Refresh Kendo Grid Funtion
//-----------------------------------------------------


//---Move focus on next field when enter is pressed
// register jQuery extension
jQuery.extend(jQuery.expr[':'], {
    focusable: function (el, index, selector) {
        return $(el).is('a, button, :input, [tabindex]');
    }
});

$(document).on('keypress', 'input,select', function (e) {
    if (e.which == 13) {
        e.preventDefault();
        // Get all focusable elements on the page
        var $canfocus = $(':focusable');
        var index = $canfocus.index(this) + 1;
        if (index >= $canfocus.length) index = 0;
        $canfocus.eq(index).focus();
    }
});


$(document).ready(function () {

    //-------------------------------------------------------
    //start Kendo UI Grid
    //-------------------------------------------------------
    //Quote Main Kendo UI Grid start 
    $("#QuoteKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/Quote/QuoteRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        QuoteNo: { type: "string" },
                        QuoteDate: { type: "Date" },
                        Organization: { type: "string" },
                        CustomerName: { type: "string" },
                        ActionLink: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 500,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        editable: false,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: false, filterable: false, sortable: false, width: "10%" },
                { field: "QuoteNo", title: "Quote No#", hidden: true, width: "25%" },
                { field: "QuoteDate", title: "Quote Date", format: "{0:dd-MMM-yyyy}", width: "15%" },
                { field: "Organization", title: "Organization", width: "25%" },
                { field: "CustomerName", title: "Customer", width: "50%" },
                { field: "ActionLink", title: "Actions", width: "10%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });
    
    //Quote Kendo UI Grid End Here 
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addQuoteDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });

    //add Quote
    $('#lnkAddQuote').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addQuoteDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addQuoteForm");

            var isAddFormData = $form.data();

            if (isAddFormData == null) {
                //open dialog
                dialogDiv.dialog('open');
            }
            else {
                // Unbind existing validation
                $form.unbind();
                $form.data("validator", null);
                // Check document for changes
                $.validator.unobtrusive.parse(document);
                // Re add validation with changes
                $form.validate($form.data("unobtrusiveValidation").options);
                //open dialog
                dialogDiv.dialog('open');
            }

        });
        return false;
    });

    //edit Quote
    $("#editQuoteDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });


    $('#QuoteKendoGrid tbody tr td a.lnkEditQuote').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editQuoteDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editQuoteForm");

            var isEditFormData = $form.data();

            if (isEditFormData == null) {
                //open dialog
                dialogDiv.dialog('open');
            }
            else {
                // Unbind existing validation
                $form.unbind();
                $form.data("validator", null);
                // Check document for changes
                $.validator.unobtrusive.parse(document);
                // Re add validation with changes
                $form.validate($form.data("unobtrusiveValidation").options);
                //open dialog
                dialogDiv.dialog('open');
            }

        });
        return false;

    });


    //delete Quote
    $("#deleteQuoteDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#QuoteKendoGrid tbody tr td a.lnkDeleteQuote').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteQuoteDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteQuoteForm");

            var isDeleteFormData = $form.data();

            if (isDeleteFormData == null) {
                //open dialog
                dialogDiv.dialog('open');
            }
            else {
                // Unbind existing validation
                $form.unbind();
                $form.data("validator", null);
                // Check document for changes
                $.validator.unobtrusive.parse(document);
                // Re add validation with changes
                $form.validate($form.data("unobtrusiveValidation").options);
                //open dialog
                dialogDiv.dialog('open');
            }

        });
        return false;

    });
    
    //For details Quote
    $("#detailsQuoteDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#QuoteKendoGrid tbody tr td a.lnkDetailQuote').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsQuoteDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //End Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------
});





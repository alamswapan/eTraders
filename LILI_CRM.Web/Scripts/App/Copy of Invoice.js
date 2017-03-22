//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Invoice Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddInvoiceForm() {
    $('#InvoiceNo').val('');
    $('#InvoiceDate').val('');
    $('#CustomerName').val('');
    $('#CustomerAddress').val('');
    $('#Phone').val('');
    $('#Email').val('');
    $('#TIN').val('');
    $('#BIN').val('');
    $('#Product').val('');

    $('#Total').val(0);
    $('#OtherCharges').val(0);
    $('#NET').val(0);
    $('#InvoiceDetailsKendoGrid').data('kendoGrid').dataSource.data([]);
    //$('#IsActive').attr('checked', false);
}

function AddInvoiceSuccess() {
    if ($("#updateTargetId").html() == "True") {
        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddInvoiceForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Invoice Success Function
function EditInvoiceSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editInvoiceDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddInvoiceForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Invoice Success Function
function DeleteInvoiceSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteInvoiceDialog').dialog('close');

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
    //Get Invoice Grid
    var dGrid = $('#InvoiceKendoGrid').data('kendoGrid');
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
    //Invoice Main Kendo UI Grid start 
    $("#InvoiceKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/Invoice/InvoiceRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        InvoiceNumber: { type: "string" },
                        InvoiceDate: { type: "Date" },
                        Organization: { type: "string" },
                        InvoiceType: { type: "string" },
                        CustomerName: { type: "string" },
                        NET: { type: "number" },
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
        editable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: true, filterable: false, sortable: false },
                { field: "InvoiceNo", title: "Invoice No#", width: "25%" },
                { field: "InvoiceDate", title: "Invoice Date", format: "{0:dd-MMM-yyyy hh mm}", width: "50%" },
                { field: "Organization", title: "Organization", width: "25%" },
                { field: "InvoiceType", title: "InvoiceType", width: "25%" },
                { field: "CustomerName", title: "Customer", width: "25%" },
                { field: "NET", title: "NET", width: "25%" },
                { field: "ActionLink", title: "Actions", width: "25%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //Invoice Kendo UI Grid End Here 
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addInvoiceDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });

    //add Invoice
    $('#lnkAddInvoice').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addInvoiceDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addInvoiceForm");

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

    //edit Invoice
    $("#editInvoiceDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    $('#InvoiceKendoGrid tbody tr td a.lnkEditInvoice').live('click', function () {

        //change the title of the dialog
        alert("edit");
        var linkObj = $(this);
        var dialogDiv = $('#editInvoiceDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editInvoiceForm");

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


    //delete Invoice
    $("#deleteInvoiceDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#InvoiceKendoGrid tbody tr td a.lnkDeleteInvoice').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteInvoiceDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteInvoiceForm");

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

    //For details Invoice
    $("#detailsInvoiceDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#InvoiceKendoGrid tbody tr td a.lnkDetailInvoice').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsInvoiceDialog');
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





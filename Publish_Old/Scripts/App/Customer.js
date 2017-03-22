//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Customer Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddCustomerForm() {
    $('#CustomerId').val('');
    $('#CustomerName').val('');
    $('#Address1').val('');
    $('#Address2').val('');
    $('#Country').val('');
    $('#Phone').val('');
    $('#Fax').val('');
    $('#Mobile').val('');
    $('#Email').val('');
    $('#WebAddress').val('');
    $('#CustomerName').focus();
    //$('#IsActive').attr('checked', false);
}

function AddCustomerSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddCustomerForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Customer Success Function
function EditCustomerSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editCustomerDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Customer Success Function
function DeleteCustomerInfoSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteCustomerDialog').dialog('close');

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
    //Get Customer Grid
    var dGrid = $('#CustomerKendoGrid').data('kendoGrid');
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

    $("#CustomerKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/Customer/CustomerRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        CustomerId: { type: "string" },
                        CustomerName: { type: "string" },
                        Address1: { type: "string" },
                        Address2: { type: "string" },
                        Country: { type: "string" },
                        Phone: { type: "string" },
                        Fax: { type: "string" },
                        Mobile: { type: "string" },
                        Email: { type: "string" },
                        WebAddress: { type: "string" },
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
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: true, filterable: false, sortable: false },
                { field: "CustomerId", title: "CustomerId", width: "25%" },
                { field: "CustomerName", title: "Customer Name", width: "25%" },
                { field: "Address1", title: "Address", width: "25%" },
                { field: "Address2", title: "Address2", hidden: true, filterable: false, sortable: false },
                { field: "Country", title: "Country", width: "25%"},
                { field: "Phone", title: "Phone", width: "25%" },
                { field: "Fax", title: "Fax", width: "25%" },
                { field: "Mobile", title: "Mobile", width: "25%" },
                { field: "Email", title: "Email", width: "25%" },
                { field: "WebAddress", title: "Web Address", width: "25%" },
                { field: "ActionLink", title: "Actions", width: "20%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //end Kendo UI Grid
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addCustomerDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    //add Customer
    $('#lnkAddCustomer').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addCustomerDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addCustomerForm");

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

    //edit Customer
    $("#editCustomerDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    $('#CustomerKendoGrid tbody tr td a.lnkEditCustomer').live('click', function () {


        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editCustomerDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editCustomerForm");

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


    //delete Customer
    $("#deleteCustomerDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#CustomerKendoGrid tbody tr td a.lnkDeleteCustomer').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteCustomerDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteCustomerForm");

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


    //For details Customer
    $("#detailsCustomerDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#CustomerKendoGrid tbody tr td a.lnkDetailCustomer').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsCustomerDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});
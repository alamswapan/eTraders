//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Supplier Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddSupplierForm() {
    $('#SupplierName').val('');
    $('#LocalAgent').val('');
    $('#ContactPerson').val('');
    //$('#IsActive').attr('checked', false);
}

function AddSupplierSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddSupplierForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Supplier Success Function
function EditSupplierSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editSupplierDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Supplier Success Function
function DeleteSupplierSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteSupplierDialog').dialog('close');

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
    //Get Supplier Grid
    var dGrid = $('#SupplierKendoGrid').data('kendoGrid');
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

    $("#SupplierKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/Supplier/SupplierRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        SupplierId: { type: "string" },                        
                        SupplierName: { type: "string" },
                        Address: { type: "string" },
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
                { field: "SupplierId", title: "SupplierId", width: "25%" },
                { field: "SupplierName", title: "Name", width: "25%" },
                { field: "Address", title: "Address", width: "25%" },
                { field: "Country", title: "Country", width: "25%"},
                { field: "Phone", title: "Phone", width: "25%" },
                { field: "Fax", title: "Fax", width: "25%" },
                { field: "Mobile", title: "Mobile", width: "25%" },
                { field: "Email", title: "Email", width: "25%" },
                { field: "WebAddress", title: "Web Address", width: "25%" },
                { field: "ActionLink", title: "Actions", width: "14%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //end Kendo UI Grid
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addSupplierDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    //add Supplier
    $('#lnkAddSupplier').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addSupplierDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addSupplierForm");

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

    //edit Supplier
    $("#editSupplierDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    $('#SupplierKendoGrid tbody tr td a.lnkEditSupplier').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editSupplierDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editSupplierForm");

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


    //delete Supplier
    $("#deleteSupplierDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#SupplierKendoGrid tbody tr td a.lnkDeleteSupplier').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteSupplierDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteSupplierForm");

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


    //For details Supplier
    $("#detailsSupplierDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#SupplierKendoGrid tbody tr td a.lnkDetailSupplier').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsSupplierDialog');
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
//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Product Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddProductForm() {
    $('#ProductCode').val('');
    $('#ProductName').val('');
    $('#GenericName').val('');
    $('#HSCode').val('');
    $('#BaseUnit').val('');
    $('#SamplingUnit').val('');
    $('#UnitPrice').val(0);
    $('#DutyStructure').val(0);
    $('#Origin').val('');
    $('#MOQ').val(0);
    $('#LeadTime').val('');

    $('#ProductCode').focus();
    //$('#IsActive').attr('checked', false);
}

function AddProductSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddProductForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Product Success Function
function EditProductSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editProductDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddProductForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Product Success Function
function DeleteProductSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteProductDialog').dialog('close');

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
    //Get Product Grid
    var dGrid = $('#ProductKendoGrid').data('kendoGrid');
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

    $("#ProductKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/Product/ProductRead"
                /*
                read: {
                url: "/SD/Product/ProductRead",
                data: ProductRead
                }
                },*/
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        ProductCode: { type: "string", editable: false },
                        ProductName: { type: "string", editable: false },
                        GenericName: { type: "string", editable: false },
                        HScode: { type: "string", editable: false },
                        BaseUnit: { type: "string", editable: false },
                        SamplingUnit: { type: "string", editable: false },
                        UnitPrice: { type: "number" },
                        ActionLink: { type: "string", editable: false }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 500,
        editable: true,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: true, filterable: false, sortable: false },
                { field: "ProductCode", title: "Product Code", editable: false, width: "25%" },
                { field: "ProductName", title: "Product Name", editable: false, width: "50%" },
                { field: "GenericName", title: "Generic Name", editable: false, width: "50%" },
                { field: "HScode", title: "HS Code", editable: false, width: "50%" },
                { field: "BaseUnit", title: "Base Unit", editable: false, width: "25%" },
                { field: "SamplingUnit", title: "Sample Unit", editable: false, width: "25%" },
                { field: "UnitPrice", title: "Unit Price", width: "25%" },
                { field: "ActionLink", title: "Actions", width: "25%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //end Kendo UI Grid
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addProductDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    //add Product
    $('#lnkAddProduct').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addProductDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addProductForm");

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

    //edit Product
    $("#editProductDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    $('#ProductKendoGrid tbody tr td a.lnkEditProduct').live('click', function () {

        alert('Product Found ');
        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editProductDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editProductForm");

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


    //delete Product
    $("#deleteProductDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#ProductKendoGrid tbody tr td a.lnkDeleteProduct').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteProductDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteProductForm");

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


    //For details Product
    $("#detailsProductDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#ProductKendoGrid tbody tr td a.lnkDetailProduct').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsProductDialog');
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
﻿//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add SampleRequest Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddSampleRequestForm() {
    $('#RequestNo').val('');
    $('#Organization').val('');
    $('#SupplierId').val('');
    $('#SupplierAddress').val('');
    $('#ArticleCode').val('');
    $('#SampleRequestDetailsKendoGrid').data('kendoGrid').dataSource.data([]);
//    $('#VAT').val(0);
//    $('#Discount').val(0);
//    $('#Roundoff').val(0);
//    $('#NET').val(0);
//    $('#CashTaken').val(0);
//    $('#ChangeAmount').val(0);
    //$('#IsActive').attr('checked', false);
}

function AddSampleRequestSuccess() {
    if ($("#updateTargetId").html() == "True") {
        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddSampleRequestForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit SampleRequest Success Function
function EditSampleRequestSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editSampleRequestDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddSampleRequestForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete SampleRequest Success Function
function DeleteSampleRequestSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteSampleRequestDialog').dialog('close');

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
    //Get SampleRequest Grid
    var dGrid = $('#SampleRequestKendoGrid').data('kendoGrid');
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
    //SampleRequest Main Kendo UI Grid start 
    $("#SampleRequestKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/SampleRequest/SampleRequestRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        RequestNo: { type: "string" },
                        RequestDate: { type: "Date" },
                        Organization: { type: "string" },
                        SupplierName: { type: "string" },
                        TransporterName: { type: "string" },
                        DocTrackingNo: { type: "string" },
                        DeliveryState: { type: "string" },
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
                { field: "RequestNo", title: "SampleRequest No#", hidden: true, width: "25%" },
                { field: "RequestDate", title: "Request Date", format: "{0:dd-MMM-yyyy}", width: "15%" },
                { field: "Organization", title: "Organization", width: "25%" },
                { field: "SupplierName", title: "Supplier", width: "50%" },
                { field: "TransporterName", title: "Courier", width: "25%" },
                { field: "DocTrackingNo", title: "Tracck No", width: "10%" },
                { field: "DeliveryState", title: "Status", width: "10%" },
                { field: "ActionLink", title: "Actions", width: "10%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });
    
    //SampleRequest Kendo UI Grid End Here 
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addSampleRequestDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });

    //add SampleRequest
    $('#lnkAddSampleRequest').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addSampleRequestDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addSampleRequestForm");

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

    //edit SampleRequest
    $("#editSampleRequestDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });


    $('#SampleRequestKendoGrid tbody tr td a.lnkEditSampleRequest').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editSampleRequestDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editSampleRequestForm");

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


    //delete SampleRequest
    $("#deleteSampleRequestDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#SampleRequestKendoGrid tbody tr td a.lnkDeleteSampleRequest').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteSampleRequestDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteSampleRequestForm");

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
    
    //For details SampleRequest
    $("#detailsSampleRequestDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#SampleRequestKendoGrid tbody tr td a.lnkDetailSampleRequest').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsSampleRequestDialog');
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





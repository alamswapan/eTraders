//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add VoucherInfo Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddVoucherInfoForm() {
    $('#VoucherNo').val('');    
}

function AddVoucherInfoSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        $('#addVoucherInfoDialog').dialog('close');

//        //Reset Add Form
//        ResetAddVoucherInfoForm();

//        //Refresh Kendo Grid
//        dtKendoGridAddRefresh();
        dtKendoGridRefresh();
    }
    else {

        //show message in popup
        $("#updateTargetId").show();
    }

}

// Edit Voucher Success Function
function EditVoucherInfoSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editVoucherInfoDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Voucher Info Success Function
function DeleteVoucherInfoSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteVoucherInfoDialog').dialog('close');

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

    var dGrid = $('#voucherInfoKendoGrid').data('kendoGrid');
    dGrid.dataSource.read();

//    $("#voucherInfoKendoGrid").data('kendoGrid').dataSource.data([]);
}


function dtKendoGridAddRefresh() {

    $("#VoucherDetailsForAddKendoGrid").data('kendoGrid').dataSource.data([]);
}

//end Refresh Kendo Grid Funtion
//-----------------------------------------------------

$(document).ready(function () {

    //-------------------------------------------------------
    //start Kendo UI Grid

    $("#voucherInfoKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/PettyCashVoucher/PettyCashVoucherRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        VoucherNo: { type: "number" },
                        CompanyCode: { type: "string" },
                        PayTo: { type: "string" },
                        StaffID: { type: "string" },
                        VoucherDate: { type: "date", format: "dd-MMM-yyyy" },
                        UserId: { type: "string" },

                        ActionLink: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 450,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: true, filterable: false, sortable: false },
                { field: "VoucherNo", title: "Voucher No.", width: "10%", attributes: { class: "k-grid-column-number"} },
                { field: "CompanyCode", title: "Company Code", width: "10%", attributes: { class: "k-grid-column-number"} },
                { field: "PayTo", title: "Pay To", width: "35%" },
                { field: "StaffID", title: "Staff ID", width: "10%", attributes: {style: "text-align:center"} },
                { field: "VoucherDate", title: "Voucher Date", width: "10%", template: '#= kendo.toString(VoucherDate,"dd/MM/yyyy") #', attributes: { style: "text-align:center"} },
                { field: "UserId", title: "User Id", width: "10%", attributes: { class: "k-grid-column-number"} },

                { field: "ActionLink", title: "Actions", width: "7%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //end Kendo UI Grid
    //-----------------------------------------------------


    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addVoucherInfoDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });

    //add Voucher
    $('#lnkAddVoucherInfo').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addVoucherInfoDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addVoucherInfoForm");

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

    //edit VoucherInfo
    $("#editVoucherInfoDialog").dialog({
        autoOpen: false,
        width: '90%',
        resizable: false,
        modal: true

    });

    $('#voucherInfoKendoGrid tbody tr td a.lnkEditPettyCashVoucher').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editVoucherInfoDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editVoucherInfoForm");

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

    //delete Voucher
    $("#deleteVoucherInfoDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });

    $('#voucherInfoKendoGrid tbody tr td a.lnkDeletePettyCashVoucher').live('click', function () {


        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteVoucherInfoDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteVoucherInfoForm");

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

    //For details Voucher
    $("#detailsVoucherInfoDailog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#voucherInfoKendoGrid tbody tr td a.lnkDetailVoucherInfo').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsVoucherInfoDailog');
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
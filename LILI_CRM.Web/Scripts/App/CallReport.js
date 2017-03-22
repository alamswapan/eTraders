//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Project Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

//function Grid_OnRowSelect(e) {
//    var data = this.dataItem(this.select());
//    YourRowID = data.id; //IMP
//    alert(data.Unit);
//}

window.globalVarStageId = "";
window.globalVarStageName = "";

Grid_OnRowSelect = function (e) {
    var data = this.dataItem(this.select());
    YourRowID = data.id; //IMP
    alert(data.Unit);
}

function selectedIndexChanged(e) {
    
        //alert(this.dataItem(this.select()).StageId);
        var selectedStageId = this.dataItem(this.select()).StageId;
        var selectedStageName = this.dataItem(this.select()).StageName;
        //alert(selectedValue);
        window.globalVarStageId = selectedStageId;
        window.globalVarStageName = selectedStageName;
        //alert(window.globalVar);
    }

    function onChange(arg) {
        
    var data = this.dataItem(this.select());
    this.dataItem(this.select()).StageName = window.globalVarStageName;
    data.set("StageIdTemp", window.globalVarStageId);
    data.set("StageId", window.globalVarStageId);
    data.set("StageName", window.globalVarStageName);

}

function ResetAddProjectForm() {
    $('#ProjectName').val('');
    $('#CurrentStageId').val('');
    $('#CustomerId').val('');
    $('#SupplierId').val('');
    $('#Description').val('');
    $('#SellingOpportunity').val('');
    $('#PotVolPerYear').val('');
    $('#PotAmountPerYear').val('');
    $('#RemarkNextAction').val('');
    $('#CommunicationChannelId').val('');
    $('#SalesPerson').val('');
    $('#IsActive').attr('checked', false);
}

function ResetAddSalesCallInquiryForm() {   
    $('#CustomerId').val('');
    $('#SupplierId').val('');
}

function AddProjectSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddProjectForm();

        //Refresh Kendo Grid
        dtProjectKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

function AddSalesCallInquirySuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddSalesCallInquiryForm();

        //Refresh Kendo Grid
        dtSalesCallInquiryKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

function EditSalesCallInquirySuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editSalesCallInquiryDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Refresh Kendo Grid
        dtSalesCallInquiryKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}


// Edit Project Success Function
function EditProjectSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editProjectDialogNew').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Refresh Kendo Grid
        dtProjectKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Project Success Function
function DeleteProjectSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteProjectDialog').dialog('close');

        SuccessMessage("Information has been deleted successfully");

        //Refresh Kendo Grid
        dtProjectKendoGridRefresh();
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
function dtProjectKendoGridRefresh() {
    //Get Project Grid
    var dGrid = $('#ProjectKendoGrid').data('kendoGrid');
    dGrid.dataSource.read();
}

function dtSalesCallInquiryKendoGridRefresh() {
    //Get Project Grid
    var dGrid = $('#SalesCallInquiryKendoGrid').data('kendoGrid');
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


    //Project Grid
    $("#ProjectKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/CallReport/ProjectRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        ProjectName: { type: "string" },
                        CurrentStage: { type: "string" },
                        Description: { type: "string" },
                        SellingOpportunity: { type: "string" },
                        PotVolPerYear: { type: "string" },
                        PotAmountPerYear: { type: "string" },
                        RemarkNextAction: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 200,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: true, filterable: false, sortable: false },
                { field: "ProjectName", title: "Project Name", width: "25%" },
                { field: "CurrentStage", title: "Current Stage", width: "25%" },
                { field: "Description", title: "Description", width: "25%" },
                { field: "SellingOpportunity", title: "Selling Opportunity(Items)", width: "25%" },
                { field: "PotVolPerYear", title: "Pot Vol/Yr", width: "25%" },
                { field: "PotAmountPerYear", title: "Pot $/yr", width: "25%" },
                { field: "RemarkNextAction", title: "Remarks > Next Action", width: "25%" },
                { field: "ActionLink", title: "Actions", width: "14%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });




    // Sales Call Inquiry Grid
    $("#SalesCallInquiryKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/CallReport/SalesCallInquiryRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        CustomerName: { type: "string" },
                        SupplierName: { type: "string" },
                        ActionLink: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 200,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", width: "5%", hidden: false, filterable: false, sortable: false },
                    { field: "CustomerName", title: "Customer", width: "45%" },
                    { field: "SupplierName", title: "Supplier", width: "45%" },
                    { field: "ActionLink", title: "Actions", width: "5%", filterable: false, sortable: false, template: "#= ActionLink #" }
                ]
    });




    //Order Received Grid
    $("#OrderReceivedKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/CallReport/SalesCallOrderReceivedRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        CustomerName: { type: "string" },
                        SupplierName: { type: "string" },
                        ActionLink: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 200,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", width: "5%", hidden: false, filterable: false, sortable: false },
                    { field: "CustomerName", title: "Customer", width: "45%" },
                    { field: "SupplierName", title: "Supplier", width: "45%" },
                    { field: "ActionLink", title: "Actions", width: "5%", filterable: false, sortable: false, template: "#= ActionLink #" }
                ]
    });






    //Order Lost Grid
    $("#OrderLostKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/CallReport/SalesCallOrderLostRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        CustomerName: { type: "string" },
                        SupplierName: { type: "string" },
                        ActionLink: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 200,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", width: "5%", hidden: false, filterable: false, sortable: false },
                    { field: "CustomerName", title: "Customer", width: "45%" },
                    { field: "SupplierName", title: "Supplier", width: "45%" },
                    { field: "ActionLink", title: "Actions", width: "5%", filterable: false, sortable: false, template: "#= ActionLink #" }
                ]
    });







    //end Kendo UI Grid
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addProjectDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    $("#editProjectDialogNew").dialog({
        autoOpen: false,
        width: 850,
        resizable: false,
        modal: true
    });


    $("#deleteProjectDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });

    $("#addSalesCallInquiryDialog").dialog({
        autoOpen: false,
        width: 1050,
        resizable: false,
        modal: true
    });

    $("#editSalesCallInquiryDialog").dialog({
        autoOpen: false,
        width: 1050,
        resizable: false,
        modal: true
    });

    $("#deleteSalesCallInquiryDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });

    //add Project
    $('#lnkAddProject').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addProjectDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addProjectForm");

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

    //add SalesCallInquiry
    $('#lnkAddSalesCallInquiry').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addSalesCallInquiryDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addSalesCallInquiryForm");

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





    $('#ProjectKendoGrid tbody tr td a.lnkEditCallReport').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editProjectDialogNew');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editProjectForm");

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


    $('#SalesCallInquiryKendoGrid tbody tr td a.lnkEditCallReport').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editSalesCallInquiryDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editSalesCallInquiryForm");

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






    $('#OrderReceivedKendoGrid tbody tr td a.lnkEditCallReport').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editSalesCallInquiryDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editSalesCallInquiryForm");

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



    $('#OrderLostKendoGrid tbody tr td a.lnkEditCallReport').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editSalesCallInquiryDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editSalesCallInquiryForm");

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








    $('#ProjectKendoGrid tbody tr td a.lnkDeleteCallReport').live('click', function () {
        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteProjectDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteProjectForm");

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


    //For details Project
    $("#detailsProjectDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#ProjectKendoGrid tbody tr td a.lnkDetailProject').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsProjectDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------


    //edit SampleRequest
    $("#editSampleRequestDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });


    $('#SampleRequestIndexKendoGrid tbody tr td a.lnkEditSampleRequest').live('click', function () {

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
                //$('#editProjectDialogNew').dialog('close')
                $('#divProjectEdit').hide();
                $('#SampleRequestIndexGridZone').hide();
                $('#indexPageSampleSubmission').hide();
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





    //edit SampleSubmission
    $("#editSampleSubmissionDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });


    $('#SampleSubmissionIndexKendoGrid tbody tr td a.lnkEditSampleSubmission').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);

        var dialogDiv = $('#editSampleSubmissionDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editSampleSubmissionForm");

            var isEditFormData = $form.data();

            if (isEditFormData == null) {

                //open dialog
                dialogDiv.dialog('open');
            }
            else {
                //$('#editProjectDialogNew').dialog('close')
                $('#divProjectEdit').hide();
                $('#SampleSubmissionIndexGridZone').hide();
                $('#indexPageSampleRequest').hide();
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









});
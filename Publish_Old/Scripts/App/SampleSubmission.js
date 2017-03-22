//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add SampleSubmission Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddSampleSubmissionForm() {
    $('#RequestNo').val('');
    $('#Organization').val('');
    $('#CustomerId').val('');
    $('#CusContactId').val('');
    $('#ArticleCode').val('');
    $('#SampleSubmissionDetailsKendoGrid').data('kendoGrid').dataSource.data([]);

}

function AddSampleSubmissionSuccess() {
    if ($("#updateTargetId").html() == "True") {
        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddSampleSubmissionForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit SampleSubmission Success Function
function EditSampleSubmissionSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editSampleSubmissionDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddSampleSubmissionForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete SampleSubmission Success Function
function DeleteSampleSubmissionSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteSampleSubmissionDialog').dialog('close');

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
    //Get SampleSubmission Grid
    var dGrid = $('#SampleSubmissionKendoGrid').data('kendoGrid');
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
    //SampleSubmission Main Kendo UI Grid start 
    $("#SampleSubmissionKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/SampleSubmission/SampleSubmissionRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        SubmissionNo: { type: "string" },
                        SubmissionDate: { type: "Date" },
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
                { field: "SubmissionNo", title: "SampleSubmission No#", hidden: true, width: "25%" },
                { field: "SubmissionDate", title: "Request Date", format: "{0:dd-MMM-yyyy}", width: "15%" },
                { field: "Organization", title: "Organization", width: "25%" },
                { field: "CustomerName", title: "Customer", width: "50%" },
                { field: "ActionLink", title: "Actions", width: "10%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });
    
    //SampleSubmission Kendo UI Grid End Here 
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addSampleSubmissionDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });

    //add SampleSubmission
    $('#lnkAddSampleSubmission').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addSampleSubmissionDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addSampleSubmissionForm");

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

    //edit SampleSubmission
    $("#editSampleSubmissionDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });


    $('#SampleSubmissionKendoGrid tbody tr td a.lnkEditSampleSubmission').live('click', function () {

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


    //delete SampleSubmission
    $("#deleteSampleSubmissionDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#SampleSubmissionKendoGrid tbody tr td a.lnkDeleteSampleSubmission').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteSampleSubmissionDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteSampleSubmissionForm");

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
    
    //For details SampleSubmission
    $("#detailsSampleSubmissionDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#SampleSubmissionKendoGrid tbody tr td a.lnkDetailSampleSubmission').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsSampleSubmissionDialog');
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





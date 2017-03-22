//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add DebitNote Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddDebitNoteForm() {
    $('#DebitNoteNo').val('');
    $('#DebitNoteDate').val('');
    $('#DebitNoteDetailsKendoGrid').data('kendoGrid').dataSource.data([]);
    //$('#IsActive').attr('checked', false);
}

function AddDebitNoteSuccess() {
    if ($("#updateTargetId").html() == "True") {
        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddDebitNoteForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit DebitNote Success Function
function EditDebitNoteSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editDebitNoteDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddDebitNoteForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete DebitNote Success Function
function DeleteDebitNoteSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteDebitNoteDialog').dialog('close');

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
    //Get DebitNote Grid
    var dGrid = $('#DebitNoteKendoGrid').data('kendoGrid');
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
    //alert("grid");
    //-------------------------------------------------------
    //start Kendo UI Grid
    //-------------------------------------------------------
    //DebitNote Main Kendo UI Grid start 
    $("#DebitNoteKendoGrid").kendoGrid({
        dataSource: {
            transport: {
                read: "/PCV/DebitNote/DebitNoteRead"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        DebitNoteNumber: { type: "string" },
                        DebitNoteDate: { type: "Date" },
                        Organization: { type: "string" },
                        DebitNoteType: { type: "string" },
                        CustomerName: { type: "string" },
                        NET: { type: "number" },
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
                { field: "DebitNoteNo", title: "DebitNote No#", width: "25%" },
                { field: "DebitNoteDate", title: "DebitNote Date", format: "{0:dd-MMM-yyyy hh mm}", width: "50%" },
                { field: "Organization", title: "Organization", width: "25%" },
                { field: "DebitNoteType", title: "DebitNoteType", width: "25%" },
                { field: "CustomerName", title: "Customer", width: "25%" },
                { field: "NET", title: "NET", width: "25%" },
                { field: "ActionLink", title: "Actions", width: "25%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //DebitNote Kendo UI Grid End Here 
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addDebitNoteDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });

    //add DebitNote
    $('#lnkAddDebitNote').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addDebitNoteDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addDebitNoteForm");

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

    //edit DebitNote
    $("#editDebitNoteDialog").dialog({
        autoOpen: false,
        width: 1250,
        resizable: false,
        modal: true
    });


    $('#DebitNoteKendoGrid tbody tr td a.lnkEditDebitNote').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editDebitNoteDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editDebitNoteForm");

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



    //For details DebitNote
    $("#detailsDebitNoteDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#DebitNoteKendoGrid tbody tr td a.lnkDetailDebitNote').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsDebitNoteDialog');
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





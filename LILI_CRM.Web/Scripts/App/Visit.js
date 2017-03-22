//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Visit Success Function

function SuccessMessage(msg) {
    $("#message").html('');
    $("#message").html(msg);
    $("#message").show();
    $('#message').delay(400).slideDown(400).delay(3000).slideUp(400);
}

function ResetAddVisitForm() {
    $('#PointOfDiscussion').val('');
    $('#Details').val('');
    $('#Remarks').val('');
}

function AddVisitSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //show message in popup
        $("#updateTargetId").html('');
        $("#updateTargetId").html('Information has been saved successfully');
        $("#updateTargetId").show();

        //Reset Add Form
        ResetAddVisitForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Visit Success Function
function EditVisitSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editVisitDialog').dialog('close');

        SuccessMessage("Information has been updated successfully");

        //Reset Add Form
        //ResetAddVisitForm();

        //Refresh Kendo Grid
        dtKendoGridRefresh();
    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Visit Success Function
function DeleteVisitSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteVisitDialog').dialog('close');

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
    //Get Visit Grid
    var dGrid = $('#VisitKendoGrid').data('kendoGrid');
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

    $("#VisitKendoGrid").kendoGrid({
        dataSource: {
            transport: {
            read: "/PCV/Visit/VisitRead"
                /*
                read: {
                    url: "/SD/Visit/VisitRead",
                    data: VisitRead
                    }
                },*/
            },
            schema: {
                model: {
                    fields: {
                                Id: { type: "number" },
                                PreparationDate: { type: "date", format: "dd-MMM-yyyy" },
                                PointOfDiscussion: { type: "string", editable: false },
                                ActionLink: { type: "string" , editable: false }
                            }
                }
            },
            pageSize: 20,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },
        height: 400,
        editable: true,
        filterable: true,
        groupable: true,
        sortable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [20, 40, 60, 80, 100]
        },
        columns: [{ field: "Id", title: "Id", hidden: false, filterable: false, sortable: false, width: "10%" },
                { field: "PreparationDate", title: "Date Of Preparation", width: "15%", template: '#= kendo.toString(PreparationDate,"dd/MM/yyyy") #', attributes: { style: "text-align:center"} },
                { field: "PointOfDiscussion", title: "Point Of Discussion", editable: false, width: "70%" },
                { field: "ActionLink", title: "Actions", width: "7%", filterable: false, sortable: false, template: "#= ActionLink #" }
            ]
    });

    //end Kendo UI Grid
    //-----------------------------------------------------

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addVisitDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    //add Visit
    $('#lnkAddVisit').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addVisitDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#addVisitForm");

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

    //edit Visit
    $("#editVisitDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });


    $('#VisitKendoGrid tbody tr td a.lnkEditVisit').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editVisitDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#editVisitForm");

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


    //delete Visit
    $("#deleteVisitDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true
    });


    $('#VisitKendoGrid tbody tr td a.lnkDeleteVisit').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteVisitDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);

            //get form
            var $form = $("#deleteVisitForm");

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


    //For details Visit
    $("#detailsVisitDialog").dialog({
        autoOpen: false,
        width: 650,
        resizable: false,
        modal: true
    });

    $('#VisitKendoGrid tbody tr td a.lnkDetailVisit').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsVisitDialog');
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
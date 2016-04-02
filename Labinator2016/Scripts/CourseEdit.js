var ajm = {
    "type": "POST",
    "url": '../MachineAjax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        data = $.extend({}, data, { "SessionId": $('#Session').val() });
        data = JSON.stringify(data);
        return data;
    }
};

var dcm = [
        { "data": "VMName" },
        { "data": "IsActive" },
        { "data": "CourseMachineTempId" }
];

function crm(Row, Data, Index) {
    imgTag = '<input type="checkbox" onclick="UpdateActive(' + Data["CourseMachineTempId"] + ',this.checked)"';
    if (Data["IsActive"] == true) {
        imgTag = imgTag + ' checked'
    }
    imgTag = imgTag + '/>';
    $('td', Row).eq(1).html(imgTag);
    $('td', Row).eq(2).html("");
};

function UpdateMachines() {
    var parameters = { "Course": $('#CourseId').val(), "Template": $('#Template').val(), "Session": $('#Session').val() };
    $.ajax({
        type: "POST",
        url: "../Refresh",
        data: JSON.stringify(parameters),
        contentType: "application/json",
        datatype: "html",
        error: function () { alert("Error updating Status!!"); },
        success: function (result) {
            oTable.ajax.reload(null, false);// user paging is not reset on reload
        }
    });
}

function UpdateActive(MachineId, state) {
    var parameters = { "configuration": MachineId, "active": state };
    $.ajax({
        type: "POST",
        url: "../Active",
        data: JSON.stringify(parameters),
        contentType: "application/json",
        datatype: "html",
        error: function () { alert("Error updating Status!!"); }
    });
}
function resize() {
    $('.dataTables_scrollBody').css('height', '1px');
    var boxHeight = $('.wallpaper').height();
    var dt = $('#Table');
    var datatableHeight = dt.height();
    var brHeight = $('#bannerRow').height();
    var nrHeight = $('#NavRow').height();
    var dcHeight = $('#NameRow').height();
    var csHeight = $('#DaysRow').height();
    var usHeight = $('#HoursRow').height();
    var dtHeight = $('#TemplateRow').height();
    var btHeight = $('#ButtonRow').height();
    var tableHeight = boxHeight - (datatableHeight + brHeight + nrHeight + dcHeight + csHeight + usHeight + dtHeight + btHeight + 60);
    $('.dataTables_scrollBody').css('height', tableHeight + 'px');
}
$(document).load(refreshList(ajm, dcm, crm));

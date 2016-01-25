var aj = {
    "type": "POST",
    "url": 'Courses/Ajax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        //                data = $.extend({}, data, { "Classroom": $('#SelectedRoom').val() });
        data = JSON.stringify(data);
        return data;
    }
};

var dc = [
       { "data": "Name" },
       { "data": "StartTime" },
       { "data": "Days" },
       { "data": "Hours" },
       { "data": "CourseId" }
];

function columnRows(Row, Data, Index) {
    var link = '<a class="btn btn-primary"';
    link = link + ' href="Courses/Edit/' + Data["CourseId"] + '">';
    link = link + 'Edit';
    link = link + '</a>';
    link = link + '&nbsp;<a class="btn btn-danger"';
    link = link + ' href="Courses/Delete/' + Data["CourseId"] + '">';
    link = link + 'Delete';
    link = link + '</a>';
    $('td:eq(4)', Row).html(link);
    var re = /-?\d+/;
    var m = re.exec(Data["StartTime"]);
    var d = new Date(parseInt(m[0]));
    var t = ("00" + d.getHours()).slice(-2) + ":" + ("00" + d.getMinutes()).slice(-2);
    $('td:eq(1)', Row).html(t);

};

var ajm = {
    "type": "POST",
    "url": '../MachineAjax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        data = $.extend({}, data, { "Session": $('#Session').val() });
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

var aj = {
    "type": "POST",
    "url": 'Classrooms/Ajax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        //                data = $.extend({}, data, { "Classroom": $('#SelectedRoom').val() });
        data = JSON.stringify(data);
        return data;
    }
};

function columnRows(Row, Data, Index) {
    var link = '<a class="btn btn-primary"';
    link = link + ' href="Home/Grid/' + Data["ClassroomId"] + '">';
    link = link + 'Open';
    link = link + '</a>';
    link = link + '&nbsp;<a class="btn btn-default"';
    link = link + ' href="Classrooms/Edit/' + Data["ClassroomId"] + '">';
    link = link + 'Edit';
    link = link + '</a>';
    link = link + '&nbsp;<a class="btn btn-danger"';
    link = link + ' href="Classrooms/Delete/' + Data["ClassroomId"] + '">';
    link = link + 'Delete';
    link = link + '</a>';
    $('td:eq(3)', Row).html(link);

};
var dc = [
        { "data": "jsDate" },
        { "data": "course.Name" },
        { "data": "dataCenter.Name" },
        { "data": "ClassroomId" }
];

var aj = {
    "type": "POST",
    "url": 'Classrooms/Ajax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        data = $.extend({}, data, { "ShowAll": $('#ShowAll').val() });
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
$(document).load(refreshList(aj, dc, columnRows));
$("div.toolbar").html('<center><a href="Classrooms/Edit/0" class = "btn btn-primary">Add</a></center>');
$("[name='ShowAll']").bootstrapSwitch({
    "onText": "All Rooms",
    "offText": "My Rooms",
    "labelText": "Show"
});

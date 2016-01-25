var aj = {
    "type": "POST",
    "url": 'Users/Ajax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        //                data = $.extend({}, data, { "Classroom": $('#SelectedRoom').val() });
        data = JSON.stringify(data);
        return data;
    }
};
var dc = [
        { "data": "EmailAddress" },
        { "data": "IsInstructor" },
        { "data": "IsAdministrator" },
        { "data": "UserId" },
];
function columnRows(Row, Data, Index) {
    var link = '<a class="btn btn-primary"';
    link = link + ' href="Users/Edit/' + Data["UserId"] + '">';
    link = link + 'Edit';
    link = link + '</a>';
    link = link + '&nbsp;<a class="btn btn-danger"';
    link = link + ' href="Users/Delete/' + Data["UserId"] + '">';
    link = link + 'Delete';
    link = link + '</a>';
    $('td:eq(3)', Row).html(link);
};

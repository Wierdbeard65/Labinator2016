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
function resize() {
    $('.dataTables_scrollBody').css('height', '1px');
    var boxHeight = $('.wallpaper').height();
    var dt = $('#Table');
    var datatableHeight = dt.height();
    var brHeight = $('#bannerRow').height();
    var nrHeight = $('#NavRow').height();
    var tableHeight = boxHeight - (datatableHeight + brHeight + nrHeight + 40);
    $('.dataTables_scrollBody').css('height', tableHeight + 'px');
}
$(document).load(refreshList(aj, dc, columnRows));
$("div.toolbar").html('<center><a href="Users/Edit/0" class = "btn btn-primary">Add</a></center>');

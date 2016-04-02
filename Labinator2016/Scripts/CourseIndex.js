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

function resize() {
    $('.dataTables_scrollBody').css('height', '1px');
    var boxHeight = $('.wallpaper').height();
    var dt = $('#Table');
    var datatableHeight = dt.height();
    var brHeight = $('#bannerRow').height();
    var nrHeight = $('#NavRow').height();
    var tableHeight = boxHeight - (datatableHeight + brHeight + nrHeight + 60);
    $('.dataTables_scrollBody').css('height', tableHeight + 'px');
}
$(document).load(refreshList(aj, dc, columnRows));
$("div.toolbar").html('<center><a href="Courses/Edit/0" class = "btn btn-primary">Add</a></center>');

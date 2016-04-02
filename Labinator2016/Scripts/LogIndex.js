var aj = {
    "type": "POST",
    "url": 'Reports/Ajax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        //                data = $.extend({}, data, { "Classroom": $('#SelectedRoom').val() });
        data = JSON.stringify(data);
        return data;
    }
};

function columnRows(Row, Data, Index) {
};
var dc = [
        { "data": "jsTime" },
        { "data": "User"},
        { "data": "Msg" },
        { "data": "Detail" }
];
function resize() {
    $('.dataTables_scrollBody').css('height', '1px');
    var boxHeight = $('.wallpaper').height();
    var dt = $('#Table');
    var datatableHeight = dt.height();
    var brHeight = $('#bannerRow').height();
    var nrHeight = $('#NavRow').height();
    var tableHeight = boxHeight - (datatableHeight + brHeight + nrHeight  + 60);
    $('.dataTables_scrollBody').css('height', tableHeight + 'px');
}
$(document).load(refreshList(aj, dc, columnRows));
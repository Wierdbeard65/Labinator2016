var aj = {
    "type": "POST",
    "url": '../../Classrooms/AjaxSeat',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        data = $.extend({}, data, { "SessionId": $('#SessionId').val() });
        data = JSON.stringify(data);
        return data;
    }
};

var dc = [
       { "data": "user" },
       { "data": "user" }
];

function columnRows(Row, Data, Index) {
    var link = '<a class="btn btn-danger"';
    link = link + ' href="#" onclick="removeUser(' + Data["SeatTempId"] + ')"">';
    link = link + 'Delete';
    link = link + '</a>';
    $('td:eq(1)', Row).html(link);
    var user = Data["user"]["EmailAddress"];
    $('td:eq(0)', Row).html(user);
};

function addUser() {
    var parameters = { "Classroom": $('#ClassroomId').val(), "NewSeats": $('#newseats').val(), "Session": $('#SessionId').val() };
    $.ajax({
        type: "POST",
        url: "../AddSeats",
        data: JSON.stringify(parameters),
        contentType: "application/json",
        datatype: "html",
        error: function () { alert("Error adding Seats!!"); },
        success: function (result) {
            oTable.ajax.reload(null, false);// user paging is not reset on reload
        }
    });
    $("#newseats").val("");
}

function removeUser(id) {
    var parameters = { "SeatTempId": id };
    $.ajax({
        type: "POST",
        url: "../RemoveSeat",
        data: JSON.stringify(parameters),
        contentType: "application/json",
        datatype: "html",
        error: function () { alert("Error removing Seat!!"); },
        success: function (result) {
            oTable.ajax.reload(null, false);// user paging is not reset on reload
        }
    });

}

function cancelAdd() {
    $("#newseats").val("");
}

function resize() {
    $('.dataTables_scrollBody').css('height', '1px');
    var boxHeight = $('.wallpaper').height();
    var dt = $('#Table');
    var datatableHeight = dt.height();
    var brHeight = $('#bannerRow').height();
    var nrHeight = $('#NavRow').height();
    var dcHeight = $('#DCRow').height();
    var csHeight = $('#CrsRow').height();
    var usHeight = $('#UserRow').height();
    var dtHeight = $('#DateRow').height();
    var btHeight = $('#ButtonRow').height();
    var tableHeight = boxHeight - (datatableHeight + brHeight + nrHeight + dcHeight + csHeight + usHeight + dtHeight +btHeight + 60);
    $('.dataTables_scrollBody').css('height', tableHeight + 'px');
}
$(document).load(refreshList(aj, dc, columnRows));
$("div.toolbar").html('<center><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Add</button></center>');
$('#datetimepicker').datetimepicker({daysOfWeekDisabled: [0, 6]});

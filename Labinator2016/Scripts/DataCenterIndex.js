var aj = {
    "type": "POST",
    "url": 'DataCenters/Ajax',
    "contentType": 'application/json; charset=utf-8',
    'data': function (data) {
        data = JSON.stringify(data);
        return data;
    }
};

var dc = [
       { "data": "Name" },
       { "data": "Timezone" },
       { "data": "Type" },
       { "data": "GateWayIP" },
       { "data": "DataCenterId" }
];

function columnRows(Row, Data, Index) {
    var link = '<a class="btn btn-primary"';
    link = link + ' href="DataCenters/Edit/' + Data["DataCenterId"] + '">';
    link = link + 'Edit';
    link = link + '</a>';
    link = link + '&nbsp;<a class="btn btn-danger"';
    link = link + ' href="DataCenters/Delete/' + Data["DataCenterId"] + '">';
    link = link + 'Delete';
    link = link + '</a>';
    $('td:eq(4)', Row).html(link);
    if (Data["Type"]) {
        $('td:eq(2)', Row).html("Hyper-V");
    } else {
        $('td:eq(2)', Row).html("SkyTap");
    }
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
$("div.toolbar").html('<center><a href="DataCenters/Edit/0" class = "btn btn-primary">Add</a></center>');

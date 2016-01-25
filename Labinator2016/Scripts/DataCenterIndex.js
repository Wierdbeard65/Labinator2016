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
    var link = '<a class="btn"';
    link = link + ' href="DataCenters/Edit/' + Data["DataCenterId"] + '">';
    link = link + 'Edit';
    link = link + '</a>';
    link = link + '&nbsp;<a class="btn"';
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

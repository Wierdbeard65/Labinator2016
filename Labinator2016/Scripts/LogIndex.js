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

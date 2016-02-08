var oTable;
function refreshList(aj, dc, cr) {
    oTable = $('#datatable').DataTable(
        {
            "serverSide": true,
            "Searching": false,
            "Ordering": false,
            "ajax": aj,
            "dom": 'lfrtip<"toolbar">',
            "rowCallback": cr,
            "processing": true,
            "pagingType": "full_numbers",
            "deferRender": true,
            "columns": dc,
            "language": {
                "emptyTable": "Empty List"
            }
        }
    );
//    oTable.buttons().container().appendTo($('.col-sm-6:eq(0)', oTable.table().container()));
    //$('#datatable').on('draw.dt', function () {
    //    $(this).parent().find(".dataTables_paginate a").each(function () {
    //        if ($(this).children("span").length === 0) {
    //            $(this).html('&nbsp;<span class="btn">' + $(this).html() + '</span>&nbsp;');
    //        }
    //    });
    //});
    setInterval(function () {
        oTable.ajax.reload(null, false);// user paging is not reset on reload
    }, 15000);
}

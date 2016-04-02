var oTable;
function refreshList(aj, dc, cr) {
    oTable = $('#datatable').DataTable(
        {
            "scrollY": '1px',
            "serverSide": true,
            "Searching": false,
            "Ordering": false,
            "ajax": aj,
            "dom": '<"col-sm-6"l><"col-sm-6"f>t<"col-sm-4"i><"toolbar col-sm-2"><"col-sm-6"p>',
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
    resize();
    $(window).resize(function () {
        resize();
    });
    setInterval(function () {
        oTable.ajax.reload(null, false);// user paging is not reset on reload
    }, 15000);
}

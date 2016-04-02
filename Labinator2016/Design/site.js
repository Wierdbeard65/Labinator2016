var dataset = [
        {
            "VMName": "Machine 1",
            "IsActive": "True",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 2",
            "IsActive": "False",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 3",
            "IsActive": "True",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 4",
            "IsActive": "False",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 5",
            "IsActive": "True",
            "CourseMachineTempId": "1"
        },

       {
           "VMName": "Machine 1",
           "IsActive": "True",
           "CourseMachineTempId": "1"
       },
        {
            "VMName": "Machine 2",
            "IsActive": "False",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 3",
            "IsActive": "True",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 4",
            "IsActive": "False",
            "CourseMachineTempId": "1"
        },
        {
            "VMName": "Machine 5",
            "IsActive": "True",
            "CourseMachineTempId": "1"
        },

];
var oTable;
function refreshList(aj, dc, cr) {
    oTable = $('#datatable').DataTable(
        {
            "scrollY":'100px',
            "serverSide": false,
            "Searching": false,
            "Ordering": false,
            "data": dataset,
            "dom": '<"col-sm-6"l><"col-sm-6"f>t<"col-sm-5"i><"toolbar col-sm-2"><"col-sm-5"p>',
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
        //oTable.ajax.reload(null, false);// user paging is not reset on reload
    }, 15000);
}
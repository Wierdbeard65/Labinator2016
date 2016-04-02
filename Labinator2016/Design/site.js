var dataset = [
        {
            "EmailAddress": "student1@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "1"
        }, {
            "EmailAddress": "student2@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "2"
        }, {
            "EmailAddress": "student3@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "3"
        }, {
            "EmailAddress": "student4@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "4"
        }, {
            "EmailAddress": "student5@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "5"
        }, {
            "EmailAddress": "student6@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "6"
        }, {
            "EmailAddress": "student7@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "7"
        }, {
            "EmailAddress": "student8@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "8"
        }, {
            "EmailAddress": "student9@gmail.com",
            "IsInstructor": false,
            "IsAdministrator": false,
            "UserId": "9"
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
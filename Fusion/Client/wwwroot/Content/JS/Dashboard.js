function LoadDataTable(table) {
    $(table).DataTable().destroy();
    $(document).ready(function () {
        $(table).DataTable();
    });
}

function LoadSimpleDataTable(table) {
    $(table).DataTable().destroy();
    $(document).ready(function () {
        $(table).DataTable({ searching: false, pageLength: 5, lengthChange: false });
    });
}
var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Customer/Part/GetStorage',
        columns: [
            { 'data': 'name' },
            { 'data': 'benchmark' },
            { 'data': 'score' },
            { 'data': 'price' },
            { 'data': 'releaseDate' }
        ]
    });
});
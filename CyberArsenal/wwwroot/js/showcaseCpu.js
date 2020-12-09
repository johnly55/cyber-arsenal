var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Customer/Part/GetCpu',
        columns: [
            { 'data': 'name' },
            { 'data': 'benchmark' },
            { 'data': 'score' },
            { 'data': 'price' },
            { 'data': 'releaseDate' }
        ]
    });
});
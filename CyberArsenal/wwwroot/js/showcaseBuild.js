var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Customer/Home/GetBuilds',
        columns: [
            { 'data': 'name' },
            { 'data': 'cpu' },
            { 'data': 'gpu' },
            { 'data': 'ram' },
            { 'data': 'storage' }
        ]
    });
});
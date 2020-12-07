var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Customer/Build/GetAll',
        columns: [
            { 'data': 'name' },
            { 'data': 'cpu' },
            { 'data': 'gpu' },
            { 'data': 'ram' },
            { 'data': 'storage' }
        ]
    });
});
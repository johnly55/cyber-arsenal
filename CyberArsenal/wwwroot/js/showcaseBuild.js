var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Customer/Home/GetBuilds',
        columns: [
            { 'data': 'applicationUser.userName' },
            { 'data': 'cpuName' },
            { 'data': 'gpuName' },
            { 'data': 'ramName' },
            { 'data': 'storageName' },
            { 'data': 'score' }
        ]
    });
});
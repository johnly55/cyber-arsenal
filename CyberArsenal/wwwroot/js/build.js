var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Customer/Build/GetAll',
        columns: [
            { 'data': 'name' },
            { 'data': 'cpuName' },
            { 'data': 'gpuName' },
            { 'data': 'ramName' },
            { 'data': 'storageName' },
            { 'data': 'storageSecondary' },
            { 'data': 'motherBoard' },
            { 'data': 'powerSupply' },
            { 'data': 'case' },
            { 'data': 'score' },
            { 'data': 'date' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                    <div class="row">
                        <div class="pr-1 pb-1">
                            <a class="btn btn-info" style="width:90px"
                                href="/Customer/Build/Detail/${data}"><i class="fas fa-info-circle"></i><br /> Details</a>
                        </div>
                        <div class="pr-1 pb-1">
                            <a class="btn btn-primary" style="width:90px" 
                                href="/Customer/Build/Upsert/${data}"><i class="fas fa-pen-fancy"></i><br /> Edit</a>
                        </div>
                        <div>
                            <button onclick="Delete(${data})" class="btn btn-danger" style="width:90px;height:100%"><i class="fas fa-trash-alt"></i> Delete</button>
                        </div>
                    </div>
                            `
                }
            }
        ]
    });
});

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Data is not recoverable!',
        icon: 'warning',
        showDenyButton: true,
        confirmButtonText: `Delete`,
        denyButtonText: `Cancel`,
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: "/Customer/Build/Delete",
                data: { id: id },
                success: function (data) {
                    if (data.success) {
                        table.ajax.reload();
                        Swal.fire(
                            'Deleted!',
                            'Object successfully deleted.',
                            'success'
                        )
                    }
                    else {
                        table.ajax.reload();
                        Swal.fire(
                            'Not Athorized!',
                            'Delete aborted.',
                            'error'
                        )
                    }
                }
            });
        }
    })
}
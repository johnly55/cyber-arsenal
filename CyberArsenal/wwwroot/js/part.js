var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Admin/Part/GetAll',
        columns: [
            { 'data': 'type' },
            { 'data': 'name' },
            { 'data': 'benchmark' },
            { 'data': 'score' },
            { 'data': 'price' },
            { 'data': 'releaseDate' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                    <div class="row">
                        <div class="pr-1 pb-1">
                            <a class="btn btn-info" style="width:90px"
                                href="/Admin/Part/Detail/${data}"><i class="fas fa-info-circle"></i><br /> Details</a>
                        </div>
                        <div class="pr-1 pb-1">
                        <a class="btn btn-primary" style="width:90px" 
                            href="/Admin/Part/Upsert/${data}"><i class="fas fa-pen-fancy"></i><br /> Edit</a>
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
                url: "/Admin/Part/Delete",
                data: { id: id },
                success: function (data) {
                    table.ajax.reload();
                    Swal.fire(
                        'Deleted!',
                        'Object successfully deleted.',
                        'success'
                    )
                }
            });
        }
    })
}
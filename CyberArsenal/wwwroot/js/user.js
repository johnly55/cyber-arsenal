var table;
$(document).ready(function () {
    table = $('#myTable').DataTable({
        responsive: true,
        ajax: '/Admin/User/GetAll',
        columns: [
            { 'data': 'userName' },
            { 'data': 'email' },
            { 'data': 'lockoutEnabled' },
            { 'data': 'lockoutEnd' },
            {
                'data': { id: 'id', lockoutEnd: 'lockoutEnd', lockoutEnabled : 'lockoutEnabled' },
                'render': function (data) {
                    var today = new Date().getTime();
                    var lockoutDate = new Date(data.lockoutEnd).getTime();
                    if (data.lockoutEnabled == false) {
                        return `
                        <div>
                            <button class="btn bg-black" style="width:90px;height:60px"><i class="fas fa-lock"> Unlockable</i></button>
                        </div>
                            `
                    }
                    else {
                        if (today > lockoutDate) {
                            return `
                        <div>
                            <button onclick="LockUnlock('${data.id}')" class="btn btn-success" style="width:90px;height:60px"><i class="fas fa-lock-open"></i> Lock</button>
                        </div>
                            `
                        }
                        else {
                            return `
                        <div>
                            <button onclick="LockUnlock('${data.id}')" class="btn btn-danger" style="width:90px;height:60px"><i class="fas fa-lock"></i> Unlock</button>
                        </div>
                            `
                        }
                    }
                }
            }
        ]
    });
});

function LockUnlock(id) {
    console.log(id);
    Swal.fire({
        title: 'Are you sure?',
        text: 'Lock/Unlock',
        icon: 'question',
        showDenyButton: true,
        confirmButtonText: `Lock/Unlock`,
        denyButtonText: `Cancel`,
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Admin/User/LockUnlock",
                data: { id: id },
                success: function (data) {
                    if (data.success) {
                        table.ajax.reload();
                    }
                    else {
                        Swal.fire(
                            'Error',
                            'User cannot be found.',
                            'warning'
                        )
                    }
                }
            });
        }
    })
}
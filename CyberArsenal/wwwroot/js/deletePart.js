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
                data: { id: id},
                success: function (data) {
                    if (data.success) {
                        window.location = "/Admin/Part/Index";
                    }
                }
            });
        }
    })
}
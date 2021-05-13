var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url" : "/Admin/Author/GetAll"
        },
        "columns": [
            {"data": "authorId", "width": "5%"},
            {"data": "lastName", "width": "13%"},
            {"data": "firstName", "width": "12%"},
            { "data": "phoneNumber", "width": "10%" },
            { "data": "address", "width": "17%" },
            { "data": "city", "width": "10%" },
            { "data": "state", "width": "8%" },
            { "data": "zip", "width": "5%" },
            { "data": "contract", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Author/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/Author/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>&nbsp;
                                </a>
                            </div>
                            `;
                }, "width":"10%"
            },
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "Deleted data cannot be restored!",
        icon: "warning",
        buttons: true,
        danagerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

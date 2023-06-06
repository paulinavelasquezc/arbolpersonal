$(document).ready(function () {

    $('#ModalDetails').on('hidden.bs.modal', function (e) {        
        $('#divDescription').empty();
    });
});

function CorrectSolutions(data) {
    var table = $('#report').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        deferRender: true,
        columns: [
            { data: 'DecisionID', "visible": false },
            { data: 'NameDefect' },
            { data: 'NameDecision' },
            { data: 'DescriptionDecision' },
            { data: 'Cantidad' }
        ],
        rowId: 'extn',
        select: true,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Exportar a Excel',
                title: 'Soluciones más Correctas Excel'
            },
            {
                extend: 'pdf',
                text: 'Exportar a PDF',
                title: 'Soluciones más Correctas PDF'
            },
            {
                extend: 'print',
                text: 'Imprimir ',
                title: 'Soluciones más Correctas Imprimir'
            }
        ]
    });

    $('#report tbody').on('click', 'tr', function () {
        var data = table.row(this).data();

        $.ajax({
            url: '/OperatorReports/DetailsCorrectSolutions',
            type: "POST",
            data: '{_decisionID: "' + data.DecisionID + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status) {
                    $('#ModalDetails').modal('show');

                    var table = GenerateTable(data.userSystem);
                    $('#divDescription').append(table);
                    //$('#txtTitleCause').val(data.userSystem.Name);
                    //$('#textDescriptionCause').val(data.userSystem.Description);

                } else {
                    alert('-->');
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            } // fin success
        }); // fin ajax 
    });


    function GenerateTable(user) {
        var table = "<table id='tableUser' class='table table-bordered table-striped text-center'>";
        table += "<tr>";
        table += "<th>Nombre</th>";
        table += "<th>Apellido</th>";
        table += "<th>Máquina</th>";
        table += "<th>Fecha ingreso</th>";
        table += "</tr>";
        for (var i = 0; i < user.length; i++) {
            table += "<tr>";
            table += "<td>" + user[i].NameUser+"</td>";
            table += "<td>" + user[i].LastNameUser + "</td>";
            table += "<td>" + user[i].NameMachine + "</td>";
            table += "<td>" + user[i].UpdateDate + "</td>";
            table += "</tr>";
        }
        table += "</table>";

        return table;
    }
}
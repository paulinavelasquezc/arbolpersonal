$(document).ready(function () {


});

function loadUserSystem(data) {   
    var table = $('#report').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        deferRender: true,
        columns: [
            { data: 'NameUser' },
            { data: 'LastNameUser' },
            { data: 'UserName' },
            { data: 'NameMachine' },
            { data: 'UpdateDate' },
            { data: 'Cantidad' }
        ],
        rowId: 'extn',
        select: true,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Exportar a Excel',
                title: 'Reporte Usuario Excel'
            },
            {
                extend: 'pdf',
                text: 'Exportar a PDF',
                title: 'Reporte Usuario PDF'
            },
            {
                extend: 'print',
                text: 'Imprimir ',
                title: 'Reporte Usuario Imprimir'
            }
        ]
    });
}
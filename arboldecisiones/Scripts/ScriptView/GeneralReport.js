$(document).ready(function () {


});

function loadReportGeneral(data) {
    var table = $('#report').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        deferRender: true,
        columns: [
            { data: 'NameConfiguration' },
            { data: 'NameLocation' },
            { data: 'NameUser' },
            { data: 'NameMachine' },
            { data: 'DefectUpdateDate' },
            { data: 'NameDecision' },
            { data: 'DecisionUpdateDate' }
        ],
        rowId: 'extn',
        select: true,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Exportar a Excel',
                title: 'Reporte general Excel'
            },
            {
                extend: 'pdf',
                text: 'Exportar a PDF',
                title: 'Reporte general PDF'
            },
            {
                extend: 'print',
                text: 'Imprimir ',
                title: 'Reporte general Imprimir'
            }            
        ]
    });
}
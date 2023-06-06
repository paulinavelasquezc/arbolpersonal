$(document).ready(function () {


});

function loadDefectsConsulted(data) {
    var table = $('#report').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        deferRender: true,
        columns: [
            { data: 'NameConfiguration' },
            { data: 'NameLocation' },            
            { data: 'DefectUpdateDate' },
            { data: 'Cantidad' }            
        ],
        rowId: 'extn',
        select: true,
        dom: 'Bfrtip',        
        buttons: [
            {
                extend: 'excel',
                text: 'Exportar a Excel',
                title: 'Defecto más consultado Excel'
            },
            {
                extend: 'pdf',
                text: 'Exportar a PDF',
                title: 'Defecto más consultado PDF'
            },
            {
                extend: 'print',
                text: 'Imprimir ',
                title: 'Defecto más consultado Imprimir'
            }
        ]
    });
}
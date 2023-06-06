$(document).ready(function () {

    $('#btnAdd').click(function () {

        try {
            var continuar = true;


            if ($("#LocationID option:selected").val() === "0") {
                continuar = false;
                $('#LocationID').siblings('span.error').css('visibility', 'visible');
            }
            else {
                $('#LocationID').siblings('span.error').css('visibility', 'hidden');
            }

            if (!($('#textDefects').val().trim() !== '') || ($('#textDefects').val().trim().length <= 3)) {
                continuar = false;
                $('#textDefects').siblings('span.error').css('visibility', 'visible');
            }
            else {
                $('#textDefects').siblings('span.error').css('visibility', 'hidden');
            }

            $('#ConfigError').text('');

            if ($('#imgCarga img').length === 0) {
                continuar = false;
                $('#ConfigError').text("Debe ingresar mínimo una Imagen.");
            }
            if (continuar) {

                //Guardar la tabla TreeConfiguration
                var Process = new FormData();

                var MultimediaFileConfig = $(".filesConfig")[0].files[0];
                var typeFile = VaidateTypeFile($(".filesConfig")[0].files[0]);

                Process.append("MultimediaFile", MultimediaFileConfig);
                Process.append("typeFile", typeFile);
                Process.append("Name", $('#textDefects').val());
                Process.append("LocationID", $("#LocationID option:selected").val());

                $.ajax({
                    url: '/Defects/Save',
                    type: "POST",
                    data: Process,
                    processData: false,
                    contentType: false,
                    async: false,
                    beforeSend: function (objeto) {
                        //$('#progressModalDefect').css({ display: 'block' });
                    },
                    complete: function () {
                        //$('#progressModalDefect').css('display', 'none');
                        //$('#btnAddCause').prop("disabled", false);
                    },
                    success: function (data) {
                        if (data.status) {
                            window.location.href = '/Defects/Index';
                        }
                        else {
                            alert('Error servidor');
                        }

                    }, error: function (xhr, ajaxOptions, thrownError) {
                        console(xhr.status);
                    }
                });
            }
        }
        catch (e) {
            console(e);
        }
    });



});

//Validar el tamaño de imagen y vídeo 
function FileSize(FileSize, type) {
    var iSize = (FileSize / 1024);
    if (iSize / 1024 > 1) {
        if (((iSize / 1024) / 1024) > 1) {
            iSize = (Math.round(((iSize / 1024) / 1024) * 100) / 100);
            alert("EL tamaño del archivo no es permitido tamaño " + iSize + "Gb");
            return false;
        }
        else {
            iSize = (Math.round((iSize / 1024) * 100) / 100);

            if (type === "imagen") {
                if (iSize > 3) {
                    alert("EL tamaño del archivo no es permitido tamaño actual " + iSize + "Mb");
                    return false;
                } else {
                    return true;
                }
            }

            if (type === "video") {
                if (iSize > 50) {
                    alert("EL tamaño del archivo no es permitido tamaño actual " + iSize + "Mb");
                    alert("El tamaño máximo permitido para las vídeo es 50 Mb");
                    return false;
                } else {
                    return true;
                }
            }
        }
    }
    else {
        iSize = (Math.round(iSize * 100) / 100);
        return (iSize + "kb");
    }
}

function VaidateTypeFile(f) {
    if (f.type.match('image.*')) {
        return "Image";
    }

    if (f.type.match('video/MPEG') || f.type.match('video/mp4')) {
        return "video";
    }
}
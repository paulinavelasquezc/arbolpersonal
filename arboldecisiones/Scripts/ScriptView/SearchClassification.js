$(document).ready(function () {
    sessionStorage.setItem('locationID', 0);

    $("#CategoryID").change(function () {
        var locationID = 0;
        var categoryId = $("#CategoryID option:selected").val();
        if (categoryId !== 0) {
            if (sessionStorage.getItem('locationID') !== 0) {
                locationID = sessionStorage.getItem('locationID');
                ViewParcialDefectos(locationID, categoryId);
            } else {
                alert("Debe seleccionar una ubicación de la botella");
            }
        } else {
            if (sessionStorage.getItem('locationID') !== 0) {
                locationID = sessionStorage.getItem('locationID');
                ViewParcialDefectos(locationID, categoryId);
            }
        }
    });

    $("#btnFilter").click(function () {
        ViewParcialDefectosFilter();
    });

    $('#txtFilter').keyup(function (e) {
        if (e.keyCode === 13) {
            ViewParcialDefectosFilter();
        }
    });

    $("#btnFlint").click(function () {
        $("#Boca").attr("src", '/Content/Images/System/BocaFlint.png');
        $("#Cuello").attr("src", '/Content/Images/System/CuelloFlint.png');
        $("#Hombro").attr("src", '/Content/Images/System/HombroFlint.png');
        $("#Cuerpo").attr("src", '/Content/Images/System/CuerpoFlint.png');
        $("#Fondo").attr("src", '/Content/Images/System/FondoFlint.png');
    });

    $("#btnVerde").click(function () {
        $("#Boca").attr("src", '/Content/Images/System/BocaVerde.png');
        $("#Cuello").attr("src", '/Content/Images/System/CuelloVerde.png');
        $("#Hombro").attr("src", '/Content/Images/System/HombroVerde.png');
        $("#Cuerpo").attr("src", '/Content/Images/System/CuerpoVerde.png');
        $("#Fondo").attr("src", '/Content/Images/System/FondoVerde.png');
    });

    $("#btnAmbar").click(function () {
        $("#Boca").attr("src", '/Content/Images/System/BocaAmbar.png');
        $("#Cuello").attr("src", '/Content/Images/System/CuelloAmbar.png');
        $("#Hombro").attr("src", '/Content/Images/System/HombroAmbar.png');
        $("#Cuerpo").attr("src", '/Content/Images/System/CuerpoAmbar.png');
        $("#Fondo").attr("src", '/Content/Images/System/FondoAmbar.png');
    });
});

function ViewParcialDefectosAtras(_treeConfigurationID, _idFather) {  

    if (_idFather === 0) {
        if ($("#btnFilter").val() === "") {
            var locationID = "0";
            var categoryId = $("#CategoryID option:selected").val();
            if (categoryId !== "0") {
                if (sessionStorage.getItem('locationID') !== "0") {
                    locationID = sessionStorage.getItem('locationID');
                    ViewParcialDefectos(locationID, categoryId);
                } else {
                    alert("Debe seleccionar una ubicación de la botella");
                }
            } else {
                if (sessionStorage.getItem('locationID') !== "0") {
                    locationID = sessionStorage.getItem('locationID');
                    ViewParcialDefectos(locationID, categoryId);
                }
            }
        } else {
            ViewParcialDefectosFilter();
        }
    } else {
        $.get("ReturnCausasSoluciones", { _treeConfigurationID: _treeConfigurationID, _treeDecisionID: _idFather}, function (data) {
            
            $("#ViewParcial").empty();
            $("#ViewParcial").html(data);
        });
    }

}



function ViewParcialDefectosFilter() {
    SelectedBottleColor(99);
    $.get("ViewParcialDefectosFilter", { _filter: $("#txtFilter").val() }, function (data) {

        $("#ViewParcial").empty();
        $("#ViewParcial").html(data);
    });
}

function ViewParcialDefectos(id) {

    SelectedBottleColor(id);
    var categoryId = $("#CategoryID option:selected").val();

    sessionStorage.setItem('locationID', id);
    $.get("ViewParcialDefectos", { _locationID: id, _categoryID: categoryId }, function (data) {

        $("#ViewParcial").empty();
        $("#ViewParcial").html(data);
    });
}

function ViewParcialCausas(id) {

    $.get("ViewParcialCausas", { _treeConfigurationID: id }, function (data) {

        $("#ViewParcial").empty();
        $("#ViewParcial").html(data);
    });
}

/*Método que se ejecuta cuando se quiere ver el detalle de los defectos, busca las causas y las soluciones*/
function ViewParcialCausasSoluciones(treeConfigurationID, treeDecisionID, defect) {

    $.get("ViewParcialCausasSoluciones", { _treeConfigurationID: treeConfigurationID, _treeDecisionID: treeDecisionID, _defect: defect }, function (data) {

        $("#ViewParcial").empty();
        $("#ViewParcial").html(data);
    });
}

function ViewParcialSolution(id) {

    $.get("ViewParcialSolution", { _treeDecisionID: id }, function (data) {

        $("#ViewParcial").empty();
        $("#ViewParcial").html(data);
    });
}

function CloseModalMultimedia() {
    var modal = document.getElementById('ImageModal');
    modalImgImage = document.getElementById("imgShowImage");
    modalImgImage.src = "";
    modalImgVideo = document.getElementById("imgShowVideo");
    modalImgVideo.src = "";
    modal.style.display = "none";
    modalImgImage.style.display = "none";
    modalImgVideo.style.display = "none";
}

function ShowModalMultimedia(file, multimedia, text) {

    // Get the modal
    var modal = document.getElementById('ImageModal');
    var modalImg = "";

    // Get the image and insert it inside the modal - use its "alt" text as a caption  
    var captionText = document.getElementById("lblcaption");


    if (multimedia === "Image") {
        modalImg = document.getElementById("imgShowImage");
        modalImg.src = file.src;
        captionText.innerHTML = text;
    }
    if (multimedia === "Video") {
        modalImg = document.getElementById("imgShowVideo");
        modalImg.src = file.firstChild.src;
        captionText.innerHTML = file.alt;
        captionText.innerHTML = text;
    }
    modalImg.width = '30px';
    modalImg.height = '30px';
    modalImg.style.display = "block";
    modal.style.display = "block";
}


function ValidateSolution(TreeConfigurationID, TreeDecisionID) {

    BootstrapDialog.confirm({
        title: 'Solución',
        message: '¿Se solucionó?',
        type: BootstrapDialog.TYPE_INFO, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is false
        draggable: true, // <-- Default value is false
        btnCancelLabel: 'No', // <-- Default value is 'Cancel',
        btnOKLabel: 'Si', // <-- Default value is 'OK',
        btnOKClass: 'btn-info', // <-- If you didn't specify it, dialog type will be used,
        callback: function (result) {
            if (result) {                
                CorrectSolution(TreeConfigurationID, TreeDecisionID, true);
            } else {
                ViewParcialCausasSoluciones(TreeConfigurationID, TreeDecisionID, false);
            }
        }
    });

}

function SelectedBottleColor(IdLocation) {

    var id = parseInt(IdLocation);
    $("#General").removeClass("Selected-Bottle");
    $("#Boca").removeClass("Selected-Bottle");
    $("#Cuello").removeClass("Selected-Bottle");
    $("#Hombro").removeClass("Selected-Bottle");
    $("#Cuerpo").removeClass("Selected-Bottle");
    $("#Talon").removeClass("Selected-Bottle");
    $("#Fondo").removeClass("Selected-Bottle");

    if (id === 1) {
        $("#Boca").addClass("Selected-Bottle");
    } else if (id === 2) {
        $("#Cuello").addClass("Selected-Bottle");
    } else if (id === 3) {
        $("#Hombro").addClass("Selected-Bottle");
    } else if (id === 4) {
        $("#Cuerpo").addClass("Selected-Bottle");
    } else if (id === 5) {
        $("#Talon").addClass("Selected-Bottle");
    } else if (id === 6) {
        $("#Fondo").addClass("Selected-Bottle");
    } else if (id === 99) {
        $("#General").addClass("Selected-Bottle");
        $("#Boca").addClass("Selected-Bottle");
        $("#Cuello").addClass("Selected-Bottle");
        $("#Hombro").addClass("Selected-Bottle");
        $("#Cuerpo").addClass("Selected-Bottle");
        $("#Talon").addClass("Selected-Bottle");
        $("#Fondo").addClass("Selected-Bottle");
    }
}

function CloseModalDescription() {
    var modal = document.getElementById('ModalDescripcion');
    modal.style.display = "none";
}

function ShowModalDescription(Name, Definition) {
    // Get the modal
    var modal = document.getElementById('ModalDescripcion');

    var titleModalDescription = document.getElementById("titleModalDescription");
    var bodyModalDescription = document.getElementById("bodyModalDescription");

    titleModalDescription.innerHTML = Name;
    bodyModalDescription.innerHTML = Definition;

    modal.style.display = "block";
}

function CorrectSolution(treeConfigurationID, treeDecisionID, Solution) {

    $.get("CorrectSolution", { _treeConfigurationID: treeConfigurationID, _treeDecisionID: treeDecisionID, _solution: Solution }, function (data) {
        location.reload();
    });
}
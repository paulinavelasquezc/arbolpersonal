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

    function ViewParcialDefectosAtras() {

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
    }



    function ViewParcialDefectosFilter() {
        SelectedBottleColor(99);
        $.get("ViewParcialDefectosFilter", { _filter: $("#txtFilter").val() }, function (data) {

            $("#ViewParcial").empty();
            $("#ViewParcial").html(data);
        });
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
});

function ViewParcialDefectos(id) {

    SelectedBottleColor(id);
    var categoryId = $("#CategoryID option:selected").val();

    sessionStorage.setItem('locationID', id);
    $.get("ViewParcialDefectos", { _locationID: id, _categoryID: categoryId }, function (data) {

        $("#ViewParcial").empty();
        $("#ViewParcial").html(data);
    });
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
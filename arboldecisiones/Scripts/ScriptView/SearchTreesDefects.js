$(document).ready(function () {

    var imagen = document.getElementById("imgShowImage");
    imagen.addEventListener("click", function (e) {
        getFullscreen(this);
    }, false);


    function getFullscreen(element) {
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullScreen) {
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullscreen) {
            element.webkitRequestFullscreen();
        } else if (element.msRequestFullscreen) {
            element.msRequestFullscreen();
        }
    }
});
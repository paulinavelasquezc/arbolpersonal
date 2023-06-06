$(document).ready(function () {
    sessionStorage.setItem('counting', 0);
    //addConfig button click event
    $('#addConfig').click(function () {
        //validation and addConfig order items
        var isAllValid = true;

        var output = document.getElementById("listConfig");
        if (!(output.innerHTML !== '')) {
            isAllValid = false;
            $('#listConfig').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#listConfig').siblings('span.error').css('visibility', 'hidden');
        }

        //if (!($('#descripcionConfig').val().trim() !== '') || ($('#descripcionConfig').val().trim().length <= 5)) {
        //    isAllValid = false;
        //    $('#descripcionConfig').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#descripcionConfig').siblings('span.error').css('visibility', 'hidden');
        //}

        if (isAllValid) {
            var $newRow = $('#mainrowConfig').clone().removeAttr('id');

            //Replace addConfig button with remove button
            $('#addConfig', $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#descripcionConfig,#listConfig,#addConfig,#filesConfig', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //$('#files', $newRow).remove();
            $('#fileSpanConfig', $newRow).addClass('disabled');
            $(".filesConfig", $newRow).prop('disabled', true);
            //append clone row
            $('#ConfigImagenAdd tbody').append($newRow);

            //clear select data            
            $('#descripcionConfig').val('');
            document.getElementById("listConfig").innerHTML = "";
            $('#ConfigError').empty();
        }

    });

    //remove button click event
    $('#ConfigImagenAdd').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#btnAddCause').click(function () {

        var continuar = true;

        if (!($('#DefectID').val().trim() !== '')) {
            continuar = false;
            $('#DefectID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#DefectID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#LocationID').val().trim() !== '')) {
            continuar = false;
            $('#LocationID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#LocationID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#CategoryID').val().trim() !== '')) {
            continuar = false;
            $('#CategoryID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#CategoryID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#textDefinicion').val().trim() !== '') || ($('#textDefinicion').val().trim().length <= 5)) {
            continuar = false;
            $('#textDefinicion').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#textDefinicion').siblings('span.error').css('visibility', 'hidden');
        }

        if ($("#CategoryID option:selected").val() === "0") {
            continuar = false;
            $('#CategoryID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#CategoryID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($("#LocationID option:selected").val() === "0") {
            continuar = false;
            $('#LocationID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#LocationID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($("#DefectID option:selected").val() === "0") {
            continuar = false;
            $('#DefectID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#DefectID').siblings('span.error').css('visibility', 'hidden');
        }

        $('#ConfigError').text('');

        if ($('#ConfigImagenAdd tbody tr').length === 0) {
            continuar = false;
            $('#ConfigError').text("Debe ingresar mínimo una Imagen.");
        }

        if (continuar) {
            $("#divTreeConfiguration *").prop('disabled', true);
            $('#divTreeDecision').addClass('divShow ').removeClass('divHide');
        }

    });

    //addCause button click event
    $('#addCause').click(function () {
        //validation and addCause order items
        var isAllValid = true;


        var output = document.getElementById("listCause");
        if (!(output.innerHTML !== '')) {
            isAllValid = false;
            $('#listCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#listCause').siblings('span.error').css('visibility', 'hidden');
        }

        //if (!($('#descripcionCause').val().trim() !== '') || ($('#descripcionCause').val().trim().length <= 5)) {
        //    isAllValid = false;
        //    $('#descripcionCause').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#descripcionCause').siblings('span.error').css('visibility', 'hidden');
        //}

        if (isAllValid) {
            var $newRow = $('#mainrowCause').clone().removeAttr('id');

            //Replace addCause button with remove button
            $('#addCause', $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#descripcionCause,#listCause,#addCause,#filesCause', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            $('#fileSpanCause', $newRow).addClass('disabled');
            $(".filesCause", $newRow).prop('disabled', true);
            $(".descripcionCause", $newRow).prop('disabled', true);
            //append clone row
            $('#causeImagenAdd tbody').append($newRow);

            //clear select data            
            $('#descripcionCause').val('');
            document.getElementById("listCause").innerHTML = "";
            $('#causeError').empty();
        }

    });

    //remove button click event
    $('#causeImagenAdd').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    //addSolution button click event
    $('#addSolution').click(function () {
        //validation and addSolution order items
        var isAllValid = true;

        var output = document.getElementById("listSolution");
        if (!(output.innerHTML !== '')) {
            isAllValid = false;
            $('#listSolution').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#listSolution').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#descripcionSolution').val().trim() !== '') || ($('#descripcionSolution').val().trim().length <= 5)) {
            isAllValid = false;
            $('#descripcionSolution').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#descripcionSolution').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrowSolution').clone().removeAttr('id');

            //Replace addSolution button with remove button
            $('#addSolution', $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#descripcionSolution,#listSolution,#addSolution,#filesSolution', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            $('#fileSpanSolution', $newRow).addClass('disabled');
            $(".filesSolution", $newRow).prop('disabled', true);
            $(".descripcionSolution ", $newRow).prop('disabled', true);
            //append clone row
            $('#solutionImagenAdd tbody').append($newRow);

            //clear select data            
            $('#descripcionSolution').val('');
            //document.getElementById("listSolution").innerHTML = "";
            $('#solutionError').empty();
        }

    });

    //remove button click event
    $('#solutionImagenAdd').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    //accordion
    //document.addEventListener("DOMContentLoaded", function (event) {

    var acc = document.getElementsByClassName("accordionOn");
    var panel = document.getElementsByClassName('panelOn');

    for (var i = 0; i < acc.length; i++) {
        acc[i].onclick = function () {
            var setClasses = !this.classList.contains('active');
            setClass(acc, 'active', 'remove');
            setClass(panel, 'show', 'remove');

            if (setClasses) {
                this.classList.toggle("active");
                this.nextElementSibling.classList.toggle("show");
            }
        };
    }

    var accIn = document.getElementsByClassName("accordionIn");
    var panelIn = document.getElementsByClassName('panelIn');

    for (var e = 0; e < accIn.length; e++) {
        accIn[e].onclick = function () {
            var setClasses = !this.classList.contains('active');
            setClass(accIn, 'active', 'remove');
            setClass(panelIn, 'show', 'remove');

            if (setClasses) {
                this.classList.toggle("active");
                this.nextElementSibling.classList.toggle("show");
            }
        };
    }

    function setClass(els, className, fnName) {
        for (var i = 0; i < els.length; i++) {
            els[i].classList[fnName](className);
        }
    }

    //});

    $('#btnBlockSolution').click(function () {
        //validar si falta algún campo por ingresar
        var fields = validateEmptyFields();
        if (fields) {
            $('#btnBlockSolution').siblings('span.error').css('visibility', 'hidden');
            $(".fieldsSolution *").prop('disabled', true);
            $(".fieldsCause *").prop('disabled', true);
            var counting = parseInt(sessionStorage.getItem('counting'));
            var con = counting + 1;      
            var previousTitle = $("#titleAccotdionCause").text();
            //var titleCause = $("#txtTitleCause").val();
            //var title = previousTitle + " - Sub Causa - " + titleCause;
            $('#addedSolution').append(AddSolutionHtml(counting, previousTitle));
            txtTitleCause_change(counting, "Sub Causa");

            AccordionIn(counting);
            BtnBlockSolution(counting);
            AddSolutionButtonImg(counting);
            //Create event of Cause
            BtnBlockCauseSolutionSolution(counting);
            sessionStorage.setItem('counting', con);
        } else {
            $('#btnBlockSolution').siblings('span.error').css('visibility', 'visible');
        }
    });

    $('#btnBlockCauseSolution').click(function () {
        //validar si falta algún campo por ingresar
        var fields = validateEmptyFields();
        if (fields) {
            $('#btnBlockCauseSolution').siblings('span.error').css('visibility', 'hidden');
            $(".fieldsSolution *").prop('disabled', true);
            $(".fieldsCause *").prop('disabled', true);
            var counting = parseInt(sessionStorage.getItem('counting'));
            var con = counting + 1;
            var previousTitle = $("#titleAccotdionCause").text();
            var titleCause = $("#txtTitleCause").val();
            var title = previousTitle + " - Sub Causa - " + titleCause;
            $('#addedCauseSolution').append(AddCauseHtml(counting, title));
            AccordionOn(counting);
            BtnBlockCauseSolution(counting);
            AddCauseButtonImg(counting);
            //Create event of solution
            BtnBlockSolutionCause(counting);
            sessionStorage.setItem('counting', con);
        } else {
            $('#btnBlockCauseSolution').siblings('span.error').css('visibility', 'visible');
        }
    });

    $('#addedSolution').on('click', '.remove', function () {
        $(this).parents('.containerSolution').remove();
    });

    $('#btnBlockCauses').click(function () {
        //validar si falta algún campo por ingresar
        var fields = validateEmptyFields();
        if (fields) {
            $('#btnBlockCauses').siblings('span.error').css('visibility', 'hidden');
            $("#containerCauseMain *").prop('disabled', true);
            var $newRowCause = $('#containerCauseMain').clone().removeAttr('id');

            ////Replace addCause button with remove button
            //$('.removeCause', $newRowCause).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');
            //$('.removeSolution', $newRowCause).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');
            ////Mostrar botton eliminar 
            //$('.btnRemoveCause', $newRowCause).show();
            ////Mostrar botton eliminar 
            //$('.btnRemoveSolution', $newRowCause).show();

            //remove id attribute from new clone row
            $('#addedCauseSolution,#titleAccotdionCause,.txtTitleCause,.textDescriptionCause,.removeCause,.causeImagenAdd,.tableCause', $newRowCause).removeAttr('id');
            $('#addedSolution,.txtTitleSolution,.textDescriptionSolution,.removeSolution,.solutionImagenAdd,.tableSolution,.addedSolution', $newRowCause).removeAttr('id');

            ////$('span.error', $newRowCause).remove();
            $(".tableCause", $newRowCause).remove();
            $('.addCause', $newRowCause).remove();
            $('.btnBlockSolution', $newRowCause).remove();
            $('.btnBlockCauseSolution', $newRowCause).remove();

            $("p", $newRowCause).css("background-color", "#53ec73b0");


            //Solution
            //remove id attribute from new clone row
            //$('span.error', $newRowCause).remove();
            $(".tableSolution", $newRowCause).remove();
            $('.addSolution', $newRowCause).remove();

            ////#-----------------------------------------------------------------------------#

            //append clone row
            $('#addedCause').append($newRowCause);

            var acc = document.getElementsByClassName("accordionOn");
            //var panel = document.getElementsByClassName('panelOn');

            for (var e = 0; e < acc.length; e++) {
                acc[e].onclick = function () {
                    var panel = this.nextElementSibling;
                    var setClasses = this.classList.contains('active');

                    this.classList.remove('active');
                    panel.classList.remove('show');

                    if (!setClasses) {
                        this.classList.toggle("active");
                        this.nextElementSibling.classList.toggle("show");
                    }
                };
            }

            var accIn = document.getElementsByClassName("accordionIn");
            //var panelIn = document.getElementsByClassName('panelIn');

            for (var i = 0; i < accIn.length; i++) {
                accIn[i].onclick = function () {
                    var panelIn = this.nextElementSibling;
                    var setClasses = this.classList.contains('active');

                    this.classList.remove('active');
                    panelIn.classList.remove('show');

                    if (!setClasses) {
                        this.classList.toggle("active");
                        this.nextElementSibling.classList.toggle("show");
                    }
                };
            }


            var defectoName = $("#DefectID option:selected").text() + ". ";
            $("#titleAccotdionCause").html("Defecto:" + defectoName);
            $('#txtTitleCause').val('');
            $('#textDescriptionCause').val('');
            $('#descripcionCause').val('');
            document.getElementById("listCause").innerHTML = "";
            $("#causeImagenAdd tbody tr").remove();
            $("#addedSolution div").remove();
            $("#addedCauseSolution div").remove();
            $("#containerCauseMain *").prop('disabled', false);
        } else {
            $('#btnBlockCauses').siblings('span.error').css('visibility', 'visible');
        }

    });

    $('#addedCause').on('click', '.remove', function () {
        $(this).parents('.containerCause').remove();
    });



    $('#submit').click(function () {

        var AddCause = true;
        var AddSolution = true;

        if (!($('#txtTitleCause').val().trim() !== '')) {
            AddCause = false;
            $('#txtTitleCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#txtTitleCause').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#textDescriptionCause').val().trim() !== '')) {
            AddCause = false;
            $('#textDescriptionCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#textDescriptionCause').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#causeImagenAdd tbody tr').length === 0) {
            $('#causeImagenAdd').siblings('span.error').css('visibility', 'visible');
            AddCause = false;
        } else {
            $('#descripcionCause').siblings('span.error').css('visibility', 'hidden');
            $('#listCause').siblings('span.error').css('visibility', 'hidden');
        }

        if (!AddCause) {
            $('#btnBlockCauses').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#btnBlockCauses').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#txtTitleSolution').val().trim() !== '')) {
            AddSolution = false;
            $('#txtTitleSolution').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#txtTitleSolution').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('#textDescriptionSolution').val().trim() !== '')) {
            AddSolution = false;
            $('#textDescriptionSolution').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#textDescriptionSolution').siblings('span.error').css('visibility', 'hidden');
        }


        if ($('#solutionImagenAdd tbody tr').length === 0) {
            $('#solutionError').text("Debe ingresar mínimo una Imagen.");
            AddSolution = false;
        } else {

            $('#descripcionSolution').siblings('span.error').css('visibility', 'hidden');
            $('#listSolution').siblings('span.error').css('visibility', 'hidden');
        }
        if (!AddSolution) {
            $('#btnBlockSolution').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#btnBlockSolution').siblings('span.error').css('visibility', 'hidden');
        }


        if ((AddCause) && (AddSolution)) {

            //Guardar la tabla TreeConfiguration
            var TreeConfiData = new FormData();
            $('#ConfigImagenAdd tbody tr').each(function (index, ele) {

                var MultimediaFileConfig = ($(".filesConfig", this))[0].files[0];
                var typeFile = VaidateTypeFile(($(".filesConfig", this))[0].files[0]);
                var DescriptionConfig = $('.descripcionConfig', this).val();

                TreeConfiData.append("MultimediaFile", MultimediaFileConfig);
                TreeConfiData.append("typeFile", typeFile);
                TreeConfiData.append("Description", DescriptionConfig);
            });

            TreeConfiData.append("Definition", $('#textDefinicion').val());
            TreeConfiData.append("CategoryID", $("#CategoryID option:selected").val());
            TreeConfiData.append("LocationID", $("#LocationID option:selected").val());
            TreeConfiData.append("DefectID", $("#DefectID option:selected").val());

            $.ajax({
                url: '/ConfigTreeDecisions/SaveTreeConfiguration',
                type: "POST",
                data: TreeConfiData,
                processData: false,
                contentType: false,
                async: false,
                success: function (data) {
                    if (data.status) {
                        SaveTreeDecisionsCause(data.treeConfigurationID);
                    }
                    else {
                        alert('Configuración');
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
    });

    function SaveTreeDecisionsCause(treeConfigurationID) {

        $('.containerCause').each(function (index, eleContCause) {
            var TreeDecisCauseData = new FormData();

            var txtTitleCause = $('.txtTitleCause', this).val();
            var textDescriptionCause = $('.textDescriptionCause', this).val();
            var ckbACtivoCause = $('.ckbACtivoCause').prop('checked');
            var type = "Cause";

            TreeDecisCauseData.append("treeConfigID", treeConfigurationID);
            TreeDecisCauseData.append("treeID", 0);
            TreeDecisCauseData.append("txtTitle", txtTitleCause);
            TreeDecisCauseData.append("textDescription", textDescriptionCause);
            TreeDecisCauseData.append("ckbACtivo", ckbACtivoCause);
            TreeDecisCauseData.append("type", type);


            $('.causeImagenAdd tbody tr', this).each(function (index, ele) {

                var MultimediaFileCause = ($('.filesCause', this))[0].files[0];
                var typeFile = VaidateTypeFile(($(".filesCause", this))[0].files[0]);
                var DescriptionCuse = $('.descripcionCause', this).val();

                TreeDecisCauseData.append("MultimediaFile", MultimediaFileCause);
                TreeDecisCauseData.append("typeFile", typeFile);
                TreeDecisCauseData.append("Description", DescriptionCuse);

            });

            $.ajax({
                url: '/ConfigTreeDecisions/SaveTreeDecision',
                type: "POST",
                data: TreeDecisCauseData,
                processData: false,
                contentType: false,
                async: false,
                success: function (dataCause) {
                    if (dataCause.status) {
                        $('.containerSolution', eleContCause).each(function (indexCont, ele) {
                            var TreeDecisSolutionData = new FormData();

                            var txtTitleSolution = $('.txtTitleSolution', this).val();
                            var textDescriptionSolution = $('.textDescriptionSolution', this).val();
                            var ckbACtivoSolution = $('.ckbACtivoSolution').prop('checked');
                            var type = "Solution";

                            TreeDecisSolutionData.append("treeConfigID", treeConfigurationID);
                            TreeDecisSolutionData.append("treeID", dataCause.treeDecisionID);
                            TreeDecisSolutionData.append("txtTitle", txtTitleSolution);
                            TreeDecisSolutionData.append("textDescription", textDescriptionSolution);
                            TreeDecisSolutionData.append("ckbACtivo", ckbACtivoSolution);
                            TreeDecisSolutionData.append("type", type);

                            $('.solutionImagenAdd tbody tr', this).each(function (indexImage, ele) {

                                var MultimediaFileSolution = ($('.filesSolution', this))[0].files[0];
                                var typeFile = VaidateTypeFile(($(".filesSolution", this))[0].files[0]);
                                var DescriptionSolution = $('.descripcionSolution', this).val();

                                TreeDecisSolutionData.append("MultimediaFile", MultimediaFileSolution);
                                TreeDecisSolutionData.append("typeFile", typeFile);
                                TreeDecisSolutionData.append("Description", DescriptionSolution);

                            });

                            $.ajax({
                                url: '/ConfigTreeDecisions/SaveTreeDecision',
                                type: "POST",
                                data: TreeDecisSolutionData,
                                processData: false,
                                contentType: false,
                                async: false,
                                success: function (dataSolution) {
                                    if (dataSolution.status) {
                                        alert('Correcto')
                                    } else {
                                        alert('Solution');
                                    }
                                }, error: function (xhr, ajaxOptions, thrownError) {
                                    alert(xhr.status);
                                    alert(thrownError);
                                }
                            });


                        });

                    } else {
                        alert('Cause');
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                } // fin success
            }); // fin ajax SaveTreeDecision
        }); //fin containerCause
    } //fin SaveTreeDecisionsCause


    $('#DefectID').change(function () {
        $("#titleAccotdionCause").html("Defecto:" + $("#DefectID option:selected").text() + ".");
    });

    function AddSolutionHtml(id, title) {
        var Solution = '';
        Solution += '        <div id="containerSolution' + id + '" class="containerSolution">';
        Solution += '            <p style="background-color: #A7D1EA;" class="titlesMain text-center"><strong>Solución</strong></p>';
        Solution += '            <p id="titleAccotdionSolution' + id + '" class="accordionIn" style="background-color: #A7D1EA;">' + title + '</p>';
        Solution += '            <div id="panelIn' + id + '" class="panelIn">';
        Solution += '                <div id="btnRemoveSolution' + id + '" class="btnRemoveSolution row text-center" style="display: none;">';
        Solution += '                    <div class="form-group">';
        Solution += '                        <div class="col-xs-12 col-sm-12 col-md-12">';
        Solution += '                            <input type="button" id="removeSolution' + id + '" value="Eliminar" class="removeSolution btn btn-danger" />';
        Solution += '                        </div>';
        Solution += '                    </div>';
        Solution += '                </div>';
        Solution += '                <div id="fieldsSolution' + id + '" class="fieldsSolution">';
        Solution += '                    <div class="row">';
        Solution += '                        <div class="col-xs-12 col-sm-12 col-md-12 heightDiv">';
        Solution += '                            <div class="form-group">';
        Solution += '                                <div class="col-md-1">';
        Solution += '                                    <label class="control-label">Titulo:</label>';
        Solution += '                                </div>';
        Solution += '                                <div class="col-md-9">';
        Solution += '                                    <textarea id="txtTitleSolution' + id + '" class="txtTitleSolution form-control" maxlength="50"></textarea>';
        Solution += '                                    <span class="error">Debe ingresar el titulo de la Solución, mínimo 6 caracteres</span>';
        Solution += '                                </div>';
        Solution += '                                <div class="control-label col-md-1">';
        Solution += '                                    <label class="control-label">Activo:</label>';
        Solution += '                                </div>';
        Solution += '                                <div class="col-md-1 checkbox">';
        Solution += '                                    <div class="checkbox">';
        Solution += '                                        <input type="checkbox" checked="checked" class="ckbACtivoSolution" name="ckbACtivoSolution">';
        Solution += '                                        </div>';
        Solution += '                                    </div>';
        Solution += '                                </div>';
        Solution += '                            </div>';
        Solution += '                        </div>';
        Solution += '                        <div class="row">';
        Solution += '                            <div class="col-xs-12 col-sm-12 col-md-12 heightDiv">';
        Solution += '                                <div class="form-group">';
        Solution += '                                    <div class="col-md-1">';
        Solution += '                                        <label class="control-label">Descripción:</label>';
        Solution += '                                    </div>';
        Solution += '                                    <div class="col-md-11">';
        Solution += '                                        <textarea id="textDescriptionSolution' + id + '" class="textDescriptionSolution form-control" maxlength="250"></textarea>';
        Solution += '                                        <span class="error">Debe ingresar el descripción de la Solución , mínimo 6 caracteres</span>';
        Solution += '                                    </div>';
        Solution += '                                </div>';
        Solution += '                            </div>';
        Solution += '                        </div>';
        Solution += '                        <div class="imageSolution">';
        Solution += '                            <label class="control-label col-md-4">';
        Solution += '                                Imagenes de la Solución';
        Solution += '                            </label>';
        Solution += '                            <div class="col-md-12">';
        Solution += '                                <table class="tableSolution table table-responsive" style="margin-bottom: -12px" id="tableSolution">';
        Solution += '                                    <tr>';
        Solution += '                                        <td><label class="control-label">Imagen</label></td>';
        Solution += '                                        <td><label class="control-label">Descripción</label></td>';
        Solution += '                                        <td><label class="control-label">Buscar</label></td>';
        Solution += '                                        <td>&nbsp;</td>';
        Solution += '                                    </tr>';
        Solution += '                                    <tr class="mycontainer" id="mainrowSolution' + id + '">';
        Solution += '                                        <td width="128px">';
        Solution += '                                            <output id="listSolution' + id + '" class="listSolution"></output>';
        Solution += '                                            <span class="error">Debe Seleccionar una Imagen</span>';
        Solution += '                                        </td>';
        Solution += '                                        <td width="700px">';
        Solution += '                                            <textarea id="descripcionSolution' + id + '" class="descripcionSolution form-control" maxlength="250"></textarea>';
        Solution += '                                            <span class="error">Debe Ingresar la descripción del campo, mínimo 6 caracteres</span>';
        Solution += '                                        </td>';
        Solution += '                                        <td width="80px">';
        Solution += '                                            <span id="fileSpanSolution' + id + '" class="btn btn-primary btn-file glyphicon glyphicon-search">';
        Solution += '                                                <input type="file" id="filesSolution' + id + '" class="filesSolution" name="files" onchange="ShowImagen(this,' + "'listSolution" + id + "'" + ')" />';
        Solution += '                                            </span>';
        Solution += '                                        </td>';
        Solution += '                                        <td width="80px">';
        Solution += '                                            <input type="button" id="addSolution' + id + '" value="Agregar" style="width:80px" class="addSolution btn btn-success" />';
        Solution += '                                        </td>';
        Solution += '                                    </tr>';
        Solution += '                                </table>';
        Solution += '                            </div>';
        Solution += '                            <div id="listSolucion' + id + '">';
        Solution += '                                <div class="col-md-12">';
        Solution += '                                    <table class="solutionImagenAdd table table-responsive" id="solutionImagenAdd' + id + '"><tbody></tbody></table>';
        Solution += '                                    <span class="error" id="solutionError' + id + '" style="color:red">Debe ingresar mínimo una Imagen</span>';
        Solution += '                                </div>';
        Solution += '                            </div>';
        Solution += '                        </div>';
        Solution += '                    </div>';
        Solution += '                    <div class="row">';
        Solution += '                        <div class="col-xs-12 col-sm-12 col-md-12 text-center">';
        Solution += '                            <div class="col-xs-6 col-sm-6 col-md-6">';
        Solution += '                                <div class="form-group">';
        Solution += '                                    <input type="button" id="btnBlockSolution' + id + '" value="Crear Solución" class="btnBlockSolution btn btn-success" />';
        Solution += '                                    <span class="error">Debe ingresar toda la información de la solución anterior para poder crear otra.</span>';
        Solution += '                                </div>';
        Solution += '                            </div>';
        Solution += '                            <div class="col-xs-6 col-sm-6 col-md-6">';
        Solution += '                                <div class="form-group">';
        Solution += '                                    <input type="button" id="btnBlockCauseSolution' + id + '" value="Crear Sub Causa" class="btnBlockCauseSolution btn btn-success" />';
        Solution += '                                    <span class="error">Debe ingresar toda la información de la Causa anterior para poder crear otra.</span>';
        Solution += '                                </div>';
        Solution += '                            </div>';
        Solution += '                        </div>';
        Solution += '                    </div>';
        Solution += '                    <div id="addedCauseSolution' + id + '">';
        Solution += '                    </div>';
        Solution += '                    <div id="addedSolution' + id + '">';
        Solution += '                    </div>';
        Solution += '                </div>';
        Solution += '            </div>';
        return Solution;
    }

    function AddCauseHtml(id, title) {
        var Cause = '';
        Cause += '            <div id="containerCause' + id + '" class="containerCause">';
        Cause += '            <p style="background-color: #F4D2F9;" class="titlesMain text-center"><strong>Causa</strong></p>';
        Cause += '                <p id="titleAccotdionCause' + id + '" class="accordionOn" style="background-color: #F4D2F9;">' + title + '</p>';
        Cause += '                <div id="panelOn' + id + '" class="panelOn">';
        Cause += '                    <div id="btnRemoveCause' + id + '" class="btnRemoveCause row text-center" style="display: none;">';
        Cause += '                        <div class="form-group">';
        Cause += '                            <div class="col-xs-12 col-sm-12 col-md-12">';
        Cause += '                                <input type="button" id="removeCause' + id + '" value="Eliminar" class="removeCause btn btn-danger" />';
        Cause += '                            </div>';
        Cause += '                        </div>';
        Cause += '                    </div>';
        Cause += '                    <div id="fieldsCause' + id + '" class="fieldsCause">';
        Cause += '                    <div class="row">';
        Cause += '                        <div class="col-xs-12 col-sm-12 col-md-12 heightDiv">';
        Cause += '                            <div class="form-group">';
        Cause += '                                <div class="col-md-1">';
        Cause += '                                    <label class="control-label">Titulo:</label>';
        Cause += '                                </div>';
        Cause += '                                <div class="col-md-9">';
        Cause += '                                    <textarea id="txtTitleCause' + id + '" class="txtTitleCause form-control" maxlength="50"></textarea>';
        Cause += '                                    <span class="error">Debe ingresar el titulo de la Causa, mínimo 6 caracteres</span>';
        Cause += '                                </div>';
        Cause += '                                <div class="control-label col-md-1">';
        Cause += '                                    <label class="control-label">Activo:</label>';
        Cause += '                                </div>';
        Cause += '                                <div class="col-md-1 checkbox">';
        Cause += '                                    <div class="checkbox">';
        Cause += '                                        <input type="checkbox" checked="checked" class="ckbACtivoCause" name="ckbACtivoCause">';
        Cause += '                                    </div>';
        Cause += '                                    </div>';
        Cause += '                                </div>';
        Cause += '                            </div>';
        Cause += '                        </div>';
        Cause += '                        <div class="row">';
        Cause += '                            <div class="col-xs-12 col-sm-12 col-md-12 heightDiv">';
        Cause += '                                <div class="form-group">';
        Cause += '                                    <div class="col-md-1">';
        Cause += '                                        <label class="control-label">Descripción:</label>';
        Cause += '                                    </div>';
        Cause += '                                    <div class="col-md-11">';
        Cause += '                                        <textarea id="textDescriptionCause' + id + '" class="textDescriptionCause form-control" maxlength="250"></textarea>';
        Cause += '                                        <span class="error">Debe ingresar el descrición de la Causa, mínimo 6 caracteres</span>';
        Cause += '                                    </div>';
        Cause += '                                </div>';
        Cause += '                            </div>';
        Cause += '                        </div>';
        Cause += '                        <div class="imageCause">';
        Cause += '                            <label class="control-label col-md-4">';
        Cause += '                                Imagenes de la Causa';
        Cause += '                        </label>';
        Cause += '                            <div class="col-md-12">';
        Cause += '                                <table class="tableCause table table-responsive" style="margin-bottom: -12px" id="tableCause' + id + '">';
        Cause += '                                    <tr>';
        Cause += '                                        <td><label class="control-label">Imagen</label></td>';
        Cause += '                                        <td><label class="control-label">Descripción</label></td>';
        Cause += '                                        <td><label class="control-label">Buscar</label></td>';
        Cause += '                                        <td>&nbsp;</td>';
        Cause += '                                    </tr>';
        Cause += '                                    <tr class="mycontainer" id="mainrowCause' + id + '">';
        Cause += '                                        <td width="128px">';
        Cause += '                                            <output id="listCause' + id + '" class="listCause"></output>';
        Cause += '                                            <span class="error">Debe Seleccionar una Imagen</span>';
        Cause += '                                        </td>';
        Cause += '                                        <td width="700px">';
        Cause += '                                            <textarea id="descripcionCause' + id + '" class="descripcionCause form-control" maxlength="250"></textarea>';
        Cause += '                                            <span class="error">Debe Ingresar la descripción del campo, mínimo 6 caracteres</span>';
        Cause += '                                        </td>';
        Cause += '                                        <td width="80px">';
        Cause += '                                            <span id="fileSpanCause' + id + '" class="btn btn-primary btn-file glyphicon glyphicon-search">';
        Cause += '                                                <input type="file" id="filesCause' + id + '" class="filesCause" name="files" onchange="ShowImagen(this,' + "'listCause" + id + "'" + ')" />';
        Cause += '                                            </span>';
        Cause += '                                        </td>';
        Cause += '                                        <td width="80px">';
        Cause += '                                            <input type="button" id="addCause' + id + '" value="Agregar" style="width:80px" class="addCause' + id + ' btn btn-success" />';
        Cause += '                                        </td>';
        Cause += '                                    </tr>';
        Cause += '                                </table>';
        Cause += '                            </div>';
        Cause += '                            <div id="listCausa' + id + '">';
        Cause += '                                <div class="col-md-12">';
        Cause += '                                    <table class="causeImagenAdd table table-responsive" id="causeImagenAdd' + id + '"><tbody></tbody></table>';
        Cause += '                                    <span class="error" id="causeError' + id + '" style="color:red">Debe ingresar mínimo una Imagen</span>';
        Cause += '                                </div>';
        Cause += '                            </div>';
        Cause += '                        </div>';
        Cause += '                        </div >';
        Cause += '                        <div class="row">';
        Cause += '                            <div class="col-xs-12 col-sm-12 col-md-12 text-center">';
        Cause += '                                <div class="col-xs-6 col-sm-6 col-md-6">';
        Cause += '                                    <div class="form-group">';
        Cause += '                                        <input type="button" id="btnBlockSolution' + id + '" value="Crear Solución" class="btnBlockSolution btn btn-success" />';
        Cause += '                                        <span class="error">Debe ingresar toda la información de la solución anterior para poder crear otra.</span>';
        Cause += '                                    </div>';
        Cause += '                                </div>';
        Cause += '                                <div class="col-xs-6 col-sm-6 col-md-6">';
        Cause += '                                    <div class="form-group">';
        Cause += '                                        <input type="button" id="btnBlockCauseSolution' + id + '" value="Crear Sub Causa" class="btnBlockCauseSolution btn btn-success" />';
        Cause += '                                        <span class="error">Debe ingresar toda la información de la Causa anterior para poder crear otra.</span>';
        Cause += '                                    </div>';
        Cause += '                                </div>';
        Cause += '                            </div>';
        Cause += '                        </div>';
        Cause += '                        <div id="addedCauseSolution' + id + '">';
        Cause += '                        </div>';
        Cause += '                        <div id="addedSolution' + id + '">';
        Cause += '                        </div>';
        Cause += '                    </div>';
        Cause += '                </div>';
        return Cause;
    }


    function BtnBlockCauseSolution(id) {
        $('#btnBlockCauseSolution' + id).click(function () {
            //validar si falta algún campo por ingresar
            var fields = validateEmptyFields();
            if (fields) {
                $('#btnBlockCauseSolution' + id).siblings('span.error').css('visibility', 'hidden');
                var counting = parseInt(sessionStorage.getItem('counting'));
                $(".fieldsSolution *").prop('disabled', true);
                $(".fieldsCause *").prop('disabled', true);
                var con = counting + 1;
                var previousTitle = $("#titleAccotdionCause" + id).text();
                var titleCause = $('#txtTitleCause' + id).val();
                var title = previousTitle + " - Sub Causa - " + titleCause;
                $('#addedCauseSolution' + id).append(AddCauseHtml(counting, title));
                AccordionOn(counting);
                BtnBlockCauseSolution(counting);
                AddCauseButtonImg(counting);
                //Create event of solution
                BtnBlockSolutionCause(counting);
                sessionStorage.setItem('counting', con);
            } else {
                $('#btnBlockCauseSolution' + id).siblings('span.error').css('visibility', 'visible');
            }
        });
    }

    function BtnBlockCauseSolutionSolution(id) {
        $('#btnBlockCauseSolution' + id).click(function () {
            //validar si falta algún campo por ingresar
            var fields = validateEmptyFields();
            if (fields) {
                $('#btnBlockCauseSolution' + id).siblings('span.error').css('visibility', 'hidden');
                var counting = parseInt(sessionStorage.getItem('counting'));
                $(".fieldsSolution *").prop('disabled', true);
                $(".fieldsCause *").prop('disabled', true);
                var con = counting + 1;
                var previousTitle = $("#titleAccotdionSolution" + id).text();
                var titleSolution = $('#txtTitleSolution' + id).val();
                var title = previousTitle + " - Solución - " + titleSolution;
                $('#addedCauseSolution' + id).append(AddCauseHtml(counting, title));
                AccordionOn(counting);
                BtnBlockCauseSolution(counting);
                AddCauseButtonImg(counting);
                //Create event of solution
                BtnBlockSolutionCause(counting);
                sessionStorage.setItem('counting', con);
            } else {
                $('#btnBlockCauseSolution' + id).siblings('span.error').css('visibility', 'visible');
            }
        });
    }

    function BtnBlockSolution(id) {
        $('#btnBlockSolution' + id).click(function () {
            //validar si falta algún campo por ingresar
            var fields = validateEmptyFields();
            if (fields) {
                $('#btnBlockSolution' + id).siblings('span.error').css('visibility', 'hidden');
                var counting = parseInt(sessionStorage.getItem('counting'));
                $(".fieldsSolution *").prop('disabled', true);
                $(".fieldsCause *").prop('disabled', true);
                var con = counting + 1;

                var previousTitle = $("#titleAccotdionSolution" + id).text();
                var titleSolution = $("#txtTitleSolution" + id).val();
                var title = previousTitle + " - Solución - " + titleSolution;

                $('#addedSolution' + id).append(AddSolutionHtml(counting, title));
                AccordionIn(counting);
                BtnBlockSolution(counting);
                AddSolutionButtonImg(counting);
                //Create event of Cause 
                BtnBlockCauseSolutionSolution(counting);
                sessionStorage.setItem('counting', con);
            } else {
                $('#btnBlockSolution' + id).siblings('span.error').css('visibility', 'visible');
            }
        });
    }


    function BtnBlockSolutionCause(id) {
        $('#btnBlockSolution' + id).click(function () {
            //validar si falta algún campo por ingresar
            var fields = validateEmptyFields();
            if (fields) {
                $('#btnBlockSolution' + id).siblings('span.error').css('visibility', 'hidden');
                var counting = parseInt(sessionStorage.getItem('counting'));
                $(".fieldsSolution *").prop('disabled', true);
                $(".fieldsCause *").prop('disabled', true);
                var con = counting + 1;
                var previousTitle = $('#titleAccotdionCause' + id).text();
                var titleCause = $('#txtTitleCause' + id).val();
                var title = previousTitle + " - Sub Causa - " + titleCause;
                $('#addedSolution' + id).append(AddSolutionHtml(counting, title));
                AccordionIn(counting);
                BtnBlockSolution(counting);
                AddSolutionButtonImg(counting);
                //Create event of Cause 
                BtnBlockCauseSolutionSolution(counting);
                sessionStorage.setItem('counting', con);
            } else {
                $('#btnBlockSolution' + id).siblings('span.error').css('visibility', 'visible');
            }
        });
    }



    function AddCauseButtonImg(id) {
        //addCause button click event
        $('#addCause' + id).click(function () {
            //validation and addCause order items
            var isAllValid = true;

            var output = document.getElementById("listCause" + id);
            if (!(output.innerHTML !== '')) {
                isAllValid = false;
                $('#listCause' + id).siblings('span.error').css('visibility', 'visible');
            }
            else {
                $('#listCause' + id).siblings('span.error').css('visibility', 'hidden');
            }

            if (isAllValid) {
                var $newRow = $('#mainrowCause' + id).clone().removeAttr('id');

                //Replace addCause button with remove button
                $('#addCause' + id, $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');

                //remove id attribute from new clone row                
                $('#descripcionCause' + id + ',#listCause' + id + ',#addCause' + id + ',#filesCause' + id, $newRow).removeAttr('id');
                $('span.error', $newRow).remove();
                $('#fileSpanCause' + id, $newRow).addClass('disabled');
                $(".filesCause", $newRow).prop('disabled', true);
                $(".descripcionCause", $newRow).prop('disabled', true);
                //append clone row
                $('#causeImagenAdd' + id + ' tbody').append($newRow);

                //clear select data            
                $('#descripcionCause' + id).val('');
                document.getElementById("listCause" + id).innerHTML = "";
                $('#causeError' + id).empty();
            }
        });

        //remove button click event
        $('#causeImagenAdd' + id).on('click', '.remove', function () {
            $(this).parents('tr').remove();
        });
    }

    function AddSolutionButtonImg(id) {
        //addSolution button click event
        $('#addSolution' + id).click(function () {
            //validation and addSolution order items
            var isAllValid = true;


            var output = document.getElementById("listSolution" + id);
            if (!(output.innerHTML !== '')) {
                isAllValid = false;
                $('#listSolution' + id).siblings('span.error').css('visibility', 'visible');
            }
            else {
                $('#listSolution' + id).siblings('span.error').css('visibility', 'hidden');
            }

            if (isAllValid) {
                var $newRow = $('#mainrowSolution' + id).clone().removeAttr('id');

                //Replace addSolution button with remove button
                $('#addSolution' + id, $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');

                //remove id attribute from new clone row                
                $('#descripcionSolution' + id + ',#listSolution' + id + ',#addSolution' + id + ',#filesSolution' + id, $newRow).removeAttr('id');
                $('span.error', $newRow).remove();
                $('#fileSpanSolution' + id, $newRow).addClass('disabled');
                $(".filesSolution", $newRow).prop('disabled', true);
                $(".descripcionSolution", $newRow).prop('disabled', true);
                //append clone row
                $('#solutionImagenAdd' + id + ' tbody').append($newRow);

                //clear select data            
                $('#descripcionSolution' + id).val('');
                document.getElementById("listSolution" + id).innerHTML = "";
                $('#solutionError' + id).empty();
            }
        });

        //remove button click event
        $('#solutionImagenAdd' + id).on('click', '.remove', function () {
            $(this).parents('tr').remove();
        });
    }

    function AccordionOn(id) {
        var acc = document.getElementById('titleAccotdionCause' + id);
        var panel = document.getElementById('panelOn' + id);

        acc.onclick = function () {
            var setClasses = acc.classList.contains('active');

            acc.classList.remove('active');
            panel.classList.remove('show');

            if (!setClasses) {
                this.classList.toggle("active");
                this.nextElementSibling.classList.toggle("show");
            }
        };
    }

    function AccordionIn(id) {
        var acc = document.getElementById('titleAccotdionSolution' + id);
        var panel = document.getElementById('panelIn' + id);

        acc.onclick = function () {
            var setClasses = acc.classList.contains('active');

            acc.classList.remove('active');
            panel.classList.remove('show');

            if (!setClasses) {
                this.classList.toggle("active");
                this.nextElementSibling.classList.toggle("show");
            }
        };
    }

    function validateEmptyFields() {
        var AddCauseP = true;

        $('#containerCauseMain .txtTitleCause').each(function (index, ele) {
            if (!(ele.value !== '') || (ele.value.length <= 5)) {
                AddCauseP = false;
                $("#" + ele.id).siblings('span.error').css('visibility', 'visible');
                return false;
            }
            else {
                $("#" + ele.id).siblings('span.error').css('visibility', 'hidden');
            }
        });

        $('#containerCauseMain .textDescriptionCause').each(function (index, ele) {

            if (!(ele.value !== '') || (ele.value.length <= 5)) {
                AddCauseP = false;
                $("#" + ele.id).siblings('span.error').css('visibility', 'visible');
                return false;
            }
            else {
                $("#" + ele.id).siblings('span.error').css('visibility', 'hidden');
            }
        });


        $('#containerCauseMain .causeImagenAdd').each(function (index, ele) {
            if ($("#" + ele.id + " tbody tr").length === 0) {
                $("#" + ele.id).siblings('span.error').css('visibility', 'visible');
                AddCauseP = false;
                return false;
            }
        });


        var AddSolutionP = true;

        $('#containerCauseMain .txtTitleSolution').each(function (index, ele) {
            if (!(ele.value !== '') || (ele.value.length <= 5)) {
                AddSolutionP = false;
                $("#" + ele.id).siblings('span.error').css('visibility', 'visible');
                return false;
            }
            else {
                $("#" + ele.id).siblings('span.error').css('visibility', 'hidden');
            }
        });

        $('#containerCauseMain .textDescriptionSolution').each(function (index, ele) {

            if (!(ele.value !== '') || (ele.value.length <= 5)) {
                AddSolutionP = false;
                $("#" + ele.id).siblings('span.error').css('visibility', 'visible');
                return false;
            }
            else {
                $("#" + ele.id).siblings('span.error').css('visibility', 'hidden');
            }
        });


        $('#containerCauseMain .solutionImagenAdd').each(function (index, ele) {
            if ($("#" + ele.id + " tbody tr").length === 0) {
                $("#" + ele.id).siblings('span.error').css('visibility', 'visible');
                AddSolutionP = false;
                return false;
            }
        });

        if (AddCauseP && AddSolutionP) {
            $('#containerCauseMain').each(function (index, ele) {
                $('span.error', ele).css('visibility', 'hidden');
            });
            return true;
        } else {
            return false;
        }
    }


    $('#txtTitleCause').change(function () {
        var defectoName = $("#DefectID option:selected").text() + ". ";
        var titleCause = $('#txtTitleCause').val();
        $("#titleAccotdionCause").html("Defecto:" + defectoName + "Causa:" + titleCause + ".");
    });

    function txtTitleCause_change(id, type) {
        $('#txtTitleCause' + id).click(function () {
            var defectoName = $("#DefectID option:selected").text() + ".";
            var titleCause = $('#txtTitleCause' + id).val();
            $("#titleAccotdionCause" + id).html("Defecto:" + defectoName + type + titleCause + ".");
        });
    }
});

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
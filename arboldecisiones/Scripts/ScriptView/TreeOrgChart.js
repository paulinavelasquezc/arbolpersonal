$(document).ready(function () {

    //validar  campo textDefinicion con ckeditor
    var editor = CKEDITOR.instances.textDefinicion;
    var locked;
    editor.on('key', function (evt) {

        var currentLength = editor.getData().length,
            maximumLength = 800;
        if (currentLength >= maximumLength) {
            if (!locked) {
                // Record the last legal content.
                editor.fire('saveSnapshot'), locked = 1;
                // Cancel the keystroke.
                evt.cancel();
            }
            else
                // Check after this key has effected.
                setTimeout(function () {
                    // Rollback the illegal one.  
                    if (editor.getData().length > maximumLength)
                        editor.execCommand('undo');
                    else
                        locked = 0;
                }, 0);
        }
    });
    
    //validar  campo textDescriptionCause con ckeditor
    var editorDescripCause = CKEDITOR.instances.textDescriptionCause;
    var locked;
    editorDescripCause.on('key', function (evt) {

        var currentLength = editorDescripCause.getData().length,
            maximumLength = 800;
        if (currentLength >= maximumLength) {
            if (!locked) {
                // Record the last legal content.
                editorDescripCause.fire('saveSnapshot'), locked = 1;
                // Cancel the keystroke.
                evt.cancel();
            }
            else
                // Check after this key has effected.
                setTimeout(function () {
                    // Rollback the illegal one.  
                    if (editorDescripCause.getData().length > maximumLength)
                        editorDescripCause.execCommand('undo');
                    else
                        locked = 0;
                }, 0);
        }
    });


    //addConfig button click event
    $('#addConfig').click(function () {
        //validation and addConfig order items
        var dcog = CKEDITOR.instances.campoeditor.getData();
        var isAllValid = true;

        var output = document.getElementById("listConfig");
        if (!(output.innerHTML !== '')) {
            isAllValid = false;
            $('#listConfig').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#listConfig').siblings('span.error').css('visibility', 'hidden');
        }


        if (isAllValid) {
       
            var dcog = CKEDITOR.instances.campoeditor.getData();

            var $newRow = $('#mainrowConfig').clone().removeAttr('id');          

            //Replace addConfig button with remove button
            $('#addConfig', $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');
        
            $('#editordiv', $newRow).remove();
            $('#descripcionConfig', $newRow).html(dcog);
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
            CKEDITOR.instances.campoeditor.setData('');
            document.getElementById("listConfig").innerHTML = "";
            $('#ConfigError').empty();
   
        }

    });

    //remove button click event
    $('#ConfigImagenAdd').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });



    $('#btnAddCause').click(function () {

        try {
            var continuar = true;

            var dats = CKEDITOR.instances.textDefinicion.getData();

            if (!(dats !== '') || (dats.length <= 3)) {
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
                var $chartContainer = $('#chart-container');
                var nodeVals = $("#DefectID option:selected").text();

                var datascource = {
                    'name': 'Defecto',
                    'title': nodeVals
                };
                //Guardar la tabla TreeConfiguration
                var TreeConfiData = new FormData();
                $('#ConfigImagenAdd tbody tr').each(function (index, ele) {

                    var MultimediaFileConfig = ($(".filesConfig", this))[0].files[0];
                    var typeFile = VaidateTypeFile(($(".filesConfig", this))[0].files[0]);
                    var DescriptionConfigHtml = $('.descripcionConfig', this).html();                                        
                    var DescriptionConfig = btoa(unescape(encodeURIComponent(DescriptionConfigHtml)));


                    TreeConfiData.append("MultimediaFile", MultimediaFileConfig);
                    TreeConfiData.append("typeFile", typeFile);
                    TreeConfiData.append("Description", DescriptionConfig);
                });                                   
                var fileContent = btoa(unescape(encodeURIComponent(dats)));                

                TreeConfiData.append("Definition", fileContent);
                TreeConfiData.append("CategoryID", $("#CategoryID option:selected").val());
                TreeConfiData.append("DefectID", $("#DefectID option:selected").val());
                $('#btnAddCause').prop("disabled", true);
                $.ajax({
                    url: '/TreeOrgChart/SaveTreeConfiguration',
                    type: "POST",
                    data: TreeConfiData,
                    processData: false,
                    contentType: false,
                    async: false,
                    beforeSend: function (objeto) {
                        $('#progressModalDefect').css({ display: 'block' });
                    },
                    complete: function () {
                        $('#progressModalDefect').css('display', 'none');
                        $('#btnAddCause').prop("disabled", false);
                    },
                    success: function (data) {
                        //if (data.save) {
                        if (data.status) {
                            $('#btnModalDefect').hide();
                            var idTreConf = data.treeConfigurationID;
                            if (!$chartContainer.children('.orgchart').length) {// if the original chart has been deleted
                                oc = $chartContainer.orgchart({
                                    'data': datascource,
                                    'nodeContent': 'title',
                                    'direction': 'l2r',
                                    //'chartClass': 'edit-state',
                                    //'exportButton': true,
                                    'exportFilename': 'Diagrama',
                                    'parentNodeSymbol': 'fa-th-large',
                                    'createNode': function ($node, data) {

                                        if (typeof data.id === "undefined") {
                                            $node[0].id = idTreConf;
                                        } else {
                                            $node[0].id = data.id;
                                        }
                                    }
                                });
                                oc.$chart.addClass('view-state');
                                $('#ModalDefect').modal('hide');

                                $('#btnUpdateDefect').show();
                                $('#btnAddDecisionsDefect').show();
                                $('#btnAddCause').hide();

                                oc.$chartContainer.on('click', '.node', function () {
                                    var $this = $(this);
                                    $('#selected-node').val($this.find('.title').text()).data('node', $this);

                                    var $node = $('#selected-node').data('node');
                                    if ($node[0].firstElementChild.innerText === 'Defecto') {
                                        $('#ModalDefect').modal('show');
                                        LoadInfoModalDefect($node[0].id);
                                    } else if ($node[0].firstElementChild.innerText === 'Unión') {
                                        $('#ModalDecisions').modal('show');
                                        LoadInfoModalDecisions($node[0].id, function () {
                                            $('#btnDecisions').hide();
                                            $('#btnAddJoinDecision').hide();
                                            $('#btnUpdateDecisions').show();
                                            $('#btnRemoveDecisions').show();
                                        });

                                    } else {
                                        $('#ModalDecisions').modal('show');
                                        LoadInfoModalDecisions($node[0].id, function () {
                                            $('#btnUpdateDecisions').show();
                                            $('#btnRemoveDecisions').show();
                                            $('#btnAddJoinDecision').show();
                                            $('#btnDecisions').hide();
                                            $('#btnAddDecisions').show();
                                        });
                                    }
                                });
                            }
                        }
                        else {
                            alert('Error servidor');
                        }
                        //} else {
                        //    alert('No tiene permitido ingresar mas de 25 defectos');
                        //}
                        $('#btnAddCause').prop("disabled", false);
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        $('#btnAddCause').prop("disabled", false);
                        console(xhr.status);
                        //alert(thrownError);
                    }
                });
            }
        }
        catch (e) {
            console(e);
        }
    });



    $('#btnDecisions').click(function () {
        var continuar = true;
       

        if (!($('#txtTitleCause').val().trim() !== '') || ($('#txtTitleCause').val().trim().length <= 3)) {
            continuar = false;
            $('#txtTitleCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#txtTitleCause').siblings('span.error').css('visibility', 'hidden');
        }

        var _descriptionCause = CKEDITOR.instances.textDescriptionCause.getData();

        if (!(_descriptionCause !== '') || (_descriptionCause.length <= 3)) {
            continuar = false;
            $('#textDescriptionCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#textDescriptionCause').siblings('span.error').css('visibility', 'hidden');
        }

        $('#causeError').text('');

        if ($('input[name=type]:checked').val() !== 'Unión') {
            if ($('#causeImagenAdd tbody tr').length === 0) {
                continuar = false;
                $('#causeError').text("Debe ingresar mínimo una Imagen.");
            }
        }

        if (continuar) {
            var $node = $('#selected-node').data('node');

            var TreeDecisCauseData = new FormData();            
            var txtTitleCause = $('#txtTitleCause').val();
            var textDescriptionCause = btoa(unescape(encodeURIComponent(_descriptionCause)));
            var ckbACtivoCause = $('.ckbACtivoCause').prop('checked');
            var type = $('input[name=type]:checked').val();

            var padre = 0;
            var treeConfigID = 0;
            if ($node[0].firstElementChild.innerText === 'Defecto') {
                sessionStorage.setItem('treeConfigID', $node[0].id);
                treeConfigID = $node[0].id;
            } else {
                padre = $node[0].id;
                treeConfigID = parseInt(sessionStorage.getItem('treeConfigID'));
            }

            TreeDecisCauseData.append("treeConfigID", treeConfigID);
            TreeDecisCauseData.append("treeID", padre);

            TreeDecisCauseData.append("txtTitle", txtTitleCause);
            TreeDecisCauseData.append("textDescription", textDescriptionCause);
            TreeDecisCauseData.append("ckbACtivo", ckbACtivoCause);
            TreeDecisCauseData.append("type", type);

            if ($('input[name=type]:checked').val() === 'Unión') {
                var randomColor = RandomColor();
                TreeDecisCauseData.append("colourHex", randomColor);
            } else {
                TreeDecisCauseData.append("colourHex", "");
            }



            $('.causeImagenAdd tbody tr').each(function (index, ele) {
               
                var MultimediaFileCause = ($('.filesCause', this))[0].files[0];
                var typeFile = VaidateTypeFile(($(".filesCause", this))[0].files[0]);
                var DescriptionCuseHTML = $('.descripcionCause', this).html();
                var DescriptionCuse = btoa(unescape(encodeURIComponent(DescriptionCuseHTML)));
                
                TreeDecisCauseData.append("MultimediaFile", MultimediaFileCause);
                TreeDecisCauseData.append("typeFile", typeFile);
                TreeDecisCauseData.append("Description", DescriptionCuse);

            });
            $('#btnDecisions').prop("disabled", true);
            $.ajax({
                url: '/TreeOrgChart/SaveTreeDecision',
                type: "POST",
                data: TreeDecisCauseData,
                processData: false,
                contentType: false,
                async: false,
                beforeSend: function (objeto) {
                    $('#progressModalDecisions').css({ display: 'block' });
                },
                complete: function () {
                    $('#progressModalDecisions').css('display', 'none');
                    $('#btnDecisions').prop("disabled", false);
                },
                success: function (dataCause) {
                    if (dataCause.status) {

                        var $chartContainer = $('#chart-container');
                        var nodeVals = [];
                        nodeVals.push($('#txtTitleCause').val());

                        var hasChild = $node.parent().attr('colspan') > 0 ? true : false;
                        if (!hasChild) {
                            //var rel = nodeVals.length > 1 ? '110' : '100';
                            var rel = '100';
                            oc.addChildren($node, nodeVals.map(function (item) {
                                return { 'title': item, 'name': type, 'relationship': rel, 'id': dataCause.treeDecisionID };
                            }));
                        } else {
                            oc.addSiblings($node.closest('tr').siblings('.nodes').find('.node:first'), nodeVals.map(function (item) {
                                return { 'title': item, 'name': type, 'relationship': '110', 'id': dataCause.treeDecisionID };
                            }));
                        }

                        if ($('input[name=type]:checked').val() === 'Unión') {
                            $('#' + dataCause.treeDecisionID + ' .title').css("background-color", randomColor);
                            $('#' + dataCause.treeDecisionID + ' .content').css("border-color", randomColor);
                        }

                        $('#ModalDecisions').modal('hide');

                    } else {
                        alert('Cause');
                    }
                    $('#btnDecisions').prop("disabled", false);
                }, error: function (xhr, ajaxOptions, thrownError) {
                    $('#btnDecisions').prop("disabled", false);
                    alert(xhr.status);
                    alert(thrownError);
                } // fin success
            }); // fin ajax SaveTreeDecision               
        }

    });


    //addCause button click event
    $('#addCause').click(function () {
        //validation and addCause order items
        var isAllValid = true;
        var _descripCause = CKEDITOR.instances.campEditDescripCause.getData();

        var output = document.getElementById("listCause");
        if (!(output.innerHTML !== '')) {
            isAllValid = false;
            $('#listCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#listCause').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrowCause').clone().removeAttr('id');

            //Replace addCause button with remove button
            $('#addCause', $newRow).addClass('remove').val('Eliminar').removeClass('btn-success').addClass('btn-danger');

            $('#editordivDescripCause', $newRow).remove();	 
            $('#descripcionCause', $newRow).html(_descripCause);
            
            //remove id attribute from new clone row
            $('#descripcionCause,#listCause,#addCause,#filesCause', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            $('#fileSpanCause', $newRow).addClass('disabled');
            $(".filesCause", $newRow).prop('disabled', true);
            //append clone row
            $('#causeImagenAdd tbody').append($newRow);

            //clear select data            
            $('#descripcionCause').val('');
            CKEDITOR.instances.campEditDescripCause.setData('');
            document.getElementById("listCause").innerHTML = "";
            $('#causeError').empty();
        }

    });

    //remove button click event
    $('#causeImagenAdd').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    function LoadInfoModalDefect(_treeConfigID) {
        sessionStorage.setItem('TreeConfigurationID', _treeConfigID);
        var _id = parseInt(_treeConfigID);

        $.ajax({
            url: '/TreeOrgChart/QueryTreeConfiguration',
            type: "POST",
            data: '{_treeConfigurationID: "' + _id + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status) {
                    $('#DefectID').val(data.treConfig.DefectID);
                    $('#CategoryID').val(data.treConfig.CategoryID);
                    CKEDITOR.instances.textDefinicion.setData(data.treConfig.Definition);                    
                    //$('#textDefinicion').val(data.treConfig.Definition);
                    $('#ConfigImagenAdd tbody tr').closest("tr").remove();
                    ShowMultimediaConfig(data.treeMultimedia);

                } else {
                    alert('Cause');
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            } // fin success
        }); // fin ajax SaveTreeDecision            
    }

    function ShowMultimediaConfig(data) {

        for (var i = 0; i < data.length; i++) {
            var multimedia = "";
            var url = data[i].Url.replace('~', '');
            if (data[i].Type === "Image") {
                multimedia = ['<img class="MeasuredImage" src="' + url + '" />'].join('');
            } else {
                multimedia = ['<video class="MeasuredVideo"controls><source src="' + url + '">.</video>'].join('');
            }

            var code = '<tr class="mycontainer">';
            code += '   <td width="80px">';
            code += '       <span class="btn btn-primary btn-file glyphicon glyphicon-search" disabled>';
            code += '       <input type="text" class="idMultimedia"  value="' + data[i].TreeMultimediaID + '" hidden>';
            code += '       </span>';
            code += '   </td>';
            code += '   <td width="128px">';
            code += '       <output class="listConfig">' + multimedia + '</output>';
            code += '       <span class="error">Debe Seleccionar una Imagen</span>';
            code += '   </td>';
            code += '   <td width="700px">';
            code += '             <div  id="descripcionConfig" name="descripcionConfig" class="descripcionConfig scrollDescripcionHtml">' + data[i].Description;            
            code += '             </div>';
            code += '   </td>';
            code += '   <td width="80px">';
            code += '       <input type="button" value="Eliminar" style="width:80px" class="btn remove btn-danger" />';
            code += '   </td>';
            code += '</tr>';
            $('#ConfigImagenAdd tbody').append(code);
        }
    }

    $('#btnUpdateDefect').click(function () {
        var continuar = true;

        if (!($('#DefectID').val().trim() !== '')) {
            continuar = false;
            $('#DefectID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#DefectID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#CategoryID').val().trim() !== '')) {
            continuar = false;
            $('#CategoryID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#CategoryID').siblings('span.error').css('visibility', 'hidden');
        }


        var dats = CKEDITOR.instances.textDefinicion.getData();

        if (!(dats !== '') || (dats.length <= 3)) {
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
            //Actualizar la tabla TreeConfiguration
            var TreeConfiData = new FormData();
            $('#ConfigImagenAdd tbody tr').each(function (index, ele) {

                var MultimediaFileConfig = null;
                var typeFile = null;
                var id = 0;
                if ($(".filesConfig", this).length > 0) {
                    MultimediaFileConfig = ($(".filesConfig", this))[0].files[0];
                    typeFile = VaidateTypeFile(($(".filesConfig", this))[0].files[0]);
                } else {
                    id = $(".idMultimedia", this).val();
                    var file = new File(['gestiondeclientes'],
                        'sinImage.jpg',
                        { type: 'image/jpg' });
                    MultimediaFileConfig = file;
                }

                var DescriptionConfigHtml = $('.descripcionConfig', this).html();
                var DescriptionConfig = btoa(unescape(encodeURIComponent(DescriptionConfigHtml)));
                
                TreeConfiData.append("MultimediaFile", MultimediaFileConfig);
                TreeConfiData.append("typeFile", typeFile);
                TreeConfiData.append("Description", DescriptionConfig);
                TreeConfiData.append("idMultimedia", parseInt(id));
            });

            var fileContent = btoa(unescape(encodeURIComponent(dats)));
            TreeConfiData.append("Definition", fileContent);
            TreeConfiData.append("CategoryID", $("#CategoryID option:selected").val());
            TreeConfiData.append("DefectID", $("#DefectID option:selected").val());
            TreeConfiData.append("TreeConfigurationID", parseInt(sessionStorage.getItem('TreeConfigurationID')));

            $.ajax({
                url: '/TreeOrgChart/UpdateTreeConfiguration',
                type: "POST",
                data: TreeConfiData,
                processData: false,
                contentType: false,
                async: false,
                beforeSend: function (objeto) {
                    $('#progressModalDefect').css({ display: 'block' });
                },
                complete: function () {
                    $('#progressModalDefect').css('display', 'none');
                },
                success: function (data) {
                    if (data.status) {
                        $('#ModalDefect').modal('hide');
                        var id = parseInt(sessionStorage.getItem('TreeConfigurationID'));
                        var val = $("#DefectID option:selected").text();
                        $("#" + id + " .content").text(val);
                    }
                    else {
                        alert('Error');
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    console(xhr.status);
                    //alert(thrownError);
                }
            });
        }
    });

    $('#btnAddDecisionsDefect').click(function () {
        NewDecisions();
    });

    function CleanFieldsDecisions() {
        $('#txtTitleCause').val("");
        CKEDITOR.instances.textDescriptionCause.setData('');
        //$('#textDescriptionCause').val("");
        $('#causeImagenAdd tbody tr').closest("tr").remove();
        $('#causeError').text('');
        $('#txtTitleCause').siblings('span.error').css('visibility', 'hidden');
        $('#textDescriptionCause').siblings('span.error').css('visibility', 'hidden');
        $('#listCause').siblings('span.error').css('visibility', 'hidden');
    }

    function LoadInfoModalDecisions(_treeDecisionID, callback) {
        sessionStorage.setItem('TreeDecisionID', _treeDecisionID);
        var _id = parseInt(_treeDecisionID);

        $.ajax({
            url: '/TreeOrgChart/QueryTreeDecision',
            type: "POST",
            data: '{_treeDecisionID: "' + _id + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status) {


                    if (data.treeDecision.JoinFatherID === 0) {
                        $('#btnAddDecisions').show();
                        $('#btnRemoveJoin').hide();
                    } else {
                        $('#btnAddDecisions').hide();
                        $('#btnRemoveJoin').show();
                    }
                    sessionStorage.setItem('JoinFatherID', data.treeDecision.JoinFatherID);

                    $('#txtTitleCause').val(data.treeDecision.Name);
                    CKEDITOR.instances.textDescriptionCause.setData(data.treeDecision.Description);
                    //$('#textDescriptionCause').val(data.treeDecision.Description);

                    if (data.treeDecision.TypeID === 1) {
                        document.getElementById("Causa").checked = true;
                    } else if (data.treeDecision.TypeID === 2) {
                        document.getElementById("Solucion").checked = true;
                    } else if (data.treeDecision.TypeID === 3) {
                        document.getElementById("Decision").checked = true;
                    } else if (data.treeDecision.TypeID === 4) {
                        document.getElementById("Union").checked = true;
                    }


                    if (data.treeDecision.Active) {
                        document.getElementById("ckbACtivoCause").checked = true;
                    } else {
                        document.getElementById("ckbACtivoCause").checked = false;
                    }

                    $('#causeImagenAdd tbody tr').closest("tr").remove();

                    ShowMultimediaDecision(data.treeMultimedia);
                    callback();

                } else {
                    alert('Cause');
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            } // fin success
        }); // fin ajax SaveTreeDecision            
    }

    function ShowMultimediaDecision(data) {

        for (var i = 0; i < data.length; i++) {
            var multimedia = "";
            var url = data[i].Url.replace('~', '');
            if (data[i].Type === "Image") {
                multimedia = ['<img class="MeasuredImage" src="' + url + '" />'].join('');
            } else {
                multimedia = ['<video class="MeasuredVideo"controls><source src="' + url + '">.</video>'].join('');
            }

            var code = '<tr class="mycontainer">';
            code += '   <td width="80px">';
            code += '       <span class="btn btn-primary btn-file glyphicon glyphicon-search" disabled>';
            code += '       <input type="text" class="idMultimedia"  value="' + data[i].TreeMultimediaID + '" hidden>';
            code += '       </span>';
            code += '   </td>';
            code += '   <td width="128px">';
            code += '       <output class="listCause">' + multimedia + '</output>';
            code += '       <span class="error">Debe Seleccionar una Imagen</span>';
            code += '   </td>';
            code += '   <td width="700px">';
            code += '   <div  id="descripcionCause" name="descripcionCause" class="descripcionCause scrollDescripcionHtml">' + data[i].Description;
            code += '   </div>';
            code += '   </td>';
            code += '   <td width="80px">';
            code += '       <input type="button" value="Eliminar" style="width:80px" class="btn remove btn-danger" />';
            code += '   </td>';
            code += '</tr>';
            $('#causeImagenAdd tbody').append(code);
        }
    }


    $('#btnUpdateDecisions').click(function () {
        var continuar = true;

        if (!($('#txtTitleCause').val().trim() !== '') || ($('#txtTitleCause').val().trim().length <= 3)) {
            continuar = false;
            $('#txtTitleCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#txtTitleCause').siblings('span.error').css('visibility', 'hidden');
        }

        var _descriptioneditCause = CKEDITOR.instances.textDescriptionCause.getData();
        if (!(_descriptioneditCause !== '') || (_descriptioneditCause.length <= 3)) {
            continuar = false;
            $('#textDescriptionCause').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#textDescriptionCause').siblings('span.error').css('visibility', 'hidden');
        }

        $('#causeError').text('');

        if ($('input[name=type]:checked').val() !== 'Unión') {
            if ($('#causeImagenAdd tbody tr').length === 0) {
                continuar = false;
                $('#causeError').text("Debe ingresar mínimo una Imagen.");
            }
        }

        if (continuar) {
            
            var _textDescriptionCause = btoa(unescape(encodeURIComponent(_descriptioneditCause)));     
            var TreeDecisCauseData = new FormData();

            var txtTitleCause = $('#txtTitleCause').val();
            var textDescriptionCause = _textDescriptionCause;
            var ckbACtivoCause = $('.ckbACtivoCause').prop('checked');
            var type = $('input[name=type]:checked').val();


            TreeDecisCauseData.append("treeDecisionID", parseInt(sessionStorage.getItem('TreeDecisionID')));
            //TreeDecisCauseData.append("treeID", padre);

            TreeDecisCauseData.append("txtTitle", txtTitleCause);
            TreeDecisCauseData.append("textDescription", textDescriptionCause);
            TreeDecisCauseData.append("ckbACtivo", ckbACtivoCause);
            TreeDecisCauseData.append("type", type);

            if ($('input[name=type]:checked').val() !== 'Unión') {
                TreeDecisCauseData.append("colourHex", "");
            } else {
                var randomColor = RandomColor();
                TreeDecisCauseData.append("colourHex", randomColor);
            }


            $('#causeImagenAdd tbody tr').each(function (index, ele) {

                var MultimediaFileConfig = null;
                var typeFile = null;
                var id = 0;
                if ($(".filesCause", this).length > 0) {
                    MultimediaFileConfig = ($(".filesCause", this))[0].files[0];
                    typeFile = VaidateTypeFile(($(".filesCause", this))[0].files[0]);
                } else {
                    id = $(".idMultimedia", this).val();
                    var file = new File(['gestiondeclientes'],
                        'sinImage.jpg',
                        { type: 'image/jpg' });
                    MultimediaFileConfig = file;
                }

                var DescripcionCauseHtml = $('.descripcionCause', this).html();
                var DescripcionCause = btoa(unescape(encodeURIComponent(DescripcionCauseHtml)));                

                TreeDecisCauseData.append("MultimediaFile", MultimediaFileConfig);
                TreeDecisCauseData.append("typeFile", typeFile);
                TreeDecisCauseData.append("Description", DescripcionCause);
                TreeDecisCauseData.append("idMultimedia", parseInt(id));
            });

            $.ajax({
                url: '/TreeOrgChart/UpdateTreeDecision',
                type: "POST",
                data: TreeDecisCauseData,
                processData: false,
                contentType: false,
                async: false,
                beforeSend: function (objeto) {
                    $('#progressModalDecisions').css({ display: 'block' });
                },
                complete: function () {
                    $('#progressModalDecisions').css('display', 'none');
                },
                success: function (data) {
                    if (data.status) {

                        $('#ModalDecisions').modal('hide');
                        var id = parseInt(sessionStorage.getItem('TreeDecisionID'));
                        var val = $('#txtTitleCause').val();
                        $("#" + id + " .content").text(val);

                        if (!data.father) {
                            $("#" + id + " .title").text(type);
                            if ($('input[name=type]:checked').val() !== 'Unión') {
                                $('#' + id + ' .title').removeAttr("style");
                                $('#' + id + ' .content').removeAttr("style");
                            } else {
                                $('#' + id + ' .title').css("background-color", data.colourHex);
                                $('#' + id + ' .content').css("border-color", data.colourHex);
                            }
                        } else {
                            alert("Esta decisión no se puede cambiar el tipo(Causa, Solución,Decisión), por que esta unida con otro decisión");
                        }
                    }
                    else {
                        alert('Error');
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    console(xhr.status);
                    //alert(thrownError);
                }
            });
        }
    });

    $('#btnRemoveDecisions').click(function () {

        var _treeConfigurationID = parseInt(sessionStorage.getItem('TreeConfigurationID'));
        var _treeDecisionID = parseInt(sessionStorage.getItem('TreeDecisionID'));
        $.ajax({
            url: '/TreeOrgChart/DeleteTreeDecision',
            type: "POST",
            data: '{_treeDecisionID: "' + _treeDecisionID + '", _treeConfigurationID: "' + _treeConfigurationID + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status) {

                    if (data.remove === 1) {
                        $('#ModalDecisions').modal('hide');
                        var $node = $('#selected-node').data('node');
                        oc.removeNodes($node);
                        $('#selected-node').val('').data('node', null);
                    } else if (data.remove === 2) {
                        alert("No se puede eliminar. Tiene otras asociaciones.");
                    } else {
                        alert("No se puede eliminar. Tiene registros asociados.");
                    }

                } else {
                    alert('Error servidor');
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            } // fin success
        }); // fin ajax SaveTreeDecision    
    });

    $('#btnAddDecisions').click(function () {
        NewDecisions();
    });

    $('#btnAddJoinDecision').click(function () {
        $('#infoShowDecision').hide();
        JoinDecisions();
    });


    function NewDecisions() {
        $('#ModalDefect').modal('hide');
        $('#lblTypeShow')[0].innerHTML = "Causa";
        CleanFieldsDecisions();
        $('#ModalDecisions').modal('show');
        $('#btnUpdateDecisions').hide();
        $('#btnRemoveDecisions').hide();
        $('#btnAddDecisions').hide();
        $('#btnAddJoinDecision').hide();
        $('#btnDecisions').show();
    }

    function JoinDecisions() {
        var _id = parseInt(sessionStorage.getItem('TreeConfigurationID'));
        $.ajax({
            url: '/TreeOrgChart/QueryDecisionsXDefect',
            type: "POST",
            data: '{_treeConfigurationID: "' + _id + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status) {

                    $('#cbxDecisions').find('option:not(:first)').remove();


                    if (parseInt(sessionStorage.getItem('JoinFatherID')) === 0) {
                        $.each(data.treeDecision, function (key, registro) {
                            $("#cbxDecisions").append('<option value=' + registro.TreeDecisionID + '>' + registro.Name + '</option>');
                        });
                    } else {
                        $.each(data.treeDecision, function (key, registro) {
                            if (parseInt(sessionStorage.getItem('JoinFatherID')) === registro.TreeDecisionID) {
                                $("#cbxDecisions").append('<option value=' + registro.TreeDecisionID + ' selected>' + registro.Name + '</option>');
                            } else {
                                $("#cbxDecisions").append('<option value=' + registro.TreeDecisionID + '>' + registro.Name + '</option>');
                            }
                        });
                        $('#cbxDecisions').trigger("change");
                    }


                    $('#ModalDecisions').modal('hide');
                    $('#ModalJoin').modal('show');
                } else {
                    alert('Consultar Decisiones');
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            } // fin success
        }); // fin ajax SaveTreeDecision    
    }

    $('#cbxDecisions').change(function () {
        var _id = $("#cbxDecisions option:selected").val();

        if (_id !== "0") {
            $.ajax({
                url: '/TreeOrgChart/QueryTreeDecision',
                type: "POST",
                data: '{_treeDecisionID: "' + _id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.status) {
                        $('#txtTitleShow').val(data.treeDecision.Name);
                        $('#textDescriptionShow').html(data.treeDecision.Description);
                        //$('#textDescriptionShow').val(data.treeDecision.Description
                        if (data.treeDecision.TypeID === 1) {
                            $('#lblTypeShow')[0].innerHTML = "Causa";
                        } else if (data.treeDecision.TypeID === 2) {
                            $('#lblTypeShow')[0].innerHTML = "Solucion";
                        }
                        else if (data.treeDecision.TypeID === 4) {
                            $('#lblTypeShow')[0].innerHTML = "Unión";
                        }

                        $('#infoShowDecision').show();
                    } else {
                        alert('Cause');
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                } // fin success
            });
        } else {
            $('#infoShowDecision').hide();
        }
    });


    $('#btnGuardarJoin').click(function () {
        var _id = parseInt($("#cbxDecisions option:selected").val());

        if (_id !== 0) {
            $('#cbxDecisions').siblings('span.error').css('visibility', 'hidden');
            var _TreeDecisionID = parseInt(sessionStorage.getItem('TreeDecisionID'));

            $.ajax({
                url: '/TreeOrgChart/UpdateJoinDecision',
                type: "POST",
                data: '{_TreeDecisionID: "' + _TreeDecisionID + '", _Father: "' + _id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.status) {
                        $('#' + _TreeDecisionID + ' .title').css("background-color", data.colourHex);
                        $('#' + _TreeDecisionID + ' .content').css("border-color", data.colourHex);
                        $('#ModalJoin').modal('hide');
                    }
                    else {
                        alert('Error');
                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    console(xhr.status);
                    //alert(thrownError);
                }
            });

        } else {
            $('#cbxDecisions').siblings('span.error').css('visibility', 'visible');
        }
    });

    $('#btnRemoveJoin').click(function () {
        var _id = parseInt($("#cbxDecisions option:selected").val());

        var _TreeDecisionID = parseInt(sessionStorage.getItem('TreeDecisionID'));
        var _JoinFatherID = parseInt(sessionStorage.getItem('JoinFatherID'));
        $.ajax({
            url: '/TreeOrgChart/UpdateJoinDecision',
            type: "POST",
            data: '{_TreeDecisionID: "' + _TreeDecisionID + '", _Father: "' + 0 + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status) {
                    $('#' + _TreeDecisionID + ' .title').removeAttr("style");
                    $('#' + _TreeDecisionID + ' .content').removeAttr("style");
                    $('#ModalJoin').modal('hide');
                }
                else {
                    alert('Error');
                }
            }, error: function (xhr, ajaxOptions, thrownError) {
                console(xhr.status);
                //alert(thrownError);
            }
        });
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

function RandomColor() {
    hexadecimal = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F")
    color_aleatorio = "#";
    for (i = 0; i < 6; i++) {
        posarray = aleatorio(0, hexadecimal.length);
        color_aleatorio += hexadecimal[posarray];
    }
    return color_aleatorio;
}

function aleatorio(inferior, superior) {
    numPosibilidades = superior - inferior;
    aleat = Math.random() * numPosibilidades;
    aleat = Math.floor(aleat);
    return parseInt(inferior) + aleat;
}

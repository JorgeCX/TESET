$(document).ready(function () {

    var ajaxRequestRoles = $.ajax({
        type: "GET",
        url: appBaseUrl + "api/Manteniemto/getRoles",
        contentType: false,
        processData: false,
        contentType: "application/json;chartset=utf-8",
    });
    ajaxRequestRoles.done(function (done) {
        $("#IDPerfil").empty();
        $("#APerfil").append("<option></option>")
        $.each(done, function (clave, dato) {
            $("#IDPerfil").append("<option value=\"" + dato.Value + "\">" + dato.Text + "</option>");
            $("#APerfil").append("<option value=\"" + dato.Value + "\">" + dato.Text + "</option>");
        });
    });
    //Cargar Combobox de Roles:
    var TableUsuario = $('#TableUsuario').DataTable({
        "bFilter": true,
        "scrollX": true,
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" },
        "sScrollXInner": "100%",
        "sAjaxDataProp": "",
        "aoColumns": [
        {
            "targets": -1,
            "orderable": false,
            "data": null,
            "defaultContent": "<div class='btn-group'> <button id='BtUModif' style='font-size:12px' class='btn btn-default'><span class='glyphicon glyphicon-pencil'></span></button><button id='btnEliminarUser' style='font-size:12px' class='btn btn-danger'><span class='glyphicon glyphicon-remove'></span></button></div> "
        },
        { "mData": "UserName" },
        { "mData": "Email" },
        { "mData": "PhoneNumber" },
        { "mData": "Perfil" },
        {
            "mData": "LockoutEnabled",
            render: function (data, type, row) {
                return "<input type='checkbox' id='CheSatus'> <label id='PCheack' />"
            }
        }],
        "sAjaxSource": appBaseUrl + "api/Manteniemto/getUsuarios",
        rowCallback: function (row, data) {

            var p = $('#PCheack', row);
            //p[0].innerHTML = data.LockoutEnabled;
            if (data.LockoutEnabled) {
                p[0].innerHTML = "Activado";
            }
            else {
                p[0].innerHTML = "Desactivado";
            }
            $('#CheSatus', row).prop('checked', data.LockoutEnabled == true);

        }
    });

    var btnAltaUser = $('#btnAltaUser');
    var btnGuardarRe = $('#btnGuardarRe');
    var btnGuardarCamRe = $('#btnGuardarCamRe');
    var btnContinuar = $('#btnContinuar');
    var btnRefresT = $('#btnRefresT');
    var idUserName = $('#idUserName');
    var idEmail = $('#idEmail');
    var idtelefono = $('#idtelefono');
    var IDPerfil = $('#IDPerfil');
    var GDatos = null;
    $('#TableUsuario tbody').on('click', '#btnEliminarUser', function () {
        var tr = $(this).closest('tr');
        var datos = TableUsuario.row(tr).data();
        GDatos = datos;
        $('#modalDel').modal("show");
    });

    $("#btnDelete").click(function () {        
        $.ajax({
            type: "Delete",
            url: appBaseUrl + "api/Manteniemto/DeleteUser",
            data: JSON.stringify(GDatos),
            contentType: "application/json;chartset=utf-8",
            statusCode: {
                200: function (done) {
                    TableUsuario.ajax.reload(function () {

                        var message = ['bottom-right', 'success', done]
                        $('.' + message[0]).notify({
                            message: { text: message[2] },
                            type: message[1],
                            fadeOut: {
                                delay: 10000
                            }
                        }).show();
                    });
                },
                404: function (done) {

                    var message = ['bottom-right', 'warning', 'No se puedo hacer su opercion']
                    $('.' + message[0]).notify({
                        message: { text: message[2] },
                        type: message[1],
                        fadeOut: {
                            delay: 1000
                        }
                    }).show();
                    $('#UserAdd').modal("show");
                },
                400: function (done) {

                    var message = ['bottom-right', 'warning', 'No se puedo hacer su opercion']
                    $('.' + message[0]).notify({
                        message: { text: message[2] },
                        type: message[1],
                        fadeOut: {
                            delay: 1000
                        }
                    }).show();
                    $('#UserAdd').modal("show");
                }
            }
        }   );
    });
    $('#TableUsuario tbody').on('click', '#BtElimin', function () {
        var tr = $(this).closest('tr');
        var row = TableUsuario.row(tr);
    });
    $('#TableUsuario tbody').on('click', '#BtUModif', function () {
        var tr = $(this).closest('tr');
        var datos = TableUsuario.row(tr).data();
        $('#RepoModUser').modal("show");
        idUserName.val(datos.UserName)
        idEmail.val(datos.Email)
        idtelefono.val(datos.PhoneNumber)
        IDPerfil.val(datos.Perfil)
        GDatos = datos;

    });

    btnAltaUser.click(function () {
        $("#FormAlta")[0].reset();
        $('#UserAdd').modal("show");
    })
    btnGuardarCamRe.click(function () {
        $('#FormUpdate').submit();
    })
    $('#FormAlta').on('submit', function (e) {
        if (e.result) {
            $('#UserAdd').modal("hide");
           
            var NewUsuario = new Object();
            NewUsuario.UserName = $("#AUserName").val();
            NewUsuario.Email = $("#AEmail").val();
            NewUsuario.Telefono = $("#Atelefono").val();
            NewUsuario.Perfil = $("#APerfil").val();
            NewUsuario.Password = $("#AContra1").val();
            NewUsuario.ConfirmPassword = $("#AContra2").val();




            $.ajax({
                type: "Post",
                url: appBaseUrl + "api/Manteniemto/AltaUser",
                data: JSON.stringify(NewUsuario),
                contentType: "application/json;chartset=utf-8",
                statusCode: {
                    200: function (done) {
                        TableUsuario.ajax.reload(function () {
                            
                            var message = ['bottom-right', 'success', done]
                            $('.' + message[0]).notify({
                                message: { text: message[2] },
                                type: message[1],
                                fadeOut: {
                                    delay: 10000
                                }
                            }).show();
                        });
                    },
                    404: function (done) {
                        
                        var message = ['bottom-right', 'warning', 'No se puedo hacer su opercion']
                        $('.' + message[0]).notify({
                            message: { text: message[2] },
                            type: message[1],
                            fadeOut: {
                                delay: 1000
                            }
                        }).show();
                        $('#UserAdd').modal("show");
                    },
                    400: function (done) {
                        
                        var message = ['bottom-right', 'warning', 'No se puedo hacer su opercion']
                        $('.' + message[0]).notify({
                            message: { text: message[2] },
                            type: message[1],
                            fadeOut: {
                                delay: 1000
                            }
                        }).show();
                        $('#UserAdd').modal("show");
                    }
                }
            });
        }
        else {
            return false;
        }
    });
    $('#FormUpdate').on('submit', function (e) {
        if (e.result) {
            $('#static').modal("show");
        }
        else {
            return false;
        }
    });
    $('#btnGAtlaUser').click(function () {
        $('#FormAlta').submit();

    });


    $('#btnContinuar2').on('click', function () {
       
        $.ajax({
            type: "PUT",
            url: appBaseUrl + "api/Manteniemto/modStatusUser",
            data: JSON.stringify(GDatos),
            contentType: "application/json;chartset=utf-8",
            statusCode: {
                200: function (done) {
                    TableUsuario.ajax.reload(function () {
                        
                        var message = ['bottom-right', 'success', done]
                        $('.' + message[0]).notify({
                            message: { text: message[2] },
                            type: message[1],
                            fadeOut: {
                                delay: 10000
                            }
                        }).show();
                    });
                },
                404: function (done) {
                    
                    var message = ['bottom-right', 'warning', 'No se puedo hacer su opercion']
                    $('.' + message[0]).notify({
                        message: { text: message[2] },
                        type: message[1],
                        fadeOut: {
                            delay: 1000
                        }
                    }).show();
                },
                400: function (done) {
                    
                    var message = ['bottom-right', 'warning', 'No se puedo hacer su opercion']
                    $('.' + message[0]).notify({
                        message: { text: message[2] },
                        type: message[1],
                        fadeOut: {
                            delay: 1000
                        }
                    }).show();
                }
            }
        });
    })
    btnRefresT.click(function () {
        TableUsuario.columns.adjust().draw();
        TableUsuario.ajax.reload(function () {
            TableUsuario.columns.adjust().draw();
        });
    })
    btnContinuar.click(function () {
       
        $('#RepoModUser').modal("hide");

        var Usuario = new Object();
        Usuario.id = GDatos.id;
        Usuario.LockoutEnabled = GDatos.LockoutEnabled;
        Usuario.Email = idEmail.val();
        Usuario.PhoneNumber = idtelefono.val();
        Usuario.UserName = idUserName.val();
        Usuario.Perfil = IDPerfil.val();

        var ajaxRequest = $.ajax({
            type: "POST",
            timeout: 20000,
            url: appBaseUrl + "api/Manteniemto/modUser",
            contentType: false,
            processData: false,
            contentType: "application/json;chartset=utf-8",
            data: JSON.stringify(Usuario),
        });
        ajaxRequest.done(function (done) {
            TableUsuario.ajax.reload(function () {
                
                var message = ['bottom-right', 'success', done]
                $('.' + message[0]).notify({
                    message: { text: message[2] },
                    type: message[1],
                    fadeOut: {
                        delay: 1000
                    }
                }).show();
            });
        });
        ajaxRequest.error(function (error) {
            
            var message = ['bottom-right', 'warning', 'Error al Modificar Usuario']
            $('.' + message[0]).notify({
                message: { text: message[2] },
                type: message[1],
                fadeOut: {
                    delay: 1000
                }
            }).show();
            $('#RepoModUser').modal("show");
            idUserName.focus();
            return false;
            //alert("Error al Subir los Archivos!");
        });
    })
    $('#TableUsuario tbody').on('click', '#CheSatus', function () {
        var data = TableUsuario.row($(this).parents('tr')).data();
        $('#Ptext2').text("Esta Seguro que desea Desactivar ó Activar al Usuario: " + data.UserName);
        $('#static2').modal("show");
        GDatos = null;
        GDatos = data;

    });
});
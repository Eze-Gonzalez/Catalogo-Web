﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="CatalogoWeb.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Notificacion tamaño máximo --%>
    <div id="modal" class="modal-completo">
        <div class="m-content text-light text-center">
            <div class="card-header">
                <h5 class="modal-title">Tamaño Excedido</h5>
                <span class="close" id="cerrar">&times;</span>
            </div>
            <div class="card-body">
                <div class="swal-icon swal-icon--error">
                    <div class="swal-icon--error__x-mark">
                        <span class="swal-icon--error__line swal-icon--error__line--left"></span>
                        <span class="swal-icon--error__line swal-icon--error__line--right"></span>
                    </div>
                </div>
                <p class="card-text">El archivo es demasiado grande. El tamaño máximo permitido es de 2 MB.</p>
            </div>
            <div class="card-footer mt-4">
                <button type="button" id="btnCerrarM" class="btn btn-primary btn-outline-light w-120 c">Aceptar</button>
            </div>
        </div>
    </div>
    <%-- Fin notificacion tamaño --%>
    <div class="bg-black bg-opacity-50 center-col">
        <%-- Formato datos usuario --%>
        <div class="col mt-4">
            <%-- Bienvenida --%>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row mb-5">
                        <div class="mb-2">
                            <h3 class="text-light center-row">Bienvenido
                        <asp:Label ID="lblUsuario" runat="server" CssClass="ms-2" Text="Label"></asp:Label>
                            </h3>
                        </div>
                        <div>
                            <p class="center-row text-light">Aquí podrá editar los datos de su cuenta.</p>
                        </div>
                        <asp:Label ID="lblSinCambios" CssClass="h3 text-light center-row" runat="server" Text="⚠️AVISO: No se detectaron cambios a realizar⚠️" Visible="false"></asp:Label>
                    </div>
                    <%-- Fin bienvenida --%>

                    <%-- Datos "principales" --%>
                    <div class="row text-light">
                        <div class="col-6">
                            <div class="mb-3">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" onchange="CheckTextChanged()"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblApellido" runat="server" Text="Apellido:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-5">
                                <label class="form-label">Imagen de perfil</label>
                                <p>¿Cómo desea subir su imagen de perfil?</p>
                                <div class="mb-2 center-row">
                                    <div class="divToggleButton me-2 align-center">
                                        <asp:RadioButton ID="rdbUrl" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                                        <asp:Label ID="lblToggleButton"
                                            AssociatedControlID="rdbUrl" runat="server"
                                            ToolTip="Cambiar imagen mediante un enlace de internet" />
                                    </div>
                                    <asp:Label ID="lblUrl" runat="server" Text="Mediante URL (enlace de internet)" CssClass="me-2"></asp:Label>
                                    <div class="divToggleButton me-2 align-center">
                                        <asp:RadioButton ID="rdbLocal" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                                        <asp:Label ID="lblToggleButtonLocal"
                                            AssociatedControlID="rdbLocal" runat="server"
                                            ToolTip="Cambiar imagen desde su dispositivo" />
                                    </div>
                                    <asp:Label ID="lblLocal" runat="server" Text="Subir una imagen desde su dispositivo"></asp:Label>
                                </div>
                                <%if (rdbUrl.Checked)
                                    {%>
                                <asp:TextBox ID="txtImagenUrl" CssClass="form-control" runat="server" placeholder="Ingrese un enlace de internet" OnTextChanged="txtNombre_TextChanged"></asp:TextBox>
                                <%}
                                    else if (rdbLocal.Checked)
                                    {  %>
                                <asp:FileUpload ID="fileLocal" runat="server" CssClass="form-control" onchange="ValidateSize(this)" />
                                <%}
                                %>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="mb-5 center-row">
                                <asp:Image ID="imgPerfil" ImageUrl="https://i.imgur.com/yzczBvI.png" runat="server" CssClass="img-profile" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- Fin datos principales --%>

            <%-- Estructura botones aceptar/cancelar --%>
            <div class="row">
                <div class="col-6 flex-end">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-outline-light btn-primary w-120" OnClick="btnAceptar_Click" />
                </div>
                <div class="col-6">
                    <a href="Default.aspx" class="btn btn-outline-light btn-danger w-120">Cancelar</a>
                </div>
            </div>
            <%-- Fin estructura botones aceptar/cancelar --%>
            <hr class="border-5 mt-3" />
            <%-- Estructura datos de acceso --%>
            <h3 class="text-center text-light">Datos de acceso</h3>

            <div class="row text-light">
                <div class="col-6 mb-2 center-col">
                    <div class="mb-3">
                        <asp:Label ID="Label1" CssClass="h5 form-label" runat="server" Text="Email:"></asp:Label>
                    </div>
                    <div class="mb-3">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblEmailUser" runat="server" CssClass="form-label alert-link" Text="Label"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col center-col-justify mb-2">
                    <asp:Button ID="btnCambiarEmail" runat="server" Text="Cambiar Email" CssClass="btn btn-primary btn-outline-light w-170" OnClick="btnCambiarEmail_Click" />
                </div>
            </div>
            <div class="row text-light">
                <div class="col-6 mb-2 center-col">
                    <div class="mb-3">
                        <asp:Label ID="Label2" CssClass="h5 form-label" runat="server" Text="Contraseña:"></asp:Label>
                    </div>
                    <div class="mb-3">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblPassUser" runat="server" CssClass="form-label alert-link" Text="Label"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col center-col-justify mb-2">
                    <asp:Button ID="btnCambiarPass" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-primary btn-outline-light w-170" OnClick="btnCambiarPass_Click" />
                </div>
            </div>
            <%-- Fin estructura datos de acceso --%>
            <hr class="border-5 mt-4" />
        </div>
    </div>
    <%-- Fin Formulario --%>

    <%-- Modal cambio email/contraseña --%>

    <asp:Panel ID="modalCambio" CssClass="modalAbrir" runat="server">
        <div class="card bg-black text-bg-danger border">
            <%-- Titulo --%>
            <div class="card-header center-row">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblCambioAcceso" CssClass="h4" runat="server" Text="Label"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCambiarEmail" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCambiarPass" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <%-- Fin Titulo --%>
            </div>
            <div class="card-body">
                <%-- Campos --%>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <%if (changeEmail)
                            {  %>
                        <label class="card-text">Ingrese su email actual:</label>
                        <asp:TextBox ID="txtEmailActual" CssClass="form-control" placeholder="emailactual@emailactual.com" runat="server"></asp:TextBox>
                        <label class="card-text">Ingrese un nuevo email:</label>
                        <asp:TextBox ID="txtEmailNuevo" CssClass="form-control" placeholder="emailnuevo@emailnuevo.com" runat="server"></asp:TextBox>
                        <%} %>

                        <%if (changePass)
                            { %>
                        <label class="card-text">Ingrese su contraseña actual:</label>
                        <asp:TextBox ID="txtPassActual" TextMode="Password" CssClass="form-control" placeholder="Contraseña Actual" runat="server"></asp:TextBox>
                        <label class="card-text">Ingrese una nueva contraseña:</label>
                        <asp:TextBox ID="txtPassNueva" TextMode="Password" CssClass="form-control" placeholder="Nueva Contraseña" runat="server"></asp:TextBox>
                        <label class="card-text">Repita la nueva contraseña:</label>
                        <asp:TextBox ID="txtPassRepetir" TextMode="Password" CssClass="form-control" placeholder="Repetir Nueva Contraseña" runat="server"></asp:TextBox>
                        <%}  %>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCambiarEmail" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCambiarPass" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <%-- Fin campos --%>
            </div>
            <div class="card-footer center-row mb-3">
                <%-- Botones cancelar/aceptar --%>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnCambiar" CssClass="btn btn-primary btn-outline-light w-120 me-3" runat="server" Text="Guardar" OnClick="btnCambiar_Click" />
                        <asp:Button ID="btnCerrarModal" CssClass="btn btn-danger w-120" runat="server" Text="Cancelar" OnClick="btnCerrarModal_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- Fin botones --%>
            </div>
        </div>
    </asp:Panel>
    <%-- Fin modal --%>
    <asp:Button ID="btnMostrarModal" runat="server" Text="Button" Style="display: none" />
    <%-- Ajax para controlar modal cambio email/pass --%>
    <ajax:ModalPopupExtender runat="server" ID="ajxModal" OkControlID="btnCambiar" CancelControlID="btnCerrarModal"
        TargetControlID="btnMostrarModal" PopupControlID="modalCambio" BackgroundCssClass="modalBG">
    </ajax:ModalPopupExtender>
    <%-- Fin Ajax modal --%>
</asp:Content>

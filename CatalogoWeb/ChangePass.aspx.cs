﻿using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;

namespace CatalogoWeb
{
    public partial class ChangePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Validar.sesion(Session["usuario"]))
                {
                    Session.Add("ErrorCode", "Ya hay una sesión iniciada");
                    Session.Add("Error", "Actualmente hay una sesión iniciada, para cambiar su contraseña, por favor, diríjase a la página Mi perfil y seleccione la opción de cambiar datos de acceso");
                    Response.Redirect("Error.aspx");
                }
                else if (Session["ErrorLogin"] != null)
                {
                    lblCambiar.Visible = false;
                    lblError.Text = (string)Session["ErrorLogin"];
                    lblError.Visible = true;
                }
                else
                    Response.Redirect("Login.aspx");
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = false;
                string titulo = "";
                string mensaje;
                string script;
                txtEmail.Text = txtEmail.Text.ToLower();
                Usuario usuario = Session["usuarioEmergencia"] != null ? (Usuario)Session["usuarioEmergencia"] : null;
                if (!Page.IsValid)
                {
                    status = false;
                    titulo = "No se admiten campos vacíos";
                    mensaje = "Debe completar todos los campos para continuar.";
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                    return;
                }
                mensaje = Helper.passOlvidada(usuario, txtEmail.Text, txtPassword.Text, txtRepetir.Text, ref status, ref titulo);
                script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                if (status)
                {
                    Session.Clear();
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}
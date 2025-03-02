﻿using Datos;
using Helpers;
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
    public partial class Details : System.Web.UI.Page
    {
        public bool Eliminar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Validar.admin(Session["usuario"]))
                    btnRegresar.Text = "Ver listado";
                else
                    btnRegresar.Text = "Volver al inicio";
                DatosProducto datos = new DatosProducto();
                Producto producto = datos.traerProducto(int.Parse(Request.QueryString["id"]));
                Title = "Detalles de " + producto.Nombre;
                if (!IsPostBack)
                {
                    Eliminar = false;
                    imgProducto.ImageUrl = Helper.cargarImagen(producto);
                    txtId.Text = producto.Id.ToString();
                    txtCodigo.Text = producto.Codigo;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    txtCategoria.Text = producto.Categoria.Descripcion;
                    txtMarca.Text = producto.Marca.Descripcion;
                    txtPrecio.Text = producto.Precio.ToString();
                    Session.Add("id", producto.Id);
                }
                if (Validar.sesion(Session["usuario"]))
                {
                    int idUser = ((Usuario)Session["usuario"]).Id;
                    if (Validar.favExistente(producto.Id, idUser))
                        imgFav.ImageUrl = "https://i.imgur.com/69Mns5z.png";
                    else
                        imgFav.ImageUrl = "https://i.imgur.com/w9bX4Nr.png";
                }
            }
            catch (ArgumentNullException)
            {
                Session.Add("ErrorCode", "No se seleccionó ningun producto.");
                Session.Add("Error", "No se encontró ningun artículo. Para mostrar los detalles de un artículo, por favor seleccione un artículo y presione en el boton 'Ver detalles'");
                Response.Redirect("Error.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddProduct.aspx?id=" + Session["id"], false);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = ((List<Producto>)Session["productos"]).Find(p => p.Id == int.Parse(Request.QueryString["Id"]));
                Eliminar = true;
                lblConfirmación.Text = "¿Está seguro que desea eliminar el producto " + producto.Nombre + "?. Ésta acción no se puede revertir.";
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                DatosProducto.eliminar(int.Parse(Request.QueryString["id"]));
                DatosFavorito.forzarEliminacion(int.Parse(Request.QueryString["Id"]));
                Response.Redirect("ProductList.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Eliminar = false;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar.admin(Session["usuario"]))
                    Response.Redirect("ProductList.aspx");
                else
                    Response.Redirect("Default.aspx");
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void imgFav_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                {
                    int idProd = Session["id"] != null ? (int)Session["id"] : 0;
                    int idUser = Session["usuario"] != null ? ((Usuario)Session["usuario"]).Id : 0;
                    if (Validar.favExistente(idProd, idUser))
                    {
                        DatosFavorito.eliminarFav(idProd, idUser);
                        imgFav.ImageUrl = "https://i.imgur.com/w9bX4Nr.png";

                    }
                    else
                    {
                        Favorito fav = new Favorito();
                        fav.Producto = new Producto();
                        fav.Usuario = new Usuario();
                        fav.Producto.Id = idProd;
                        fav.Usuario.Id = idUser;
                        DatosFavorito.agregarFav(fav);
                        imgFav.ImageUrl = "https://i.imgur.com/Bem812K.gif";

                    }
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
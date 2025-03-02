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

namespace CatalogoWeb
{
    public partial class MyFavorites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Mis Favoritos";
            try
            {
                if (!IsPostBack)
                {
                    DatosProducto datos = new DatosProducto();
                    int id = Session["usuario"] != null ? ((Usuario)Session["usuario"]).Id : 0;
                    List<Favorito> lista = DatosFavorito.listar(id);
                    List<Producto> favoritos = new List<Producto>();
                    foreach (Favorito fav in lista)
                    {
                        favoritos.Add(datos.traerProducto(fav.Producto.Id));
                        foreach (Producto producto in favoritos)
                        {
                            producto.ImagenUrl = Helper.cargarImagen(producto);
                        }
                    }
                    if (favoritos.Count == 0)
                        lblFavoritos.Visible = true;
                    else
                        lblFavoritos.Visible = false;
                    repFav.DataSource = favoritos;
                    repFav.DataBind();
                }
            }
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
                ImageButton imgbtn = (ImageButton)sender;
                RepeaterItem item = (RepeaterItem)imgbtn.NamingContainer;
                ImageButton imgbtnId = (ImageButton)item.FindControl("imgProd");
                int idProd = int.Parse(imgbtnId.AlternateText);
                int idUser = Session["usuario"] != null ? ((Usuario)Session["usuario"]).Id : 0;
                DatosFavorito.eliminarFav(idProd, idUser);
                DatosProducto datos = new DatosProducto();
                List<Favorito> lista = DatosFavorito.listar(idUser);
                List<Producto> favoritos = new List<Producto>();
                foreach (Favorito fav in lista)
                {
                    favoritos.Add(datos.traerProducto(fav.Producto.Id));
                    Helper.cargarImgRep(favoritos);
                }
                repFav.DataSource = null;
                repFav.DataSource = favoritos;
                repFav.DataBind();
                if (favoritos.Count == 0)
                    lblFavoritos.Visible = true;
                else
                    lblFavoritos.Visible = false;
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void imgProd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imgbtn = (ImageButton)sender;
                RepeaterItem item = (RepeaterItem)imgbtn.NamingContainer;
                ImageButton imgbtnId = (ImageButton)item.FindControl("imgProd");
                int idProd = int.Parse(imgbtnId.AlternateText);
                Response.Redirect("Details.aspx?id=" + idProd);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}
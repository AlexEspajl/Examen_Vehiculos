using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Examen_Vehiculos.Modelo;
using System.ComponentModel;
using static Examen_Vehiculos.Modelo.responseAseg;

namespace Examen_Vehiculos
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarDdlMarca();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlMarca.SelectedItem.Value) == 0)
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (Convert.ToInt32(ddlSubMarca.SelectedItem.Value) == 0)
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (Convert.ToInt32(ddlModelo.SelectedItem.Value) == 0)
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (Convert.ToInt32(ddlDescripcion.SelectedItem.Value) == 0)
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (txtCodigoPostal.Text == "")
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (txtEstado.Text == "")
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (txtMunicipio.Text == "")
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else if (Convert.ToInt32(ddlColonia.SelectedItem.Value) == 0)
            {
                ShowAlertMessage("Hay campos vacios");
            }
            else
            {
                try
                {
                    string valueDescripcionId = lblDescripcionId.Text;

                    HttpClient client = new HttpClient();

                    var descripcionId = new Dictionary<string, string>
                        {
                           { "descripcionId" , valueDescripcionId }
                        };
                    var content = new FormUrlEncodedContent(descripcionId);
                    var response = client.PostAsync(
                           "https://web.aarco.com.mx/api-examen/api/examen/crear-peticion", content).Result;
                    

                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;

                        try
                        {

                            string apiUrl = "https://web.aarco.com.mx/api-examen/api/examen/peticion/";
                            string url = string.Format(apiUrl + "{0}", res);
                            url = url.Replace("\"", "");
                            var responseas = client.GetAsync(url).Result;

                            string resp = responseas.Content.ReadAsStringAsync().Result;

                            Asegurdora aseguradorasObj = JsonConvert.DeserializeObject<Asegurdora>(resp);

                            for(int i = 0; i < aseguradorasObj.aseguradoras.Count; i++)
                            {
                                if(aseguradorasObj.aseguradoras[i].AseguradoraId == 1)
                                {
                                    lblAxa.Text = "$ " + aseguradorasObj.aseguradoras[i].Tarifa.ToString();
                                }
                                if (aseguradorasObj.aseguradoras[i].AseguradoraId == 2)
                                {
                                    lblChubb.Text = "$ " + aseguradorasObj.aseguradoras[i].Tarifa.ToString();
                                }
                                if (aseguradorasObj.aseguradoras[i].AseguradoraId == 3)
                                {
                                    lblZurich.Text = "$ " + aseguradorasObj.aseguradoras[i].Tarifa.ToString();
                                }
                                if (aseguradorasObj.aseguradoras[i].AseguradoraId == 4)
                                {
                                    lblQualitas.Text = "$ " + aseguradorasObj.aseguradoras[i].Tarifa.ToString();
                                }
                                if (aseguradorasObj.aseguradoras[i].AseguradoraId == 5)
                                {
                                    lblHdi.Text = "$ " + aseguradorasObj.aseguradoras[i].Tarifa.ToString();
                                }
                            }

                            Aseguradoras.Visible = true;
                            
                        }
                        catch
                        {
                            ShowAlertMessage("No se obtuvo la petición");
                        }
                    }
                    else
                    {
                        ShowAlertMessage("No se obtuvo la petición");
                    }
                }
                catch
                {
                    ShowAlertMessage("No se obtuvo la petición");
                }
            }
        }
        protected void Aceptar_Click(object sender, EventArgs e)
        {
            ddlMarca.Items.Clear();
            ddlSubMarca.Items.Clear();
            ddlModelo.Items.Clear();
            ddlDescripcion.Items.Clear();
            ddlColonia.Items.Clear();
            llenarDdlMarca();
            txtCodigoPostal.Text = "";
            txtEstado.Text = "";
            txtMunicipio.Text = "";

        }

        public static void ShowAlertMessage(string error)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                error = error.Replace("'", "'");
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valorMarca = Convert.ToInt32(ddlMarca.SelectedItem.Value);
            llenarDdlSubMarca(valorMarca);
            lblDescripcionId.Text = "";
        }

        protected void ddlSubMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valorSubMarca = Convert.ToInt32(ddlSubMarca.SelectedItem.Value);
            llenarDdlModelo(valorSubMarca);
            lblDescripcionId.Text = "";
        }

        protected void ddlModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valorModelo = Convert.ToInt32(ddlModelo.SelectedItem.Value);
            llenarDdlDescripcion(valorModelo);
            lblDescripcionId.Text = "";
        }

        protected void ddlDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorDescripcion = ddlDescripcion.SelectedItem.Value;
            lblDescripcionId.Text = valorDescripcion;
        }

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            string apiUrl = "https://web.aarco.com.mx/api-examen/api/examen/sepomex/";

            HttpClient client = new HttpClient();

            var response = client.GetAsync(string.Format(apiUrl + "{0}", txtCodigoPostal.Text)).Result;

            string res = response.Content.ReadAsStringAsync().Result;

            //var jObj = (JObject)JsonConvert.DeserializeObject(res, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
            //var jsonData = JObject.Parse(res);

            dynamic api = JObject.Parse(res);
            string quote = api.CatalogoJsonString;
            quote = quote.Substring(1);
            quote = quote.Substring(0, quote.Length - 1);   
            JavaScriptSerializer js = new JavaScriptSerializer();
            //dynamic d = js.Deserialize<dynamic>(quote);
            // reponse resp = js.Deserialize<reponse>(quote);
            //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(res);

            Root codigoPostal = JsonConvert.DeserializeObject<Root>(quote);

            txtEstado.Text = codigoPostal.Municipio.Estado.sEstado;

            txtMunicipio.Text = codigoPostal.Municipio.sMunicipio;

            DataTable dsColonia = DatatableConversion.ToDataTable(codigoPostal.Ubicacion);

            ddlColonia.DataSource = dsColonia;
            ddlColonia.DataTextField = "sUbicacion";
            ddlColonia.DataValueField = "iIdUbicacion";
            ddlColonia.DataBind();

            ddlColonia.Items.Insert(0, new ListItem("<Seleccionar una colonia>", "0"));
        }
        
        private void llenarDdlMarca()
        {
            string apiUrl = "http://localhost:61773/api/Catalogos/Marca/";
            
            HttpClient client = new HttpClient();
           
            var response = client.GetAsync(apiUrl).Result;

            var res = response.Content.ReadAsStringAsync().Result;
           
            DataTable dsMarcas = JsonConvert.DeserializeObject<DataTable>(res);

            ddlMarca.DataSource = dsMarcas;
            ddlMarca.DataTextField = "MarcaDes";
            ddlMarca.DataValueField = "idMarca";
            ddlMarca.DataBind();

            ddlMarca.Items.Insert(0, new ListItem("<Seleccionar un marca>", "0"));
            ddlSubMarca.Items.Insert(0, new ListItem("<Seleccionar una submarca>", "0"));
            ddlModelo.Items.Insert(0, new ListItem("<Seleccionar un modelo>", "0"));
            ddlDescripcion.Items.Insert(0, new ListItem("<Seleccionar una descripcion>", "0"));
            ddlColonia.Items.Insert(0, new ListItem("<Seleccionar una colonia>", "0"));
        }

        private void llenarDdlSubMarca(int idMarca)
        {
            string apiUrl = "http://localhost:61773/api/Catalogos/SubMarca/";
            
            HttpClient client = new HttpClient();
            
            var response = client.GetAsync(string.Format(apiUrl + "?idMarca={0}", idMarca)).Result;

            var res = response.Content.ReadAsStringAsync().Result;
            
            DataTable dsSubMarca = JsonConvert.DeserializeObject<DataTable>(res);

            ddlSubMarca.DataSource = dsSubMarca;
            ddlSubMarca.DataTextField = "SubMarcaDes";
            ddlSubMarca.DataValueField = "idSubMarca";
            ddlSubMarca.DataBind();
            ddlSubMarca.Items.Insert(0, new ListItem("<Seleccionar una submarca>", "0"));
        }

        private void llenarDdlModelo(int idSubMarca)
        {
            string apiUrl = "http://localhost:61773/api/Catalogos/Modelo/";
            
            HttpClient client = new HttpClient();

            var response = client.GetAsync(string.Format(apiUrl + "?idSubMarca={0}", idSubMarca)).Result;

            var res = response.Content.ReadAsStringAsync().Result;
            
            DataTable dsModelo = JsonConvert.DeserializeObject<DataTable>(res);

            ddlModelo.DataSource = dsModelo;
            ddlModelo.DataTextField = "ModeloDes";
            ddlModelo.DataValueField = "idModelo";
            ddlModelo.DataBind();
            ddlModelo.Items.Insert(0, new ListItem("<Seleccionar un modelo>", "0"));
        }

        private void llenarDdlDescripcion(int idModelo)
        {
            string apiUrl = "http://localhost:61773/api/Catalogos/Descripcion/";
            
            HttpClient client = new HttpClient();
           
            var response = client.GetAsync(string.Format(apiUrl + "?idModelo={0}", idModelo)).Result;

            var res = response.Content.ReadAsStringAsync().Result;
            
            DataTable dsDescripcion = JsonConvert.DeserializeObject<DataTable>(res);

            ddlDescripcion.DataSource = dsDescripcion;
            ddlDescripcion.DataTextField = "DescripcionDes";
            ddlDescripcion.DataValueField = "DescripcionId";
            ddlDescripcion.DataBind();
            ddlDescripcion.Items.Insert(0, new ListItem("<Seleccionar una descripcion>", "0"));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace ContraseñasSeguras.SkyDrive
{
    static class clSkyDrive
    {
        private const string cAplicacionClientId = "00000000440EC971";
        private const string cAplicacionRedirectUri = @"http://masdominiodesarrollo.es/";
        //private const string cAplicacionRedirectUri = @"https://login.live.com/oauth20_desktop.srf";
        private const string cAplicacionAccessToken = "xoW5rQikZ7pcgexG0Lm8RJpgJJVFF4Yd";

        private const string cURIAutorizacion = @"https://login.live.com/oauth20_authorize.srf";
        private const string cURIDirectoriosRaiz = @"https://apis.live.net/v5.0/me/skydrive/files";
        private const string cURIPermisosActuales = @"https://apis.live.net/v5.0/me/permissions";

        static private string sAccessToken = "";

        private const string cNombreRaiz = "SkyDrive";
        public const string cTipoHijoDirectorio = "folder";
        public const string cTipoHijoFichero = "file";

        public static void inicializar()
        {
            sAccessToken = string.Empty;
        }

        public static string getURLLogin()
        {
            var authorizeUri = new StringBuilder(cURIAutorizacion);

            authorizeUri.AppendFormat("?client_id={0}&", cAplicacionClientId);
            //authorizeUri.AppendFormat("scope={0}&", "wl.signin%20wl.skydrive%20wl.photos");
            authorizeUri.AppendFormat("scope={0}&", "wl.signin%20wl.skydrive%20wl.skydrive_update");
            authorizeUri.AppendFormat("response_type={0}&", "token");
            authorizeUri.AppendFormat("expires_in={0}&", "30");
            authorizeUri.AppendFormat("redirect_uri={0}", UrlEncode(cAplicacionRedirectUri));

            return @authorizeUri.ToString();
        }

        private static string UrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }

            var values = new Dictionary<string, string>
            {
                { "!", "%21" },
                { "#", "%23" },
                { "$", "%24" },
                { "&", "%26" },
                { "'", "%27" },
                { "(", "%28" },
                { ")", "%29" },
                { "*", "%2A" },
                { "+", "%2B" },
                { ",", "%2C" },
                { "/", "%2F" },
                { ":", "%3A" },
                { ";", "%3B" },
                { "=", "%3D" },
                { "?", "%3F" },
                { "@", "%40" },
                { "[", "%5B" },
                { "]", "%5D" }
            };

            var data = new StringBuilder(new string(temp));
            foreach (string character in values.Keys)
            {
                data.Replace(character, values[character]);
            }

            return data.ToString();
        }

        public static bool conexionActiva()
        {
            string result = string.Empty;
            HttpWebResponse hResp = null;
            if (AccessToken == null || AccessToken.Length == 0) return false;
            try
            {
                string url = string.Format(cURIPermisosActuales + @"?access_token={0}", AccessToken);
                HttpStatusCode hEstado = HttpStatusCode.OK;
                hResp = llamarGETASkyDrive(url, ref hEstado);
                if (hEstado.Equals(HttpStatusCode.Unauthorized))
                    return false;

                using (StreamReader sr = new StreamReader(hResp.GetResponseStream(), Encoding.UTF8))
                {
                    result = @sr.ReadToEnd();
                }

                if (result.Contains("wl.skydrive"))
                    return true;
                else
                    return false;
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.Unauthorized))
                    {
                        return false;
                    }
                    else if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.ProxyAuthenticationRequired))
                    {
                        return false;
                    }
                    else
                        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al comprobar la conexión con SkyDrive (" + sDes + ")", "clSkyDrive.conexionActiva", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al comprobar la conexión con SkyDrive", "clSkyDrive.conexionActiva", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al comprobar la conexión con SkyDrive", "clSkyDrive.conexionActiva", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        public static bool reanudarConexionActiva(Form frmPadre)
        {
            string result = string.Empty;
            try
            {
                string url = getURLLogin();
                HttpStatusCode hEstado = HttpStatusCode.OK;
                HttpWebResponse hResp = llamarGETASkyDrive(url, ref hEstado);
                if (hEstado.Equals(HttpStatusCode.Unauthorized))
                {
                    using (var signIndlg = new ContraseñasSeguras.CapaPresentacion.frmSkyDriveLogin())
                    {
                        if (signIndlg.ShowDialog(frmPadre) == DialogResult.OK)
                        {
                            AccessToken = signIndlg.AuthCode;
                        }
                        else
                        {
                            return false;
                            //throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("No se pudo conectar con SkyDrive", "clSkyDrive.reanudarConexionActiva", ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                        }
                    }
                }

                var x = hResp.ResponseUri.AbsoluteUri.Split(new[] { "#access_token=" }, StringSplitOptions.RemoveEmptyEntries);
                AccessToken = x[1].Split(new[] { '&' })[0];
                using (StreamReader sr = new StreamReader(hResp.GetResponseStream(), Encoding.UTF8))
                {
                    result = @sr.ReadToEnd();
                }
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;

                    if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.Unauthorized))
                    {
                        return false;
                    }
                    else if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.ProxyAuthenticationRequired))
                    {
                        return false;
                    }
                    else
                        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al comprobar la conexión con SkyDrive (" + sDes + ")", "clSkyDrive.conexionActiva", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al comprobar la conexión con SkyDrive", "clSkyDrive.conexionActiva", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al comprobar la conexión con SkyDrive", "clSkyDrive.conexionActiva", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
            return true;
        }

        public static void reconectar(Form frmPadre)
        {
            if (conexionActiva()) return;

            if (AccessToken.Equals(string.Empty))
            {
                //MessageBox.Show("Abro login");
                using (var signIndlg = new ContraseñasSeguras.CapaPresentacion.frmSkyDriveLogin())
                {
                    if (signIndlg.ShowDialog(frmPadre) == DialogResult.OK)
                    {
                        AccessToken = signIndlg.AuthCode;
                        //MessageBox.Show("Tengo token");
                    }
                    else
                        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("No se pudo conectar con SkyDrive", "clSkyDrive.reconectar", ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            else
            {
                if (!reanudarConexionActiva(frmPadre))
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("No se pudo conectar con SkyDrive y debería", "clSkyDrive.reconectar", ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcProgramacion);
            }
        }

        private static void ponerHijosEnControl(TreeNode tNodo, TreeView control, StreamReader reader, bool bSoloDirectorios)
        {
            TreeNode tNodo2Nivel;
            using (reader)
            {
                var json = reader.ReadToEnd();

                var hijos = JsonConvert.DeserializeObject<clRESTLogin>(json);

                foreach (var hijo in hijos.Hijos)
                {
                    if (hijo.Tipo.Equals(cTipoHijoFichero))
                        if (!hijo.Nombre.EndsWith(".pas"))
                            continue;

                    if (bSoloDirectorios)
                        if (!hijo.Tipo.Equals(cTipoHijoDirectorio))
                            continue;

                    tNodo2Nivel = new TreeNode(hijo.Nombre);
                    tNodo2Nivel.Name = hijo.LocalizacionUpload;
                    tNodo2Nivel.Tag = json;
                    if (hijo.Tipo.Equals(cTipoHijoFichero))
                        tNodo2Nivel.ImageIndex = 1;
                    else
                        tNodo2Nivel.ImageIndex = 0;
                    tNodo2Nivel.SelectedImageIndex = tNodo2Nivel.ImageIndex;

                    tNodo.Nodes.Add(tNodo2Nivel);
                }
            }

            control.Refresh();
            control.SelectedNode = tNodo;
            control.SelectedNode.Expand();
        }

        public static void getDirectoriosRaiz(TreeView control, bool bSoloDirectorios)
        {
            control.Nodes.Add(cNombreRaiz);
            TreeNode tNodo = new TreeNode();
            tNodo = control.Nodes[0];
            tNodo.ImageIndex = 0;
            tNodo.SelectedImageIndex = tNodo.ImageIndex;

            HttpStatusCode hEstado = HttpStatusCode.OK;
            HttpWebResponse response = llamarGETASkyDrive(cURIDirectoriosRaiz, ref hEstado);
            if (hEstado.Equals(HttpStatusCode.Unauthorized))
                return;

            ponerHijosEnControl(tNodo, control, new StreamReader(response.GetResponseStream()), bSoloDirectorios);
        }

        public static void getHijosDeDirectorio(TreeView control, bool bSoloDirectorios)
        {
            TreeNode tNodo = control.SelectedNode;
            if (tNodo == null) return;

            HttpStatusCode hEstado = HttpStatusCode.OK;
            HttpWebResponse response = llamarGETASkyDrive(tNodo.Name, ref hEstado);
            if (hEstado.Equals(HttpStatusCode.Unauthorized))
                return;

            ponerHijosEnControl(tNodo, control, new StreamReader(response.GetResponseStream()), bSoloDirectorios);
        }

        public static byte[] descargarFicheroAByte(string sURLFichero)
        {
            try
            {
                string url = string.Format(sURLFichero + @"?access_token={0}", AccessToken);
                WebClient clienteWeb = new WebClient();
                clienteWeb.Encoding = Encoding.UTF8;
                byte[] btContenidoEncriptado = clienteWeb.DownloadData(url);
                return btContenidoEncriptado;
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive (" + sDes + ")", "clSkyDrive.descargarFicheroAStream", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAStream", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAStream", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        public static MemoryStream descargarFicheroAStream(string sURLFichero)
        {
            try
            {
                string url = string.Format(sURLFichero + @"?access_token={0}", AccessToken);
                WebClient clienteWeb = new WebClient();
                clienteWeb.Encoding = Encoding.UTF8;
                byte[] bContenidoEncriptado = clienteWeb.DownloadData(url);
                MemoryStream mContenidoFichero = new MemoryStream(bContenidoEncriptado);
                return mContenidoFichero;
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive (" + sDes + ")", "clSkyDrive.descargarFicheroAStream", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAStream", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAStream", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        public static string AccessToken
        {
            get { return clSkyDrive.sAccessToken; }
            set { clSkyDrive.sAccessToken = value; }
        }

        public static void guardarFicheroPUT(string sRutaCompletaFichero, MemoryStream stContenidoFicheroEncriptado)
        {
            // En sRutaCompletaFichero va la URL con el directorio y el nombre del fichero nuevo / existente a guardar. Algo del estilo siguiente :
            // string url = string.Format(@"https://apis.live.net/v5.0/me/skydrive/files/{0}?access_token={1}", Path.GetFileName(fileName), access_token);

            string sURLFichero = sRutaCompletaFichero;
            try
            {
                // Probar esta manera
                string url = string.Format(sRutaCompletaFichero + @"?access_token={0}", AccessToken);
                byte[] buff = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(stContenidoFicheroEncriptado);
                WebClient clienteWeb = new WebClient();
                clienteWeb.Encoding = Encoding.UTF8;
                clienteWeb.UploadData(new Uri(url), "PUT", buff);
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive (" + sDes + ")", "clSkyDrive.guardarFicheroPUT", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive", "clSkyDrive.guardarFicheroPUT", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive", "clSkyDrive.guardarFicheroPUT", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        private static HttpWebResponse llamarGETASkyDriveConProxy(string sURLBase, ref HttpStatusCode hEstado)
        {
            HttpWebRequest request = null;
            try
            {
                hEstado = HttpStatusCode.OK;
                var requestUri = new StringBuilder(sURLBase);
                requestUri.AppendFormat("?access_token={0}", AccessToken);
                request = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
                request.Method = "GET";

                if (AccessToken.Length > 0)
                    return (HttpWebResponse)request.GetResponse();

                IWebProxy proxy = request.Proxy;
                if (proxy != null)
                {
                    WebProxy mProxy = new WebProxy();
                    mProxy.Address = new Uri(requestUri.ToString());
                    mProxy.Credentials = new NetworkCredential("SE46765", "Castro06");
                    request.Proxy = mProxy;
                    return (HttpWebResponse)request.GetResponse();
                }
                else
                {
                    return (HttpWebResponse)request.GetResponse();
                }
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    hEstado = ((HttpWebResponse)wexc1.Response).StatusCode;
                    if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.Unauthorized))
                    {
                        return null;
                    }
                    else if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.ProxyAuthenticationRequired))
                    {
                        return null;
                    }
                    else
                        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive (" + sDes + ")", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        private static HttpWebResponse llamarGETASkyDrive(string sURLBase, ref HttpStatusCode hEstado)
        {
            HttpWebResponse hh = llamarGETASkyDriveConProxy(sURLBase, ref hEstado);
            return hh;

            HttpWebRequest request = null;
            try
            {
                hEstado = HttpStatusCode.OK;
                var requestUri = new StringBuilder(sURLBase);
                requestUri.AppendFormat("?access_token={0}", AccessToken);
                request = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
                request.Method = "GET";
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    hEstado = ((HttpWebResponse)wexc1.Response).StatusCode;
                    if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.Unauthorized))
                    {
                        return null;
                    }
                    else if (((HttpWebResponse)wexc1.Response).StatusCode.Equals(HttpStatusCode.ProxyAuthenticationRequired))
                    {
                        return null;
                    }
                    else
                        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive (" + sDes + ")", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        public static string descargarFicheroAString(string sURLFichero)
        {
            string result = null;
            HttpWebResponse response = null;
            try
            {
                HttpStatusCode hEstado = HttpStatusCode.OK;
                response = llamarGETASkyDrive(sURLFichero, ref hEstado);
                if (hEstado.Equals(HttpStatusCode.Unauthorized))
                    return string.Empty;

                Encoding responseEncoding;
                if (response.CharacterSet != null && response.CharacterSet.Length != 0)
                    responseEncoding = Encoding.GetEncoding(response.CharacterSet);
                else
                    responseEncoding = Encoding.UTF8;
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), responseEncoding))
                {
                    result = @sr.ReadToEnd();
                }
            }
            catch (WebException wexc1)
            {
                if (wexc1.Status == WebExceptionStatus.ProtocolError)
                {
                    // can also get the decription: 
                    string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
                    int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive (" + sDes + ")", "clSkyDrive.descargarFicheroAString", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
                else
                {
                    throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAString", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                }
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAString", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }

            return result;
        }























        //private static HttpWebResponse llamarGETASkyDrive(string sURLBase)
        //{
        //    try
        //    {
        //        var requestUri = new StringBuilder(sURLBase);
        //        requestUri.AppendFormat("?access_token={0}", AccessToken);

        //        var request = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
        //        request.Method = "GET";
        //        //request.ContentType = "text/html";

        //        return (HttpWebResponse)request.GetResponse();
        //    }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive (" + sDes + ")", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }
        //}

        //private static HttpWebRequest llamarPUTASkyDrive(string sURLBase)
        //{
        //    try
        //    {
        //        var requestUri = new StringBuilder(sURLBase);
        //        requestUri.AppendFormat("?access_token={0}", AccessToken);

        //        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUri.ToString());
        //        request.Method = "PUT";
        //        request.ContentType = "application/octet-stream";
        //        return request;
        //    }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive (" + sDes + ")", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarGETASkyDrive", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }

        //}

        //private static void ponerHijosEnControl(TreeNode tNodo, TreeView control, StreamReader reader, bool bSoloDirectorios)
        //{
        //    TreeNode tNodo2Nivel;
        //    using (reader)
        //    {
        //        var json = reader.ReadToEnd();

        //        var hijos = JsonConvert.DeserializeObject<clRESTLogin>(json);

        //        foreach (var hijo in hijos.Hijos)
        //        {
        //            if (hijo.Tipo.Equals(cTipoHijoFichero))
        //                if (!hijo.Nombre.EndsWith(".pas"))
        //                    continue;

        //            if (bSoloDirectorios)
        //                if (!hijo.Tipo.Equals(cTipoHijoDirectorio))
        //                    continue;

        //            tNodo2Nivel = new TreeNode(hijo.Nombre);
        //            tNodo2Nivel.Name = hijo.LocalizacionUpload;
        //            tNodo2Nivel.Tag = json;
        //            if (hijo.Tipo.Equals(cTipoHijoFichero))
        //                tNodo2Nivel.ImageIndex = 1;
        //            else
        //                tNodo2Nivel.ImageIndex = 0;
        //            tNodo2Nivel.SelectedImageIndex = tNodo2Nivel.ImageIndex;

        //            tNodo.Nodes.Add(tNodo2Nivel);
        //        }
        //    }

        //    control.Refresh();
        //    control.SelectedNode = tNodo;
        //    control.SelectedNode.Expand();
        //}

        //public static void getDirectoriosRaiz(TreeView control, bool bSoloDirectorios)
        //{
        //    control.Nodes.Add(cNombreRaiz);
        //    TreeNode tNodo = new TreeNode();
        //    tNodo = control.Nodes[0];
        //    tNodo.ImageIndex = 0;
        //    tNodo.SelectedImageIndex = tNodo.ImageIndex;

        //    var response = llamarGETASkyDrive(cURIDirectoriosRaiz);
        //    ponerHijosEnControl(tNodo, control, new StreamReader(response.GetResponseStream()), bSoloDirectorios);
        //}

        //public static void getHijosDeDirectorio(TreeView control, bool bSoloDirectorios)
        //{
        //    TreeNode tNodo = control.SelectedNode;
        //    if (tNodo == null) return;

        //    var response = llamarGETASkyDrive(tNodo.Name);
        //    ponerHijosEnControl(tNodo, control, new StreamReader(response.GetResponseStream()), bSoloDirectorios);
        //}

        //public static MemoryStream descargarFicheroAStream(string sURLFichero)
        //{
        //    try
        //    {
        //        MemoryStream mContenidoFichero = new MemoryStream();

        //        var response = llamarGETASkyDrive(sURLFichero);
        //        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //        //var json = reader.ReadToEnd();
        //        //Console.WriteLine(json.ToString());
        //        //var hijos = JsonConvert.DeserializeObject<clRESTLogin>(json);

        //        response.GetResponseStream().CopyTo(mContenidoFichero);
        //        return mContenidoFichero;

        //        //Encoding responseEncoding;
        //        //if (response.CharacterSet != null && response.CharacterSet.Length != 0)
        //        //    responseEncoding = Encoding.GetEncoding(response.CharacterSet);
        //        //else
        //        //    responseEncoding = Encoding.UTF8;
        //        //StreamReader sr = new StreamReader(response.GetResponseStream(), responseEncoding);
        //        //sr.BaseStream.CopyTo(mContenidoFichero);
        //        //return mContenidoFichero;

        //    }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive (" + sDes + ")", "clSkyDrive.descargarFicheroAStream", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAStream", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAStream", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }

        //}

        ////public static string descargarAString(string sURLFichero)
        ////{

        ////    string result = null;
        ////    HttpWebResponse Response = null;
        ////    try
        ////    {
        ////        HttpWebResponse hwRes;
        ////        hwRes.a
        ////        Response = llamarGETASkyDrive(sURLFichero);
        ////        int bufferSize = 1;

        ////        .Clear();
        ////        Response.ClearHeaders();
        ////        Response.ClearContent();
        ////        Response.AppendHeader("Content-Disposition:", "attachment; filename=Sample.jpg");
        ////        Response.AppendHeader("Content-Length", objResponse.ContentLength.ToString());
        ////        Response.ContentType = "image/jpeg";
        ////        byte[] byteBuffer = new byte[bufferSize + 1];
        ////        MemoryStream memStrm = new MemoryStream(byteBuffer, true);
        ////        Stream strm = objRequest.GetResponse().GetResponseStream();
        ////        byte[] bytes = new byte[bufferSize + 1];

        ////        while (strm.Read(byteBuffer, 0, byteBuffer.Length) > 0)
        ////        {
        ////            Response.BinaryWrite(memStrm.ToArray());
        ////            Response.Flush();
        ////        }

        ////        Response.Close();
        ////        Response.End();
        ////        memStrm.Close();
        ////        memStrm.Dispose();
        ////        strm.Dispose(); 
        ////    }
        ////    catch (WebException wexc1)
        ////    {
        ////        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        ////        {
        ////            // can also get the decription: 
        ////            //  ((HttpWebResponse)wexc1.Response).StatusDescription;
        ////            // status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        if (response != null)
        ////            response.Close();
        ////    }

        ////    return result;

        ////}

        //public static string descargarFicheroAString(string sURLFichero)
        //{
        //    string result = null;
        //    HttpWebResponse response = null;
        //    try
        //    {
        //        response = llamarGETASkyDrive(sURLFichero);
        //        Encoding responseEncoding;
        //        if (response.CharacterSet != null && response.CharacterSet.Length != 0)
        //            responseEncoding = Encoding.GetEncoding(response.CharacterSet);
        //        else
        //            responseEncoding = Encoding.UTF8;
        //        using (StreamReader sr = new StreamReader(response.GetResponseStream(), responseEncoding))
        //        {
        //            result = @sr.ReadToEnd();
        //        }
        //    }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive (" + sDes + ")", "clSkyDrive.descargarFicheroAString", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAString", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al descargar fichero de SkyDrive", "clSkyDrive.descargarFicheroAString", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }

        //    return result;
        //}

        //private static string UrlEncode(string s)
        //{
        //    char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
        //    for (int i = 0; i < temp.Length - 2; i++)
        //    {
        //        if (temp[i] == '%')
        //        {
        //            temp[i + 1] = char.ToUpper(temp[i + 1]);
        //            temp[i + 2] = char.ToUpper(temp[i + 2]);
        //        }
        //    }

        //    var values = new Dictionary<string, string>
        //    {
        //        { "!", "%21" },
        //        { "#", "%23" },
        //        { "$", "%24" },
        //        { "&", "%26" },
        //        { "'", "%27" },
        //        { "(", "%28" },
        //        { ")", "%29" },
        //        { "*", "%2A" },
        //        { "+", "%2B" },
        //        { ",", "%2C" },
        //        { "/", "%2F" },
        //        { ":", "%3A" },
        //        { ";", "%3B" },
        //        { "=", "%3D" },
        //        { "?", "%3F" },
        //        { "@", "%40" },
        //        { "[", "%5B" },
        //        { "]", "%5D" }
        //    };

        //    var data = new StringBuilder(new string(temp));
        //    foreach (string character in values.Keys)
        //    {
        //        data.Replace(character, values[character]);
        //    }

        //    return data.ToString();
        //}

        //public static void guardarFicheroPUT(string sRutaCompletaFichero, MemoryStream stContenidoFicheroEncriptado)
        //{
        //    // En sRutaCompletaFichero va la URL con el directorio y el nombre del fichero nuevo / existente a guardar. Algo del estilo siguiente :
        //    // string url = string.Format(@"https://apis.live.net/v5.0/me/skydrive/files/{0}?access_token={1}", Path.GetFileName(fileName), access_token);

        //    string sURLFichero = sRutaCompletaFichero;
        //    HttpWebRequest request = null;
        //    try
        //    {
        //        // Probar esta manera
        //        //string url = string.Format(sRutaCompletaFichero + @"?access_token={1}", AccessToken);
        //        //byte[] buff = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(stContenidoFicheroEncriptado);
        //        //using (var client = new WebClient())
        //        //{
        //        //    client.UploadDataAsync(new Uri(url), "PUT", buff);
        //        //}
    
        //        request = llamarPUTASkyDrive(sURLFichero);
        //        byte[] buff = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(stContenidoFicheroEncriptado);

        //        request.ContentLength = buff.Length;
        //        //request.MediaType = "application/json";
        //        request.ContentType = "application/json; charset=utf-8";

        //        //using (var dataStream = request.GetRequestStream())
        //        //{
        //        //    dataStream.Write(buff, 0, buff.Length);
        //        //}
        //        Stream reqStream = request.GetRequestStream();
        //        reqStream.Write(buff, 0, buff.Length);

        //        string resJson = "";
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            StreamReader sr = new StreamReader(response.GetResponseStream());
        //            resJson = sr.ReadToEnd();
        //        }
        //    }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive (" + sDes + ")", "clSkyDrive.guardarFicheroPUT", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive", "clSkyDrive.guardarFicheroPUT", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive", "clSkyDrive.guardarFicheroPUT", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }
        //    finally
        //    {
        //        //if (request != null)
        //        //    request.cClose();
        //    }
        //}

        //private static HttpWebRequest llamarPOSTASkyDrive(string sURLBase)
        //{
        //    try
        //    {
        //        var requestUri = new StringBuilder(sURLBase);
        //        requestUri.AppendFormat("?access_token={0}", AccessToken);

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
        //        request.Method = "POST";
        //        request.ContentType = "multipart/form-data; boundary=AaB03x";
        //        return request;
        //    }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive (" + sDes + ")", "clSkyDrive.llamarPOSTASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarPOSTASkyDrive", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "clSkyDrive.llamarPOSTASkyDrive", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }

        //}

        //public static void guardarFicheroPOST(string sRutaCompletaFichero, string sNombreFichero, MemoryStream stContenidoFicheroEncriptado)
        //{
        //    HttpWebRequest request = null;
        //    try
        //    {
        //        request = llamarPOSTASkyDrive(sRutaCompletaFichero);

        //        string requestHeader = "--AaB03x" + Environment.NewLine + "Content-Disposition: form-data; name=\"file\"; filename=\"" + sNombreFichero + "\"" + Environment.NewLine + "Content-Type: " + "multipart/form-data; boundary=AaB03x" + Environment.NewLine + Environment.NewLine;
        //        string requestTrailer = "--AaB03x--";
        //        byte[] buffHeader = Encoding.UTF8.GetBytes(requestHeader);
        //        byte[] buffTrailer = Encoding.UTF8.GetBytes(requestTrailer);
        //        byte[] buff = new byte[stContenidoFicheroEncriptado.Length];
        //        stContenidoFicheroEncriptado.Read(buff, 0, buff.Length);
        //        //using (StreamReader sr = new StreamReader(stContenidoFicheroEncriptado))
        //        //{
        //        //    int ii = sr.Read(buff, 0, stContenidoFicheroEncriptado.Length);
        //        //}
        //        request.ContentLength = buffHeader.Length + buffTrailer.Length + buff.Length;
        //        using (Stream reqStream = request.GetRequestStream())
        //        {
        //            reqStream.Write(buffHeader, 0, buffHeader.Length);
        //            reqStream.Write(buff, 0, buff.Length);
        //            reqStream.Write(buffTrailer, 0, buffTrailer.Length);
        //        }
        //        string resJson = "";
        //        using (WebResponse res = request.GetResponse())
        //        {
        //            StreamReader sr = new StreamReader(res.GetResponseStream());
        //            resJson = sr.ReadToEnd();
        //        }
        //        MessageBox.Show(resJson);
        //                }
        //    catch (WebException wexc1)
        //    {
        //        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            // can also get the decription: 
        //            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        //            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar fichero en SkyDrive (" + sDes + ")", "clSkyDrive.guardarFicheroPOST", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //        else
        //        {
        //            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar fichero en SkyDrive", "clSkyDrive.guardarFicheroPOST", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //        }
        //    }
        //    catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
        //    {
        //        throw exApp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar fichero en SkyDrive", "clSkyDrive.guardarFicheroPOST", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        //    }
        //    finally
        //    {
        //        //if (request != null)
        //        //    request.Close();
        //    }
        //}

        ////private static void wc_UploadFileCompleted(object sender, UploadDataCompletedEventArgs e) 
        ////{ 
        ////    MemoryStream ms = new MemoryStream(e.Result); 
        ////    StreamReader sr1 = new StreamReader(ms);
        ////    string sResultado = sr1.ReadToEnd();
        ////}

        ////public static void guardarFicheroWebClientPUT(string sRutaCompletaFichero, MemoryStream stContenidoFicheroEncriptado)
        ////{
        ////    try
        ////    {
        ////        WebClient wc = new WebClient();   
        ////        wc.UploadDataCompleted += new UploadDataCompletedEventHandler(wc_UploadFileCompleted);
        ////        //wc.UploadFileAsync(new Uri(sRutaCompletaFichero), System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "photo.jpg"));
        ////        byte[] aDatos = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(stContenidoFicheroEncriptado);
        ////        wc.UploadDataAsync(new Uri(sRutaCompletaFichero), aDatos);
        ////    }
        ////    catch (WebException wexc1)
        ////    {
        ////        if (wexc1.Status == WebExceptionStatus.ProtocolError)
        ////        {
        ////            // can also get the decription: 
        ////            string sDes = ((HttpWebResponse)wexc1.Response).StatusDescription;
        ////            int status = (int)((HttpWebResponse)wexc1.Response).StatusCode;
        ////            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive (" + sDes + ")", "clSkyDrive.guardarFicheroPUT", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        ////        }
        ////        else
        ////        {
        ////            throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive", "clSkyDrive.guardarFicheroPUT", wexc1, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al guardar el fichero en SkyDrive", "clSkyDrive.guardarFicheroPUT", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
        ////    }
        ////    finally
        ////    {
        ////    }
        ////}
    }
}

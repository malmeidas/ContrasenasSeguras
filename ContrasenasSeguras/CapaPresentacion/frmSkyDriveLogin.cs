using System;
using System.Windows.Forms;
using ContraseñasSeguras.SkyDrive;

namespace ContraseñasSeguras.CapaPresentacion
{
    public partial class frmSkyDriveLogin : Form
    {
        //private const string scope = "wl.skydrive_update";
        //private const string client_id = "00000000440EC971";
        //private const string signInUrl = 
        //@"https://login.live.com/oauth20_authorize.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_type=token&scope={1}";
        private string _authCode;
        private bool bMensajeNoConexionMostrado = false;
        private bool bRespondidoSkyDrive = false;

        public string AuthCode
        {
            get
            {
                return _authCode;
            }
        }

        public frmSkyDriveLogin()
        {
            try
            {
                InitializeComponent();


                bMensajeNoConexionMostrado = false;
                this.lblEstado.Text = "Conectando con el Login de SkyDrive";
                wbSkyDriveAuth.DocumentCompleted += recibirRespuestaLogin;
                wbSkyDriveAuth.Navigate(clSkyDrive.getURLLogin());
            }
            catch (Exception ex)
            {
                ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp = new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "frmSkyDriveLogin", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                exApp.verExcepcion(this);
            }
        }

        void recibirRespuestaLogin(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //MessageBox.Show("Respuesta " + e.Url.AbsoluteUri.ToString());
                if (e.Url.AbsoluteUri.Contains("#access_token="))
                {
                    //MessageBox.Show("Tiene token");
                    this.lblEstado.Text = "Recibiendo respuesta de SkyDrive";
                    var x = e.Url.AbsoluteUri.Split(new[] { "#access_token=" }, StringSplitOptions.RemoveEmptyEntries);
                    _authCode = x[1].Split(new[] { '&' })[0];
                    DialogResult = DialogResult.OK;
                    bRespondidoSkyDrive = true;
                    Close();
                }
                else
                {
                    //MessageBox.Show("No tiene token");
                    if (this.wbSkyDriveAuth.Document != null)
                    {
                        if (this.wbSkyDriveAuth.Document.Url.AbsoluteUri.StartsWith(@"res://"))
                        {
                            this.Close();
                            if (!bRespondidoSkyDrive)
                                ContraseñasSeguras.Comunes.clMensajesACliente.mensaje("No se pudo conectar con SkyDrive.\r\nPor favor, compruebe la conexión", ContraseñasSeguras.Comunes.clMensajesACliente.cTipoMensajeAviso);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp = new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al conectar con SkyDrive", "recibirRespuestaLogin", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
                exApp.verExcepcion(this);
            }
        }

        void evWindow_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Let the browser know we handled this error.
            e.Handled = true;
        }

    }
}

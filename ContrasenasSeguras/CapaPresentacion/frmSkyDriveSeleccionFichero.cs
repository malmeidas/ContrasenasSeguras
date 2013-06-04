using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ContraseñasSeguras.SkyDrive;

namespace ContraseñasSeguras.CapaPresentacion
{
    public partial class frmSkyDriveSeleccionFichero : Form
    {
        private string sFicheroSelecionado;
        private string sURIFicheroSeleccionado;
        private string sNombreFichero;
        private bool bSoloDirectorios;

        public frmSkyDriveSeleccionFichero()
        {
            InitializeComponent();
        }

        private void frmSkyDriveSeleccionFichero_Load(object sender, EventArgs e)
        {
            sFicheroSelecionado = string.Empty;
            sURIFicheroSeleccionado = string.Empty;
            sNombreFichero = string.Empty;
            if (bSoloDirectorios)
            {
                this.txtNombre.Visible = true;
                this.txtNombre.Text = "Nombre del fichero";
            }
            else
            {
                this.txtNombre.Visible = false;
            }

            try
            {
                clSkyDrive.reconectar(this);
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
                this.Close();
                return;
            }

            //if (clSkyDrive.AccessToken.Equals(string.Empty))
            //{
            //    using (var signIndlg = new frmSkyDriveLogin())
            //    {
            //        if (signIndlg.ShowDialog(this) == DialogResult.OK)
            //        {
            //            clSkyDrive.AccessToken = signIndlg.AuthCode;
            //        }
            //    }
            //}

            if (!clSkyDrive.AccessToken.Equals(string.Empty))
                clSkyDrive.getDirectoriosRaiz(this.tviCloud, bSoloDirectorios);
        }

        private void lbAceptar_Click(object sender, EventArgs e)
        {
            if (bSoloDirectorios)
            {
                if (this.txtNombre.Text == null || this.txtNombre.Text.Length == 0)
                {
                    ContraseñasSeguras.Comunes.clMensajesACliente.mensaje("Introduce un nombre de fichero");
                    return;
                }

                if (sURIFicheroSeleccionado.Equals(string.Empty))
                {
                    ContraseñasSeguras.Comunes.clMensajesACliente.mensaje("Selecciona el directorio en el que se guardará el fichero");
                    return;
                }
                sNombreFichero = this.txtNombre.Text;
            }

            this.Close();
        }

        private void lbCancelar_Click(object sender, EventArgs e)
        {
            sFicheroSelecionado = string.Empty;
            sURIFicheroSeleccionado = string.Empty;
            sNombreFichero = string.Empty;
            this.Close();
        }

        private void tviCloud_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.ImageIndex == 1)
            {
                this.lbFichero.Text = e.Node.Text;
                sFicheroSelecionado = e.Node.Text;
                sURIFicheroSeleccionado = e.Node.Name;
            }
            else
            {
                this.tviCloud.SelectedNode = e.Node;
                if (bSoloDirectorios)
                {
                    this.lbFichero.Text = e.Node.Text;
                    sFicheroSelecionado = e.Node.Text;
                    sURIFicheroSeleccionado = e.Node.Name;
                }

                if (e.Node.Nodes.Count == 0)
                    clSkyDrive.getHijosDeDirectorio(this.tviCloud, bSoloDirectorios);
                else if (e.Node.IsExpanded)
                    e.Node.Collapse();
                else
                    e.Node.Expand();
            }
        }

        public string FicheroSelecionado
        {
            get { return sFicheroSelecionado; }
            set { sFicheroSelecionado = value; }
        }

        public string URIFicheroSeleccionado
        {
            get { return sURIFicheroSeleccionado; }
            set { sURIFicheroSeleccionado = value; }
        }

        public bool SoloDirectorios
        {
            get { return bSoloDirectorios; }
            set { bSoloDirectorios = value; }
        }

        public string NombreFichero
        {
            get { return sNombreFichero; }
            set { sNombreFichero = value; }
        }

        private void tviCloud_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control ctrl1 = this.tviCloud.GetChildAtPoint(e.Location);
            TreeNode tNodo = this.tviCloud.GetNodeAt(e.Location);
            if (tNodo != null)
            {
                this.tviCloud.SelectedNode = tNodo;
                if (tNodo.ImageIndex == 1)
                {
                    sFicheroSelecionado = tNodo.Text;
                    sURIFicheroSeleccionado = tNodo.Name;
                    this.Close();
                }
                else
                {
                    if (bSoloDirectorios)
                    {
                        sFicheroSelecionado = tNodo.Text;
                        sURIFicheroSeleccionado = tNodo.Name;
                        this.Close();
                    }

                    if (tNodo.Nodes.Count == 0)
                        clSkyDrive.getHijosDeDirectorio(this.tviCloud, bSoloDirectorios);
                    else if (tNodo.IsExpanded)
                        tNodo.Collapse();
                    else
                        tNodo.Expand();
                }
            }
        }
    }
}

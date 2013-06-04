using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ContraseñasSeguras.Comunes;

namespace ContraseñasSeguras.CapaPresentacion
{
    public partial class frmContrasena : Form
    {
        private string mContrasenaActual;
        private bool mParaGuardar;
        private string mNombreFichero;

        public string NombreFichero
        {
            get { return mNombreFichero; }
            set { mNombreFichero = value; }
        }

        public bool ParaGuardar
        {
            get { return mParaGuardar; }
            set { mParaGuardar = value; }
        }

        public string ContrasenaActual
        {
            get { return mContrasenaActual; }
            set { mContrasenaActual = value; }
        }

        public frmContrasena()
        {
            InitializeComponent();
        }

        private void frmContrasena_Load(object sender, EventArgs e)
        {
            if (ParaGuardar)
            {
                this.txtConfirmacion.Text = mContrasenaActual;
                this.txtContrasena.Text = mContrasenaActual;
            }
            else
            {
                this.txtContrasena.Text = "";
                this.txtConfirmacion.Visible = false;
                this.label2.Visible = false;
                this.Text = mNombreFichero + " - Contraseña fichero";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!ParaGuardar)
                mContrasenaActual = "";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ParaGuardar)
            {
                if (this.txtContrasena.Text.Equals(this.txtConfirmacion.Text))
                {
                    mContrasenaActual = this.txtConfirmacion.Text;
                    this.Close();
                }
                else
                {
                    clMensajesACliente.mensaje("No coinciden las contraseñas introducidas", clMensajesACliente.cTipoMensajeAviso);
                }
            }
            else
            {
                mContrasenaActual = this.txtContrasena.Text;
                this.Close();
            }
        }
    }
}

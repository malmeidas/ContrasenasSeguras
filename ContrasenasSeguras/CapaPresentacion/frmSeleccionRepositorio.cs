using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContraseñasSeguras.CapaPresentacion
{
    public partial class frmSeleccionRepositorio : Form
    {
        string mRepositorio;

        public string Repositorio
        {
            get { return mRepositorio; }
            set { mRepositorio = value; }
        }

        public frmSeleccionRepositorio()
        {
            InitializeComponent();
        }

        private void btnLocal_Click(object sender, EventArgs e)
        {
            mRepositorio = ContraseñasSeguras.Comunes.clConstantes.repositorioFicheroLocal;
            this.Close();
        }

        private void btnSkyDrive_Click(object sender, EventArgs e)
        {
            mRepositorio = ContraseñasSeguras.Comunes.clConstantes.repositorioFicheroSkyDrive;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            mRepositorio = "";
            this.Close();
        }
    }
}

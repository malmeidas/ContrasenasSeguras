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
    public partial class frmRenombrar : Form
    {
        private string sNombre;

        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public frmRenombrar()
        {
            InitializeComponent();
        }

        private void frmRenombrar_Load(object sender, EventArgs e)
        {
            this.Width = this.txtNombre.Width;
            this.Height = this.txtNombre.Height;
            this.txtNombre.Top = 0;
            this.txtNombre.Left = 0;
            this.txtNombre.Text = sNombre;
        }

        private void frmRenombrar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sNombre = this.txtNombre.Text;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sNombre = this.txtNombre.Text;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenombrar_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class frmExcepciones : Form
    {
        public frmExcepciones()
        {
            InitializeComponent();
        }

        private void frmExcepciones_Load(object sender, EventArgs e)
        {
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                this.Close();
            frmPrincipal ff;
            ff = (frmPrincipal)Application.OpenForms[0];
            //ff = (frmPrincipal)this.Parent.FindForm();
            ff.accionDespuesExcepcion(this.txtTipo.Text);
        }

    }
}

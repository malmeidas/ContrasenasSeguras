using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContraseñasSeguras.Comunes
{
    static class clMensajesACliente
    {
        public const string cTipoMensajeAviso = "aviso";
        public const string cTipoMensajeExito = "exito";
        private const string cTitulo = "¡Atencion!";

        static public void mensaje(string sMensaje)
        {
            MessageBox.Show(sMensaje, cTitulo);
        }

        static public void mensaje(string sMensaje, string sTipoMensaje)
        {
            if (sTipoMensaje == cTipoMensajeAviso)
                MessageBox.Show(sMensaje, cTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (sTipoMensaje == cTipoMensajeExito)
                MessageBox.Show(sMensaje, cTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

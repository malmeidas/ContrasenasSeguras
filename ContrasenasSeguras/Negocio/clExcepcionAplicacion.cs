using System;
using System.Windows.Forms;
using ContraseñasSeguras.CapaPresentacion;

#pragma warning disable 0162, 0168

namespace ContraseñasSeguras.Negocio
{
    class clExcepcionAplicacion : ApplicationException
    {
        public const string cTipoExcFinAplicacion = "FIN"; // Se cierra la aplicación
        public const string cTipoExcFicheroIncorrecto = "FICHERO"; // Se cierra el fichero sin guardar los datos
        public const string cTipoExcError = "ERROR"; // Se informa pero no se hace nada más
        public const string cTipoExcProgramacion = "PROGRAMACION";  // Se cierra la aplicación

        private string strTipoExcepcion;
        private string strMensaje;
        private string strMetodo;

        private Exception exAplicacion;

        public clExcepcionAplicacion(string sMensaje, string sMetodo)
        {
            strMensaje = sMensaje;
            strMetodo = sMetodo;
            strTipoExcepcion = clExcepcionAplicacion.cTipoExcError;
        }

        public clExcepcionAplicacion(string sMensaje, string sMetodo, string sTipoExcepcion)
        {
            strMensaje = sMensaje;
            strMetodo = sMetodo;
            strTipoExcepcion = sTipoExcepcion;
        }

        public clExcepcionAplicacion(string sMensaje, string sMetodo, System.Exception exApp)
        {
            strMensaje = sMensaje;
            strMetodo = sMetodo;
            exAplicacion = exApp;
            strTipoExcepcion = clExcepcionAplicacion.cTipoExcError;
        }


        public clExcepcionAplicacion(string sMensaje, string sMetodo, System.Exception exApp, string sTipoExcepcion) 
        {
            strMensaje = sMensaje;
            strMetodo = sMetodo;
            exAplicacion = exApp;
            strTipoExcepcion = sTipoExcepcion;
        }
 
        // Constructor needed for serialization when exception propagates from a remoting server to the client.
        protected clExcepcionAplicacion(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {}

        public void verExcepcion(Form frmPadre)
        {
            if (this.strTipoExcepcion.Equals(cTipoExcError) || this.strTipoExcepcion.Equals(cTipoExcFicheroIncorrecto))
            {
                if (ContraseñasSeguras.Comunes.clConstantes.bEnPruebas)
                    verMensajeDepuracion();
                else
                    verMensajeACliente();
            }
            else if (this.strTipoExcepcion.Equals(cTipoExcFinAplicacion))
            {
                if (ContraseñasSeguras.Comunes.clConstantes.bEnPruebas)
                    verMensajeDepuracion();
                else
                    verMensajeACliente();
                Application.Exit();
            }
            else if (this.strTipoExcepcion.Equals(cTipoExcProgramacion))
            {
                verFormularioExcepcion(frmPadre);
            }
            else
            {
                verFormularioExcepcion(frmPadre);
            }
        }

        private void verFormularioExcepcion(Form frmPadre)
        {
            frmExcepciones mfrm = new frmExcepciones();
            mfrm.Controls.Find("txtTipo", true)[0].Text = strTipoExcepcion;
            mfrm.Controls.Find("txtMetodo", true)[0].Text = strMetodo;
            if (exAplicacion != null)
                mfrm.Controls.Find("txtMensaje", true)[0].Text = strMensaje + "(" + exAplicacion.Message + ")";
            else
                mfrm.Controls.Find("txtMensaje", true)[0].Text = strMensaje;
            mfrm.Controls.Find("txtTraza", true)[0].Text = this.StackTrace;
            mfrm.ShowDialog(frmPadre);
            mfrm.Close();
        }

        private void verMensajeACliente()
        {
            string str;
            if (exAplicacion == null)
            {
                str = "Se ha producido el error \n" + strMensaje;
            }
            else
            {
                str = strMensaje + "\n" + exAplicacion.Message;
            }
            MessageBox.Show(str, "¡Atencion!",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void verMensajeDepuracion()
        {
            string str;
            if (exAplicacion == null)
            {
                str = "Se ha producido el error de tipo (" + strTipoExcepcion + ")\n" + strMensaje + "\n" + strMetodo;
            }
            else
            {
                str = strMensaje + "\n" + exAplicacion.Message + "\n" + strMetodo;
            }
            MessageBox.Show(str, "¡Atencion!",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
    }

}

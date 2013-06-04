using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using ContraseñasSeguras.Comunes;
using ContraseñasSeguras.Negocio;
using ContraseñasSeguras.SkyDrive;
using System.IO;

#pragma warning disable 0162, 0168

namespace ContraseñasSeguras.CapaPresentacion
{
    public partial class frmPrincipal : Form
    {

        private clGestoraXML clXML;
        private bool bDatosFicheroCambiados;
        private bool bDatosRegistroCambiados;
        private bool bFicheroCargado;
        private bool bFicheroInicioCargado;
        private bool bCierreControlado;
        private List<TreeNode> lResultadoBusqueda = new List<TreeNode>();
        private string sCadenaBuscada;
        private int iNodoResultadoBusquedaMostrado;

        private const int iPosArbolX = 1;
        private const int iPosArbolY = 29;
        private const int iPosDatosX = 9;
        private const int iPosDatosY = 160;

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        private void iniciarFormulario()
        {
            this.KeyPreview = true;
            bDatosFicheroCambiados = false;
            bDatosRegistroCambiados = false;
            bFicheroCargado = false;
            bCierreControlado = false;

            this.tviXML.Nodes.Clear();
            this.tviXML.HotTracking = true;
            this.tstBH_Favoritos.DropDownItems.Clear();
            this.tviXML.Refresh();
            this.txtComentarios.Text = "";
            this.txtContrasena.Text = "";
            this.txtRuta.Text = "";
            this.txtUsuario.Text = "";
            this.tstBE_NElementos.Text = "";

            this.tstBH_CadenaBusqueda.Items.Clear();

            controlarVisualizacionControles(null);
        }

        public frmPrincipal(string RutaFichero)
        {
            InitializeComponent();

            clXML = new clGestoraXML();

            iniciarFormulario();

            if (RutaFichero.Length > 0)
            {
                try
                {
                    abrirFichero(RutaFichero, "", null, "");
                    bFicheroInicioCargado = true;
                }
                catch (clExcepcionAplicacion ex)
                {
                    ex.verExcepcion(this);
                    return;
                }
            }
            bFicheroInicioCargado = false;
        }

        private void añadirFicheroAbiertoASetting(string sNombre, string sRuta)
        {
            //clUtilidades.addFicheroAbiertoASettings(sNombre, sRuta);
            //cargarMenusFicherosAbiertos();
            Application.UserAppDataRegistry.SetValue(clConstantes.sUltimoFicheroAbierto, sRuta);
            Application.UserAppDataRegistry.SetValue(clConstantes.sUltimoFicheroAbiertoNombre, sNombre);
        }

        private void cargarMenusFicherosAbiertos()
        {
            //Hashtable htFicherosAbiertosLocal = new Hashtable();
            //Hashtable htFicherosAbiertosSkyDrive = new Hashtable();
            //htFicherosAbiertosLocal = Properties.Settings.Default.FicherosAbiertosLocal;
            //htFicherosAbiertosSkyDrive = Properties.Settings.Default.FicherosAbiertosSkyDrive;
            //if (htFicherosAbiertosLocal == null) htFicherosAbiertosLocal = new Hashtable();
            //if (htFicherosAbiertosSkyDrive == null) htFicherosAbiertosSkyDrive = new Hashtable();
            //ToolStripMenuItem[] itLocal = new ToolStripMenuItem[htFicherosAbiertosLocal.Count];
            //ToolStripMenuItem[] itSkyDrive = new ToolStripMenuItem[htFicherosAbiertosSkyDrive.Count];
            //int iLocal = 0;
            //int iSkyDrive = 0;

            //foreach (DictionaryEntry fichero in htFicherosAbiertosLocal)
            //{
            //    itLocal[iLocal] = new ToolStripMenuItem();
            //    itLocal[iLocal].Name = fichero.Key.ToString();
            //    itLocal[iLocal].Tag = "Abrir fichero " + fichero.Key.ToString();
            //    itLocal[iLocal].Text = fichero.Value.ToString();
            //    itLocal[iLocal].Visible = true;
            //    itLocal[iLocal].Click += new EventHandler(miFicherosAbiertosLocalClickHandler);
            //}

            //this.tstBH_Abrir_SB_Local.DropDownItems.AddRange(itLocal);

            //foreach (DictionaryEntry fichero in htFicherosAbiertosSkyDrive)
            //{
            //    itSkyDrive[iSkyDrive] = new ToolStripMenuItem();
            //    itSkyDrive[iSkyDrive].Name = fichero.Key.ToString();
            //    itSkyDrive[iSkyDrive].Tag = "Abrir fichero " + fichero.Key.ToString();
            //    itSkyDrive[iSkyDrive].Text = fichero.Value.ToString();
            //    itSkyDrive[iSkyDrive].Visible = true;
            //    itSkyDrive[iSkyDrive].Click += new EventHandler(miFicherosAbiertosSkyDriveClickHandler);
            //}

            //this.tstBH_Abrir_SB_SkyDrive.DropDownItems.AddRange(itLocal);
        }

        private void miFicherosAbiertosLocalClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            // Take some action based on the data in clickedItem
            MessageBox.Show("Local : " + clickedItem.Text + " en " + clickedItem.Name);
        }

        private void miFicherosAbiertosSkyDriveClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            // Take some action based on the data in clickedItem
            MessageBox.Show("SkyDrive : " + clickedItem.Text + " en " + clickedItem.Name);
        }

        private void cargarFavoritos()
        {
            this.tstBH_Favoritos.DropDownItems.Clear();
            List<clFavorito> cFavoritos = clXML.getFavoritos();
            if (cFavoritos == null || cFavoritos.Count() == 0) return;

            ToolStripItem[] items = new ToolStripItem[cFavoritos.Count() + 1];
            bool bSeparador = false;
            int j;

            for (int i = 0; i < cFavoritos.Count() + 1; i++)
            {
                if (!bSeparador && cFavoritos[i].Tipo.Equals(clConstantes.TipoFavoritoUltimoCopiado))
                {
                    items[i] = new ToolStripSeparator();
                    bSeparador = true;
                    continue;
                }
                items[i] = new ToolStripMenuItem();
                items[i].Name = "Favorito" + i.ToString();
                if (bSeparador)
                    j = i - 1;
                else
                    j = i;
                items[i].Text = cFavoritos[j].Texto;
                items[i].Tag = cFavoritos[j].ID;
                if (cFavoritos[j].Tipo.Equals(clConstantes.TipoFavoritoNVeces))
                   items[i].Image = Properties.Resources.List_NumberedHS;
                else
                   items[i].Image = Properties.Resources.clock;
                items[i].Click += new EventHandler(btnFavoritos_Click);
            }

            this.tstBH_Favoritos.DropDownItems.AddRange(items);
        }

        private void btnFavoritos_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (clickedItem == null) return;
            clXML.NodoSeleccionado = clXML.findNodoDocXML(clickedItem.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            if (clXML.NodoSeleccionado == null)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo de XML no encontrado", "frmPrincipal.btnFavoritos_Click", clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }

            clXML.findNodoTreeView(this.tviXML, clickedItem.Tag.ToString());
            if (this.tviXML.SelectedNode == null)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo de árbol no encontrado", "frmPrincipal.btnFavoritos_Click", clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }
            cargarDatosNodo(clXML.NodoSeleccionado);
            this.tviXML.Refresh();
        }

        private void btnCopiarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetData(DataFormats.Text, (Object)this.txtUsuario.Text);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnCopiarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                clXML.estadisticasAccionCopiado(this.tviXML.SelectedNode.Tag.ToString());
                bDatosRegistroCambiados = true;
                cargarFavoritos();
                Clipboard.SetData(DataFormats.Text, (Object)this.txtContrasena.Text);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnCopiarURL_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetData(DataFormats.Text, (Object)this.txtRuta.Text);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnLanzarURL_Click(object sender, EventArgs e)
        {
            if (this.txtRuta.Text != null && this.txtRuta.Text.Length != 0)
                System.Diagnostics.Process.Start("IEXPLORE.EXE", this.txtRuta.Text);
        }

        private void tstBH_Nuevo_Click(object sender, EventArgs e)
        {
            controlarCambiosEnFichero();
            try
            {
                clXML.iniciarDocumentoXML("", "");
                clXML.unirXMLaTreeView(this.tviXML);
                bDatosFicheroCambiados = true;
                this.clXML.NodoSeleccionado = this.clXML.DocXML.ChildNodes[1].ChildNodes[1]; // El 0 es el de declaracion del XML y el 1 la raiz y luego DatosFichero y ya Contraseñas
                this.tviXML.SelectedNode = this.tviXML.Nodes[0];
                mnuRenombrar_Click(null, null);
                bFicheroCargado = true;
                controlarVisualizacionControles(this.clXML.NodoSeleccionado);
                Application.DoEvents();
                cargarFavoritos();
            }
            catch (clExcepcionAplicacion ex)
            {
                ex.verExcepcion(this);
                return;
            }
            this.tviXML.Refresh();   
        }

        private void abrirFichero(string sRutaFichero, string sNombreFichero, MemoryStream stContenidoFicheroEncriptado, string sContenidoFicheroDesencriptado)
        {
            this.tstBE_NElementos.Text = "Abriendo fichero " + sNombreFichero;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (sContenidoFicheroDesencriptado.Length != 0)
                    clXML.iniciarDocumentoXML(null, sContenidoFicheroDesencriptado, sRutaFichero, sNombreFichero);
                else if (stContenidoFicheroEncriptado != null)
                    clXML.iniciarDocumentoXML(stContenidoFicheroEncriptado, "", sRutaFichero, sNombreFichero);
                else
                    clXML.iniciarDocumentoXML(sRutaFichero, sNombreFichero);

                string sContrasenaIntroducida = abrirFormularioContrasena(false);
                if (!sContrasenaIntroducida.Equals(clXML.getContrasenaFichero()))
                {
                    clMensajesACliente.mensaje("La contraseña introducida no coindice", clMensajesACliente.cTipoMensajeAviso);
                    if (!bFicheroCargado)
                    {
                        iniciarFormulario();
                        clXML.iniciar();
                        this.tstBE_NElementos.Text = "";
                        Cursor.Current = Cursors.Default;
                        Application.DoEvents();
                        return;
                    }
                }
                else
                {
                    iniciarFormulario();
                    clXML.unirXMLaTreeView(this.tviXML);
                    bDatosFicheroCambiados = false;
                }
            }
            catch (clExcepcionAplicacion ex)
            {
                this.tstBE_NElementos.Text = "";
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
                ex.verExcepcion(this);
                return;
            }
            Application.DoEvents();
            clXML.NodoSeleccionado = clXML.findNodoDocXML("0", clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            clXML.findNodoTreeView(this.tviXML, "0");
            this.tviXML.Refresh();
            bFicheroCargado = true;
            controlarVisualizacionControles(this.clXML.NodoSeleccionado);
            Application.DoEvents();
            cargarFavoritos();
            añadirFicheroAbiertoASetting(sNombreFichero, sRutaFichero);
            this.Text = "Contraseñas Seguras - " + clXML.Repositorio.ToUpper() + " - " + clXML.RutaFicheroXMLNombre;
            this.tstBE_NElementos.Text = "";
            Cursor.Current = Cursors.Default;
            Application.DoEvents();
        }

        private void AbrirLocal()
        {
            controlarCambiosEnFichero();

            OpenFileDialog ficheroSeleccionado = new OpenFileDialog();
            ficheroSeleccionado.Title = "Selecciona el fichero con los datos";
            ficheroSeleccionado.Filter = clConstantes.SeleccionExtensionFicheros;

            Application.DoEvents();
            if (ficheroSeleccionado.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    abrirFichero(ficheroSeleccionado.FileName.ToString(), "", null, "");
                }
                catch (clExcepcionAplicacion ex)
                {
                    ex.verExcepcion(this);
                    return;
                }
            }
        }

        private void abrirSkyDrive()
        {
            try
            {
                clSkyDrive.reconectar(this);
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
                return;
            }

            //if (clSkyDrive.AccessToken.Equals(string.Empty))
            //{
            //    string access_token = string.Empty;
            //    using (var signIndlg = new frmSkyDriveLogin())
            //    {
            //        if (signIndlg.ShowDialog(this) == DialogResult.OK)
            //        {
            //            clSkyDrive.AccessToken = signIndlg.AuthCode;
            //        }
            //        else
            //            return;
            //    }
            //}

            if (!clSkyDrive.AccessToken.Equals(string.Empty))
            {
                frmSkyDriveSeleccionFichero frmSel = new frmSkyDriveSeleccionFichero();
                frmSel.SoloDirectorios = false;
                frmSel.ShowDialog(this);
                if (!frmSel.FicheroSelecionado.Equals(string.Empty))
                {
                    //MessageBox.Show("Ahora se abre " + frmSel.FicheroSelecionado);
                    //Stream st = clSkyDrive.descargarFichero(frmSel.URIFicheroSeleccionado);
                    clXML.RutaFicheroXMLNombre = frmSel.FicheroSelecionado;
                    try
                    {
                        if (clConstantes.bNoEncriptar)
                        {
                            Application.DoEvents();
                            string str = clSkyDrive.descargarFicheroAString(frmSel.URIFicheroSeleccionado);
                            Application.DoEvents();
                            if (str == null)
                                throw new clExcepcionAplicacion("Error al descargar el fichero", "frmPrincipal.tstBH_SkyDrive_Click", clExcepcionAplicacion.cTipoExcError);
                            abrirFichero(frmSel.URIFicheroSeleccionado, frmSel.FicheroSelecionado, null, str);
                        }
                        else
                        {
                            Application.DoEvents();
                            MemoryStream str = clSkyDrive.descargarFicheroAStream(frmSel.URIFicheroSeleccionado);
                            Application.DoEvents();
                            if (str == null)
                                throw new clExcepcionAplicacion("Error al descargar el fichero", "frmPrincipal.tstBH_SkyDrive_Click", clExcepcionAplicacion.cTipoExcError);
                            abrirFichero(frmSel.URIFicheroSeleccionado, frmSel.FicheroSelecionado, str, "");
                        }
                    }
                    catch (clExcepcionAplicacion ex)
                    {
                        ex.verExcepcion(this);
                        return;
                    }
                }
            }
            else
            {
                clMensajesACliente.mensaje("Error al logarse en SkyDrive. Inténtelo de nuevo", clMensajesACliente.cTipoMensajeAviso);
            }
        }

        private void tstBH_Abrir_Click(object sender, EventArgs e)
        {
            AbrirLocal();
        }

        private void tstBH_SkyDrive_Click(object sender, EventArgs e)
        {
            abrirSkyDrive();
        }
        
        private void tstBH_Abrir_SB_ButtonClick(object sender, EventArgs e)
        {
            AbrirLocal();
        }

        private void tstBH_Abrir_SB_Local_Click(object sender, EventArgs e)
        {
            AbrirLocal();
        }

        private void tstBH_Abrir_SB_SkyDrive_Click(object sender, EventArgs e)
        {
            abrirSkyDrive();
        }

        private void tstBH_Guardar_SB_ButtonClick(object sender, EventArgs e)
        {
            guardar(clConstantes.repositorioFicheroLocal);
        }

        private void tstBH_Guardar_SB_Local_Click(object sender, EventArgs e)
        {
            guardar(clConstantes.repositorioFicheroLocal);
        }

        private void tstBH_Guardar_SB_SkyDrive_Click(object sender, EventArgs e)
        {
            guardar(clConstantes.repositorioFicheroSkyDrive);
        }

        private void guardar(string sRepositorio)
        {
            try
            {
                controlarCambiosEnRegistro();

                if (this.bDatosFicheroCambiados)
                {
                    string mContrasenaFichero = clXML.getContrasenaFichero();
                    if (mContrasenaFichero == null || mContrasenaFichero.Length == 0)
                    {
                        if (MessageBox.Show("El fichero no tiene contraseña. ¿La quiere crear ahora?", "¡Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            abrirFormularioContrasena(true);
                        }
                    }

                    Application.DoEvents();

                    if (sRepositorio.Equals(clConstantes.repositorioFicheroSkyDrive))
                    {
                        Application.DoEvents();
                        clSkyDrive.reconectar(this);
                    }

                    if (clXML.guardarDocumentoXML(sRepositorio, this))
                    {
                        Application.DoEvents();
                        añadirFicheroAbiertoASetting(clXML.RutaFicheroXMLNombre, clXML.RutaFicheroXML);
                        this.bDatosFicheroCambiados = false;
                        this.tstBE_NElementos.Text = "Documento guardado";
                    }
                }
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            catch (Exception ex)
            {
                clExcepcionAplicacion exApp2 = new clExcepcionAplicacion("Error al grabar", "frmPrincipal.guardar", ex, clExcepcionAplicacion.cTipoExcError);
                exApp2.verExcepcion(this);
            }
        }

        private void tstBH_Guardar_Click(object sender, EventArgs e)
        {
            string sRepositorio = "";
            if (clXML.RutaFicheroXML.Length == 0)
            {
                Application.DoEvents();
                frmSeleccionRepositorio frm = new frmSeleccionRepositorio();
                frm.Repositorio = "";
                frm.ShowDialog(this);
                if (frm.Repositorio.Length == 0)
                {
                    return;
                }
                sRepositorio = frm.Repositorio;
            }

            Application.DoEvents();
            guardar(sRepositorio);
            Application.DoEvents();
            
            //if (clXML.RutaFicheroXML.StartsWith(clConstantes.inicioRutaSkyDrive))
            //    guardar(clConstantes.repositorioFicheroSkyDrive);
            //else
            //    guardar(clConstantes.repositorioFicheroLocal);
        }

        private void tstBH_Carpeta_Click(object sender, EventArgs e)
        {
            if (this.tviXML.SelectedNode == null)
            {
                clMensajesACliente.mensaje("Seleccione un elemento", clMensajesACliente.cTipoMensajeAviso);
                return;
            }

            Application.DoEvents();
            string strIDCreado;
            // guargo los datos del nodo actual
            controlarCambiosEnRegistro();

            try
            {
                Application.DoEvents();
                if (this.clXML.NodoSeleccionado.Name == clConstantes.tipoNodoXMLHoja)
                {
                    strIDCreado = clXML.addCarpetaContraseñaAXML(this.clXML.NodoSeleccionado.ParentNode, clConstantes.NuevaCarpeta);
                    clGestoraXML_Orden.SortXml(this.clXML.NodoSeleccionado.ParentNode);
                }
                else
                {
                    strIDCreado = clXML.addCarpetaContraseñaAXML(this.clXML.NodoSeleccionado, clConstantes.NuevaCarpeta);
                    clGestoraXML_Orden.SortXml(this.clXML.NodoSeleccionado);
                }

                clXML.unirXMLaTreeView(this.tviXML);
                clXML.findNodoTreeView(this.tviXML, strIDCreado);
                if (this.tviXML.SelectedNode == null)
                {
                    clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo no encontrado en el arbol", "frmPrincipal.tstBH_Contrasena_Click", clExcepcionAplicacion.cTipoExcProgramacion);
                    exApp.verExcepcion(this);
                }

                this.tviXML.SelectedNode.Parent.ExpandAll();

                this.clXML.NodoSeleccionado = clXML.findNodoDocXML(strIDCreado, clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                if (this.clXML.NodoSeleccionado == null)
                {
                    clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo no encontrado en el XML", "frmPrincipal.tstBH_Contrasena_Click", clExcepcionAplicacion.cTipoExcProgramacion);
                    exApp.verExcepcion(this);
                }

                cargarDatosNodo(this.clXML.NodoSeleccionado);
                controlarVisualizacionControles(this.clXML.NodoSeleccionado);
                mnuRenombrar_Click(null, null);
                bDatosFicheroCambiados = true;
                cargarFavoritos();
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            Application.DoEvents();
        }

        private void tstBH_Contrasena_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (this.tviXML.SelectedNode == null)
            {
                clMensajesACliente.mensaje("Seleccione un elemento", clMensajesACliente.cTipoMensajeAviso);
                return;
            }

            string strIDCreado;
            // guargo los datos del nodo actual
            controlarCambiosEnRegistro();

            Application.DoEvents();
            try
            {
                if (this.clXML.NodoSeleccionado.Name == clConstantes.tipoNodoXMLHoja)
                {
                    strIDCreado = clXML.addHojaContraseñaAXML(this.clXML.NodoSeleccionado.ParentNode, clConstantes.NuevaContraseña);
                    clGestoraXML_Orden.SortXml(this.clXML.NodoSeleccionado.ParentNode);
                }
                else
                {
                    strIDCreado = clXML.addHojaContraseñaAXML(this.clXML.NodoSeleccionado, clConstantes.NuevaContraseña);
                    clGestoraXML_Orden.SortXml(this.clXML.NodoSeleccionado);
                }
                clXML.unirXMLaTreeView(this.tviXML);
                clXML.findNodoTreeView(this.tviXML, strIDCreado);
                if (this.tviXML.SelectedNode == null)
                {
                    clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo no encontrado en el arbol", "frmPrincipal.tstBH_Contrasena_Click", clExcepcionAplicacion.cTipoExcProgramacion);
                    exApp.verExcepcion(this);
                }

                //this.tviXML.SelectedNode.Parent.ExpandAll();

                this.clXML.NodoSeleccionado = clXML.findNodoDocXML(strIDCreado, clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                if (this.clXML.NodoSeleccionado == null)
                {
                    clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo no encontrado en el XML", "frmPrincipal.tstBH_Contrasena_Click", clExcepcionAplicacion.cTipoExcProgramacion);
                    exApp.verExcepcion(this);
                }

                cargarDatosNodo(this.clXML.NodoSeleccionado);
                controlarVisualizacionControles(this.clXML.NodoSeleccionado);
                mnuRenombrar_Click(null, null);
                bDatosFicheroCambiados = true;
                this.tviXML.Refresh();
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            Application.DoEvents();
            cargarFavoritos();
        }

        private void tstBH_ContrasenaFichero_Click(object sender, EventArgs e)
        {
            abrirFormularioContrasena(true);
        }

        private void tstBH_Cortar_Click(object sender, EventArgs e)
        {
            if (this.tviXML.SelectedNode.Tag.ToString().Equals("0"))
            {
                clMensajesACliente.mensaje("No se puede mover la raiz del árbol", clMensajesACliente.cTipoMensajeAviso);
                return;
            }

            clXML.NodoSeleccionadoClipboard = this.tviXML.SelectedNode;
            if (clXML.NodoSeleccionadoClipboard == null) return;
            clXML.AccionClipboard = clConstantes.AccionClipboardCortar;
        }

        private void tstBH_Copiar_Click(object sender, EventArgs e)
        {
            if (this.tviXML.SelectedNode.Tag.ToString().Equals("0"))
            {
                clMensajesACliente.mensaje("No se puede copiar la raiz del árbol", clMensajesACliente.cTipoMensajeAviso);
                return;
            }

            clXML.NodoSeleccionadoClipboard = this.tviXML.SelectedNode;
            if (clXML.NodoSeleccionadoClipboard == null) return;
            clXML.AccionClipboard = clConstantes.AccionClipboardCopiar;
        }

        private void tstBH_Pegar_Click(object sender, EventArgs e)
        {
            TreeNode tNodo = this.tviXML.SelectedNode;
            if (tNodo == null) return;
            try
            {
                controlarCambiosEnRegistro();
                clXML.ejecutarPegar(tNodo, this.tviXML);
                //clXML.unirXMLaTreeView(this.tviXML);
                bDatosFicheroCambiados = true;
                //this.tviXML.Refresh();
                //clXML.findNodoTreeView(this.tviXML, tNodo.Tag.ToString());
                controlarVisualizacionControles(clXML.NodoSeleccionado);
                //this.tviXML.SelectedNode.ExpandAll();

            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            finally
            {
                clXML.AccionClipboard = "";
            }
            cargarFavoritos();
        }

        private void tstBH_Eliminar_Click(object sender, EventArgs e)
        {
            if (this.tviXML.SelectedNode == null) return;

            if (this.tviXML.SelectedNode.Tag.ToString().Equals("0"))
            {
                clMensajesACliente.mensaje("No se puede eliminar la raiz del árbol", clMensajesACliente.cTipoMensajeAviso);
                return;
            }

            XmlNode xNodo = clXML.findNodoDocXML(this.tviXML.SelectedNode.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            if (xNodo != null)
            {
                if (MessageBox.Show("¿Está seguro de que quiere borrarlo?", "¡Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (xNodo.HasChildNodes)
                    {
                        if (MessageBox.Show("La carpeta contiene contraseñas. \n¿Está seguro de que quiere borrarlo?", "¡Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            clXML.eliminarNodo(this.tviXML.SelectedNode, this.tviXML);
                            bDatosFicheroCambiados = true;
                            //xNodo.RemoveAll();
                            //clXML.unirXMLaTreeView(this.tviXML);
                            this.tviXML.Refresh();
                        }
                    }
                    else
                    {
                        clXML.eliminarNodo(this.tviXML.SelectedNode, this.tviXML);
                        bDatosFicheroCambiados = true;
                        //xNodo.RemoveAll();
                        //clXML.unirXMLaTreeView(this.tviXML);
                        this.tviXML.Refresh();
                        //xNodo.RemoveAll();
                        //clXML.unirXMLaTreeView(this.tviXML);
                        //this.tviXML.Refresh();
                    }
                }
            }
            cargarFavoritos();
        }

        private void cargarDatosNodo(XmlNode xNode)
        {
            if (xNode == null) return;

            controlarVisualizacionControles(xNode);

            try
            {
                if (xNode.Name != clConstantes.tipoNodoXMLHoja)
                {
                    this.txtComentarios.Text = xNode.Attributes[clConstantes.atXMLComentarios].Value.ToString();
                }
                else
                {
                    this.txtUsuario.Text = xNode.Attributes[clConstantes.atXMLUsuario].Value.ToString();
                    this.txtContrasena.Text = xNode.Attributes[clConstantes.atXMLContraseña].Value.ToString();
                    this.txtRuta.Text = xNode.Attributes[clConstantes.atXMLRuta].Value.ToString();
                    this.txtComentarios.Text = xNode.Attributes[clConstantes.atXMLComentarios].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Error en el fichero de datos", "frmPrincipal.cargarDatosNodo", ex, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
                exApp.verExcepcion(this);
            }
        }

        private void controlarVisualizacionControles(XmlNode xNodo)
        {
            if (bFicheroCargado)
            {
                this.tstBH_Guardar.Enabled = true;
                this.tstBH_ContrasenaFichero.Enabled = true;
                this.tstBH_Favoritos.Enabled = true;
                this.tstBH_Copiar.Enabled = true;
                this.tstBH_Cortar.Enabled = true;
                this.tstBH_Eliminar.Enabled = true;
                this.tstBH_Pegar.Enabled = true;
                this.tstBH_Buscar.Enabled = true;
                this.tstBH_CadenaBusqueda.Enabled = true;
                this.tstBH_Carpeta.Enabled = true;
                this.tstBH_Configuracion.Enabled = true;
                this.tstBH_Contrasena.Enabled = true;
            }
            else
            {
                this.tstBH_Guardar.Enabled = false;
                this.tstBH_ContrasenaFichero.Enabled = false;
                this.tstBH_Favoritos.Enabled = false;
                this.tstBH_Copiar.Enabled = false;
                this.tstBH_Cortar.Enabled = false;
                this.tstBH_Eliminar.Enabled = false;
                this.tstBH_Pegar.Enabled = false;
                this.tstBH_Buscar.Enabled = false;
                this.tstBH_CadenaBusqueda.Enabled = false;
                this.tstBH_Carpeta.Enabled = false;
                this.tstBH_Configuracion.Enabled = false;
                this.tstBH_Contrasena.Enabled = false;
            }


            bool bEsRama;
            if (xNodo == null)
                bEsRama = true;
            else
                bEsRama = xNodo.Name != clConstantes.tipoNodoXMLHoja;

            if (bEsRama)
            {
                this.labComentarios.Visible = false;
                this.labContrasena.Visible = false;
                this.labRuta.Visible = false;
                this.labUsuario.Visible = false;
                this.txtContrasena.Visible = false;
                this.txtRuta.Visible = false;
                this.txtUsuario.Visible = false;
                this.btnCopiarContrasena.Visible = false;
                this.btnCopiarURL.Visible = false;
                this.btnCopiarUsuario.Visible = false;
                this.btnLanzarURL.Visible = false;
                this.lbF4.Visible = false;
                this.lbF5.Visible = false;
                this.lbF6.Visible = false;
                this.lbF7.Visible = false;

                this.txtComentarios.TabIndex = 0;
                
                //this.tstBH_Copiar.Enabled = false;
                //this.tstBH_Cortar.Enabled = false;
                //this.tstBH_Pegar.Enabled = true;

                if (xNodo != null)
                    this.tstBE_NElementos.Text = "Contiene " + xNodo.ChildNodes.Count.ToString() + " hijos";
            }
            else
            {
                this.labComentarios.Visible = true;
                this.labContrasena.Visible = true;
                this.labRuta.Visible = true;
                this.labUsuario.Visible = true;
                this.txtContrasena.Visible = true;
                this.txtRuta.Visible = true;
                this.txtUsuario.Visible = true;
                this.btnCopiarContrasena.Visible = true;
                this.btnCopiarURL.Visible = true;
                this.btnCopiarUsuario.Visible = true;
                this.btnLanzarURL.Visible = true;
                this.lbF4.Visible = true;
                this.lbF5.Visible = true;
                this.lbF6.Visible = true;
                this.lbF7.Visible = true;

                this.txtUsuario.TabIndex = 0;
                this.txtContrasena.TabIndex = 1;
                this.txtRuta.TabIndex = 2;
                this.txtComentarios.TabIndex = 7;
                this.btnCopiarUsuario.TabIndex = 3;
                this.btnCopiarContrasena.TabIndex = 4;
                this.btnCopiarURL.TabIndex = 5;
                this.btnLanzarURL.TabIndex = 6;

                this.tstBH_Copiar.Enabled = true;
                this.tstBH_Cortar.Enabled = true;
                this.tstBH_Pegar.Enabled = true;

                if (xNodo != null)
                    this.tstBE_NElementos.Text = "Modificado el " + xNodo.Attributes[clConstantes.atXMLUltimaModificacion].Value;
            }

            frmPrincipal_Resize(null, null);
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            this.bDatosRegistroCambiados = true;
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            this.bDatosRegistroCambiados = true;
        }

        private void txtRuta_TextChanged(object sender, EventArgs e)
        {
            this.bDatosRegistroCambiados = true;
        }

        private void txtComentarios_TextChanged(object sender, EventArgs e)
        {
            this.bDatosRegistroCambiados = true;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            controlarCambiosEnRegistro();
        }

        private void txtContrasena_Leave(object sender, EventArgs e)
        {
            controlarCambiosEnRegistro();
        }

        private void txtRuta_Leave(object sender, EventArgs e)
        {
            controlarCambiosEnRegistro();
        }

        private void txtComentarios_Leave(object sender, EventArgs e)
        {
            controlarCambiosEnRegistro();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            clSkyDrive.inicializar();
        }

        private void frmPrincipal_Shown(object sender, EventArgs e)
        {

            if (bFicheroInicioCargado) return;
            string sUltimoAbierto = string.Empty;
            string sUltimoAbiertoNombre = string.Empty;


            try
            {
                sUltimoAbierto = (string)Application.UserAppDataRegistry.GetValue(clConstantes.sUltimoFicheroAbierto);
                sUltimoAbiertoNombre = (string)Application.UserAppDataRegistry.GetValue(clConstantes.sUltimoFicheroAbiertoNombre);
                //sUltimoAbierto = "https://apis.live.net/v5.0/file.f5bed81998796d61.F5BED81998796D61!8784/content/";
                //sUltimoAbierto = "https://apis.live.net/v5.0/file.f5bed81998796d61.F5BED81998796D61!8785/content/";
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (sUltimoAbierto != null && sUltimoAbierto.Length > 0)
                {
                    Application.DoEvents();
                    if (sUltimoAbierto.StartsWith(clConstantes.inicioRutaSkyDrive))
                    {
                        if (clSkyDrive.AccessToken.Equals(string.Empty))
                        {
                            string access_token = string.Empty;
                            using (var signIndlg = new frmSkyDriveLogin())
                            {
                                if (signIndlg.ShowDialog(this) == DialogResult.OK)
                                {
                                    clSkyDrive.AccessToken = signIndlg.AuthCode;
                                }
                                else
                                {
                                    bFicheroInicioCargado = false;
                                    return;
                                }
                            }
                        }

                        if (clSkyDrive.AccessToken.Equals(string.Empty))
                        {
                            clMensajesACliente.mensaje("No se ha autorizado correctamente en SkyDrive", clMensajesACliente.cTipoMensajeAviso);
                        }
                        else
                        {
                            this.tstBE_NElementos.Text = "Descargando el fichero";
                            try
                            {
                                if (clConstantes.bNoEncriptar)
                                {
                                    Application.DoEvents();
                                    string str = clSkyDrive.descargarFicheroAString(sUltimoAbierto);
                                    Application.DoEvents();
                                    if (str == null)
                                        throw new clExcepcionAplicacion("Error al descargar el fichero", "frmPrincipal.Load", clExcepcionAplicacion.cTipoExcError);
                                    abrirFichero(sUltimoAbierto, sUltimoAbiertoNombre, null, str);
                                }
                                else
                                {
                                    Application.DoEvents();
                                    MemoryStream str = clSkyDrive.descargarFicheroAStream(sUltimoAbierto);
                                    //string contenido = clSkyDrive.descargarFicheroAString(sUltimoAbierto);
                                    //MemoryStream str = clUtilidades.parseStringToMemoryStream(contenido);
                                    Application.DoEvents();
                                    if (str == null)
                                        throw new clExcepcionAplicacion("Error al descargar el fichero", "frmPrincipal.Load", clExcepcionAplicacion.cTipoExcError);
                                    abrirFichero(sUltimoAbierto, sUltimoAbiertoNombre, str, "");
                                }
                            }
                            catch (clExcepcionAplicacion ex)
                            {
                                ex.verExcepcion(this);
                                return;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            abrirFichero(sUltimoAbierto, sUltimoAbiertoNombre, null, "");
                        }
                        catch (clExcepcionAplicacion ex)
                        {
                            ex.verExcepcion(this);
                            return;
                        }
                    }
                    Application.DoEvents();
                }
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            catch (Exception ex)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Error al cargar el formulario", "frmPrincipal_Shown", ex, clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bCierreControlado) return;

            this.tstBE_NElementos.Text = "Comprobando cambios en el fichero";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            controlarCambiosEnFichero();

            Cursor.Current = Cursors.Default;
            Application.DoEvents();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.U))
            {
                this.txtUsuario.Focus();
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.C))
            {
                this.txtContrasena.Focus();
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.R))
            {
                this.txtRuta.Focus();
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.O))
            {
                this.txtComentarios.Focus();
                return true;
            }
            else if (keyData == (Keys.F4))
            {
                this.btnCopiarUsuario_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.F5))
            {
                this.btnCopiarContrasena_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.F6))
            {
                this.btnCopiarURL_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.F7))
            {
                this.btnLanzarURL_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.F4))
            {
                this.btnCerrar_Click(null, null);
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                tstBH_Nuevo_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                if (bFicheroCargado) tstBH_Guardar_Click(null, null);
            }
            else if (e.Alt && e.KeyCode == Keys.F4)
            {
                btnCerrar_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                tstBH_Abrir_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                if (bFicheroCargado) tstBH_ContrasenaFichero_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (bFicheroCargado) tstBH_Carpeta_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                if (bFicheroCargado) tstBH_Contrasena_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (bFicheroCargado) this.mnuRenombrar_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (bFicheroCargado) this.tstBH_Favoritos.ShowDropDown();
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (bFicheroCargado) this.btnCopiarUsuario_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (bFicheroCargado) this.btnCopiarContrasena_Click(null, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (bFicheroCargado) this.btnCopiarURL_Click(null, null);
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (bFicheroCargado) this.btnLanzarURL_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.B)
            {
                if (bFicheroCargado) this.tstBH_CadenaBusqueda.Focus();
            }
        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.niIcono.Visible = true;
                this.niIcono.ShowBalloonTip(500);
                this.Hide();
                return;
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                this.niIcono.Visible = false;
            }

            // Point(Distancia hasta la izq LEFT, Distancia hasta la superior TOP)

            if (this.txtUsuario.Visible)
                this.txtComentarios.Location = new System.Drawing.Point(iPosDatosX, iPosDatosY);
            else
                this.txtComentarios.Location = new System.Drawing.Point(iPosDatosX, iPosArbolY);

            this.tviXML.Location = new System.Drawing.Point(iPosArbolX, iPosArbolY);
            this.tviXML.Size = new System.Drawing.Size(325, this.Size.Height - iPosArbolY - 65);

            this.grpDatos.Size = new System.Drawing.Size(this.Size.Width - this.tviXML.Size.Width - 30, this.Size.Height - iPosDatosX - 85);
            this.txtComentarios.Size = new System.Drawing.Size(this.grpDatos.Size.Width - 20, this.grpDatos.Size.Height - this.txtComentarios.Location.Y - 15);
            // Rama
            // this.txtComentarios.Location = new System.Drawing.Point(9, 17);
            // this.txtComentarios.Size = new System.Drawing.Size(489, 487);
            // Hoja
            // this.txtComentarios.Location = new System.Drawing.Point(6, 161);
            // this.txtComentarios.Size = new System.Drawing.Size(492, 343);
            //MessageBox.Show("Resize");

        }

        private void niIcono_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
        }

        private void controlarCambiosEnFichero()
        {
            controlarCambiosEnRegistro();

            if (this.bDatosFicheroCambiados)
            {
                if (MessageBox.Show("Los datos del fichero han cambiado\n¿Desea guardar los cambios?", "¡Atencion!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    clXML.guardarDocumentoXML(clConstantes.repositorioFicheroLocal, this);
                    this.bDatosFicheroCambiados = false;
                }
            }
        }

        private void controlarCambiosEnRegistro()
        {
            try
            {
                Application.DoEvents();
                if (this.bDatosRegistroCambiados)
                {
                    clXML.cambiarDatosElemento(this.tviXML.SelectedNode.Text, this.txtUsuario.Text, this.txtContrasena.Text, this.txtRuta.Text, this.txtComentarios.Text);
                    this.bDatosRegistroCambiados = false;
                    this.bDatosFicheroCambiados = true;
                }
            }
            catch (Exception ex)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Error al controlar los cambios \n Posiblemente esté seleccionando muy rápidamente los nodos", "frmPrincipal.controlarCambiosEnRegistro", ex, clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    clEncriptacion.probarEncriptacionStream();
        //    MessageBox.Show("Fin Mio XML");
        //}

        //private void toolStripButton2_Click(object sender, EventArgs e)
        //{
        //    clEncriptacion.probarEncriptacion();
        //    MessageBox.Show("Fin Ejemplo");
        //}

        private void mnuAñadirContraseña_Click(object sender, EventArgs e)
        {
            tstBH_Contrasena_Click(sender, e);
        }

        private void mnuAñadirCarpeta_Click(object sender, EventArgs e)
        {
            tstBH_Carpeta_Click(sender, e);
        }

        private void mnuCortar_Click(object sender, EventArgs e)
        {
            tstBH_Cortar_Click(sender, e);
        }

        private void mnuCopiar_Click(object sender, EventArgs e)
        {
            tstBH_Copiar_Click(sender, e);
        }

        private void mnuPegar_Click(object sender, EventArgs e)
        {
            tstBH_Pegar_Click(sender, e);
        }

        private void mnuExpandir_Click(object sender, EventArgs e)
        {
            TreeNode tNode = this.tviXML.SelectedNode;
            if (tNode == null) return;
            tNode.ExpandAll();
        }

        private void mnuRenombrar_Click(object sender, EventArgs e)
        {
            TreeNode tNode = this.tviXML.SelectedNode;
            if (tNode == null) return;
            frmRenombrar mfrm = new frmRenombrar();
            string sNombre = this.tviXML.SelectedNode.Text;
            mfrm.Nombre = sNombre;
            //mfrm.Location = new Point(GetCursorPosition().X, GetCursorPosition().Y);

            // Esta es la configuracion para un formulario con barra de titutlo
            //int x = this.tviXML.SelectedNode.Bounds.X - 12;
            //int y = this.tviXML.SelectedNode.Bounds.Y - 36; 

            // Y esta para un formulario con FormBorderStyle = None
            int x = this.tviXML.SelectedNode.Bounds.X - 3;
            int y = this.tviXML.SelectedNode.Bounds.Y - 4;
            Point point = new Point(x, y);
            Point absPoint = this.tviXML.PointToScreen(point);

            mfrm.Location = absPoint;
            //mfrm.Location = new Point(tNode.Bounds.X, tNode.Bounds.Y);
            mfrm.ShowDialog(this);
            if (mfrm.Nombre == null) return;
            if (mfrm.Nombre.Equals(sNombre)) return;
            if (mfrm.Nombre.Length == 0) return;

            string strID = this.tviXML.SelectedNode.Tag.ToString();

            this.clXML.NodoSeleccionado = clXML.findNodoDocXML(strID, clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            if (this.clXML.NodoSeleccionado == null)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo no encontrado en el XML", "frmPrincipal.tviXML_AfterLabelEdit", clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }

            this.tviXML.SelectedNode.Text = mfrm.Nombre;
            mfrm.Close();
            this.bDatosRegistroCambiados = true;
            if (this.clXML.NodoSeleccionado.Name.Equals(clConstantes.tipoNodoXMLContraseñas)) return;
            controlarCambiosEnRegistro();

            clGestoraXML_Orden.SortXml(this.clXML.NodoSeleccionado.ParentNode);
            clXML.unirXMLaTreeView(this.tviXML);

            clXML.findNodoTreeView(this.tviXML, strID);
            if (this.tviXML.SelectedNode == null)
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo no encontrado en el arbol", "frmPrincipal.tviXML_AfterLabelEdit", clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }

            cargarDatosNodo(this.clXML.NodoSeleccionado);
            controlarVisualizacionControles(this.clXML.NodoSeleccionado);
            //TreeNode tNode = this.tviXML.SelectedNode;
            //if (tNode == null) return;
            //this.tviXML.LabelEdit = true;
            //tNode.BeginEdit();
        }
        
        private void tviXML_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Application.DoEvents();
                Point p = new Point(e.X, e.Y);

                TreeNode tNodoSeleccionado = this.tviXML.GetNodeAt(p);
                if (tNodoSeleccionado != null)
                {
                    XmlNode xNode;
                    xNode = clXML.findNodoDocXML(tNodoSeleccionado.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                    if (xNode != null)
                    {
                        this.tviXML.SelectedNode = tNodoSeleccionado;

                        if (xNode.Name != clConstantes.tipoNodoXMLHoja)
                        {
                            mnuCortar.Enabled = false;
                            mnuCopiar.Enabled = false;
                        }
                        else
                        {
                            mnuCortar.Enabled = true;
                            mnuCopiar.Enabled = true;
                        }
                        this.ctxMenu.Show(this.tviXML, e.Location);
                    }
                }
            }
            Application.DoEvents();
        }

        private void tviXML_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            XmlNode xNode;
            xNode = clXML.findNodoDocXML(e.Node.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            Application.DoEvents();
            if (xNode != null)
            {
                // Antes de cambiar de nodo compruebo si el que está visible ha cambiado o no
                // Si ha cambiado guardo los cambios en el docXML
                controlarCambiosEnRegistro();
                this.tviXML.SelectedNode = e.Node;
                this.clXML.NodoSeleccionado = xNode;
                cargarDatosNodo(xNode);
                bDatosRegistroCambiados = false;
            }
            else
            {
                clExcepcionAplicacion exApp = new clExcepcionAplicacion("Nodo (" + e.Node.Tag.ToString() + ") no encontrado", "frmPrincipal.tviXML_NodeMouseClick", clExcepcionAplicacion.cTipoExcProgramacion);
                exApp.verExcepcion(this);
            }
            Application.DoEvents();
            //Console.WriteLine("Nodeclick : " + clXML.NodoSeleccionado.Attributes["id"].Value);
        }

        private void tviXML_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //if (e.Label != null)
            //{
            //    if (e.Label.Length > 0)
            //    {
            //        this.tviXML.LabelEdit = false;

            //        //if (this.tviXML.Nodes[0].Nodes.Count > 1)
            //        //{
            //        //    Console.WriteLine("A. Arbol, primer hijo " + this.tviXML.Nodes[0].Nodes[0].Text);
            //        //    Console.WriteLine("A. Arbol, segundo hijo " + this.tviXML.Nodes[0].Nodes[1].Text);
            //        //}
            //        //else
            //        //    Console.WriteLine("A. Nodos " + this.tviXML.Nodes[0].Nodes.Count.ToString());
            //        //Console.WriteLine("AfterLabelEdit - inicio : " + clXML.NodoSeleccionado.Attributes["id"].Value);

            //        string strID = this.tviXML.SelectedNode.Tag.ToString();
            //        //Console.WriteLine("AfterLabelEdit - strID : " + strID);
            //        this.clXML.NodoSeleccionado = clXML.findNodoDocXML(strID, clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            //        if (this.clXML.NodoSeleccionado == null)
            //        {
            //            ExcepcionAplicacion exApp = new ExcepcionAplicacion("Nodo no encontrado en el XML", "frmPrincipal.tviXML_AfterLabelEdit", ExcepcionAplicacion.cTipoExcProgramacion);
            //            exApp.verExcepcion(this);
            //        }

            //        this.tviXML.SelectedNode.Text = e.Label;
            //        //this.tviXML.SelectedNode.EndEdit(false);
            //        this.bDatosRegistroCambiados = true;
            //        controlarCambiosEnRegistro();
                    
            //        if (this.clXML.NodoSeleccionado.Name.Equals(clConstantes.tipoNodoXMLContraseñas)) return;

            //        cGestoraXML_Orden.SortXml(this.clXML.NodoSeleccionado.ParentNode);
            //        clXML.unirXMLaTreeView(this.tviXML);

            //        //if (this.tviXML.Nodes[0].Nodes.Count > 1)
            //        //{
            //        //    Console.WriteLine("B. Arbol, primer hijo " + this.tviXML.Nodes[0].Nodes[0].Text);
            //        //    Console.WriteLine("B. Arbol, segundo hijo " + this.tviXML.Nodes[0].Nodes[1].Text);
            //        //}
            //        //else
            //        //    Console.WriteLine("B. Nodos " + this.tviXML.Nodes[0].Nodes.Count.ToString());

            //        clXML.findNodoTreeView(this.tviXML, strID);
            //        if (this.tviXML.SelectedNode == null)
            //        {
            //            ExcepcionAplicacion exApp = new ExcepcionAplicacion("Nodo no encontrado en el arbol", "frmPrincipal.tviXML_AfterLabelEdit", ExcepcionAplicacion.cTipoExcProgramacion);
            //            exApp.verExcepcion(this);
            //        }

            //        //this.tviXML.SelectedNode.Parent.ExpandAll();

            //        cargarDatosNodo(this.clXML.NodoSeleccionado);
            //        controlarVisualizacionControles(this.clXML.NodoSeleccionado);
            //        e.CancelEdit = false;
            //    }
            //    else
            //    {
            //        e.CancelEdit = true;
            //        MessageBox.Show("Debe indicar algún texto", "¡Atencion!");
            //        e.Node.BeginEdit();
            //    }
                

            //    //if (this.tviXML.Nodes[0].Nodes.Count > 1)
            //    //{
            //    //    Console.WriteLine("C. Arbol, primer hijo " + this.tviXML.Nodes[0].Nodes[0].Text);
            //    //    Console.WriteLine("C. Arbol, segundo hijo " + this.tviXML.Nodes[0].Nodes[1].Text);
            //    //}
            //    //else
            //    //    Console.WriteLine("C. Nodos " + this.tviXML.Nodes[0].Nodes.Count.ToString());
            //    //Console.WriteLine("AfterLabelEdit - fin : " + clXML.NodoSeleccionado.Attributes["id"].Value);
            //}
        }

        private void tviXML_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XmlNode xNodo = clXML.findNodoDocXML(this.tviXML.SelectedNode.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                if (xNodo == null) return;
                controlarVisualizacionControles(xNodo);
                cargarDatosNodo(xNodo);
            }
        }

        private void tviXML_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Application.DoEvents();
            TreeNode tNodoSeleccionado = e.Node;
            if (tNodoSeleccionado != null)
            {
                XmlNode xNode;
                xNode = clXML.findNodoDocXML(tNodoSeleccionado.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                if (xNode != null)
                {
                    this.clXML.NodoSeleccionado = xNode;
                    controlarVisualizacionControles(xNode);
                    cargarDatosNodo(xNode);
                    //this.tviXML.SelectedNode = tNodoSeleccionado;
                }
            }
        }

        private void tviXML_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void tviXML_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tviXML_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode tNodoSeleccionado;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode tNodoDestino = ((TreeView)sender).GetNodeAt(pt);

                tNodoSeleccionado = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (tNodoSeleccionado.Tag.ToString().Equals("0"))
                {
                    clMensajesACliente.mensaje("No se puede mover la raiz del árbol", clMensajesACliente.cTipoMensajeAviso);
                    return;
                }

                XmlNode xNodoDestino;
                xNodoDestino = clXML.findNodoDocXML(tNodoDestino.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                XmlNode xNodoSeleccionado;
                xNodoSeleccionado = clXML.findNodoDocXML(tNodoSeleccionado.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                if (xNodoDestino == null || tNodoSeleccionado == null)
                {
                    clExcepcionAplicacion exApp = new clExcepcionAplicacion("El TreeNode seleccionado o destino no tiene XmlNode asociado", "frmPrincipal.tviXML_DragDrop", clExcepcionAplicacion.cTipoExcProgramacion);
                    exApp.verExcepcion(this);
                }

                string sID = "id=\"" + xNodoDestino.Attributes[clConstantes.atXMLID].Value.ToString() + "\"";
                if (xNodoSeleccionado.OuterXml.Contains(sID))
                {
                    clMensajesACliente.mensaje("No se puede mover a un hijo", clMensajesACliente.cTipoMensajeAviso);
                    return;
                }

                if (!tNodoDestino.Tag.Equals(tNodoSeleccionado.Tag))
                {
                    if (xNodoDestino.Name != clConstantes.tipoNodoXMLHoja)
                    {
                        // Actualizamos el control
                        tNodoDestino.Nodes.Add((TreeNode)tNodoSeleccionado.Clone());
                        tNodoDestino.Expand();
                        tNodoSeleccionado.Remove();
                        // y ahora el documento XML
                        xNodoSeleccionado.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
                        xNodoDestino.AppendChild((XmlNode)xNodoSeleccionado.CloneNode(true));
                        xNodoDestino.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
                        xNodoSeleccionado.ParentNode.RemoveChild(xNodoSeleccionado);
                        this.bDatosFicheroCambiados = true;
                    }
                    else
                    {
                        TreeNode tNodoPadreDestino;
                        tNodoPadreDestino = tNodoDestino.Parent;
                        xNodoDestino = clXML.findNodoDocXML(tNodoPadreDestino.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                        if (xNodoDestino == null)
                        {
                            clExcepcionAplicacion exApp = new clExcepcionAplicacion("El TreeNode padre del destino no tiene XmlNode asociado", "frmPrincipal.tviXML_DragDrop", clExcepcionAplicacion.cTipoExcProgramacion);
                            exApp.verExcepcion(this);
                        }

                        if (tNodoPadreDestino != null)
                        {
                            tNodoPadreDestino.Nodes.Add((TreeNode)tNodoSeleccionado.Clone());
                            tNodoPadreDestino.Expand();
                            tNodoSeleccionado.Remove();
                            // y ahora el documento XML
                            xNodoSeleccionado.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
                            xNodoDestino.AppendChild((XmlNode)xNodoSeleccionado.CloneNode(true));
                            xNodoDestino.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
                            xNodoSeleccionado.ParentNode.RemoveChild(xNodoSeleccionado);
                            this.bDatosFicheroCambiados = true;
                        }
                        else
                        {
                            clExcepcionAplicacion exApp = new clExcepcionAplicacion("El nodo hoja seleccionado no tiene padre", "frmPrincipal.tviXML_DragDrop", clExcepcionAplicacion.cTipoExcProgramacion);
                            exApp.verExcepcion(this);
                        }
                    }
                }
            }

        }

        private string abrirFormularioContrasena(bool bParaGuardar)
        {
            string mContrasenaActual = "";
            try
            {
                frmContrasena frm = new frmContrasena();
                if (bParaGuardar)
                    mContrasenaActual = clXML.getContrasenaFichero();
                else
                    frm.NombreFichero = clXML.RutaFicheroXMLNombre;

                frm.ContrasenaActual = mContrasenaActual;
                frm.ParaGuardar = bParaGuardar;
                frm.ShowDialog(this);
                if (bParaGuardar)
                {
                    // si la contraseña ha cambiado, la guardo
                    if (!mContrasenaActual.Equals(frm.ContrasenaActual))
                    {
                        clXML.guardarContrasenaFichero(frm.ContrasenaActual);
                        bDatosFicheroCambiados = true;
                    }
                }
                else
                {
                    mContrasenaActual = frm.ContrasenaActual;
                }
                frm.Close();
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            return mContrasenaActual;
        }

        private void tstBH_Buscar_Click(object sender, EventArgs e)
        {
            if (this.tstBH_CadenaBusqueda == null || this.tstBH_CadenaBusqueda.Text.Length == 0) return;
            buscarNodo();
        }

        private void buscarNodo()
        {
            if (sCadenaBuscada == this.tstBH_CadenaBusqueda.Text)
            {
                if (iNodoResultadoBusquedaMostrado < lResultadoBusqueda.Count())
                {
                    this.tviXML.SelectedNode = lResultadoBusqueda.ElementAt(iNodoResultadoBusquedaMostrado);
                    XmlNode xNodo = clXML.findNodoDocXML(this.tviXML.SelectedNode.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                    controlarVisualizacionControles(xNodo);
                    cargarDatosNodo(xNodo);
                    iNodoResultadoBusquedaMostrado = iNodoResultadoBusquedaMostrado + 1;
                    return;
                }
            }

            lResultadoBusqueda.Clear();
            
            iNodoResultadoBusquedaMostrado = 0;
            sCadenaBuscada = this.tstBH_CadenaBusqueda.Text;
            for (int iCount = 0; iCount < this.tviXML.Nodes.Count; iCount++)
            {
                if (this.tviXML.Nodes[iCount].Text.ToUpper().Contains(sCadenaBuscada.ToUpper()))
                {
                    lResultadoBusqueda.Add(this.tviXML.Nodes[iCount]);
                }
                lResultadoBusqueda.AddRange(buscarNodoRecursivamente(this.tviXML.Nodes[iCount]));
            }

            if (iNodoResultadoBusquedaMostrado < lResultadoBusqueda.Count())
            {
                this.tviXML.SelectedNode = lResultadoBusqueda.ElementAt(iNodoResultadoBusquedaMostrado);
                XmlNode xNodo = clXML.findNodoDocXML(this.tviXML.SelectedNode.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                controlarVisualizacionControles(xNodo);
                cargarDatosNodo(xNodo);
                iNodoResultadoBusquedaMostrado = iNodoResultadoBusquedaMostrado + 1;
            }
            this.tstBH_CadenaBusqueda.Items.Add(sCadenaBuscada);
        }

        private List<TreeNode> buscarNodoRecursivamente(TreeNode tNodo)
        {
            List<TreeNode> lResultado = new List<TreeNode>();
            for (int iCount = 0; iCount < tNodo.Nodes.Count; iCount++)
            {
                if (tNodo.Nodes[iCount].Text.ToUpper().Contains(sCadenaBuscada.ToUpper()))
                {
                    lResultado.Add(tNodo.Nodes[iCount]);
                }
                lResultado.AddRange(buscarNodoRecursivamente(tNodo.Nodes[iCount]));
            }
            return lResultado;
        }

        private void tstBH_CadenaBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buscarNodo();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            clExcepcionAplicacion ex = new clExcepcionAplicacion("Mensaje", "Metodo", clExcepcionAplicacion.cTipoExcFinAplicacion);
            ex.verExcepcion(this);
        }

        public void accionDespuesExcepcion(string sTipoExcepcion)
        {
            if (sTipoExcepcion.Equals(clExcepcionAplicacion.cTipoExcFinAplicacion))
            {
                bDatosFicheroCambiados = false;
                Application.Exit();
            }
            else if (sTipoExcepcion.Equals(clExcepcionAplicacion.cTipoExcProgramacion))
            {
                bDatosFicheroCambiados = false;
                Application.Exit();
            }
            else if (sTipoExcepcion.Equals(clExcepcionAplicacion.cTipoExcFicheroIncorrecto))
            {
                bDatosFicheroCambiados = false;
                iniciarFormulario();
            }
        }

        private void tstBH_Configuracion_Click(object sender, EventArgs e)
        {
            // <?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<raiz>\r\n  <datosfichero>\r\n    <contraseñaFichero value=\"1\" ultimamodificacion=\"03-05-2013 15:41:25\" />\r\n    <favoritos />\r\n  </datosfichero>\r\n  <contraseñas comentarios=\"\" id=\"0\" name=\"Mis contraseñas\" ultimamodificacion=\"03-05-2013 15:41:14\">\r\n    <hoja comentarios=\"\" contrasena=\"\" id=\"1\" name=\"Nueva contraseña\" nvecescopiado=\"0\" ruta=\"\" ultimamodificacion=\"03-05-2013 15:41:16\" ultimocopiado=\"\" usuario=\"\" />\r\n    <hoja comentarios=\"\" contrasena=\"\" id=\"2\" name=\"Nueva contraseña(1)\" nvecescopiado=\"0\" ruta=\"\" ultimamodificacion=\"03-05-2013 15:41:17\" ultimocopiado=\"\" usuario=\"\" />\r\n    <hoja comentarios=\"\" contrasena=\"\" id=\"3\" name=\"Nueva contraseña(2)\" nvecescopiado=\"0\" ruta=\"\" ultimamodificacion=\"03-05-2013 15:41:17\" ultimocopiado=\"\" usuario=\"\" />\r\n  </contraseñas>\r\n</raiz>
            string sContenido = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<raiz>\r\n  <datosfichero>\r\n    <contraseñaFichero value=\"1\" ultimamodificacion=\"03-05-2013 15:41:25\" />\r\n    <favoritos />\r\n  </datosfichero>\r\n  <contraseñas comentarios=\"\" id=\"0\" name=\"Mis contraseñas\" ultimamodificacion=\"03-05-2013 15:41:14\">\r\n    <hoja comentarios=\"\" contrasena=\"\" id=\"1\" name=\"Nueva contraseña\" nvecescopiado=\"0\" ruta=\"\" ultimamodificacion=\"03-05-2013 15:41:16\" ultimocopiado=\"\" usuario=\"\" />\r\n    <hoja comentarios=\"\" contrasena=\"\" id=\"2\" name=\"Nueva contraseña(1)\" nvecescopiado=\"0\" ruta=\"\" ultimamodificacion=\"03-05-2013 15:41:17\" ultimocopiado=\"\" usuario=\"\" />\r\n    <hoja comentarios=\"\" contrasena=\"\" id=\"3\" name=\"Nueva contraseña(2)\" nvecescopiado=\"0\" ruta=\"\" ultimamodificacion=\"03-05-2013 15:41:17\" ultimocopiado=\"\" usuario=\"\" />\r\n  </contraseñas>\r\n</raiz>";
            XmlDocument doc = new XmlDocument();
            byte[] byteArray = Encoding.UTF8.GetBytes(sContenido); 
            MemoryStream stream = new MemoryStream(byteArray);
            doc.PreserveWhitespace = false;
            doc.Load(stream);

            //using (Stream stContenido = clUtilidades.parseStringToStream(sContenido))
            //{
            //    XmlTextWriter wr = new XmlTextWriter(stContenido, Encoding.UTF8);
            //    wr.Formatting = Formatting.None; // here's the trick !
            //    doc.Save(wr);
            //    wr.Close();
            //}
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.tstBE_NElementos.Text = "Comprobando cambios en el fichero";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            controlarCambiosEnFichero();

            Cursor.Current = Cursors.Default;
            Application.DoEvents();
            bCierreControlado = true;

            Application.Exit();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                clSkyDrive.reconectar(this);
                MessageBox.Show("Conexión restablecida con SkyDrive");
            }
            catch (clExcepcionAplicacion exApp)
            {
                exApp.verExcepcion(this);
            }
            //MessageBox.Show(clSkyDrive.conexionActiva().ToString());
        }
    }
}

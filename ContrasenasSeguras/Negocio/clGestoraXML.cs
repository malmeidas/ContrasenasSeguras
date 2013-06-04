using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using ContraseñasSeguras.Comunes;

#pragma warning disable 0162

namespace ContraseñasSeguras.Negocio
{

    class clGestoraXML
    {
        private XmlDocument docXML;
        private long lngIDMax = 0;
        private XmlNode mNodoSeleccionado;
        private TreeNode tNodoSeleccionadoClipboard;
        private string sAccionClipboard;

        private string mRutaFicheroXML;
        private string mRutaFicheroXMLNombre;
        private string mRutaFicheroXMLRuta;
        private string mRepositorio;

        public clGestoraXML()
        {
            docXML = new XmlDocument();
        }

        public void iniciar()
        {
            docXML.RemoveAll();
            tNodoSeleccionadoClipboard = null;
            lngIDMax = 0;
            mRutaFicheroXML = "";
            mRutaFicheroXMLNombre = "";
            mRutaFicheroXMLRuta = "";
            mRepositorio = "";
        }

        public bool guardarDocumentoXML(string sRepositorioFichero, Form frmPadre)
        {
            // Como puede ser que el fichero sea nuevo, entonces la ruta no está definida
            if (mRutaFicheroXML == null || mRutaFicheroXML.Length == 0)
            {
                if (sRepositorioFichero.Equals(clConstantes.repositorioFicheroLocal))
                {
                    SaveFileDialog ficheroSeleccionado = new SaveFileDialog();
                    ficheroSeleccionado.Title = "Indica el fichero donde guardar los datos";
                    ficheroSeleccionado.Filter = clConstantes.SeleccionExtensionFicheros;

                    if (ficheroSeleccionado.ShowDialog(frmPadre) == DialogResult.OK)
                    {
                        mRutaFicheroXML = ficheroSeleccionado.FileName.ToString();
                        clUtilidades.getRutaNombreEnRutaCompleta(RutaFicheroXML, ref mRutaFicheroXMLRuta, ref mRutaFicheroXMLNombre, ref mRepositorio, sRepositorioFichero);

                        guardarDocumento();
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (sRepositorioFichero.Equals(clConstantes.repositorioFicheroSkyDrive))
                {
                    ContraseñasSeguras.CapaPresentacion.frmSkyDriveSeleccionFichero frmSel = new ContraseñasSeguras.CapaPresentacion.frmSkyDriveSeleccionFichero();
                    frmSel.SoloDirectorios = true;
                    frmSel.ShowDialog(frmPadre);
                    if (!frmSel.URIFicheroSeleccionado.Equals(string.Empty) && !frmSel.NombreFichero.Equals(string.Empty))
                    {
                        try
                        {
                            mRutaFicheroXML = frmSel.URIFicheroSeleccionado + frmSel.NombreFichero;
                            clUtilidades.getRutaNombreEnRutaCompleta(RutaFicheroXML, ref mRutaFicheroXMLRuta, ref mRutaFicheroXMLNombre, ref mRepositorio, sRepositorioFichero, frmSel.NombreFichero);

                            guardarDocumento();
                        }
                        catch (clExcepcionAplicacion ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new clExcepcionAplicacion("Repositorio no reconocido", "clGestoraXML.guardarDocumentoXML", clExcepcionAplicacion.cTipoExcProgramacion);
                }
            }
            else
            {
                guardarDocumento();
            }
            return true;
        }

        private void guardarDocumento()
        {
            try
            {
                if (!clConstantes.bNoEncriptar)
                {
                    if (mRutaFicheroXML.StartsWith(clConstantes.inicioRutaSkyDrive))
                    {
                        MemoryStream strXMLDesencriptado = new MemoryStream();
                        docXML.Save(strXMLDesencriptado);
                        MemoryStream strXMLEncriptado = clEncriptacion.encriptarStreamAStream(strXMLDesencriptado, clConstantes.claveEncriptacion);
                        ContraseñasSeguras.SkyDrive.clSkyDrive.guardarFicheroPUT(mRutaFicheroXML, strXMLEncriptado);
                        //MemoryStream memStream = (MemoryStream)strXMLEncriptado;
                        //ContraseñasSeguras.SkyDrive.clSkyDrive.guardarFicheroPOST(mRutaFicheroXML, "5 codificado POST.pas", (MemoryStream)memStream);
                    }
                    else
                    {
                        MemoryStream strXMLDesencriptado = new MemoryStream();
                        docXML.Save(strXMLDesencriptado);
                        clEncriptacion.encriptarStreamAFichero(strXMLDesencriptado, mRutaFicheroXML, clConstantes.claveEncriptacion);
                    }
                }
                else
                {
                    if (mRutaFicheroXML.StartsWith(clConstantes.inicioRutaSkyDrive))
                    {
                        MemoryStream strXMLDesencriptado = new MemoryStream();
                        docXML.Save(strXMLDesencriptado);
                        //ContraseñasSeguras.SkyDrive.clSkyDrive.guardarFicheroPOST(mRutaFicheroXML, "5 descodificado POST.pas", strXMLDesencriptado);
                        ContraseñasSeguras.SkyDrive.clSkyDrive.guardarFicheroPUT(mRutaFicheroXML, strXMLDesencriptado);
                        //ContraseñasSeguras.SkyDrive.clSkyDrive.guardarFicheroWebClientPUT(mRutaFicheroXML, strXMLDesencriptado);
                    }
                    else
                    {
                        docXML.Save(mRutaFicheroXML);
                    }
                }
            }
            catch (clExcepcionAplicacion exAPP)
            {
                throw exAPP;
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Error al guardar el fichero", "clGestoraXML.guardarDocumentoXML", ex, clExcepcionAplicacion.cTipoExcError);
            }
        }

        public void iniciarDocumentoXML(MemoryStream mContenidoEncriptado, String sContenidoDesencriptado, string RutaFicheroXML, string sNombreFicheroXML)
        {
            iniciar();

            if (mContenidoEncriptado == null && sContenidoDesencriptado == null)
            {
                throw new clExcepcionAplicacion("Contenido del fichero encriptado vacio", "clGestoraXML.iniciarDocumentoXML", clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
            }

            try
            {
                if (clConstantes.bNoEncriptar)
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(sContenidoDesencriptado);
                    MemoryStream stream = new MemoryStream(byteArray);
                    docXML.Load(stream);
                }
                else
                {
                    MemoryStream strXMLDesencriptado = new MemoryStream();
                    strXMLDesencriptado = clEncriptacion.desencriptarStreamAStream(mContenidoEncriptado, clConstantes.claveEncriptacion);
                    strXMLDesencriptado.Flush();
                    strXMLDesencriptado.Position = 0;
                    docXML.Load(strXMLDesencriptado);
                    strXMLDesencriptado.Close();
                }

                mRutaFicheroXML = RutaFicheroXML;
                clUtilidades.getRutaNombreEnRutaCompleta(RutaFicheroXML, ref mRutaFicheroXMLRuta, ref mRutaFicheroXMLNombre, ref mRepositorio, "", sNombreFicheroXML);
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Excepción al leer el XML", "clGestoraXML.iniciarDocumentoXML", ex);
            }
        }

        public void iniciarDocumentoXML(string RutaFicheroXML, string sNombreFicheroXML)
        {
            iniciar();

            if (RutaFicheroXML.Length != 0)
            {
                if (!System.IO.File.Exists(RutaFicheroXML))
                {
                    throw new clExcepcionAplicacion("El fichero (" + RutaFicheroXML + ") no existe", "clGestoraXML.iniciarDocumentoXML");
                    return;
                }

                try
                {
                    if (!clConstantes.bNoEncriptar)
                    {
                        MemoryStream strXMLDesencriptado = new MemoryStream();
                        strXMLDesencriptado = clEncriptacion.desencriptarFicheroAStream(RutaFicheroXML, clConstantes.claveEncriptacion);
                        strXMLDesencriptado.Flush();
                        strXMLDesencriptado.Position = 0;
                        docXML.Load(strXMLDesencriptado);
                        strXMLDesencriptado.Close();
                    }
                    else
                    {
                        docXML.Load(RutaFicheroXML);
                    }
                    mRutaFicheroXML = RutaFicheroXML;
                    clUtilidades.getRutaNombreEnRutaCompleta(RutaFicheroXML, ref mRutaFicheroXMLRuta, ref mRutaFicheroXMLNombre, ref mRepositorio, "", sNombreFicheroXML);
                }
                catch (Exception ex)
                {
                    throw new clExcepcionAplicacion("Excepción al leer el XML", "clGestoraXML.iniciarDocumentoXML", ex);
                }
            }
            else
            {
                //try
                //{
                //    int aa = 0;
                //    int ee = 1 / aa;
                //}
                //catch (Exception ex)
                //{
                //    throw new clExcepcionAplicacion("Mensaje", "Metodo", ex,clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
                //}

                XmlNode docNode = docXML.CreateXmlDeclaration("1.0", "UTF-8", null);
                docXML.AppendChild(docNode);
                
                XmlNode nodoRaiz = docXML.CreateElement(clConstantes.tipoNodoXMLRaiz);
                docXML.AppendChild(nodoRaiz);

                XmlNode nodoDatosfichero = docXML.CreateNode(System.Xml.XmlNodeType.Element, "", clConstantes.tipoNodoXMLDatosFichero, "");
                nodoRaiz.AppendChild(nodoDatosfichero);

                XmlNode xNodo = docXML.CreateNode(System.Xml.XmlNodeType.Element, "", clConstantes.tipoNodoXMLContrasenaFichero, "");
                XmlElement xElemento;
                xElemento = (XmlElement)xNodo;
                xElemento.SetAttribute(clConstantes.atXMLValor, "");
                xElemento.SetAttribute(clConstantes.atXMLUltimaModificacion, DateTime.Now.ToString(clConstantes.formatoFechaHora));
                nodoDatosfichero.AppendChild(xNodo);

                xNodo = docXML.CreateNode(System.Xml.XmlNodeType.Element, "", clConstantes.tipoNodoXMLFavoritos, "");
                nodoDatosfichero.AppendChild(xNodo);

                xNodo = docXML.CreateNode(System.Xml.XmlNodeType.Element, "", clConstantes.tipoNodoXMLContraseñas, "");
                xElemento = (XmlElement)xNodo;
                this.lngIDMax = 0;
                xElemento.SetAttribute(clConstantes.atXMLID, this.lngIDMax.ToString());
                xElemento.SetAttribute(clConstantes.atXMLNombre, "Mis contraseñas");
                xElemento.SetAttribute(clConstantes.atXMLComentarios, "");
                xElemento.SetAttribute(clConstantes.atXMLUltimaModificacion, DateTime.Now.ToString(clConstantes.formatoFechaHora));
                nodoRaiz.AppendChild(xNodo);

                //docXML.Save(Console.Out);
            }
            //docXML.Save(Console.Out);
        }

        public void eliminarNodo(TreeNode tNodoABorrar, TreeView control)
        {
            if (tNodoABorrar == null) return;
            if (control == null) return;

            XmlNode xNodo = findNodoDocXML(tNodoABorrar.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            if (xNodo == null) return;

            control.Nodes.Remove(tNodoABorrar);
            xNodo.ParentNode.RemoveChild(xNodo);
        }

        public void unirXMLaTreeView(TreeView control)
        {
            if (docXML == null) 
                throw new clExcepcionAplicacion("Documento XML no iniciado","clGestoraXML.unirXMLaTreeView",clExcepcionAplicacion.cTipoExcProgramacion);

            // quitamos los elementos vacios
            string sXML = docXML.OuterXml;
            sXML.Replace("<rama></rama>", "");
            sXML.Replace("<hoja></hoja>", "");
            sXML.Replace("<rama />", "");
            sXML.Replace("<hoja />", "");
            sXML.Replace("<rama/>", "");
            sXML.Replace("<hoja/>", "");
            
            docXML.RemoveAll();
            docXML.LoadXml(sXML);
            clGestoraXML_Orden.SortXml(docXML.DocumentElement.FirstChild.NextSibling);

            Console.WriteLine("Despues de ordenar " + docXML.OuterXml);

            long lngID;

            lngID = 0;
            lngIDMax = 0;
            
            try
            {
                control.Nodes.Clear();

                // Localizo el nodo de contraseñas porque solo tengo que cargar en el TreeView ese nodo
                XmlNode xNodoContraseñas = docXML.DocumentElement.FirstChild.NextSibling;

                validarNodo(xNodoContraseñas, "clGestoraXML.unirXMLaTreeView");

                control.Nodes.Add(xNodoContraseñas.Attributes[clConstantes.atXMLNombre].Value);
                
                TreeNode tnNodo = new TreeNode();
                tnNodo = control.Nodes[0];
                tnNodo.ImageIndex = 0;
                tnNodo.SelectedImageIndex = tnNodo.ImageIndex;

                lngID = clUtilidades.parseStringToLong(xNodoContraseñas.Attributes[clConstantes.atXMLID].Value);
                if (lngIDMax < lngID) lngIDMax = lngID;
                
                tnNodo.Tag = lngID.ToString();

                addNodoATreeView(xNodoContraseñas, tnNodo);
            }
            catch (clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (XmlException xmlEx)
            {
                throw new clExcepcionAplicacion("Excepción al cargar el XML", "clGestoraXML.unirXMLaTreeView", xmlEx,clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Excepción al cargar el XML", "clGestoraXML.unirXMLaTreeView", ex, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
            }
        }

        private void addNodoATreeView(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;
            long lngID;

            try
            {
                if (inXmlNode.Name == clConstantes.tipoNodoXMLDatosFichero) return;

                if (!inXmlNode.HasChildNodes && inXmlNode.Attributes.Count == 0)
                {
                    // Si es un elemento vacio lo eliminamos
                    inXmlNode.ParentNode.RemoveChild(inXmlNode);
                    return;
                }

                validarNodo(inXmlNode, "clGestoraXML.addNodoATreeView");

                if (inXmlNode.HasChildNodes)
                {
                    nodeList = inXmlNode.ChildNodes;
                    for (i = 0; i <= nodeList.Count - 1; i++)
                    {
                        xNode = inXmlNode.ChildNodes[i];

                        if (!xNode.HasChildNodes && xNode.Attributes.Count == 0)
                        {
                            xNode.ParentNode.RemoveChild(xNode);
                            continue;
                        }

                        validarNodo(xNode, "clGestoraXML.addNodoATreeView");

                        tNode = new TreeNode(xNode.Attributes[clConstantes.atXMLNombre].Value);
                        tNode.Tag = xNode.Attributes[clConstantes.atXMLID].Value;
                        if (xNode.Name.Equals(clConstantes.tipoNodoXMLRama))
                            tNode.ImageIndex = 0;
                        else
                            tNode.ImageIndex = 1;
                        tNode.SelectedImageIndex = tNode.ImageIndex;
                        
                        inTreeNode.Nodes.Add(tNode);

                        lngID = clUtilidades.parseStringToLong(xNode.Attributes[clConstantes.atXMLID].Value);
                        if (lngIDMax < lngID) lngIDMax = lngID;

                        addNodoATreeView(xNode, tNode);
                    }
                }
            }
            catch (clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Excepción al cargar los nodos del XML", "clGestoraXML.addNodoATreeView", ex);
            }
        }

        private void validarNodo(XmlNode xNodo, string MetodoLlamante)
        {
            if (xNodo.Name == clConstantes.tipoNodoXMLDatosFichero || xNodo.Name == clConstantes.tipoNodoXMLRaiz) return;

            XmlElement xElement = (XmlElement)xNodo;

            if (xNodo.Name != clConstantes.tipoNodoXMLHoja)
            {
                if (!xElement.HasAttribute(clConstantes.atXMLID))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLID + "' en nodo rama", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLNombre))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLNombre + "' en nodo rama", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLComentarios))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLComentarios + "' en nodo rama", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLUltimaModificacion))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLUltimaModificacion + "' en nodo rama", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
            }
            else
            {
                if (!xElement.HasAttribute(clConstantes.atXMLID))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLID + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLNombre))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLNombre + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLUsuario))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLUsuario + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLContraseña))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLContraseña + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLRuta))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLRuta + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLComentarios))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLComentarios + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLNVecesCopiado))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLNVecesCopiado + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLUltimoCopiado))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLUltimoCopiado + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);

                if (!xElement.HasAttribute(clConstantes.atXMLUltimaModificacion))
                    throw new clExcepcionAplicacion("Falta atributo '" + clConstantes.atXMLUltimaModificacion + "' en nodo hoja", MetodoLlamante, clExcepcionAplicacion.cTipoExcFicheroIncorrecto);
            }
        }

        public string addCarpetaContraseñaAXML(XmlNode xNodoSeleccionado, string NombreCarpeta)
        {
            if (xNodoSeleccionado == null) throw new clExcepcionAplicacion("Nodo seleccionado nulo al añadir carpeta", "clGestoraXML.a", clExcepcionAplicacion.cTipoExcProgramacion);
            if (NombreCarpeta == null || NombreCarpeta.Length == 0) throw new clExcepcionAplicacion("Nombre de carpeta vacia al añadir carpeta", "clGestoraXML.a", clExcepcionAplicacion.cTipoExcProgramacion);
            if (xNodoSeleccionado.Name == clConstantes.tipoNodoXMLHoja) throw new clExcepcionAplicacion("No se puede añadir una rama a una hoja al añadir carpeta", "clGestoraXML.a", clExcepcionAplicacion.cTipoExcProgramacion);

            XmlNode xRama = docXML.CreateElement(clConstantes.tipoNodoXMLRama);
            XmlAttribute xAtributo = docXML.CreateAttribute(clConstantes.atXMLID);
            this.lngIDMax = this.lngIDMax + 1;
            xAtributo.Value = this.lngIDMax.ToString();
            xRama.Attributes.Append(xAtributo);

            xAtributo = docXML.CreateAttribute(clConstantes.atXMLComentarios);
            xAtributo.Value = "";
            xRama.Attributes.Append(xAtributo);
            
            xAtributo = docXML.CreateAttribute(clConstantes.atXMLNombre);
            int iNumeroRepeticiones = getNumeroRepeticionesAtributo(NombreCarpeta, clConstantes.atXMLNombre, clConstantes.TipoBusquedaNodoEmpiezaPor);
            if (iNumeroRepeticiones == 0)
                xAtributo.Value = NombreCarpeta;
            else
                xAtributo.Value = NombreCarpeta + "(" + iNumeroRepeticiones.ToString() + ")";
            xRama.Attributes.Append(xAtributo);

            xAtributo = docXML.CreateAttribute(clConstantes.atXMLUltimaModificacion);
            xAtributo.Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
            xRama.Attributes.Append(xAtributo);

            xNodoSeleccionado.AppendChild(xRama);
            //XmlNode xNodo = docXML.CreateNode(System.Xml.XmlNodeType.Element, "", clConstantes.tipoNodoXMLRama, "");
            //XmlElement xElemento;
            //xElemento = (XmlElement)xNodo;
            //xElemento.SetAttribute(clConstantes.atXMLNombre, NombreCarpeta);
            //xElemento.SetAttribute(clConstantes.atXMLComentarios, "");
            //this.lngIDMax = this.lngIDMax + 1;
            //xElemento.SetAttribute(clConstantes.atXMLID, this.lngIDMax.ToString());
            //xElemento.SetAttribute(clConstantes.atXMLUltimaModificacion, DateTime.Now.ToString(clConstantes.formatoFechaHora));
            //xNodoSeleccionado.AppendChild(xNodo);
            return lngIDMax.ToString();
        }

        public string addHojaContraseñaAXML(XmlNode xNodoSeleccionado, string NombreContraseña)
        {
            if (xNodoSeleccionado == null) throw new clExcepcionAplicacion("Nodo seleccionado nulo al añadir contraseña", "clGestoraXML.a", clExcepcionAplicacion.cTipoExcProgramacion);
            if (NombreContraseña == null || NombreContraseña.Length == 0) throw new clExcepcionAplicacion("Nombre de contraseña vacia al añadir contraseña", "clGestoraXML.a", clExcepcionAplicacion.cTipoExcProgramacion);
            if (xNodoSeleccionado.Name == clConstantes.tipoNodoXMLHoja) throw new clExcepcionAplicacion("No se puede añadir una hoja a una hoja al añadir hoja", "clGestoraXML.a", clExcepcionAplicacion.cTipoExcProgramacion);

            XmlNode xNodo = docXML.CreateNode(System.Xml.XmlNodeType.Element, "", clConstantes.tipoNodoXMLHoja, "");
            XmlElement xElemento;
            xElemento = (XmlElement)xNodo;
            int iNumeroRepeticiones = getNumeroRepeticionesAtributo(NombreContraseña, clConstantes.atXMLNombre, clConstantes.TipoBusquedaNodoEmpiezaPor);
            if (iNumeroRepeticiones == 0)
                xElemento.SetAttribute(clConstantes.atXMLNombre, NombreContraseña);
            else
                xElemento.SetAttribute(clConstantes.atXMLNombre, NombreContraseña + "(" + iNumeroRepeticiones.ToString() + ")");
            xElemento.SetAttribute(clConstantes.atXMLComentarios, "");
            this.lngIDMax = this.lngIDMax + 1;
            xElemento.SetAttribute(clConstantes.atXMLID, this.lngIDMax.ToString());
            xElemento.SetAttribute(clConstantes.atXMLContraseña, "");
            xElemento.SetAttribute(clConstantes.atXMLRuta, "");
            xElemento.SetAttribute(clConstantes.atXMLUsuario, "");
            xElemento.SetAttribute(clConstantes.atXMLNVecesCopiado, "0");
            xElemento.SetAttribute(clConstantes.atXMLUltimoCopiado, "");
            xElemento.SetAttribute(clConstantes.atXMLUltimaModificacion, DateTime.Now.ToString(clConstantes.formatoFechaHora));
            xNodoSeleccionado.AppendChild(xNodo);
            return lngIDMax.ToString();
        }

        public string getContrasenaFichero()
        {
            try
            {
                foreach (XmlNode xNodo in docXML.DocumentElement.FirstChild.ChildNodes)
                {
                    if (xNodo.Name.Equals(clConstantes.tipoNodoXMLContrasenaFichero))
                        return xNodo.Attributes[clConstantes.atXMLValor].Value;
                }
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Excepcion al buscar la contraseña de fichero", "clGestoraXML.getContrasenaFichero", ex, clExcepcionAplicacion.cTipoExcProgramacion);
            }

            throw new clExcepcionAplicacion("Nodo de contraseña de fichero no encontrada", "clGestoraXML.getContrasenaFichero", clExcepcionAplicacion.cTipoExcProgramacion);
        }

        public void guardarContrasenaFichero(string sContrasena)
        {
            foreach (XmlNode xNodo in docXML.DocumentElement.FirstChild.ChildNodes)
            {
                if (xNodo.Name.Equals(clConstantes.tipoNodoXMLContrasenaFichero))
                {
                    xNodo.Attributes[clConstantes.atXMLValor].Value = sContrasena;
                    xNodo.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
                }
            }

        }

//        public XmlNode findNodoDocXML(string strABuscar, string sAtributo, string TipoBusquedaNodo, string[] sBuscarEnNodosTipo)
        public XmlNode findNodoDocXML(string strABuscar, string sAtributo, string sTipoBusquedaNodo)
        {
            XmlNode xNodoContraseñas;

            if (docXML == null) return null;

            xNodoContraseñas = docXML.DocumentElement.FirstChild.NextSibling;
            if (sTipoBusquedaNodo.Equals(clConstantes.TipoBusquedaNodoExacta))
            {
                if (strABuscar.Equals(xNodoContraseñas.Attributes[sAtributo].Value.ToString()))
                    return xNodoContraseñas;
            }
            else if (sTipoBusquedaNodo.Equals(clConstantes.TipoBusquedaNodoEmpiezaPor))
            {
                if (strABuscar.StartsWith(xNodoContraseñas.Attributes[sAtributo].Value.ToString()))
                    return xNodoContraseñas;
            }

            if (xNodoContraseñas.HasChildNodes == true)
                return findNodoDocXMLRecursivo(strABuscar, xNodoContraseñas, sAtributo, sTipoBusquedaNodo);
            else
                return null;

        }

        private XmlNode findNodoDocXMLRecursivo(string strABuscar, XmlNode inXmlNode, string sAtributo, string sTipoBusquedaNodo)
        {
            XmlNode xNode;
            XmlNode xNodeEncontrado;
            XmlNodeList nodeList;
            int i;

            try
            {
                if (inXmlNode.HasChildNodes)
                {
                    nodeList = inXmlNode.ChildNodes;
                    for (i = 0; i <= nodeList.Count - 1; i++)
                    {
                        xNode = inXmlNode.ChildNodes[i];

                        if (sTipoBusquedaNodo.Equals(clConstantes.TipoBusquedaNodoExacta))
                        {
                            if (strABuscar.Equals(xNode.Attributes[sAtributo].Value.ToString()))
                                return xNode;
                        }
                        else if (sTipoBusquedaNodo.Equals(clConstantes.TipoBusquedaNodoEmpiezaPor))
                        {
                            if (strABuscar.StartsWith(xNode.Attributes[sAtributo].Value.ToString()))
                                return xNode;
                        }

                        xNodeEncontrado = findNodoDocXMLRecursivo(strABuscar, xNode, sAtributo, sTipoBusquedaNodo);
                        if (xNodeEncontrado != null) return xNodeEncontrado;
                    }
                }
                else
                {
                    return null;
                }
                return null;
            }
            catch (clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Excepción al cargar los nodos del XML", "clGestoraXML.findNodoDocXMLRecursivo", ex);
            }
        }

        public void findNodoTreeView(TreeView control, string strID)
        {
            TreeNode tNodo;

            if (control == null || control.Nodes.Count == 0) return;
            control.SelectedNode = null;
            tNodo = control.Nodes[0];
            if (strID.Equals(tNodo.Tag.ToString()))
            {
                control.SelectedNode = tNodo;
                return;
            }
            findNodoTreeViewRecursivo(control, strID, tNodo);
        }

        private void findNodoTreeViewRecursivo(TreeView control, string strID, TreeNode inTreeNode)
        {
            TreeNodeCollection colNodos;
            TreeNode tNodo;
            try
            {
                if (inTreeNode.Nodes.Count != 0)
                {
                    colNodos = inTreeNode.Nodes;
                    for (int i = 0; i <= inTreeNode.Nodes.Count - 1; i++)
                    {
                        tNodo = inTreeNode.Nodes[i];
                        if (strID.Equals(tNodo.Tag.ToString()))
                        {
                            control.SelectedNode = tNodo;
                            return;
                        }

                        findNodoTreeViewRecursivo(control, strID, tNodo);
                        if (control.SelectedNode != null) return;
                    }
                }
                else
                {
                    return;
                }
                return;
            }
            catch (clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Excepción al buscar un nodo en el arbol", "clGestoraXML.findNodoTreeViewRecursivo", ex);
            }
        }

        public int getNumeroRepeticionesAtributo(string strABuscar, string sAtributo, string sTipoBusquedaNodo)
        {
            string strXMLContrasenas;
            string sPatronABuscar;
            int iNumeroRepeticiones = 0;
            strXMLContrasenas = docXML.DocumentElement.FirstChild.NextSibling.OuterXml;
            if ( sTipoBusquedaNodo.Equals(clConstantes.TipoBusquedaNodoExacta))
                sPatronABuscar = sAtributo + "=\u0022" + strABuscar + "\u0022";
            else
                sPatronABuscar = sAtributo + "=\u0022" + strABuscar;

            iNumeroRepeticiones = strXMLContrasenas.Split(new String[] { sPatronABuscar }, StringSplitOptions.None).Count() - 1;
            return iNumeroRepeticiones;
        }

        public void cambiarDatosElemento(string nombre, string usuario, string contrasena, string ruta, string comentarios)
        {
            XmlElement xElement = (XmlElement)mNodoSeleccionado;
            xElement.SetAttribute(clConstantes.atXMLNombre, nombre);

            if (mNodoSeleccionado.Name != clConstantes.tipoNodoXMLHoja)
            {
                mNodoSeleccionado.Attributes[clConstantes.atXMLComentarios].Value = comentarios;
                mNodoSeleccionado.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
            }
            else
            {
                mNodoSeleccionado.Attributes[clConstantes.atXMLUsuario].Value = usuario;
                mNodoSeleccionado.Attributes[clConstantes.atXMLContraseña].Value = contrasena;
                mNodoSeleccionado.Attributes[clConstantes.atXMLRuta].Value = ruta;
                mNodoSeleccionado.Attributes[clConstantes.atXMLComentarios].Value = comentarios;
                mNodoSeleccionado.Attributes[clConstantes.atXMLUltimaModificacion].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
            }
        }

        public void ejecutarPegar(TreeNode tNodoDestino, TreeView control)
        {
            // estas condiciones hay que pensarlas bien porque se puede pegar un texto creado una clave de ese nombre
            if (tNodoDestino == null || tNodoSeleccionadoClipboard == null) return;
            if (sAccionClipboard == null || sAccionClipboard.Length == 0) return;

            // comprobamos que existen los ID de los nodos de TreeView seleccionados
            XmlNode xNodoOrigen;
            XmlNode xNodoDestino;
            try
            {
                xNodoOrigen = findNodoDocXML(tNodoSeleccionadoClipboard.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
                xNodoDestino = findNodoDocXML(tNodoDestino.Tag.ToString(), clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            }
            catch (clExcepcionAplicacion exApp)
            {
                throw exApp;
            }

            XmlNode xNodoNuevo = xNodoOrigen.CloneNode(true);
            if (sAccionClipboard.Equals(clConstantes.AccionClipboardCortar))
            {
                eliminarNodo(tNodoSeleccionadoClipboard, control);
            }
            else
            {
                cambiarIDEnNodo(xNodoNuevo);
            }

            if (xNodoDestino.Name.Equals(clConstantes.tipoNodoXMLHoja))
            {
                xNodoDestino.ParentNode.AppendChild(xNodoNuevo);
            }
            else
            {
                xNodoDestino.AppendChild(xNodoNuevo);
            }
            // Actualizamos el arbol del control
            unirXMLaTreeView(control);
            // Seleccionamos el nuevo nodo y expandimos
            findNodoTreeView(control, xNodoNuevo.Attributes[clConstantes.atXMLID].Value);
            control.SelectedNode.ExpandAll();
        }

        public void estadisticasAccionCopiado(string sID)
        {
            XmlNode xNodo = findNodoDocXML(sID, clConstantes.atXMLID, clConstantes.TipoBusquedaNodoExacta);
            if (xNodo == null) return;
            int i = int.Parse(xNodo.Attributes[clConstantes.atXMLNVecesCopiado].Value) + 1;
            xNodo.Attributes[clConstantes.atXMLNVecesCopiado].Value = i.ToString();
            xNodo.Attributes[clConstantes.atXMLUltimoCopiado].Value = DateTime.Now.ToString(clConstantes.formatoFechaHora);
        }

        private void cambiarIDEnNodo(XmlNode xNodoRaiz)
        {
            XmlNodeList xListaNodos;
            // No consigo que en la lista se incluye el SELF asi que lo hago a mano
            lngIDMax = lngIDMax + 1;
            xNodoRaiz.Attributes[clConstantes.atXMLID].Value = lngIDMax.ToString();

            xListaNodos = xNodoRaiz.SelectNodes("//*");
            foreach (XmlNode xNodo in xListaNodos)
            {
                lngIDMax = lngIDMax + 1;
                xNodo.Attributes[clConstantes.atXMLID].Value = lngIDMax.ToString();
            }
        }

        public List<clFavorito> getFavoritos()
        {
            int iNFavoritos1;
            int iNFavoritos2;
            var xElementos1 = docXML.GetElementsByTagName(clConstantes.tipoNodoXMLHoja).OfType<XmlElement>().OrderByDescending(name => (int)int.Parse(name.Attributes[clConstantes.atXMLNVecesCopiado].Value));
            var xElementos2 = docXML.GetElementsByTagName(clConstantes.tipoNodoXMLHoja).OfType<XmlElement>().OrderByDescending(name => (string)name.Attributes[clConstantes.atXMLUltimoCopiado].Value);

            if (xElementos1.Count() > 9)
                iNFavoritos1 = 9;
            else
                iNFavoritos1 = xElementos1.Count() - 1;

            if (xElementos2.Count() > 9)
                iNFavoritos2 = 9;
            else
                iNFavoritos2 = xElementos2.Count() - 1;

            if (iNFavoritos1 + iNFavoritos2 == -2) return null;

            List<clFavorito> xFavoritos = new List<clFavorito>();
            clFavorito xFavorito;
            
            for (int i = 0; i <= iNFavoritos1; i++)
            {
                xFavorito = new clFavorito();
                xFavorito.Tipo = clConstantes.TipoFavoritoNVeces;
                xFavorito.Imagen = "ContraseñasSeguras.Properties.Resources.List_NumberedHS";
                xFavorito.Texto = xElementos1.ElementAt(i).Attributes[clConstantes.atXMLNombre].Value;
                xFavorito.ID = xElementos1.ElementAt(i).Attributes[clConstantes.atXMLID].Value;
                xFavoritos.Add(xFavorito);
            }

            for (int j = 0; j <= iNFavoritos2; j++)
            {
                xFavorito = new clFavorito();
                xFavorito.Tipo = clConstantes.TipoFavoritoUltimoCopiado;
                xFavorito.Imagen = "ContraseñasSeguras.Properties.Resources.clock";
                xFavorito.Texto = xElementos2.ElementAt(j).Attributes[clConstantes.atXMLNombre].Value;
                xFavorito.ID = xElementos2.ElementAt(j).Attributes[clConstantes.atXMLID].Value;
                xFavoritos.Add(xFavorito);
            }

            return xFavoritos;
        }

        public XmlDocument DocXML
        {
            get { return docXML; }
            set { docXML = value; }
        }

        public XmlNode NodoSeleccionado
        {
            get { return mNodoSeleccionado; }
            set { mNodoSeleccionado = value; }
        }

        public TreeNode NodoSeleccionadoClipboard
        {
            get { return tNodoSeleccionadoClipboard; }
            set { tNodoSeleccionadoClipboard = value; }
        }

        public string AccionClipboard
        {
            get { return sAccionClipboard; }
            set { sAccionClipboard = value; }
        }

        public string RutaFicheroXML
        {
            get { return mRutaFicheroXML; }
            //set { mRutaFicheroXML = value; }
        }

        public string RutaFicheroXMLNombre
        {
            get { return mRutaFicheroXMLNombre; }
            set { mRutaFicheroXMLNombre = value; }
        }

        public string RutaFicheroXMLRuta
        {
            get { return mRutaFicheroXMLRuta; }
            set { mRutaFicheroXMLRuta = value; }
        }

        public string Repositorio
        {
            get { return mRepositorio; }
            set { mRepositorio = value; }
        }

    }
}
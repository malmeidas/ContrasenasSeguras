using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Serialization;

namespace ContraseñasSeguras.Comunes
{
    public abstract class clGestoraXML_Orden
    {
        public static void SortXml(XmlDocument document)
        {
            SortXml(document.DocumentElement);
        }

        public static void SortXml(XmlNode rootNode)
        {
            SortAttributes(rootNode.Attributes);
            SortElements(rootNode);
            foreach (XmlNode childNode in rootNode.ChildNodes)
            {
                SortXml(childNode);
            }
        }

        public static void SortAttributes(XmlAttributeCollection attribCol)
        {
            if (attribCol == null)
                return;

            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;
                for (int i = 1; i < attribCol.Count; i++)
                {
                    if (String.Compare(attribCol[i].Name, attribCol[i - 1].Name, true) < 0)
                    {
                        attribCol.InsertBefore(attribCol[i], attribCol[i - 1]);
                        hasChanged = true;
                    }
                }
            }

        }

        /// Usa el algoritmo de ordenacion de burbuja (bubble sort algorithm)
        public static void SortElements(XmlNode node)
        {
            bool changed = true;
            bool cambiarPorId;
            while (changed)
            {
                changed = false;
                for (int i = 1; i < node.ChildNodes.Count; i++)
                {
                    cambiarPorId = String.Compare(node.ChildNodes[i].Attributes[clConstantes.atXMLNombre].Value, node.ChildNodes[i - 1].Attributes[clConstantes.atXMLNombre].Value, true) < 0;
                    if (node.ChildNodes[i].Name == clConstantes.tipoNodoXMLRama && node.ChildNodes[i - 1].Name == clConstantes.tipoNodoXMLHoja)
                    {
                        node.InsertBefore(node.ChildNodes[i], node.ChildNodes[i - 1]);
                        changed = true;
                    }
                    else if (node.ChildNodes[i].Name == clConstantes.tipoNodoXMLHoja && node.ChildNodes[i - 1].Name == clConstantes.tipoNodoXMLRama)
                    {
                    }
                    else if (cambiarPorId)
                    {
                        node.InsertBefore(node.ChildNodes[i], node.ChildNodes[i - 1]);
                        changed = true;
                    }
                }
            }
        }
    }
}

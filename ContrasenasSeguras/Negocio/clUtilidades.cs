using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;

namespace ContraseñasSeguras.Negocio
{
    static class clUtilidades
    {
        public static void addFicheroAbiertoASettings(string sNombre, string sRuta)
        {
            //Hashtable htFicherosAbiertos = new Hashtable();
            //htFicherosAbiertos = Properties.Settings.Default.FicherosAbiertosLocal;
            //if (htFicherosAbiertos == null) htFicherosAbiertos = new Hashtable();
            //htFicherosAbiertos.Add(sNombre, sRuta);
            
            //Properties.Settings.Default.FicherosAbiertosLocal.Add(sNombre, sRuta);
            //Properties.Settings.Default.Save();
        }

        public static bool clickOutOFormulario(int X, int Y, System.Windows.Forms.Control frm)
        {
            //if (frm.PointToScreen(click.Location).X < frm.PointToScreen(frm.Location).X)
            //    return true;
            //else if (frm.PointToScreen(click.Location).Y < frm.PointToScreen(frm.Location).Y)
            //    return true;
            //else if (frm.PointToScreen(click.Location).X > frm.PointToScreen(frm.Location).X + frm.Height)
            //    return true;
            //else if (frm.PointToScreen(click.Location).Y > frm.PointToScreen(frm.Location).Y + frm.Width)
            //    return true;

            if (X < frm.Location.X)
                return true;
            else if (Y < frm.Location.Y)
                return true;
            else if (X > frm.Location.X + frm.Width)
                return true;
            else if (Y > frm.Location.Y + frm.Height)
                return true;

            return false;
        }

        public static long parseStringToLong(string Texto)
        {
            long lng1 = 0;
            try
            {
                lng1 = long.Parse(Texto);
            }
            catch (Exception ex)
            {
                throw new clExcepcionAplicacion("Error al parsear un texto a número", clExcepcionAplicacion.cTipoExcProgramacion, ex);
            }
            return lng1;
        }

        public static System.IO.Stream parseStringToStream(string s)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static System.IO.MemoryStream parseStringToMemoryStream(string s)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
            stream.Flush();
            stream.Position = 0;
            return stream;
        }

        public static byte[] parseStreamToArrayByte(System.IO.FileStream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static byte[] parseStreamToArrayByte(System.IO.MemoryStream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static byte[] parseStreamToArrayByte(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static byte[] parseStringToArrayByte(string sCadena)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(sCadena);
        }

        public static string parseArrayByteToString(byte[] bt)
        {
            return Encoding.UTF8.GetString(bt);
        }

        public static void getRutaNombreEnRutaCompleta(string sRutaCompleta, ref string sRuta, ref string sNombre, ref string sRepositorio, string sRepositorioDefinido = "", string sNombreDefinido = "")
        {
            string sDelimitadores = @"\/";
            char[] delimitadores = sDelimitadores.ToCharArray(0, sDelimitadores.Length);
            sNombre = "";
            sRuta = "";
            if (sNombreDefinido.Length == 0)
            {
                string[] sPartes = sRutaCompleta.Split(delimitadores);
                if (sPartes.Count() > 0)
                {
                    sNombre = sPartes[sPartes.Count() - 1];
                    sRuta = sRutaCompleta.Substring(0, sRutaCompleta.Length - sNombre.Length);
                }
            }
            else
            {
                sRuta = sRutaCompleta;
                sNombre = sNombreDefinido;
            }

            if (sRepositorioDefinido.Length == 0)
            {
                if (sRutaCompleta.ToUpper().StartsWith("HTTP"))
                    sRepositorio = ContraseñasSeguras.Comunes.clConstantes.repositorioFicheroSkyDrive;
                else
                    sRepositorio = ContraseñasSeguras.Comunes.clConstantes.repositorioFicheroLocal;
            }
            else
                sRepositorio = sRepositorioDefinido;

        }
    }
}

using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace ContraseñasSeguras.Comunes
{
    static class clEncriptacion
    {
        // El codigo comentado abajo no es valido porque al trabajar con varios tipos de Stream (FileStream y MemoryStream sobre todo)
        // pues resulta que el proceso de encriptación y desencriptación no funcionaba porque daba una excepción de BAD DATA
        // El problema era que los Stream no eran los mismos y que para que la encriptación funcione hay que trabajar siempre con array de byte

        static public byte[] encriptarAByteAAByte(byte[] btNoEncriptado, string sKey)
        {
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform encriptador = DES.CreateEncryptor();
                byte[] btEncriptado = encriptador.TransformFinalBlock(btNoEncriptado, 0, btNoEncriptado.Length);
                return btEncriptado;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al encriptar", "encriptarAByteAAByte", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        static public byte[] desencriptarAByteAAByte(byte[] btEncriptado, string sKey)
        {
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencriptador = DES.CreateDecryptor();
                byte[] btNoEncriptado = desencriptador.TransformFinalBlock(btEncriptado, 0, btEncriptado.Length);
                return btNoEncriptado;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al desencriptar", "desencriptarAByteAAByte", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        static public MemoryStream desencriptarFicheroAStream(string sRutaFichero, string sKey)
        {
            MemoryStream msDesencriptado;
            try
            {
                FileStream fsFichero = new FileStream(sRutaFichero, FileMode.Open, FileAccess.Read);
                byte[] btEncriptado = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(fsFichero);
                byte[] btDesencriptado = desencriptarAByteAAByte(btEncriptado, sKey);
                msDesencriptado = new MemoryStream(btDesencriptado);
                msDesencriptado.Flush();
                msDesencriptado.Position = 0;
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al desencriptar fichero", "desencriptarFicheroAStream", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
            return msDesencriptado;
        }

        static public MemoryStream desencriptarStreamAStream(MemoryStream msEncriptado, string sKey)
        {
            MemoryStream msDesencriptado;
            try
            {
                byte[] btEncriptado = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(msEncriptado);
                byte[] btDesencriptado = desencriptarAByteAAByte(btEncriptado, sKey);
                msDesencriptado = new MemoryStream(btDesencriptado);
                msDesencriptado.Flush();
                msDesencriptado.Position = 0;
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al desencriptar stream", "desencriptarStreamAStream", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
            return msDesencriptado;
        }

        static public void encriptarStreamAFichero(MemoryStream msSinEncriptar, string sRutaFichero, string sKey)
        {
            try
            {
                byte[] btSinEncriptar = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(msSinEncriptar);
                byte[] btEncriptado = encriptarAByteAAByte(btSinEncriptar, sKey);
                FileStream fsFichero = new FileStream(sRutaFichero, FileMode.OpenOrCreate, FileAccess.Write);
                fsFichero.Write(btEncriptado, 0, btEncriptado.Length);
                fsFichero.Close();
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al encriptar al fichero", "encriptarStreamAFichero", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
        }

        static public MemoryStream encriptarStreamAStream(MemoryStream msSinEncriptar, string sKey)
        {
            MemoryStream msEncriptado;
            try
            {
                byte[] btSinEncriptar = ContraseñasSeguras.Negocio.clUtilidades.parseStreamToArrayByte(msSinEncriptar);
                byte[] btEncriptado = encriptarAByteAAByte(btSinEncriptar, sKey);
                msEncriptado = new MemoryStream(btEncriptado);
                msEncriptado.Flush();
                msEncriptado.Position = 0;
            }
            catch (ContraseñasSeguras.Negocio.clExcepcionAplicacion exApp)
            {
                throw exApp;
            }
            catch (Exception ex)
            {
                throw new ContraseñasSeguras.Negocio.clExcepcionAplicacion("Error al encriptar al fichero", "encriptarStreamAFichero", ex, ContraseñasSeguras.Negocio.clExcepcionAplicacion.cTipoExcError);
            }
            return msEncriptado;
        }

        ////  Call this function to remove the key from memory after use for security
        //[System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint="RtlZeroMemory")]
        //public static extern bool ZeroMemory(IntPtr Destination, int Length);

        //// Function to Generate a 64 bits Key.
        //static string GenerateKey() 
        //{
        //    // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
        //    DESCryptoServiceProvider desCrypto =(DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

        //    // Use the Automatically generated key for Encryption. 
        //    return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        //}

        //static public void encriptarStreamAFichero(Stream strXMLDesencriptado, string sFicheroEncriptado, string sKey)
        //{
        //    FileStream fsEncrypted = new FileStream(sFicheroEncriptado, FileMode.Create, FileAccess.Write);

        //    // For additional security Pin the key.
        //    // GCHandle gch = GCHandle.Alloc(sKey, GCHandleType.Pinned);

        //    DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        //    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    ICryptoTransform desencrypt = DES.CreateEncryptor();
        //    CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

        //    strXMLDesencriptado.Flush();
        //    strXMLDesencriptado.Position = 0;
        //    byte[] bytearrayinput = new byte[strXMLDesencriptado.Length];
        //    strXMLDesencriptado.Read(bytearrayinput, 0, bytearrayinput.Length);

        //    //using (StreamWriter plainTextStream = new StreamWriter(cryptostream, Encoding.UTF8))
        //    //{
        //    //    plainTextStream.Write(bytearrayinput);
        //    //}
        //    cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
        //    cryptostream.Close();
        //    strXMLDesencriptado.Close();
        //    fsEncrypted.Close();

        //    // Remove the Key from memory. 
        //    // ZeroMemory(gch.AddrOfPinnedObject(), sKey.Length * 2);
        //    // gch.Free();
        //}

        //static public MemoryStream encriptarStreamAStream(Stream strXMLDesencriptado, string sKey)
        //{
        //    // For additional security Pin the key.
        //    // GCHandle gch = GCHandle.Alloc(sKey, GCHandleType.Pinned);

        //    DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        //    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    ICryptoTransform desencrypt = DES.CreateEncryptor();
        //    MemoryStream strXMLEncriptadoTmp = new MemoryStream();
        //    CryptoStream cryptostream = new CryptoStream(strXMLEncriptadoTmp, desencrypt, CryptoStreamMode.Write);

        //    strXMLDesencriptado.Flush();
        //    strXMLDesencriptado.Position = 0;
        //    byte[] bytearrayinput = new byte[strXMLDesencriptado.Length];
        //    strXMLDesencriptado.Read(bytearrayinput, 0, bytearrayinput.Length);
        //    cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            
        //    MemoryStream strXMLEncriptado = new MemoryStream();
        //    strXMLEncriptadoTmp.Flush();
        //    strXMLEncriptadoTmp.Position = 0;
        //    strXMLEncriptadoTmp.CopyTo(strXMLEncriptado);
        //    cryptostream.Close();
        //    strXMLDesencriptado.Close();

        //    // Remove the Key from memory. 
        //    // ZeroMemory(gch.AddrOfPinnedObject(), sKey.Length * 2);
        //    // gch.Free();
        //    return strXMLEncriptado;
        //}

        //static public void probarEncriptacionStream()
        //{
        //    MemoryStream mstr = new MemoryStream();

        //    GCHandle gch = GCHandle.Alloc(clConstantes.claveEncriptacion, GCHandleType.Pinned);

        //    // Salva el fichero encriptado
        //    FileStream fsSinEncriptar = new FileStream(@"C:\Users\Marcos\Documents\XML1.xml", FileMode.Open, FileAccess.Read);
        //    encriptarStreamAFichero(fsSinEncriptar, @"C:\Users\Marcos\Documents\XML1E.txt", clConstantes.claveEncriptacion);

        //    // Abre el fichero y lo desencripta
        //    MemoryStream msDesencriptado = new MemoryStream();
        //    msDesencriptado = desencriptarFicheroAStream(@"C:\Users\Marcos\Documents\XML1E.txt", clConstantes.claveEncriptacion);
        //    msDesencriptado.Flush();
        //    msDesencriptado.Position = 0;

        //    FileStream fiDesencriptado = new FileStream(@"C:\Users\Marcos\Documents\XML1D.txt", FileMode.Create, System.IO.FileAccess.Write);
        //    byte[] bytes = new byte[msDesencriptado.Length];
        //    msDesencriptado.Read(bytes, 0, (int)msDesencriptado.Length);
        //    fiDesencriptado.Write(bytes, 0, bytes.Length);
        //    fiDesencriptado.Close();
        //    msDesencriptado.Close();

        //    // Remove the Key from memory. 
        //    ZeroMemory(gch.AddrOfPinnedObject(), clConstantes.claveEncriptacion.Length * 2);
        //    gch.Free();
        //}

        //static public MemoryStream desencriptarFicheroAStream(string sFicheroEncriptado, string sKey)
        //{
        //    // For additional security Pin the key.
        //    // GCHandle gch = GCHandle.Alloc(sKey, GCHandleType.Pinned);

        //    DESCryptoServiceProvider dDesencriptador = new DESCryptoServiceProvider();
        //    dDesencriptador.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    dDesencriptador.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

        //    FileStream fsFicheroEncriptado = new FileStream(sFicheroEncriptado, FileMode.Open, FileAccess.Read);
        //    ICryptoTransform desdecrypt = dDesencriptador.CreateDecryptor();
        //    CryptoStream cryptostreamDecr = new CryptoStream(fsFicheroEncriptado, desdecrypt, CryptoStreamMode.Read);
        //    MemoryStream strXMLDesencriptado1 = new MemoryStream();
        //    StreamWriter fsDecrypted = new StreamWriter(strXMLDesencriptado1);
        //    fsDecrypted.Write(new StreamReader(cryptostreamDecr, Encoding.UTF8).ReadToEnd());
        //    fsDecrypted.Flush();
        //    strXMLDesencriptado1.Flush();
        //    strXMLDesencriptado1.Position = 0;

        //    // Remove the Key from memory. 
        //    // ZeroMemory(gch.AddrOfPinnedObject(), sKey.Length * 2);
        //    // gch.Free();

        //    dDesencriptador.Clear();
        //    dDesencriptador.Dispose();
        //    return strXMLDesencriptado1;
        //}

        //static public MemoryStream desencriptarStreamAStream(Stream mContenidoEncriptado, string sKey)
        //{
        //    // For additional security Pin the key.
        //    // GCHandle gch = GCHandle.Alloc(sKey, GCHandleType.Pinned);

        //    DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        //    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    //DES.Mode = CipherMode.CFB;
        //    //DES.Padding = PaddingMode.ISO10126;

        //    ICryptoTransform desdecrypt = DES.CreateDecryptor();
            
        //    CryptoStream cryptostreamDecr = new CryptoStream(mContenidoEncriptado, desdecrypt, CryptoStreamMode.Read);
        //    MemoryStream strXMLDesencriptado1 = new MemoryStream();
        //    cryptostreamDecr.CopyTo(strXMLDesencriptado1);
        //    //StreamWriter fsDecrypted = new StreamWriter(strXMLDesencriptado1);
        //    //fsDecrypted.Write(new StreamReader(cryptostreamDecr, Encoding.UTF8).ReadToEnd());
        //    //fsDecrypted.Flush();
        //    strXMLDesencriptado1.Flush();
        //    strXMLDesencriptado1.Position = 0;

        //    // Remove the Key from memory. 
        //    // ZeroMemory(gch.AddrOfPinnedObject(), sKey.Length * 2);
        //    // gch.Free();

        //    return strXMLDesencriptado1;
        //}

        //static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        //{
        //    FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
        //    FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);

        //    DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        //    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    ICryptoTransform desencrypt = DES.CreateEncryptor();
        //    CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

        //    byte[] bytearrayinput = new byte[fsInput.Length];
        //    fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
        //    cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
        //    cryptostream.Close();
        //    fsInput.Close();
        //    fsEncrypted.Close();
        //}

        //static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        //{
        //    DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        //    //A 64 bit key and IV is required for this provider.
        //    //Set secret key For DES algorithm.
        //    DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    //Set initialization vector.
        //    DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

        //    //Create a file stream to read the encrypted file back.
        //    FileStream fsread = new FileStream(sInputFilename,FileMode.Open,FileAccess.Read);
        //    //Create a DES decryptor from the DES instance.
        //    ICryptoTransform desdecrypt = DES.CreateDecryptor();
        //    //Create crypto stream set to read and do a 
        //    //DES decryption transform on incoming bytes.
        //    CryptoStream cryptostreamDecr = new CryptoStream(fsread,desdecrypt,CryptoStreamMode.Read);
        //    //Print the contents of the decrypted file.
        //    StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
        //    fsDecrypted.Write(new StreamReader(cryptostreamDecr, Encoding.UTF8).ReadToEnd());
        //    fsDecrypted.Flush();
        //    fsDecrypted.Close();
        //} 

        //static public void probarEncriptacion()
        //{
        //    // Must be 64 bits, 8 bytes.
        //    // Distribute this key to the user who will decrypt this file.
        //    string sSecretKey = clConstantes.claveEncriptacion;
         
        //    // Get the Key for the file to Encrypt.
        //    // sSecretKey = GenerateKey();

        //    // For additional security Pin the key.
        //    GCHandle gch = GCHandle.Alloc( sSecretKey,GCHandleType.Pinned );
         
        //    // Encrypt the file.        
        //    EncryptFile(@"C:\Users\Marcos\Documents\XML2.xml", @"C:\Users\Marcos\Documents\Encrypted.txt", sSecretKey);

        //    // Decrypt the file.
        //    DecryptFile(@"C:\Users\Marcos\Documents\Encrypted.txt", @"C:\Users\Marcos\Documents\Decrypted.txt", sSecretKey);

        //    // Remove the Key from memory. 
        //    ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
        //    gch.Free();
        //}


    }
}

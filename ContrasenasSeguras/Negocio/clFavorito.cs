using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContraseñasSeguras.Negocio
{
    class clFavorito
    {
        private string mTipo;
        private string mImagen;
        private string mID;
        private string mTexto;

        public string Tipo
        {
            get { return mTipo; }
            set { mTipo = value; }
        }

        public string Imagen
        {
            get { return mImagen; }
            set { mImagen = value; }
        }

        public string Texto
        {
            get { return mTexto; }
            set { mTexto = value; }
        }


        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }
    }
}

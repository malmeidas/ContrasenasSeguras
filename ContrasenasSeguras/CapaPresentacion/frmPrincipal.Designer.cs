namespace ContraseñasSeguras.CapaPresentacion
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.tviXML = new System.Windows.Forms.TreeView();
            this.imlXML = new System.Windows.Forms.ImageList(this.components);
            this.tstBarraHerramientas = new System.Windows.Forms.ToolStrip();
            this.tstBH_Nuevo = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Abrir = new System.Windows.Forms.ToolStripButton();
            this.tstBH_SkyDrive = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Guardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tstBH_ContrasenaFichero = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tstBH_Contrasena = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Carpeta = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tstBH_Favoritos = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tstBH_Cortar = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Copiar = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Pegar = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Eliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tstBH_Buscar = new System.Windows.Forms.ToolStripButton();
            this.tstBH_CadenaBusqueda = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tstBH_Configuracion = new System.Windows.Forms.ToolStripButton();
            this.tstBH_Abrir_SB = new System.Windows.Forms.ToolStripSplitButton();
            this.tstBH_Abrir_SB_Local = new System.Windows.Forms.ToolStripMenuItem();
            this.tstBH_Abrir_SB_SkyDrive = new System.Windows.Forms.ToolStripMenuItem();
            this.tstBH_Guardar_SB = new System.Windows.Forms.ToolStripSplitButton();
            this.tstBH_Guardar_SB_Local = new System.Windows.Forms.ToolStripMenuItem();
            this.tstBH_Guardar_SB_SkyDrive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.lbF7 = new System.Windows.Forms.Label();
            this.lbF6 = new System.Windows.Forms.Label();
            this.lbF5 = new System.Windows.Forms.Label();
            this.lbF4 = new System.Windows.Forms.Label();
            this.btnLanzarURL = new System.Windows.Forms.Button();
            this.btnCopiarURL = new System.Windows.Forms.Button();
            this.btnCopiarContrasena = new System.Windows.Forms.Button();
            this.btnCopiarUsuario = new System.Windows.Forms.Button();
            this.labComentarios = new System.Windows.Forms.Label();
            this.labRuta = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.labContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.labUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAñadirContraseña = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAñadirCarpeta = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRenombrar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCortar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopiar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPegar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExpandir = new System.Windows.Forms.ToolStripMenuItem();
            this.niIcono = new System.Windows.Forms.NotifyIcon(this.components);
            this.ssBarraEstado = new System.Windows.Forms.StatusStrip();
            this.tstBE_NElementos = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.tstBarraHerramientas.SuspendLayout();
            this.grpDatos.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.ssBarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // tviXML
            // 
            this.tviXML.AllowDrop = true;
            this.tviXML.HideSelection = false;
            this.tviXML.ImageIndex = 0;
            this.tviXML.ImageList = this.imlXML;
            this.tviXML.Location = new System.Drawing.Point(1, 29);
            this.tviXML.Name = "tviXML";
            this.tviXML.SelectedImageIndex = 0;
            this.tviXML.Size = new System.Drawing.Size(325, 510);
            this.tviXML.TabIndex = 43;
            this.tviXML.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tviXML_AfterLabelEdit);
            this.tviXML.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tviXML_ItemDrag);
            this.tviXML.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tviXML_AfterSelect);
            this.tviXML.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tviXML_NodeMouseClick);
            this.tviXML.DragDrop += new System.Windows.Forms.DragEventHandler(this.tviXML_DragDrop);
            this.tviXML.DragEnter += new System.Windows.Forms.DragEventHandler(this.tviXML_DragEnter);
            this.tviXML.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tviXML_KeyDown);
            this.tviXML.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tviXML_MouseUp);
            // 
            // imlXML
            // 
            this.imlXML.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlXML.ImageStream")));
            this.imlXML.TransparentColor = System.Drawing.Color.Transparent;
            this.imlXML.Images.SetKeyName(0, "Folder.png");
            this.imlXML.Images.SetKeyName(1, "base_key_32.png");
            this.imlXML.Images.SetKeyName(2, "List_BulletsHS.png");
            this.imlXML.Images.SetKeyName(3, "clock.png");
            // 
            // tstBarraHerramientas
            // 
            this.tstBarraHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstBH_Nuevo,
            this.tstBH_Abrir,
            this.tstBH_SkyDrive,
            this.tstBH_Guardar,
            this.toolStripSeparator4,
            this.tstBH_ContrasenaFichero,
            this.toolStripSeparator10,
            this.tstBH_Contrasena,
            this.tstBH_Carpeta,
            this.toolStripSeparator6,
            this.tstBH_Favoritos,
            this.toolStripSeparator9,
            this.tstBH_Cortar,
            this.tstBH_Copiar,
            this.tstBH_Pegar,
            this.tstBH_Eliminar,
            this.toolStripSeparator5,
            this.tstBH_Buscar,
            this.tstBH_CadenaBusqueda,
            this.toolStripSeparator7,
            this.tstBH_Configuracion,
            this.tstBH_Abrir_SB,
            this.tstBH_Guardar_SB});
            this.tstBarraHerramientas.Location = new System.Drawing.Point(0, 0);
            this.tstBarraHerramientas.Name = "tstBarraHerramientas";
            this.tstBarraHerramientas.Size = new System.Drawing.Size(855, 25);
            this.tstBarraHerramientas.TabIndex = 40;
            this.tstBarraHerramientas.Text = "Barra herramientas";
            // 
            // tstBH_Nuevo
            // 
            this.tstBH_Nuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Nuevo.Image = global::ContraseñasSeguras.Properties.Resources.document;
            this.tstBH_Nuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Nuevo.Name = "tstBH_Nuevo";
            this.tstBH_Nuevo.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Nuevo.Text = "Nuevo";
            this.tstBH_Nuevo.ToolTipText = "Nuevo (N)";
            this.tstBH_Nuevo.Click += new System.EventHandler(this.tstBH_Nuevo_Click);
            // 
            // tstBH_Abrir
            // 
            this.tstBH_Abrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Abrir.Image = global::ContraseñasSeguras.Properties.Resources.folderopen;
            this.tstBH_Abrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Abrir.Name = "tstBH_Abrir";
            this.tstBH_Abrir.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Abrir.Text = "Abrir";
            this.tstBH_Abrir.ToolTipText = "Abrir (A)";
            this.tstBH_Abrir.Click += new System.EventHandler(this.tstBH_Abrir_Click);
            // 
            // tstBH_SkyDrive
            // 
            this.tstBH_SkyDrive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_SkyDrive.Image = global::ContraseñasSeguras.Properties.Resources.SkyDriveLogo;
            this.tstBH_SkyDrive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_SkyDrive.Name = "tstBH_SkyDrive";
            this.tstBH_SkyDrive.Size = new System.Drawing.Size(23, 22);
            this.tstBH_SkyDrive.Text = "SkyDrive";
            this.tstBH_SkyDrive.Click += new System.EventHandler(this.tstBH_SkyDrive_Click);
            // 
            // tstBH_Guardar
            // 
            this.tstBH_Guardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Guardar.Image = global::ContraseñasSeguras.Properties.Resources.FloppyDisk;
            this.tstBH_Guardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Guardar.Name = "tstBH_Guardar";
            this.tstBH_Guardar.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Guardar.Text = "Guardar";
            this.tstBH_Guardar.ToolTipText = "Guardar (G)";
            this.tstBH_Guardar.Click += new System.EventHandler(this.tstBH_Guardar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tstBH_ContrasenaFichero
            // 
            this.tstBH_ContrasenaFichero.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_ContrasenaFichero.Image = global::ContraseñasSeguras.Properties.Resources.security;
            this.tstBH_ContrasenaFichero.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_ContrasenaFichero.Name = "tstBH_ContrasenaFichero";
            this.tstBH_ContrasenaFichero.Size = new System.Drawing.Size(23, 22);
            this.tstBH_ContrasenaFichero.Text = "Contraseña";
            this.tstBH_ContrasenaFichero.ToolTipText = "Contraseña (Q)";
            this.tstBH_ContrasenaFichero.Click += new System.EventHandler(this.tstBH_ContrasenaFichero_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tstBH_Contrasena
            // 
            this.tstBH_Contrasena.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Contrasena.Image = global::ContraseñasSeguras.Properties.Resources.Key;
            this.tstBH_Contrasena.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Contrasena.Name = "tstBH_Contrasena";
            this.tstBH_Contrasena.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Contrasena.Text = "Nueva contraseña";
            this.tstBH_Contrasena.ToolTipText = "Nueva contraseña (P)";
            this.tstBH_Contrasena.Click += new System.EventHandler(this.tstBH_Contrasena_Click);
            // 
            // tstBH_Carpeta
            // 
            this.tstBH_Carpeta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Carpeta.Image = global::ContraseñasSeguras.Properties.Resources.Folder;
            this.tstBH_Carpeta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Carpeta.Name = "tstBH_Carpeta";
            this.tstBH_Carpeta.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Carpeta.Text = "Nueva carpeta";
            this.tstBH_Carpeta.ToolTipText = "Nueva carpeta (F)";
            this.tstBH_Carpeta.Click += new System.EventHandler(this.tstBH_Carpeta_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tstBH_Favoritos
            // 
            this.tstBH_Favoritos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Favoritos.Image = global::ContraseñasSeguras.Properties.Resources.FavoriteStar_16x16;
            this.tstBH_Favoritos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Favoritos.Name = "tstBH_Favoritos";
            this.tstBH_Favoritos.Size = new System.Drawing.Size(32, 22);
            this.tstBH_Favoritos.Text = "Favoritos (F3)";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // tstBH_Cortar
            // 
            this.tstBH_Cortar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Cortar.Image = global::ContraseñasSeguras.Properties.Resources.cut;
            this.tstBH_Cortar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Cortar.Name = "tstBH_Cortar";
            this.tstBH_Cortar.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Cortar.Text = "Cortar";
            this.tstBH_Cortar.Click += new System.EventHandler(this.tstBH_Cortar_Click);
            // 
            // tstBH_Copiar
            // 
            this.tstBH_Copiar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Copiar.Image = global::ContraseñasSeguras.Properties.Resources.copy;
            this.tstBH_Copiar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Copiar.Name = "tstBH_Copiar";
            this.tstBH_Copiar.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Copiar.Text = "Copiar";
            this.tstBH_Copiar.Click += new System.EventHandler(this.tstBH_Copiar_Click);
            // 
            // tstBH_Pegar
            // 
            this.tstBH_Pegar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Pegar.Image = global::ContraseñasSeguras.Properties.Resources.paste;
            this.tstBH_Pegar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Pegar.Name = "tstBH_Pegar";
            this.tstBH_Pegar.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Pegar.Text = "Pegar";
            this.tstBH_Pegar.Click += new System.EventHandler(this.tstBH_Pegar_Click);
            // 
            // tstBH_Eliminar
            // 
            this.tstBH_Eliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Eliminar.Image = global::ContraseñasSeguras.Properties.Resources.delete;
            this.tstBH_Eliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Eliminar.Name = "tstBH_Eliminar";
            this.tstBH_Eliminar.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Eliminar.Text = "Elminar";
            this.tstBH_Eliminar.Click += new System.EventHandler(this.tstBH_Eliminar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tstBH_Buscar
            // 
            this.tstBH_Buscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Buscar.Image = global::ContraseñasSeguras.Properties.Resources._174_magnify_uncompressed;
            this.tstBH_Buscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Buscar.Name = "tstBH_Buscar";
            this.tstBH_Buscar.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Buscar.Text = "Buscar";
            this.tstBH_Buscar.ToolTipText = "Buscar (B)";
            this.tstBH_Buscar.Click += new System.EventHandler(this.tstBH_Buscar_Click);
            // 
            // tstBH_CadenaBusqueda
            // 
            this.tstBH_CadenaBusqueda.Name = "tstBH_CadenaBusqueda";
            this.tstBH_CadenaBusqueda.Size = new System.Drawing.Size(121, 25);
            this.tstBH_CadenaBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstBH_CadenaBusqueda_KeyDown);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tstBH_Configuracion
            // 
            this.tstBH_Configuracion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Configuracion.Image = global::ContraseñasSeguras.Properties.Resources.base_cog_32;
            this.tstBH_Configuracion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Configuracion.Name = "tstBH_Configuracion";
            this.tstBH_Configuracion.Size = new System.Drawing.Size(23, 22);
            this.tstBH_Configuracion.Text = "Configuración";
            this.tstBH_Configuracion.Visible = false;
            this.tstBH_Configuracion.Click += new System.EventHandler(this.tstBH_Configuracion_Click);
            // 
            // tstBH_Abrir_SB
            // 
            this.tstBH_Abrir_SB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Abrir_SB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstBH_Abrir_SB_Local,
            this.tstBH_Abrir_SB_SkyDrive});
            this.tstBH_Abrir_SB.Image = global::ContraseñasSeguras.Properties.Resources.folderopen;
            this.tstBH_Abrir_SB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Abrir_SB.Name = "tstBH_Abrir_SB";
            this.tstBH_Abrir_SB.Size = new System.Drawing.Size(32, 22);
            this.tstBH_Abrir_SB.Text = "toolStripSplitButton1";
            this.tstBH_Abrir_SB.Visible = false;
            this.tstBH_Abrir_SB.ButtonClick += new System.EventHandler(this.tstBH_Abrir_SB_ButtonClick);
            // 
            // tstBH_Abrir_SB_Local
            // 
            this.tstBH_Abrir_SB_Local.Name = "tstBH_Abrir_SB_Local";
            this.tstBH_Abrir_SB_Local.Size = new System.Drawing.Size(119, 22);
            this.tstBH_Abrir_SB_Local.Text = "Local";
            this.tstBH_Abrir_SB_Local.Click += new System.EventHandler(this.tstBH_Abrir_SB_Local_Click);
            // 
            // tstBH_Abrir_SB_SkyDrive
            // 
            this.tstBH_Abrir_SB_SkyDrive.Image = global::ContraseñasSeguras.Properties.Resources.SkyDriveLogo;
            this.tstBH_Abrir_SB_SkyDrive.Name = "tstBH_Abrir_SB_SkyDrive";
            this.tstBH_Abrir_SB_SkyDrive.Size = new System.Drawing.Size(119, 22);
            this.tstBH_Abrir_SB_SkyDrive.Text = "SkyDrive";
            this.tstBH_Abrir_SB_SkyDrive.Click += new System.EventHandler(this.tstBH_Abrir_SB_SkyDrive_Click);
            // 
            // tstBH_Guardar_SB
            // 
            this.tstBH_Guardar_SB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstBH_Guardar_SB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstBH_Guardar_SB_Local,
            this.tstBH_Guardar_SB_SkyDrive});
            this.tstBH_Guardar_SB.Image = global::ContraseñasSeguras.Properties.Resources.FloppyDisk;
            this.tstBH_Guardar_SB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstBH_Guardar_SB.Name = "tstBH_Guardar_SB";
            this.tstBH_Guardar_SB.Size = new System.Drawing.Size(32, 22);
            this.tstBH_Guardar_SB.Text = "toolStripSplitButton1";
            this.tstBH_Guardar_SB.Visible = false;
            this.tstBH_Guardar_SB.ButtonClick += new System.EventHandler(this.tstBH_Guardar_SB_ButtonClick);
            // 
            // tstBH_Guardar_SB_Local
            // 
            this.tstBH_Guardar_SB_Local.Name = "tstBH_Guardar_SB_Local";
            this.tstBH_Guardar_SB_Local.Size = new System.Drawing.Size(119, 22);
            this.tstBH_Guardar_SB_Local.Text = "Local";
            this.tstBH_Guardar_SB_Local.Click += new System.EventHandler(this.tstBH_Guardar_SB_Local_Click);
            // 
            // tstBH_Guardar_SB_SkyDrive
            // 
            this.tstBH_Guardar_SB_SkyDrive.Image = global::ContraseñasSeguras.Properties.Resources.SkyDriveLogo;
            this.tstBH_Guardar_SB_SkyDrive.Name = "tstBH_Guardar_SB_SkyDrive";
            this.tstBH_Guardar_SB_SkyDrive.Size = new System.Drawing.Size(119, 22);
            this.tstBH_Guardar_SB_SkyDrive.Text = "SkyDrive";
            this.tstBH_Guardar_SB_SkyDrive.Click += new System.EventHandler(this.tstBH_Guardar_SB_SkyDrive_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // grpDatos
            // 
            this.grpDatos.Controls.Add(this.lbF7);
            this.grpDatos.Controls.Add(this.lbF6);
            this.grpDatos.Controls.Add(this.lbF5);
            this.grpDatos.Controls.Add(this.lbF4);
            this.grpDatos.Controls.Add(this.btnLanzarURL);
            this.grpDatos.Controls.Add(this.btnCopiarURL);
            this.grpDatos.Controls.Add(this.btnCopiarContrasena);
            this.grpDatos.Controls.Add(this.btnCopiarUsuario);
            this.grpDatos.Controls.Add(this.labComentarios);
            this.grpDatos.Controls.Add(this.labRuta);
            this.grpDatos.Controls.Add(this.txtRuta);
            this.grpDatos.Controls.Add(this.labContrasena);
            this.grpDatos.Controls.Add(this.txtContrasena);
            this.grpDatos.Controls.Add(this.labUsuario);
            this.grpDatos.Controls.Add(this.txtUsuario);
            this.grpDatos.Controls.Add(this.txtComentarios);
            this.grpDatos.Location = new System.Drawing.Point(335, 29);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(513, 510);
            this.grpDatos.TabIndex = 41;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "Datos";
            // 
            // lbF7
            // 
            this.lbF7.AutoSize = true;
            this.lbF7.Location = new System.Drawing.Point(465, 126);
            this.lbF7.Name = "lbF7";
            this.lbF7.Size = new System.Drawing.Size(19, 13);
            this.lbF7.TabIndex = 15;
            this.lbF7.Text = "F7";
            // 
            // lbF6
            // 
            this.lbF6.AutoSize = true;
            this.lbF6.Location = new System.Drawing.Point(435, 126);
            this.lbF6.Name = "lbF6";
            this.lbF6.Size = new System.Drawing.Size(19, 13);
            this.lbF6.TabIndex = 13;
            this.lbF6.Text = "F6";
            // 
            // lbF5
            // 
            this.lbF5.AutoSize = true;
            this.lbF5.Location = new System.Drawing.Point(462, 68);
            this.lbF5.Name = "lbF5";
            this.lbF5.Size = new System.Drawing.Size(19, 13);
            this.lbF5.TabIndex = 11;
            this.lbF5.Text = "F5";
            // 
            // lbF4
            // 
            this.lbF4.AutoSize = true;
            this.lbF4.Location = new System.Drawing.Point(462, 30);
            this.lbF4.Name = "lbF4";
            this.lbF4.Size = new System.Drawing.Size(19, 13);
            this.lbF4.TabIndex = 9;
            this.lbF4.Text = "F4";
            // 
            // btnLanzarURL
            // 
            this.btnLanzarURL.Image = global::ContraseñasSeguras.Properties.Resources._112_RightArrowLong_Blue_16x16_72;
            this.btnLanzarURL.Location = new System.Drawing.Point(461, 100);
            this.btnLanzarURL.Name = "btnLanzarURL";
            this.btnLanzarURL.Size = new System.Drawing.Size(24, 23);
            this.btnLanzarURL.TabIndex = 16;
            this.btnLanzarURL.UseVisualStyleBackColor = true;
            this.btnLanzarURL.Click += new System.EventHandler(this.btnLanzarURL_Click);
            // 
            // btnCopiarURL
            // 
            this.btnCopiarURL.Image = global::ContraseñasSeguras.Properties.Resources.CopyHS;
            this.btnCopiarURL.Location = new System.Drawing.Point(432, 100);
            this.btnCopiarURL.Name = "btnCopiarURL";
            this.btnCopiarURL.Size = new System.Drawing.Size(23, 23);
            this.btnCopiarURL.TabIndex = 14;
            this.btnCopiarURL.UseVisualStyleBackColor = true;
            this.btnCopiarURL.Click += new System.EventHandler(this.btnCopiarURL_Click);
            // 
            // btnCopiarContrasena
            // 
            this.btnCopiarContrasena.Image = global::ContraseñasSeguras.Properties.Resources.CopyHS;
            this.btnCopiarContrasena.Location = new System.Drawing.Point(432, 62);
            this.btnCopiarContrasena.Name = "btnCopiarContrasena";
            this.btnCopiarContrasena.Size = new System.Drawing.Size(23, 23);
            this.btnCopiarContrasena.TabIndex = 12;
            this.btnCopiarContrasena.UseVisualStyleBackColor = true;
            this.btnCopiarContrasena.Click += new System.EventHandler(this.btnCopiarContrasena_Click);
            // 
            // btnCopiarUsuario
            // 
            this.btnCopiarUsuario.Image = global::ContraseñasSeguras.Properties.Resources.CopyHS;
            this.btnCopiarUsuario.Location = new System.Drawing.Point(432, 25);
            this.btnCopiarUsuario.Name = "btnCopiarUsuario";
            this.btnCopiarUsuario.Size = new System.Drawing.Size(23, 23);
            this.btnCopiarUsuario.TabIndex = 10;
            this.btnCopiarUsuario.UseVisualStyleBackColor = true;
            this.btnCopiarUsuario.Click += new System.EventHandler(this.btnCopiarUsuario_Click);
            // 
            // labComentarios
            // 
            this.labComentarios.AutoSize = true;
            this.labComentarios.Location = new System.Drawing.Point(17, 142);
            this.labComentarios.Name = "labComentarios";
            this.labComentarios.Size = new System.Drawing.Size(65, 13);
            this.labComentarios.TabIndex = 7;
            this.labComentarios.Text = "C&omentarios";
            // 
            // labRuta
            // 
            this.labRuta.AutoSize = true;
            this.labRuta.Location = new System.Drawing.Point(17, 106);
            this.labRuta.Name = "labRuta";
            this.labRuta.Size = new System.Drawing.Size(63, 13);
            this.labRuta.TabIndex = 5;
            this.labRuta.Text = "URL / &Ruta";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(84, 103);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(336, 20);
            this.txtRuta.TabIndex = 5;
            this.txtRuta.TextChanged += new System.EventHandler(this.txtRuta_TextChanged);
            this.txtRuta.Leave += new System.EventHandler(this.txtRuta_Leave);
            // 
            // labContrasena
            // 
            this.labContrasena.AutoSize = true;
            this.labContrasena.Location = new System.Drawing.Point(19, 68);
            this.labContrasena.Name = "labContrasena";
            this.labContrasena.Size = new System.Drawing.Size(61, 13);
            this.labContrasena.TabIndex = 3;
            this.labContrasena.Text = "&Contraseña";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(84, 65);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(336, 20);
            this.txtContrasena.TabIndex = 3;
            this.txtContrasena.UseSystemPasswordChar = true;
            this.txtContrasena.TextChanged += new System.EventHandler(this.txtContrasena_TextChanged);
            this.txtContrasena.Leave += new System.EventHandler(this.txtContrasena_Leave);
            // 
            // labUsuario
            // 
            this.labUsuario.AutoSize = true;
            this.labUsuario.Location = new System.Drawing.Point(37, 31);
            this.labUsuario.Name = "labUsuario";
            this.labUsuario.Size = new System.Drawing.Size(43, 13);
            this.labUsuario.TabIndex = 1;
            this.labUsuario.Text = "&Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(84, 28);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(336, 20);
            this.txtUsuario.TabIndex = 1;
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
            this.txtUsuario.Leave += new System.EventHandler(this.txtUsuario_Leave);
            // 
            // txtComentarios
            // 
            this.txtComentarios.AcceptsReturn = true;
            this.txtComentarios.AcceptsTab = true;
            this.txtComentarios.Location = new System.Drawing.Point(9, 160);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(492, 343);
            this.txtComentarios.TabIndex = 7;
            this.txtComentarios.TextChanged += new System.EventHandler(this.txtComentarios_TextChanged);
            this.txtComentarios.Leave += new System.EventHandler(this.txtComentarios_Leave);
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAñadirContraseña,
            this.mnuAñadirCarpeta,
            this.toolStripSeparator1,
            this.mnuRenombrar,
            this.toolStripSeparator2,
            this.mnuCortar,
            this.mnuCopiar,
            this.mnuPegar,
            this.toolStripSeparator3,
            this.mnuExpandir});
            this.ctxMenu.Name = "contextMenuStrip1";
            this.ctxMenu.Size = new System.Drawing.Size(191, 176);
            // 
            // mnuAñadirContraseña
            // 
            this.mnuAñadirContraseña.Name = "mnuAñadirContraseña";
            this.mnuAñadirContraseña.Size = new System.Drawing.Size(190, 22);
            this.mnuAñadirContraseña.Text = "Añadir Contraseña (P)";
            this.mnuAñadirContraseña.Click += new System.EventHandler(this.mnuAñadirContraseña_Click);
            // 
            // mnuAñadirCarpeta
            // 
            this.mnuAñadirCarpeta.Name = "mnuAñadirCarpeta";
            this.mnuAñadirCarpeta.Size = new System.Drawing.Size(190, 22);
            this.mnuAñadirCarpeta.Text = "Añadir  Carpeta (F)";
            this.mnuAñadirCarpeta.Click += new System.EventHandler(this.mnuAñadirCarpeta_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // mnuRenombrar
            // 
            this.mnuRenombrar.Name = "mnuRenombrar";
            this.mnuRenombrar.Size = new System.Drawing.Size(190, 22);
            this.mnuRenombrar.Text = "Renombrar (F2)";
            this.mnuRenombrar.Click += new System.EventHandler(this.mnuRenombrar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // mnuCortar
            // 
            this.mnuCortar.Name = "mnuCortar";
            this.mnuCortar.Size = new System.Drawing.Size(190, 22);
            this.mnuCortar.Text = "Cortar";
            this.mnuCortar.Click += new System.EventHandler(this.mnuCortar_Click);
            // 
            // mnuCopiar
            // 
            this.mnuCopiar.Name = "mnuCopiar";
            this.mnuCopiar.Size = new System.Drawing.Size(190, 22);
            this.mnuCopiar.Text = "Copiar";
            this.mnuCopiar.Click += new System.EventHandler(this.mnuCopiar_Click);
            // 
            // mnuPegar
            // 
            this.mnuPegar.Name = "mnuPegar";
            this.mnuPegar.Size = new System.Drawing.Size(190, 22);
            this.mnuPegar.Text = "Pegar";
            this.mnuPegar.Click += new System.EventHandler(this.mnuPegar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // mnuExpandir
            // 
            this.mnuExpandir.Name = "mnuExpandir";
            this.mnuExpandir.Size = new System.Drawing.Size(190, 22);
            this.mnuExpandir.Text = "Expandir todo";
            this.mnuExpandir.Click += new System.EventHandler(this.mnuExpandir_Click);
            // 
            // niIcono
            // 
            this.niIcono.BalloonTipText = "La aplicación sigue ejecutándose";
            this.niIcono.BalloonTipTitle = "Contraseñas seguras";
            this.niIcono.Icon = ((System.Drawing.Icon)(resources.GetObject("niIcono.Icon")));
            this.niIcono.Text = "Contraseñas seguras";
            this.niIcono.Visible = true;
            this.niIcono.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niIcono_MouseDoubleClick);
            // 
            // ssBarraEstado
            // 
            this.ssBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstBE_NElementos});
            this.ssBarraEstado.Location = new System.Drawing.Point(0, 544);
            this.ssBarraEstado.Name = "ssBarraEstado";
            this.ssBarraEstado.Size = new System.Drawing.Size(855, 22);
            this.ssBarraEstado.TabIndex = 44;
            this.ssBarraEstado.Text = "statusStrip1";
            // 
            // tstBE_NElementos
            // 
            this.tstBE_NElementos.Name = "tstBE_NElementos";
            this.tstBE_NElementos.Size = new System.Drawing.Size(0, 17);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(808, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 23);
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(755, 2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(47, 23);
            this.btnTest.TabIndex = 45;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 566);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.ssBarraEstado);
            this.Controls.Add(this.tstBarraHerramientas);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.tviXML);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.Text = "Contraseñas seguras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Shown += new System.EventHandler(this.frmPrincipal_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrincipal_KeyDown);
            this.Resize += new System.EventHandler(this.frmPrincipal_Resize);
            this.tstBarraHerramientas.ResumeLayout(false);
            this.tstBarraHerramientas.PerformLayout();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ssBarraEstado.ResumeLayout(false);
            this.ssBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tviXML;
        private System.Windows.Forms.ToolStripButton tstBH_Nuevo;
        private System.Windows.Forms.ToolStripButton tstBH_Guardar;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label labComentarios;
        private System.Windows.Forms.Label labRuta;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label labContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label labUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.ToolStripButton tstBH_ContrasenaFichero;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuAñadirContraseña;
        private System.Windows.Forms.ToolStripMenuItem mnuAñadirCarpeta;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuRenombrar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuCortar;
        private System.Windows.Forms.ToolStripMenuItem mnuCopiar;
        private System.Windows.Forms.ToolStripMenuItem mnuPegar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuExpandir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tstBH_Cortar;
        private System.Windows.Forms.ToolStripButton tstBH_Copiar;
        private System.Windows.Forms.ToolStripButton tstBH_Pegar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tstBH_Carpeta;
        private System.Windows.Forms.ToolStripButton tstBH_Contrasena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tstBH_Buscar;
        private System.Windows.Forms.ToolStripComboBox tstBH_CadenaBusqueda;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tstBH_Configuracion;
        private System.Windows.Forms.ImageList imlXML;
        private System.Windows.Forms.ToolStripButton tstBH_Abrir;
        private System.Windows.Forms.Button btnCopiarUsuario;
        private System.Windows.Forms.Button btnLanzarURL;
        private System.Windows.Forms.Button btnCopiarURL;
        private System.Windows.Forms.Button btnCopiarContrasena;
        private System.Windows.Forms.ToolStripButton tstBH_Eliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSplitButton tstBH_Favoritos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStrip tstBarraHerramientas;
        private System.Windows.Forms.ToolStripButton tstBH_SkyDrive;
        private System.Windows.Forms.ToolStripSplitButton tstBH_Abrir_SB;
        private System.Windows.Forms.ToolStripMenuItem tstBH_Abrir_SB_Local;
        private System.Windows.Forms.ToolStripMenuItem tstBH_Abrir_SB_SkyDrive;
        private System.Windows.Forms.ToolStripSplitButton tstBH_Guardar_SB;
        private System.Windows.Forms.ToolStripMenuItem tstBH_Guardar_SB_Local;
        private System.Windows.Forms.ToolStripMenuItem tstBH_Guardar_SB_SkyDrive;
        private System.Windows.Forms.Label lbF7;
        private System.Windows.Forms.Label lbF6;
        private System.Windows.Forms.Label lbF5;
        private System.Windows.Forms.Label lbF4;
        private System.Windows.Forms.NotifyIcon niIcono;
        private System.Windows.Forms.StatusStrip ssBarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel tstBE_NElementos;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnTest;
    }
}


namespace ContraseñasSeguras.CapaPresentacion
{
    partial class frmSkyDriveSeleccionFichero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSkyDriveSeleccionFichero));
            this.tviCloud = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbFichero = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.lbAceptar = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbCancelar = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tviCloud
            // 
            this.tviCloud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tviCloud.ImageIndex = 0;
            this.tviCloud.ImageList = this.imageList1;
            this.tviCloud.Location = new System.Drawing.Point(0, 0);
            this.tviCloud.Name = "tviCloud";
            this.tviCloud.SelectedImageIndex = 0;
            this.tviCloud.Size = new System.Drawing.Size(574, 513);
            this.tviCloud.TabIndex = 1;
            this.tviCloud.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tviCloud_NodeMouseClick);
            this.tviCloud.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tviCloud_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.png");
            this.imageList1.Images.SetKeyName(1, "document.ico");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbFichero});
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(574, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbFichero
            // 
            this.lbFichero.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lbFichero.Name = "lbFichero";
            this.lbFichero.Size = new System.Drawing.Size(559, 17);
            this.lbFichero.Spring = true;
            this.lbFichero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbAceptar,
            this.lbCancelar});
            this.statusStrip2.Location = new System.Drawing.Point(0, 467);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(574, 24);
            this.statusStrip2.TabIndex = 3;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // lbAceptar
            // 
            this.lbAceptar.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbAceptar.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.lbAceptar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lbAceptar.Name = "lbAceptar";
            this.lbAceptar.Size = new System.Drawing.Size(279, 19);
            this.lbAceptar.Spring = true;
            this.lbAceptar.Text = "Aceptar";
            this.lbAceptar.Click += new System.EventHandler(this.lbAceptar_Click);
            // 
            // lbCancelar
            // 
            this.lbCancelar.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbCancelar.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.lbCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lbCancelar.Name = "lbCancelar";
            this.lbCancelar.Size = new System.Drawing.Size(279, 19);
            this.lbCancelar.Spring = true;
            this.lbCancelar.Text = "Cancelar";
            this.lbCancelar.Click += new System.EventHandler(this.lbCancelar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtNombre.Location = new System.Drawing.Point(0, 447);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(574, 20);
            this.txtNombre.TabIndex = 4;
            // 
            // frmSkyDriveSeleccionFichero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 513);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tviCloud);
            this.Name = "frmSkyDriveSeleccionFichero";
            this.ShowIcon = false;
            this.Text = "Selecciona fichero de contraseñas";
            this.Load += new System.EventHandler(this.frmSkyDriveSeleccionFichero_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tviCloud;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbFichero;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel lbAceptar;
        private System.Windows.Forms.ToolStripStatusLabel lbCancelar;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtNombre;
    }
}
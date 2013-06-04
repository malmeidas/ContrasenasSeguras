namespace ContraseñasSeguras.CapaPresentacion
{
    partial class frmSkyDriveLogin
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
            this.wbSkyDriveAuth = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbSkyDriveAuth
            // 
            this.wbSkyDriveAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbSkyDriveAuth.Location = new System.Drawing.Point(0, 0);
            this.wbSkyDriveAuth.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbSkyDriveAuth.Name = "wbSkyDriveAuth";
            this.wbSkyDriveAuth.Size = new System.Drawing.Size(341, 444);
            this.wbSkyDriveAuth.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstado});
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(341, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblEstado
            // 
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 17);
            // 
            // frmSkyDriveLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 444);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.wbSkyDriveAuth);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSkyDriveLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Autenticarse en SkyDrive";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbSkyDriveAuth;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblEstado;
    }
}
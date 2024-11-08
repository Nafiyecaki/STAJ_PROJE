namespace STAJ_PROJE
{
    partial class AnaSayfa
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cARİHESAPLARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mALZEMELERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fATURALARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iRSALİYELERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sİPARİŞLERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cARİHESAPLARToolStripMenuItem,
            this.mALZEMELERToolStripMenuItem,
            this.fATURALARToolStripMenuItem,
            this.iRSALİYELERToolStripMenuItem,
            this.sİPARİŞLERToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(961, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cARİHESAPLARToolStripMenuItem
            // 
            this.cARİHESAPLARToolStripMenuItem.Name = "cARİHESAPLARToolStripMenuItem";
            this.cARİHESAPLARToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.cARİHESAPLARToolStripMenuItem.Text = "CARİ HESAPLAR";
            this.cARİHESAPLARToolStripMenuItem.Click += new System.EventHandler(this.cARİHESAPLARToolStripMenuItem_Click);
            // 
            // mALZEMELERToolStripMenuItem
            // 
            this.mALZEMELERToolStripMenuItem.Name = "mALZEMELERToolStripMenuItem";
            this.mALZEMELERToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.mALZEMELERToolStripMenuItem.Text = "MALZEMELER";
            // 
            // fATURALARToolStripMenuItem
            // 
            this.fATURALARToolStripMenuItem.Name = "fATURALARToolStripMenuItem";
            this.fATURALARToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.fATURALARToolStripMenuItem.Text = "FATURALAR";
            this.fATURALARToolStripMenuItem.Click += new System.EventHandler(this.fATURALARToolStripMenuItem_Click);
            // 
            // iRSALİYELERToolStripMenuItem
            // 
            this.iRSALİYELERToolStripMenuItem.Name = "iRSALİYELERToolStripMenuItem";
            this.iRSALİYELERToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.iRSALİYELERToolStripMenuItem.Text = "İRSALİYELER";
            // 
            // sİPARİŞLERToolStripMenuItem
            // 
            this.sİPARİŞLERToolStripMenuItem.Name = "sİPARİŞLERToolStripMenuItem";
            this.sİPARİŞLERToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.sİPARİŞLERToolStripMenuItem.Text = "SİPARİŞLER";
            // 
            // AnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 450);
            this.Controls.Add(this.menuStrip1);
            this.Name = "AnaSayfa";
            this.Text = "AnaSayfa";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cARİHESAPLARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mALZEMELERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fATURALARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iRSALİYELERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sİPARİŞLERToolStripMenuItem;
    }
}
namespace STAJ_PROJE
{
    partial class malzemeler
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
            this.txtmlzadi = new System.Windows.Forms.TextBox();
            this.txtmlzkodu = new System.Windows.Forms.TextBox();
            this.text1 = new System.Windows.Forms.Label();
            this.text2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eKLEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEĞİŞTİRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sİLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtozelkod = new System.Windows.Forms.TextBox();
            this.txtyetkikodu = new System.Windows.Forms.TextBox();
            this.text3 = new System.Windows.Forms.Label();
            this.text4 = new System.Windows.Forms.Label();
            this.txtlistele = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtmlzadi
            // 
            this.txtmlzadi.Location = new System.Drawing.Point(110, 9);
            this.txtmlzadi.Name = "txtmlzadi";
            this.txtmlzadi.Size = new System.Drawing.Size(100, 22);
            this.txtmlzadi.TabIndex = 0;
            // 
            // txtmlzkodu
            // 
            this.txtmlzkodu.Location = new System.Drawing.Point(110, 56);
            this.txtmlzkodu.Name = "txtmlzkodu";
            this.txtmlzkodu.Size = new System.Drawing.Size(100, 22);
            this.txtmlzkodu.TabIndex = 1;
            // 
            // text1
            // 
            this.text1.AutoSize = true;
            this.text1.Location = new System.Drawing.Point(2, 15);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(91, 16);
            this.text1.TabIndex = 2;
            this.text1.Text = "Malzeme Adı :";
            // 
            // text2
            // 
            this.text2.AutoSize = true;
            this.text2.Location = new System.Drawing.Point(2, 56);
            this.text2.Name = "text2";
            this.text2.Size = new System.Drawing.Size(102, 16);
            this.text2.TabIndex = 3;
            this.text2.Text = "Malzeme Kodu :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridView1.Location = new System.Drawing.Point(-4, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(811, 385);
            this.dataGridView1.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eKLEToolStripMenuItem,
            this.dEĞİŞTİRToolStripMenuItem,
            this.sİLToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 76);
            // 
            // eKLEToolStripMenuItem
            // 
            this.eKLEToolStripMenuItem.Name = "eKLEToolStripMenuItem";
            this.eKLEToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.eKLEToolStripMenuItem.Text = "EKLE";
            this.eKLEToolStripMenuItem.Click += new System.EventHandler(this.eKLEToolStripMenuItem_Click_1);
            // 
            // dEĞİŞTİRToolStripMenuItem
            // 
            this.dEĞİŞTİRToolStripMenuItem.Name = "dEĞİŞTİRToolStripMenuItem";
            this.dEĞİŞTİRToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.dEĞİŞTİRToolStripMenuItem.Text = "DEĞİŞTİR";
            this.dEĞİŞTİRToolStripMenuItem.Click += new System.EventHandler(this.dEĞİŞTİRToolStripMenuItem_Click);
            // 
            // sİLToolStripMenuItem
            // 
            this.sİLToolStripMenuItem.Name = "sİLToolStripMenuItem";
            this.sİLToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.sİLToolStripMenuItem.Text = "SİL";
            this.sİLToolStripMenuItem.Click += new System.EventHandler(this.sİLToolStripMenuItem_Click);
            // 
            // txtozelkod
            // 
            this.txtozelkod.Location = new System.Drawing.Point(409, 12);
            this.txtozelkod.Name = "txtozelkod";
            this.txtozelkod.Size = new System.Drawing.Size(100, 22);
            this.txtozelkod.TabIndex = 2;
            // 
            // txtyetkikodu
            // 
            this.txtyetkikodu.Location = new System.Drawing.Point(409, 56);
            this.txtyetkikodu.Name = "txtyetkikodu";
            this.txtyetkikodu.Size = new System.Drawing.Size(100, 22);
            this.txtyetkikodu.TabIndex = 3;
            // 
            // text3
            // 
            this.text3.AutoSize = true;
            this.text3.Location = new System.Drawing.Point(297, 12);
            this.text3.Name = "text3";
            this.text3.Size = new System.Drawing.Size(61, 16);
            this.text3.TabIndex = 8;
            this.text3.Text = "Özel Kod";
            // 
            // text4
            // 
            this.text4.AutoSize = true;
            this.text4.Location = new System.Drawing.Point(297, 62);
            this.text4.Name = "text4";
            this.text4.Size = new System.Drawing.Size(71, 16);
            this.text4.TabIndex = 9;
            this.text4.Text = "Yetki Kodu";
            // 
            // txtlistele
            // 
            this.txtlistele.Location = new System.Drawing.Point(595, 39);
            this.txtlistele.Name = "txtlistele";
            this.txtlistele.Size = new System.Drawing.Size(100, 39);
            this.txtlistele.TabIndex = 4;
            this.txtlistele.Text = "LİSTELE";
            this.txtlistele.UseVisualStyleBackColor = true;
            this.txtlistele.Click += new System.EventHandler(this.txtlistele_Click);
            // 
            // malzemeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 490);
            this.Controls.Add(this.txtlistele);
            this.Controls.Add(this.text4);
            this.Controls.Add(this.text3);
            this.Controls.Add(this.txtyetkikodu);
            this.Controls.Add(this.txtozelkod);
            this.Controls.Add(this.text2);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.txtmlzkodu);
            this.Controls.Add(this.txtmlzadi);
            this.Controls.Add(this.dataGridView1);
            this.Name = "malzemeler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "malzemeler";
            this.Load += new System.EventHandler(this.malzemeler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtmlzadi;
        private System.Windows.Forms.TextBox txtmlzkodu;
        private System.Windows.Forms.Label text1;
        private System.Windows.Forms.Label text2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtozelkod;
        private System.Windows.Forms.TextBox txtyetkikodu;
        private System.Windows.Forms.Label text3;
        private System.Windows.Forms.Label text4;
        private System.Windows.Forms.Button txtlistele;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eKLEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dEĞİŞTİRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sİLToolStripMenuItem;
    }
}
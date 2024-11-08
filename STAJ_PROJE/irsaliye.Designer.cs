namespace STAJ_PROJE
{
    partial class irsaliyeler
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
            this.txtficheno = new System.Windows.Forms.TextBox();
            this.txtlistele = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eKLEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sİLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtdate = new System.Windows.Forms.DateTimePicker();
            this.txtcarikodu = new System.Windows.Forms.TextBox();
            this.txtcariadı = new System.Windows.Forms.TextBox();
            this.txttutar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtficheno
            // 
            this.txtficheno.Location = new System.Drawing.Point(110, 18);
            this.txtficheno.Name = "txtficheno";
            this.txtficheno.Size = new System.Drawing.Size(100, 22);
            this.txtficheno.TabIndex = 0;
            // 
            // txtlistele
            // 
            this.txtlistele.Location = new System.Drawing.Point(661, 51);
            this.txtlistele.Name = "txtlistele";
            this.txtlistele.Size = new System.Drawing.Size(106, 44);
            this.txtlistele.TabIndex = 2;
            this.txtlistele.Text = "LİSTELE";
            this.txtlistele.UseVisualStyleBackColor = true;
            this.txtlistele.Click += new System.EventHandler(this.txtlistele_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fiş Numarası :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tarih :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.Location = new System.Drawing.Point(-2, 136);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(799, 319);
            this.dataGridView1.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eKLEToolStripMenuItem,
            this.sİLToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(111, 52);
            // 
            // eKLEToolStripMenuItem
            // 
            this.eKLEToolStripMenuItem.Name = "eKLEToolStripMenuItem";
            this.eKLEToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.eKLEToolStripMenuItem.Text = "EKLE";
            this.eKLEToolStripMenuItem.Click += new System.EventHandler(this.eKLEToolStripMenuItem_Click_1);
            // 
            // sİLToolStripMenuItem
            // 
            this.sİLToolStripMenuItem.Name = "sİLToolStripMenuItem";
            this.sİLToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.sİLToolStripMenuItem.Text = "SİL";
            this.sİLToolStripMenuItem.Click += new System.EventHandler(this.sİLToolStripMenuItem_Click);
            // 
            // txtdate
            // 
            this.txtdate.Location = new System.Drawing.Point(110, 63);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(200, 22);
            this.txtdate.TabIndex = 5;
            // 
            // txtcarikodu
            // 
            this.txtcarikodu.Location = new System.Drawing.Point(480, 12);
            this.txtcarikodu.Name = "txtcarikodu";
            this.txtcarikodu.Size = new System.Drawing.Size(100, 22);
            this.txtcarikodu.TabIndex = 6;
            // 
            // txtcariadı
            // 
            this.txtcariadı.Location = new System.Drawing.Point(480, 51);
            this.txtcariadı.Name = "txtcariadı";
            this.txtcariadı.Size = new System.Drawing.Size(100, 22);
            this.txtcariadı.TabIndex = 7;
            // 
            // txttutar
            // 
            this.txttutar.Location = new System.Drawing.Point(480, 91);
            this.txttutar.Name = "txttutar";
            this.txttutar.Size = new System.Drawing.Size(100, 22);
            this.txttutar.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cari Kodu :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Cari Adı :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(391, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Tutar :";
            // 
            // irsaliyeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txttutar);
            this.Controls.Add(this.txtcariadı);
            this.Controls.Add(this.txtcarikodu);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtlistele);
            this.Controls.Add(this.txtficheno);
            this.Name = "irsaliyeler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "irsaliye";
            this.Load += new System.EventHandler(this.irsaliyeler_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtficheno;
        private System.Windows.Forms.Button txtlistele;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker txtdate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eKLEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sİLToolStripMenuItem;
        private System.Windows.Forms.TextBox txtcarikodu;
        private System.Windows.Forms.TextBox txtcariadı;
        private System.Windows.Forms.TextBox txttutar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
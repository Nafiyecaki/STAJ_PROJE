namespace STAJ_PROJE
{
    partial class malzemeler_ekle
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
            this.txtmlzkodu = new System.Windows.Forms.TextBox();
            this.txtmlzadi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnkaydet = new System.Windows.Forms.Button();
            this.txtozelkod = new System.Windows.Forms.TextBox();
            this.txtyetkikod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtmlzkodu
            // 
            this.txtmlzkodu.Location = new System.Drawing.Point(145, 31);
            this.txtmlzkodu.Name = "txtmlzkodu";
            this.txtmlzkodu.Size = new System.Drawing.Size(100, 22);
            this.txtmlzkodu.TabIndex = 0;
            // 
            // txtmlzadi
            // 
            this.txtmlzadi.Location = new System.Drawing.Point(145, 77);
            this.txtmlzadi.Name = "txtmlzadi";
            this.txtmlzadi.Size = new System.Drawing.Size(100, 22);
            this.txtmlzadi.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Malzeme Kodu :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Malzeme Adı :";
            // 
            // btnkaydet
            // 
            this.btnkaydet.Location = new System.Drawing.Point(569, 279);
            this.btnkaydet.Name = "btnkaydet";
            this.btnkaydet.Size = new System.Drawing.Size(148, 77);
            this.btnkaydet.TabIndex = 6;
            this.btnkaydet.Text = "KAYDET";
            this.btnkaydet.UseVisualStyleBackColor = true;
            this.btnkaydet.Click += new System.EventHandler(this.btnkaydet_Click);
            // 
            // txtozelkod
            // 
            this.txtozelkod.Location = new System.Drawing.Point(145, 129);
            this.txtozelkod.Name = "txtozelkod";
            this.txtozelkod.Size = new System.Drawing.Size(100, 22);
            this.txtozelkod.TabIndex = 7;
            // 
            // txtyetkikod
            // 
            this.txtyetkikod.Location = new System.Drawing.Point(145, 184);
            this.txtyetkikod.Name = "txtyetkikod";
            this.txtyetkikod.Size = new System.Drawing.Size(100, 22);
            this.txtyetkikod.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Özel Kod :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Yetki Kod :";
            // 
            // malzemeler_ekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtyetkikod);
            this.Controls.Add(this.txtozelkod);
            this.Controls.Add(this.btnkaydet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmlzadi);
            this.Controls.Add(this.txtmlzkodu);
            this.Name = "malzemeler_ekle";
            this.Text = "malzemeler_ekle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtmlzkodu;
        private System.Windows.Forms.TextBox txtmlzadi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnkaydet;
        private System.Windows.Forms.TextBox txtozelkod;
        private System.Windows.Forms.TextBox txtyetkikod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
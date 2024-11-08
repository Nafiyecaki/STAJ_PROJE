using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class GirisEkrani : Form
    {
        SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");

        public GirisEkrani()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterScreen; // Formu ekranın ortasında açar
            this.Visible = true;
            this.Opacity = 1.0;
            this.FormBorderStyle = FormBorderStyle.Sizable; // Pencere kenar çubuğunu ayarlar
            this.WindowState = FormWindowState.Normal; // Formu normal boyuta getirir
            this.Size = new Size(300, 200); // Formun boyutlarını ayarlayın
            // this.Location = new Point(100, 100); // Formun ekran içinde olduğundan emin olun

            this.Load += GirisEkrani_Load;
            this.KeyDown += GirisEkrani_KeyDown; // KeyDown olayı için event handler ekliyoruz

            // Formun ön planda görünmesini sağlar
            this.Activated += (s, e) =>
            {
                this.BringToFront();
            };
        }

        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            // Form yüklenirken yapılacak işlemler
        }

        private void btngiris_Click_1(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string sql = "SELECT * FROM users WHERE Kullanici_Adi=@adi AND Sifre=@sifresi";
                SqlParameter prm1 = new SqlParameter("@adi", txtkullaniciadi.Text.Trim());
                SqlParameter prm2 = new SqlParameter("@sifresi", txtsifre.Text.Trim());
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    AnaSayfa anasayfa = new AnaSayfa(); // Faturalar formunu oluşturuyoruz
                    anasayfa.Show(); // Faturalar formunu gösteriyoruz
                   // this.Hide(); // Giriş ekranını gizleyebilirsiniz
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalıdır");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş işlemi başarısız oldu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void GirisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btngiris_Click_1(sender, e); // Enter tuşuna basıldığında butonun Click olayını tetikler
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class malzemeler : Form
    {
        SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");

        public malzemeler()
        {
            InitializeComponent();
        }

        public void KayitGetir2(string MalzemeKodu = null, string MalzemeAdı = null, string ÖzelKod = null, string YetkiKodu = null)
        {
            try
            {
                baglanti.Open();

                string kayit = @"
                    SELECT ITM.CODE AS 'MALZEME KODU', ITM.NAME AS 'MALZEME ADI', ITM.SPECODE AS 'ÖZEL KOD', ITM.CYPHCODE AS 'YETKİ KODU'
                    FROM LG_001_ITEMS ITM
                    WHERE (@MalzemeKodu IS NULL OR ITM.CODE LIKE @MalzemeKodu)
                    AND (@MalzemeAdı IS NULL OR ITM.NAME LIKE @MalzemeAdı)
                    AND (@ÖzelKod IS NULL OR ITM.SPECODE LIKE @ÖzelKod)
                    AND (@YetkiKodu IS NULL OR ITM.CYPHCODE LIKE @YetkiKodu)";

                using (SqlCommand komut = new SqlCommand(kayit, baglanti))
                {
                    komut.Parameters.AddWithValue("@MalzemeKodu", string.IsNullOrEmpty(MalzemeKodu) ? (object)DBNull.Value : "%" + MalzemeKodu + "%");
                    komut.Parameters.AddWithValue("@MalzemeAdı", string.IsNullOrEmpty(MalzemeAdı) ? (object)DBNull.Value : "%" + MalzemeAdı + "%");
                    komut.Parameters.AddWithValue("@ÖzelKod", string.IsNullOrEmpty(ÖzelKod) ? (object)DBNull.Value : "%" + ÖzelKod + "%");
                    komut.Parameters.AddWithValue("@YetkiKodu", string.IsNullOrEmpty(YetkiKodu) ? (object)DBNull.Value : "%" + YetkiKodu + "%");

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri getirme hatası: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void txtlistele_Click(object sender, EventArgs e)
        {
            string mlzkodu = txtmlzkodu.Text.Trim();
            string mlzadi = txtmlzadi.Text.Trim();
            string ozelkod = txtozelkod.Text.Trim();
            string yetkikodu = txtyetkikodu.Text.Trim();
            KayitGetir2(mlzkodu, mlzadi, ozelkod, yetkikodu);
        }

        private void eKLEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (malzemeler_ekle mlze = new malzemeler_ekle())
            {
                if (mlze.ShowDialog() == DialogResult.OK)
                {
                    KayitGetir2(); // Yeni kayıt eklendikten sonra DataGridView'ı güncelle
                }
            }
        }

        private void dEĞİŞTİRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string malzemeKodu = selectedRow.Cells["MALZEME KODU"].Value.ToString();

                using (malzemeler_ekle mlze = new malzemeler_ekle(malzemeKodu))
                {
                    if (mlze.ShowDialog() == DialogResult.OK)
                    {
                        KayitGetir2(); // Güncelleme sonrası DataGridView'ı güncelle
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin.");
            }
        }

        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string malzemeKodu = selectedRow.Cells["MALZEME KODU"].Value.ToString();

                var result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True"))
                    {
                        try
                        {
                            baglanti.Open();
                            string deleteQuery = "DELETE FROM LG_001_ITEMS WHERE CODE = @CODE";

                            using (SqlCommand komut = new SqlCommand(deleteQuery, baglanti))
                            {
                                komut.Parameters.AddWithValue("@CODE", malzemeKodu);
                                int rowsAffected = komut.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Kayıt başarıyla silindi.");
                                    KayitGetir2(); // Kayıt silindikten sonra DataGridView'ı güncelle
                                }
                                else
                                {
                                    MessageBox.Show("Silinecek kayıt bulunamadı.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Veri silme hatası: " + ex.Message);
                        }
                        finally
                        {
                            if (baglanti.State == ConnectionState.Open)
                            {
                                baglanti.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin.");
            }
        }

        private void malzemeler_Load(object sender, EventArgs e)
        {
            KayitGetir2(); // Form yüklendiğinde kayıtları getir
        }
    }
}

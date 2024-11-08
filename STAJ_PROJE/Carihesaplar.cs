using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class Carihesaplar : Form
    {
        private SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");

        public Carihesaplar()
        {
            InitializeComponent();
        }

        public void KayitGetir1(string CariKodu = null, string CariAdi = null, string Adres1 = null, string Şehir = null, string Ülke = null, string TelefonNumarası = null, string Mail = null, string KimlikNumarası = null)
        {
            try
            {
                baglanti.Open();
                string kayit = @"
                    SELECT CLC.CODE AS 'CARİ KODU', CLC.DEFINITION_ AS 'CARİ ADI', CLC.ADDR1 'ADRES 1', CLC.CITY 'ŞEHİR', CLC.COUNTRY 'ÜLKE', CLC.TELNRS1 'TELEFON NUMARASI',
                           CLC.EMAILADDR 'MAİL', CLC.TAXOFFCODE 'KİMLİK NUMARASI'
                    FROM LG_001_CLCARD CLC
                    WHERE (@CariKodu IS NULL OR CLC.CODE LIKE @CariKodu)
                    AND (@CariAdi IS NULL OR CLC.DEFINITION_ LIKE @CariAdi)
                    AND (@Adres1 IS NULL OR CLC.ADDR1 LIKE @Adres1)
                    AND (@Şehir IS NULL OR CLC.CITY LIKE @Şehir)
                    AND (@Ülke IS NULL OR CLC.COUNTRY LIKE @Ülke)
                    AND (@TelefonNumarası IS NULL OR CLC.TELNRS1 LIKE @TelefonNumarası)
                    AND (@Mail IS NULL OR CLC.EMAILADDR LIKE @Mail)
                    AND (@KimlikNumarası IS NULL OR CLC.TAXOFFCODE LIKE @KimlikNumarası)";

                using (SqlCommand komut = new SqlCommand(kayit, baglanti))
                {
                    komut.Parameters.AddWithValue("@CariKodu", string.IsNullOrEmpty(CariKodu) ? (object)DBNull.Value : "%" + CariKodu + "%");
                    komut.Parameters.AddWithValue("@CariAdi", string.IsNullOrEmpty(CariAdi) ? (object)DBNull.Value : "%" + CariAdi + "%");
                    komut.Parameters.AddWithValue("@Adres1", string.IsNullOrEmpty(Adres1) ? (object)DBNull.Value : "%" + Adres1 + "%");
                    komut.Parameters.AddWithValue("@Şehir", string.IsNullOrEmpty(Şehir) ? (object)DBNull.Value : "%" + Şehir + "%");
                    komut.Parameters.AddWithValue("@Ülke", string.IsNullOrEmpty(Ülke) ? (object)DBNull.Value : "%" + Ülke + "%");
                    komut.Parameters.AddWithValue("@TelefonNumarası", string.IsNullOrEmpty(TelefonNumarası) ? (object)DBNull.Value : "%" + TelefonNumarası + "%");
                    komut.Parameters.AddWithValue("@Mail", string.IsNullOrEmpty(Mail) ? (object)DBNull.Value : "%" + Mail + "%");
                    komut.Parameters.AddWithValue("@KimlikNumarası", string.IsNullOrEmpty(KimlikNumarası) ? (object)DBNull.Value : "%" + KimlikNumarası + "%");

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
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

        private void Carihesaplar_Load(object sender, EventArgs e)
        {
            KayitGetir1();
        }

        private void btnlistele1_Click(object sender, EventArgs e)
        {
            string carikodu = txtcarikodu.Text.Trim();
            string cariadi = txtcariadi.Text.Trim();
            KayitGetir1(carikodu, cariadi);
        }

        private void eKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Cari_Hesaplar_Ekle che = new Cari_Hesaplar_Ekle())
            {
                if (che.ShowDialog() == DialogResult.OK)
                {
                    KayitGetir1();
                }
            }
        }

        private void dEĞİŞTİRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                string cariKodu = selectedRow.Cells["CARİ KODU"].Value.ToString();

                using (Cari_Hesaplar_Ekle che = new Cari_Hesaplar_Ekle(cariKodu))
                {
                    if (che.ShowDialog() == DialogResult.OK)
                    {
                        KayitGetir1();
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
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                string cariKodu = selectedRow.Cells["CARİ KODU"].Value.ToString();

                var result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True"))
                    {
                        try
                        {
                            baglanti.Open();
                            string deleteQuery = "DELETE FROM LG_001_CLCARD WHERE CODE = @CODE";

                            using (SqlCommand komut = new SqlCommand(deleteQuery, baglanti))
                            {
                                komut.Parameters.AddWithValue("@CODE", cariKodu);
                                int rowsAffected = komut.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Kayıt başarıyla silindi.");
                                    KayitGetir1();
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
    }
}

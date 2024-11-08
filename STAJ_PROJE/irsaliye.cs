using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class irsaliyeler : Form
    {
        SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");

        public irsaliyeler()
        {
            InitializeComponent();
        }

        public void KayitGetir3(string fisNumarasi = null, DateTime? Tarih = null, string cariKodu = null, string cariAdı = null, string tutar = null)
        {
            try
            {
                baglanti.Open();


                string kayit = @"
            SELECT
                STF.FICHENO AS 'FİŞ NUMARASI',
                STF.DATE_ AS 'TARİH',
                CLC.CODE AS 'CARİ KODU',
                CLC.DEFINITION_ AS 'CARİ ADI',
                STL.AMOUNT AS 'MİKTAR',
                STL.PRICE AS 'BİRİM FİYAT',
                STL.PRICE AS 'TUTAR'
            FROM LG_001_01_STLINE STL
                INNER JOIN LG_001_01_STFICHE STF ON STL.STFICHEREF = STF.LOGICALREF
                LEFT JOIN LG_001_CLCARD CLC ON STF.CLIENTREF = CLC.LOGICALREF
                LEFT JOIN LG_001_ITEMS ITM ON STL.STOCKREF = ITM.LOGICALREF
                LEFT JOIN LG_001_01_INVOICE INV ON STL.INVOICEREF = INV.LOGICALREF
            WHERE
              (@fisNumarasi IS NULL OR STF.FICHENO LIKE '%' + @fisNumarasi + '%')
                AND (@Tarih IS NULL OR STF.DATE_ = @Tarih)
                AND (@cariKodu IS NULL OR CLC.CODE LIKE '%' + @cariKodu + '%')
                AND (@cariAdi IS NULL OR CLC.DEFINITION_ LIKE '%' + @cariAdi + '%')
                AND (@tutar IS NULL OR STL.PRICE = @tutar)";

                using (SqlCommand komut = new SqlCommand(kayit, baglanti))
                {
                    komut.Parameters.AddWithValue("@fisNumarasi", string.IsNullOrEmpty(fisNumarasi) ? (object)DBNull.Value : fisNumarasi);
                    komut.Parameters.AddWithValue("@Tarih", Tarih.HasValue ? (object)Tarih.Value.Date : DBNull.Value);
                    komut.Parameters.AddWithValue("@cariKodu", string.IsNullOrEmpty(cariKodu) ? (object)DBNull.Value : cariKodu);
                    komut.Parameters.AddWithValue("@cariAdi", string.IsNullOrEmpty(cariAdı) ? (object)DBNull.Value : cariAdı);

                    // Tutar parametresinin ayarlanması
                    if (string.IsNullOrEmpty(tutar))
                    {
                        komut.Parameters.AddWithValue("@tutar", (object)DBNull.Value);
                    }
                    else
                    {
                        if (decimal.TryParse(tutar, out decimal tutarValue))
                        {
                            komut.Parameters.AddWithValue("@tutar", tutarValue);
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@tutar", (object)DBNull.Value);
                        }
                    }

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


private void txtlistele_Click_1(object sender, EventArgs e)
        {
            string fisNumarasi = txtficheno.Text.Trim();
            DateTime? Tarih = null; // Tarih parametresini burada tanımlıyoruz.
            string cariKodu = txtcarikodu.Text.Trim();
            string cariAdı = txtcariadı.Text.Trim();
            string tutar = txttutar.Text.Trim();
            KayitGetir3(fisNumarasi, Tarih , cariKodu , cariAdı , tutar);
        }

        private void eKLEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (irsaliye_Ekle ire = new irsaliye_Ekle())
            {
                if (ire.ShowDialog() == DialogResult.OK)
                {
                    KayitGetir3();
                }
            }
        }

        

        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count > 0) // Eğer dataGridView1'de herhangi bir satır seçilmişse
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // Seçilen ilk satırı al
                    string fisNumarasi = selectedRow.Cells["FİŞ NUMARASI"].Value.ToString(); // Seçilen satırdaki "CARİ KODU" hücresinin değerini al ve bir string değişkene ata

                    var result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True"))
                        {
                            try
                            {
                                baglanti.Open();
                                string deleteQuery = "DELETE FROM LG_001_01_STFICHE WHERE FICHENO = @FICHENO";

                                using (SqlCommand komut = new SqlCommand(deleteQuery, baglanti))
                                {
                                    komut.Parameters.AddWithValue("@FICHENO", fisNumarasi);
                                    int rowsAffected = komut.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Kayıt başarıyla silindi.");
                                        KayitGetir3(); // Kayıt silindikten sonra DataGridView'ı güncelle
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
                    MessageBox.Show("Lütfen bir satır seçin."); // Eğer hiçbir satır seçilmemişse, kullanıcıya bir uyarı mesajı göster
                }
            }
        }

        private void irsaliyeler_Load_1(object sender, EventArgs e)
        {
            KayitGetir3();
        }
    }
}

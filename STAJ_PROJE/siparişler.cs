using System; 
using System.Data; 
using System.Data.SqlClient; 
using System.Windows.Forms; 

namespace STAJ_PROJE
{
    public partial class siparişler : Form
    {
        
        SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");
        
        public siparişler()
        {
            InitializeComponent(); 
        }

        
        private void KayitGetir4(string fişNumarası = null, DateTime? Tarih = null, string cariKodu = null, string cariAdı = null, string tutar = null)
        {
            try
            {
                
                baglanti.Open();

               
                string kayit = @"
SELECT 
    ORF.FICHENO AS 'FİŞ NUMARASI', 
    ORF.DATE_ AS 'TARİH', 
    CLC.CODE AS 'CARİ KODU', 
    CLC.DEFINITION_ AS 'CARİ ADI',
    ITM.CODE AS 'MALZEME KODU',
    ITM.NAME AS 'MALZEME ADI',
    ORL.AMOUNT AS 'MİKTAR',
    ORL.PRICE AS 'BİRİM FİYAT', 
    ORL.TOTAL AS 'TUTAR'
FROM LG_001_01_ORFLINE ORL
INNER JOIN LG_001_01_ORFICHE ORF ON  ORL.ORDFICHEREF= ORF.LOGICALREF
INNER JOIN LG_001_CLCARD CLC ON ORF.CLIENTREF = CLC.LOGICALREF
INNER JOIN LG_001_ITEMS ITM ON ORL.STOCKREF = ITM.LOGICALREF
WHERE 
    (@fisNumarasi IS NULL OR ORF.FICHENO LIKE '%' + @fisNumarasi + '%')
    AND (@Tarih IS NULL OR CONVERT(DATE, ORF.DATE_) = @Tarih)
    AND (@cariKodu IS NULL OR CLC.CODE LIKE '%' + @cariKodu + '%')
    AND (@cariAdi IS NULL OR CLC.DEFINITION_ LIKE '%' + @cariAdi + '%')
    AND (@tutar IS NULL OR ORL.TOTAL = @tutar);
";

                
                using (SqlCommand komut = new SqlCommand(kayit, baglanti))
                {
                    komut.Parameters.AddWithValue("@fisNumarasi", string.IsNullOrEmpty(fişNumarası) ? (object)DBNull.Value : fişNumarası);
                    komut.Parameters.AddWithValue("@Tarih", Tarih.HasValue ? (object)Tarih.Value.Date : DBNull.Value);
                    komut.Parameters.AddWithValue("@cariKodu", string.IsNullOrEmpty(cariKodu) ? (object)DBNull.Value : cariKodu);
                    komut.Parameters.AddWithValue("@cariAdi", string.IsNullOrEmpty(cariAdı) ? (object)DBNull.Value : cariAdı);

                    
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
                MessageBox.Show("Veri getirme hatası: " + ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
              
                    baglanti.Close();
                
            }
        }

        private void txtlistele_Click(object sender, EventArgs e)
        {
            
            string fişNumarası = txtficheno.Text.Trim();
            DateTime? Tarih = null; 
            string cariKodu = txtcarikodu.Text.Trim();
            string cariAdı = txtcariadı.Text.Trim();
            string tutar = txttutar.Text.Trim();
            
            KayitGetir4(fişNumarası, Tarih, cariKodu, cariAdı, tutar);
        }

        private void siparişler_Load(object sender, EventArgs e)
        {
            KayitGetir4(); 
        }

        private void eKLEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (siparişler_ekle se = new siparişler_ekle())
            {
                if (se.ShowDialog() == DialogResult.OK)
                {
                    KayitGetir4(); 
                }
            }
        }

      
        private void sİLToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string fisNumarasi = selectedRow.Cells["FİŞ NUMARASI"].Value.ToString();

                var result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True"))
                    {
                        try
                        {
                            baglanti.Open();
                            string deleteQuery = "DELETE FROM LG_001_01_ORFICHE WHERE FICHENO = @FICHENO";

                            using (SqlCommand komut = new SqlCommand(deleteQuery, baglanti))
                            {
                                komut.Parameters.AddWithValue("@FICHENO", fisNumarasi);
                                int rowsAffected = komut.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Kayıt başarıyla silindi.");
                                    KayitGetir4();
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace STAJ_PROJE
{
    public partial class Faturalar : Form
    {
        SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");

        public Faturalar()
        {
            InitializeComponent();
        }

        private void KayitGetir2(string fisNo = null)
        {

            try
            {
                baglanti.Open();
                string kayit = @"
SELECT 
    INV.DATE_ AS 'TARİH', INV.FICHENO AS 'FİŞ NUMARASI',
    CLC.CODE AS 'CARİ KOD', CLC.DEFINITION_ AS 'CARİ ADI',
    ITM.CODE AS 'MALZEME KODU', ITM.NAME AS 'MALZEME ADI', 
    STL.AMOUNT AS 'MİKTAR', STL.PRICE AS 'BİRİM FİYAT', STL.TOTAL AS 'TOPLAM TUTAR'
FROM LG_001_01_INVOICE INV
LEFT JOIN LG_001_CLCARD CLC ON INV.CLIENTREF = CLC.LOGICALREF
LEFT JOIN LG_001_01_STLINE STL ON INV.LOGICALREF = STL.INVOICEREF
LEFT JOIN LG_001_ITEMS ITM ON STL.STOCKREF = ITM.LOGICALREF";



                using (SqlCommand komut = new SqlCommand(kayit, baglanti))
                {
                    komut.Parameters.AddWithValue("@FICHENO", string.IsNullOrEmpty(fisNo) ? (object)DBNull.Value : fisNo);

               


                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    fatura.DataSource = dt;
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

        private void btnlistele_Click_1(object sender, EventArgs e)
        {
            string fisNo = txtfisno.Text.Trim();
            KayitGetir2(fisNo);
        }




        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fatura.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = fatura.SelectedRows[0];
                string fisNumarasi = selectedRow.Cells["FİŞ NUMARASI"].Value.ToString();

                var result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True"))
                    {
                        try
                        {
                            baglanti.Open();
                            string deleteQuery = " DELETE FROM LG_001_01_STLINE " +
                                " WHERE INVOICEREF = (SELECT LOGICALREF FROM LG_001_01_INVOICE WHERE FICHENO = @FICHENO); " +
                                " DELETE FROM LG_001_01_INVOICE    WHERE FICHENO = @FICHENO";

                            using (SqlCommand komut = new SqlCommand(deleteQuery, baglanti))
                            {
                                komut.Parameters.AddWithValue("@FICHENO", fisNumarasi);
                                int rowsAffected = komut.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Kayıt başarıyla silindi.");
                                    KayitGetir2();
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

        private void Faturalar_Load(object sender, EventArgs e)
        {
            KayitGetir2();
        }

        private void eKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (faturalar_Ekle fe = new faturalar_Ekle())
            {
                if (fe.ShowDialog() == DialogResult.OK)
                {
                    KayitGetir2(); // Yeni kayıt eklendikten sonra DataGridView'ı güncelle
                }
            }
        }
    }
}


    


using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class Faturalar : Form
    {
        SqlConnection baglanti = new SqlConnection(@"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True");

        public Faturalar()
        {
            InitializeComponent();
        }

        public void KayitGetir(string fisNo = null)
        {
            try
            {
                baglanti.Open();
                string kayit = @"
                    SELECT INV.DATE_ 'TARİH', INV.FICHENO 'FİŞ NUMARASI',
 CLC.CODE 'CARİ KOD', CLC.DEFINITION_ 'CARİ ADI', INV.NETTOTAL 'TUTAR',

CASE INV.TRCODE
WHEN 1 THEN 'Satınalma Faturası'
WHEN 2 THEN 'Perakende Satış İade Faturası'
WHEN 3 THEN 'Toptan Satış İade Faturası'
WHEN 4 THEN 'Alınan Hizmet Faturası'
WHEN 7 THEN 'Perakende Satış  Faturası'
WHEN 8 THEN 'Toptan Satış  Faturası'
WHEN 9 THEN 'Verilen Hizmet Faturası'
WHEN 10 THEN 'Verilen Platform Faturası'
WHEN 14 THEN 'Satış Fiyat Farkı Faturası'
END AS 'FATURA TİPİ',

CASE INV.GRPCODE
WHEN 1 THEN 'ALIŞ FATURASI'
WHEN 2 THEN 'SATIŞ FATURASI'
END AS 'GRUP TÜRÜ'
FROM LG_001_01_INVOICE INV
JOIN LG_001_CLCARD CLC ON CLC.LOGICALREF = INV.CLIENTREF
WHERE INV.FICHENO LIKE @fisNo
";

                using (SqlCommand komut = new SqlCommand(kayit, baglanti))
                {
                    // Fiş numarasına göre sorgu filtreleme
                    komut.Parameters.AddWithValue("@fisNo", string.IsNullOrEmpty(fisNo) ? "%" : "%" + fisNo + "%");

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt; // DataGridView'i güncelle
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

        private void Form2_Load_1(object sender, EventArgs e)
        {
            KayitGetir(); // Form yüklendiğinde tüm kayıtları getir
        }

       

        private void btnlistele_Click_1(object sender, EventArgs e)
        {
            // txtfisnoya TextBox'ındaki değeri alarak KayitGetir metoduna gönderir
            string fisNo = txtfisno.Text.Trim();
            KayitGetir(fisNo);
        }
    }
}

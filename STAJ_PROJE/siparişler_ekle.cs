using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace STAJ_PROJE

{
    public partial class siparişler_ekle : Form
    {
        private string connectionString = @"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True";
        private string fişNumarası;
        private Dictionary<string, string> malzemeKodToAdMap = new Dictionary<string, string>();
        private Dictionary<string, string> cariKodToUnvanMap = new Dictionary<string, string>();



        public siparişler_ekle() : this(null) { }

        public siparişler_ekle(string fişNumarası)
        {
            InitializeComponent();
            this.fişNumarası = fişNumarası;

            if (!string.IsNullOrEmpty(fişNumarası))
            {
                VerileriGetir(fişNumarası);
            }
        }

        private void VerileriGetir(string fişNumarası)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = @"
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
    FROM LG_001_01_ORFICHE ORF 
    LEFT JOIN LG_001_CLCARD CLC ON ORF.CLIENTREF = CLC.LOGICALREF
    LEFT JOIN LG_001_01_ORFLINE ORL ON ORF.LOGICALREF = ORL.ORDFICHEREF
    LEFT JOIN LG_001_ITEMS ITM ON ORL.STOCKREF = ITM.LOGICALREF
    WHERE ORF.FICHENO = @FICHENO";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FICHENO", fişNumarası);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())  // Bu kontrolü kullanabilirsiniz
                            {
                                while (reader.Read())
                                {
                                    txtfisnumarasi.Text = reader["FİŞ NUMARASI"].ToString();
                                    dateTimePicker1.Value = Convert.ToDateTime(reader["TARİH"]);
                                    comboBox1.Text = reader["CARİ KODU"].ToString();
                                    comboBox2.Text = reader["CARİ ADI"].ToString();
                                    comboBox3.Text = reader["MALZEME KODU"].ToString();
                                    comboBox4.Text = reader["MALZEME ADI"].ToString();
                                    txtmiktar.Text = reader["MİKTAR"].ToString();
                                    txtbirimfiyat.Text = reader["BİRİM FİYAT"].ToString();
                                    txttutar.Text = reader["TUTAR"].ToString();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Kayıt bulunamadı.");
                            }
                        }
                    }

                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Veri getirme hatası: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Genel hata: " + ex.Message);
                }
            }
        }
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            string FisNumarası = txtfisnumarasi.Text.Trim();
            DateTime Tarih = dateTimePicker1.Value;
            string cariKod = comboBox1.SelectedItem?.ToString();
            string malzemeKodu = comboBox3.SelectedItem?.ToString();
            decimal miktar;
            decimal birimFiyat;
            decimal toplamTutar;

            // Veri doğrulama
            if (!decimal.TryParse(txtmiktar.Text.Trim(), out miktar) ||
                !decimal.TryParse(txtbirimfiyat.Text.Trim(), out birimFiyat) ||
                !decimal.TryParse(txttutar.Text.Trim(), out toplamTutar))
            {
                MessageBox.Show("Miktar, birim fiyat veya toplam tutar geçersiz.");
                return;
            }

            if (string.IsNullOrEmpty(cariKod) || string.IsNullOrEmpty(malzemeKodu))
            {
                MessageBox.Show("Cari kodu ve malzeme kodu seçilmelidir.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    
                    string insertOrficheQuery = @"
                    INSERT INTO LG_001_01_ORFICHE (FICHENO, DATE_, CLIENTREF)
                    VALUES (@FICHENO, @DATE_, (SELECT LOGICALREF FROM LG_001_CLCARD WHERE CODE = @CARICODE))";

                    using (SqlCommand insertOrficheCommand = new SqlCommand(insertOrficheQuery, connection, transaction))
                    {
                        insertOrficheCommand.Parameters.AddWithValue("@FICHENO", FisNumarası);
                        insertOrficheCommand.Parameters.AddWithValue("@DATE_", Tarih);
                        insertOrficheCommand.Parameters.AddWithValue("@CARICODE", cariKod);
                        insertOrficheCommand.ExecuteNonQuery();
                    }

                    
                    string insertDetailQuery = @"
                    INSERT INTO LG_001_01_ORFLINE (ORDFICHEREF, AMOUNT, PRICE, TOTAL, STOCKREF)
                    VALUES (@ORDFICHEREF,
                        (SELECT LOGICALREF FROM LG_001_01_ORFICHE WHERE CODE = @STOCKREF),
                        @AMOUNT, 
                        @PRICE, 
                        @TOTAL,
                        (SELECT LOGICALREF FROM LG_001_ITEMS WHERE CODE = @ITEMCODE))";

                    using (SqlCommand insertDetailCommand = new SqlCommand(insertDetailQuery, connection, transaction))
                    {
                        insertDetailCommand.Parameters.AddWithValue("@STOCKREF", cariKod);
                        insertDetailCommand.Parameters.AddWithValue("@AMOUNT", miktar);
                        insertDetailCommand.Parameters.AddWithValue("@PRICE", birimFiyat);
                        insertDetailCommand.Parameters.AddWithValue("@TOTAL", toplamTutar);
                        insertDetailCommand.Parameters.AddWithValue("@ITEMCODE", malzemeKodu);
                        insertDetailCommand.ExecuteNonQuery();
                    }

                    
                    transaction.Commit();
                    MessageBox.Show("Kayıt başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    transaction?.Rollback();
                    MessageBox.Show("Veri kaydetme hatası: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show("Genel hata: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCariKod = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedCariKod) && cariKodToUnvanMap.ContainsKey(selectedCariKod))
            {
                comboBox2.SelectedItem = cariKodToUnvanMap[selectedCariKod];
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCariAd = comboBox2.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedCariAd) && cariKodToUnvanMap.ContainsValue(selectedCariAd))
            {
                foreach (var kvp in cariKodToUnvanMap)
                {
                    if (kvp.Value == selectedCariAd)
                    {
                        comboBox1.SelectedItem = kvp.Key;
                        break;
                    }
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMalzemeKodu = comboBox3.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedMalzemeKodu) && malzemeKodToAdMap.ContainsKey(selectedMalzemeKodu))
            {
                comboBox4.SelectedItem = malzemeKodToAdMap[selectedMalzemeKodu];
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMalzemeAdı = comboBox4.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedMalzemeAdı) && malzemeKodToAdMap.ContainsValue(selectedMalzemeAdı))
            {
                foreach (var kvp in malzemeKodToAdMap)
                {
                    if (kvp.Value == selectedMalzemeAdı)
                    {
                        comboBox3.SelectedItem = kvp.Key;
                        break;
                    }
                }
            }
        }

        private void siparişler_ekle_Load(object sender, EventArgs e)
        {
            ComboboxDoldur(); 
            MalzemeDoldur(); 

            if (!string.IsNullOrEmpty(fişNumarası))
            {
                VerileriGetir(fişNumarası); 
            }
        }
        private void ComboboxDoldur()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                   
                    string query = "SELECT CODE, DEFINITION_ FROM LG_001_CLCARD";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string kod = reader["CODE"].ToString();
                                string unvan = reader["DEFINITION_"].ToString();
                                comboBox1.Items.Add(kod);
                                comboBox2.Items.Add(unvan);
                                cariKodToUnvanMap[kod] = unvan;
                                cariKodToUnvanMap[unvan] = kod;  
                            }
                        }
                    }

                    
                    query = "SELECT CODE, NAME FROM LG_001_ITEMS";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string kod = reader["CODE"].ToString();
                                string ad = reader["NAME"].ToString();
                                comboBox3.Items.Add(kod);
                                comboBox4.Items.Add(ad);
                                malzemeKodToAdMap[kod] = ad;
                                malzemeKodToAdMap[ad] = kod;  
                            }
                        }
                    }

                  
                    comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                    comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                    comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Combobox verileri getirme hatası: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Genel hata: " + ex.Message);
                }
            }
        }

        private void MalzemeDoldur()
        {
            
            comboBox3.SelectedIndexChanged += (s, e) =>
            {
                string selectedKodu = comboBox3.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedKodu) && malzemeKodToAdMap.ContainsKey(selectedKodu))
                {
                    comboBox4.SelectedItem = malzemeKodToAdMap[selectedKodu];
                }
            };

           
            comboBox4.SelectedIndexChanged += (s, e) =>
            {
                string selectedAd = comboBox4.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedAd) && malzemeKodToAdMap.ContainsKey(selectedAd))
                {
                    comboBox3.SelectedItem = malzemeKodToAdMap[selectedAd];
                }
            };
        }
    }
}

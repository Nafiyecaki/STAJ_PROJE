using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace STAJ_PROJE
{
    public partial class irsaliye_Ekle : Form
    {
        private string connectionString = @"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True";
        private string fişNumarası;
        private Dictionary<string, string> cariKodToUnvanMap = new Dictionary<string, string>();
        private Dictionary<string, string> unvanToCariKodMap = new Dictionary<string, string>();

        public irsaliye_Ekle() : this(null) { }

        public irsaliye_Ekle(string fişNumarası)
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
            SELECT STF.FICHENO AS 'FİŞ NUMARASI', STF.DATE_ AS 'TARİH', 
                   CLC.CODE AS 'CARİ KODU', CLC.DEFINITION_ AS 'CARİ ADI', 
                   STL.AMOUNT AS 'MİKAR', STL.PRICE AS 'BİRİM FİYAT', STL.TOTAL AS 'TUTAR'
            FROM LG_001_01_STFICHE STF
          LEFT JOIN LG_001_CLCARD CLC ON STF.CLIENTREF = CLC.LOGICALREF
            LEFT JOIN LG_001_01_STLINE STL ON STF.LOGICALREF = STL.STFICHEREF
            LEFT JOIN LG_001_ITEMS ITM ON STL.STOCKREF = ITM.LOGICALREF
            LEFT JOIN LG_001_01_INVOICE INV ON STL.INVOICEREF = INV.LOGICALREF
            WHERE STF.FICHENO = @FICHENO";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FICHENO", fişNumarası);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                while (reader.Read())
                                {
                                    txtfisnumarasi.Text = reader["FİŞ NUMARASI"].ToString();
                                    dateTimePicker1.Value = Convert.ToDateTime(reader["TARİH"]);
                                    comboBox1.Text = reader["CARİ KODU"].ToString();
                                    comboBox2.Text = reader["CARİ ADI"].ToString();
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
            string yeniFisNumarası = txtfisnumarasi.Text.Trim();
            DateTime Tarih = dateTimePicker1.Value;
            string cariKod = comboBox1.SelectedItem?.ToString();
            decimal miktar;
            decimal birimFiyat;
            decimal toplamTutar;

            if (!decimal.TryParse(txtmiktar.Text.Trim(), out miktar) ||
                !decimal.TryParse(txtbirimfiyat.Text.Trim(), out birimFiyat) ||
                !decimal.TryParse(txttutar.Text.Trim(), out toplamTutar))
            {
                MessageBox.Show("Miktar, birim fiyat veya toplam tutar geçersiz.");
                return;
            }

            if (string.IsNullOrEmpty(cariKod))
            {
                MessageBox.Show("Cari kodu seçilmelidir.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                  
                    string insertFicheQuery = @"
                    INSERT INTO LG_001_01_STFICHE (FICHENO, DATE_ , CLIENTREF)
                    VALUES (@FICHENO, @DATE_, (SELECT LOGICALREF FROM LG_001_CLCARD WHERE CODE = @CARICODE))";

                    int ficheLogicalRef;

                    using (SqlCommand command = new SqlCommand(insertFicheQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FICHENO", yeniFisNumarası);
                        command.Parameters.AddWithValue("@DATE_", Tarih);
                        command.Parameters.AddWithValue("@CARICODE", cariKod);
                        command.ExecuteNonQuery();

                        
                        string getLogicalRefQuery = "SELECT LOGICALREF FROM LG_001_01_STFICHE WHERE FICHENO = @FICHENO";
                        using (SqlCommand getLogicalRefCommand = new SqlCommand(getLogicalRefQuery, connection))
                        {
                            getLogicalRefCommand.Parameters.AddWithValue("@FICHENO", yeniFisNumarası);
                            ficheLogicalRef = Convert.ToInt32(getLogicalRefCommand.ExecuteScalar());
                        }
                    }

                
                    string insertLineQuery = @"
                    INSERT INTO LG_001_01_STLINE (STFICHEREF, STOCKREF, AMOUNT, PRICE, TOTAL)
                    VALUES (@STFICHEREF, 
                            (SELECT LOGICALREF FROM LG_001_ITEMS WHERE CODE = @STOCKREF), 
                            @AMOUNT, @PRICE, @TOTAL)";

                    using (SqlCommand command = new SqlCommand(insertLineQuery, connection))
                    {
                        command.Parameters.AddWithValue("@STFICHEREF", ficheLogicalRef);
                        command.Parameters.AddWithValue("@STOCKREF", cariKod);
                        command.Parameters.AddWithValue("@AMOUNT", miktar);
                        command.Parameters.AddWithValue("@PRICE", birimFiyat);
                        command.Parameters.AddWithValue("@TOTAL", toplamTutar);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kayıt başarıyla eklendi.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Veri kaydetme hatası: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
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
            if (!string.IsNullOrEmpty(selectedCariAd) && unvanToCariKodMap.ContainsKey(selectedCariAd))
            {
                comboBox1.SelectedItem = unvanToCariKodMap[selectedCariAd];
            }
        }
        private void irsaliye_Ekle_Load(object sender, EventArgs e)
        {
            ComboboxDoldur();

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

                                // Ayrı sözlüklerde sakla
                                cariKodToUnvanMap[kod] = unvan;
                                unvanToCariKodMap[unvan] = kod;
                            }
                        }
                    }

                    comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
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
    }
}

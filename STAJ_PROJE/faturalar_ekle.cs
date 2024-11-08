using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class faturalar_Ekle : Form
    {
        private string connectionString = @"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True";
        private string fisNumarasi;
        private Dictionary<string, string> malzemeKodToAdMap = new Dictionary<string, string>();
        private Dictionary<string, string> cariKodToUnvanMap = new Dictionary<string, string>();

        public faturalar_Ekle() : this(null) { }

        public faturalar_Ekle(string fisNumarasi)
        {
            InitializeComponent();
            this.fisNumarasi = fisNumarasi;



            if (!string.IsNullOrEmpty(fisNumarasi))
            {
                VerileriGetir(fisNumarasi);
            }
        }

        private void VerileriGetir(string fisNumarasi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = @"
    SELECT 
    INV.FICHENO, 
    INV.DATE_, 
    CLC.CODE AS CariKod, 
    CLC.DEFINITION_ AS CariUnvan,
    ITM.CODE AS MalzemeKodu, 
    ITM.NAME AS MalzemeAdi, 
    STL.AMOUNT, 
    STL.PRICE, 
    STL.TOTAL
    FROM  LG_001_01_INVOICE INV
    LEFT JOIN  LG_001_01_STLINE STL ON INV.LOGICALREF = STL.INVOICEREF
    LEFT JOIN  LG_001_CLCARD CLC ON INV.CLIENTREF = CLC.LOGICALREF
    LEFT JOIN  LG_001_ITEMS ITM ON STL.STOCKREF = ITM.LOGICALREF
    WHERE INV.FICHENO = @FICHENO;";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FICHENO", fisNumarasi);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                while (reader.Read())
                                {
                                txtfisnumarasi.Text = reader["FICHENO"].ToString();
                                dateTimePicker1.Value = Convert.ToDateTime(reader["DATE_"]);
                                comboBox1.SelectedItem = reader["CariKod"].ToString();
                                comboBox2.SelectedItem = reader["CariUnvan"].ToString();
                                comboBox3.SelectedItem = reader["MalzemeKodu"].ToString();
                                comboBox4.SelectedItem = reader["MalzemeAdi"].ToString();
                                txtmiktar.Text = reader["AMOUNT"].ToString();
                                txtbirimfiyat.Text = reader["PRICE"].ToString();
                                txttutar.Text = reader["TOTAL"].ToString();


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
                if (!string.IsNullOrEmpty(selectedAd))
                {
                    string kod = malzemeKodToAdMap.FirstOrDefault(x => x.Value == selectedAd).Key;
                    comboBox3.SelectedItem = kod;
                }
            };
        }

        private void btnkaydet_Click_1(object sender, EventArgs e)
        {
            string fisNumarasi = txtfisnumarasi.Text.Trim();
    DateTime tarih = dateTimePicker1.Value;
    string cariKod = comboBox1.SelectedItem?.ToString();
    string malzemeKodu = comboBox3.SelectedItem?.ToString();

    if (string.IsNullOrEmpty(fisNumarasi))
    {
        MessageBox.Show("Fatura numarası boş olamaz.");
        return;
    }

    if (!decimal.TryParse(txtmiktar.Text.Trim(), out decimal miktar) ||
        !decimal.TryParse(txtbirimfiyat.Text.Trim(), out decimal birimFiyat) ||
        !decimal.TryParse(txttutar.Text.Trim(), out decimal toplamTutar))
    {
        MessageBox.Show("Miktar, birim fiyat veya toplam tutar geçersiz.");
        return;
    }

    if (string.IsNullOrEmpty(cariKod) || string.IsNullOrEmpty(malzemeKodu))
    {
        MessageBox.Show("Tüm seçimlerin yapıldığından emin olun.");
        return;
    }

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlTransaction transaction = null;
        try
        {
            connection.Open();
            transaction = connection.BeginTransaction();

            string checkQuery = "SELECT COUNT(*) FROM LG_001_01_INVOICE WHERE FICHENO = @FICHENO";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
            {
                checkCommand.Parameters.AddWithValue("@FICHENO", fisNumarasi);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Bu fatura numarası zaten mevcut. Lütfen başka bir numara girin.");
                    transaction.Rollback();
                    return;
                }
            }

            int invoiceLogicalRef;

            string insertInvoiceQuery = @"
                INSERT INTO LG_001_01_INVOICE (DATE_, FICHENO)
                VALUES (@DATE_, @FICHENO);
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand insertInvoiceCommand = new SqlCommand(insertInvoiceQuery, connection, transaction))
            {
                insertInvoiceCommand.Parameters.AddWithValue("@DATE_", tarih);
                insertInvoiceCommand.Parameters.AddWithValue("@FICHENO", fisNumarasi);

                invoiceLogicalRef = Convert.ToInt32(insertInvoiceCommand.ExecuteScalar());
            }

            string insertDetailQuery = @"
                INSERT INTO LG_001_01_STLINE (INVOICEREF, CLIENTREF, AMOUNT, PRICE, TOTAL, STOCKREF)
                VALUES (@INVOICEREF, (SELECT LOGICALREF FROM LG_001_CLCARD WHERE CODE = @CARICODE), @AMOUNT, @PRICE, @TOTAL,
                (SELECT LOGICALREF FROM LG_001_ITEMS WHERE CODE = @ITEMCODE))";

            using (SqlCommand insertDetailCommand = new SqlCommand(insertDetailQuery, connection, transaction))
            {
                insertDetailCommand.Parameters.AddWithValue("@INVOICEREF", invoiceLogicalRef);
                insertDetailCommand.Parameters.AddWithValue("@CARICODE", cariKod);
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

             
        

               


        private void faturalar_Ekle_Load_1(object sender, EventArgs e)
        {
            ComboboxDoldur(); 
            MalzemeDoldur();  

            if (!string.IsNullOrEmpty(fisNumarasi))
            {
                VerileriGetir(fisNumarasi); 
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
    }
}
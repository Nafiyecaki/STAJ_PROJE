using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class Cari_Hesaplar_Ekle : Form
    {
        private string connectionString = @"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True";
        private string cariKodu;

        public Cari_Hesaplar_Ekle() : this(null) { }

        public Cari_Hesaplar_Ekle(string cariKodu)
        {
            InitializeComponent();
            this.cariKodu = cariKodu;

            if (!string.IsNullOrEmpty(cariKodu))
            {
                VerileriGetir(cariKodu);
            }
        }

        private void VerileriGetir(string cariKodu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = @"
                        SELECT CODE ,DEFINITION_, ADDR1, CITY, COUNTRY, TELNRS1, EMAILADDR, TAXOFFCODE
                        FROM LG_001_CLCARD
                        WHERE CODE = @CODE";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CODE", cariKodu);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtcarikodu.Text = cariKodu;
                                txtcariadi.Text = reader["DEFINITION_"].ToString();
                                txtadres1.Text = reader["ADDR1"].ToString();
                                txtsehir.Text = reader["CITY"].ToString();
                                txtülke.Text = reader["COUNTRY"].ToString();
                                txttelno.Text = reader["TELNRS1"].ToString();
                                txtmail.Text = reader["EMAILADDR"].ToString();
                                txttc.Text = reader["TAXOFFCODE"].ToString();
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
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void btnkaydet_Click_1(object sender, EventArgs e)
        {
            string CariKodu = txtcarikodu.Text.Trim();
            string CariAdı = txtcariadi.Text.Trim();
            string Address1 = txtadres1.Text.Trim();
            string Şehir = txtsehir.Text.Trim();
            string Ülke = txtülke.Text.Trim();
            string TelefonNumarası = txttelno.Text.Trim();
            string Mail = txtmail.Text.Trim();
            string KimlikNumarası = txttc.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query;

                    // Kodu güncellenmişse UPDATE, yoksa INSERT
                    if (!string.IsNullOrEmpty(cariKodu))
                    {
                        query = @"
                            UPDATE LG_001_CLCARD
                            SET CODE =@CODE , DEFINITION_ = @DEFINITION_ , ADDR1=@ADDR1 , CITY=@CITY , COUNTRY =@COUNTRY , TELNRS1=@TELNRS1 , EMAILADDR=@EMAILADDR , TAXOFFCODE = @TAXOFFCODE 
                            WHERE CODE = @OriginalCODE";
                    }
                    else
                    {
                        query = @"
                            INSERT INTO LG_001_CLCARD (CODE, DEFINITION_ , ADDR1 , CITY, COUNTRY , TELNRS1 , EMAILADDR , TAXOFFCODE )
                            VALUES (@CODE, @DEFINITION_ , @ADDR1 , @CITY , @COUNTRY , @TELNRS1 , @EMAILADDR , @TAXOFFCODE)";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CODE", CariKodu);
                        command.Parameters.AddWithValue("@DEFINITION_", CariAdı);
                        command.Parameters.AddWithValue("@ADDR1", Address1);
                        command.Parameters.AddWithValue("@CITY", Şehir);
                        command.Parameters.AddWithValue("@COUNTRY", Ülke);
                        command.Parameters.AddWithValue("@TELNRS1", TelefonNumarası);
                        command.Parameters.AddWithValue("@EMAILADDR", Mail);
                        command.Parameters.AddWithValue("@TAXOFFCODE", KimlikNumarası);

                        if (!string.IsNullOrEmpty(cariKodu))
                        {
                            command.Parameters.AddWithValue("@OriginalCODE", cariKodu);
                        }

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show(!string.IsNullOrEmpty(cariKodu) ? "Kayıt başarıyla güncellendi." : "Kayıt başarıyla eklendi.");
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
    }
}


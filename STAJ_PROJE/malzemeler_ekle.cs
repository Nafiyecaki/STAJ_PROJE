using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class malzemeler_ekle : Form
    {
        private string connectionString = @"DATA SOURCE=DESKTOP-1DTA4LP;DATABASE=LOGO;Trusted_Connection=True";
        private string malzemeKodu;

       
        public malzemeler_ekle() : this(null) { }

        
        public malzemeler_ekle(string malzemeKodu)
        {
            InitializeComponent();
            this.malzemeKodu = malzemeKodu;

            
            if (!string.IsNullOrEmpty(malzemeKodu))
            {
                VerileriGetir1(malzemeKodu);
            }
        }

        private void VerileriGetir1(string malzemeKodu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = @"
                        SELECT CODE, NAME ,SPECODE , CYPHCODE
                        FROM LG_001_ITEMS
                        WHERE CODE = @CODE";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CODE", malzemeKodu);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtmlzkodu.Text = reader["CODE"].ToString();
                                txtmlzadi.Text = reader["NAME"].ToString();
                                txtozelkod.Text = reader["SPECODE"].ToString();
                                txtyetkikod.Text = reader["CYPHCODE"].ToString();
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
            string MalzemeKodu = txtmlzkodu.Text.Trim();
            string MalzemeAdı = txtmlzadi.Text.Trim();
            string ÖzelKod = txtozelkod.Text.Trim();
            string YetkiKod = txtyetkikod.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query;

                    
                    if (!string.IsNullOrEmpty(malzemeKodu))
                    {
                        query = @"
                            UPDATE LG_001_ITEMS
                            SET CODE = @CODE, NAME = @NAME , SPECODE=@SPECODE , CYPHCODE=@CYPHCODE
                            WHERE CODE = @OriginalCODE";
                    }
                    else
                    {
                        query = @"
                            INSERT INTO LG_001_ITEMS (CODE, NAME , SPECODE , CYPHCODE)
                            VALUES (@CODE, @NAME ,@SPECODE,@CYPHCODE)";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CODE", MalzemeKodu);
                        command.Parameters.AddWithValue("@NAME", MalzemeAdı);
                        command.Parameters.AddWithValue("@SPECODE", ÖzelKod);
                        command.Parameters.AddWithValue("@CYPHCODE", YetkiKod);

                        if (!string.IsNullOrEmpty(malzemeKodu))
                        {
                            command.Parameters.AddWithValue("@OriginalCODE", malzemeKodu);
                        }

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show(!string.IsNullOrEmpty(malzemeKodu) ? "Kayıt başarıyla güncellendi." : "Kayıt başarıyla eklendi.");
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

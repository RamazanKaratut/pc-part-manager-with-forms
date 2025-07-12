using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Design
{
    public partial class UrunBilgileriGoruntule : Form
    {
        public UrunBilgileriGoruntule()
        {
            InitializeComponent();
        }
        public int KullaniciID;
        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string commandString = "select *from URUNLER where UrunTur = @UrunTur and UrunMarka = @UrunMarka and UrunModel = @UrunModel";
        string kayıtkontrolString = "select *from KULLANICI where kullaniciID = @kullaniciID";
        private void UrunBilgileriGoruntule_Load(object sender, EventArgs e)
        {
            AdminGetir();
        }
        void AdminGetir()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(kayıtkontrolString, connection))
            {
                command.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (KullaniciID == Convert.ToInt32(reader["kullaniciID"].ToString().TrimEnd()))
                        {
                            admintext.Text = reader["kullaniciAdSoyad"].ToString().TrimEnd();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bilgilerinizi kontrol ediniz.");
                        connection.Close();
                    }
                }
            }
        }
        private void UrunBilgileriGoruntule_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

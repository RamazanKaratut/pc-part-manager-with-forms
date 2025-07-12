using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Computer_Design
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string commandString = "select *from  KULLANICI where kullaniciAdSoyad = @KullaniciAdSoyad and kullaniciSifre = @KullaniciSifre";

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void girisyapbuton_Click(object sender, EventArgs e)  // Kayýt olup olmadýðýný kontrol ederek giriþ yapar.
        {
            string KullaniciAdSoyad = adsoyadtext.Text.ToString().TrimEnd(), KullaniciSifre = sifretext.Text.ToString().TrimEnd();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int kullaniciID;
            using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@KullaniciAdSoyad", KullaniciAdSoyad);
                command.Parameters.AddWithValue("@KullaniciSifre", KullaniciSifre);

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        kullaniciID = Convert.ToInt32(reader["kullaniciID"].ToString().TrimEnd());
                        MessageBox.Show($"Giriþ baþarýlý!\nHoþgeldiniz sayýn {KullaniciAdSoyad}.");
                        if (reader["kullaniciDurumu"].ToString().TrimEnd() == "Admin")
                        {
                            connection.Close();
                            UrunEkle urunEkle = new UrunEkle();
                            urunEkle.kullaniciIDurunEkle = kullaniciID;
                            this.Hide();
                            urunEkle.Show();
                        }
                        else
                        {
                            connection.Close();
                            MagazaSayfasi magazaSayfasi = new MagazaSayfasi();
                            magazaSayfasi.KullaniciID = kullaniciID;
                            this.Hide();
                            magazaSayfasi.Show();
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

        private void kayitollabel_Click(object sender, EventArgs e)  // Kayýt olma ekranýna gider.
        {
            KayitOl kayitOl = new KayitOl();
            this.Hide();
            kayitOl.Show();
        }

        private void sifremiunuttumlabel_Click(object sender, EventArgs e)  // Þifre yenileme ekranýna gider.
        {
            SifreYenile sifreYenile = new SifreYenile();
            this.Hide();
            sifreYenile.Show();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)  // Þifre yazarken þifrenin görünmesini saðlar.
        {
            sifretext.PasswordChar = '\0';
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)  // Þifre yazarken þifrenin görünmemesini saðlar.
        {
            sifretext.PasswordChar = '*';
        }

        private void adsoyadtext_Leave(object sender, EventArgs e)  // Ad ya da soyad boþsa o kýsmý doldurmayý zorunlu kýlar.
        {
            if (string.IsNullOrEmpty(adsoyadtext.Text))
            {
                MessageBox.Show("Ad soyad bilgilerinizi doldurunuz.");
                adsoyadtext.Focus();
            }
        }

        private void sifretext_Leave(object sender, EventArgs e)  // Þifre kýsmý boþsa o kýsmý doldurmayý zorunlu kýlar.
        {
            if (string.IsNullOrEmpty(sifretext.Text))
            {
                MessageBox.Show("Þifreniz boþ olamaz!");
                sifretext.Focus();
            }
        }
        private void sifretext_KeyDown(object sender, KeyEventArgs e)  // Eðer þifresini girerken enter tuþuna basarsa direk giriþ iþlemini yapar.
        {
            if (e.KeyData == Keys.Enter)
            {
                girisyapbuton_Click(sender, e);
            }
        }

        private void adsoyadtext_KeyDown(object sender, KeyEventArgs e) // // Eðer adýný ve soyadýný girerken enter tuþuna basarsa direk giriþ iþlemini yapar.
        {
            if (e.KeyData == Keys.Enter)
            {
                girisyapbuton_Click(sender, e);
            }
        }
    }
}

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

        private void girisyapbuton_Click(object sender, EventArgs e)  // Kay�t olup olmad���n� kontrol ederek giri� yapar.
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
                        MessageBox.Show($"Giri� ba�ar�l�!\nHo�geldiniz say�n {KullaniciAdSoyad}.");
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

        private void kayitollabel_Click(object sender, EventArgs e)  // Kay�t olma ekran�na gider.
        {
            KayitOl kayitOl = new KayitOl();
            this.Hide();
            kayitOl.Show();
        }

        private void sifremiunuttumlabel_Click(object sender, EventArgs e)  // �ifre yenileme ekran�na gider.
        {
            SifreYenile sifreYenile = new SifreYenile();
            this.Hide();
            sifreYenile.Show();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)  // �ifre yazarken �ifrenin g�r�nmesini sa�lar.
        {
            sifretext.PasswordChar = '\0';
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)  // �ifre yazarken �ifrenin g�r�nmemesini sa�lar.
        {
            sifretext.PasswordChar = '*';
        }

        private void adsoyadtext_Leave(object sender, EventArgs e)  // Ad ya da soyad bo�sa o k�sm� doldurmay� zorunlu k�lar.
        {
            if (string.IsNullOrEmpty(adsoyadtext.Text))
            {
                MessageBox.Show("Ad soyad bilgilerinizi doldurunuz.");
                adsoyadtext.Focus();
            }
        }

        private void sifretext_Leave(object sender, EventArgs e)  // �ifre k�sm� bo�sa o k�sm� doldurmay� zorunlu k�lar.
        {
            if (string.IsNullOrEmpty(sifretext.Text))
            {
                MessageBox.Show("�ifreniz bo� olamaz!");
                sifretext.Focus();
            }
        }
        private void sifretext_KeyDown(object sender, KeyEventArgs e)  // E�er �ifresini girerken enter tu�una basarsa direk giri� i�lemini yapar.
        {
            if (e.KeyData == Keys.Enter)
            {
                girisyapbuton_Click(sender, e);
            }
        }

        private void adsoyadtext_KeyDown(object sender, KeyEventArgs e) // // E�er ad�n� ve soyad�n� girerken enter tu�una basarsa direk giri� i�lemini yapar.
        {
            if (e.KeyData == Keys.Enter)
            {
                girisyapbuton_Click(sender, e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Design
{
    public partial class KayitOl : Form
    {
        public KayitOl()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string kayıtkontrolString = "select *from KULLANICI";
        string kayıtString = "insert into KULLANICI (kullaniciAdSoyad, kullaniciSifre, kullaniciPosta, kullaniciTelefon, kullaniciDurumu) values (@KullaniciAdi, @Sifre, @Posta, @Telefon, @KullaniciDurumu)";
        int randomSayı;
        Random randomAtama = new Random();
        Form form = null;
        private void pictureBox1_MouseEnter(object sender, EventArgs e)  // Fare göz'e girerse PasswordChar'ı kapatır.
        {
            sifretext.PasswordChar = '\0';
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)  // Fare göz'den çıkarsa PasswordChar'u düzenler.
        {
            sifretext.PasswordChar = '*';
        }

        private void label6_Click(object sender, EventArgs e)  // Giriş ekranına geçiş yapar.
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void KayitOl_FormClosing(object sender, FormClosingEventArgs e)  // Kayıt ekranından çıkarsa arka plandan da kapatır.
        {
            Application.Exit();
        }

        private void kayitolbuton_Click(object sender, EventArgs e)// Databaseyi kontrol et ve kayıt varsa kaydetme kayıt yoksa kaydı tamamlayıp giriş yapma ekranına dön
        {
            if (!string.IsNullOrEmpty(adsoyadtext.Text) && !string.IsNullOrEmpty(sifretext.Text) && !string.IsNullOrEmpty(yenidensifretext.Text) && !string.IsNullOrEmpty(postatext.Text) && !string.IsNullOrEmpty(telefontext.Text))
            {
                bool kayıtKontrol = false;
                string kullaniciDurumu = "Müşteri";
                string KullaniciAdi = adsoyadtext.Text.ToString().TrimEnd(), Sifre = sifretext.Text.ToString().TrimEnd(), Posta = postatext.Text.ToString().TrimEnd(), Telefon = telefontext.Text.ToString().TrimEnd();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using (SqlCommand command = new SqlCommand(kayıtkontrolString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (KullaniciAdi == reader["kullaniciAdSoyad"].ToString().TrimEnd())
                            {
                                kayıtKontrol = true;
                                MessageBox.Show("Kullanıcı bilgileri zaten mevcut");
                                return;
                            }
                        }
                    }
                }
                if (kayıtKontrol == false)
                {
                    form = new Form();
                    this.AddOwnedForm(form);
                    form.Text = "Computer Design - Kayıt Olma Ekranı";
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.Size = new Size(500, 200);
                    form.FormBorderStyle = FormBorderStyle.FixedSingle;
                    form.MaximizeBox = false;
                    Bitmap bitmap = Properties.Resources.Aha_Soft_Security_Secrecy_128;
                    Icon icon = Icon.FromHandle(bitmap.GetHicon());
                    form.Icon = icon;
                    form.Show();

                    Panel panel = new Panel();
                    panel.Size = new Size(460, 140);
                    panel.Location = new Point(10, 10);
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    form.Controls.Add(panel);
                    Label labelBilgiver = new Label();
                    labelBilgiver.Location = new Point(120, 30);
                    labelBilgiver.Size = new Size(400, 30);
                    labelBilgiver.Text = "Mailinize gönderdiğimiz kodu giriniz.";

                    TextBox textKod = new TextBox();
                    textKod.Location = new Point(175, 60);
                    textKod.MaxLength = 6;
                    textKod.Size = new Size(100, 40);

                    panel.Controls.Add(labelBilgiver);
                    panel.Controls.Add(textKod);

                    MailGönder();
                    textKod.Leave += (sender, e) =>
                    {
                        if (textKod.TextLength != 6)
                        {
                            MessageBox.Show("Kodu yanlış girdiniz.");
                            textKod.Focus();
                        }
                    };
                    Button button = new Button();
                    button.Text = "Kodu onayla";
                    button.Size = new Size(100, 40);
                    button.Location = new Point(175, 90);
                    button.Click += (sender, e) =>
                    {
                        if (string.IsNullOrWhiteSpace(textKod.Text))
                        {
                            MessageBox.Show("Lütfen gönderilen kodu giriniz.");
                            textKod.Focus();
                        }
                        else if (Convert.ToInt32(textKod.Text.TrimEnd()) != randomSayı)
                        {
                            MessageBox.Show("Kodu yanlış girdiniz.");
                            textKod.Focus();
                        }
                        else
                        {
                            form.Hide();
                            this.Enabled = true;
                            KayıtTamamla();
                        }
                    };

                    panel.Controls.Add(button);
                    form.FormClosing += (sender, e) =>
                    {
                        this.Enabled = true;
                    };
                }
            }
            else
            {
                MessageBox.Show("Bilgileri boşluksuz doldurun.");
            }
        }
        void MailGönder()  // Mail hesabına bir rastgele şifre gönderir.
        {
            randomSayı = randomAtama.Next(100000, 1000000);
            try
            {
                string gönderenmail = "your@gmail.com";
                string gönderenşifre = "11111111111";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(gönderenmail);
                mailMessage.Subject = "Computer Design - Kayıt Onay";
                mailMessage.To.Add(new MailAddress(postatext.Text.TrimEnd()));
                mailMessage.Body = $"Sisteme kayıt olmak için gerekli kod;\n                    {randomSayı}\nBu kodu istenilen alana yazıp kayıt işleminizi tamamlayabilirsiniz.";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(gönderenmail, gönderenşifre),
                    EnableSsl = true,
                };
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderirken hata oluştu. Hata: " + ex.Message);
            }
        }
        void KayıtTamamla()  // Mail girişi doğru girildiyse kayıt tamamlanır.
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand kayıtCommand = new SqlCommand(kayıtString, connection))
            {
                string kullaniciDurumu = "Müşteri";
                string KullaniciAdi = adsoyadtext.Text.ToString().TrimEnd(), Sifre = sifretext.Text.ToString().TrimEnd(), Posta = postatext.Text.ToString().TrimEnd(), Telefon = telefontext.Text.ToString().TrimEnd();
                kayıtCommand.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
                kayıtCommand.Parameters.AddWithValue("@Sifre", Sifre);
                kayıtCommand.Parameters.AddWithValue("@Posta", Posta);
                kayıtCommand.Parameters.AddWithValue("@Telefon", Telefon);
                kayıtCommand.Parameters.AddWithValue("@KullaniciDurumu", kullaniciDurumu);
                kayıtCommand.ExecuteNonQuery();
                MessageBox.Show("Kayıt işlemi başarılı giriş sayfasına aktarılıyorsunuz.");
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
            }
        }
        private void telefontext_KeyDown(object sender, KeyEventArgs e)   // Eğer telefon numarasını da girdikten sonra enter tuşuna basarsa direk kaydolma işlemini yapar.
        {
            if (e.KeyCode == Keys.Enter)
            {
                kayitolbuton_Click(sender, e);
            }
        }

        private void telefontext_Enter(object sender, EventArgs e)  // Telefon numarası girişinin bilgisini ToolTip ile verir.
        {
            kontrolToolTip.Show("Bu alana 11 haneli telefon numaranızı giriniz.", telefontext, telefontext.Width, telefontext.Height - 20, 5000);
        }

        private void postatext_Enter(object sender, EventArgs e)  // Posta girişinin bilgisini ToolTip ile verir.
        {
            kontrolToolTip.Show("Bu alana mailinizi doğru biçimde yazınız.", postatext, postatext.Width, postatext.Height - 20, 5000);
        }

        private void sifretext_Enter(object sender, EventArgs e)  // Şifre girişinin bilgisini ToolTip ile verir.
        {
            kontrolToolTip.Show("Bu alana şifrenizi giriniz en az 1 büyük rakam ve en az 1 rakam olacak biçimde ve \naynı zamanda en az 8 haneli en fazla 16 haneli olsun", sifretext, sifretext.Width, sifretext.Height - 20, 5000);
        }

        private void adsoyadtext_Enter(object sender, EventArgs e)  // Ad soyad girişinin bilgisini ToolTip ile verir.
        {
            kontrolToolTip.Show("Bu alana adınızı ve soyadınızı boşluklu ve baş harfleri büyük olacak biçimde yazınız.", adsoyadtext, adsoyadtext.Width, adsoyadtext.Height - 20, 5000);
        }

        private void adsoyadtext_Leave(object sender, EventArgs e) // Ad soyad bilgisi boşsa, baş harfleri küçükse, arada boşluk yoksa uyar.
        {
            Regex regex = new Regex(@"^[A-ZÇĞİÖŞÜ][a-zçğıöşü]+\s[A-ZÇĞİÖŞÜ][a-zçğıöşü]+$");
            if (!regex.Match(adsoyadtext.Text).Success)
            {
                MessageBox.Show("Adınızın soyadınızın bilgisini düzeltip tekrar giriniz.");
                adsoyadtext.Focus();
            }
        }

        private void sifretext_Leave(object sender, EventArgs e) // Şifre bilgisi boşsa uyar ve Şifre içerisinde büyük bir harf ve bir sayı olmasına dikkat et.
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[A-Z]).{8,}$");
            if (!regex.Match(sifretext.Text).Success)
            {
                MessageBox.Show("Şireniz minimum 8 karakter olmalı aynı zamanda içerisinde en az 1 sayı ve en az 1 harf içermek zorundadır.");
                sifretext.Focus();
            }
        }

        private void yenidensifretext_Leave(object sender, EventArgs e) // Şifre bilgisi boşsa uyar.
        {
            if (string.IsNullOrWhiteSpace(yenidensifretext.Text))
            {
                MessageBox.Show("Şifre bilgisini doldurunuz.");
                yenidensifretext.Focus();
            }
            if (yenidensifretext.Text != sifretext.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor.");
                yenidensifretext.Focus();
            }
        }

        private void postatext_Leave(object sender, EventArgs e)  // Posta bilgisi boşsa, yanlışlık varsa uyar.
        {
            Regex regex = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
            if (!regex.Match(postatext.Text).Success)
            {
                MessageBox.Show("Mail bilginizi kontrol ediniz.");
                postatext.Focus();
            }
        }

        private void telefontext_Leave(object sender, EventArgs e)  // Telefon bilgisi boşsa uyar ve 11 haneli ve sadece int değeri kabul et.
        {
            Regex regex = new Regex(@"^(05(\d{9}))$");
            if (!regex.Match(telefontext.Text).Success)
            {
                MessageBox.Show("Telefon numaranız 11 haneli ve sadece sayı değeri girebilirsiniz.");
                telefontext.Focus();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(yenidensifretext.PasswordChar == '*')
            {
                yenidensifretext.PasswordChar = '\0';
            }
            else if(yenidensifretext.PasswordChar == '\0')
            {
                yenidensifretext.PasswordChar = '*';
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Design
{
    public partial class SifreYenile : Form
    {
        public SifreYenile()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string commandString = "SELECT *FROM KULLANICI WHERE kullaniciAdSoyad = @KullaniciAdSoyad AND kullaniciPosta = @KullaniciPosta AND kullaniciTelefon = @KullaniciTelefon";
        string updateString = "UPDATE KULLANICI SET kullaniciSifre = @YeniSifre WHERE kullaniciAdSoyad = @KullaniciAdSoyad AND kullaniciPosta = @KullaniciPosta AND kullaniciTelefon = @KullaniciTelefon";
        Random randomAtama = new Random();
        Form form = null;
        int randomSayı;
        private void sifreyenilebuton_Click(object sender, EventArgs e) // Bilgileri kontrol et bilgiler uygun yani doğruysa şifreyi yenile
        {
            string KullaniciAdSoyad = adsoyadtext.Text.ToString().TrimEnd(), KullaniciPosta = postatext.Text.ToString().TrimEnd(), KullaniciTelefon = telefontext.Text.ToString().TrimEnd();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kullanıcı doğrulama sorgusu
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@KullaniciAdSoyad", KullaniciAdSoyad);
                    command.Parameters.AddWithValue("@KullaniciPosta", KullaniciPosta);
                    command.Parameters.AddWithValue("@KullaniciTelefon", KullaniciTelefon);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reader.Close();
                            this.Enabled = false;
                            form = new Form();
                            this.AddOwnedForm(form);
                            form.Text = "Computer Design - Şifre Yenileme";
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
                            labelBilgiver.AutoSize = false;
                            labelBilgiver.Size = new Size(350, 20);
                            labelBilgiver.Text = "Mailinize gönderdiğimiz kodu giriniz.";
                            labelBilgiver.Location = new Point(120, 30);

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
                                    panel.Controls.Clear();
                                    Label labelSifre = new Label();
                                    labelSifre.AutoSize = false;
                                    labelSifre.Size = new Size(350, 20);
                                    labelSifre.Text = "Yeni şifrenizi bu alana giriniz.";
                                    labelSifre.Location = new Point(150, 20);

                                    TextBox textSifre = new TextBox();
                                    textSifre.Location = new Point(175, 60);
                                    textSifre.MaxLength = 16;
                                    textSifre.Size = new Size(100, 40);
                                    textSifre.PasswordChar = '*';

                                    PictureBox pictureBox = new PictureBox();
                                    pictureBox.Image = Properties.Resources.Custom_Icon_Design_Mono_General_4_Eye_512;
                                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pictureBox.Size = new Size(25, 25);
                                    pictureBox.Location = new Point(280, 60);

                                    pictureBox.MouseEnter += (sender, e) =>
                                    {
                                        textSifre.PasswordChar = '\0';
                                    };

                                    pictureBox.MouseLeave += (sender, e) =>
                                    {
                                        textSifre.PasswordChar = '*';
                                    };

                                    textSifre.Enter += (sender, e) =>
                                    {
                                        kontrolToolTip.Show("Bu alana şifrenizi giriniz en az 1 büyük rakam ve en az 1 rakam olacak biçimde ve \naynı zamanda en az 8 haneli en fazla 16 haneli olsun", textSifre, textSifre.Width, textSifre.Height + 10, 5000);
                                    };
                                    textSifre.Leave += (sender, e) =>
                                    {
                                        Regex regex = new Regex(@"^(?=.*\d)(?=.*[A-Z]).{8,}$");
                                        if (!regex.Match(textSifre.Text).Success)
                                        {
                                            MessageBox.Show("Şireniz minimum 8 karakter olmalı aynı zamanda içerisinde en az 1 sayı ve en az 1 harf içermek zorundadır.");
                                            textSifre.Focus();
                                        }
                                    };
                                    Button buttonOnay = new Button();
                                    buttonOnay.Location = new Point(175, 90);
                                    buttonOnay.Size = new Size(100, 40);
                                    buttonOnay.Text = "Şifreyi onayla";
                                    buttonOnay.Click += (sender, e) =>
                                    {
                                        if (string.IsNullOrWhiteSpace(textSifre.Text))
                                        {
                                            MessageBox.Show("Şifre bilgisini doldurunuz.");
                                            textSifre.Focus();
                                        }
                                        else
                                        {
                                            form.Hide();
                                            ŞifreGüncelle(textSifre, KullaniciAdSoyad, KullaniciPosta, KullaniciTelefon);
                                        }
                                    };
                                    panel.Controls.Add(pictureBox);
                                    panel.Controls.Add(buttonOnay);
                                    panel.Controls.Add(labelSifre);
                                    panel.Controls.Add(textSifre);
                                }
                            };
                            panel.Controls.Add(button);
                            form.FormClosing += (sender, e) =>
                            {
                                this.Enabled = true;
                            };
                        }
                        else
                        {
                            MessageBox.Show("Bilgiler uyuşmuyor veya kullanıcı bulunamadı.");
                        }
                    }
                }
            }
        }
        void MailGönder()  // Şifreyi yenilemek için mail hesabına rastgele şifre gönderir.
        {
            randomSayı = randomAtama.Next(100000, 1000000);
            try
            {
                string gönderenmail = "your@gmail.com";
                string gönderenşifre = "11111111111";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(gönderenmail);
                mailMessage.Subject = "Computer Design Şifre Yenileme";
                mailMessage.To.Add(new MailAddress(postatext.Text.TrimEnd()));
                mailMessage.Body = $"Sisteme giriş için gerekli kod;\n                    {randomSayı}\nBu kodu istenilen alana yazıp şifrenizi sıfırlayabilirsiniz.";

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
        void ŞifreGüncelle(TextBox textSifre, string KullaniciAdSoyad, string KullaniciPosta, string KullaniciTelefon)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand updateCommand = new SqlCommand(updateString, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@YeniSifre", textSifre.Text.TrimEnd());
                        updateCommand.Parameters.AddWithValue("@KullaniciAdSoyad", KullaniciAdSoyad);
                        updateCommand.Parameters.AddWithValue("@KullaniciPosta", KullaniciPosta);
                        updateCommand.Parameters.AddWithValue("@KullaniciTelefon", KullaniciTelefon);

                        connection.Open();
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Şifre başarıyla güncellendi.");
                            this.Enabled = true;
                            Form1 form1 = new Form1();
                            this.Hide();
                            form1.Show();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı bilgileri bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }
        private void SifreYenile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
        private void postatext_Leave(object sender, EventArgs e)  // Posta bilgisi boşsa, yanlışlık varsa uyar.
        {
            Regex regex = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
            if (!regex.Match(postatext.Text).Success)
            {
                MessageBox.Show("Posta bilginizi kontrol ediniz.");
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

        private void label6_Click(object sender, EventArgs e)  // Giriş ekranına döner.
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
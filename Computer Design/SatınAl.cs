using Computer_Design.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Loader;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Word = Microsoft.Office.Interop.Word;

namespace Computer_Design
{
    public partial class SatınAl : Form
    {
        public SatınAl()
        {
            InitializeComponent();
        }
        class Şehir
        {
            public int ŞehirID { get; set; }
            public string ŞehirAdi { get; set; }
        }
        class İlçe
        {
            public int ŞehirID { get; set; }
            public string MahalleAdi {  get; set; }
        }
        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string commandString = "select *from  iller";
        string ilceCommand = "select *from  ilceler";
        string kayıtkontrolString = "select *from KULLANICI where kullaniciID = @kullaniciID";
        string kartKayitString = "insert into KARTBILGILERI (kullaniciID, kartSahibi, kartNo, kartSktAy, kartSktYil, kartCVC2) values (@kullaniciID, @kartSahibi, @kartNo, @kartSktAy, @kartSktYil, @kartCVC2)";
        string siparisKayitString = "insert into SIPARIS (kullaniciID, taksitBilgisi, siparisTarihi, odenenMiktar) values (@kullaniciID, @taksitBilgisi, @siparisTarihi, @odenenMiktar)";
        string adresKayitString = "insert into ADRESBILGILERI (kullaniciID, kullaniciSehir, kullaniciIlce, kullaniciTamAdres) values (@kullaniciID, @kullaniciSehir, @kullaniciIlce, @kullaniciTamAdres)";
        string adresString = "select *from ADRESBILGILERI where kullaniciID = @KullaniciID";
        string kartString = "select *from KARTBILGILERI where kullaniciID = @KullaniciID";
        string updateString = "update URUNLISTESI set UrunStok = UrunStok - 1 where UrunID = @UrunID and UrunStok > 0";
        string siparisDetayString = "insert into siparisDetay (siparisID, UrunID) values (@siparisID, @UrunID)";
        string sonkayitString = "SELECT TOP 1 SiparisID FROM SIPARIS WHERE kullaniciID = @kullaniciID ORDER BY siparisTarihi DESC";
        string mailString = "select *from KULLANICI where kullaniciID = @KullaniciID";
        public List<MagazaSayfasi.ÜrünlerDataBase> ürünListesi = new List<MagazaSayfasi.ÜrünlerDataBase>();
        List<Şehir> Şehirler = new List<Şehir>();
        List<İlçe> İlçeler = new List<İlçe>();
        public int KullaniciID;
        int ykonumu = 10;
        double toplamTutar = 0;
        private void SatınAl_Load(object sender, EventArgs e)
        {
            kayıtlıKartlaraBak();
            AdreseBak();
            ilComboYazdir();
            ilceComboYazdir();
            ayveYılComboDoldur();
            KullaniciBilgisi();
            sepetPanelineVeriYazdır();
            kartNoKontrol();
        }
        void kayıtlıKartlaraBak()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(kartString, connection))
            {
                command.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (KullaniciID == Convert.ToInt32(reader["kullaniciID"].ToString().TrimEnd()))
                        {
                            kartSahibiAdSoyad.Text = reader.GetString(2);
                            kartNotext.Text = reader.GetString(3);
                            int ayGiriş = reader.GetInt32(4);
                            ayCombo.Text = ayGiriş.ToString();
                            ayCombo.SelectedItem = ayGiriş;
                            int yılGiriş = reader.GetInt32(5);
                            yılCombo.Text = yılGiriş.ToString();
                            yılCombo.SelectedItem = yılGiriş;
                            cvc2Kodu.Text = reader.GetInt32(6).ToString();
                        }
                    }
                }
            }
        }
        void AdreseBak()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(adresString, connection))
            {
                command.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (KullaniciID == Convert.ToInt32(reader["kullaniciID"].ToString().TrimEnd()))
                        {
                            string ilcombo = reader.GetString(2);
                            ilCombo.Text = ilcombo.ToString();
                            ilCombo.SelectedItem = ilcombo;
                            string ilcecombo = reader.GetString(3);
                            ilceCombo.Text = ilcecombo.ToString();
                            ilceCombo.SelectedItem = ilcecombo;
                            tamAdresText.Text = reader.GetString(4);
                        }
                    }
                }
            }
        }
        void ayveYılComboDoldur()
        {
            for (int i = 1; i <= 12; i++)
            {
                ayCombo.Items.Add(i.ToString());
            }
            DateTime dateTime1 = DateTime.Now;
            int dateTime = dateTime1.Year - 2000;
            for (int i = 0; i <= 12; i++)
            {
                yılCombo.Items.Add((dateTime + i).ToString());
            }
        }
        void KullaniciBilgisi()
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
                            kullaniciadilabel.Text = reader["kullaniciAdSoyad"].ToString().TrimEnd();
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
        void ilComboYazdir()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Şehir şehir = new Şehir();
                            şehir.ŞehirID = reader.GetInt32(0);
                            şehir.ŞehirAdi = reader.GetString(1);
                            Şehirler.Add(şehir);
                        }
                    }
                }
            }
            foreach (var items in Şehirler)
            {
                ilCombo.Items.Add(items.ŞehirAdi);
            }
        }
        void ilceComboYazdir()
        {
            ilceCombo.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(ilceCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            İlçe ilçe = new İlçe();
                            ilçe.ŞehirID = reader.GetInt32(2);
                            ilçe.MahalleAdi = reader.GetString(1);
                            İlçeler.Add(ilçe);
                        }
                    }
                }
            }
            foreach(var items in İlçeler)
            {
                ilceCombo.Items.Add(items.MahalleAdi);
            }
        }
        void sepetPanelineVeriYazdır()
        {
            foreach (var items in ürünListesi)
            {
                toplamTutar += items.UrunFiyati;
                Label sepetLabel = new Label();
                sepetLabel.Text = items.UrunTuru + "\n" + items.UrunMarka + "\n" + items.UrunModel;
                sepetLabel.Location = new Point(10, ykonumu + 10);
                sepetLabel.AutoSize = true;

                PictureBox pctr = new PictureBox();
                pctr.Size = new Size(75, 75);
                pctr.Image = items.UrunFoto;
                pctr.Location = new Point(300, ykonumu);
                pctr.SizeMode = PictureBoxSizeMode.StretchImage;
                pctr.Click += (sender, e) =>
                {
                    Form ÖzellikForm = new Form();
                    this.AddOwnedForm(ÖzellikForm);
                    ÖzellikForm.BackColor = Color.White;
                    ÖzellikForm.StartPosition = FormStartPosition.CenterScreen;
                    Bitmap bitmap = Properties.Resources.Graphicloads_100_Flat_Cart;
                    Icon icon = Icon.FromHandle(bitmap.GetHicon());
                    ÖzellikForm.Icon = icon;
                    ÖzellikForm.Text = $"{items.UrunMarka} {items.UrunModel} ürünü gösteriliyor.";
                    ÖzellikForm.Size = new Size(750, 500);
                    ÖzellikForm.MaximizeBox = false;
                    ÖzellikForm.MinimizeBox = false;
                    ÖzellikForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    ÖzellikForm.Show();

                    Label label = new Label();
                    label.AutoSize = true;
                    label.TextAlign = ContentAlignment.MiddleLeft;
                    label.Location = new Point(20, 30);
                    label.Text = $"Ürün kategorisi: " + items.UrunTuru + "\nÜrün markası: " + items.UrunMarka + "\nÜrün modeli: " + items.UrunModel;

                    PictureBox pictureAlınan = new PictureBox();
                    pictureAlınan.Location = new Point(425, 10);
                    pictureAlınan.Size = new Size(300, 300);
                    pictureAlınan.Image = items.UrunFoto;
                    pictureAlınan.SizeMode = PictureBoxSizeMode.StretchImage;

                    Label seçilenFiyat = new Label();
                    seçilenFiyat.Location = new Point(610, 380);
                    seçilenFiyat.Text = $"{items.UrunFiyati} TL\n{items.UrunStok} tane bulunmakta.";
                    seçilenFiyat.TextAlign = ContentAlignment.MiddleRight;
                    seçilenFiyat.AutoSize = true;

                    Label özellikler = new Label();
                    özellikler.AutoSize = false;
                    özellikler.Size = new Size(250, 100);
                    özellikler.TextAlign = ContentAlignment.MiddleLeft;
                    özellikler.Location = new Point(20, 100);
                    if (items.UrunTuru == "Anakart")
                    {
                        özellikler.Text = "Uyumlu işlemci: " + items.UrunOzellik1 + "\n" + "DDR özelliği: " + items.UrunOzellik2 + "\n" + "PCI özelliği: " + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "Ram")
                    {
                        özellikler.Text = "Kapasitesi: " + items.UrunOzellik1 + "\n" + "DDR özelliği: " + items.UrunOzellik2 + "\n" + "MHz değeri: " + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "İşlemci")
                    {
                        özellikler.Text = "İşlemci uyumluluğu: " + items.UrunOzellik1 + "\n" + "Çekirdek sayısı: " + items.UrunOzellik2 + "\n" + "GHz değeri: " + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "İşlemci Soğutucu")
                    {
                        özellikler.Text = "İntel desteği: " + items.UrunOzellik1 + "\n" + "AMD desteği: " + items.UrunOzellik2 + "\n" + "Fan boyutu: " + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "M2-SSD" || items.UrunTuru == "SSD" || items.UrunTuru == "HDD")
                    {
                        özellikler.Text = "Kapasitesi: " + items.UrunOzellik1 + "\n" + "Okuma / Yazma hızı: " + items.UrunOzellik2 + "\n" + "Uyumluluk: " + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "Monitör")
                    {
                        özellikler.Text = items.UrunOzellik1 + "\n" + items.UrunOzellik2 + "\n" + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "Kasa")
                    {
                        özellikler.Text = "Kasa tipi: " + items.UrunOzellik1 + "\n" + "PSU: " + items.UrunOzellik2 + "\n" + "Fan özelliği: " + items.UrunOzellik3;
                    }
                    else if (items.UrunTuru == "Ekran Kartı")
                    {
                        özellikler.Text = "Kapasitesi:  " + items.UrunOzellik1 + "\n" + "Bit özelliği: " + items.UrunOzellik2 + "\n" + "PCI özelliği: " + items.UrunOzellik3;
                    }
                    else
                    {
                        özellikler.Text = items.UrunOzellik1 + "\n" + items.UrunOzellik2 + "\n" + items.UrunOzellik3;
                    }
                    Label diğerÖzellikler = new Label();
                    diğerÖzellikler.AutoSize = false;
                    diğerÖzellikler.Size = new Size(250, 150);
                    diğerÖzellikler.TextAlign = ContentAlignment.MiddleLeft;
                    diğerÖzellikler.Location = new Point(20, 220);
                    diğerÖzellikler.Text = items.UrunDigerOzellikler;

                    ÖzellikForm.Controls.Add(diğerÖzellikler);
                    ÖzellikForm.Controls.Add(özellikler);
                    ÖzellikForm.Controls.Add(seçilenFiyat);
                    ÖzellikForm.Controls.Add(pictureAlınan);
                    ÖzellikForm.Controls.Add(label);
                };

                Label fiyatLabel = new Label();
                fiyatLabel.Text = items.UrunFiyati + " TL";
                fiyatLabel.Location = new Point(500, ykonumu + 25);
                fiyatLabel.AutoSize = true;

                panelSepet.Controls.Add(fiyatLabel);
                panelSepet.Controls.Add(sepetLabel);
                panelSepet.Controls.Add(pctr);

                ykonumu += 100;
            }
            toplamTutar = Math.Round(toplamTutar, 2);
            tutarLabel.Text = "Toplam tutar " + toplamTutar.ToString() + " TL";
        }
        private void label1_Click(object sender, EventArgs e)
        {
            MagazaSayfasi magazaSayfasi = new MagazaSayfasi();
            magazaSayfasi.ürünSepetListesiDeneme = ürünListesi;
            magazaSayfasi.KullaniciID = KullaniciID;
            this.Hide();
            magazaSayfasi.Show();
        }
        private void SatınAl_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void kartNotext_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^\d{16}$");
            if (!regex.Match(kartNotext.Text).Success)
            {
                MessageBox.Show("Kart numarasını doğru giriniz.");
                kartNotext.Focus();
            }
            if (string.IsNullOrEmpty(kartNotext.Text))
            {
                MessageBox.Show("Kart numarasını giriniz.");
                kartNotext.Focus();
            }
            kartNoKontrol();
        }
        void kartNoKontrol()
        {
            Regex visaRegex = new Regex("^4[0-9]{12}(?:[0-9]{3})?$");
            Regex masterRegex = new Regex("^5[1-5][0-9]{14}$");
            Regex expressRegex = new Regex("^3[47][0-9]{13}$");
            Regex dinersRegex = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            Regex discoverRegex = new Regex("^6(?:011|5[0-9]{2})[0-9]{12}$");
            Regex jcbRegex = new Regex("^(?:2131|1800|35\\d{3})\\d{11}$");


            if (visaRegex.IsMatch(kartNotext.Text))
                cardFoto.Image = Resources.Simpleicons_Team_Simple_Visa_128;
            else if (masterRegex.IsMatch(kartNotext.Text))
                cardFoto.Image = Resources.Simpleicons_Team_Simple_Mastercard_128;
            else if (expressRegex.IsMatch(kartNotext.Text))
                cardFoto.Image = Resources.Ekran_görüntüsü_2024_05_16_190359;
            else if (dinersRegex.IsMatch(kartNotext.Text))
                cardFoto.Image = Resources.Designbolts_Credit_Card_Payment_Diners_Club_International_128;
            else if (discoverRegex.IsMatch(kartNotext.Text))
                cardFoto.Image = Resources.Discover_logo_28a70026a79d4023adafb0f5e2e773cf;
            else if (jcbRegex.IsMatch(kartNotext.Text))
                cardFoto.Image = Resources.Simpleicons_Team_Simple_Jcb_128;
            else
                cardFoto.Image = Resources.Oxygen_Icons_org_Oxygen_Mimetypes_unknown_128;
        }
        private void ilCombo_Leave(object sender, EventArgs e)
        {
            if (ilCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Şehir seçimini yapınız.");
                ilCombo.Focus();
            }
            ilceCombo.Items.Clear();
            foreach(var items in İlçeler)
            {
                if(items.ŞehirID == ilCombo.SelectedIndex + 1)
                {
                    ilceCombo.Items.Add(items.MahalleAdi);
                }
            }
        }

        private void ilceCombo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ilceCombo.Text))
            {
                MessageBox.Show("Lütfen ilçenizi seçiniz.");
                ilceCombo.Focus();
            }
        }
        private void ilCombo_Enter(object sender, EventArgs e)
        {
            tamAdresText.Clear();
            ilceCombo.Text = "";
        }

        private void odemeTamamla_Click(object sender, EventArgs e)
        {
            bool adresBool = false;
            bool kartBool = false;
            foreach (Control items in adreskayitGroup.Controls)
            {
                RadioButton rdb = (RadioButton)(items);
                if(rdb.Checked)
                    adresBool = true;
            }
            foreach (Control items in kartkayitGroup.Controls)
            {
                RadioButton rdb = (RadioButton)(items);
                if (rdb.Checked)
                    kartBool = true;
            }
            if (ürünListesi.Count == 0)
            {
            }
            else if (kartBool == false)
            {
                MessageBox.Show("Kart bilgilerinizin kayıt edilip edilmeyeceğini işaretleyiniz.");
            }
            else if (adresBool == false)
            {
                MessageBox.Show("Adres bilgilerinizin kayıt edilip edilmeyeceğini işaretleyiniz.");
            }
            else if (taksitSecimBilgisi == false)
            {
                MessageBox.Show("Lütfen taksit bilgisini seçiniz");
            }
            else if (string.IsNullOrEmpty(kartSahibiAdSoyad.Text) || string.IsNullOrEmpty(kartNotext.Text) || string.IsNullOrEmpty(cvc2Kodu.Text) || string.IsNullOrEmpty(tamAdresText.Text))
            {
                MessageBox.Show("Bütün bilgileri eksiksiz doldurunuz.");
            }
            else if (!ilCombo.Items.Contains(ilCombo.Text) || !ilceCombo.Items.Contains(ilceCombo.Text) || !ayCombo.Items.Contains(ayCombo.Text) || !yılCombo.Items.Contains(yılCombo.Text))
            {
                MessageBox.Show("Bütün bilgileri eksiksiz doldurunuz.");
            }
            else
            {
                Regex regex = new Regex(@"^[A-ZÇĞİÖŞÜ][a-zçğıöşü]+\s[A-ZÇĞİÖŞÜ][a-zçğıöşü]+$");
                if (!regex.Match(kartSahibiAdSoyad.Text).Success)
                {
                    MessageBox.Show("Kart sahibinin ad, soyad bilgilerini doğru giriniz.");
                    kartSahibiAdSoyad.Focus();
                }
                if (radioEvetAdres.Checked)
                {
                    adresMevcutMu();
                }
                if (radioEvetKart.Checked)
                {
                    kartMevcutMu();
                }
                siparişBilgisiKaydet();
                stokAzalt();
                MessageBox.Show("Alışveriş tamamlandı.");

                string kullaniciPosta = "";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(mailString, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kullaniciPosta = reader["kullaniciPosta"].ToString();
                            }
                        }
                    }
                }
                string siparisAdres;
                siparisAdres = tamAdresText.Text + " ";
                if (!siparisAdres.Contains(ilCombo.Text))
                    siparisAdres += ilCombo.Text.ToString() + " ";
                if (!siparisAdres.Contains(ilceCombo.Text))
                    siparisAdres += ilceCombo.Text.ToString();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("       Computer Design Online Alışveriş");
                sb.AppendLine();
                sb.AppendLine("Bizi tercih ettiğiniz için teşekkür ederiz. Yine bekleriz.");
                sb.AppendLine();
                sb.AppendLine("Fatura bilgileriniz: ");
                foreach(var items in ürünListesi)
                {
                    sb.AppendLine(items.UrunMarka + "  " + items.UrunModel + "                   Fiyatı: " + items.UrunFiyati + " TL");
                }
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("Toplam tutar: " + toplamTutar + " TL");
                sb.AppendLine("KDV tutarı: " + (toplamTutar / 100) * 8 + " TL");
                sb.AppendLine("Müşteri adı: " + kullaniciadilabel.Text.ToString());
                sb.AppendLine();
                sb.AppendLine("Müşteri adresi: " + siparisAdres);
                sb.AppendLine("\nÖdeme yöntemi: " + taksitBilgisi);
                sb.AppendLine(); 
                sb.AppendLine("Satın alım tarihi: " + DateTime.Now.ToString("dd/MM/yyyy"));
                sb.AppendLine("Keyifli kullanımlar dileriz.");
                MessageBox.Show(sb.ToString());
                ürünListesi.Clear();
                panelSepet.Controls.Clear();
                panel2.Controls.Clear();
                tutarLabel.Text = "";

                try
                {
                    string gönderenmail = "computerdesigntr@gmail.com";
                    string gönderenşifre = "zoob kpxs bbye fqgu";

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(gönderenmail);
                    mailMessage.Subject = "Computer Design - Sipariş Faturanız";
                    mailMessage.To.Add(new MailAddress(kullaniciPosta));
                    mailMessage.Body = $"{sb}";

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

                sb.Clear();

                /*
                Word.Application wordApp = new Word.Application();
                Word.Document wordDoc = new Word.Document();
                wordApp.Visible = false;
                wordDoc = wordApp.Documents.Add();
                wordDoc.Content.Text = sb.ToString();
                string pdfYolu = "";

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save as PDF";
                    saveFileDialog.DefaultExt = "pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pdfYolu = saveFileDialog.FileName;
                    }
                }
                wordDoc.SaveAs2(pdfYolu, Word.WdSaveFormat.wdFormatPDF);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);

                wordDoc.Close();
                wordApp.Quit();
                wordDoc = null;
                wordApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                try
                {
                    string gönderenmail = "computerdesigntr@gmail.com";
                    string gönderenşifre = "zoob kpxs bbye fqgu";

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(gönderenmail);
                    mailMessage.Subject = "Computer Design - Fatura Bilgisi";
                    mailMessage.To.Add(new MailAddress(kullaniciPosta));
                    mailMessage.Body = $"Bizi kullandığınız için teşekkür ederiz.;\n";

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(gönderenmail, gönderenşifre),
                        EnableSsl = true,
                    };

                    Attachment ek = new Attachment(pdfYolu);
                    mailMessage.Attachments.Add(ek);

                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mail gönderirken hata oluştu. Hata: " + ex.Message);
                }
                */
            }
        }
        void stokAzalt()  // Ürün satın alınınca alınan ürünlerin stoklarını azaltır.
        {
            foreach (var items in ürünListesi)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updateString, connection))
                    {
                        command.Parameters.AddWithValue("UrunID", items.UrunID);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }
        void kartMevcutMu()  // Kart bilgisi veri tabanına kaydedilecek ise önceden kayıt edilip edilmediğini kontrol eder.
        {
            string kartString = @"SELECT COUNT(*) FROM KARTBILGILERI WHERE kullaniciID = @kullaniciID AND kartNo = @kartNo AND kartSktAy = @kartSktAy AND kartSktYil = @kartSktYil AND kartCVC2 = @kartCVC2";
            bool kartKayıt = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand kontrolCommand = new SqlCommand(kartString, connection))
                {
                    kontrolCommand.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    kontrolCommand.Parameters.AddWithValue("@kartNo", kartNotext.Text.ToString().TrimEnd());
                    kontrolCommand.Parameters.AddWithValue("@kartSktAy", ayCombo.Text.ToString().TrimEnd());
                    kontrolCommand.Parameters.AddWithValue("@kartSktYil", yılCombo.Text.ToString().TrimEnd());
                    kontrolCommand.Parameters.AddWithValue("@kartCVC2", cvc2Kodu.Text.ToString());

                    int count = (int)kontrolCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Kart bilgileri zaten kayıtlı");
                        kartKayıt = true;
                    }
                }
            }
            if (kartKayıt == false)
                kartıKaydet();
        }
        void kartıKaydet()  // Kart bilgisi veri tabanında kayıtlı değilse kaydeder.
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(kartKayitString, connection))
                {
                    cmd.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    cmd.Parameters.AddWithValue("@kartSahibi", kartSahibiAdSoyad.Text.ToString().TrimEnd());
                    cmd.Parameters.AddWithValue("@kartNo", kartNotext.Text.ToString().TrimEnd());
                    cmd.Parameters.AddWithValue("@kartSktAy", Convert.ToInt32(ayCombo.Text.TrimEnd()));
                    cmd.Parameters.AddWithValue("@kartSktYil", Convert.ToInt32(yılCombo.Text.TrimEnd()));
                    cmd.Parameters.AddWithValue("@kartCVC2", cvc2Kodu.Text.ToString().TrimEnd());
                    cmd.ExecuteNonQuery();
                }
            }
        }
        void siparişBilgisiKaydet()  // Sipariş bilgisini veri tabanına kaydeder.
        {
            DateTime date = DateTime.Now;
            int siparisID = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(siparisKayitString, connection))
                {
                    cmd.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    cmd.Parameters.AddWithValue("@taksitBilgisi", taksitBilgisi);
                    cmd.Parameters.AddWithValue("@siparisTarihi", date);
                    cmd.Parameters.AddWithValue("@odenenMiktar", Convert.ToDouble(label10.Text));
                    cmd.ExecuteNonQuery();
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sonkayitString, connection))
                {
                    cmd.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    siparisID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var items in ürünListesi)
                {
                    using (SqlCommand cmd = new SqlCommand(siparisDetayString, connection))
                    {
                        cmd.Parameters.AddWithValue("@siparisID", siparisID);
                        cmd.Parameters.AddWithValue("@UrunID", items.UrunID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        void adresMevcutMu()  // Adres bilgisi veri tabanına kaydedilecek ise önceden kayıt edilip edilmediğini kontrol eder.
        {
            string kontrolSorgusu = @"SELECT COUNT(*) FROM ADRESBILGILERI WHERE kullaniciID = @kullaniciID AND kullaniciSehir = @kullaniciSehir AND kullaniciIlce = @kullaniciIlce AND kullaniciTamAdres = @kullaniciTamAdres";
            bool adresKayit = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand kontrolCommand = new SqlCommand(kontrolSorgusu, connection))
                {
                    kontrolCommand.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    kontrolCommand.Parameters.AddWithValue("@kullaniciSehir", ilCombo.Text.ToString().TrimEnd());
                    kontrolCommand.Parameters.AddWithValue("@kullaniciIlce", ilceCombo.Text.ToString().TrimEnd());
                    kontrolCommand.Parameters.AddWithValue("@kullaniciTamAdres", tamAdresText.Text.ToString());

                    int count = (int)kontrolCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Adres bilgileri zaten kayıtlı");
                        adresKayit = true;
                    }
                }
            }
                if (adresKayit == false)
                   adresiKaydet();
        }
        void adresiKaydet() // Adres bilgisi veri tabanında kayıtlı değilse kaydeder.
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(adresKayitString, connection))
                {
                    command.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    command.Parameters.AddWithValue("@kullaniciSehir", ilCombo.Text.ToString().TrimEnd());
                    command.Parameters.AddWithValue("@kullaniciIlce", ilceCombo.Text.ToString().TrimEnd());
                    command.Parameters.AddWithValue("@kullaniciTamAdres", tamAdresText.Text.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }
        private void cvc2Kodu_Leave(object sender, EventArgs e)  // CVC2 doğruluğunu kontrol eder.
        {
            Regex regex = new Regex(@"^\d{3}$");
            if (!regex.Match(cvc2Kodu.Text).Success)
            {
                MessageBox.Show("Kart CVC2 kodu hatalı.");
                cvc2Kodu.Focus();
            }
        }
        private void ayCombo_Leave(object sender, EventArgs e)  // Ay bilgisini kontrol eder.
        {
            string ayGiriş = ayCombo.Text;
            if (ayCombo.Items.Contains(ayGiriş))
            {
                ayCombo.SelectedItem = ayGiriş;
            }
            if (ayCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Ay seçimini yapınız.");
                ayCombo.Focus();
            }
        }
        private void yılCombo_Leave(object sender, EventArgs e)  // Yıl bilgisini kontrol eder
        {
            string yılGiriş = yılCombo.Text;
            if (yılCombo.Items.Contains(yılGiriş))
            {
                yılCombo.SelectedItem = yılGiriş;
            }
            if (yılCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Yıl seçimini yapınız.");
                yılCombo.Focus();
            }
        }
        private void tekCekim_Click(object sender, EventArgs e)  // Tek çekimse ona göre ayarlar.
        {
            taksitBilgisi = "Tek çekim";
            taksitLabel.Text = "Toplam tutar: " + toplamTutar.ToString() + " TL";
            odenecekMikLabel.Text = toplamTutar.ToString();
            odenecekMikLabel.Visible = false;
            taksitSecimBilgisi = true;
            label10.Text = toplamTutar.ToString("0.0");
        }

        double aylikBedel = 0;
        double geciciTutar = 0;
        string taksitBilgisi;
        bool taksitSecimBilgisi = false;
        private void ikiAy_Click(object sender, EventArgs e)  // 2 ay taksitse ona göre ayarlar.
        {
            taksitBilgisi = "2 ay taksit";
            geciciTutar = toplamTutar + 500;
            aylikBedel = geciciTutar / 2;
            taksitLabel.Text = "Toplam tutar: " + geciciTutar.ToString("0.0") + " TL";
            odenecekMikLabel.Visible = true;
            odenecekMikLabel.Text = "Aylık taksit: " + aylikBedel.ToString("0.0") + " TL";
            taksitSecimBilgisi = true;
            label10.Text = geciciTutar.ToString("0.0");
        }

        private void dortAy_Click(object sender, EventArgs e)  // 4 ay taksitse ona göre ayarlar.
        {
            taksitBilgisi = "4 ay taksit";
            geciciTutar = toplamTutar + 1000;
            aylikBedel = geciciTutar / 4;
            taksitLabel.Text = "Toplam tutar: " + geciciTutar.ToString("0.0") + " TL";
            odenecekMikLabel.Visible = true;
            odenecekMikLabel.Text = "Aylık taksit: " + aylikBedel.ToString("0.0") + " TL";
            taksitSecimBilgisi = true;
            label10.Text = geciciTutar.ToString("0.0");
        }

        private void altiAy_Click(object sender, EventArgs e)  // 6 ay taksitse ona göre ayarlar.
        {
            taksitBilgisi = "6 ay taksit";
            geciciTutar = toplamTutar + 1500;
            aylikBedel = geciciTutar / 6;
            taksitLabel.Text = "Toplam tutar: " + geciciTutar.ToString("0.0") + " TL";
            odenecekMikLabel.Visible = true;
            odenecekMikLabel.Text = "Aylık taksit: " + aylikBedel.ToString("0.0") + " TL";
            taksitSecimBilgisi = true;
            label10.Text = geciciTutar.ToString("0.0");
        }

        private void onikiAy_Click(object sender, EventArgs e)  // 12 ay taksitse ona göre ayarlar.
        {
            taksitBilgisi = "12 ay taksit";
            geciciTutar = toplamTutar + 3000;
            aylikBedel = geciciTutar / 12;
            taksitLabel.Text = "Toplam tutar: " + geciciTutar.ToString("0.0") + " TL";
            odenecekMikLabel.Visible = true;
            odenecekMikLabel.Text = "Aylık taksit: " + aylikBedel.ToString("0.0") + " TL";
            taksitSecimBilgisi = true;
            label10.Text = geciciTutar.ToString("0.0");
        }
    }
}
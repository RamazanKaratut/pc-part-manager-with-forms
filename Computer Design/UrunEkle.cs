using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Computer_Design
{
    public partial class UrunEkle : Form
    {
        public UrunEkle()
        {
            InitializeComponent();
        }
        public int kullaniciIDurunEkle;
        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string commandString = "select *from URUNLISTESI where UrunTur = @UrunTur and UrunMarka = @UrunMarka and UrunModel = @UrunModel";
        string kayıtkontrolString = "select *from KULLANICI where kullaniciID = @kullaniciID";
        string dataGridString = "select *from URUNLISTESI";
        string deleteString = "delete from URUNLISTESI where UrunID = @UrunID";
        string updateString = "update URUNLISTESI set UrunTur = @UrunTur, UrunMarka = @UrunMarka, UrunModel = @UrunModel, UrunLink = @UrunLink, UrunFiyati = @UrunFiyati, UrunStok = @UrunStok, UrunOzellik1 = @UrunOzellik1, UrunOzellik2 = @UrunOzellik2, UrunOzellik3 = @UrunOzellik3, UrunDigerOzellikler = @UrunDigerOzellikler where UrunID = @UrunID";
        string kategoriString = "select kategoriAdi from KATEGORİLER";
        string kategoriEkleString = "insert into KATEGORİLER (kategoriAdi) values (@kategoriAdi)";
        string kayıtString = "insert into URUNLISTESI (UrunTur, UrunMarka, UrunModel, UrunLink, UrunFiyati, UrunStok, UrunOzellik1, UrunOzellik2, UrunOzellik3, UrunDigerOzellikler, UrunFoto) values (@UrunTur, @UrunMarka, @UrunModel, @UrunLink, @UrunFiyati, @UrunStok, @UrunOzellik1, @UrunOzellik2, @UrunOzellik3, @UrunDigerOzellikler, @UrunFoto)";

        void VarlıkKontrol() // Ürünün mevcut olup olmadığı kontrol edilir.
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen ürün türünü seçiniz!");
                comboBox1.Focus();
            }
            else
            {
                string UrunTur = comboBox1.Text.ToString().TrimEnd(), UrunMarka = UrunMarkaText.Text.ToString().TrimEnd(), UrunModel = UrunModelText.Text.ToString().TrimEnd(), UrunOzellik1 = UrunOzellik1Text.Text.ToString().TrimEnd(), UrunOzellik2 = UrunOzellik2Text.Text.ToString().TrimEnd(), UrunOzellik3 = UrunOzellik3Text.Text.ToString().TrimEnd(), UrunFiyati = UrunFiyatiText.Text.ToString().TrimEnd(), UrunStok = UrunStokText.Text.ToString().TrimEnd(), UrunDigerOzellıkler = UrunDigerOzellıklerText.Text.ToString().TrimEnd(), UrunLink = UrunLinkText.Text.ToString().TrimEnd();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@UrunTur", UrunTur);
                    command.Parameters.AddWithValue("@UrunMarka", UrunMarka);
                    command.Parameters.AddWithValue("@UrunModel", UrunModel);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["UrunTur"].ToString().TrimEnd() == UrunTur && reader["UrunMarka"].ToString().TrimEnd() == UrunMarka && reader["UrunModel"].ToString().TrimEnd() == UrunModel)
                            {
                                MessageBox.Show("Bu ürün bilgisi daha önceden kayıt edilmiş.");
                                return;
                            }
                        }
                        else
                        {
                            connection.Close();
                            KayıtKontrol();  // Ürün mevcut değilse KayıtKontrol() fonksiyonuna gider.
                        }
                    }
                }
            }
        }
        void KayıtKontrol()  // Ürün mevcut değilse buraya kaydedilir.
        {
            string UrunTur = comboBox1.Text.ToString().TrimEnd(), UrunMarka = UrunMarkaText.Text.ToString().TrimEnd(),
                   UrunModel = UrunModelText.Text.ToString().TrimEnd(), UrunOzellik1 = UrunOzellik1Text.Text.ToString().TrimEnd(),
                   UrunOzellik2 = UrunOzellik2Text.Text.ToString().TrimEnd(), UrunOzellik3 = UrunOzellik3Text.Text.ToString().TrimEnd(),
                   UrunFiyati = UrunFiyatiText.Text.ToString().TrimEnd(), UrunStok = UrunStokText.Text.ToString().TrimEnd(),
                   UrunDigerOzellıkler = UrunDigerOzellıklerText.Text.ToString().TrimEnd(), UrunLink = UrunLinkText.Text.ToString().TrimEnd();
            if (UrunFoto.Image == null)
            {
                MessageBox.Show("Ürünün fotoğrafını yükleyiniz!");
                return;
            }
            else
            {
                try
                {
                    DialogResult dialogResult;
                    dialogResult = MessageBox.Show("Ürün ekleme işlemini tamamlamak istiyor musunuz?", "Ürün Ekleme", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        Image img = UrunFoto.Image;
                        SqlParameter param = new SqlParameter("@UrunFoto", SqlDbType.Image);
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        param.Value = ms.ToArray();

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(kayıtString, connection))
                            {
                                MessageBox.Show("Ürün ekleme işlemi tamamlandı!\nVeriler temizleniyor.");

                                command.Parameters.AddWithValue("@UrunTur", UrunTur);
                                command.Parameters.AddWithValue("@UrunMarka", UrunMarka);
                                command.Parameters.AddWithValue("@UrunModel", UrunModel);
                                command.Parameters.AddWithValue("@UrunOzellik1", UrunOzellik1);
                                command.Parameters.AddWithValue("@UrunOzellik2", UrunOzellik2);
                                command.Parameters.AddWithValue("@UrunOzellik3", UrunOzellik3);
                                command.Parameters.AddWithValue("@UrunDigerOzellikler", UrunDigerOzellıkler);
                                command.Parameters.AddWithValue("@UrunStok", UrunStok);
                                command.Parameters.AddWithValue("@UrunLink", UrunLink);
                                command.Parameters.AddWithValue("@UrunFiyati", UrunFiyati);
                                command.Parameters.Add(param);

                                command.ExecuteNonQuery();
                                connection.Close();

                                comboBox1.Text = "Seçiniz!"; UrunMarkaText.Clear(); UrunModelText.Clear();
                                UrunOzellik1Text.Clear(); UrunOzellik2Text.Clear(); UrunOzellik3Text.Clear();
                                UrunFiyatiText.Clear(); UrunStokText.Clear(); UrunDigerOzellıklerText.Clear();
                                UrunLinkText.Clear(); UrunFoto.Image = null;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Kayıt hatası algılandı. Bilgileri kontrol ediniz.");
                }
            }
        }

        private void fotoyukle_Click(object sender, EventArgs e)  // Ürün fotoğrafı yüklemek için openFileDialog kullanılır.
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Resim Seç";
            openFileDialog.Filter = "Png Dosyaları(*.png)|*.png| Jpg Dosyaları (*.jpg)| *.jpg| Jpeg Dosyaları(*.jpeg)|*.jpeg| Gif Dosyaları(*.gif)|*.gif| Tif Dosyaları (*.tif)|*tif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                UrunFoto.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void eklemetamamla_Click(object sender, EventArgs e)
        {
            VarlıkKontrol();   // Ürün zaten mevcutsa eklemez
        }

        private void UrunEkle_Load(object sender, EventArgs e)  // Admin bilgilerini sağ üstte yazdırmak için kullanılır.
        {
            KategoriBilgisiYazdır();
            AdminBilgisi();
            DataGridBilgiYazdır();
            VerileriTemizle();
        }
        void KategoriBilgisiYazdır()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(kategoriString, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                    }
                    comboBox1.Items.Add("Diğer");
                }
            }
            connection.Close();
        }
        void AdminBilgisi()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(kayıtkontrolString, connection))
            {
                command.Parameters.AddWithValue("@kullaniciID", kullaniciIDurunEkle);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (kullaniciIDurunEkle == Convert.ToInt32(reader["kullaniciID"].ToString().TrimEnd()))
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
        void DataGridBilgiYazdır()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dataGridString, connection))
                {
                    DataTable tablo = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(dataGridString, connection);
                    dataAdapter.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                }
            }
        }
        private void UrunEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void UrunOzellik1Text_Leave(object sender, EventArgs e)  // UrunOzellik1Text eğer boşsa buraya odaklanır.
        {
            if (string.IsNullOrEmpty(UrunOzellik1Text.Text))
            {
                MessageBox.Show("Bütün bilgilerin doldurunuz.");
                UrunOzellik1Text.Focus();
            }
        }
        private void UrunStokText_Leave_1(object sender, EventArgs e)  // UrunStokText eğer boşsa veya yanlışsa buraya odaklanır.
        {
            Regex regex = new Regex(@"^-?\d+$");
            if (!regex.Match(UrunStokText.Text).Success)
            {
                MessageBox.Show("Stok sayısını int olarak giriniz girmeniz yeterli.");
                UrunStokText.Clear();
                UrunStokText.Focus();
            }
        }
        private void UrunFiyatiText_Leave(object sender, EventArgs e)  // UrunFiyatiText eğer boşsa veya fiyat için uygun değilse buraya odaklanır.
        {
            Regex regex = new Regex(@"^\d+(\.\d+)?$");
            if (!regex.Match(UrunFiyatiText.Text).Success)
            {
                MessageBox.Show("Fiyat bilgisini güncelleyiniz/ düzeltiniz.");
                UrunFiyatiText.Clear();
                UrunFiyatiText.Focus();
            }
        }
        // kontrolToolTip.Show("Bu alana adınızı ve soyadınızı boşluklu ve baş harfleri büyük olacak biçimde yazınız.", adsoyadtext, adsoyadtext.Width, adsoyadtext.Height - 20, 5000);
        private void UrunOzellik1Text_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Anakart") // AnaKart
            {
                kontrolToolTip.Show("Bu alana işlemci uyumluluk bilgisini giriniz.\nİntel işlemcilerde LGA, AMD işlemcilerde AM4 ya da AM5 bilgisi.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Ekran Kartı") // Ekran Kartı
            {
                kontrolToolTip.Show("Bu alana bellek bilgisini yazınız.\nBellek bilgisi GB cinsinden olmalı.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Ram") // RAM
            {
                kontrolToolTip.Show("Bu alana bellek bilgisini yazınız.\nBellek bilgisi GB cinsinden olmalı.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "İşlemci") // İşlemci
            {
                kontrolToolTip.Show("Bu alana soket bilgisini yazınız.\nİntel işlemcilerde LGA, AMD işlemcilerde AM4 ya da AM5 bilgisi.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "İşlemci Soğutucu") // İşlemci Soğutucu
            {
                kontrolToolTip.Show("Bu alana Intel işlemcilere uyumlu soket bilgisini giriniz.\nİntel işlemcilerde LGA bilgisi", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "M2-SSD") // M2-SSD
            {
                kontrolToolTip.Show("Bu alana kapasite bilgisini giriniz.\nKapasite bilgisi GB cinsinden olmalı.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "SSD" || comboBox1.Text == "HDD") // SSD- HDD
            {
                kontrolToolTip.Show("Bu alana kapasite bilgisini giriniz.\nKapasite bilgisi GB cinsinden olmalı.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Kasa") // Kasa
            {
                kontrolToolTip.Show("Bu alana kasa tipini giriniz.\nFull Tower, ATX, Micro-ATX", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "PSU") // PSU
            {
                kontrolToolTip.Show("Bu alana güç kapasitesi bilgisini giriniz.\nGüç bilgisi W cinsinden olmalı.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Monitör") // Monitör
            {
                kontrolToolTip.Show("Bu alana ekran tazeleme hız bilgisini giriniz.\nTazeleme hız bilgisi Hz cinsinden olmalı.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
            else
            {
                kontrolToolTip.Show("Bu alana ürünün önemli özelliğini yazınız.", UrunOzellik1Text, UrunOzellik1Text.Width, UrunOzellik1Text.Height - 20, 5000);
            }
        }
        private void UrunOzellik2Text_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Anakart") // AnaKart
            {
                kontrolToolTip.Show("Bu alana RAM uyumluluk bilgisini giriniz.\nRAM uyumluluk bilgisi DDR cinsinden olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Ekran Kartı") // Ekran Kartı
            {
                kontrolToolTip.Show("Bu alana ekran kartının Bit bilgisini giriniz.\nBit bilgisi 128 Bit, 196 Bit vs. şeklinde olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Ram") // RAM
            {
                kontrolToolTip.Show("Bu alana RAM uyumluluk bilgisini giriniz.\nRAM uyumluluk DDR cinsinden olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "İşlemci") // İşlemci
            {
                kontrolToolTip.Show("Bu alana işlemci çekirdek bilgisini giriniz.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "İşlemci Soğutucu") // İşlemci Soğutucu
            {
                kontrolToolTip.Show("Bu alana İşlemci AMD uyumluluk bilgisini giriniz.\nAMD işlemcilerde AM4, AM5 bilgisi", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "M2-SSD") // M2-SSD
            {
                kontrolToolTip.Show("Bu alana okuma ve yazma hızı bilgisini yazınız\nÖrneğin 7500 MB/s - 3500 MB/s şeklinde olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "SSD" || comboBox1.Text == "HDD") // SSD- HDD
            {
                kontrolToolTip.Show("Bu alana okuma ve yazma hızı bilgisini yazınız\nÖrneğin 7500 MB/s - 3500 MB/s şeklinde olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Kasa") // Kasa
            {
                kontrolToolTip.Show("Bu alana PSU varlığı bilgisini yazınız.\nVar ya da Yok", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "PSU") // PSU
            {
                kontrolToolTip.Show("Bu alana verimlilik sınıfının bilgsini yazınız.\n80 Plus bilgisi burada olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Monitör") // Monitör
            {
                kontrolToolTip.Show("Bu alana tepki süresini yazınız.\nTepki süresi ms cinsinden olmalı.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
            else
            {
                kontrolToolTip.Show("Bu alana ürünün önemli özelliğini yazınız.", UrunOzellik2Text, UrunOzellik2Text.Width, UrunOzellik2Text.Height - 20, 5000);
            }
        }
        // kontrolToolTip ile bilgi vermek için kullanılır.
        private void UrunDigerOzellıklerText_Enter(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Anakart")
            {
                kontrolToolTip.Show("Bu alana ürünün genel özelliklerini yazınız.\nFull Tower, ATX, Micro-ATX bilgisi bu alana yazılmalı", UrunDigerOzellıklerText, UrunDigerOzellıklerText.Width, UrunDigerOzellıklerText.Height - 20, 5000);
            }
            else
                kontrolToolTip.Show("Bu alana ürünün genel özelliklerini yazınız.", UrunDigerOzellıklerText, UrunDigerOzellıklerText.Width, UrunDigerOzellıklerText.Height - 20, 5000);
        }
        // kontrolToolTip ile bilgi vermek için kullanılır.
        private void UrunOzellik3Text_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Anakart") // AnaKart
            {
                kontrolToolTip.Show("Bu alana PCI Express yuva bilgisini yazınız.\n PCI Express bilgisi PCI Express X16 şeklindedir.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Ekran Kartı") // Ekran Kartı
            {
                kontrolToolTip.Show("Bu alana PCI Express yuva bilgisini yazınız.\n PCI Express bilgisi PCI Express X16 şeklindedir.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Ram") // RAM
            {
                kontrolToolTip.Show("Bu alana hız bilgisini yazınız.\nHız bilgisi MHz cinsinden olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "İşlemci") // İşlemci
            {
                kontrolToolTip.Show("Bu alana işlemci hızını yazınız.\nİşlemci hızı GHz cinsinden olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "İşlemci Soğutucu") // İşlemci Soğutucu
            {
                kontrolToolTip.Show("Bu alana fan boyut bilgisini yazınız.\nFan boyut bilgisi mm cinsinden olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "M2-SSD") // M2-SSD
            {
                kontrolToolTip.Show("Bu alana M2-SSD PCI-e bilgisini yazınız.\nPCI-e bilgisi PCI-e 4-0 vs. şeklinde olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "SSD" || comboBox1.Text == "HDD") // SSD- HDD
            {
                kontrolToolTip.Show("Bu alana bağlantı arayüzü bilgisini giriniz.\nBağlantı arayüzü E-sata vs. şeklinde olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Kasa") // Kasa
            {
                kontrolToolTip.Show("Bu alana gereken diğer özellikleri giriniz.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "PSU") // PSU
            {
                kontrolToolTip.Show("Bu alana ses bilgisini yazınız.\nSesli ya da Sessiz şeklinde olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else if (comboBox1.Text == "Monitör") // Monitör
            {
                kontrolToolTip.Show("Bu alana boyut bilgisini giriniz.\nBoyut bilgisi İnç cinsinden olmalı.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
            else
            {
                kontrolToolTip.Show("Bu alana ürünün önemli özelliğini yazınız.", UrunOzellik3Text, UrunOzellik3Text.Width, UrunOzellik3Text.Height - 20, 5000);
            }
        }

        private void UrunOzellik3Text_Leave(object sender, EventArgs e)  // UrunOzellik3Text kutusu boşsa veya uygun değilse buraya odaklanır.
        {
            if (string.IsNullOrEmpty(UrunOzellik3Text.Text))
            {
                MessageBox.Show("Bütün bilgilerin doldurunuz.");
                UrunOzellik3Text.Focus();
            }
        }

        private void UrunOzellik2Text_Leave(object sender, EventArgs e)  // UrunOzellik2Text kutusu boşsa veya uygun değilse buraya odaklanır.
        {
            if (string.IsNullOrEmpty(UrunOzellik2Text.Text))
            {
                MessageBox.Show("Bütün bilgilerin doldurunuz.");
                UrunOzellik2Text.Focus();
            }
        }

        private void UrunLinkText_Leave(object sender, EventArgs e)  // UrunLinkText kutusu boşsa veya link bilgisi yanlışsa buraya odaklanır.
        {
            Regex regex = new Regex(@"\b(?:https?|ftp):\/\/\S+\b");
            if (!regex.Match(UrunLinkText.Text).Success)
            {
                MessageBox.Show("Ürün linkini doğru şekilde yazınız!");
                UrunLinkText.Focus();
            }
        }

        private void UrunMarkaText_Leave(object sender, EventArgs e)  // UrunMarkaText kutusu boşsa buraya odaklanır.
        {
            if (string.IsNullOrEmpty(UrunMarkaText.Text))
            {
                MessageBox.Show("Ürün marka bilgisini eksiksiz doldurunuz!");
                UrunMarkaText.Focus();
            }
        }

        private void UrunModelText_Leave(object sender, EventArgs e) // UrunModelText kutusu boşsa buraya odaklanır.
        {
            if (string.IsNullOrEmpty(UrunMarkaText.Text))
            {
                MessageBox.Show("Ürün model bilgisini eksiksiz doldurunuz!");
                UrunModelText.Focus();
            }
        }

        private void UrunDigerOzellıklerText_Leave(object sender, EventArgs e) // UrunDigerOzellıklerText kutusu boşsa buraya odaklanır.
        {
            if (string.IsNullOrEmpty(UrunDigerOzellıklerText.Text))
            {
                MessageBox.Show("Ürün diğer özellikler bilgisini eksiksiz doldurunuz!");
                UrunDigerOzellıklerText.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Anakart") // Anakart
            {
                labelozelik1.Text = "İşlemci uyumluluk bilgisi";
                labelozellik2.Text = "RAM uyumluluk bilgisi";
                labelozellik3.Text = "PCI Express uyumluluk bilgisi";
            }
            else if (comboBox1.Text == "Ekran Kartı") // Ekran Kartı 
            {
                labelozelik1.Text = "Bellek bilgisi";
                labelozellik2.Text = "Bit bilgisini";
                labelozellik3.Text = "PCI Express yuva bilgisi";
            }
            else if (comboBox1.Text == "Ram") // RAM
            {
                labelozelik1.Text = "Bellek bilgisi";
                labelozellik2.Text = "Soket bilgisi";
                labelozellik3.Text = "Hız bilgisi";
            }
            else if (comboBox1.Text == "İşlemci") // İşlemci
            {
                labelozelik1.Text = "Soket bilgisi";
                labelozellik2.Text = "Çekirkek bilgisi";
                labelozellik3.Text = "GHz bilgisi";
            }
            else if (comboBox1.Text == "İşlemci Soğutucu") // İşlemci Soğutucu
            {
                labelozelik1.Text = "İşlemci Intel uyumluluk bilgisi";
                labelozellik2.Text = "İşlemci AMD uyumluluk bilgisi";
                labelozellik3.Text = "Fan boyut bilgisi";
            }
            else if (comboBox1.Text == "M2-SSD") // M2-SSD
            {
                labelozelik1.Text = "Kapasite bilgisi";
                labelozellik2.Text = "Okuma ve yazma bilgisi";
                labelozellik3.Text = "PCI-e Bilgisi";
            }
            else if (comboBox1.Text == "Kasa") // Kasa
            {
                labelozelik1.Text = "Kasa tipi";
                labelozellik2.Text = "PSU varlık bilgisi";
                labelozellik3.Text = "Gereken diğer özellik";
            }
            else if (comboBox1.Text == "Monitör") // Monitör
            {
                labelozelik1.Text = "Tazeleme hızı bilgisi";
                labelozellik2.Text = "Tepki süresi";
                labelozellik3.Text = "İnç bilgisi";
            }
            else if (comboBox1.Text == "SSD" || comboBox1.Text == "HDD") // SSD, HDD
            {
                labelozelik1.Text = "Kapasite bilgisi";
                labelozellik2.Text = "Okuma ve yazma bilgisi";
                labelozellik3.Text = "Bağlantı arayüzü";
            }
            else if (comboBox1.Text == "PSU") // PSU
            {
                labelozelik1.Text = "Güç kapasitesi bilgisi";
                labelozellik2.Text = "Verimlilik sınıfı bilgisi";
                labelozellik3.Text = "Sessizlik bilgisi";
            }
            else if (comboBox1.Text == "Diğer")
            {
                labelozelik1.Text = "Birinci özellik";
                labelozellik2.Text = "İkinci özellik";
                labelozellik3.Text = "Üçüncü özellik";
                string UrunTur = Interaction.InputBox("Lütfen ürünün türünü giriniz.", "Ürün Tür Gİrişi");
                if (string.IsNullOrEmpty(UrunTur))
                {
                    MessageBox.Show("Ürün türünü boş bırakamazsınız!");
                    return;
                }
                comboBox1.Items.Remove("Diğer");
                comboBox1.Items.Add(UrunTur);
                comboBox1.Text = UrunTur;
                comboBox1.Items.Add("Diğer");
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(kategoriEkleString, connection))
                        {
                            command.Parameters.AddWithValue("@kategoriAdi", UrunTur);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Kategori ekleme hatası");
                }
            }
            else
            {
                labelozelik1.Text = "Birinci özellik";
                labelozellik2.Text = "İkinci özellik";
                labelozellik3.Text = "Üçüncü özellik";
            }
        }

        private void temizlebuton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            dialogResult = MessageBox.Show("Yazılı bilgileri temizlemek istiyor musunuz?", "Temizleme Bilgisi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK)
            {
                VerileriTemizle();
            }
        }
        void VerileriTemizle()
        {
            comboBox1.Text = "Seçiniz!"; UrunMarkaText.Clear(); UrunModelText.Clear();
            UrunOzellik1Text.Clear(); UrunOzellik2Text.Clear(); UrunOzellik3Text.Clear();
            UrunFiyatiText.Clear(); UrunStokText.Clear(); UrunDigerOzellıklerText.Clear();
            UrunLinkText.Clear(); UrunFoto.Image = null;
        }
        private void urunsil_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            dialogResult = MessageBox.Show("Bu ürünü silmek istiyor musunuz?", "Ürün Silme", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK)
            {
                int UrunID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deleteString, connection))
                    {
                        command.Parameters.AddWithValue("@UrunID", UrunID);
                        command.ExecuteNonQuery();
                        DataGridBilgiYazdır();
                    }
                    connection.Close();
                }
            }
        }

        private void urunguncelle_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            dialogResult = MessageBox.Show("Bu ürünün güncelleme işlemini onaylıyor musunuz?", "Ürün Güncelle", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK)
            {
                string UrunTur = comboBox1.Text.ToString().TrimEnd(), UrunMarka = UrunMarkaText.Text.ToString().TrimEnd(),
                       UrunModel = UrunModelText.Text.ToString().TrimEnd(), UrunOzellik1 = UrunOzellik1Text.Text.ToString().TrimEnd(),
                       UrunOzellik2 = UrunOzellik2Text.Text.ToString().TrimEnd(), UrunOzellik3 = UrunOzellik3Text.Text.ToString().TrimEnd(),
                       UrunDigerOzellikler = UrunDigerOzellıklerText.Text.ToString().TrimEnd(), UrunLink = UrunLinkText.Text.ToString().TrimEnd();
                int UrunID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                int UrunStok = Convert.ToInt32(UrunStokText.Text.ToString().TrimEnd());
                double UrunFiyati = Convert.ToDouble(UrunFiyatiText.Text.ToString().TrimEnd());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updateString, connection))
                    {
                        command.Parameters.AddWithValue("@UrunID", UrunID);
                        command.Parameters.AddWithValue("@UrunTur", UrunTur);
                        command.Parameters.AddWithValue("@UrunMarka", UrunMarka);
                        command.Parameters.AddWithValue("@UrunModel", UrunModel);
                        command.Parameters.AddWithValue("@UrunOzellik1", UrunOzellik1);
                        command.Parameters.AddWithValue("@UrunOzellik2", UrunOzellik2);
                        command.Parameters.AddWithValue("@UrunOzellik3", UrunOzellik3);
                        command.Parameters.AddWithValue("@UrunFiyati", UrunFiyati);
                        command.Parameters.AddWithValue("@UrunStok", UrunStok);
                        command.Parameters.AddWithValue("@UrunDigerOzellikler", UrunDigerOzellikler);
                        command.Parameters.AddWithValue("@UrunLink", UrunLink);

                        command.ExecuteNonQuery();
                        DataGridBilgiYazdır();
                    }
                    connection.Close();
                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            VerileriTemizle();
            byte[] imageBytes = (byte[])dataGridView1.CurrentRow.Cells[4].Value;
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            UrunMarkaText.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            UrunModelText.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                UrunFoto.Image = Image.FromStream(ms);
            }
            UrunLinkText.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            UrunFiyatiText.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            UrunStokText.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            UrunOzellik1Text.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            UrunOzellik2Text.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            UrunOzellik3Text.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            UrunDigerOzellıklerText.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
        }

        private void urunFotoGuncelle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Resim Seç";
            openFileDialog.Filter = "Png Dosyaları(*.png)|*.png| Jpg Dosyaları (*.jpg)| *.jpg| Jpeg Dosyaları(*.jpeg)|*.jpeg| Gif Dosyaları(*.gif)|*.gif| Tif Dosyaları (*.tif)|*tif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                UrunFoto.Image = Image.FromFile(openFileDialog.FileName);
            DialogResult dialogResult;
            dialogResult = MessageBox.Show("Bu ürünün güncelleme işlemini onaylıyor musunuz?", "Ürün Güncelle", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    string UrunTur = comboBox1.Text.ToString().TrimEnd(), UrunMarka = UrunMarkaText.Text.ToString().TrimEnd(),
                           UrunModel = UrunModelText.Text.ToString().TrimEnd(), UrunOzellik1 = UrunOzellik1Text.Text.ToString().TrimEnd(),
                           UrunOzellik2 = UrunOzellik2Text.Text.ToString().TrimEnd(), UrunOzellik3 = UrunOzellik3Text.Text.ToString().TrimEnd(),
                           UrunFiyati = UrunFiyatiText.Text.ToString().TrimEnd(), UrunStok = UrunStokText.Text.ToString().TrimEnd(),
                           UrunDigerOzellikler = UrunDigerOzellıklerText.Text.ToString().TrimEnd(), UrunLink = UrunLinkText.Text.ToString().TrimEnd();
                    int UrunID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());


                    Image img = UrunFoto.Image;
                    SqlParameter param = new SqlParameter("@UrunFoto", SqlDbType.Image);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    param.Value = ms.ToArray();
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(updateString, connection))
                            {
                                command.Parameters.AddWithValue("@UrunID", UrunID);
                                command.Parameters.AddWithValue("@UrunTur", UrunTur);
                                command.Parameters.AddWithValue("@UrunMarka", UrunMarka);
                                command.Parameters.AddWithValue("@UrunModel", UrunModel);
                                command.Parameters.AddWithValue("@UrunOzellik1", UrunOzellik1);
                                command.Parameters.AddWithValue("@UrunOzellik2", UrunOzellik2);
                                command.Parameters.AddWithValue("@UrunOzellik3", UrunOzellik3);
                                command.Parameters.AddWithValue("@UrunFiyati", UrunFiyati);
                                command.Parameters.AddWithValue("@UrunStok", UrunStok);
                                command.Parameters.AddWithValue("@UrunDigerOzellikler", UrunDigerOzellikler);
                                command.Parameters.AddWithValue("@UrunLink", UrunLink);
                                command.Parameters.Add(param);

                                command.ExecuteNonQuery();
                                DataGridBilgiYazdır();
                            }
                            connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
    }
}
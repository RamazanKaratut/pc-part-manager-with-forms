using System.Data.SqlClient;
using System.Diagnostics;

namespace Computer_Design
{
    public partial class MagazaSayfasi : Form
    {
        public MagazaSayfasi()
        {
            InitializeComponent();
        }
        List<ÜrünlerDataBase> ürünlerListesi = new List<ÜrünlerDataBase>();
        public class ÜrünlerDataBase
        {
            public int UrunID { get; set; }
            public string UrunTuru { get; set; }
            public string UrunMarka { get; set; }
            public string UrunModel { get; set; }
            public Image UrunFoto { get; set; }
            public string UrunLink { get; set; }
            public double UrunFiyati { get; set; }
            public int UrunStok { get; set; }
            public string UrunOzellik1 { get; set; }
            public string UrunOzellik2 { get; set; }
            public string UrunOzellik3 { get; set; }
            public string UrunDigerOzellikler { get; set; }
        }

        List<GeçmişSipariş> geçmişSipariş = new List<GeçmişSipariş>();
        class GeçmişSipariş
        {
            public int GeçmişBilgi { get; set; }
            public DateTime siparisTarihi { get; set; }
        }
        class YorumlarSınıfı
        {
            public int kullanici { get; set; }
            public int ürün { get; set; }
            public string yorumu { get; set; }
            public int yıldız { get; set; }
            public string yorumYazan { get; set; }
        }
        List<YorumlarSınıfı> YorumlarListesi = new List<YorumlarSınıfı>();

        void ListeyeÜrünEkle(ÜrünlerDataBase ürün)
        {
            ürünlerListesi.Add(ürün);
        }
        List<ÜrünlerDataBase> ürünSepetListesi = new List<ÜrünlerDataBase>();
        List<ÜrünlerDataBase> ürünSıralama = new List<ÜrünlerDataBase>();
        public List<ÜrünlerDataBase> ürünSepetListesiDeneme = new List<ÜrünlerDataBase>();
        List<ÜrünlerDataBase> geçmişSiparişler = new List<ÜrünlerDataBase>();
        List<string> KategoriList = new List<string>();
        public int KullaniciID;

        string connectionString = @"Data Source=DESKTOP-QUNJPTH\SQLEXPRESS;Initial Catalog=ComputerDesign;Integrated Security=True";
        string kayıtkontrolString = "select *from KULLANICI where kullaniciID = @kullaniciID";
        string ürünlerString = "select *from URUNLISTESI";
        string comboString = "select *from KATEGORiLER";
        string siparişString = "select *from SIPARIS where kullaniciID = @kullaniciID";
        string siparişDetayString = "select *from siparisDetay where siparisID = @siparisID";
        string yorumKontrolString = "select *from URUNYORUM where kullaniciID = @KullaniciID and UrunID = @UrunID";
        string yorumString = "insert into URUNYORUM (kullaniciID, UrunID, urunYorum, urunYildizi) values (@KullaniciID, @UrunID, @UrunYorum, @UrunYildizi)";
        string yorumSilString = "delete from URUNYORUM where kullaniciID = @KullaniciID and UrunID = @UrunID";
        string yorumGüncelleString = "update URUNYORUM set kullaniciID = @KullaniciID, UrunID = @UrunID, urunYorum = @UrunYorum, urunYildizi = @UrunYildizi where kullaniciID = @KullaniciID and UrunID = @UrunID";
        string yorumlarıAlString = "select *from URUNYORUM";
        string kullaniciString = "select *from KULLANICI where kullaniciID = @KullaniciID";
        int y_konumu = 10;
        int ykonumu = 10;
        int m = 0; // bulunduğumuz sayfa
        int n = 3; // gösterilecek ürün sayısı
        int sepetM = 0;
        int sepetN = 2;
        int index = 0;
        Random random = new Random();
        int randomFoto = 0;
        Panel panelSepet = null;
        Form sepet = null;
        string ürünNe;
        string ürünNe2;
        string ürünNe3;
        string ürünNe4;
        private void MagazaSayfasi_Load(object sender, EventArgs e)
        {
            ürünYorumlarınıEkle();
            KategorileriEkle();
            kategoriListEkle();
            KullanıcıBilgiYazdır();
            VeriTabanındanVeriAl();
            ListedenKopyala();
            ListeleKonumu();
            sepetiKontrolEt();
            rastgeleVeriGöster();
            PaneleVeriYazdır();
            siparişGeçmişiniAl();
        }
        void ürünYorumlarınıEkle()  // Veri tabanından ürün yorumlarını alır.
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(yorumlarıAlString, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            YorumlarSınıfı ys = new YorumlarSınıfı();
                            ys.kullanici = reader.GetInt32(1);
                            ys.ürün = reader.GetInt32(2);
                            ys.yorumu = reader.GetString(3);
                            ys.yıldız = reader.GetInt32(4);

                            YorumlarListesi.Add(ys);
                        }
                        reader.Close();
                    }
                }
            }
            foreach (var items in YorumlarListesi)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(kullaniciString, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciID", items.kullanici);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                items.yorumYazan = reader.GetString(1);
                        }
                    }
                }
            }
        }

        void siparişGeçmişiniAl()  // Veri tabanından kullanıcının yaptığı siparişleri alır.
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(siparişString, connection))
                {
                    command.Parameters.AddWithValue("@kullaniciID", KullaniciID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GeçmişSipariş geçmiş = new GeçmişSipariş();
                            geçmiş.GeçmişBilgi = reader.GetInt32(0);
                            geçmiş.siparisTarihi = reader.GetDateTime(3);
                            geçmişSipariş.Add(geçmiş);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }
        }
        Panel geçmişPanel = null;
        Form geçmişForm = null;
        void siparişGeçmişiniYükle()  // Veri tabanından alınan sipariş geçmişini geçmişSiparişler listesine eklemek için kullanılır.
        {
            int i = 1;
            int ykonum = 10;
            foreach (var items in geçmişSipariş)
            {
                Button buton = new Button();
                buton.Text = i.ToString() + ". siparişinizi " + items.siparisTarihi + " tarihinde yaptınız";
                buton.Location = new Point(10, ykonum);
                buton.Size = new Size(520, 80);
                buton.Click += (sender, e) =>
                {
                    geçmişPanel.Controls.Clear();
                    ykonumu = 10;
                    geçmişSiparişler.Clear();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(siparişDetayString, connection))
                        {
                            cmd.Parameters.AddWithValue("@siparisID", items.GeçmişBilgi);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreach (var items1 in ürünlerListesi)
                                    {
                                        if (reader.GetInt32(2) == items1.UrunID)
                                        {
                                            geçmişSiparişler.Add(items1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GeçmişListeVerisiYazdir();
                };
                i++;
                geçmişPanel.Controls.Add(buton);
                ykonum += 100;
            }

            geçmişForm.FormClosing += (sender, e) =>
            {
                geçmişForm.Hide();
                gecmisSiparislerLabel.Enabled = true;
            };
        }
        void GeçmişListeVerisiYazdir()  // geçmişSiparişler listesini kontrol edip ekrana yazdırır.
        {
            ykonumu = 10;
            Label labelgeri = new Label();
            labelgeri.Text = "Geri Dön";
            labelgeri.Location = new Point(10, 0);
            labelgeri.Click += (sender, e) =>
            {
                geçmişPanel.Controls.Clear();

                siparişGeçmişiniYükle();
            };
            geçmişPanel.Controls.Add(labelgeri);
            ykonumu = 20;

            foreach (var items in geçmişSiparişler)
            {
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
                    PictureVeriGöster(items, false, true);
                };

                Label fiyatLabel = new Label();
                fiyatLabel.Text = items.UrunFiyati + " TL";
                fiyatLabel.Location = new Point(450, ykonumu + 25);
                fiyatLabel.AutoSize = true;

                geçmişPanel.Controls.Add(fiyatLabel);
                geçmişPanel.Controls.Add(sepetLabel);
                geçmişPanel.Controls.Add(pctr);

                ykonumu += 100;
            }
        }
        void sepetiKontrolEt()  // SatınAl formundan bu forma geçiş yaptığında sepet sıfırlanmaması için kullanılır.
        {
            ürünSepetListesi.Clear();
            if (ürünSepetListesiDeneme.Count == 0)
            {
            }
            else
            {
                foreach (var items in ürünSepetListesiDeneme)
                {
                    foreach (var items2 in ürünlerListesi)
                    {
                        if (items2.UrunID == items.UrunID)
                        {
                            ürünSepetListesi.Add(items2);
                        }
                    }
                }
            }
        }
        void kategoriListEkle()  
        {
            foreach (var items in KategoriList)
            {
                kategoriSecCombo.Items.Add(items);
            }
        }
        void ListeleKonumu()  // Ekran açıldığında kategori seçim paneli ekrandan çıkartılır.
        {
            panelListe.Location = new Point(-380, 300);
        }
        void KategorileriEkle()  // kategoriSecCombo ifadesine kategorileri ekler
        {
            kategoriSecCombo.Items.Add("Hepsi");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(comboString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KategoriList.Add(reader.GetString(1));
                        }
                    }
                }
                connection.Close();
            }
        }
        void ListedenKopyala()
        {
            foreach (var items in ürünlerListesi)
            {
                if (items.UrunStok != 0)
                    ürünSıralama.Add(items);
            }
        }
        void PaneleVeriYazdır()  // Oluşturulan panele 3 tane veri yazdırmak için kullanılır. 
        {
            panelDeneme.Controls.Clear();

            if (index == 14)
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Toplama işlemi tamamlandı. Ödeme işlemine gitmek ister misiniz?", "Ödeme Ekranına Geçiş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    index = 0;
                    SatınAl satınAl = new SatınAl();
                    satınAl.ürünListesi = ürünSepetListesi;
                    satınAl.KullaniciID = KullaniciID;
                    this.Hide();
                    satınAl.Show();
                }
                else if (dialog == DialogResult.Cancel)
                {
                    index = 0;
                    foreach (var items in ürünlerListesi)
                    {
                        ürünSıralama.Add(items);
                    }
                    m = 0;
                    y_konumu = 10;
                    kendinTopla.Text = "Kendin topla";
                    label6.Visible = true;
                    kategoriSecCombo.Visible = true;
                    panelListe.Size = new Size(357, 200);
                    PaneleVeriYazdır();
                }
            }
            var ilkÜçÜrün = ürünSıralama.Skip(m * n).Take(n);
            foreach (var items in ilkÜçÜrün)
            {
                Label label = new Label();
                label.Text = items.UrunMarka + " " + items.UrunTuru + "\n" + items.UrunModel;
                label.AutoSize = true;
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Location = new Point(5, y_konumu + 25);

                Label özellikLabel = new Label();
                özellikLabel.Text = items.UrunOzellik1 + "\n" + items.UrunOzellik2 + "\n" + items.UrunOzellik3;
                özellikLabel.AutoSize = true;
                özellikLabel.TextAlign = ContentAlignment.MiddleLeft;
                özellikLabel.Location = new Point(350, y_konumu + 15);

                Label diğerÖzelliklerLabel = new Label();
                diğerÖzelliklerLabel.Text = items.UrunDigerOzellikler;
                diğerÖzelliklerLabel.TextAlign = ContentAlignment.MiddleCenter;
                diğerÖzelliklerLabel.Size = new Size(450, 75);
                diğerÖzelliklerLabel.Location = new Point(500, y_konumu);

                Label yeniLabel = new Label();
                yeniLabel.Text = items.UrunFiyati.ToString() + " TL" + "\n" + items.UrunStok + " tane mevcut";
                yeniLabel.AutoSize = true;
                yeniLabel.Location = new Point(1060, y_konumu + 20);

                System.Windows.Forms.Button button = new System.Windows.Forms.Button();
                button.Size = new Size(100, 40);
                button.Location = new Point(1165, y_konumu + 15);
                if (!ürünSepetListesi.Contains(items))
                {
                    button.Enabled = true;
                    button.Text = "Sepete ekle";
                    if (kendinTopla.Text == "Kendin topla")
                    {
                        button.Click += (sender, e) =>
                        {
                            index = 0;
                            ürünSepetListesi.Add(items);
                            y_konumu = 10;
                            PaneleVeriYazdır();
                        };
                    }
                    else
                    {
                        button.Click += (sender, e) =>
                        {
                            if (index == 0)
                            {
                                butonAtla.Visible = false;
                                ürünNe = items.UrunOzellik1;
                                ürünNe2 = items.UrunOzellik2;
                                ürünNe3 = items.UrunOzellik3;
                                ürünNe4 = items.UrunDigerOzellikler;
                            }
                            if (index == 5 && items.UrunOzellik2 == "Var")
                            {
                                index++;
                            }
                            ürünSepetListesi.Add(items);
                            ürünSıralama.Clear();
                            index++;
                            if (index < KategoriList.Count)
                            {
                                foreach (var items in ürünlerListesi)
                                {
                                    if (items.UrunTuru == KategoriList[index])
                                    {
                                        if (index == 1)
                                        {
                                            if (items.UrunOzellik1 == ürünNe)
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                        }
                                        else if (index == 2)
                                        {
                                            if (items.UrunOzellik1 == ürünNe || items.UrunOzellik2 == ürünNe)
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                        }
                                        else if (index == 3)
                                        {
                                            if (items.UrunOzellik3 == ürünNe3)
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                        }
                                        else if (index == 4)
                                        {
                                            if (items.UrunOzellik2 == ürünNe2)
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                        }
                                        else if (index == 5)
                                        {
                                            if (ürünNe4.Contains(items.UrunOzellik1) && items.UrunOzellik1 == "Micro-ATX")
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                            else if ((items.UrunOzellik1 == "ATX" && ürünNe4.Contains("Micro-ATX")) || (items.UrunOzellik1 == "ATX" && ürünNe4.Contains("ATX")))
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                            else if ((items.UrunOzellik1 == "Full Tower" && ürünNe4.Contains("Micro-ATX")) || (items.UrunOzellik1 == "Full Tower" && ürünNe4.Contains("ATX")) || (items.UrunOzellik1 == "Full Tower" && ürünNe4.Contains("Full Tower")))
                                            {
                                                ürünSıralama.Add(items);
                                            }
                                        }
                                        else if (index == 6)
                                        {
                                            ürünSıralama.Add(items);
                                        }
                                        else if (index == 7)
                                        {
                                            butonAtla.Visible = true;
                                            ürünSıralama.Add(items);
                                        }
                                        else
                                        {
                                            ürünSıralama.Add(items);
                                        }
                                    }
                                }
                            }
                            y_konumu = 10;
                            m = 0;
                            PaneleVeriYazdır();
                        };
                    }
                    panelDeneme.Controls.Add(button);
                }
                else if (ürünSepetListesi.Contains(items))
                {
                    button.Text = "Sepetten çıkart";
                    button.Click += (sender, e) =>
                    {
                        ürünSepetListesi.Remove(items);
                        y_konumu = 10;
                        PaneleVeriYazdır();
                        sepetPanelineVeriYazdır();
                    };
                    panelDeneme.Controls.Add(button);
                }
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = items.UrunFoto;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Size = new Size(75, 75);
                pictureBox.Location = new Point(250, y_konumu + 5);
                pictureBox.Click += (sender, e) =>
                {
                    PictureVeriGöster(items, true, false);
                };

                y_konumu += 100;

                panelDeneme.Controls.Add(diğerÖzelliklerLabel);
                panelDeneme.Controls.Add(özellikLabel);
                panelDeneme.Controls.Add(yeniLabel);
                panelDeneme.Controls.Add(label);
                panelDeneme.Controls.Add(pictureBox);
            }

            if (m <= 0)
                oncekiSayfaButon.Enabled = false;
            else
                oncekiSayfaButon.Enabled = true;

            if ((m + 1) * n >= ürünSıralama.Count)
                sonrakiSayfaButon.Enabled = false;
            else
                sonrakiSayfaButon.Enabled = true;
        }
        void VeriTabanındanVeriAl()  // Veritabanındaki verileri listeye kaydeder
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(ürünlerString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ÜrünlerDataBase ürün = new ÜrünlerDataBase();
                            ürün.UrunID = reader.GetInt32(0);
                            ürün.UrunTuru = reader.GetString(1);
                            ürün.UrunMarka = reader.GetString(2);
                            ürün.UrunModel = reader.GetString(3);
                            byte[] fotobyte = (byte[])reader.GetValue(4);
                            using (MemoryStream ms = new MemoryStream(fotobyte))
                            {
                                ürün.UrunFoto = Image.FromStream(ms);
                            }
                            ürün.UrunLink = reader.GetString(5);
                            ürün.UrunFiyati = reader.GetDouble(6);
                            ürün.UrunStok = reader.GetInt32(7);
                            ürün.UrunOzellik1 = reader.GetString(8);
                            ürün.UrunOzellik2 = reader.GetString(9);
                            ürün.UrunOzellik3 = reader.GetString(10);
                            ürün.UrunDigerOzellikler = reader.GetString(11);

                            ListeyeÜrünEkle(ürün);
                        }
                    }
                }
            }
        }
        void KullanıcıBilgiYazdır()  // Ekranda kullanıcının kim olduğunu yazdırır.
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
        private void MagazaSayfasi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox1_Click(object sender, EventArgs e)  // Sepet ekranını açar.
        {
            sepet = new Form();
            this.AddOwnedForm(sepet);
            pictureBox1.Enabled = false;
            sepet.Size = new Size(600, 285);
            sepet.StartPosition = FormStartPosition.Manual;
            sepet.Location = new Point(1010, 300);
            sepet.BackColor = Color.White;
            Bitmap bitmap = Properties.Resources.Graphicloads_100_Flat_Cart;
            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            sepet.Icon = icon;
            bitmap.Dispose();
            icon.Dispose();
            sepet.Text = "Sepetiniz";
            sepet.MaximizeBox = false;
            sepet.MinimizeBox = false;
            sepet.Show();
            panelSepet = new Panel();
            panelSepet.Size = new Size(530, 240);
            panelSepet.Location = new Point(0, 10);
            sepet.Controls.Add(panelSepet);
            sepet.FormBorderStyle = FormBorderStyle.FixedSingle;
            sepetPanelineVeriYazdır();
        }

        void sepetPanelineVeriYazdır()  // Seçilen ürünü sepete ekler. Ve sepet panelinde görüntüler
        {
            System.Windows.Forms.Button butonAşağı = new System.Windows.Forms.Button();
            butonAşağı.Size = new Size(40, 80);
            butonAşağı.Location = new Point(540, 100);
            butonAşağı.BackgroundImage = Properties.Resources.Steve_Zondicons_Arrow_Thick_Down_128;
            butonAşağı.BackgroundImageLayout = ImageLayout.Stretch;
            butonAşağı.Click += (sender, e) =>
            {
                if ((sepetM + 1) * sepetN >= ürünSepetListesi.Count)
                {
                }
                else
                {
                    panelSepet.Controls.Clear();
                    ykonumu = 10;
                    sepetM++;
                    sepetPanelineVeriYazdır();
                }
            };
            System.Windows.Forms.Button butonYukarı = new System.Windows.Forms.Button();
            butonYukarı.Location = new Point(540, 20);
            butonYukarı.Size = new Size(40, 80);
            butonYukarı.BackgroundImage = Properties.Resources.Steve_Zondicons_Arrow_Thick_Up_128;
            butonYukarı.BackgroundImageLayout = ImageLayout.Stretch;

            butonYukarı.Click += (sender, e) =>
            {
                if (sepetM <= 0)
                {
                }
                else
                {
                    panelSepet.Controls.Clear();
                    ykonumu = 10;
                    sepetM--;
                    sepetPanelineVeriYazdır();
                }
            };

            sepet.Controls.Add(butonAşağı);
            sepet.Controls.Add(butonYukarı);

            System.Windows.Forms.Button buttonSipariş = new System.Windows.Forms.Button();
            buttonSipariş.Size = new Size(40, 40);
            buttonSipariş.Location = new Point(540, 200);
            buttonSipariş.BackgroundImage = Properties.Resources.Graphicloads_100_Flat_Cart_128__1_;
            buttonSipariş.BackgroundImageLayout = ImageLayout.Stretch;
            buttonSipariş.Click += (sender, e) =>
            {
                if (ürünSepetListesi.Count == 0)
                {
                    MessageBox.Show("Sepetinizde ürün bulunmamaktadır.", "Sepetiniz Boş");
                }
                else
                {
                    DialogResult dialog;
                    dialog = MessageBox.Show("Sepetinizi onaylıyor musunuz?", "Sepet Onayı", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialog == DialogResult.OK)
                    {
                        sepet.Close();
                        SatınAl satınAl = new SatınAl();
                        satınAl.ürünListesi = ürünSepetListesi;
                        satınAl.KullaniciID = KullaniciID;
                        this.Hide();
                        satınAl.Show();
                    }
                }
            };

            sepet.Controls.Add(buttonSipariş);

            double sepetTutarı = 0;
            foreach (var items in ürünSepetListesi)
            {
                sepetTutarı += items.UrunFiyati;
            }
            sepetTutarı = Math.Round(sepetTutarı, 2);
            Label labelTutar = new Label();
            labelTutar.Text = sepetTutarı + " TL";
            labelTutar.TextAlign = ContentAlignment.MiddleRight;
            labelTutar.Location = new Point(430, 200);

            panelSepet.Controls.Add(labelTutar);

            System.Windows.Forms.Button sepetTemizle = new System.Windows.Forms.Button();
            sepetTemizle.Size = new Size(150, 40);
            sepetTemizle.Location = new Point(10, 190);
            sepetTemizle.Text = "Sepeti temizle";
            sepetTemizle.Click += (sender, e) =>
            {
                if (ürünSepetListesi.Count == 0)
                {
                    MessageBox.Show("Sepetinizde zaten ürün yok.", "Sepetiniz Boş");
                }
                else
                {
                    DialogResult dialogTemizle;
                    dialogTemizle = MessageBox.Show("Sepetteki her şeyi kaldırmak istediğinize emin misiniz", "Sepet Temizleme", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogTemizle == DialogResult.OK)
                    {
                        ykonumu = 10;
                        ürünSepetListesi.Clear();
                        panelSepet.Controls.Clear();
                        sepetPanelineVeriYazdır();
                    }
                }
            };
            panelSepet.Controls.Add(sepetTemizle);

            var ikiTane = ürünSepetListesi.Skip(sepetM * sepetN).Take(sepetN);
            foreach (var items in ikiTane)
            {
                ÜrünBilgisiVer(items, true);
            }
            sepet.FormClosing += (sender, e) =>
            {
                panelSepet.Controls.Clear();
                sepetM = 0;
                y_konumu = 10;
                ykonumu = 10;
                pictureBox1.Enabled = true;
                PaneleVeriYazdır();
            };
        }

        private void oncekiSayfaButon_Click(object sender, EventArgs e)  // Ürünleri gösterirken önceki 3 elemanı gösterir
        {
            m--;
            y_konumu = 10;
            PaneleVeriYazdır();
        }
        private void sonrakiSayfaButon_Click(object sender, EventArgs e)  // Ürünleri gösterirken sonraki 3 elemanı gösterir
        {
            m++;
            y_konumu = 10;
            PaneleVeriYazdır();
        }
        Form ÖzellikForm = null;
        int sayacform = 0;
        void rastgeleVeriGöster()  // Rastgele ürün seçer.
        {
            do
            {
                randomFoto = random.Next(ürünlerListesi.Count);
            } while (ürünlerListesi[randomFoto] == null || ürünSepetListesi.Contains(ürünlerListesi[randomFoto]));
            timer1.Start();

            rastgeleFoto.Image = ürünlerListesi[randomFoto].UrunFoto;
            rastgeleFoto.Click += (sender, e) =>
            {
                sayacform++;
                timer1.Stop();
                if (ÖzellikForm == null || ÖzellikForm.IsDisposed)
                {
                    if(sayacform == 1)
                        PictureVeriGöster(ürünlerListesi[randomFoto], true, false);
                }
            };

        }

        void ÜrünBilgisiVer(ÜrünlerDataBase items, bool deneme)  // Ürünün genel bilgisi için Marka, model, Fotoğraf, Fiyat, Stok
        {
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
                PictureVeriGöster(items, true, false);
            };

            Label fiyatLabel = new Label();
            fiyatLabel.Text = items.UrunFiyati + " TL";
            fiyatLabel.Location = new Point(400, ykonumu + 25);
            fiyatLabel.AutoSize = true;

            PictureBox pctrÇıkar = new PictureBox();
            pctrÇıkar.Image = Properties.Resources.Github_Octicons_X_16_128;
            pctrÇıkar.Size = new Size(25, 25);
            pctrÇıkar.Location = new Point(500, ykonumu + 20);
            pctrÇıkar.SizeMode = PictureBoxSizeMode.StretchImage;

            panelSepet.Controls.Add(fiyatLabel);
            panelSepet.Controls.Add(sepetLabel);
            panelSepet.Controls.Add(pctr);
            panelSepet.Controls.Add(pctrÇıkar);

            ykonumu += 100;

            pctrÇıkar.Click += (sender, e) =>
            {
                ykonumu = 10;
                ürünSepetListesi.Remove(items);
                panelSepet.Controls.Clear();
                sepetPanelineVeriYazdır();
            };
        }
        void PictureVeriGöster(ÜrünlerDataBase items, bool deneme, bool denemeYorum) // deneme = satın alabilir, denemeYorum = yorum yapabilir mi  // PictureBox' a tıklanırsa açılır ve o ürünün verisini gösterir
        {
            timer1.Stop();
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
            seçilenFiyat.Location = new Point(590, 380);
            seçilenFiyat.Text = $"{items.UrunFiyati} TL\n{items.UrunStok} tane bulunmakta.";
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


            System.Windows.Forms.Button butonAlınan = new System.Windows.Forms.Button();
            butonAlınan.Location = new Point(590, 325);
            butonAlınan.Size = new Size(130, 40);
            if (deneme == true)
            {
                if (!ürünSepetListesi.Contains(items))
                {
                    butonAlınan.Enabled = true;
                    butonAlınan.Text = "Sepete ekle";
                    butonAlınan.Click += (sender, e) =>
                    {
                        ürünSepetListesi.Add(items);
                        y_konumu = 10;
                        ÖzellikForm.Close();
                        PaneleVeriYazdır();
                    };
                    ÖzellikForm.Controls.Add(butonAlınan);
                }
                else if (ürünSepetListesi.Contains(items))
                {
                    butonAlınan.Enabled = true;
                    butonAlınan.Text = "Sepetten Çıkar";
                    butonAlınan.Click += (sender, e) =>
                    {
                        ürünSepetListesi.Remove(items);
                        y_konumu = 10;
                        ÖzellikForm.Close();
                        PaneleVeriYazdır();
                    };
                    ÖzellikForm.Controls.Add(butonAlınan);
                }
                Button yorumGörüntüle = new Button();
                yorumGörüntüle.Text = "Ürünün yorumlarını görüntüle";
                yorumGörüntüle.Size = new Size(130, 40);
                yorumGörüntüle.Location = new Point(450, 325);
                yorumGörüntüle.Click += (sender, e) =>
                {
                    timer1.Stop();
                    yorumGörüntüle.Enabled = false;
                    Form formYorum = new Form();
                    this.AddOwnedForm(formYorum);
                    formYorum.StartPosition = FormStartPosition.CenterScreen;
                    Bitmap bitmap = Properties.Resources.Picol_Picol_Comment_128;
                    Icon icon = Icon.FromHandle(bitmap.GetHicon());
                    formYorum.Icon = icon;
                    bitmap.Dispose();
                    icon.Dispose();
                    formYorum.Text = $"{items.UrunMarka} {items.UrunTuru} adlı ürünün yorumları";
                    formYorum.Size = new Size(900, 365);

                    Panel panelYorum = new Panel();
                    panelYorum.Location = new Point(10, 10);

                    int yKonum11 = 300;
                    int sayacY = 0;
                    foreach (var itm in YorumlarListesi)
                    {
                        if (itm.ürün == items.UrunID)
                        {
                            sayacY++;
                            if (sayacY >= 4)
                                yKonum11 += 50;
                        }
                    }
                    panelYorum.Size = new Size(850, yKonum11);
                    panelYorum.BorderStyle = BorderStyle.FixedSingle;
                    formYorum.FormBorderStyle = FormBorderStyle.FixedSingle;
                    formYorum.Show();
                    formYorum.AutoScroll = true;

                    formYorum.Controls.Add(panelYorum);

                    int yYorum = 25;
                    foreach (var yorumY in YorumlarListesi)
                    {
                        if (yorumY.ürün == items.UrunID)
                        {
                            Label labelYazar = new Label();
                            labelYazar.Text = yorumY.yorumYazan;
                            labelYazar.Location = new Point(5, yYorum + 10);
                            labelYazar.Size = new Size(150, 30);
                            panelYorum.Controls.Add(labelYazar);

                            int index = yorumY.yıldız;
                            int xYorum = 200;
                            for (int i = 1; i <= 5; i++)
                            {
                                PictureBox pctrYıldız = new PictureBox();
                                pctrYıldız.Size = new Size(25, 25);
                                pctrYıldız.Location = new Point(xYorum + 10, yYorum + 5);
                                pctrYıldız.SizeMode = PictureBoxSizeMode.StretchImage;
                                if (i <= index)
                                {
                                    pctrYıldız.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_full_128;
                                }
                                else
                                {
                                    pctrYıldız.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_empty_128;
                                }
                                xYorum += 30;
                                panelYorum.Controls.Add(pctrYıldız);
                            }
                            Label labelBilgi = new Label();
                            labelBilgi.Text = yorumY.yorumu;
                            labelBilgi.Location = new Point(xYorum + 25, yYorum - 5);
                            labelBilgi.TextAlign = ContentAlignment.MiddleCenter;
                            labelBilgi.AutoSize = false;
                            labelBilgi.Size = new Size(450, 50);
                            panelYorum.Controls.Add(labelBilgi);

                            yYorum += 100;
                        }
                    }
                    formYorum.FormClosing += (sender, e) =>
                    {
                        timer1.Start();
                        yorumGörüntüle.Enabled = true;
                    };
                };
                ÖzellikForm.Controls.Add(yorumGörüntüle);

                foreach (var yorum in YorumlarListesi)
                {
                    if (yorum.ürün == items.UrunID)
                    {
                        double yorumlar = 0;
                        double yorumsayisi = 0;
                        foreach (var itemYorum in YorumlarListesi)
                        {
                            if (itemYorum.ürün == items.UrunID)
                            {
                                yorumsayisi++;
                                yorumlar += itemYorum.yıldız;
                            }
                        }
                        double alinanYildiz = yorumlar / yorumsayisi;

                        int xYorum1 = 40;
                        int yYorum1 = 350;
                        for (int i = 1; i <= 5; i++)
                        {
                            PictureBox pctrYıldız = new PictureBox();
                            pctrYıldız.Size = new Size(25, 25);
                            pctrYıldız.Location = new Point(xYorum1, yYorum1);
                            pctrYıldız.SizeMode = PictureBoxSizeMode.StretchImage;
                            if (i <= alinanYildiz)
                            {
                                pctrYıldız.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_full_128;
                            }
                            else if (i == (int)alinanYildiz + 1 && alinanYildiz % 1 != 0)  // i == (int)alinanYildiz + 1  Bu satırı eklemezsem boş olması gereken her yıldız yarım yıldız oluyor.
                            {
                                pctrYıldız.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_half_full_128;
                            }
                            else
                            {
                                pctrYıldız.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_empty_128;
                            }
                            xYorum1 += 30;
                            ÖzellikForm.Controls.Add(pctrYıldız);
                        }
                    }
                }
            }
            else
            {

            }
            if (denemeYorum == true)
            {
                bool yorumVarMı = false;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(yorumKontrolString, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                        cmd.Parameters.AddWithValue("@UrunID", items.UrunID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int dbKullaniciID = (int)reader["kullaniciID"];
                                int dbUrunID = (int)reader["UrunID"];

                                if (dbKullaniciID == KullaniciID && dbUrunID == items.UrunID)
                                {
                                    yorumVarMı = true;
                                }
                            }
                        }
                    }
                }
                if (yorumVarMı == true)
                {
                    Button butonYorumSil = new Button();
                    butonYorumSil.Text = "Yorumunuzu silmek için tıklayın";
                    butonYorumSil.Location = new Point(170, 375);
                    butonYorumSil.Size = new Size(130, 40);
                    butonYorumSil.Click += (sender, e) =>
                    {
                        DialogResult dialog;
                        dialog = MessageBox.Show("Ürün yorumunuzu silmeyi gerçekten istiyor musunuz?", "Computer Design - Yorum Sil", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.OK)
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand(yorumSilString, connection))
                                {
                                    cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                                    cmd.Parameters.AddWithValue("@UrunID", items.UrunID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        MessageBox.Show("Yorumunuz silindi.");
                        ÖzellikForm.Close();
                    };
                    ÖzellikForm.Controls.Add(butonYorumSil);
                }
                int ürünYıldızı = 0;

                Button butonYorum = new Button();
                if (yorumVarMı == false)
                    butonYorum.Text = "Ürüne yorum yaz";
                else
                    butonYorum.Text = "Ürün yorumunuzu güncelleyin";
                butonYorum.Location = new Point(20, 375);
                butonYorum.Size = new Size(130, 40);
                butonYorum.Click += (sender, e) =>
                {
                    bool YıldızSecim = false;
                    butonYorum.Enabled = false;
                    Form formYorum = new Form();
                    formYorum.Text = "Computer Design - Ürüne Yorum Yaz";
                    Bitmap bitmap = Properties.Resources.Picol_Picol_Comment_128;
                    Icon icon = Icon.FromHandle(bitmap.GetHicon());
                    formYorum.StartPosition = FormStartPosition.CenterScreen;
                    formYorum.Icon = icon;
                    bitmap.Dispose();
                    icon.Dispose();
                    formYorum.Size = new Size(500, 300);
                    this.AddOwnedForm(formYorum);
                    formYorum.Show();

                    Label labelBilgi = new Label();
                    labelBilgi.Text = "Yorumunuzu yazınız";
                    labelBilgi.AutoSize = false;
                    labelBilgi.Size = new Size(200, 20);
                    labelBilgi.Location = new Point(10, 20);
                    formYorum.Controls.Add(labelBilgi);

                    TextBox textYorum = new TextBox();
                    textYorum.Location = new Point(10, 40);
                    textYorum.Multiline = true;
                    textYorum.Size = new Size(200, 210);
                    formYorum.Controls.Add(textYorum);

                    bool YorumTamam = false;

                    List<PictureBox> pictureBoxes = new List<PictureBox>();
                    int xYorum = 280;
                    for (int i = 1; i <= 5; i++)
                    {
                        PictureBox yıldızP1 = new PictureBox();
                        yıldızP1.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_empty_128;
                        yıldızP1.Size = new Size(25, 25);
                        yıldızP1.Location = new Point(xYorum, 100);
                        yıldızP1.SizeMode = PictureBoxSizeMode.StretchImage;
                        yıldızP1.Tag = i;

                        yıldızP1.MouseEnter += (sender, e) =>
                        {
                            if (YorumTamam == false)
                            {
                                int index = (int)((PictureBox)sender).Tag;
                                for (int j = 0; j < index; j++)
                                {
                                    pictureBoxes[j].Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_full_128;
                                }
                            }
                        };
                        yıldızP1.MouseLeave += (sender, e) =>
                        {
                            if (YorumTamam == false)
                            {
                                foreach (var pb in pictureBoxes)
                                {
                                    pb.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_empty_128;
                                }
                            }
                        };
                        yıldızP1.MouseClick += (sender, e) =>
                        {
                            foreach (var pb in pictureBoxes)
                            {
                                pb.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_empty_128;
                            }
                            int index = (int)((PictureBox)sender).Tag;
                            for (int j = 0; j < index; j++)
                            {
                                pictureBoxes[j].Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_full_128;
                            }
                            ürünYıldızı = index;
                            YorumTamam = true;
                            YıldızSecim = true;
                        };
                        formYorum.Controls.Add(yıldızP1);
                        xYorum += 30;
                        pictureBoxes.Add(yıldızP1);
                    }

                    Button butonOnayla = new Button();
                    butonOnayla.Text = "Yorumu onayla";
                    butonOnayla.Location = new Point(290, 210);
                    butonOnayla.Size = new Size(130, 40);
                    butonOnayla.Click += (sender, e) =>
                    {
                        if (string.IsNullOrWhiteSpace(textYorum.Text))
                        {
                            MessageBox.Show("Lütfen ürün hakkındaki yorumunuzu yazınız.");
                        }
                        else if (ürünYıldızı == 0)
                        {
                            MessageBox.Show("Ürünün yıldız seçiminizi yapınız.");
                        }
                        else if (YıldızSecim == false)
                        {
                            MessageBox.Show("Ürünün yıldız seçiminizi yapınız.");
                        }
                        else
                        {
                            MessageBox.Show("Yorumunuz kaydedildi.");
                            if (yorumVarMı == false)
                            {
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();
                                    using (SqlCommand cmd = new SqlCommand(yorumString, connection))
                                    {
                                        cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                                        cmd.Parameters.AddWithValue("@UrunID", items.UrunID);
                                        cmd.Parameters.AddWithValue("@UrunYorum", textYorum.Text.TrimEnd());
                                        cmd.Parameters.AddWithValue("@UrunYildizi", ürünYıldızı);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();
                                    using (SqlCommand cmd = new SqlCommand(yorumGüncelleString, connection))
                                    {
                                        cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                                        cmd.Parameters.AddWithValue("@UrunID", items.UrunID);
                                        cmd.Parameters.AddWithValue("@UrunYorum", textYorum.Text.TrimEnd());
                                        cmd.Parameters.AddWithValue("@UrunYildizi", ürünYıldızı);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            textYorum.Clear();
                            foreach (var pb in pictureBoxes)
                            {
                                pb.Image = Properties.Resources.Custom_Icon_Design_Flatastic_2_Star_empty_128;
                            }
                            ürünYıldızı = 0;
                        }
                        ÖzellikForm.Close();
                        formYorum.Close();
                        PictureVeriGöster(items, false, true);
                    };

                    formYorum.Controls.Add(butonOnayla);

                    formYorum.FormClosing += (sender, e) =>
                    {
                        butonYorum.Enabled = true;
                    };
                };
                ÖzellikForm.Controls.Add(butonYorum);
            }
            else
            {
            }

            ÖzellikForm.FormClosing += (sender, e) =>
            {
                sayacform = 0;
                timer1.Start();
            };
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
        }

        private void timer1_Tick(object sender, EventArgs e)  // Rastgele veri göstermek için
        {
            rastgeleVeriGöster();
        }

        private void adaGoreSırala_SelectedIndexChanged(object sender, EventArgs e)  // Kategoriye göre sıralar
        {
            if (adaGoreSırala.SelectedIndex == 0)
            {
                ürünSıralama.Sort((x, y) => x.UrunTuru.CompareTo(y.UrunTuru));
                y_konumu = 10;
                PaneleVeriYazdır();
            }
            else if (adaGoreSırala.SelectedIndex == 1)
            {
                ürünSıralama.Sort((x, y) => y.UrunTuru.CompareTo(x.UrunTuru));
                y_konumu = 10;
                PaneleVeriYazdır();
            }
        }

        private void fiyataGoreSirala_SelectedIndexChanged(object sender, EventArgs e)  // Fiyata göre sıralamak için
        {
            if (fiyataGoreSirala.SelectedIndex == 0)
            {
                ürünSıralama.Sort((x, y) => x.UrunFiyati.CompareTo(y.UrunFiyati));
                y_konumu = 10;
                PaneleVeriYazdır();
            }
            else if (fiyataGoreSirala.SelectedIndex == 1)
            {
                ürünSıralama.Sort((x, y) => y.UrunFiyati.CompareTo(x.UrunFiyati));
                y_konumu = 10;
                PaneleVeriYazdır();
            }
        }

        private void adaGoreSirala_SelectedIndexChanged(object sender, EventArgs e)  // Markaya göre sıralama için 
        {
            if (adaGoreSirala.SelectedIndex == 0)
            {
                ürünSıralama.Sort((x, y) => x.UrunMarka.CompareTo(y.UrunMarka));
                y_konumu = 10;
                PaneleVeriYazdır();
            }
            else if (adaGoreSirala.SelectedIndex == 1)
            {
                ürünSıralama.Sort((x, y) => y.UrunMarka.CompareTo(x.UrunMarka));
                y_konumu = 10;
                PaneleVeriYazdır();
            }
        }

        private void kategoriSecCombo_SelectedIndexChanged(object sender, EventArgs e)  // Kategori seçimi
        {
            ürünSıralama.Clear();

            if (kategoriSecCombo.Text == "Hepsi")
            {
                foreach (var items in ürünlerListesi)
                {
                    m = 0;
                    ürünSıralama.Add(items);
                    PaneleVeriYazdır();
                    y_konumu = 10;
                }
            }
            else
            {
                foreach (var items in ürünlerListesi)
                {
                    if (kategoriSecCombo.Text == items.UrunTuru)
                    {
                        m = 0;
                        ürünSıralama.Add(items);
                        y_konumu = 10;
                        PaneleVeriYazdır();
                    }
                }
            }
        }
        private void kendinTopla_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = true;
            ürünSıralama.Clear();
            if (kendinTopla.Text == "Ana sayfa")
            {
                index = 0;
                foreach (var items in ürünlerListesi)
                {
                    ürünSıralama.Add(items);
                }
                m = 0;
                y_konumu = 10;
                kendinTopla.Text = "Kendin topla";
                label6.Visible = true;
                kategoriSecCombo.Visible = true;
                panelListe.Size = new Size(357, 200);
                PaneleVeriYazdır();
            }
            else if (kendinTopla.Text == "Kendin topla")
            {
                index = 0;
                foreach (var items in ürünlerListesi)
                {
                    if (items.UrunTuru == KategoriList[index])
                    {
                        ürünSıralama.Add(items);
                    }
                }
                m = 0;
                y_konumu = 10;
                kendinTopla.Text = "Ana sayfa";
                label6.Visible = false;
                kategoriSecCombo.Visible = false;
                panelListe.Size = new Size(357, 150);
                PaneleVeriYazdır();
            }
        }
        /*
        private void pictureBox2_MouseEnter(object sender, EventArgs e)  // Üzerine fare gelince kategori paneli görükür.
        {
            if (panelListe.Location == new Point(-350, 408))
                panelListe.Location = new Point(12, 408);
            else
                panelListe.Location = new Point(-350, 408);
        }
        */
        private void kategoriButon_Click(object sender, EventArgs e)  // Bu butona tıklayınca kategori butonu açılır.
        {

            if (panelListe.Location == new Point(-400, 308))
                panelListe.Location = new Point(45, 287);

            else
                panelListe.Location = new Point(-400, 308);
        }
        private void butonAtla_Click(object sender, EventArgs e)  // Kendin topla işlemi yaparken elinizde ürün varsa o ürünü geçebilirsiniz. Belirli kriterlerde geçerli.
        {
            index++;
            ürünSıralama.Clear();
            if (index < KategoriList.Count)
            {
                foreach (var items in ürünlerListesi)
                {
                    if (items.UrunTuru == KategoriList[index])
                    {
                        ürünSıralama.Add(items);
                    }
                }
                y_konumu = 10;
                m = 0;
                PaneleVeriYazdır();
            }
            else
            {
                PaneleVeriYazdır();
                butonAtla.Visible = false;
            }
        }
        private void aramaPicture_Click(object sender, EventArgs e)  // Enter tuşuna basmak yerine buraya da basılabilir.
        {
            arananÜrün();
        }

        void arananÜrün()
        {
            ürünSıralama.Clear();
            string arananKısım = arananurunText.Text.ToLower().TrimEnd();
            foreach (var items in ürünlerListesi)
            {
                if (items.UrunTuru.ToLower().Contains(arananKısım) || items.UrunMarka.ToLower().Contains(arananKısım) || items.UrunModel.ToLower().Contains(arananKısım) || items.UrunOzellik1.ToLower().Contains(arananKısım) || items.UrunOzellik2.ToLower().Contains(arananKısım) || items.UrunOzellik3.ToLower().Contains(arananKısım) || items.UrunDigerOzellikler.ToLower().Contains(arananKısım))
                {
                    ürünSıralama.Add(items);
                }

                y_konumu = 10;
                m = 0;
                PaneleVeriYazdır();
            }
        }
        private void arananurunText_KeyPress(object sender, KeyPressEventArgs e)  // Eğer metin ararken enter tuşuna basarsa arama yapar.
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                arananÜrün();
            }
        }
        private void aramaTemizle_Click(object sender, EventArgs e)  // Çarpıya basınca aramak için girilen metin temizlenir.
        {
            arananurunText.Clear();
            ürünSıralama.Clear();
            foreach (var items in ürünlerListesi)
            {
                ürünSıralama.Add(items);
            }
            y_konumu = 10;
            m = 0;
            PaneleVeriYazdır();
        }

        private void gecmisSiparisler_Click(object sender, EventArgs e)  // Kullanıcının daha önceden yaptığı siparişler görüntülenir.
        {
            geçmişForm = new Form();
            geçmişPanel = new Panel();
            gecmisSiparislerLabel.Enabled = false;
            this.AddOwnedForm(geçmişForm);
            Bitmap bitmap = Properties.Resources.Github_Octicons_History_24;
            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            geçmişForm.Icon = icon;
            bitmap.Dispose();
            icon.Dispose();
            geçmişForm.Text = "Computer Design - Geçmiş Siparişleriniz";
            geçmişForm.Size = new Size(600, 400);
            geçmişForm.StartPosition = FormStartPosition.Manual;
            geçmişForm.Location = new Point(1010, 385);
            geçmişForm.BackColor = Color.White;
            geçmişForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            geçmişForm.Show();
            geçmişForm.MaximizeBox = false;
            geçmişPanel.Size = new Size(560, 340);
            geçmişPanel.AutoScroll = true;
            geçmişForm.Controls.Add(geçmişPanel);
            geçmişPanel.Location = new Point(10, 10);
            siparişGeçmişiniYükle();

            geçmişForm.FormClosing += (sender, e) =>
            {
                y_konumu = 10;
                m = 0;
                PaneleVeriYazdır();
            };
        }

        private void label8_Click(object sender, EventArgs e) // Ana menüye gönderir.
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Hesabınızdan çıkış yapmak istediğinize emin misiniz", "Computer Design - Hesaptan Çıkış", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
            }
        }
    }
}
namespace Computer_Design
{
    partial class MagazaSayfasi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagazaSayfasi));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            kullaniciadilabel = new Label();
            oncekiSayfaButon = new Button();
            sonrakiSayfaButon = new Button();
            panelDeneme = new Panel();
            panelListe = new Panel();
            label3 = new Label();
            adaGoreSırala = new ComboBox();
            label4 = new Label();
            adaGoreSirala = new ComboBox();
            fiyataGoreSirala = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            kategoriSecCombo = new ComboBox();
            rastgeleFoto = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            kendinTopla = new Button();
            label2 = new Label();
            kategoriButon = new Button();
            butonAtla = new Button();
            arananurunText = new TextBox();
            label7 = new Label();
            aramaPicture = new PictureBox();
            aramaTemizle = new PictureBox();
            gecmisSiparislerLabel = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelListe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rastgeleFoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)aramaPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)aramaTemizle).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Window;
            label1.Location = new Point(1243, 65);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 1;
            label1.Text = "Sepet";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.Window;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1238, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(47, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // kullaniciadilabel
            // 
            kullaniciadilabel.BackColor = SystemColors.Window;
            kullaniciadilabel.Location = new Point(1122, 102);
            kullaniciadilabel.Name = "kullaniciadilabel";
            kullaniciadilabel.RightToLeft = RightToLeft.Yes;
            kullaniciadilabel.Size = new Size(157, 20);
            kullaniciadilabel.TabIndex = 3;
            kullaniciadilabel.Text = "Kullanıcı Adı";
            // 
            // oncekiSayfaButon
            // 
            oncekiSayfaButon.BackColor = SystemColors.Window;
            oncekiSayfaButon.Font = new Font("Segoe UI", 8.25F);
            oncekiSayfaButon.Location = new Point(12, 613);
            oncekiSayfaButon.Name = "oncekiSayfaButon";
            oncekiSayfaButon.Size = new Size(72, 38);
            oncekiSayfaButon.TabIndex = 4;
            oncekiSayfaButon.Text = "Önceki Sayfa";
            oncekiSayfaButon.UseVisualStyleBackColor = false;
            oncekiSayfaButon.Click += oncekiSayfaButon_Click;
            // 
            // sonrakiSayfaButon
            // 
            sonrakiSayfaButon.BackColor = SystemColors.Window;
            sonrakiSayfaButon.Font = new Font("Segoe UI", 8.25F);
            sonrakiSayfaButon.Location = new Point(90, 613);
            sonrakiSayfaButon.Name = "sonrakiSayfaButon";
            sonrakiSayfaButon.Size = new Size(72, 38);
            sonrakiSayfaButon.TabIndex = 5;
            sonrakiSayfaButon.Text = "Sonraki Sayfa";
            sonrakiSayfaButon.UseVisualStyleBackColor = false;
            sonrakiSayfaButon.Click += sonrakiSayfaButon_Click;
            // 
            // panelDeneme
            // 
            panelDeneme.BackColor = SystemColors.Window;
            panelDeneme.BorderStyle = BorderStyle.FixedSingle;
            panelDeneme.Location = new Point(12, 316);
            panelDeneme.Name = "panelDeneme";
            panelDeneme.Size = new Size(1273, 291);
            panelDeneme.TabIndex = 7;
            // 
            // panelListe
            // 
            panelListe.BackColor = SystemColors.Window;
            panelListe.BorderStyle = BorderStyle.FixedSingle;
            panelListe.Controls.Add(label3);
            panelListe.Controls.Add(adaGoreSırala);
            panelListe.Controls.Add(label4);
            panelListe.Controls.Add(adaGoreSirala);
            panelListe.Controls.Add(fiyataGoreSirala);
            panelListe.Controls.Add(label5);
            panelListe.Controls.Add(label6);
            panelListe.Controls.Add(kategoriSecCombo);
            panelListe.Location = new Point(45, 287);
            panelListe.Name = "panelListe";
            panelListe.Size = new Size(357, 202);
            panelListe.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 20);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 15;
            label3.Text = "Kategoriye göre sırala";
            // 
            // adaGoreSırala
            // 
            adaGoreSırala.FormattingEnabled = true;
            adaGoreSırala.Items.AddRange(new object[] { "Artan sırada", "Azalan sırada" });
            adaGoreSırala.Location = new Point(200, 18);
            adaGoreSırala.Name = "adaGoreSırala";
            adaGoreSırala.Size = new Size(144, 23);
            adaGoreSırala.TabIndex = 11;
            adaGoreSırala.Text = "Kategoriye göre sırala";
            adaGoreSırala.SelectedIndexChanged += adaGoreSırala_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 70);
            label4.Name = "label4";
            label4.Size = new Size(144, 15);
            label4.TabIndex = 0;
            label4.Text = "Alfabetik sıraya göre sırala";
            // 
            // adaGoreSirala
            // 
            adaGoreSirala.FormattingEnabled = true;
            adaGoreSirala.Items.AddRange(new object[] { "Artan sırada", "Azalan sırada" });
            adaGoreSirala.Location = new Point(200, 68);
            adaGoreSirala.Name = "adaGoreSirala";
            adaGoreSirala.Size = new Size(144, 23);
            adaGoreSirala.TabIndex = 13;
            adaGoreSirala.Text = "Adına göre sırala";
            adaGoreSirala.SelectedIndexChanged += adaGoreSirala_SelectedIndexChanged;
            // 
            // fiyataGoreSirala
            // 
            fiyataGoreSirala.FormattingEnabled = true;
            fiyataGoreSirala.Items.AddRange(new object[] { "Artan sırada", "Azalan sırada" });
            fiyataGoreSirala.Location = new Point(200, 118);
            fiyataGoreSirala.Name = "fiyataGoreSirala";
            fiyataGoreSirala.Size = new Size(144, 23);
            fiyataGoreSirala.TabIndex = 12;
            fiyataGoreSirala.Text = "Fiyata göre sırala";
            fiyataGoreSirala.SelectedIndexChanged += fiyataGoreSirala_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 120);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 16;
            label5.Text = "Fiyata göre sırala";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 170);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 17;
            label6.Text = "Kategorize et";
            // 
            // kategoriSecCombo
            // 
            kategoriSecCombo.FormattingEnabled = true;
            kategoriSecCombo.Location = new Point(200, 168);
            kategoriSecCombo.Name = "kategoriSecCombo";
            kategoriSecCombo.Size = new Size(144, 23);
            kategoriSecCombo.TabIndex = 14;
            kategoriSecCombo.Text = "Kategori seçiniz";
            kategoriSecCombo.SelectedIndexChanged += kategoriSecCombo_SelectedIndexChanged;
            // 
            // rastgeleFoto
            // 
            rastgeleFoto.BackColor = SystemColors.Window;
            rastgeleFoto.Location = new Point(12, 12);
            rastgeleFoto.Name = "rastgeleFoto";
            rastgeleFoto.Size = new Size(250, 250);
            rastgeleFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            rastgeleFoto.TabIndex = 8;
            rastgeleFoto.TabStop = false;
            // 
            // timer1
            // 
            timer1.Interval = 7500;
            timer1.Tick += timer1_Tick;
            // 
            // kendinTopla
            // 
            kendinTopla.BackColor = SystemColors.Window;
            kendinTopla.Location = new Point(1176, 613);
            kendinTopla.Name = "kendinTopla";
            kendinTopla.Size = new Size(109, 38);
            kendinTopla.TabIndex = 9;
            kendinTopla.Text = "Kendin topla";
            kendinTopla.UseVisualStyleBackColor = false;
            kendinTopla.Click += kendinTopla_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 265);
            label2.Name = "label2";
            label2.Size = new Size(282, 15);
            label2.TabIndex = 10;
            label2.Text = "İlginizi çeken ürün olursa fotoğrafa tıklamanız yeterli";
            // 
            // kategoriButon
            // 
            kategoriButon.BackColor = SystemColors.Window;
            kategoriButon.BackgroundImage = Properties.Resources.Arturo_Wibawa_Akar_Three_line_horizontal_128;
            kategoriButon.BackgroundImageLayout = ImageLayout.Stretch;
            kategoriButon.Location = new Point(12, 287);
            kategoriButon.Name = "kategoriButon";
            kategoriButon.Size = new Size(27, 27);
            kategoriButon.TabIndex = 20;
            kategoriButon.UseVisualStyleBackColor = false;
            kategoriButon.Click += kategoriButon_Click;
            // 
            // butonAtla
            // 
            butonAtla.BackColor = SystemColors.Window;
            butonAtla.Location = new Point(1061, 613);
            butonAtla.Name = "butonAtla";
            butonAtla.Size = new Size(109, 38);
            butonAtla.TabIndex = 21;
            butonAtla.Text = "Atla";
            butonAtla.UseVisualStyleBackColor = false;
            butonAtla.Visible = false;
            butonAtla.Click += butonAtla_Click;
            // 
            // arananurunText
            // 
            arananurunText.Location = new Point(529, 291);
            arananurunText.Name = "arananurunText";
            arananurunText.Size = new Size(225, 23);
            arananurunText.TabIndex = 22;
            arananurunText.KeyPress += arananurunText_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(498, 294);
            label7.Name = "label7";
            label7.Size = new Size(25, 15);
            label7.TabIndex = 23;
            label7.Text = "Ara";
            // 
            // aramaPicture
            // 
            aramaPicture.Image = Properties.Resources.Gakuseisean_Ivista_2_Start_Menu_Search_128;
            aramaPicture.Location = new Point(760, 291);
            aramaPicture.Name = "aramaPicture";
            aramaPicture.Size = new Size(22, 23);
            aramaPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            aramaPicture.TabIndex = 24;
            aramaPicture.TabStop = false;
            aramaPicture.Click += aramaPicture_Click;
            // 
            // aramaTemizle
            // 
            aramaTemizle.Image = Properties.Resources.Github_Octicons_X_16_128;
            aramaTemizle.Location = new Point(788, 291);
            aramaTemizle.Name = "aramaTemizle";
            aramaTemizle.Size = new Size(22, 23);
            aramaTemizle.SizeMode = PictureBoxSizeMode.StretchImage;
            aramaTemizle.TabIndex = 25;
            aramaTemizle.TabStop = false;
            aramaTemizle.Click += aramaTemizle_Click;
            // 
            // gecmisSiparislerLabel
            // 
            gecmisSiparislerLabel.AutoSize = true;
            gecmisSiparislerLabel.Location = new Point(1098, 145);
            gecmisSiparislerLabel.Name = "gecmisSiparislerLabel";
            gecmisSiparislerLabel.Size = new Size(187, 15);
            gecmisSiparislerLabel.TabIndex = 26;
            gecmisSiparislerLabel.Text = "Geçmiş siparişleriniz için tıklayınız!";
            gecmisSiparislerLabel.Click += gecmisSiparisler_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1226, 293);
            label8.Name = "label8";
            label8.Size = new Size(59, 15);
            label8.TabIndex = 27;
            label8.Text = "Çıkış yap?";
            label8.Click += label8_Click;
            // 
            // MagazaSayfasi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.Window;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1297, 663);
            Controls.Add(label8);
            Controls.Add(gecmisSiparislerLabel);
            Controls.Add(aramaTemizle);
            Controls.Add(aramaPicture);
            Controls.Add(label7);
            Controls.Add(arananurunText);
            Controls.Add(butonAtla);
            Controls.Add(kategoriButon);
            Controls.Add(panelListe);
            Controls.Add(label2);
            Controls.Add(kendinTopla);
            Controls.Add(rastgeleFoto);
            Controls.Add(panelDeneme);
            Controls.Add(sonrakiSayfaButon);
            Controls.Add(oncekiSayfaButon);
            Controls.Add(kullaniciadilabel);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MagazaSayfasi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Computer Design - Mağaza Sayfası";
            FormClosing += MagazaSayfasi_FormClosing;
            Load += MagazaSayfasi_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelListe.ResumeLayout(false);
            panelListe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rastgeleFoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)aramaPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)aramaTemizle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private PictureBox pictureBox1;
        private Label kullaniciadilabel;
        private Button oncekiSayfaButon;
        private Button sonrakiSayfaButon;
        private Panel panelDeneme;
        private PictureBox rastgeleFoto;
        private System.Windows.Forms.Timer timer1;
        private Button kendinTopla;
        private Label label2;
        private ComboBox adaGoreSırala;
        private ComboBox fiyataGoreSirala;
        private ComboBox adaGoreSirala;
        private ComboBox kategoriSecCombo;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panelListe;
        private Button kategoriButon;
        private Button butonAtla;
        private TextBox arananurunText;
        private Label label7;
        private PictureBox aramaPicture;
        private PictureBox aramaTemizle;
        private Label gecmisSiparislerLabel;
        private Label label8;
    }
}
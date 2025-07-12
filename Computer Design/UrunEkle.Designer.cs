namespace Computer_Design
{
    partial class UrunEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrunEkle));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            labelozelik1 = new Label();
            labelozellik2 = new Label();
            labelozellik3 = new Label();
            eklemetamamla = new Button();
            UrunMarkaText = new TextBox();
            UrunModelText = new TextBox();
            UrunOzellik1Text = new TextBox();
            UrunOzellik2Text = new TextBox();
            UrunOzellik3Text = new TextBox();
            label7 = new Label();
            fotoyukle = new Button();
            UrunFoto = new PictureBox();
            label8 = new Label();
            UrunFiyatiText = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            label9 = new Label();
            UrunStokText = new TextBox();
            label10 = new Label();
            UrunDigerOzellıklerText = new TextBox();
            label11 = new Label();
            admintext = new Label();
            label12 = new Label();
            UrunLinkText = new TextBox();
            kontrolToolTip = new ToolTip(components);
            comboBox1 = new ComboBox();
            temizlebuton = new Button();
            urunsil = new Button();
            urunguncelle = new Button();
            dataGridView1 = new DataGridView();
            urunFotoGuncelle = new Button();
            ((System.ComponentModel.ISupportInitialize)UrunFoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(234, 228, 221);
            label1.Location = new Point(120, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Ürün türü";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(234, 228, 221);
            label2.Location = new Point(138, 39);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 1;
            label2.Text = "Marka";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(234, 228, 221);
            label3.Location = new Point(137, 69);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 2;
            label3.Text = "Model";
            // 
            // labelozelik1
            // 
            labelozelik1.BackColor = Color.FromArgb(234, 228, 221);
            labelozelik1.Location = new Point(10, 101);
            labelozelik1.Name = "labelozelik1";
            labelozelik1.RightToLeft = RightToLeft.Yes;
            labelozelik1.Size = new Size(168, 18);
            labelozelik1.TabIndex = 3;
            // 
            // labelozellik2
            // 
            labelozellik2.BackColor = Color.FromArgb(234, 228, 221);
            labelozellik2.Location = new Point(10, 132);
            labelozellik2.Name = "labelozellik2";
            labelozellik2.RightToLeft = RightToLeft.Yes;
            labelozellik2.Size = new Size(168, 15);
            labelozellik2.TabIndex = 4;
            // 
            // labelozellik3
            // 
            labelozellik3.BackColor = Color.FromArgb(234, 228, 221);
            labelozellik3.Location = new Point(10, 162);
            labelozellik3.Name = "labelozellik3";
            labelozellik3.RightToLeft = RightToLeft.Yes;
            labelozellik3.Size = new Size(168, 18);
            labelozellik3.TabIndex = 5;
            // 
            // eklemetamamla
            // 
            eklemetamamla.Location = new Point(759, 80);
            eklemetamamla.Name = "eklemetamamla";
            eklemetamamla.Size = new Size(164, 46);
            eklemetamamla.TabIndex = 12;
            eklemetamamla.Text = "Eklemeyi tamamla";
            eklemetamamla.UseVisualStyleBackColor = true;
            eklemetamamla.Click += eklemetamamla_Click;
            // 
            // UrunMarkaText
            // 
            UrunMarkaText.Location = new Point(185, 36);
            UrunMarkaText.MaxLength = 50;
            UrunMarkaText.Name = "UrunMarkaText";
            UrunMarkaText.Size = new Size(146, 23);
            UrunMarkaText.TabIndex = 2;
            UrunMarkaText.Leave += UrunMarkaText_Leave;
            // 
            // UrunModelText
            // 
            UrunModelText.Location = new Point(185, 66);
            UrunModelText.MaxLength = 50;
            UrunModelText.Name = "UrunModelText";
            UrunModelText.Size = new Size(146, 23);
            UrunModelText.TabIndex = 3;
            UrunModelText.Leave += UrunModelText_Leave;
            // 
            // UrunOzellik1Text
            // 
            UrunOzellik1Text.Location = new Point(185, 96);
            UrunOzellik1Text.MaxLength = 50;
            UrunOzellik1Text.Name = "UrunOzellik1Text";
            UrunOzellik1Text.Size = new Size(146, 23);
            UrunOzellik1Text.TabIndex = 4;
            UrunOzellik1Text.Enter += UrunOzellik1Text_Enter;
            UrunOzellik1Text.Leave += UrunOzellik1Text_Leave;
            // 
            // UrunOzellik2Text
            // 
            UrunOzellik2Text.Location = new Point(185, 126);
            UrunOzellik2Text.MaxLength = 50;
            UrunOzellik2Text.Name = "UrunOzellik2Text";
            UrunOzellik2Text.Size = new Size(146, 23);
            UrunOzellik2Text.TabIndex = 5;
            UrunOzellik2Text.Enter += UrunOzellik2Text_Enter;
            UrunOzellik2Text.Leave += UrunOzellik2Text_Leave;
            // 
            // UrunOzellik3Text
            // 
            UrunOzellik3Text.Location = new Point(185, 156);
            UrunOzellik3Text.MaxLength = 50;
            UrunOzellik3Text.Name = "UrunOzellik3Text";
            UrunOzellik3Text.Size = new Size(146, 23);
            UrunOzellik3Text.TabIndex = 6;
            UrunOzellik3Text.Enter += UrunOzellik3Text_Enter;
            UrunOzellik3Text.Leave += UrunOzellik3Text_Leave;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(234, 228, 221);
            label7.Location = new Point(82, 189);
            label7.Name = "label7";
            label7.Size = new Size(96, 15);
            label7.TabIndex = 13;
            label7.Text = "Ürünün fotoğrafı";
            // 
            // fotoyukle
            // 
            fotoyukle.Location = new Point(185, 185);
            fotoyukle.Name = "fotoyukle";
            fotoyukle.Size = new Size(148, 23);
            fotoyukle.TabIndex = 7;
            fotoyukle.Text = "Fotoğraf yükle";
            fotoyukle.UseVisualStyleBackColor = true;
            fotoyukle.Click += fotoyukle_Click;
            // 
            // UrunFoto
            // 
            UrunFoto.BackColor = Color.FromArgb(234, 228, 221);
            UrunFoto.Location = new Point(348, 6);
            UrunFoto.Name = "UrunFoto";
            UrunFoto.Size = new Size(233, 228);
            UrunFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            UrunFoto.TabIndex = 15;
            UrunFoto.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(234, 228, 221);
            label8.Location = new Point(116, 217);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 16;
            label8.Text = "Ürün fiyatı";
            // 
            // UrunFiyatiText
            // 
            UrunFiyatiText.Location = new Point(185, 214);
            UrunFiyatiText.MaxLength = 10;
            UrunFiyatiText.Name = "UrunFiyatiText";
            UrunFiyatiText.Size = new Size(146, 23);
            UrunFiyatiText.TabIndex = 8;
            UrunFiyatiText.Leave += UrunFiyatiText_Leave;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(234, 228, 221);
            label9.Location = new Point(117, 249);
            label9.Name = "label9";
            label9.Size = new Size(61, 15);
            label9.TabIndex = 18;
            label9.Text = "Stok sayısı";
            // 
            // UrunStokText
            // 
            UrunStokText.Location = new Point(185, 246);
            UrunStokText.MaxLength = 5;
            UrunStokText.Name = "UrunStokText";
            UrunStokText.Size = new Size(146, 23);
            UrunStokText.TabIndex = 9;
            UrunStokText.Leave += UrunStokText_Leave_1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(234, 228, 221);
            label10.Location = new Point(94, 279);
            label10.Name = "label10";
            label10.Size = new Size(84, 15);
            label10.TabIndex = 20;
            label10.Text = "Diğer özellikler";
            // 
            // UrunDigerOzellıklerText
            // 
            UrunDigerOzellıklerText.Location = new Point(185, 279);
            UrunDigerOzellıklerText.MaxLength = 250;
            UrunDigerOzellıklerText.Multiline = true;
            UrunDigerOzellıklerText.Name = "UrunDigerOzellıklerText";
            UrunDigerOzellıklerText.Size = new Size(397, 53);
            UrunDigerOzellıklerText.TabIndex = 10;
            UrunDigerOzellıklerText.Enter += UrunDigerOzellıklerText_Enter;
            UrunDigerOzellıklerText.Leave += UrunDigerOzellıklerText_Leave;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.FromArgb(234, 228, 221);
            label11.Location = new Point(768, 14);
            label11.Name = "label11";
            label11.Size = new Size(88, 15);
            label11.TabIndex = 22;
            label11.Text = "Admin bilgileri:";
            // 
            // admintext
            // 
            admintext.AutoSize = true;
            admintext.Location = new Point(768, 44);
            admintext.Name = "admintext";
            admintext.Size = new Size(0, 15);
            admintext.TabIndex = 24;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.FromArgb(234, 228, 221);
            label12.Location = new Point(348, 252);
            label12.Name = "label12";
            label12.Size = new Size(58, 15);
            label12.TabIndex = 25;
            label12.Text = "Ürün linki";
            // 
            // UrunLinkText
            // 
            UrunLinkText.Location = new Point(435, 246);
            UrunLinkText.MaxLength = 250;
            UrunLinkText.Name = "UrunLinkText";
            UrunLinkText.Size = new Size(146, 23);
            UrunLinkText.TabIndex = 11;
            UrunLinkText.Leave += UrunLinkText_Leave;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(185, 6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(146, 23);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "Seçiniz!";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // temizlebuton
            // 
            temizlebuton.Location = new Point(759, 238);
            temizlebuton.Name = "temizlebuton";
            temizlebuton.Size = new Size(164, 46);
            temizlebuton.TabIndex = 15;
            temizlebuton.Text = "Bilgileri temizle";
            temizlebuton.UseVisualStyleBackColor = true;
            temizlebuton.Click += temizlebuton_Click;
            // 
            // urunsil
            // 
            urunsil.Location = new Point(759, 132);
            urunsil.Name = "urunsil";
            urunsil.Size = new Size(164, 48);
            urunsil.TabIndex = 13;
            urunsil.Text = "Ürünü Sil";
            urunsil.UseVisualStyleBackColor = true;
            urunsil.Click += urunsil_Click;
            // 
            // urunguncelle
            // 
            urunguncelle.Location = new Point(759, 186);
            urunguncelle.Name = "urunguncelle";
            urunguncelle.Size = new Size(164, 46);
            urunguncelle.TabIndex = 14;
            urunguncelle.Text = "Ürün Güncelle";
            urunguncelle.UseVisualStyleBackColor = true;
            urunguncelle.Click += urunguncelle_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 338);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(920, 219);
            dataGridView1.TabIndex = 29;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // urunFotoGuncelle
            // 
            urunFotoGuncelle.Location = new Point(759, 290);
            urunFotoGuncelle.Name = "urunFotoGuncelle";
            urunFotoGuncelle.Size = new Size(164, 46);
            urunFotoGuncelle.TabIndex = 30;
            urunFotoGuncelle.Text = "Ürünün fotoğrafını güncelle";
            urunFotoGuncelle.UseVisualStyleBackColor = true;
            urunFotoGuncelle.Click += this.urunFotoGuncelle_Click;
            // 
            // UrunEkle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(234, 228, 221);
            BackgroundImage = Properties.Resources.tech__1_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 561);
            Controls.Add(urunFotoGuncelle);
            Controls.Add(dataGridView1);
            Controls.Add(urunguncelle);
            Controls.Add(urunsil);
            Controls.Add(temizlebuton);
            Controls.Add(UrunLinkText);
            Controls.Add(label12);
            Controls.Add(admintext);
            Controls.Add(label11);
            Controls.Add(UrunDigerOzellıklerText);
            Controls.Add(label10);
            Controls.Add(UrunStokText);
            Controls.Add(label9);
            Controls.Add(UrunFiyatiText);
            Controls.Add(label8);
            Controls.Add(UrunFoto);
            Controls.Add(fotoyukle);
            Controls.Add(label7);
            Controls.Add(UrunOzellik3Text);
            Controls.Add(UrunOzellik2Text);
            Controls.Add(UrunOzellik1Text);
            Controls.Add(UrunModelText);
            Controls.Add(UrunMarkaText);
            Controls.Add(eklemetamamla);
            Controls.Add(labelozellik3);
            Controls.Add(labelozellik2);
            Controls.Add(labelozelik1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "UrunEkle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Computer Design - Ürün Ekle";
            FormClosing += UrunEkle_FormClosing;
            Load += UrunEkle_Load;
            ((System.ComponentModel.ISupportInitialize)UrunFoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label labelozelik1;
        private Label labelozellik2;
        private Label labelozellik3;
        private Button eklemetamamla;
        private TextBox UrunMarkaText;
        private TextBox UrunModelText;
        private TextBox UrunOzellik1Text;
        private TextBox UrunOzellik2Text;
        private TextBox UrunOzellik3Text;
        private Label label7;
        private Button fotoyukle;
        private PictureBox UrunFoto;
        private Label label8;
        private TextBox UrunFiyatiText;
        private OpenFileDialog openFileDialog1;
        private Label label9;
        private TextBox UrunStokText;
        private Label label10;
        private TextBox UrunDigerOzellıklerText;
        private Label label11;
        private Label admintext;
        private Label label12;
        private TextBox UrunLinkText;
        private ToolTip kontrolToolTip;
        private ComboBox comboBox1;
        private Button temizlebuton;
        private Button urunsil;
        private Button urunguncelle;
        private DataGridView dataGridView1;
        private Button urunFotoGuncelle;  
    }
}
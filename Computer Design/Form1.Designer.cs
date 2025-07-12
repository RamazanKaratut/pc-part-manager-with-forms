namespace Computer_Design
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            adsoyadtext = new TextBox();
            sifretext = new TextBox();
            girisyapbuton = new Button();
            kayitollabel = new Label();
            sifremiunuttumlabel = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(234, 228, 221);
            label1.Location = new Point(166, 149);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 0;
            label1.Text = "Ad Soyad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(234, 228, 221);
            label2.Location = new Point(136, 214);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 1;
            label2.Text = "Kullanıcı Şifresi:";
            // 
            // adsoyadtext
            // 
            adsoyadtext.Location = new Point(258, 146);
            adsoyadtext.MaxLength = 100;
            adsoyadtext.Name = "adsoyadtext";
            adsoyadtext.Size = new Size(100, 23);
            adsoyadtext.TabIndex = 1;
            adsoyadtext.KeyDown += adsoyadtext_KeyDown;
            adsoyadtext.Leave += adsoyadtext_Leave;
            // 
            // sifretext
            // 
            sifretext.Location = new Point(258, 210);
            sifretext.MaxLength = 16;
            sifretext.Name = "sifretext";
            sifretext.PasswordChar = '*';
            sifretext.Size = new Size(100, 23);
            sifretext.TabIndex = 2;
            sifretext.KeyDown += sifretext_KeyDown;
            sifretext.Leave += sifretext_Leave;
            // 
            // girisyapbuton
            // 
            girisyapbuton.Location = new Point(510, 301);
            girisyapbuton.Name = "girisyapbuton";
            girisyapbuton.Size = new Size(212, 57);
            girisyapbuton.TabIndex = 3;
            girisyapbuton.Text = "Giriş yap";
            girisyapbuton.UseVisualStyleBackColor = true;
            girisyapbuton.Click += girisyapbuton_Click;
            // 
            // kayitollabel
            // 
            kayitollabel.AutoSize = true;
            kayitollabel.BackColor = Color.FromArgb(234, 228, 221);
            kayitollabel.Location = new Point(510, 361);
            kayitollabel.Name = "kayitollabel";
            kayitollabel.Size = new Size(48, 15);
            kayitollabel.TabIndex = 5;
            kayitollabel.Text = "Kayıt Ol";
            kayitollabel.Click += kayitollabel_Click;
            // 
            // sifremiunuttumlabel
            // 
            sifremiunuttumlabel.AutoSize = true;
            sifremiunuttumlabel.BackColor = Color.FromArgb(234, 228, 221);
            sifremiunuttumlabel.Location = new Point(622, 361);
            sifremiunuttumlabel.Name = "sifremiunuttumlabel";
            sifremiunuttumlabel.Size = new Size(100, 15);
            sifremiunuttumlabel.TabIndex = 6;
            sifremiunuttumlabel.Text = "Şifremi Unuttum?";
            sifremiunuttumlabel.Click += sifremiunuttumlabel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(234, 228, 221);
            pictureBox1.Image = Properties.Resources.Custom_Icon_Design_Mono_General_4_Eye_512;
            pictureBox1.Location = new Point(364, 210);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(234, 228, 221);
            BackgroundImage = Properties.Resources.tech__1_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(734, 411);
            Controls.Add(pictureBox1);
            Controls.Add(sifremiunuttumlabel);
            Controls.Add(kayitollabel);
            Controls.Add(girisyapbuton);
            Controls.Add(sifretext);
            Controls.Add(adsoyadtext);
            Controls.Add(label2);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Computer Design - Giriş";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox adsoyadtext;
        private TextBox sifretext;
        private Button girisyapbuton;
        private Label kayitollabel;
        private Label sifremiunuttumlabel;
        private PictureBox pictureBox1;
    }
}

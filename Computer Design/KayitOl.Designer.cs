namespace Computer_Design
{
    partial class KayitOl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KayitOl));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            adsoyadtext = new TextBox();
            sifretext = new TextBox();
            yenidensifretext = new TextBox();
            postatext = new TextBox();
            telefontext = new TextBox();
            pictureBox1 = new PictureBox();
            label6 = new Label();
            kayitolbuton = new Button();
            kontrolToolTip = new ToolTip(components);
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(234, 228, 221);
            label1.Location = new Point(170, 90);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 0;
            label1.Text = "Ad soyad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(234, 228, 221);
            label2.Location = new Point(196, 140);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 1;
            label2.Text = "Şifre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(234, 228, 221);
            label3.Location = new Point(151, 190);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 2;
            label3.Text = "Şifre Yeniden";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(234, 228, 221);
            label4.Location = new Point(176, 240);
            label4.Name = "label4";
            label4.Size = new Size(50, 15);
            label4.TabIndex = 3;
            label4.Text = "e- Posta";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(234, 228, 221);
            label5.Location = new Point(121, 290);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 4;
            label5.Text = "Telefon Numarası";
            // 
            // adsoyadtext
            // 
            adsoyadtext.Location = new Point(270, 87);
            adsoyadtext.MaxLength = 100;
            adsoyadtext.Name = "adsoyadtext";
            adsoyadtext.Size = new Size(138, 23);
            adsoyadtext.TabIndex = 1;
            adsoyadtext.Enter += adsoyadtext_Enter;
            adsoyadtext.Leave += adsoyadtext_Leave;
            // 
            // sifretext
            // 
            sifretext.Location = new Point(270, 137);
            sifretext.MaxLength = 16;
            sifretext.Name = "sifretext";
            sifretext.PasswordChar = '*';
            sifretext.Size = new Size(138, 23);
            sifretext.TabIndex = 2;
            sifretext.Enter += sifretext_Enter;
            sifretext.Leave += sifretext_Leave;
            // 
            // yenidensifretext
            // 
            yenidensifretext.Location = new Point(270, 187);
            yenidensifretext.MaxLength = 16;
            yenidensifretext.Name = "yenidensifretext";
            yenidensifretext.PasswordChar = '*';
            yenidensifretext.Size = new Size(138, 23);
            yenidensifretext.TabIndex = 3;
            yenidensifretext.Leave += yenidensifretext_Leave;
            // 
            // postatext
            // 
            postatext.Location = new Point(270, 237);
            postatext.MaxLength = 50;
            postatext.Name = "postatext";
            postatext.Size = new Size(138, 23);
            postatext.TabIndex = 4;
            postatext.Enter += postatext_Enter;
            postatext.Leave += postatext_Leave;
            // 
            // telefontext
            // 
            telefontext.Location = new Point(270, 287);
            telefontext.MaxLength = 11;
            telefontext.Name = "telefontext";
            telefontext.Size = new Size(138, 23);
            telefontext.TabIndex = 5;
            telefontext.Enter += telefontext_Enter;
            telefontext.KeyDown += telefontext_KeyDown;
            telefontext.Leave += telefontext_Leave;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(234, 228, 221);
            pictureBox1.Image = Properties.Resources.Custom_Icon_Design_Mono_General_4_Eye_512;
            pictureBox1.Location = new Point(414, 137);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(234, 228, 221);
            label6.Location = new Point(12, 9);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 11;
            label6.Text = "Geri Dön";
            label6.Click += label6_Click;
            // 
            // kayitolbuton
            // 
            kayitolbuton.Location = new Point(510, 301);
            kayitolbuton.Name = "kayitolbuton";
            kayitolbuton.Size = new Size(183, 59);
            kayitolbuton.TabIndex = 12;
            kayitolbuton.Text = "Kayıt Ol";
            kayitolbuton.UseVisualStyleBackColor = true;
            kayitolbuton.Click += kayitolbuton_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(234, 228, 221);
            pictureBox2.Image = Properties.Resources.Custom_Icon_Design_Mono_General_4_Eye_512;
            pictureBox2.Location = new Point(414, 185);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 25);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 13;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // KayitOl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.tech__1_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(734, 411);
            Controls.Add(pictureBox2);
            Controls.Add(kayitolbuton);
            Controls.Add(label6);
            Controls.Add(pictureBox1);
            Controls.Add(telefontext);
            Controls.Add(postatext);
            Controls.Add(yenidensifretext);
            Controls.Add(sifretext);
            Controls.Add(adsoyadtext);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "KayitOl";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Computer Design - Kayıt Ol";
            FormClosing += KayitOl_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox adsoyadtext;
        private TextBox sifretext;
        private TextBox yenidensifretext;
        private TextBox postatext;
        private TextBox telefontext;
        private PictureBox pictureBox1;
        private Label label6;
        private Button kayitolbuton;
        private ToolTip kontrolToolTip;
        private PictureBox pictureBox2;
    }
}
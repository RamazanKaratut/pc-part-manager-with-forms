namespace Computer_Design
{
    partial class SifreYenile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SifreYenile));
            sifreyenilebuton = new Button();
            label6 = new Label();
            telefontext = new TextBox();
            postatext = new TextBox();
            adsoyadtext = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label1 = new Label();
            kontrolToolTip = new ToolTip(components);
            SuspendLayout();
            // 
            // sifreyenilebuton
            // 
            sifreyenilebuton.Location = new Point(510, 301);
            sifreyenilebuton.Name = "sifreyenilebuton";
            sifreyenilebuton.Size = new Size(183, 59);
            sifreyenilebuton.TabIndex = 6;
            sifreyenilebuton.Text = "Mail Gönder";
            sifreyenilebuton.UseVisualStyleBackColor = true;
            sifreyenilebuton.Click += sifreyenilebuton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(234, 228, 221);
            label6.Location = new Point(12, 15);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 24;
            label6.Text = "Geri Dön";
            label6.Click += label6_Click;
            // 
            // telefontext
            // 
            telefontext.Location = new Point(301, 222);
            telefontext.MaxLength = 11;
            telefontext.Name = "telefontext";
            telefontext.Size = new Size(138, 23);
            telefontext.TabIndex = 3;
            telefontext.Leave += telefontext_Leave;
            // 
            // postatext
            // 
            postatext.Location = new Point(301, 172);
            postatext.MaxLength = 50;
            postatext.Name = "postatext";
            postatext.Size = new Size(138, 23);
            postatext.TabIndex = 2;
            postatext.Leave += postatext_Leave;
            // 
            // adsoyadtext
            // 
            adsoyadtext.Location = new Point(301, 125);
            adsoyadtext.MaxLength = 100;
            adsoyadtext.Name = "adsoyadtext";
            adsoyadtext.Size = new Size(138, 23);
            adsoyadtext.TabIndex = 1;
            adsoyadtext.Leave += adsoyadtext_Leave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(234, 228, 221);
            label5.Location = new Point(158, 225);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 21;
            label5.Text = "Telefon Numarası";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(234, 228, 221);
            label4.Location = new Point(207, 175);
            label4.Name = "label4";
            label4.Size = new Size(50, 15);
            label4.TabIndex = 19;
            label4.Text = "e- Posta";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(234, 228, 221);
            label1.Location = new Point(201, 125);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 13;
            label1.Text = "Ad soyad";
            // 
            // SifreYenile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.tech__1_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(734, 411);
            Controls.Add(sifreyenilebuton);
            Controls.Add(label6);
            Controls.Add(telefontext);
            Controls.Add(postatext);
            Controls.Add(adsoyadtext);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SifreYenile";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Computer Design - Şifre Yenile";
            FormClosing += SifreYenile_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button sifreyenilebuton;
        private Label label6;
        private TextBox telefontext;
        private TextBox postatext;
        private TextBox adsoyadtext;
        private Label label5;
        private Label label4;
        private Label label1;
        private ToolTip kontrolToolTip;
    }
}
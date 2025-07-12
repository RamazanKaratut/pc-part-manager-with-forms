using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Design
{
    public partial class SepetForm : Form
    {
        public SepetForm()
        {
            InitializeComponent();
        }

        private void SepetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void SepetForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

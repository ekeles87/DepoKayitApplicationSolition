using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepoKayitApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string connectionString = string.Empty;
        private void Baglanbtn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textboxkulAdı.Text) && !string.IsNullOrEmpty(textboxsifre.Text) )
            {
                connectionString = string.Format(@"Server=SUART-EKELES\SQLEXPRESS;Database=MASTER;User Id={0};Password = {1};", textboxkulAdı.Text, textboxsifre.Text);

            }
            else
            {
                connectionString = @"Server=SUART-EKELES\SQLEXPRESS;Database=MASTER;Integrated Security= True;";
            }
        }
    }
}

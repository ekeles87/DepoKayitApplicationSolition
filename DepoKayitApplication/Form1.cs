using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            try
            {
                this.cmbDataBase.Items.Clear();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select name from sys.databases";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                 while(reader.Read())
                {
                    string dbad = reader.GetString(0);
                    this.cmbDataBase.Items.Add(dbad);

                }
                reader.Close();
                

                conn.Close();
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void cmbDataBase_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string dbname = cmbtablo.Text;
            if (!string.IsNullOrEmpty(textboxkulAdı.Text) && !string.IsNullOrEmpty(textboxsifre.Text))
            {
                connectionString = string.Format(@"Server=SUART-EKELES\SQLEXPRESS;Database={0};User Id={1};Password = {2};", dbname, textboxkulAdı.Text, textboxsifre.Text);

            }
            else
            {
                connectionString = string.Format(@"Server=SUART-EKELES\SQLEXPRESS;Database={0};Integrated Security= True;", dbname);
            }

            try
            {
                this.cmbtablo.Items.Clear();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select name from sys.tables";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string tabload = reader.GetString(0);
                    this.cmbtablo.Items.Add(tabload);

                }
                reader.Close();


                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}

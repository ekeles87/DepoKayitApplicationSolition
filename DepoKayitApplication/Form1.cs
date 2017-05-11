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

        private void cmbDataBase_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string dbname = cmbDataBase.Text;
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

        private void cmbtablo_SelectedIndexChanged(object sender, EventArgs e)
        {

            string dbname = cmbDataBase.Text;
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
                this.chckdListBox.Items.Clear();
                string tablead = cmbtablo.Text;

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("SELECT COLUMN_NAME FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName", dbname);
                cmd.Parameters.AddWithValue("@TableName", tablead);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string columnname = reader.GetString(0);
                    this.chckdListBox.Items.Add(columnname);

                }
                reader.Close();


                conn.Close();

                cmd.CommandText = string.Format("select * from {0}",tablead);

                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                dgvTablo.DataSource = dt;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxkulAdı.Text) && !string.IsNullOrEmpty(textboxsifre.Text))
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
                while (reader.Read())
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

        private void secbtn_Click(object sender, EventArgs e)
        {
            string dbname = cmbDataBase.Text;
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
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("select * from {0}", cmbtablo.Text);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }

        private void chckdListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chckdListBox.CheckedItems.Count==0)
            {
                return;
            }
            string dbname = cmbDataBase.Text;
            if (!string.IsNullOrEmpty(textboxkulAdı.Text) && !string.IsNullOrEmpty(textboxsifre.Text))
            {
                connectionString = string.Format(@"Server=SUART-EKELES\SQLEXPRESS;Database={0};User Id={1};Password = {2};", dbname, textboxkulAdı.Text, textboxsifre.Text);

            }
            else
            {
                connectionString = string.Format(@"Server=SUART-EKELES\SQLEXPRESS;Database={0};Integrated Security= True;", dbname);
            }

            string tablename = cmbtablo.Text;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();

                string sorgu = string.Empty;
                string colonlar = string.Empty;

                foreach (object item in chckdListBox.CheckedItems)
                {
                    colonlar += string.Format("[{0}],", item.ToString());

                }
                colonlar = colonlar.TrimEnd(',');

                sorgu = string.Format("select {0} from {1}", colonlar, tablename);

                cmd.CommandText = sorgu;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}

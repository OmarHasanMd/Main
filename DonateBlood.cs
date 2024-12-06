using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Blood
{
    public partial class DonateBlood : Form
    {
        public DonateBlood()
        {
            InitializeComponent();
            populate();
            bloodStock();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MuOmarHasan\Documents\BloodBankDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        private void populate()
        {
            Con.Open();
            string Query = "select * from DonorTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DonorsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void bloodStock()
        {
            Con.Open();
            string Query = "select * from BloodTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BloodStockDVG.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void DonateBlood_Load(object sender, EventArgs e)
        {

        }
        int oldstock;
        private void GetStock(string BGroup)
        {
            Con.Open();
            string query = "select * from BloodTbl where Bgroup='" + BGroup + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt=new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                oldstock = Convert.ToInt32(dr["BStock"].ToString());
            }
            Con.Close();

        }

        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DNameTb.Text = DonorsDGV.SelectedRows[0].Cells[1].Value.ToString();
            BGroupTb.Text = DonorsDGV.SelectedRows[0].Cells[6].Value.ToString();
            GetStock(BGroupTb.Text);
        }
        private void reset()
        {
            DNameTb.Text = "";
            BGroupTb.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (DNameTb.Text == "")
            {
                MessageBox.Show("Select A donor");
            }
            else
            {
                try
                {
                    int stock = oldstock+1; // Increment blood stock by 1
                    string query = "update BloodTbl set BStock=" + stock + " where BGroup='" + BGroupTb.Text + "';";

                    Con.Open();
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donation Successful");
                    Con.Close();

                    reset(); // Clear input fields (assuming it's a function)
                    bloodStock(); // Refresh blood stock data (assuming it's a function)
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message); // Show error message if any exception occurs
                }
            }
        }
    }
}

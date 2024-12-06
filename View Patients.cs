//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Guna.UI2.WinForms;
//using Microsoft.Data.SqlClient;

//namespace Blood
//{
//    public partial class View_Patients : Form
//    {
//        public View_Patients()
//        {
//            InitializeComponent();
//            populate();
//        }
//        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MuOmarHasan\Documents\BloodBankDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");
//        private void populate()
//        {
//            Con.Open();
//            string Query = "select * from PatientTbl";
//            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
//            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
//            var ds = new DataSet();
//            sda.Fill(ds);
//            PatientsDVG.DataSource = ds.Tables[0];
//            Con.Close();
//        }

//        private void ViewDonor_Load(object sender, EventArgs e)
//        {
//        }


//        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
//        {

//        }

//        int key = 0;
//        private void DonorDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            PNameTb.Text = PatientsDVG.SelectedRows[0].Cells[1].Value.ToString();
//            PAgeTb.Text = PatientsDVG.SelectedRows[0].Cells[2].Value.ToString();
//            PphoneTb.Text = PatientsDVG.SelectedRows[0].Cells[3].Value.ToString();
//            PGenCb.SelectedItem = PatientsDVG.SelectedRows[0].Cells[4].Value.ToString();
//            PBGroupCb.SelectedItem = PatientsDVG.SelectedRows[0].Cells[5].Value.ToString();
//            PAddressTb.Text = PatientsDVG.SelectedRows[0].Cells[6].Value.ToString();
//            if (PNameTb.Text == "")
//            {
//                key = 0;
//            }
//            else
//            {
//                key = Convert.ToInt32(PatientsDVG.SelectedRows[0].Cells[0].Value.ToString());
//            }
//        }
//        private void Reset()
//        {
//            PNameTb.Text = "";
//            PAgeTb.Text = "";
//            PphoneTb.Text = "";
//            PGenCb.SelectedIndex = -1;
//            PBGroupCb.SelectedIndex = -1;
//            PAddressTb.Text = "";
//            key = 0;
//        }


//        private void guna2Button2_Click(object sender, EventArgs e)
//        { 
//            private void bunifuThinButton22_Click(object sender, EventArgs e)
//        {
//            if (key == 0)
//            {
//                MessageBox.Show("Select the Patient to Delete");
//            }
//            else
//            {
//                try
//                {
//                    string query = "Delete from PatientTbl where PNum=" + key + ";";
//                    Con.Open();
//                    SqlCommand cmd = new SqlCommand(query, Con);
//                    cmd.ExecuteNonQuery();
//                    MessageBox.Show("Patient Successfully Deleted");
//                    Con.Close();
//                    Reset();
//                    populate();
//                }
//                catch (Exception Ex)
//                {
//                    MessageBox.Show(Ex.Message);

//                }
//            }
//        }

//    }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;

namespace Blood
{
    public partial class View_Patients : Form
    {
        public View_Patients()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MuOmarHasan\Documents\BloodBankDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        private void populate()
        {
            Con.Open();
            string Query = "select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatientsDVG.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ViewDonor_Load(object sender, EventArgs e)
        {
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        int key = 0;

        private void DonorDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PNameTb.Text = PatientsDVG.SelectedRows[0].Cells[1].Value.ToString();
            PAgeTb.Text = PatientsDVG.SelectedRows[0].Cells[2].Value.ToString();
            PphoneTb.Text = PatientsDVG.SelectedRows[0].Cells[3].Value.ToString();
            PGenCb.SelectedItem = PatientsDVG.SelectedRows[0].Cells[4].Value.ToString();
            PBGroupCb.SelectedItem = PatientsDVG.SelectedRows[0].Cells[5].Value.ToString();
            PAddressTb.Text = PatientsDVG.SelectedRows[0].Cells[6].Value.ToString();
            if (PNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PatientsDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Reset()
        {
            PNameTb.Text = "";
            PAgeTb.Text = "";
            PphoneTb.Text = "";
            PGenCb.SelectedIndex = -1;
            PBGroupCb.SelectedIndex = -1;
            PAddressTb.Text = "";
            key = 0;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Patient to Delete");
            }
            else
            {
                try
                {
                    string query = "Delete from PatientTbl where PNum=" + key + ";";
                    Con.Open();
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Successfully Deleted");
                    Con.Close();
                    Reset();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Patient Pat = new Patient();
            Pat.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PNameTb?.Text) ||
                string.IsNullOrEmpty(PphoneTb?.Text) ||
                string.IsNullOrEmpty(PAgeTb?.Text) ||
                PGenCb?.SelectedIndex == -1 ||
                PBGroupCb?.SelectedIndex == -1 ||
                string.IsNullOrEmpty(PAddressTb?.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                // Validate if key is not null
                if (key == null)
                {
                    MessageBox.Show("Key value is missing.");
                    return;
                }

                // Corrected column name "PAddress"
                string query = "UPDATE PatientTbl SET " +
                               "Pname = @PName, " +
                               "Page = @PAge, " +
                               "Pphone = @PPhone, " +
                               "PGender = @PGender, " +
                               "PBGroup = @PBGroup, " +
                               "PAddress = @PAddress " +  // Corrected here
                               "WHERE PNum = @PNum;";

                Con.Open();
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    // Use parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@PName", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PAge", int.TryParse(PAgeTb.Text, out int age) ? age : throw new Exception("Invalid age format."));
                    cmd.Parameters.AddWithValue("@PPhone", PphoneTb.Text);
                    cmd.Parameters.AddWithValue("@PGender", PGenCb.SelectedItem?.ToString() ?? string.Empty);
                    cmd.Parameters.AddWithValue("@PBGroup", PBGroupCb.SelectedItem?.ToString() ?? string.Empty);
                    cmd.Parameters.AddWithValue("@PAddress", PAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PNum", key);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Patient Successfully Updated");
                Con.Close();
                Reset();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Con.State == System.Data.ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

    }
}

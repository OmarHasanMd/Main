using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Blood
{
    public partial class BloodTransfert : Form
    {
        public BloodTransfert()
        {
            InitializeComponent();
            fillPatientCb();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MuOmarHasan\Documents\BloodBankDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        private void fillPatientCb()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select PNum from PatientTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PName", typeof(string));
            dt.Load(rdr);
            PatientIdCb.ValueMember = "PNum";
            PatientIdCb.DataSource = dt;

            Con.Close();
        }
        private void GetData()
        {
            Con.Open();
            string query = "select * from PatientTbl where PNum='" + PatientIdCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PatNameTb.Text = dr["PName"].ToString();
                BloodGroup.Text = dr["PBGroup"].ToString();
            }
            Con.Close();

        }
        int stock = 0;
        private void GetStock(string BGroup)
        {
            Con.Open();
            string query = "select * from BloodTbl where Bgroup='" + BGroup + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stock = Convert.ToInt32(dr["BStock"].ToString());
            }
            Con.Close();

        }
        private void BloodTransfert_Load(object sender, EventArgs e)
        {

        }
        /*int oldstock;
        private void GetStock(string BGroup)
        {
            Con.Open();
            string query = "select * from BloodTbl where Bgroup='" + BGroup + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                oldstock = Convert.ToInt32(dr["BStock"].ToString());
            }
            Con.Close();

        }*/
        private void PatientIdCb_SelectionChangeComitted(object sender, EventArgs e)
        {
            GetData();
            GetStock(BloodGroup.Text);
            if (stock > 0)
            {
                TransferBtn.Visible = true;
                AvailableLbl.Text = "Available STock";
                AvailableLbl.Visible = true;
            }
            else
            {
                TransferBtn.Visible = false;
                AvailableLbl.Text = "STock not Available";
                AvailableLbl.Visible = true;

            }
        }
        private void Reset()
        {
            PatNameTb.Text = "";
            //PatientIdCb.SelectedIndex = -1;
            BloodGroup.Text = "";
            AvailableLbl.Visible = false;
            TransferBtn.Visible = false;
        }
        private void updateStock()
        {
            int newstock = stock - 1;
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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        //private void updateStock()
        //{
        //    try
        //    {
        //        // Validate if key is not null
        //        if (key == null)
        //        {
        //            MessageBox.Show("Key value is missing.");
        //            return;
        //        }

            //        // Corrected column name "PAddress"
            //        string query = "UPDATE BloodTbl SET " +
            //                       "Pname = @PName, " +
            //                       "Page = @PAge, " +
            //                       "Pphone = @PPhone, " +
            //                       "PGender = @PGender, " +
            //                       "PBGroup = @PBGroup, " +
            //                       "PAddress = @PAddress " +  // Corrected here
            //                       "WHERE PNum = @PNum;";

            //        Con.Open();
            //        using (SqlCommand cmd = new SqlCommand(query, Con))
            //        {
            //            // Use parameters to prevent SQL injection
            //            cmd.Parameters.AddWithValue("@PName", PNameTb.Text);
            //            cmd.Parameters.AddWithValue("@PAge", int.TryParse(PAgeTb.Text, out int age) ? age : throw new Exception("Invalid age format."));
            //            cmd.Parameters.AddWithValue("@PPhone", PphoneTb.Text);
            //            cmd.Parameters.AddWithValue("@PGender", PGenCb.SelectedItem?.ToString() ?? string.Empty);
            //            cmd.Parameters.AddWithValue("@PBGroup", PBGroupCb.SelectedItem?.ToString() ?? string.Empty);
            //            cmd.Parameters.AddWithValue("@PAddress", PAddressTb.Text);
            //            cmd.Parameters.AddWithValue("@PNum", key);

            //            cmd.ExecuteNonQuery();
            //        }

            //        MessageBox.Show("Patient Successfully Updated");
            //        Con.Close();
            //        Reset();
            //        populate();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    finally
            //    {
            //        if (Con.State == System.Data.ConnectionState.Open)
            //        {
            //            Con.Close();
            //        }
            //    }
            //}
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string query = "insert into TransferTbl values('" + PatNameTb.Text + "','" + BloodGroup.Text + "');";

                    Con.Open();
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfull Transfer");
                    Con.Close();
                    GetStock(BloodGroup.Text);
                    updateStock();
                    Reset();
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

        private void label6_Click(object sender, EventArgs e)
        {
            Blood_Stock Bstock = new Blood_Stock();
            Bstock.Show();
            this.Hide();
        }
    }
}

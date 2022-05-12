using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOperationADO
{
    public partial class Form7 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form7()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public DataSet GetStudent()
        {
            da = new SqlDataAdapter("select * from Student", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "stud");
            return ds;


        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ds = GetStudent();
            DataRow row = ds.Tables["stud"].NewRow();
            row["RollNo"] = textBoxRoll.Text;
            row["Name"] = textBoxName.Text;
            row["Branch"] = textBoxBranch.Text;
            row["Percentage"] = textBoxPercentage.Text;
            ds.Tables["stud"].Rows.Add(row);
            int res = da.Update(ds.Tables["stud"]);
            if (res == 1)
                MessageBox.Show("Record saved");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetStudent();
            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(textBoxRoll.Text));
            if (row != null)
            {
                textBoxName.Text = row["Name"].ToString();
                textBoxBranch.Text = row["Branch"].ToString();
                textBoxPercentage.Text = row["Percentage"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int roll= Convert.ToInt32(textBoxRoll.Text);
            if (string.IsNullOrEmpty(textBoxName.Text) && roll > 0)
            {
                MessageBox.Show("Enter name or salary should be greater than 0");
            }
            else
            {
                ds = GetStudent();

                DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(textBoxRoll.Text));
                if (row != null)
                {
                    row["Name"] = textBoxName.Text;
                    row["Branch"] = textBoxBranch.Text;
                    row["Percentage"] = textBoxPercentage.Text;
                    int res = da.Update(ds.Tables["stud"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }
         }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetStudent();

            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(textBoxRoll.Text));
            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["stud"]);
                if (res == 1)
                    MessageBox.Show("record deleted");
            }
            else
            {
                MessageBox.Show("Record not found");
            }
            }

       
    }
}

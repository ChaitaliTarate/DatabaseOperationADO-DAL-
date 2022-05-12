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
    public partial class Form8 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form8()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public DataSet GetProduct1()
        {
            da = new SqlDataAdapter("select * from Product1", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "prod");
            return ds;


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetProduct1();
            DataRow row = ds.Tables["prod"].NewRow();
            row["PId"] = textBoxId.Text;
            row["PName"] = textBoxName.Text;
            row["Price"] = textBoxPrice.Text;

            ds.Tables["prod"].Rows.Add(row);
            int res = da.Update(ds.Tables["prod"]);
            if (res == 1)
                MessageBox.Show("Record saved");

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetProduct1();
            DataRow row = ds.Tables["prod"].Rows.Find(Convert.ToInt32(textBoxId.Text));
            if (row != null)
            {
                textBoxName.Text = row["PName"].ToString();
                textBoxPrice.Text = row["Price"].ToString();

            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxId.Text);
            if (string.IsNullOrEmpty(textBoxName.Text) && id > 0)
            {
                MessageBox.Show("Enter name or salary should be greater than 0");
            }
            else
            {
                ds = GetProduct1();

                DataRow row = ds.Tables["prod"].Rows.Find(Convert.ToInt32(textBoxId.Text));
                if (row != null)
                {
                    row["PName"] = textBoxName.Text;
                    row["Price"] = textBoxPrice.Text;

                    int res = da.Update(ds.Tables["prod"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetProduct1();

            DataRow row = ds.Tables["prod"].Rows.Find(Convert.ToInt32(textBoxId.Text));
            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["prod"]);
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
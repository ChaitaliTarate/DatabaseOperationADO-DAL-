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
    public partial class Form6 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form6()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }


        public DataSet GetEmployee1()
        {
            da = new SqlDataAdapter("select * from Employee1", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "employee1");
            return ds;


        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetEmployee1();
            DataRow row = ds.Tables["employee1"].NewRow();
            row["EmpId"] = textBoxId.Text;
            row["EmpName"] = textBoxName.Text;
            row["Designation"] = textBoxDesignation.Text;
            row["Salary"] = textBoxSalary.Text;
            ds.Tables["employee1"].Rows.Add(row);
            int res = da.Update(ds.Tables["employee1"]);
            if (res == 1)
                MessageBox.Show("Record saved");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetEmployee1();
            DataRow row = ds.Tables["employee1"].Rows.Find(Convert.ToInt32(textBoxId.Text));
            if(row!=null)
            {
                textBoxName.Text=row["EmpName"].ToString();
                textBoxDesignation.Text = row["Designation"].ToString();
                textBoxSalary.Text = row["Salary"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double sal = Convert.ToDouble(textBoxSalary.Text);
            if (string.IsNullOrEmpty(textBoxName.Text) && sal > 0)
            {
                MessageBox.Show("Enter name or salary should be greater than 0");
            }
            else
            {
                 ds = GetEmployee1();
                
                DataRow row = ds.Tables["employee1"].Rows.Find(Convert.ToInt32(textBoxId.Text));
                if (row != null)
                {
                    row["EmpName"] = textBoxName.Text;
                    row["Designation"] = textBoxDesignation.Text;
                    row["Salary"] = textBoxSalary.Text;
                    int res = da.Update(ds.Tables["employee1"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetEmployee1();
           
            DataRow row = ds.Tables["employee1"].Rows.Find(Convert.ToInt32(textBoxId.Text));
            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["employee1"]);
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

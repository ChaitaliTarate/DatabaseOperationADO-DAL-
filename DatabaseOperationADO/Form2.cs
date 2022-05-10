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

namespace DatabaseOperationADO
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=DESKTOP-V05A0QF\SQLEXPRESS;database=ThinkQuotient;Integrated Security=True");
        }
        public void ClearAll()
        {
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxDesignation.Clear();
            textBoxSalary.Clear();

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into EmpTable values(@eid,@ename,@designation,@salary)";
                cmd = new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(textBoxId.Text));
                cmd.Parameters.AddWithValue("@ename", textBoxName.Text);
                cmd.Parameters.AddWithValue("@designation", textBoxDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(textBoxSalary.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Records Inserted");
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from EmpTable where EmpId=@eid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(textBoxId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        textBoxName.Text = dr["EmpName"].ToString();
                        textBoxDesignation.Text = dr["Designation"].ToString();
                        textBoxSalary.Text = dr["Salary"].ToString();

                       
                    }
                }
                else
                {
                    MessageBox.Show("Record not Found");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update EmpTable set EmpName=@ename,Designation=@designation,Salary=@salary where EmpId=@eid";
                cmd = new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(textBoxId.Text));
                cmd.Parameters.AddWithValue("@ename", textBoxName.Text);
                cmd.Parameters.AddWithValue("@designation",textBoxDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(textBoxSalary.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Records Updated");
                    ClearAll();
                }
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from EmpTable where EmpId=@eid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(textBoxId.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if(res==1)
                {
                    MessageBox.Show("Record deleted");
                    ClearAll();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(EmpId) from EmpTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
                Object obj = cmd.ExecuteScalar();
                if(obj==DBNull.Value)
                {
                    textBoxId.Text = "1";
                }
                else
                {
                    int eid = Convert.ToInt32(obj);
                    eid++;
                    textBoxId.Text = eid.ToString();
                }
                textBoxId.Enabled = false;
                textBoxName.Clear();
                textBoxDesignation.Clear();
                textBoxSalary.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from EmpTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not Found");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBoxId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxDesignation.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxSalary.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOperationADO
{
    
    public partial class Form3 : Form
    {
        StudentDAL studentDAl = new StudentDAL();
        public Form3()
        {
            InitializeComponent();
        }

        public void ClearAll()
        {
            textBoxRoll.Clear();
            textBoxName.Clear();
            textBoxBranch.Clear();
            textBoxPercentage.Clear();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();
                student.RollNo = Convert.ToInt32(textBoxRoll.Text);
                student.Name = textBoxName.Text;
                student.Branch = textBoxBranch.Text;
                student.Percentage = Convert.ToInt32(textBoxPercentage.Text);

                int res = studentDAl.CreateStudent(student);

                if (res == 1)
                {
                    MessageBox.Show("Records Inserted");
                    textBoxRoll.Enabled = true;
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = studentDAl.GetStudentByRoll(Convert.ToInt32(textBoxRoll.Text));
              
                textBoxName.Text=student.Name;
                textBoxBranch.Text = student.Branch;
                textBoxPercentage.Text = student.Percentage.ToString();
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();
                student.RollNo = Convert.ToInt32(textBoxRoll.Text);
                student.Name = textBoxName.Text;
                student.Branch = textBoxBranch.Text;
                student.Percentage = Convert.ToInt32(textBoxPercentage.Text);

                int res = studentDAl.UpdateStudent(student);
                if(res==1)
                {
                    MessageBox.Show("Records updated ");
               
                    ClearAll();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();
                student.RollNo = Convert.ToInt32(textBoxRoll.Text);
               
                int res = studentDAl.DeleteStudent(student);

                if (res == 1)
                {
                    MessageBox.Show("Records deleted");
                  
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable table = studentDAl.GetAllStudent();
            dataGridView1.DataSource = table;
        }
    }
 }


using DatabaseOperationADO.DAL;
using DatabaseOperationADO.Model;
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
    public partial class Form4 : Form
    {
        Employee1DAL empdal = new Employee1DAL();
        public Form4()
        {
            InitializeComponent();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable table = empdal.GetAllEmployee1();
            dataGridView1.DataSource = table;
        }


     
        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee1 employee1 = new Employee1();
            employee1.EmpId = Convert.ToInt32(textBoxId.Text);
            employee1.EmpName = textBoxName.Text;
            employee1.Designation = textBoxDesignation.Text;
            employee1.Salary = Convert.ToDouble(textBoxSalary.Text);
            int res = empdal.Save(employee1);
            if (res == 1)
                MessageBox.Show("Inserted the record");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Employee1 employee1 = empdal.GetEmployee1ById(Convert.ToInt32(textBoxId.Text));
            if (employee1.EmpId > 0)
            {
              //  textBoxId.Text = employee1.EmpId.ToString();
             textBoxName.Text =employee1.EmpName;
                textBoxDesignation.Text = employee1.Designation;
               textBoxSalary.Text = employee1.Salary.ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Employee1 employee1 = new Employee1();
            employee1.EmpId = Convert.ToInt32(textBoxId.Text);
            employee1.EmpName = textBoxName.Text;
            employee1.Designation = textBoxDesignation.Text;
            employee1.Salary = Convert.ToDouble(textBoxSalary.Text);
            int res = empdal.Update(employee1);
            if (res == 1)
                MessageBox.Show("updated the record");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = empdal.Delete(Convert.ToInt32(textBoxId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
        }
    }
}

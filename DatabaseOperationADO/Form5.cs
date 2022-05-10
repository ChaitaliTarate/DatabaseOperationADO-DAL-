using DatabaseOperationADO.DAL;
using DatabaseOperationADO.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOperationADO
{
    public partial class Form5 : Form
    {
       
        ProductDAL prodDAL = new ProductDAL();
        public Form5()
        {
            InitializeComponent();
        }
        public void ClearAll()
        {
            textBoxId.Clear();
            textBoxName.Clear();
            textBoxPrice.Clear();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
                Product prod = new Product();
               // prod.PId = Convert.ToInt32(textBoxId.Text);
                prod.PName = textBoxName.Text;
                prod.Price = Convert.ToDouble(textBoxPrice.Text);
                int res = prodDAL.SaveProduct(prod);
                if (res == 1)
                {
                    MessageBox.Show("Record inserted");
                    textBoxId.Enabled = true;
                    ClearAll();
                }
            
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Product prod = prodDAL.GetProductById(Convert.ToInt32(textBoxId.Text));
            textBoxName.Text = prod.PName;
            textBoxPrice.Text = prod.Price.ToString();
        }

        private void btnShowAllProduct_Click(object sender, EventArgs e)
        {
            DataTable table = prodDAL.GetAllEmployee1();
            dataGridView1.DataSource = table;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.PId = Convert.ToInt32(textBoxId.Text);
            prod.PName = textBoxName.Text;
            prod.Price = Convert.ToInt32(textBoxPrice.Text);
            int res = prodDAL.UpdateProduct(prod);
            if (res == 1)
            {
                MessageBox.Show("Record updated");
                ClearAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = prodDAL.Delete(Convert.ToInt32(textBoxId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
            ClearAll();
        }

    
    }
}

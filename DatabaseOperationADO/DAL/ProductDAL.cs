using DatabaseOperationADO.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseOperationADO.DAL
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public Product GetProduct1ById(int id)
        {
            Product prod = new Product();
            string qry = "select * from Product where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows) // existance of record in dr object
            {
                while (dr.Read())
                {
                    prod.PId = Convert.ToInt32(dr["Id"]);
                    prod.PName = dr["Name"].ToString();// ["Name"] should match col name
                    prod.Price= Convert.ToDouble(dr["Price"]);
                }
            }
            con.Close();
            return prod;
        }
        public int SaveProduct(Product prod)
        {

            string qry = "insert into Product1 values(@name,@price)";
            cmd = new SqlCommand(qry, con);
           // cmd.Parameters.AddWithValue("@id", prod.PId);
            cmd.Parameters.AddWithValue("@name", prod.PName);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public Product GetProductById(int id)
        {
            Product prod = new Product();
            string qry = "select * from Product1 where PId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows) // existance of record in dr object
            {
                while (dr.Read())
                {
                    prod.PId = Convert.ToInt32(dr["PId"]);
                    prod.PName = dr["PName"].ToString();// ["Name"] should match col name
                    prod.Price = Convert.ToInt32(dr["Price"]);
                }
            }
            con.Close();
            return prod;
        }

        public int UpdateProduct(Product prod)
        {

            string qry = "update Product1 set PName=@name,Price=@price where PId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", prod.PId);
            cmd.Parameters.AddWithValue("@name", prod.PName);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int pid)
        {
            string qry = "delete from Product1 where PId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", pid);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            return res;
        }

        public DataTable GetAllEmployee1()
        {
            DataTable table = new DataTable();
            string qry = "select * from Product1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();


            return table;
        }
    }
}

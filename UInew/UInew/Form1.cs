using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Notice;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UInew
{
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection("host=localhost;user=root;password=;database=project_data_final");
        MySqlCommand? comm;
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {


            open_connection();
            load_customer_griddata_init();
            load_employee_griddata_init();
            load_product_type_griddata_init();
            load_product_griddata_init();
            load_comboBox7_griddata_init();
            load_order_griddata_init();
            load_comboBox1_griddata_init();
            load_comboBox2_griddata_init();
            load_comboBox3_griddata_init();
            load_order_detail_griddata_init();
            load_comboBox5_griddata_init();
            load_comboBox6_griddata_init();


        }
        private void open_connection()
        {


            con.Open();

        }

        // customers

        private void load_customer_griddata_init()
        {

            string sql = "SELECT * FROM customers";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "customers");
            DataTable? table = ds.Tables["customers"];
            dataGridView3.DataSource = table != null ? table.DefaultView : null;




        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string id = dataGridView3.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                string name = dataGridView3.SelectedRows[0].Cells[1].Value?.ToString() ?? "";

                string iphone = dataGridView3.SelectedRows[0].Cells[2].Value?.ToString() ?? "";




                maskedTextBox1.Text = id;
                maskedTextBox2.Text = name;

                maskedTextBox4.Text = iphone;
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox1.Text;
            var name = maskedTextBox2.Text;

            var iphone = maskedTextBox4.Text;

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project_data_final`.`customers`(`customer_id`, `customer_name`, `phone_number`) " +
                              "VALUES (@id, @name, @phone)";

            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", name);

            comm.Parameters.AddWithValue("@phone", iphone);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_customer_griddata_init();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox1.Text;
            var name = maskedTextBox2.Text;

            var iphone = maskedTextBox4.Text;

            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project_data_final`.`customers` " +
                               "SET `customer_name` = @name, " +

                               "`phone_number` = @phone " +
                               "WHERE `customer_id` = @id";

            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", name);

            comm.Parameters.AddWithValue("@phone", iphone);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            load_customer_griddata_init();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox1.Text;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project_data_final`.`customers`" + "WHERE `customer_id` = @id";

            comm.Parameters.AddWithValue("@id", id);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_customer_griddata_init();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox4.Clear();

        }
        private void button13_Click(object sender, EventArgs e)
        {
            string id = maskedTextBox1.Text.Trim();
            string name = maskedTextBox2.Text.Trim();
            string phone = maskedTextBox4.Text.Trim();

            string query = @"
                            SELECT * 
                            FROM `project_data_final`.`customers` 
                            WHERE (`customer_id` = @id AND @id != '') 
                            OR (`customer_name` LIKE CONCAT('%', @name, '%') AND @name != '') 
                            OR (`phone_number` LIKE CONCAT('%', @phone, '%') AND @phone != '')";

            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@name", name);
                comm.Parameters.AddWithValue("@phone", phone);

                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView3.DataSource = dt;
                        MessageBox.Show("Search Result Found!");
                    }
                    else
                    {
                        MessageBox.Show("No matching records found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }


        }
        private void button9_Click(object sender, EventArgs e)
        {
            load_customer_griddata_init();
        }

        /// employee

        private void load_employee_griddata_init()
        {

            string sql = "SELECT * FROM employee";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "employee");
            DataTable? table = ds.Tables["employee"];
            dataGridView4.DataSource = table != null ? table.DefaultView : null;




        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string id = dataGridView4.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                string name = dataGridView4.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
                string iphone = dataGridView4.SelectedRows[0].Cells[2].Value?.ToString() ?? "";
                string address = dataGridView4.SelectedRows[0].Cells[3].Value?.ToString() ?? "";




                maskedTextBox7.Text = id;
                maskedTextBox6.Text = name;
                maskedTextBox5.Text = iphone;
                maskedTextBox3.Text = address;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox7.Text;
            var name = maskedTextBox6.Text;

            var iphone = maskedTextBox5.Text;
            var address = maskedTextBox3.Text;

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project_data_final`.`employee`(`employee_id`, `employee_name`, `phone_number`, `address`) " +
                              "VALUES (@id, @name, @phone, @address)";

            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", name);

            comm.Parameters.AddWithValue("@phone", iphone);
            comm.Parameters.AddWithValue("@address", address);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_employee_griddata_init();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox7.Text;
            var name = maskedTextBox6.Text;

            var iphone = maskedTextBox5.Text;
            var address = maskedTextBox3.Text;

            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project_data_final`.`employee` " +
                               "SET `employee_name` = @name, " +

                               "`phone_number` = @phone ," +
                               "`address` = @address " +
                               "WHERE `employee_id` = @id";

            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", name);

            comm.Parameters.AddWithValue("@phone", iphone);
            comm.Parameters.AddWithValue("@address", address);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_employee_griddata_init();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox7.Text;


            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project_data_final`.`employee`" + "WHERE `employee_id` = @id";

            comm.Parameters.AddWithValue("@id", id);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_employee_griddata_init();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            maskedTextBox7.Clear();
            maskedTextBox6.Clear();
            maskedTextBox5.Clear();

            maskedTextBox3.Clear();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            string id = maskedTextBox7.Text.Trim();
            string name = maskedTextBox6.Text.Trim();

            string phone = maskedTextBox5.Text.Trim();
            string address = maskedTextBox3.Text.Trim();

            string query = @"
                            SELECT * 
                            FROM `project_data_final`.`employee` 
                            WHERE (`employee_id` = @id AND @id != '') 
                            OR (`employee_name` LIKE CONCAT('%', @name, '%') AND @name != '') 
                            OR (`phone_number` LIKE CONCAT('%', @phone, '%') AND @phone != '')
                            OR (`address` LIKE CONCAT('%', @address, '%') AND @address != '')";

            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@name", name);
                comm.Parameters.AddWithValue("@phone", phone);
                comm.Parameters.AddWithValue("@address", address);

                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView4.DataSource = dt;
                        MessageBox.Show("Search Result Found!");
                    }
                    else
                    {
                        MessageBox.Show("No matching records found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
        private void button14_Click(object sender, EventArgs e)
        {
            load_employee_griddata_init();
        }

        /// product_type

        private void load_product_type_griddata_init()
        {

            string sql = "SELECT * FROM product_type";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "product_type");
            DataTable? table = ds.Tables["product_type"];
            dataGridView1.DataSource = table != null ? table.DefaultView : null;




        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                string name = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString() ?? "";





                textBox1.Text = id;
                textBox2.Text = name;

            }
        }

        private void savept_Click(object sender, EventArgs e)
        {
            var id = textBox1.Text;
            var name = textBox2.Text;



            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project_data_final`.`product_type`(`type_id`, `type_name`) " +
                              "VALUES (@id, @name )";

            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", name);



            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_product_type_griddata_init();
        }
        private void updatept_Click_1(object sender, EventArgs e)
        {
            var id = textBox1.Text;
            var name = textBox2.Text;



            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project_data_final`.`product_type` " +
                               "SET `type_name` = @name " +


                               "WHERE `type_id` = @id";

            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", name);



            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_product_type_griddata_init();
        }
        private void deletept_Click(object sender, EventArgs e)
        {
            var id = textBox1.Text;




            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project_data_final`.`product_type`" + "WHERE `type_id` = @id"; ;

            comm.Parameters.AddWithValue("@id", id);




            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_product_type_griddata_init();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

        }
        private void searchpt_Click_1(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();



            string query = @"
                            SELECT * 
                            FROM `project_data_final`.`product_type` 
                            WHERE (`type_id` = @id AND @id != '') 
                            
                            OR (`type_name` LIKE CONCAT('%', @name, '%') AND @name != '')";

            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@name", name);


                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        MessageBox.Show("Search Result Found!");
                    }
                    else
                    {
                        MessageBox.Show("No matching records found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void resetpt_Click(object sender, EventArgs e)
        {
            load_product_type_griddata_init();
        }

        //
        private void load_product_griddata_init()
        {

            string query = @"
                            SELECT  p.product_id, p.product_name, pt.type_id, pt.type_name, pr.price, p.unit
                            FROM product p
                            JOIN price pr ON p.product_id = pr.product_id
                            LEFT JOIN product_type pt ON p.type_id = pt.type_id
                            ORDER BY p.product_id";

            var cmd = con.CreateCommand();
            cmd.CommandText = query;

            DataTable dt = new DataTable();
            using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }

            dataGridView2.DataSource = dt;




        }

        private void load_comboBox7_griddata_init()
        {
            string query = "SELECT type_id, type_name FROM product_type";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // เพิ่มแถวสำหรับเลือก "---Select---" โดยใช้ string ว่างแทน DBNull
            DataRow newRow = dt.NewRow();
            newRow["type_id"] = ""; // ใช้ string ว่างแทน DBNull.Value
            newRow["type_name"] = "---Select---";
            dt.Rows.InsertAt(newRow, 0);

            comboBox7.DataSource = dt;
            comboBox7.DisplayMember = "type_name";
            comboBox7.ValueMember = "type_id";

            comboBox7.SelectedIndex = 0; // เลือกแถวแรกให้แสดง "---Select---"
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {

                string product_id = dataGridView2.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                string product_name = dataGridView2.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
                string type_id = dataGridView2.SelectedRows[0].Cells[2].Value?.ToString() ?? "";
                string price = dataGridView2.SelectedRows[0].Cells[4].Value?.ToString() ?? "";


                textBox6.Text = product_id;
                textBox5.Text = product_name;

                if (!string.IsNullOrEmpty(type_id))
                {
                    comboBox7.SelectedValue = type_id;
                }
                else
                {
                    comboBox7.SelectedIndex = -1;
                }

                textBox3.Text = price;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            var product_id = textBox6.Text;
            var product_name = textBox5.Text;
            var type_id = comboBox7.SelectedValue?.ToString();
            var price = textBox3.Text;

            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var comm = con.CreateCommand();
                    comm.Transaction = transaction;


                    comm.CommandText = @"
                INSERT INTO product (product_id, product_name, type_id) 
                VALUES (@product_id, @product_name, @type_id)";
                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.Parameters.AddWithValue("@product_name", product_name);
                    comm.Parameters.AddWithValue("@type_id", type_id);
                    comm.ExecuteNonQuery();


                    comm.CommandText = @"
                INSERT INTO price ( product_id, price) 
                VALUES ( @product_id, @price)";
                    comm.Parameters.Clear();

                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.Parameters.AddWithValue("@price", price);
                    comm.ExecuteNonQuery();


                    transaction.Commit();
                    MessageBox.Show("Save Data Completed!");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            load_product_griddata_init();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            var product_id = textBox6.Text;
            var product_name = textBox5.Text;
            var type_id = comboBox7.SelectedValue?.ToString();
            var price = textBox3.Text;

            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var comm = con.CreateCommand();
                    comm.Transaction = transaction;


                    comm.CommandText = @"
                            UPDATE product 
                            SET product_name = @product_name, type_id = @type_id
                            WHERE product_id = @product_id";
                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.Parameters.AddWithValue("@product_name", product_name);
                    comm.Parameters.AddWithValue("@type_id", type_id);
                    comm.ExecuteNonQuery();


                    comm.CommandText = @"
                                         UPDATE price 
                                          SET price = @price
                                         WHERE  product_id = @product_id";
                    comm.Parameters.Clear();

                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.Parameters.AddWithValue("@price", price);
                    comm.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Update Completed!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            load_product_griddata_init();

        }
        private void button7_Click(object sender, EventArgs e)
        {
            var product_id = textBox6.Text.Trim();

            if (string.IsNullOrEmpty(product_id))
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    var comm = con.CreateCommand();
                    comm.Transaction = transaction;


                    comm.CommandText = @"DELETE FROM price WHERE product_id = @product_id";
                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.ExecuteNonQuery();


                    comm.CommandText = @"DELETE FROM product WHERE product_id = @product_id";
                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Delete Completed!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            load_product_griddata_init();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox5.Clear();
            textBox3.Clear();


            comboBox7.SelectedValue = "";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var product_id = textBox6.Text.Trim();
            var product_name = textBox5.Text.Trim();
            var type_id = comboBox7.SelectedValue?.ToString();
            string priceText = textBox3.Text.Trim();
            object priceParam = string.IsNullOrWhiteSpace(priceText)
                ? DBNull.Value
                : decimal.TryParse(priceText, out var priceVal) ? (object)priceVal : DBNull.Value;

            string query = @"
                                    SELECT  
                                        p.product_id, 
                                        p.product_name, 
                                        pt.type_id, 
                                        pt.type_name, 
                                        pr.price, 
                                        p.unit
                                    FROM product p
                                    LEFT JOIN product_type pt ON p.type_id = pt.type_id
                                    LEFT JOIN price pr ON p.product_id = pr.product_id
                                    WHERE 
                                        (@product_id = '' OR p.product_id = @product_id)
                                        AND (@product_name = '' OR p.product_name LIKE CONCAT('%', @product_name, '%'))
                                        AND (@type_id = '' OR p.type_id = @type_id)
                                        AND (@price IS NULL OR ROUND(pr.price, 2) = ROUND(@price, 2))
";

            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                comm.Parameters.AddWithValue("@product_id", product_id);
                comm.Parameters.AddWithValue("@product_name", product_name);
                comm.Parameters.AddWithValue("@type_id", type_id ?? "");
                comm.Parameters.AddWithValue("@price", priceParam);

                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView2.DataSource = dt;
                    MessageBox.Show(dt.Rows.Count > 0 ? "Search Result Found!" : "No matching records found.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            load_product_griddata_init();

        }

        //

        private void load_order_griddata_init()
        {

            string query = @"
                            SELECT 
                                o.order_id,
                                o.date,
                                o.customer_id,
                                c.customer_name,
                                o.type_id,
                                pt.type_name,
                                o.employee_id,
                                e.employee_name
                            FROM orders o
                            JOIN customers c ON o.customer_id = c.customer_id
                            JOIN product_type pt ON o.type_id = pt.type_id
                            JOIN employee e ON o.employee_id = e.employee_id
                             order by o.order_id;";

            var cmd = con.CreateCommand();
            cmd.CommandText = query;

            DataTable dt = new DataTable();
            using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }

            dataGridView5.DataSource = dt;

        }
        private void load_comboBox1_griddata_init()
        {
            string query = "SELECT customer_id, customer_name FROM customers";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow newRow = dt.NewRow();
            newRow["customer_name"] = "---Select---";
            newRow["customer_id"] = "";
            dt.Rows.InsertAt(newRow, 0);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "customer_name";
            comboBox1.ValueMember = "customer_id"; // ✅ แก้ตรงนี้
            comboBox1.SelectedIndex = 0;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0) // ข้ามตัวเลือก "---Select---"
            {
                if (comboBox1.SelectedItem is DataRowView selectedRow)
                {
                    maskedTextBox11.Text = selectedRow["customer_id"].ToString();
                }
            }
            else
            {
                maskedTextBox11.Clear(); // ถ้าเลือก "---Select---" ให้ล้างช่อง
            }
        }


        private void load_comboBox2_griddata_init()
        {
            string query = "SELECT type_id, type_name FROM product_type";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow newRow = dt.NewRow();
            newRow["type_name"] = "---Select---";
            newRow["type_id"] = "";
            dt.Rows.InsertAt(newRow, 0);

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "type_name";
            comboBox2.ValueMember = "type_id"; // ✅ แก้ตรงนี้
            comboBox2.SelectedIndex = 0;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > 0) // ข้ามตัวเลือก "---Select---"
            {
                if (comboBox2.SelectedItem is DataRowView selectedRow)
                {
                    maskedTextBox9.Text = selectedRow["type_id"].ToString();
                }
            }
            else
            {
                maskedTextBox9.Clear(); // ถ้าเลือก "---Select---" ให้ล้างช่อง
            }

        }
        private void load_comboBox3_griddata_init()
        {
            string query = "SELECT employee_id, employee_name FROM employee";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow newRow = dt.NewRow();
            newRow["employee_name"] = "---Select---";
            newRow["employee_id"] = "";
            dt.Rows.InsertAt(newRow, 0);

            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "employee_name";
            comboBox3.ValueMember = "employee_id"; // ✅ แก้ตรงนี้
            comboBox3.SelectedIndex = 0;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex > 0) // ข้ามตัวเลือก "---Select---"
            {
                if (comboBox3.SelectedItem is DataRowView selectedRow)
                {
                    maskedTextBox8.Text = selectedRow["employee_id"].ToString();
                }
            }
            else
            {
                maskedTextBox8.Clear(); // ถ้าเลือก "---Select---" ให้ล้างช่อง
            }

        }




        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[e.RowIndex];

                string order_id = row.Cells["order_id"].Value?.ToString() ?? "";
                string date = row.Cells["date"].Value?.ToString() ?? "";
                string customer_id = row.Cells["customer_id"].Value?.ToString() ?? "";
                string type_id = row.Cells["type_id"].Value?.ToString() ?? "";
                string employee_id = row.Cells["employee_id"].Value?.ToString() ?? "";

                maskedTextBox10.Text = order_id;
                maskedTextBox11.Text = customer_id;
                maskedTextBox9.Text = type_id;
                maskedTextBox8.Text = employee_id;

                if (DateTime.TryParse(date, out var dt))
                    dateTimePicker1.Value = dt;
                else
                    dateTimePicker1.Value = DateTime.Now;

                comboBox1.SelectedValue = customer_id;
                comboBox2.SelectedValue = type_id;
                comboBox3.SelectedValue = employee_id;
            }
        }


        private void button24_Click(object sender, EventArgs e)
        {
            DateTime orderDate = dateTimePicker1.Value;

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = @"INSERT INTO `orders` (`order_id`, `customer_id`, `employee_id`, `date`, `type_id`) 
                             VALUES (@order_id, @customer_id, @employee_id, @order_date, @type_id)";

                comm.Parameters.AddWithValue("@order_id", maskedTextBox10.Text.Trim());
                comm.Parameters.AddWithValue("@customer_id", comboBox1.SelectedValue);
                comm.Parameters.AddWithValue("@employee_id", comboBox3.SelectedValue);
                comm.Parameters.AddWithValue("@order_date", orderDate);
                comm.Parameters.AddWithValue("@type_id", comboBox2.SelectedValue);

                try
                {
                    int rowsAffected = comm.ExecuteNonQuery();
                    MessageBox.Show("Save Data Completed!");
                    load_order_griddata_init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            DateTime orderDate = dateTimePicker1.Value;

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = @"UPDATE `orders` 
                             SET `customer_id` = @customer_id, 
                                 `employee_id` = @employee_id, 
                                 `date` = @order_date, 
                                 `type_id` = @type_id
                             WHERE `order_id` = @order_id";

                comm.Parameters.AddWithValue("@order_id", maskedTextBox10.Text.Trim());
                comm.Parameters.AddWithValue("@customer_id", comboBox1.SelectedValue);
                comm.Parameters.AddWithValue("@employee_id", comboBox3.SelectedValue);
                comm.Parameters.AddWithValue("@order_date", orderDate);
                comm.Parameters.AddWithValue("@type_id", comboBox2.SelectedValue);

                try
                {
                    int rowsAffected = comm.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update Data Completed!");
                        load_order_griddata_init();
                    }
                    else
                    {
                        MessageBox.Show("No record was updated. Please check order_id.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var id = maskedTextBox10.Text.Trim();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "DELETE FROM orders WHERE order_id = @id";
                comm.Parameters.AddWithValue("@id", id);

                try
                {
                    int rowsAffected = comm.ExecuteNonQuery();
                    MessageBox.Show("Delete Data Completed!");
                    load_order_griddata_init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            maskedTextBox10.Clear();
            maskedTextBox11.Clear();
            maskedTextBox9.Clear();
            maskedTextBox8.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
        }







        private void button23_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            o.order_id,
            o.date,
            o.customer_id,
            c.customer_name,
            o.type_id,
            pt.type_name,
            o.employee_id,
            e.employee_name
        FROM orders o
        JOIN customers c ON o.customer_id = c.customer_id
        JOIN product_type pt ON o.type_id = pt.type_id
        JOIN employee e ON o.employee_id = e.employee_id
        WHERE 
            (@order_id = '' OR o.order_id = @order_id)
            AND (@customer_id = '' OR o.customer_id = @customer_id)
            AND (@employee_id = '' OR o.employee_id = @employee_id)
            AND (@type_id = '' OR o.type_id = @type_id)
            AND (@order_date IS NULL OR DATE(o.date) = @order_date)";

            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                comm.Parameters.AddWithValue("@order_id", string.IsNullOrWhiteSpace(maskedTextBox10.Text)
                    ? ""
                    : maskedTextBox10.Text.Trim());

                comm.Parameters.AddWithValue("@customer_id", comboBox1.SelectedIndex > 0
                    ? comboBox1.SelectedValue.ToString()
                    : "");

                comm.Parameters.AddWithValue("@employee_id", comboBox3.SelectedIndex > 0
                    ? comboBox3.SelectedValue.ToString()
                    : "");

                comm.Parameters.AddWithValue("@type_id", comboBox2.SelectedIndex > 0
                    ? comboBox2.SelectedValue.ToString()
                    : "");

                comm.Parameters.AddWithValue("@order_date", dateTimePicker1.Checked
                    ? (object)dateTimePicker1.Value.Date
                    : DBNull.Value);

                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView5.DataSource = dt;
                        MessageBox.Show("Search Result Found!");
                    }
                    else
                    {
                        dataGridView5.DataSource = null;
                        MessageBox.Show("No matching records found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }







        private void button21_Click(object sender, EventArgs e)
        {
            load_order_griddata_init();
        }

        //

        private void load_order_detail_griddata_init()
        {
            load_order_detail_griddata_init(null);
        }

        private void load_order_detail_griddata_init(string? order_id)
        {
            string query = @"
                            SELECT
                                od.detail_id,
                                o.order_id,
                                o.date,
                                c.customer_name,
                                p.product_id,
                                p.product_name,
                                pt.type_name,
                                od.count,
                                pr.price AS unit_price,
                                od.total,
                                p.unit
                            FROM order_detail od
                            INNER JOIN orders o ON od.order_id = o.order_id
                            INNER JOIN product p ON od.product_id = p.product_id
                            INNER JOIN product_type pt ON p.type_id = pt.type_id
                            INNER JOIN price pr ON p.product_id = pr.product_id
                            INNER JOIN customers c ON o.customer_id = c.customer_id
                            " + (string.IsNullOrEmpty(order_id) ? "" : "WHERE od.order_id = @order_id") + @"
                            ORDER BY od.detail_id;";

            using (var cmd = new MySqlCommand(query, con))
            {
                if (!string.IsNullOrEmpty(order_id))
                {
                    cmd.Parameters.AddWithValue("@order_id", order_id);
                }

                DataTable dt = new DataTable();
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                dataGridView6.DataSource = dt;
            }
        }
        private void load_comboBox5_griddata_init()
        {
            string query = "SELECT order_id, type_id FROM orders ORDER BY order_id";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow newRow = dt.NewRow();
                    newRow["order_id"] = "---Select---";
                    newRow["type_id"] = DBNull.Value;
                    dt.Rows.InsertAt(newRow, 0);

                    comboBox5.DataSource = dt;
                    comboBox5.DisplayMember = "order_id";
                    comboBox5.ValueMember = "order_id";
                    comboBox5.SelectedIndex = 0;
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex > 0)
            {
                if (comboBox5.SelectedItem is DataRowView selectedRow)
                {
                    string typeId = selectedRow["type_id"]?.ToString() ?? "";

                    string query = @"
                SELECT 
                    p.product_id,
                    p.product_name,
                    p.unit,
                    pr.price
                FROM product p
                INNER JOIN price pr ON p.product_id = pr.product_id
                WHERE p.type_id = @type_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@type_id", typeId);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow newRow = dt.NewRow();
                            newRow["product_id"] = "";
                            newRow["product_name"] = "---Select---";
                            newRow["unit"] = "";
                            newRow["price"] = DBNull.Value;
                            dt.Rows.InsertAt(newRow, 0);

                            comboBox6.DataSource = dt;
                            comboBox6.DisplayMember = "product_name";
                            comboBox6.ValueMember = "product_id";
                            comboBox6.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    comboBox6.DataSource = null;
                    comboBox6.Items.Clear();
                    maskedTextBox14.Clear();
                    maskedTextBox16.Clear();
                }
            }
            else
            {
                comboBox6.DataSource = null;
                comboBox6.Items.Clear();
                maskedTextBox14.Clear();
                maskedTextBox16.Clear();
            }
        }

        private void load_comboBox6_griddata_init()
        {
            try
            {
                string query = "SELECT product_id, product_name, unit FROM product";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DataRow newRow = dt.NewRow();
                        newRow["product_id"] = "";
                        newRow["product_name"] = "---Select---";
                        newRow["unit"] = "";
                        dt.Rows.InsertAt(newRow, 0);

                        comboBox6.DataSource = dt;
                        comboBox6.DisplayMember = "product_name";
                        comboBox6.ValueMember = "product_id";

                        if (comboBox6.Items.Count > 0)
                        {
                            comboBox6.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product list: " + ex.Message);
            }
        }


        private void comboBox61_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex > 0 && comboBox6.SelectedItem is DataRowView selectedRow)
            {
                maskedTextBox14.Text = selectedRow["product_id"]?.ToString() ?? "";
                maskedTextBox16.Text = selectedRow["unit"]?.ToString() ?? "";
            }
            else
            {
                maskedTextBox14.Clear();
                maskedTextBox16.Clear();
            }
        }


        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView6.Rows[e.RowIndex].DataBoundItem != null)
            {
                var row = dataGridView6.Rows[e.RowIndex];
                maskedTextBox15.Text = row.Cells["detail_id"].Value?.ToString() ?? "";
                comboBox5.SelectedValue = row.Cells["order_id"].Value?.ToString();
                comboBox6.SelectedValue = row.Cells["product_id"].Value?.ToString();
                maskedTextBox13.Text = row.Cells["count"].Value?.ToString() ?? "";
                maskedTextBox12.Text = row.Cells["total"].Value?.ToString() ?? "";
            }
        }


        private void button29_Click(object sender, EventArgs e)
        {
            using (MySqlCommand comm = con.CreateCommand())
            {
                comm.CommandText = @"INSERT INTO order_detail 
            (detail_id, order_id, product_id, count, total) 
            VALUES (@detail_id, @order_id, @product_id, @count, @total)";

                comm.Parameters.AddWithValue("@detail_id", maskedTextBox15.Text.Trim());
                comm.Parameters.AddWithValue("@order_id", comboBox5.SelectedValue);
                comm.Parameters.AddWithValue("@product_id", comboBox6.SelectedValue);
                comm.Parameters.AddWithValue("@count", maskedTextBox13.Text.Trim());
                comm.Parameters.AddWithValue("@total", maskedTextBox12.Text.Trim());

                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Save Data Completed!");
                    load_order_detail_griddata_init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button27_Click(object sender, EventArgs e)
        {
            using (MySqlCommand comm = con.CreateCommand())
            {
                comm.CommandText = @"UPDATE order_detail 
            SET order_id = @order_id, product_id = @product_id, count = @count, total = @total 
            WHERE detail_id = @detail_id";

                comm.Parameters.AddWithValue("@detail_id", maskedTextBox15.Text.Trim());
                comm.Parameters.AddWithValue("@order_id", comboBox5.SelectedValue);
                comm.Parameters.AddWithValue("@product_id", comboBox6.SelectedValue);
                comm.Parameters.AddWithValue("@count", maskedTextBox13.Text.Trim());
                comm.Parameters.AddWithValue("@total", maskedTextBox12.Text.Trim());

                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Update Data Completed!");
                    load_order_detail_griddata_init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            using (MySqlCommand comm = con.CreateCommand())
            {
                comm.CommandText = "DELETE FROM order_detail WHERE detail_id = @detail_id";
                comm.Parameters.AddWithValue("@detail_id", maskedTextBox15.Text.Trim());

                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Delete Data Completed!");
                    load_order_detail_griddata_init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            var detail_id = maskedTextBox15.Text.Trim();
            var order_id = comboBox5.SelectedIndex > 0 ? comboBox5.SelectedValue.ToString() : "";
            var product_id = comboBox6.SelectedIndex > 0 ? comboBox6.SelectedValue.ToString() : "";
            var count = maskedTextBox13.Text.Trim();
            var total = maskedTextBox12.Text.Trim();

            string query = @"
SELECT
    od.detail_id,
    o.order_id,
    o.date,
    c.customer_name,
    p.product_id,
    p.product_name,
    pt.type_name,
    od.count,
    pr.price AS unit_price,
    od.total,
    p.unit
FROM order_detail od
INNER JOIN orders o ON od.order_id = o.order_id
INNER JOIN product p ON od.product_id = p.product_id
INNER JOIN product_type pt ON p.type_id = pt.type_id
INNER JOIN price pr ON p.product_id = pr.product_id
INNER JOIN customers c ON o.customer_id = c.customer_id
WHERE 1=1
    AND (@detail_id = '' OR od.detail_id LIKE CONCAT('%', @detail_id, '%'))
    AND (@order_id = '' OR o.order_id LIKE CONCAT('%', @order_id, '%'))
    AND (@product_id = '' OR p.product_id LIKE CONCAT('%', @product_id, '%'))
    AND (@count = '' OR CAST(od.count AS CHAR) LIKE CONCAT('%', @count, '%'))
    AND (@total = '' OR CAST(od.total AS CHAR) LIKE CONCAT('%', @total, '%'))
ORDER BY od.detail_id;
";

            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                // ส่งพารามิเตอร์ทั้งหมด (ถ้าไม่มีค่าก็ส่ง "" เพื่อไม่ให้กรอง)
                comm.Parameters.AddWithValue("@detail_id", detail_id);
                comm.Parameters.AddWithValue("@order_id", order_id);
                comm.Parameters.AddWithValue("@product_id", product_id);
                comm.Parameters.AddWithValue("@count", count);
                comm.Parameters.AddWithValue("@total", total);

                // Debug output
                string debug = "Query Preview:\n" + query + "\n\nParameters:\n";
                foreach (MySqlParameter p in comm.Parameters)
                {
                    debug += $"{p.ParameterName} = {p.Value}\n";
                }
                Console.WriteLine(debug);

                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView6.DataSource = dt;
                        MessageBox.Show("Search Result Found!");
                    }
                    else
                    {
                        dataGridView6.DataSource = null;
                        MessageBox.Show("No matching records found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void button26_Click(object sender, EventArgs e)
        {
            maskedTextBox15.Clear();
            comboBox5.SelectedIndex = 0;

            maskedTextBox13.Clear();
            maskedTextBox12.Clear();
        }


        private void button28_Click(object sender, EventArgs e)
        {
            load_order_detail_griddata_init();
        }

        private void maskedTextBox13_MaskInputRejected(object sender, EventArgs e)
        {
            if (double.TryParse(maskedTextBox13.Text, out double weight))
            {
                double pricePerKg = 0.0;

                if (comboBox6.SelectedItem is DataRowView selectedRow)
                {
                    double.TryParse(selectedRow["price"].ToString(), out pricePerKg);
                }

                double totalPrice = weight * pricePerKg;
                maskedTextBox12.Text = totalPrice.ToString("F2", CultureInfo.InvariantCulture);
            }
            else
            {
                maskedTextBox12.Clear();
            }
        }

        private void maskedTextBox13_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {



        }







        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pttype1_Load(object sender, EventArgs e)
        {

        }

        private void updatept_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void searchpt_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pt_id_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void maskedTextBox9_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

            load_customer_griddata_init();
            load_employee_griddata_init();
            load_product_type_griddata_init();
            load_product_griddata_init();
            load_comboBox7_griddata_init();
            load_order_griddata_init();
            load_comboBox1_griddata_init();
            load_comboBox2_griddata_init();
            load_comboBox3_griddata_init();
            load_order_detail_griddata_init();
            load_comboBox5_griddata_init();
            load_comboBox6_griddata_init();
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox16_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox14_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace forrms
{
    public partial class Form3 : Form
    {
        private MySqlConnection connection;
        private string connections = "server=192.168.0.89; port=3306; username= dpr2214;password= dpr2214;database= dpr2214_form";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL(); 
            dataGridView1.Columns["idtovars"].ReadOnly = true;
        }
        public void HideButton3AndDataGridView1()
        {
            button1.Visible = false;
            button3.Visible = false;




        }
        public void HideButton3AndDataGridView12()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            dataGridView1.Columns["idtovars"].Visible = false;
            dataGridView1.ReadOnly = true;


        }












        private DataTable GetDataFromMySQL()
        {
            DataTable tovars = new DataTable();

            try
            {
                MySqlConnection connection = new MySqlConnection(connections);
                connection.Open();

                string query = "SELECT idtovars, NameTovar, Price, KolVo FROM tovars";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(tovars);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении данных из MySQL: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return tovars;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                using (MySqlConnection connection = new MySqlConnection(connections))
                {
                    connection.Open();

                    string updateQuery = "UPDATE tovars SET NameTovar = @NameTovar, Price = @Price, KolVo = @KolVo WHERE idtovars = @idtovars";
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);

                    // Получаем значения из выбранной строки в DataGridView
                    int idtovars = Convert.ToInt32(selectedRow.Cells["idtovars"].Value);

                    command.Parameters.AddWithValue("@NameTovar", selectedRow.Cells["NameTovar"].Value.ToString());
                    command.Parameters.AddWithValue("@Price", Convert.ToDecimal(selectedRow.Cells["Price"].Value));
                    command.Parameters.AddWithValue("@KolVo", Convert.ToInt32(selectedRow.Cells["KolVo"].Value));
                    command.Parameters.AddWithValue("@idtovars", idtovars);

                    int rowsAffected = command.ExecuteNonQuery(); // Выполняем обновление данных в базе данных

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных.");
                    }
                }
            }
        }
    }
}

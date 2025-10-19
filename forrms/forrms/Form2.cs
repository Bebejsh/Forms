using Google.Protobuf.WellKnownTypes;
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
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();

       
        }

     



        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL();
        }


        private MySqlConnection connection;
        private string connections = "server=192.168.0.89; port=3306; username= dpr2214;password= dpr2214;database= dpr2214_form";

        private DataTable GetDataFromMySQL()
        {
            DataTable tovars = new DataTable();

            try
            {
                MySqlConnection connection = new MySqlConnection(connections);
                connection.Open();

                string query = "SELECT * FROM tovars";
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

        

        private void DeleteRow(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connections))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM tovars WHERE idtovars = @id";
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно удалена.");
                }
                else
                {
                    MessageBox.Show("Не удалось удалить строку.");
                }
            }
        }


        






        private void button2_Click(object sender, EventArgs e)   //Obnova
        {
            dataGridView1.DataSource = GetDataFromMySQL();

        }

        private void button4_Click(object sender, EventArgs e)                  //Izmenit
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

        private void button1_Click(object sender, EventArgs e)   //Dobav
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, что хотя бы одна строка выбрана
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // Получаем первую выбранную строку

                using (MySqlConnection connection = new MySqlConnection(connections))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO tovars ( NameTovar, Price, KolVo, user_idUsers) VALUES ( @NameTovar, @Price, @KolVo, @user_idUsers)";
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // Получаем значения из выбранной строки в DataGridView
                  
                    command.Parameters.AddWithValue("@NameTovar", selectedRow.Cells["NameTovar"].Value.ToString());
                    command.Parameters.AddWithValue("@Price", Convert.ToDecimal(selectedRow.Cells["Price"].Value));
                    command.Parameters.AddWithValue("@KolVo", Convert.ToInt32(selectedRow.Cells["KolVo"].Value));
                    command.Parameters.AddWithValue("@user_idUsers", Convert.ToInt32(selectedRow.Cells["user_idUsers"].Value));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Строка успешно добавлена.");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить строку.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку в DataGridView.");
            }
        }

        private void button3_Click(object sender, EventArgs e)  //Ydali
        {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells["idtovars"].Value;
                    DeleteRow(id);
                }
            }


        }
}

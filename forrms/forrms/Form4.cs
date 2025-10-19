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
    public partial class Form4 : Form
    {




        public Form4()
        {
            InitializeComponent();
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

                string query = "SELECT NameTovar, Price, KolVo FROM tovars";
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL();
        }
    }
}

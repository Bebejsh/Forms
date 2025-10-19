using forrms;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace forrms
{
    public partial class Form1 : Form
    {

        private podkl dbConnection;
        public Form1()
        {
            InitializeComponent();
            dbConnection = new podkl();
        }
            private void Form1_Load(object sender, EventArgs e)
            {
               

                dbConnection.closeConnection();
            }
        
            private void button1_Click(object sender, EventArgs e)
            {
            dbConnection.openConnection();
            MySqlConnection connection = dbConnection.GetConnection();


            string vxod = "Select count(*) from user where login =@login and password = @password";
         
            MySqlCommand Command = new MySqlCommand(vxod, connection);
            using (Command)
            { Command.Parameters.AddWithValue("@login", textBox1.Text);
              Command.Parameters.AddWithValue("@password", textBox2.Text);

                int count = Convert.ToInt32(Command.ExecuteScalar());

                if (count > 0)
                {
                    string role = ""; // ѕеременна€ дл€ хранени€ уровн€ доступа текущего пользовател€
                    string query = "SELECT prava_idPrava FROM user WHERE login = @login";
                    MySqlCommand roleCommand = new MySqlCommand(query, connection);
                    using (roleCommand)
                    {
                        roleCommand.Parameters.AddWithValue("@login", textBox1.Text);
                        role = roleCommand.ExecuteScalar().ToString();
                    }

                    if (role == "1")
                    {
                        Form2 form2 = new Form2();
                        this.Hide();
                        form2.Show();

                    }
                    else if (role == "2")
                    {
                        Form3 form3 = new Form3();
                        
                        form3.HideButton3AndDataGridView1();

                        this.Hide();
                        form3.Show();
                    }
                    else if (role == "3")
                    {
                        Form3 form3 = new Form3();
                        this.Hide();
                        form3.Show();
                        form3.HideButton3AndDataGridView12();
                    }
                    else
                    {
                        MessageBox.Show("Ќевозможно определить уровень доступа пользовател€");
                    }
                }
                else
                {
                    MessageBox.Show("Ќеправильный логин или пароль");
                }

                connection.Close();



            }







    }


}


    }









/*    if (count > 0)
            {
                Form2 form2 = new Form2();
                this.Hide();
                form2.Show();

            }
          else
          {
                MessageBox.Show("Ќеправильный логин или пароль");
            }
*/

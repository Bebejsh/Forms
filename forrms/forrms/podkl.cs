using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace forrms
{
    internal class podkl
    {
MySqlConnection connection = new MySqlConnection("server=192.168.0.89; port=3306; username= dpr2214;password= dpr2214;database= dpr2214_form");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            { connection.Open();
            }
        }


        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

        }

        public MySqlConnection GetConnection()
        {

            return connection;
        }





    }




}




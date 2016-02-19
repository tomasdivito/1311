using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace REST_magic1311.Models
{
    public class Db_Validator
    {
        private MySqlConnection connection;
        private bool connection_open;
        private MailSender mailSender;

        //Method to check if the serial is already in Use
        public string SerialInUse(string serial)
        {
            GetConnection();

            bool result = false;

            string commandText = "SELECT * FROM  `seriales`  WHERE  `Serial` = @SERIAL";

            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@SERIAL", MySqlDbType.String);
            cmd.Parameters["@SERIAL"].Value = serial;
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int estado = Convert.ToInt32(reader.GetValue(2));
                    if (estado == 0)
                    {
                        result = false;
                    }
                    else if (estado == 1)
                    {
                        result = true;
                    }
                }
                else
                {
                    return "'" + serial + "'" + " No es un serial válido";
                }
                reader.Close();
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
            }

            return result.ToString();
        }

        internal List<SerialModel> GetSerialesFromUser(string name)
        {
            GetConnection();
            List<SerialModel> seriales = new List<SerialModel>();
            string commandText = "SELECT* FROM `seriales` WHERE `Usuario` = @USER";
            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@USER", MySqlDbType.String);
            cmd.Parameters["@USER"].Value = name;

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SerialModel s = new SerialModel();
                    s.Serial = Convert.ToString(reader.GetValue(0));
                    s.AppID = Convert.ToString(reader.GetValue(1));
                    s.Usado = Convert.ToString(reader.GetValue(2));
                    s.Registrado = Convert.ToString(reader.GetValue(3));
                    s.Usuario = Convert.ToString(reader.GetValue(4));
                    seriales.Add(s);
                }

                reader.Close();
                return seriales;
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
                return null;
            }
        }

        internal void RegisterSerial(string serial, string user)
        {
            GetConnection();

            string commandText = "UPDATE `db_9f2d65_sriales`.`seriales` SET `Registrado` = '1', `Usuario` = @USER, `Available` = '0' WHERE `seriales`.`Serial` = @SERIAL;";

            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@USER", MySqlDbType.String);
            cmd.Parameters.Add("@SERIAL", MySqlDbType.String);
            cmd.Parameters["@USER"].Value = user;
            cmd.Parameters["@SERIAL"].Value = serial;

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                }

                reader.Close();
                //--------------------------------------------------------------
                //HERE METHOD TO SEND MAIL TO ME WHEN SOMEBODY REGISTER A PRODUCT
                //--------------------------------------------------------------
                mailSender = new MailSender();
                MailModel mm = new MailModel();
                mm.Subject = "NEW SERIAL REGISTERED";
                mm.Content = "The user " + user + " activated the serial " + serial;
                mailSender.SendMail(mm);
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
            }
        }

        //Check if the serial is from the game is calling by
        public bool SerialCorrectAppID(string serial, string appID)
        {
            GetConnection();

            bool result = false;

            string commandText = "SELECT * FROM  `seriales`  WHERE  `Serial` = @SERIAL";

            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@SERIAL", MySqlDbType.String);
            cmd.Parameters["@SERIAL"].Value = serial;

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = Convert.ToString(reader.GetValue(1));
                    if (id == appID)
                    {
                        result = true;
                    }
                }

                reader.Close();
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
            }

            return result;
        }

        //Method to check if the server has an owner
        public bool RegisteredSerial(string serial)
        {
            GetConnection();

            string commandText = "SELECT * FROM `seriales` WHERE `Serial` = @SERIAL";
            bool result = false;

            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@SERIAL", MySqlDbType.String);
            cmd.Parameters["@SERIAL"].Value = serial;

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int estado = Convert.ToInt32(reader.GetValue(3));
                    if (estado == 0)
                    {
                        result = false;
                    }
                    else if (estado == 1)
                    {
                        result = true;
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
            }

            return result;
        }

        //Method that update the database to activate that serial
        public string Activate(string serial)
        {
            GetConnection();
            string commandText = "UPDATE  `db_9f2d65_sriales`.`seriales` SET  `Usado` =  '1' WHERE  `seriales`.`Serial` =  @SERIAL;";

            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@SERIAL", MySqlDbType.String);
            cmd.Parameters["@SERIAL"].Value = serial;

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                }

                reader.Close();

                //--------------------------------------------------------------
                //HERE METHOD TO SEND MAIL TO ME WHEN SOMEBODY REGISTER A PRODUCT
                //--------------------------------------------------------------
                mailSender = new MailSender();
                MailModel mm = new MailModel();
                mm.Subject = "NEW SERIAL USED IN APPLICATION";
                mm.Content = "The serial: " + serial + " was used on an application successfully";
                mailSender.SendMail(mm);
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
            }

            if (SerialInUse(serial) == "True")
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }

        public void GetConnection()
        {
            connection_open = false;

            connection = new MySqlConnection();
            //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

            //            if (db_manage_connnection.DB_Connect.OpenTheConnection(connection))
            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {
                //					MessageBox::Show("No database connection connection made...\n Exiting now", "Database Connection Error");
                //					 Application::Exit();
            }

        }

        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

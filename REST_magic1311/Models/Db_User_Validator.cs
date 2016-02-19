using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleCrypto;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace REST_magic1311.Models
{
    public class Db_User_Validator
    {
        bool connection_open;
        PBKDF2 pb;
        MySqlConnection connection;
        MailSender mailSender;

        internal UserModel GetUser(string email)
        {
            GetConnection();

            string commandText = "SELECT * FROM `users` WHERE `Email` = @EMAIL";
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@EMAIL", MySqlDbType.String);
            cmd.Parameters["@EMAIL"].Value = email;
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserModel um = new UserModel();

                    um.Email = email;
                    um.Password = Convert.ToString(reader.GetValue(1));
                    um.PasswordSalt = Convert.ToString(reader.GetValue(2));
                    um.Name = Convert.ToString(reader.GetValue(3));
                    um.Lastname = Convert.ToString(reader.GetValue(4));
                    um.Country = Convert.ToString(reader.GetValue(5));
                    um.Facebook = Convert.ToString(reader.GetValue(6));
                    um.Twitter = Convert.ToString(reader.GetValue(7));
                    um.Linkedin = Convert.ToString(reader.GetValue(8));
                    reader.Close();
                    return um;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
                return null;
            }
        }

        internal bool UserAlreadyCreated(UserModel user)
        {
            bool duplicated = false;
            UserModel duplicate = GetUser(user.Email);
            if (duplicate != null)
            {
                duplicated = true;
            }
            return duplicated;
        }

        internal bool CreateNewUser(UserModel user)
        {
            GetConnection();
            bool duplicated = UserAlreadyCreated(user);

            if (duplicated == false)
            {
                string commandText = "INSERT INTO `db_9f2d65_sriales`.`users` (`Email`, `Password`, `PasswordSalt`, `Name`, `Lastname`, `Country`, `Facebook`, `Twitter`, `Linkedin`) VALUES (@EMAIL, @PASSWORD, @PASSWORDSALT, @NAME, @LASTNAME, @COUNTRY, @FACEBOOK, @TWITTER, @LINKEDIN);";

                //using parameters so we can't get our sql injected via input
                MySqlCommand cmd = new MySqlCommand(commandText, connection);
                cmd.Parameters.Add("@EMAIL", MySqlDbType.String);
                cmd.Parameters.Add("@PASSWORD", MySqlDbType.String);
                cmd.Parameters.Add("@PASSWORDSALT", MySqlDbType.String);
                cmd.Parameters.Add("@NAME", MySqlDbType.String);
                cmd.Parameters.Add("@LASTNAME", MySqlDbType.String);
                cmd.Parameters.Add("@COUNTRY", MySqlDbType.String);
                cmd.Parameters.Add("@FACEBOOK", MySqlDbType.String);
                cmd.Parameters.Add("@TWITTER", MySqlDbType.String);
                cmd.Parameters.Add("@LINKEDIN", MySqlDbType.String);

                pb = new PBKDF2();
                string encrpPass = pb.Compute(user.Password);

                cmd.Parameters["@EMAIL"].Value = user.Email;
                cmd.Parameters["@PASSWORD"].Value = encrpPass;
                cmd.Parameters["@PASSWORDSALT"].Value = pb.Salt;
                cmd.Parameters["@NAME"].Value = user.Name;
                cmd.Parameters["@LASTNAME"].Value = user.Lastname;
                cmd.Parameters["@COUNTRY"].Value = user.Country;
                cmd.Parameters["@FACEBOOK"].Value = user.Facebook;
                cmd.Parameters["@TWITTER"].Value = user.Twitter;
                cmd.Parameters["@LINKEDIN"].Value = user.Linkedin;
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
                    mm.Subject = "NEW USER REGISTERED";
                    mm.Content = "The user " + user.Email + " has been registered on the site";
                    mailSender.SendMail(mm);
                }
                catch (InvalidOperationException e)
                {
                    //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
                }
                return true;
            }
            return false;
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
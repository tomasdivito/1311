using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace REST_magic1311.Models
{
    public class Db_SoftwareActivation
    {
        bool connection_open;
        MySqlConnection connection;
        MailSender mailSender;
        Db_User_Validator db_uv;

        public string ActivateSoftware(string email, string appID)
        {
            UserModel usr = new UserModel();
            usr.Email = email;
            db_uv = new Db_User_Validator();
            if (db_uv.UserAlreadyCreated(usr))
            {
                GetConnection();
                string commandText = "INSERT INTO `db_9f2d65_sriales`.`activated_software` (`Software`, `Owner`) VALUES (@APPID, @EMAIL);";

                //using parameters so we can't get our sql injected via input
                MySqlCommand cmd = new MySqlCommand(commandText, connection);
                cmd.Parameters.Add("@EMAIL", MySqlDbType.String);
                cmd.Parameters.Add("@APPID", MySqlDbType.String);

                cmd.Parameters["@EMAIL"].Value = email;
                cmd.Parameters["@APPID"].Value = appID;

                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                    }
                    reader.Close();
                    //CHEQUEAMOS SI HAY 2 PRODUCTOS DE LA TRILOGIA ACTIVADOS
                    string res = PromoAchieved(email);
                    if (res != "")
                    {
                        if (res == "ESP")
                        {
                            //--------------------------------------------------------------
                            //HERE METHOD TO SEND MAIL TO ME WHEN SOMEBODY REGISTER A PRODUCT
                            //--------------------------------------------------------------
                            mailSender = new MailSender();
                            MailModel mm = new MailModel();
                            mm.Subject = "JUEGO REGISTRADO (playstore)";
                            mm.Content = "The user " + email + " registered a software " + appID + " and should receive a " + res + "App for free";
                            mailSender.SendMail(mm);

                            return "Producto activado con promoción conseguida (ESP)";
                        }
                        else if(res == "TRA")
                        {
                            //--------------------------------------------------------------
                            //HERE METHOD TO SEND MAIL TO ME WHEN SOMEBODY REGISTER A PRODUCT
                            //--------------------------------------------------------------
                            mailSender = new MailSender();
                            MailModel mm = new MailModel();
                            mm.Subject = "JUEGO REGISTRADO (playstore)";
                            mm.Content = "The user " + email + " registered a software " + appID + " and should receive a " + res + "App for free";
                            mailSender.SendMail(mm);

                            return "Producto activado con promoción conseguida (TRA)";
                        }
                        else if(res == "PET")
                        {
                            //--------------------------------------------------------------
                            //HERE METHOD TO SEND MAIL TO ME WHEN SOMEBODY REGISTER A PRODUCT
                            //--------------------------------------------------------------
                            mailSender = new MailSender();
                            MailModel mm = new MailModel();
                            mm.Subject = "JUEGO REGISTRADO (playstore)";
                            mm.Content = "The user " + email + " registered a software " + appID + " and should receive a " + res + "App for free";
                            mailSender.SendMail(mm);

                            return "Producto activado con promoción conseguida (PET)";
                        }
                    }
                    else
                    {

                        //--------------------------------------------------------------
                        //HERE METHOD TO SEND MAIL TO ME WHEN SOMEBODY REGISTER A PRODUCT
                        //--------------------------------------------------------------
                        mailSender = new MailSender();
                        MailModel mm = new MailModel();
                        mm.Subject = "JUEGO REGISTRADO (playstore)";
                        mm.Content = "The user " + email + " registered a software " + appID;
                        mailSender.SendMail(mm);

                        //Producto Activado con éxito pero no tuvo promoción
                        return "Producto activado";
                    }
                    //Producto Activado con éxito
                    return "Producto " + appID + "activado";
                }
                catch (InvalidOperationException e)
                {
                    //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
                    return "Error al activar " + "appID";
                }
            }
            else
            {
                return "Email incorrecto o no registrado";
            }
        }

        //CON ESTO VEMOS SI LA PERSONA TIENE 2 PRODUCTOS DE LA TRILOGÍA ACTIVADOS
        public string PromoAchieved(string email)
        {
            List<string> productosActivados = new List<string>();
            GetConnection();
            string commandText = "SELECT * FROM `activated_software` WHERE `Owner` = @EMAIL";
            //using parameters so we can't get our sql injected via input
            MySqlCommand cmd = new MySqlCommand(commandText, connection);
            cmd.Parameters.Add("@EMAIL", MySqlDbType.String);
            cmd.Parameters["@EMAIL"].Value = email;
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserModel um = new UserModel();
                    string producto = Convert.ToString(reader.GetValue(0));
                    productosActivados.Add(producto);
                }
                    reader.Close();
            }
            catch (InvalidOperationException e)
            {
                //ERROR MULTIPLES INTENTO DE CONEXION AL MISMO TIEMPO¿?
                return "";
            }

            if(productosActivados.Count < 2)
            {
                return "";
            }
            else
            {
                if(productosActivados.Contains("ESP") && productosActivados.Contains("PET") && !productosActivados.Contains("TRA"))
                {
                    return "TRA";
                }
                else if (productosActivados.Contains("ESP") && productosActivados.Contains("TRA") && !productosActivados.Contains("PET"))
                {
                    return "PET";
                }
                else if (productosActivados.Contains("TRA") && productosActivados.Contains("PET") && !productosActivados.Contains("ESP"))
                {
                    return "ESP";
                }
                else
                {
                    return "";
                }
            }
        }
        //
        //     Data base connection methods
        //
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

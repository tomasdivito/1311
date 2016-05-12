using MySql.Data.MySqlClient;
using REST_magic1311.Models;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_magic1311.Source
{
    public class Db_CardGame
    {
        bool connection_open;
        PBKDF2 pb;
        MySqlConnection connection;
        MailSender mailSender;
    }
}
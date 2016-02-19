using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_magic1311.Models
{
    public class User
    {
        private string mail;
        private string password;
        private string name;
        private string lastname;
        private string country;
        private string facebook;
        private string twitter;
        private string linkedin;

        public User (string mail, string password, string name, string lastname, string country, string facebook, string twitter, string linkedin)
        {
            this.mail = mail;
            this.password = password;
            this.name = name;
            this.lastname = lastname;
            this.country = country;
            this.facebook = facebook;
            this.twitter = twitter;
            this.linkedin = linkedin;
        }

        //Propertires of our user
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string PasswordSalt { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public string Facebook
        {
            get { return facebook; }
            set { facebook = value; }
        }

        public string Twitter
        {
            get { return twitter; }
            set { twitter = value; }
        }

        public string Linkedin
        {
            get { return linkedin; }
            set { linkedin = value; }
        }

        public static object Identity { get; internal set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    [Serializable]
    public class User
    {
        private string first_name;
        private string last_name;
        private string email;
        private int id;


        public User( int id ,string first_name, string last_name, string email)
        {
           
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.id = id;
        }

        public string FirstName
        {
            get { return this.first_name; }
        }
        public string LastName
        {
            get { return this.last_name; }
        }
        public string Email
        {
            get { return this.email; }
        }
        public int ID
        {
            get
            {
                return this.id;
            }
        }

        public string getFullName()
        {
            return this.first_name + " " + this.last_name;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp.Logic
{
    [Serializable]
    public class Admin:User
    {
        Role role;
        private string username;
        private string password;

        public Admin(int id, string firstname, string lastname, string email, string username, string password,string role) : base(id,firstname, lastname, email)
        {
            this.username = username;
            this.password = password;
            Enum.TryParse( role, out this.role);

        }
        public Role Role
        {
            get { return this.role; }
        }

        public string Username
        {
            get { return this.username; }
        }

        public string Password
        {
            get { return this.password; }
        }
    }

}


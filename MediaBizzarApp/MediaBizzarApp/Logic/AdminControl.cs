using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp.Logic
{
    public class AdminControl
    {
        private List<Admin> admins;
        private Admin loggedUser;

        public AdminControl()
        {
            admins = new List<Admin>();
            loggedUser = null;
        }
        public Admin GetLoggedUser()
        {
            return loggedUser;
        }
        public void LogUser(Admin admin)
        {
            loggedUser = admin; 
        }
        public void addAdmiin(Admin admin)
        {
           this.admins.Add(admin);
        }
        public void emptyAdmins()
        {
            this.admins.Clear();
        }
        public Admin getAdminsById(int id)
        {
            foreach (Admin admin in getAdmins())
            {
                if(id == admin.ID)
                {
                    return admin;
                }
            }
            return null;
        }

        public Admin getAdminByUsername(string username)
        {
            foreach (Admin admin in getAdmins())
            {
                if (admin.Username == username )
                {
                    return admin;
                }

            }
            return null;
        }
        public List <Admin> getAdmins()
        {
            return this.admins;
        }

    }
}

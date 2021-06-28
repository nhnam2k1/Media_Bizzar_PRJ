using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaBizzarApp.Logic;
using MySql.Data.MySqlClient;

namespace MediaBizzarApp
{
    public partial class LoginForm : Form
    {
        private Logic.AdminControl a;
        private AdminDAL adminDAL;
        public LoginForm()
        {
            a = new Logic.AdminControl();
            InitializeComponent();
            adminDAL = new AdminDAL();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            userLogin();

        }
        private void userLogin()
        {
            string[] bindings = { this.tbUsername.Text, this.tbPassword.Text };
            MySqlDataReader user = adminDAL.Login(bindings);

            user.Read();

            //check if user exists if yeshow home pahe if no show error message

            if (user.HasRows)
            {
                Admin admin = new Admin(Convert.ToInt32(user["id"]), user["first_name"].ToString(), user["last_name"].ToString(), user["email"].ToString(), user["username"].ToString(), user["password"].ToString(), user["role_title"].ToString());

                a.LogUser(admin);
                MessageBox.Show("User successfully logged in");
                this.Hide();
                Dashboard f = new Dashboard();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("User credentials are wrong !");
            }

            adminDAL.CloseConnection(user);
        }



    }
}

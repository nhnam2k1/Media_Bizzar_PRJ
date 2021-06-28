using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaBizzarApp
{
    public partial class Dashboard : MetroFramework.Forms.MetroForm
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void btnUcEmployees_Click(object sender, EventArgs e)
        {
            this.mainPnl.Controls.Clear();
            EmployeeUC employeeUC = new EmployeeUC();
            this.mainPnl.Controls.Add(employeeUC);
            employeeUC.BringToFront();
        }
        private void btnUcSchedules_Click(object sender, EventArgs e)
        {
            this.mainPnl.Controls.Clear();
            UcMetroSchedule ucMetroSchedule = new UcMetroSchedule();
            this.mainPnl.Controls.Add(ucMetroSchedule);
            ucMetroSchedule.BringToFront();
        }
        private void btnUcProducts_Click(object sender, EventArgs e)
        {

        }
        private void btnUcDepartments_Click(object sender, EventArgs e)
        {

        }
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

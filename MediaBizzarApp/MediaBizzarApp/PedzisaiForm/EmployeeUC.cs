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
    public partial class EmployeeUC : UserControl
    {

        EmployeeControl employeeControl;
        EmployeeSQLModel EmployeeSQLModel;
        public EmployeeUC()
        {
            InitializeComponent();
            employeeControl = new EmployeeControl();
            EmployeeSQLModel = new EmployeeSQLModel();
            cbGender.DataSource = Enum.GetValues(typeof(Gender));
            cbContract.DataSource = Enum.GetValues(typeof(Contract));
            cbDepartment.DataSource = Enum.GetValues(typeof(Department));
            this.pnlEmployeeHome.Show();
            this.pnlAddUser.Hide();
            renderEmployeeTable();
        }
        private bool valuesAreEmptyEmployee()
        {
            if (this.tbFirstName.Text == "" || this.tbLastName.Text == "" ||
                this.tbEmail.Text == "" || this.tbAddress.Text == "" ||
                this.tbCity.Text == "" || this.tbCountry.Text == "" || this.tbPhoneNumber == null ||
                this.tbWage == null || this.cbGender == null || this.cbContract == null || this.cbDepartment == null)
            {
                return false;
            }
            return true;
        }

        private void renderEmployeeTable()
        {
            DataTable dtEmp = new DataTable();
            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(int));
            dtEmp.Columns.Add("First Name", typeof(string));
            dtEmp.Columns.Add("Last Name", typeof(string));
            dtEmp.Columns.Add("Email", typeof(string));
            dtEmp.Columns.Add("Address", typeof(string));
            dtEmp.Columns.Add("City", typeof(string));
            dtEmp.Columns.Add("Country", typeof(string));
            dtEmp.Columns.Add("Phone Number", typeof(string));
            dtEmp.Columns.Add("Wage", typeof(string));
            dtEmp.Columns.Add("Gender", typeof(string));
            dtEmp.Columns.Add("Contract", typeof(string));
            dtEmp.Columns.Add("Deparment", typeof(string));
            dtEmp.Columns.Add("User Name", typeof(string));
            dtEmp.Columns.Add("Password", typeof(string));

            foreach (Employee employee in EmployeeSQLModel.GetEmployeesFromDatabase())
            {

                dtEmp.Rows.Add(false, employee.ID, employee.FirstName, employee.LastName, employee.Email,
                    employee.Address, employee.City, employee.Country, employee.PhoneNumber, employee.Wage,
                    employee.Gender, employee.Contract, employee.Department, employee.UserName, employee.Password);

            }
            dtgEmployees.DataSource = dtEmp;
        }
        private void UpdateEmployee()
        {

            //To be changed

            try
            {

                string gender = Convert.ToString(this.cbGender.SelectedItem);
                int department_id = this.cbDepartment.SelectedIndex;
                department_id++;
                string increased_department_id = Convert.ToString(department_id);
                int contract_id = this.cbContract.SelectedIndex;
                contract_id++;
                string increased_contract_id = Convert.ToString(contract_id);
                string[] employeeData = {this.tbFirstName.Text,this.tbLastName.Text,this.tbAddress.Text,
                    this.tbCity.Text, this.tbCountry.Text,
                    this.tbPhoneNumber.Text,gender,this.tbEmail.Text,increased_department_id,increased_contract_id,
                    this.tbWage.Text,this.tbUsername.Text,this.tbPassword.Text};

                employeeControl.UpdateEmployee(employeeData);
                MessageBox.Show("You have succesfully update information for that employee!");
                renderEmployeeTable();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        


            private void DeleteEmployee()
            {

                try
                {
                    if (MessageBox.Show("Do you want to delete this employee?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bool found = false;
                        for (int i = 0; i < dtgEmployees.Rows.Count; ++i)
                        {

                            DataGridViewRow dataRow = dtgEmployees.Rows[i];
                            bool selectedUser = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());

                            if (dataRow.IsNewRow || !selectedUser)
                            {
                                continue;
                            }

                            string id = (dataRow.Cells["ID"].Value.ToString());
                            string[] getID = { id };
                            employeeControl.DeleteEmployee(getID);
                            found = true;
                        }

                        if (found)
                        {
                            MessageBox.Show("Employee/s have been succesfully deleted");
                        }

                        EmployeeSQLModel.GetEmployeesFromDatabase();
                        this.renderEmployeeTable();



                        if (!found)
                        {
                            MessageBox.Show("You need to tick the selected box to delete a employee");
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There's been an exception!");
                }
            }
        
            

        
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            pnlEmployeeHome.Hide();
            pnlAddUser.Show();

        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DeleteEmployee();
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            // to be changed

            for (int i = 0; i < dtgEmployees.Rows.Count; i++)
            {
                DataGridViewRow dataRow = dtgEmployees.Rows[i];

                bool selectedEmployee = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedEmployee)
                {
                    try
                    {

                        string employeeId = (dataRow.Cells["ID"].Value.ToString());
                        pnlAddUser.Show();
                        Employee employee = employeeControl.getEmployeeById(Convert.ToInt32(employeeId));
                        this.tbFirstName.Text = employee.FirstName;
                        this.tbLastName.Text = employee.LastName;
                        this.tbEmail.Text = employee.Email;
                        this.tbAddress.Text = employee.Address;
                        this.tbPhoneNumber.Text = employee.PhoneNumber;
                        this.tbCity.Text = employee.City;
                        this.tbCountry.Text = employee.Country;
                        this.tbWage.Text = employee.Wage.ToString();
                        this.cbDepartment.SelectedItem = Convert.ToString(employee.Department);
                        this.cbContract.SelectedItem = Convert.ToString(employee.Contract);
                        this.cbGender.SelectedItem = Convert.ToString(employee.Gender);
                        this.tbUsername.Text = employee.UserName;
                        this.tbPassword.Text = employee.Password;



                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    EmployeeSQLModel.GetEmployeesFromDatabase();
                    renderEmployeeTable();

                }
            }
        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            pnlAddUser.Hide();
            pnlEmployeeHome.Show();
            renderEmployeeTable();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!valuesAreEmptyEmployee())
            {
                MessageBox.Show("You can not create a user wihtout entering all the fields!");
            }
            else
            {
                string gender = this.cbGender.SelectedText;
                int contract_id = this.cbContract.SelectedIndex;
                contract_id++;
                string increased_contract_id = Convert.ToString(contract_id);
                int department_id = this.cbDepartment.SelectedIndex;
                department_id++;
                string increased_department_id = Convert.ToString(department_id);


                string[] employee_bindings = { this.tbFirstName.Text, this.tbLastName.Text, this.tbEmail.Text, this.tbAddress.Text, this.tbCity.Text, this.tbCountry.Text, this.tbPhoneNumber.Text, this.tbWage.Text, gender, increased_contract_id, increased_department_id, tbUsername.Text, tbPassword.Text };
                MessageBox.Show("User successfully created!");
                employeeControl.AddEmployee(employee_bindings);
                renderEmployeeTable();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEmployee();
            
        }
    }
}

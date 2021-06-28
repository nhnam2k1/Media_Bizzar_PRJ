using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace MediaBizzarApp
{
    public class EmployeeControl
    {
        private List<Employee> employees;
        private EmployeeDAL employeeDAL;

        public EmployeeControl()
        {
            employees = new List<Employee>();
            employeeDAL = new EmployeeDAL();
        }
        public List<Employee> GetEmployees()
        {
            return this.employees;
        }
        public void AddEmployee(string[] employee_bindings)
        {
            employeeDAL.Insert(employee_bindings);
        }
        public void DeleteEmployee(string[] employee_bindings)
        {
            employeeDAL.Delete(employee_bindings);
        }
        public void UpdateEmployee(string[] employee_binding)
        {
            employeeDAL.Update(employee_binding);
        }
        public Employee getEmployeeById(int id)
        {
            foreach (Employee employee in GetEmployees())
            {
                if (id == employee.ID)
                {
                    return employee;
                }
            }
            return null;
        }
        public Employee getEmployeeByEmail(string email)
        {
            foreach (Employee employee in GetEmployees())
            {
                if (email == employee.Email)
                {
                    return employee;
                }
            }
            return null;
        }
        public void emptyEmployees()
        {
            this.employees.Clear();
        }
        public int getEmployeeCount()
        {
            return this.employees.Count;
        }
        public void renderEmployeeTable()
        {
            DataTable dtEmp = new DataTable();
            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(int));
            dtEmp.Columns.Add("Username", typeof(string));
            dtEmp.Columns.Add("Password", typeof(string));
            dtEmp.Columns.Add("First Name", typeof(string));
            dtEmp.Columns.Add("Last Name", typeof(string));
            dtEmp.Columns.Add("Gender", typeof(string));
            dtEmp.Columns.Add("Phone", typeof(string));
            dtEmp.Columns.Add("Country", typeof(string));
            dtEmp.Columns.Add("City", typeof(string));
            dtEmp.Columns.Add("Adress", typeof(string));
            dtEmp.Columns.Add("Email", typeof(string));
            dtEmp.Columns.Add("Deparment", typeof(string));
            dtEmp.Columns.Add("Contract", typeof(string));
            dtEmp.Columns.Add("Wage per hour", typeof(string));

            foreach (Employee employee in GetEmployees())
            {

                dtEmp.Rows.Add(false, employee.ID, employee.UserName, employee.Password, employee.FirstName,
                    employee.LastName, employee.Gender, employee.PhoneNumber, employee.Country, employee.City,
                    employee.Address, employee.Email, employee.Department, employee.Contract, employee.Wage);

            }
        }
    }
}

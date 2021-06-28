using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace MediaBizzarApp
{
    class EmployeeSQLModel
    {
        private SQLConnection sqlConnection;
        private string connectionString;
        private string employeeTable;
        private string contractTable;

        public EmployeeSQLModel()
        {
            sqlConnection = new SQLConnection();
            connectionString = sqlConnection.ConnectionString;

            employeeTable = "employee";
            contractTable = "contract";
        }
        public Employee[] GetEmployeesFromDatabase()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string sql = $"SELECT Employee.first_name, Employee.last_name,Employee.email, Employee.address, Employee.city, " +
                                 $"Employee.country, Employee.phone_number, Employee.wage, Employee.gender, " +
                                 $"Contract.type, Employee.department_id, Employee.id, Employee.fte " +
                                 $"FROM ({employeeTable} AS Employee " +
                                 $"INNER JOIN {contractTable} AS Contract ON Employee.contract_id = Contract.id);";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    connection.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    List<Employee> employees = new List<Employee>();

                    while (dr.Read())
                    {
                        string firstName = dr[0].ToString();
                        string lastName = dr[1].ToString();
                        string email = dr[2].ToString();
                        string address = dr[3].ToString();
                        string city = dr[4].ToString();
                        string country = dr[5].ToString();
                        string phone_number = dr[6].ToString();
                        string wage = dr[7].ToString();
                        string gender = dr[8].ToString();
                        string contract = dr[9].ToString();
                        string department = dr[10].ToString();
                        int id = Convert.ToInt32(dr[11]);
                        double fte = Convert.ToDouble(dr[12]);
                        
                        Employee employee = new Employee(id, firstName, lastName, email, address, city, country, phone_number, wage,
                                                         gender, contract, department, "", "", "", "", fte);

                        employees.Add(employee);
                    }
                    connection.Close();

                    return employees.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void TestScheduleSQL()
        {
            try
            {
                string employeeScheduleTable = "employee_Schedule";
                string employeeTable = "employee";
                string scheduleTable = "schedule";
                string sessionTable = "session";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string sql =   $"SELECT * " +
                                   $"FROM ((({employeeScheduleTable} AS EmployeeSchedule " +
                                   $"INNER JOIN {scheduleTable} AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id) " +
                                   $"INNER JOIN {employeeTable} AS Employee ON EmployeeSchedule.employee_id = Employee.id) " +
                                   $"INNER JOIN {sessionTable} AS Session ON Schedule.session_id = Session.id); ";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    connection.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    List<Employee> employees = new List<Employee>();

                    while (dr.Read())
                    {
                        
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

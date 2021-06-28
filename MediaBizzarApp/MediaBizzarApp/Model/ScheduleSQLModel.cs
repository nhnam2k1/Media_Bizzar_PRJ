using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace MediaBizzarApp
{
    class ScheduleSQLModel : IScheduleModel
    {
        private SQLConnection sqlConnection;
        private string connectionString;

        private string employeeScheduleTable;
        private string employeeTable;
        private string scheduleTable;
        private string sessionTable;

        public ScheduleSQLModel()
        {
            sqlConnection = new SQLConnection();
            connectionString = sqlConnection.ConnectionString;

            employeeScheduleTable = "employee_Schedule";
            employeeTable = "employee";
            scheduleTable = "schedule";
            sessionTable = "session";

            CheckDateAndSessionInCurrentMonthAndNextMonth();
        }
        public int GetNumberOfEmployeeShiftsAssignedInRangeDays(Employee employee, DateTime startDate, DateTime endDate)
        {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                    string sql = $"SELECT COUNT(EmployeeSchedule.employee_id) " +
                                 $"FROM (({employeeScheduleTable} AS EmployeeSchedule " +
                                 $"INNER JOIN {scheduleTable} AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id) " +
                                 $"INNER JOIN {employeeTable} AS Employee ON EmployeeSchedule.employee_id = Employee.id) " +
                                 $"WHERE (EmployeeSchedule.employee_id = @employeeID) " +
                                 $"AND (Schedule.date BETWEEN @startDate AND @endDate);";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    string sqlFormatStartDate = startDate.ToString("yyyy-MM-dd"); // Fix bug SQL DateTime Format
                    string sqlFormatEndDate = endDate.ToString("yyyy-MM-dd");     // Fix bug SQL DateTime Format
                    cmd.Parameters.AddWithValue("@employeeID", employee.ID);
                    cmd.Parameters.AddWithValue("@startDate", sqlFormatStartDate);
                    cmd.Parameters.AddWithValue("@endDate", sqlFormatEndDate);

                    connection.Open();
                    object value = cmd.ExecuteScalar();
                    int result = Convert.ToInt32(value);
                    connection.Close();

                    return result;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int GetNumberOfShiftsInRangeDates(DateTime startDate, DateTime endDate)
        {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string sql = $"SELECT COUNT(EmployeeSchedule.employee_id) " +
                                 $"FROM (({employeeScheduleTable} AS EmployeeSchedule " +
                                 $"INNER JOIN {scheduleTable} AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id) " +
                                 $"INNER JOIN {employeeTable} AS Employee ON EmployeeSchedule.employee_id = Employee.id) " +
                                 $"WHERE Schedule.date BETWEEN @startDate AND @endDate;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    string sqlFormatStartDate = startDate.ToString("yyyy-MM-dd"); // Fix bug SQL DateTime Format
                    string sqlFormatEndDate = endDate.ToString("yyyy-MM-dd");     // Fix bug SQL DateTime Format
                    cmd.Parameters.AddWithValue("@startDate", sqlFormatStartDate);
                    cmd.Parameters.AddWithValue("@endDate", sqlFormatEndDate);

                    connection.Open();
                    object value = cmd.ExecuteScalar();
                    int result = Convert.ToInt32(value);
                    connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Shift GetShiftFromDatabase(DateTime date, Session session)
        {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    Shift shift = null;
                    string sql = $"SELECT Employee.first_name, Employee.last_name, Employee.address, Employee.city, " +
                                 $"Employee.country, Employee.phone_number, Employee.wage, Employee.department_id, " +
                                 $"Employee.gender, Employee.contract_id, Employee.email, Employee.id, Employee.fte " +
                                 $"FROM ((({employeeScheduleTable} AS EmployeeSchedule " +
                                 $"INNER JOIN {scheduleTable} AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id) " +
                                 $"INNER JOIN {employeeTable} AS Employee ON EmployeeSchedule.employee_id = Employee.id) " +
                                 $"INNER JOIN {sessionTable} AS Session ON Schedule.session_id = Session.id) " +
                                 $"WHERE Schedule.date = @date AND Session.time_of_day = @session " +
                                 $"ORDER BY Employee.id;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    string sqlFormatDate = date.ToString("yyyy-MM-dd"); // Fix bug SQL DateTime Format
                    cmd.Parameters.AddWithValue("@date", sqlFormatDate);
                    cmd.Parameters.AddWithValue("@session", session.ToString());

                    connection.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    List<Employee> employees = new List<Employee>();

                    while (dr.Read())
                    {
                        string firstName = dr[0].ToString();
                        string lastName = dr[1].ToString();
                        string address = dr[2].ToString();
                        string city = dr[3].ToString();
                        string country = dr[4].ToString();
                        string phoneNr = dr[5].ToString();
                        string wage = dr[6].ToString();
                        string department = dr[7].ToString();
                        string gender = dr[8].ToString();
                        string contract = dr[9].ToString();
                        string email = dr[10].ToString();
                        int id = Convert.ToInt32(dr[11]);
                        double fte = Convert.ToDouble(dr[12]);

                        Employee employee = new Employee(id, firstName, lastName, email, address, city, country, phoneNr, 
                                                         gender, department, contract, wage, "", "", "", "", fte);
                        employees.Add(employee);
                    }
                    shift = new Shift(date, session, employees.ToArray());
                    connection.Close();

                    return shift;
                }
            }
            catch(Exception ex) {
                throw ex;
            }
        }
        public void AddShiftToDatabase(Shift shift)
        {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                    int cnt = 0;
                    int employeesLength = shift.Employees.Length;
                    string employeeStr  = "";

                    foreach (Employee employee in shift.Employees) {
                        string employeeID = $"'{employee.ID.ToString()}'";
                        employeeStr = employeeStr + employeeID; cnt++;

                        if (cnt < employeesLength) {
                            employeeStr += ", ";
                        }
                    }
                    
                    string sql =  $"INSERT INTO {employeeScheduleTable} (schedule_id, employee_id) " +
                                  $"SELECT Schedule.id, Employee.ID " +
                                  $"FROM (({scheduleTable} AS Schedule " +
                                  $"INNER JOIN {sessionTable} AS Session ON Schedule.session_id = Session.id) " +
                                  $"CROSS JOIN (SELECT ID FROM {employeeTable} WHERE ID IN ({employeeStr})) AS Employee) " +
                                  $"WHERE Schedule.date = @date AND Session.time_of_day = @session;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    string sqlFormatDate = shift.Date.ToString("yyyy-MM-dd"); // Fix bug SQL DateTime Format
                    cmd.Parameters.AddWithValue("@date", sqlFormatDate);
                    cmd.Parameters.AddWithValue("@session", shift.Session.ToString());

                    connection.Open();
                    int row_affect = cmd.ExecuteNonQuery();
                    if (row_affect == 0)
                    {
                        throw new Exception("Something wrong with the system, cannot insert the records");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public void UpdateShiftToDatabase(Shift shift)
        {
            try {
                if (!(shift is UpdatedShift))
                {
                    throw new Exception("The parameter pass to the update function of the model is not UpdatedShift object");
                }
                using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                    string listOfUpdate = "";
                    string employeesStr = "";
                    UpdatedShift updateShift = (UpdatedShift)shift;
                    int length = updateShift.OldEmployees.Length;

                    for (int i = 0; i < length; i++) {
                        int oldEmployeeID = updateShift.OldEmployees[i].ID;
                        int newEmployeeID = updateShift.NewEmployees[i].ID;

                        string updateStatement = $"WHEN EmployeeSchedule.employee_id = {oldEmployeeID} THEN {newEmployeeID} ";
                        listOfUpdate += updateStatement;
                        employeesStr += $"{oldEmployeeID}";
                        if (i < length - 1) {
                            employeesStr += ",";
                        }
                    }

                    string sql = $"UPDATE (({employeeScheduleTable} AS EmployeeSchedule " +
                                 $"INNER JOIN {scheduleTable} AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id) " +
                                 $"INNER JOIN {sessionTable} AS Session ON Schedule.Session_id = Session.id) " +
                                 $"SET EmployeeSchedule.employee_id = CASE {listOfUpdate} ELSE EmployeeSchedule.employee_id END " +
                                 $"WHERE Schedule.date = @date AND Session.time_of_day = @session " +
                                 $"AND EmployeeSchedule.employee_id IN ({employeesStr});";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    string sqlFormatDate = shift.Date.ToString("yyyy-MM-dd"); // Fix bug SQL DateTime Format
                    cmd.Parameters.AddWithValue("@date", sqlFormatDate);
                    cmd.Parameters.AddWithValue("@session", shift.Session.ToString());

                    connection.Open();
                    int rowAffect = cmd.ExecuteNonQuery();
                    if (rowAffect == 0) {
                        throw new Exception("Something went wrong with the system, cannot update");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public void DeleteShiftFromDatabase(Shift shift)
        {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                    string employeeStr = "";
                    int cnt = 0;
                    foreach(Employee employee in shift.Employees) {
                        string employeeID = $"{employee.ID}";
                        employeeStr += employeeID;  cnt++;
                        if (cnt < shift.Employees.Length){
                            employeeStr += ",";
                        }
                    }

                    string sql =  $"DELETE EmployeeSchedule " +
                                  $"FROM (({employeeScheduleTable} AS EmployeeSchedule " +
                                  $"LEFT JOIN {scheduleTable} AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id) " +
                                  $"LEFT JOIN {sessionTable}  AS Session ON Schedule.session_id = Session.id) " +
                                  $"WHERE Schedule.date = @date AND Session.time_of_day = @session " +
                                  $"AND EmployeeSchedule.employee_id IN ({employeeStr});";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    string sqlFormatDate = shift.Date.ToString("yyyy-MM-dd"); // Fix bug SQL DateTime Format
                    cmd.Parameters.AddWithValue("@date", sqlFormatDate);
                    cmd.Parameters.AddWithValue("@session", shift.Session.ToString());

                    connection.Open();
                    int row_affect = cmd.ExecuteNonQuery();
                    if (row_affect == 0) {
                        throw new Exception("Something went wrong with the system, cannot delete the records");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        private void AddNewDateAndSessionToDatabase(DateSession[] dateSessions)
        {
            try {
                if (dateSessions == null)
                {
                    throw new Exception("The parameter should not be null");
                }
                if (dateSessions.Length == 0)
                {
                    throw new Exception("The parameters should not empty");
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string sql = $"INSERT INTO {scheduleTable} (date, session_id) VALUES";

                    int length = dateSessions.Length;
                    int seenItems = 0;
                    foreach (DateSession dateSession in dateSessions) {
                        string date = dateSession.Date.ToString("yyyy-MM-dd");
                        int session_id = (int)dateSession.Session;
                        string pair = $" ('{date}', '{session_id}')";
                        sql = sql + pair;
                        seenItems++;
                        if (seenItems == length)
                        {
                            sql = sql + ";";
                        }
                        else
                        {
                            sql = sql + ",";
                        }
                    }
                    
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    connection.Open();
                    int row_affect = cmd.ExecuteNonQuery();
                    if (row_affect == 0)
                    {
                        throw new Exception("Something wrong with the system, cannot add new date and session to database");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CheckDateAndSessionInCurrentMonthAndNextMonth()
        {
            try {
                List<DateSession> missingDateSessions = new List<DateSession>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string sql = $"SELECT Schedule.date, Schedule.session_id, Session.time_of_day " +
                                 $"FROM ({scheduleTable} AS Schedule " +
                                 $"INNER JOIN {sessionTable} AS Session ON Schedule.session_id = Session.id) " +
                                 $"WHERE Schedule.date BETWEEN @startDate AND @endDate " +
                                 $"ORDER BY Schedule.date, Schedule.Session_id;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    DateTime currentDate = DateTime.Now;
                    DateTime startDateThisMonth, endDateThisMonth;
                    DateTime startDateNextMonth, endDateNextMonth;
                    GetTheFirstDayAndLastDayInMonth(currentDate, out startDateThisMonth, out endDateThisMonth);
                    GetTheFirstDayAndLastDayInMonth(currentDate.AddMonths(1), out startDateNextMonth, out endDateNextMonth);

                    cmd.Parameters.AddWithValue("@startDate", startDateThisMonth.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@endDate", endDateNextMonth.ToString("yyyy-MM-dd"));

                    connection.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    HandleMissingDateSessionsInSchedule(ref dr, ref missingDateSessions, startDateThisMonth, endDateNextMonth);
                    connection.Close();
                }
                if (missingDateSessions.Count != 0)
                {
                    AddNewDateAndSessionToDatabase(missingDateSessions.ToArray());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void HandleMissingDateSessionsInSchedule(ref MySqlDataReader dr, ref List<DateSession> dateSessions, DateTime startDate, DateTime endDate)
        {
            HashSet<DateTime>[] checkDateSessionAvailable = new HashSet<DateTime>[3];
            dateSessions = new List<DateSession>();
            for (int session_id = 0; session_id < 3; session_id++)
            {
                checkDateSessionAvailable[session_id] = new HashSet<DateTime>();
            }

            while (dr.Read()) {
                DateTime date; 
                DateTime.TryParse(dr[0].ToString(), out date);
                int session_id = Convert.ToInt32(dr[1]) - 1;
                checkDateSessionAvailable[session_id].Add(date);
            }

            IEnumerable<DateTime> listDays = EachCalendarDay(startDate, endDate);
            foreach(DateTime date in listDays)
            {
                // Check if a tuple of date and session is missing
                for (int session_id = 0; session_id < 3; session_id++)
                {
                    if (!checkDateSessionAvailable[session_id].Contains(date))
                    {
                        Session session = (Session)(session_id+1);
                        dateSessions.Add(new DateSession(date, session));
                    }
                }
            }
        }
        private void GetTheFirstDayAndLastDayInMonth(DateTime date, out DateTime firstDate, out DateTime lastDate)
        {
            firstDate = new DateTime(date.Year, date.Month, 1);
            lastDate = firstDate.AddMonths(1).AddDays(-1);
        }
        private IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }
    }
}

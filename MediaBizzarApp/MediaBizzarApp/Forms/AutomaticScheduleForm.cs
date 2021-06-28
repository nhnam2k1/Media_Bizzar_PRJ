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
    public partial class AutomaticScheduleForm : Form
    {
        private ScheduleController scheduleController;
        private AutomaticSchedulingController automaticScheduling;
        private InputValidation inputValidation;
        private EmployeeSQLModel employeeModel;
        private List<Employee> employeesInDepartment;
        private List<Shift> generatingShifts;
        private Dictionary<string, int> limitEmployeesInShift;
        private Dictionary<string, List<Shift>> employeeShiftStates;
        private const int NUM_DAYS = 7;
        private bool shiftLimitViewLoaded = false;
        public AutomaticScheduleForm()
        {
            InitializeComponent();
            scheduleController = new ScheduleController();
            automaticScheduling = new AutomaticSchedulingController();
            employeeModel = new EmployeeSQLModel();
            inputValidation = new InputValidation();
            limitEmployeesInShift = new Dictionary<string, int>();
            employeeShiftStates = new Dictionary<string, List<Shift>>();

            employeesInDepartment = employeeModel.GetEmployeesFromDatabase().ToList();
            UpdateWeeksListView();
            PrintEmployeeListView();
            //cbxTimeLimit.SelectedIndex = 0;
            cbxWeeks.SelectedIndex = 0;

            InitializeShiftLimitGridView();
        }
        private void btnGenerating_Click(object sender, EventArgs e)
        {
            try
            {
                string date = cbxWeeks.Text;
                DateTime Date = Convert.ToDateTime(date);
                int timeLimit = 10;
                int[,] limitShifts = GetLimitEmployeeInShifts();
                Shift[] shifts = automaticScheduling.ProcessAutomaticScheduling(Date, employeesInDepartment.ToArray(), 
                                                                                limitShifts, timeLimit);
                generatingShifts = shifts.ToList();
                employeeShiftStates[date] = generatingShifts;
                PrintShiftsToListView(generatingShifts.ToArray());
                MessageBox.Show("Successful generating shifts");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int[,] GetLimitEmployeeInShifts() 
        {
            Session[] sessions = (Session[])Enum.GetValues(typeof(Session));
            int day = 0, sessionCnt = 0;
            int sessionLen = sessions.Length;
            int[,] result = new int[NUM_DAYS, sessionLen];
            
            foreach(DataGridViewRow row in dgvShiftsLimit.Rows)
            {
                result[day, sessionCnt] = Convert.ToInt32(row.Cells[2].Value);
                sessionCnt++;
                if (sessionCnt == sessionLen)
                {
                    sessionCnt = 0;
                    day++;
                }
            }
            return result;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = Convert.ToDateTime(cbxWeeks.Text);
                DateTime endDate = startDate.AddDays(6);

                if (scheduleController.CheckShiftsExistedInRangeDate(startDate, endDate))
                {
                    throw new Exception("There were shifts assigned, cannot add to the system");
                }
                scheduleController.WarningFteHasBeenCalled = true;
                foreach(Shift shift in generatingShifts)
                {
                    scheduleController.AddShift(shift);
                }
                MessageBox.Show("Successful adding shifts");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PrintShiftsToListView(Shift[] shifts)
        {
            lvShiftsView.Items.Clear();
            foreach(Shift shift in shifts)
            {
                string date = shift.Date.ToShortDateString();
                string session = shift.Session.ToString();
                bool hasPrinted = false;
                Employee[] employees = shift.Employees;

                foreach(Employee employee in employees)
                {
                    string fullname = $"{employee.FirstName} {employee.LastName}";
                    ListViewItem lvi;
                    if (!hasPrinted)
                    {
                        lvi = new ListViewItem(date);
                        lvi.SubItems.Add(session);
                        hasPrinted = true;
                    }
                    else
                    {
                        lvi = new ListViewItem(" ");
                        lvi.SubItems.Add(" ");
                    }
                    lvi.SubItems.Add(fullname);
                    lvi.Name = $"{date}_{session}_{employee.ID}";
                    lvShiftsView.Items.Add(lvi);
                }
            }
        }
        private void InitializeShiftLimitGridView()
        {
            dgvShiftsLimit.Rows.Clear();
            shiftLimitViewLoaded = false;
            DateTime current = Convert.ToDateTime(cbxWeeks.SelectedItem);
            for (int day = 0; day < NUM_DAYS; day++)
            {
                DateTime followingDate = current.AddDays(day);
                bool printedDate = false;
                foreach (Session session in Enum.GetValues(typeof(Session)))
                {
                    int rowID = dgvShiftsLimit.Rows.Add();
                    string shiftID = $"{followingDate.ToShortDateString()}_{session}";
                    if (!limitEmployeesInShift.ContainsKey(shiftID))
                    {
                        limitEmployeesInShift[shiftID] = employeesInDepartment.Count;
                    }

                    DataGridViewRow row = dgvShiftsLimit.Rows[rowID];
                    
                    row.Cells[0].Value = (!printedDate ? followingDate.ToShortDateString() : " ");
                    row.Cells[1].Value = session;
                    row.Cells[2].Value = limitEmployeesInShift[shiftID];
                    row.Tag = shiftID;
                    printedDate = true;
                }
            }
            shiftLimitViewLoaded = true;
        }
        private void dgvShiftsLimit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!shiftLimitViewLoaded) return;
                string edit = dgvShiftsLimit.SelectedRows[0].Cells[2].Value.ToString();
                string shiftID = dgvShiftsLimit.SelectedRows[0].Tag.ToString();
                int result = 0;
                if (!Int32.TryParse(edit, out result))
                {
                    throw new Exception("It is not in digit format");
                }
                limitEmployeesInShift[shiftID] = result;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                dgvShiftsLimit.SelectedRows[0].Cells[2].Value = employeesInDepartment.Count;
            }
        }
        private void PrintEmployeeListView()
        {
            lvEmployees.Items.Clear();
            foreach(Employee employee in employeesInDepartment)
            {
                string name = $"{employee.FirstName} {employee.LastName}";
                ListViewItem lvi = new ListViewItem(name);
                lvi.SubItems.Add(employee.ID.ToString());
                lvi.Name = employee.ID.ToString();
                lvEmployees.Items.Add(lvi);
            }
        }
        private void lvEmployees_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvEmployees.SelectedItems.Count == 0) return;
                if (lvShiftsView.SelectedItems.Count == 0)
                {
                    throw new Exception("You have not choose shifts for editing");
                }
                string name = lvEmployees.SelectedItems[0].Name;
                string shiftName = lvShiftsView.SelectedItems[0].Name;
                string[] shiftInfo = shiftName.Split('_');
                string date = cbxWeeks.Text;

                Employee newEmployee = employeesInDepartment.Find(x => x.ID.ToString() == name);
                Employee oldEmployee = employeesInDepartment.Find(x => x.ID.ToString() == shiftInfo[2]);
                Shift targetShift = CreateTargetShift(shiftInfo[0], shiftInfo[1]);
                Shift updatedShift = generatingShifts.Find(x => x.Equals(targetShift));

                ReplaceEmployeeInCurrentShift(updatedShift, oldEmployee, newEmployee);
                employeeShiftStates[date] = generatingShifts;
                PrintShiftsToListView(generatingShifts.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lvShiftsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvShiftsView.SelectedItems.Count == 0) return;
            string name = lvShiftsView.SelectedItems[0].Name;
            string[] arr = name.Split('_');
            Employee employee = employeesInDepartment.Find(x => x.ID.ToString() == arr[2]);

            lblCurrentFTE.Text = CalculateCurrentEmployeeFTE(employee, generatingShifts.ToArray()).ToString();
            lblEmployeeName.Text = $"{employee.FirstName} {employee.LastName}";
            lblFTE.Text = employee.FTE.ToString();
        }
        private double CalculateCurrentEmployeeFTE(Employee employee, Shift[] shifts)
        {
            double result = 0;
            int cnt = 0;
            foreach(Shift shift in shifts)
            {
                Employee[] employees = shift.Employees;
                bool find = employees.Contains(employee);
                if (find) cnt++;
            }
            result = cnt * 0.1;
            return result;
        }
        private Shift CreateTargetShift(string date, string session)
        {
            DateTime time = Convert.ToDateTime(date);
            Session ses = (Session)Enum.Parse(typeof(Session), session);
            Shift shift = new Shift(time, ses, null);
            return shift;
        }
        private void ReplaceEmployeeInCurrentShift(Shift shift, Employee oldEmployee, Employee newEmployee)
        {
            List<Employee> employees = shift.Employees.ToList();
            employees.Remove(oldEmployee);
            employees.Add(newEmployee);
            DateTime date = shift.Date;
            Session session = shift.Session;
            generatingShifts.Remove(shift);
            generatingShifts.Add(new Shift(date, session, employees.ToArray()));
            generatingShifts.Sort();
        }
        private void UpdateWeeksListView()
        {
            cbxWeeks.Items.Clear();
            DateTime monday = automaticScheduling.GetNextWeekday(DateTime.Now, DayOfWeek.Monday);
            for(int week = 0; week < 4; week++)
            {
                DateTime thisWeek = monday.AddDays(week * 7);
                cbxWeeks.Items.Add(thisWeek.ToShortDateString());
            }
        }
        private void cbxWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string date = cbxWeeks.Text;
            InitializeShiftLimitGridView();

            if (!employeeShiftStates.ContainsKey(date))
            {
                employeeShiftStates[date] = new List<Shift>();
            }
            generatingShifts = employeeShiftStates[date];
            PrintShiftsToListView(generatingShifts.ToArray());
        }
    }
}

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
    public partial class EditScheduleForm : Form
    {
        private Employee[] currentEmployeesInDepartment;
        private ScheduleController scheduleController;
        private Dictionary<string, EditScheduleUtility> editScheduleUtilities;
        private Dictionary<int, List<bool>> employeeShiftStates;
        private List<Shift> initializeShifts;
        private InputValidation inputValidation;
        private const int RANGE_DAYS = 7;
        DateTime currentDate;

        public EditScheduleForm(Employee[] currentEmployeesInDepartment, Shift currentShift)
        {
            this.currentEmployeesInDepartment = currentEmployeesInDepartment;
            initializeShifts = new List<Shift>();
            employeeShiftStates = new Dictionary<int, List<bool>>();
            scheduleController = new ScheduleController();
            inputValidation = new InputValidation();

            currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            InitializeComponent();
            InitializeEditScheduleUltilities();

            IntializeShiftSelectionView();
            UpdateCurrentDepartmentEmployeeListView(currentEmployeesInDepartment);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IterateSelectionOfEmployeesChoice();
                for (int i = 0; i <= RANGE_DAYS; i++)
                {
                    DateTime followingDate = currentDate.AddDays(i);
                    foreach (Session session in Enum.GetValues(typeof(Session)))
                    {
                        ProcessTheFinalizedEmployeeShiftUpdate(followingDate, session);
                        ProcessTheUpdateOfEmployeesInShift(followingDate, session);
                        ProcessTheRemoveOfEmployeesInShift(followingDate, session);
                        ProcessTheAddOfEmployeesInShift(followingDate, session);
                    }
                }
                MessageBox.Show("Successful update schedule");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ProcessTheFinalizedEmployeeShiftUpdate(DateTime date, Session session)
        {
            string key = ConvertDateSessionToString(date, session);
            editScheduleUtilities[key].FinalizedEmployeeUpdate();
        }
        private void ProcessTheUpdateOfEmployeesInShift(DateTime date, Session session)
        {
            string key = ConvertDateSessionToString(date, session);
            UpdatedShift updatedShift = editScheduleUtilities[key].GetTheListOfUpdatedEmployees();
            if (updatedShift == null) { return; }
            scheduleController.UpdateShift(updatedShift);
        }
        private void ProcessTheRemoveOfEmployeesInShift(DateTime date, Session session)
        {
            string key = ConvertDateSessionToString(date, session);
            Shift shift = editScheduleUtilities[key].GetTheListOfRemovedEmployees();
            if (shift == null) { return; }
            scheduleController.DeleteShift(shift);
        }
        private void ProcessTheAddOfEmployeesInShift(DateTime date, Session session)
        {
            string key = ConvertDateSessionToString(date, session);
            Shift shift = editScheduleUtilities[key].GetTheListOfAddedEmployees();
            if (shift == null) { return; }
            scheduleController.WarningFteHasBeenCalled = false;
            scheduleController.AddShift(shift);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbxDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lvAvailableEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lvAvailableEmployees.SelectedIndices.Count == 0)
            //{
            //    lvCurrentEmployeeShifts.Items.Clear();
            //    return;
            //}
            //int selectedIndex = lvAvailableEmployees.SelectedIndices[0];
            //Employee employee = currentEmployeesInDepartment[selectedIndex];
            //InitializeEmployeeShiftStates(employee);
            //GeneratingEmployeeShiftView(employee);
        }
        private void InitializeEmployeeShiftStates(Employee employee)
        {
            //if (!employeeShiftStates.ContainsKey(employee.ID))
            //{
            //    employeeShiftStates[employee.ID] = new List<bool>();
            //    for (int i = 0; i <= RANGE_DAYS; i++)
            //    {
            //        DateTime followingDate = currentDate.AddDays(i);
            //        foreach(Session session in Enum.GetValues(typeof(Session)))
            //        {
            //            Shift lookupShift = new Shift(followingDate, session, null);
            //            Shift shift = initializeShifts.Find(x => x.Equals(lookupShift));
            //            List<Employee> employees = shift.Employees.ToList();
            //            employeeShiftStates[employee.ID].Add(employees.Contains(employee));
            //        }
            //    }
            //}
        }
        private void IterateSelectionOfEmployeesChoice()
        {
            try
            {
                List<Employee> employees = new List<Employee>(currentEmployeesInDepartment);
                foreach (ListViewItem lvi in lvAvailableEmployees.SelectedItems)
                {
                    string name = lvi.Name;
                    Employee employee = employees.Find(x => x.ID.ToString() == name);
                    foreach(ListViewItem lvShift in lvCurrentEmployeeShifts.Items)
                    {
                        if (lvShift.Checked)
                        {
                            string shiftName = lvShift.Name;
                            editScheduleUtilities[shiftName].KeepTrackAddEmployee(employee);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateCurrentDepartmentEmployeeListView(Employee[] employees)
        {
            lvAvailableEmployees.Items.Clear();

            if (employees == null)
            {
                return;
            }

            foreach (Employee employee in employees)
            {
                string fullname = $"{employee.FirstName} {employee.LastName}";
                ListViewItem lvi = new ListViewItem(employee.ID.ToString());
                lvi.SubItems.Add(fullname);
                lvi.Name = $"{employee.ID}";
                lvAvailableEmployees.Items.Add(lvi);
            }
        }
        private void InitializeEditScheduleUltilities()
        {
            editScheduleUtilities = new Dictionary<string, EditScheduleUtility>();
            for (int i = 0; i <= RANGE_DAYS; i++)
            {
                DateTime followingDate = currentDate.AddDays(i);
                foreach (Session session in Enum.GetValues(typeof(Session)))
                {
                    string key = ConvertDateSessionToString(followingDate, session);
                    editScheduleUtilities[key] = new EditScheduleUtility(followingDate, session);
                    Shift shift = scheduleController.GetShift(followingDate, session);
                    initializeShifts.Add(shift);
                }
            }
        }
        private string ConvertDateSessionToString(DateTime date, Session session)
        {
            string result = $"{date.ToShortDateString()}_{session}";
            return result;
        }
        private void ConvertStringToDateSession(string name, out DateTime date, out Session session)
        {
            string[] ans = name.Split('_');
            date = Convert.ToDateTime(ans[0]);
            session = (Session)Enum.Parse(typeof(Session), ans[1]);
        }
        private void lvCurrentEmployeeShifts_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                List<Employee> filters = new List<Employee>();
                DateTime date = DateTime.Now;
                Session session = Session.MORNING;
                bool isEmpty = true;
                foreach (ListViewItem lvi in lvCurrentEmployeeShifts.Items)
                {
                    if (lvi.Checked)
                    {
                        string name = lvi.Name;
                        isEmpty = false;
                        ConvertStringToDateSession(name, out date, out session);

                        foreach (Shift shift in initializeShifts)
                        {
                            if (shift.Date == date && shift.Session == session)
                            {
                                List<Employee> employeesInShift = shift.Employees.ToList();

                                foreach (Employee employee in currentEmployeesInDepartment)
                                {
                                    if (!employeesInShift.Contains(employee))
                                    {
                                        if (!filters.Contains(employee))
                                        {
                                            filters.Add(employee);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (isEmpty) UpdateCurrentDepartmentEmployeeListView(currentEmployeesInDepartment.ToArray());
                else UpdateCurrentDepartmentEmployeeListView(filters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void lvCurrentEmployeeShifts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }
        private void IntializeShiftSelectionView()
        {
            for (int i = 0; i <= RANGE_DAYS; i++)
            {
                DateTime followingDate = currentDate.AddDays(i);
                foreach (Session session in Enum.GetValues(typeof(Session)))
                {
                    ListViewItem lvi = new ListViewItem(followingDate.DayOfWeek.ToString());
                    lvi.SubItems.Add(followingDate.ToShortDateString());
                    lvi.SubItems.Add(session.ToString());
                    lvi.Name = ConvertDateSessionToString(followingDate, session);
                    lvCurrentEmployeeShifts.Items.Add(lvi);
                }
            }
        }
    }
}

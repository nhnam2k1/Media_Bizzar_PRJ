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
    public partial class UcMetroSchedule : MetroFramework.Controls.MetroUserControl
    {
        private EditScheduleForm editScheduleForm;
        private AutomaticScheduleForm automaticScheduleForm;
        private ScheduleController scheduleController;
        private ScheduleUtility scheduleUtility;
        private List<Employee> employeesInDepartment;
        private List<Shift> currentShifts;

        private EmployeeSQLModel employeeModel = new EmployeeSQLModel();
        public UcMetroSchedule()
        {
            scheduleController = new ScheduleController();
            scheduleUtility = new ScheduleUtility();
            employeesInDepartment = new List<Employee>();
            currentShifts = new List<Shift>();
            try
            {
                InitializeComponent();
                scheduleUtility.AutoAssigningShiftsFollowingWeek(ref scheduleController);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                InitializeTheComponents();
                EmployeesExample();
            }
        }
        private void btnCreateAutomaticSchedule_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEditShift_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = calSchedule.SelectionStart;
                Session session = (Session)(cbxSession.SelectedIndex + 1);
                Shift shift = scheduleController.GetShift(currentDate, session);

                editScheduleForm = new EditScheduleForm(employeesInDepartment.ToArray(), shift);
                editScheduleForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGeneratingShifts_Click(object sender, EventArgs e)
        {
            try
            {
                automaticScheduleForm = new AutomaticScheduleForm();
                automaticScheduleForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tbxSearchName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string name = tbxSearchName.Text;
                Shift[] filterShifts = scheduleUtility.GetEmployeesInShiftsByName(currentShifts.ToArray(), name);

                UpdateAllListViewByShifts(filterShifts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void calSchedule_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                DateTime startDate = calSchedule.SelectionStart;
                DateTime endDate = calSchedule.SelectionEnd;

                Shift[] selectedShifts = scheduleUtility.GetShiftsInRangeDays(ref scheduleController,
                                                                             startDate, endDate);
                currentShifts = new List<Shift>(selectedShifts);

                UpdateAllListViewByShifts(currentShifts.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EmployeesExample()
        {
            employeesInDepartment = new List<Employee>(employeeModel.GetEmployeesFromDatabase());
        }
        private void UpdateShiftsProblemsListView(Shift[] currentShifts)
        {
            Shift[] shifts = scheduleUtility.GetUnassignedShifts(currentShifts);
            lvShiftProblems.Items.Clear();
            if (currentShifts == null)
            {
                return;
            }
            foreach (Shift shift in shifts)
            {
                ListViewItem lvi = new ListViewItem(shift.Date.ToShortDateString());
                lvi.SubItems.Add(shift.Session.ToString());
                lvi.SubItems.Add("Empty Shifts");
                lvi.Name = $"{shift.Date.ToShortDateString()}{shift.Session.ToString()}";
                lvShiftProblems.Items.Add(lvi);
            }
        }
        private void UpdateScheduleOverviewListView(Shift[] shifts)
        {
            lvScheduleOverview.Items.Clear();
            if (shifts == null)
            {
                return;
            }
            foreach (Shift shift in shifts)
            {
                if (shift.Employees == null)
                {
                    continue;
                }
                foreach (Employee employee in shift.Employees)
                {
                    ListViewItem lvi = new ListViewItem(shift.Date.ToShortDateString());
                    lvi.SubItems.Add(shift.Session.ToString());
                    string name = $"{employee.FirstName} {employee.LastName}";
                    lvi.SubItems.Add(name);
                    lvScheduleOverview.Items.Add(lvi);
                }
            }
        }
        private void UpdateAllListViewByShifts(Shift[] currentShifts)
        {
            UpdateShiftsProblemsListView(currentShifts);
            UpdateScheduleOverviewListView(currentShifts);
        }
        private void InitializeTheComponents()
        {
            cbxSession.Items.Clear();
            lvScheduleOverview.Items.Clear();
            lvShiftProblems.Items.Clear();

            foreach (Session session in Enum.GetValues(typeof(Session)))
            {
                cbxSession.Items.Add(session);
            }
            cbxSession.SelectedIndex = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    class ScheduleUtility
    {
        private readonly Array sessions;
        public ScheduleUtility()
        {
            sessions = Enum.GetValues(typeof(Session));
        }
        public Shift[] GetEmployeesInShiftsByName(Shift[] shifts, string searchName)
        {
            List<Shift> getShifts = new List<Shift>();
            foreach(Shift shift in shifts)
            {
                List<Employee> selectedEmployees = new List<Employee>();
                DateTime date = shift.Date;
                Session session = shift.Session;

                if (shift.Employees != null && shift.Employees.Length != 0)
                {
                    foreach (Employee employee in shift.Employees)
                    {
                        string name = $"{employee.FirstName} {employee.LastName}";
                        if (name.Contains(searchName))
                        {
                            selectedEmployees.Add(employee);
                        }
                    }
                }
                getShifts.Add(new Shift(date, session, selectedEmployees.ToArray()));
            }
            return getShifts.ToArray();
        }
        public Shift[] GetUnassignedShifts(Shift[] shifts)
        {
            List<Shift> selectedShifts = new List<Shift>();
            foreach(Shift shift in shifts)
            {
                if (shift.Employees == null || shift.Employees.Length == 0)
                {
                    selectedShifts.Add(shift);
                }
            }
            return selectedShifts.ToArray();
        }
        public Employee[] GetEmployeesNotInAnyShifts(Employee[] employees, Shift[] shifts)
        {
            List<Employee> selectedEmployees = new List<Employee>(employees);
            
            foreach(Shift shift in shifts)
            {
                if (shift.Employees == null || shift.Employees.Length == 0)
                {
                    continue;
                }
                foreach(Employee employee in shift.Employees)
                {
                    if (selectedEmployees.Contains(employee))
                    {
                        selectedEmployees.Remove(employee);
                    }
                }
            }
            return selectedEmployees.ToArray();
        }
        public Shift[] GetShiftsInRangeDays(ref ScheduleController scheduleController, DateTime startDate, DateTime endDate)
        {
            List<Shift> selectedShifts = new List<Shift>();
            IEnumerable<DateTime> chosenRangeDates = EachCalendarDay(startDate, endDate);

            foreach (DateTime date in chosenRangeDates)
            {
                foreach(Session session in sessions)
                {
                    Shift shift = scheduleController.GetShift(date, session);
                    selectedShifts.Add(shift);
                }
            }

            return selectedShifts.ToArray();
        }
        public void AutoAssigningShiftsFollowingWeek(ref ScheduleController scheduleController)
        {
            DateTime currentDate = DateTime.Now;
            DateTime endDate = currentDate.AddDays(7);

            IEnumerable<DateTime> rangesDate = EachCalendarDay(currentDate, endDate);

            foreach(DateTime date in rangesDate)
            {
                DateTime followingWeekDate = date.AddDays(7);
                foreach(Session session in sessions)
                {
                    Shift followingWeekShift = scheduleController.GetShift(followingWeekDate, session);
                    Shift currentShift = scheduleController.GetShift(date, session);
                    if (followingWeekShift.Employees == null)
                    {
                        Shift copyShiftForFollowingWeek = new Shift(followingWeekDate, session,
                                                                    currentShift.Employees);
                        scheduleController.AddShift(copyShiftForFollowingWeek);
                    }
                }
            }
        }
        public IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }
    }
}

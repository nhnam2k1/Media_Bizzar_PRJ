using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MediaBizzarApp
{
    class EditScheduleUtility
    {
        private HashSet<Employee> addEmployees;
        private HashSet<Employee> removeEmployees;
        private List<Employee> oldEmployees;
        private List<Employee> newEmployees;
        private bool HasFinalizedEmployeeUpdate;
        private DateTime date;
        private Session session;
        public EditScheduleUtility(DateTime date, Session session)
        {
            addEmployees = new HashSet<Employee>();
            removeEmployees = new HashSet<Employee>();
            oldEmployees = new List<Employee>();
            newEmployees = new List<Employee>();

            HasFinalizedEmployeeUpdate = false;
            this.date = date;
            this.session = session;
        }
        public void KeepTrackAddEmployee(Employee employee)
        {
            if (HasFinalizedEmployeeUpdate)
            {
                throw new Exception("You cannot add or remove employees after click save button");
            }
            if (removeEmployees.Contains(employee))
            {
                removeEmployees.Remove(employee);
            }
            addEmployees.Add(employee);
        }
        public void KeepTrackRemoveEmployee(Employee employee)
        {
            if (HasFinalizedEmployeeUpdate)
            {
                throw new Exception("You cannot add or remove employees after click save button");
            }
            if (addEmployees.Contains(employee))
            {
                addEmployees.Remove(employee);
            }
            removeEmployees.Add(employee);
        }
        public void FinalizedEmployeeUpdate()
        {
            HasFinalizedEmployeeUpdate = true;
            while (addEmployees.Count != 0 && removeEmployees.Count != 0)
            {
                Employee newEmployee = addEmployees.First();
                Employee oldEmployee = removeEmployees.First();
                oldEmployees.Add(oldEmployee);
                newEmployees.Add(newEmployee);
                addEmployees.Remove(newEmployee);
                removeEmployees.Remove(oldEmployee);
            }
        }
        public Shift GetTheListOfAddedEmployees()
        {
            if (!HasFinalizedEmployeeUpdate)
            {
                throw new Exception("You have not called FinalizedEmployeeUpdate !");
            }
            
            Shift shift = null;
            if (addEmployees.Count != 0)
            {
                shift = new Shift(date, session, addEmployees.ToArray());
            }
            return shift;
        }
        public Shift GetTheListOfRemovedEmployees()
        {
            if (!HasFinalizedEmployeeUpdate)
            {
                throw new Exception("You have not called FinalizedEmployeeUpdate !");
            }

            Shift shift = null;
            if (removeEmployees.Count != 0)
            {
                shift = new Shift(date, session, removeEmployees.ToArray());
            }
            return shift;
        }
        public UpdatedShift GetTheListOfUpdatedEmployees()
        {
            if (!HasFinalizedEmployeeUpdate)
            {
                throw new Exception("You have not called FinalizedEmployeeUpdate !");
            }

            UpdatedShift updatedShift = null;
            if (oldEmployees.Count != 0 && newEmployees.Count != 0)
            {
                updatedShift = new UpdatedShift(date, session, oldEmployees.ToArray(), newEmployees.ToArray());
            }
            return updatedShift;
        }
    }
}

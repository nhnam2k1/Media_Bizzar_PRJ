using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    [Serializable]
    class UpdatedShift : Shift
    {
        private Employee[] newEmployees;
        public Employee[] NewEmployees { get { return newEmployees; } }
        public Employee[] OldEmployees { get { return base.employees; } }
        public UpdatedShift(DateTime date, Session session, Employee[] oldEmployees, Employee[] newEmployees) 
            : base(date, session, oldEmployees)
        {
            this.newEmployees = newEmployees;
            CheckLengthOfNewEmployeesSameAsTheOldEmployees();
        }
        public void CheckLengthOfNewEmployeesSameAsTheOldEmployees()
        {
            if (newEmployees == null || base.employees == null)
            {
                throw new Exception("New employees and old employees should not be null or empty");
            }
            if (newEmployees.Length != base.Employees.Length)
            {
                throw new Exception("The length of new employees are not the same as old employees");
            }
        }
    }
}

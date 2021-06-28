using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBizzarApp
{
    class ScheduleValidation
    {
        private IScheduleModelValidation scheduleModelValidation;
        public ScheduleValidation()
        {
            scheduleModelValidation = new ScheduleSQLModel();
        }
        public string ValidateScheduleByFTE(Employee[] employees, DateTime startDate, DateTime endDate)
        {
            try {
                string warningEmployees = "";

                foreach(Employee employee in employees)
                {
                    int cnt = scheduleModelValidation.GetNumberOfEmployeeShiftsAssignedInRangeDays(employee,
                              startDate, endDate);

                    double limitFTE = employee.FTE; // cnt * 4 = hours, fte = hours / 40 => fte = cnt * 4 / 40 
                    double currentFTE = cnt / 10.0;

                    if (currentFTE >= limitFTE)
                    {
                        warningEmployees += $"{employee.ID}, ";
                    }
                }
                if (warningEmployees == "")
                {
                    return null;
                }
                return warningEmployees;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

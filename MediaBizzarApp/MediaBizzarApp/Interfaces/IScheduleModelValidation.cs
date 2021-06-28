using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBizzarApp
{
    interface IScheduleModelValidation
    {
        int GetNumberOfEmployeeShiftsAssignedInRangeDays(Employee employee, 
                                                         DateTime startDate, 
                                                         DateTime endDate);
        int GetNumberOfShiftsInRangeDates(DateTime startDate, DateTime endDate);
    }
}

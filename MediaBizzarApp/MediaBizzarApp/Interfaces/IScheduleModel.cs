using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    interface IScheduleModel : IScheduleModelValidation
    {
        Shift GetShiftFromDatabase(DateTime date, Session session);
        void AddShiftToDatabase(Shift shift);
        void UpdateShiftToDatabase(Shift newShift);
        void DeleteShiftFromDatabase(Shift shift);
    }
}

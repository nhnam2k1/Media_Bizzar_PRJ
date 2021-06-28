using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    class InputValidation
    {
        public InputValidation()
        {

        }
        public void CheckDateAndSessionForEditing(DateTime date, Session session)
        {
            DateTime currentDate = DateTime.Now;

            DateTime morningShiftTime = DateTime.ParseExact("08:00:00", "HH:mm:ss", 
                                        System.Globalization.CultureInfo.InvariantCulture);
            DateTime afternoonShiftTime = DateTime.ParseExact("12:00:00", "HH:mm:ss",
                                        System.Globalization.CultureInfo.InvariantCulture);
            DateTime eveningShiftTime = DateTime.ParseExact("16:00:00", "HH:mm:ss",
                                        System.Globalization.CultureInfo.InvariantCulture);

            Session currentSession = Session.MORNING;

            if (TimeSpan.Compare(currentDate.TimeOfDay, eveningShiftTime.TimeOfDay) >= 0)
            {
                currentSession = Session.EVENING;
            }
            else if (TimeSpan.Compare(currentDate.TimeOfDay, afternoonShiftTime.TimeOfDay) >= 0)
            {
                currentSession = Session.AFTERNOON;
            }

            date = date.Date;
            currentDate = currentDate.Date;

            if (date.CompareTo(currentDate) == -1)
            {
                throw new Exception("The date is in the past");
            }
            else if (date.CompareTo(currentDate) == 0)
            {
                if (session.CompareTo(currentSession) <= 0)
                {
                    throw new Exception("The date is in the past");
                }
            }
        }
    }
}

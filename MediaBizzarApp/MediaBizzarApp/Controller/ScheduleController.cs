using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MediaBizzarApp
{
    class ScheduleController
    {
        private IScheduleModel scheduleModel;
        private InputValidation inputValidation;
        private ScheduleValidation scheduleValidation;
        public bool WarningFteHasBeenCalled;
        public ScheduleController()
        {
            scheduleModel = new ScheduleSQLModel();
            inputValidation = new InputValidation();
            scheduleValidation = new ScheduleValidation();
            WarningFteHasBeenCalled = false;
        }
        public Shift GetShift(DateTime date, Session session)
        {
            try
            {
                Shift shift = scheduleModel.GetShiftFromDatabase(date, session);
                return shift;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckShiftsExistedInRangeDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                int cnt = scheduleModel.GetNumberOfShiftsInRangeDates(startDate, endDate);
                return cnt > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void AddShift(Shift newShift)
        {
            try
            {
                try
                {
                    inputValidation.CheckDateAndSessionForEditing(newShift.Date, newShift.Session);
                }
                catch(Exception ex)
                {
                    throw new Exception("You cannot add the schedule in the past");
                }
                DateTime startDate = newShift.Date;
                DateTime endDate = newShift.Date.AddDays(6);
                string message = scheduleValidation.ValidateScheduleByFTE(newShift.Employees, startDate, endDate);

                if (message != null && !WarningFteHasBeenCalled)
                {
                    WarningFteHasBeenCalled = true;
                    message = $"Employee {message} these employees has exceeded their FTE in a week, do you want to continue ?";
                    DialogResult result = MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                    {
                        throw new Exception("You have cancel the adding employee shifts process");
                    }
                }

                scheduleModel.AddShiftToDatabase(newShift);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateShift(UpdatedShift newShift)
        {
            try
            {
                try
                {
                    inputValidation.CheckDateAndSessionForEditing(newShift.Date, newShift.Session);
                }
                catch(Exception ex)
                {
                    throw new Exception("You cannot change the schedule in the past");
                }
                scheduleModel.UpdateShiftToDatabase(newShift);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteShift(Shift shift)
        {
            try
            {
                try
                {
                    inputValidation.CheckDateAndSessionForEditing(shift.Date, shift.Session);
                }
                catch (Exception ex)
                {
                    throw new Exception("You cannot delete the schedule in the past");
                }

                scheduleModel.DeleteShiftFromDatabase(shift);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

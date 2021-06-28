using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;   
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace MediaBizzarApp
{
    class ScheduleModel
    {
        static private Dictionary<string, Shift> schedule = null;
        private string filename = "schedule.bin";
        public ScheduleModel()
        {
            try
            {
                if (schedule == null)
                {
                    LoadShift();
                }
            }
            catch(Exception ex)
            {
                schedule = new Dictionary<string, Shift>();
            }
        }
        public Shift GetShiftFromDatabase(DateTime date, Session session)
        {
            try
            {
                string key = $"{date.ToShortDateString()}{session.ToString()}";

                if (!schedule.ContainsKey(key))
                {
                    return new Shift(date, session, null);
                }

                return schedule[key];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void AddShiftToDatabase(Shift shift)
        {
            try
            {
                string key = GetIndexKey(shift);

                if (schedule.ContainsKey(key))
                {
                    throw new Exception("The schedule already available");
                }
                schedule[key] = shift;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateShiftToDatabase(Shift newShift)
        {
            try
            {
                string key = GetIndexKey(newShift);

                //if (!schedule.ContainsKey(key))
                //{
                //    throw new Exception("The schedule is not availalbe");
                //}

                schedule[key] = newShift;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteShiftFromDatabase(Shift shift)
        {
            try
            {
                string key = GetIndexKey(shift);

                if (!schedule.ContainsKey(key))
                {
                    throw new Exception("The schedule is not availalbe");
                }

                schedule.Remove(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadShift()
        {
            FileStream fs = null;
            BinaryFormatter bf = null;
            
            try
            {
                //OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Filter = "Schedule file (*.schedule) | *.schedule";
                //ofd.Title = "Get the schedule file";
                //ofd.DefaultExt = ".schedule";

                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    filename = ofd.FileName;
                //}

                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                bf = new BinaryFormatter();
                schedule = (Dictionary<string, Shift>)bf.Deserialize(fs);
            }
            catch (Exception ex)
            {
                //throw ex;
                schedule = new Dictionary<string, Shift>();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        public void SaveShift()
        {
            FileStream fs = null;
            BinaryFormatter bf = null;

            try
            {
                //if (filename == "")
                //{
                //    SaveFileDialog sfd = new SaveFileDialog();
                //    sfd.Filter = "Schedule file (*.schedule) | *.schedule";
                //    sfd.Title = "Save Shift file";
                //    sfd.DefaultExt = ".schedule";

                //    if (sfd.ShowDialog() == DialogResult.OK)
                //    {
                //        filename = sfd.FileName;
                //    }
                //}

                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                bf = new BinaryFormatter();

                bf.Serialize(fs, schedule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        private string GetIndexKey(Shift shift)
        {
            DateTime date = shift.Date;
            Session session = shift.Session;
            string key = $"{date.ToShortDateString()}{session.ToString()}";
            return key;
        }
    }    
}

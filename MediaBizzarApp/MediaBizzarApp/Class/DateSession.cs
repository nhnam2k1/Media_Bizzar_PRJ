using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    [Serializable]
    public class DateSession
    {
        public DateTime Date { get; private set; }
        public Session Session { get; private set; }
        public DateSession(DateTime date, Session session)
        {
            Date = date;
            Session = session;
        }
    }
}

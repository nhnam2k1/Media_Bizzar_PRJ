using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MediaBizzarApp
{
    [Serializable]
    public class Shift : IComparable<Shift>, IEquatable<Shift>
    {
        protected DateTime date;
        protected Session session;
        protected Employee[] employees;
        public DateTime Date { get { return date; } }
        public Session Session { get { return session; } }
        public Employee[] Employees { get { return employees; } }
        public Shift(DateTime date, Session session, Employee[] employees)
        {
            this.date = date;
            this.session = session;
            this.employees = employees;
        }

        public int CompareTo(Shift other)
        {
            int cmpDate = date.CompareTo(other.date);
            int cmpSession = session.CompareTo(other.session);

            if (cmpDate != 0)
            {
                return cmpDate;
            }
            else if (cmpSession != 0)
            {
                return cmpSession;
            }
            return 0;
        }

        public bool Equals(Shift other)
        {
            if (other == null)
            {
                return false;
            }

            bool cmpDate = date.Equals(other.date);
            bool cmpSession = session.Equals(other.session);

            return cmpDate && cmpSession;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MediaBizzarApp

{
    [Serializable]
    public class Employee : User, IEquatable<Employee>, IComparable<Employee>, IEqualityComparer<Employee>
    {

        private string address;
        private string city;
        private string country;
        private string phone_number;
        private double wage;
        private string username;
        private string password;
        private double fte;
        private int shifts_taken = 0;
        private int hours_worked = 0;
        Gender gender;
        Department department;
        Contract contract;

        public Employee(int id, string first_name, string last_name, string email, string address, string city, string country,
         string phone_number, string gender, string department, string contract, string wage, string username, string password, string shifts_taken, string hours_worked) : base(id, first_name, last_name, email)
        {
            
            this.address = address;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            if (shifts_taken != "")
            {
                this.shifts_taken = Convert.ToInt32(shifts_taken);
            }
            if (hours_worked != "")
            {

                this.hours_worked = Convert.ToInt32(hours_worked);
            }
            this.wage = Convert.ToInt32(wage);
            Enum.TryParse(gender, out this.gender);
            Enum.TryParse(department, out this.department);
            Enum.TryParse(contract, out this.contract);
            this.username = username;
            this.password = password;
        }
        public Employee(int id, string first_name, string last_name, string email, string address, string city, string country,
                        string phone_number, string gender, string department, string contract, string wage, 
                        string username, string password, string shifts_taken, string hours_worked, double fte) 
                        : base(id, first_name, last_name, email)
        {

            this.address = address;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            this.fte = fte;
            if (shifts_taken != "")
            {
                this.shifts_taken = Convert.ToInt32(shifts_taken);
            }
            if (hours_worked != "")
            {

                this.hours_worked = Convert.ToInt32(hours_worked);
            }
            this.wage = Convert.ToInt32(wage);
            Enum.TryParse(gender, out this.gender);
            Enum.TryParse(department, out this.department);
            Enum.TryParse(contract, out this.contract);
            this.username = username;
            this.password = password;
        }
        public string UserName
        {
            get
            {
                return this.username;
            }
        }

        public string Password
        {
            get { return this.password; }
        }

        public string Address
        {
            get { return this.address; }

        }

        public string City
        {
            get { return this.city; }
        }

        public string Country
        {
            get { return this.country; }
        }

        public string PhoneNumber
        {
            get { return this.phone_number; }
        }

        public double Wage
        {
            get { return this.wage; }
        }

        public int HoursWorked
        {
            get { return this.hours_worked; }
        }
        public int ShiftsTaken
        {
            get { return this.shifts_taken; }
        }
        public double FTE
        {
            get { return fte; }
        }
        // Pedezisai part

        //public Enum Gender
        //{
        //    get { return this.gender; }

        //}

        //public Enum Department
        //{
        //    get { return department; }
        //}
        //public Enum Contract
        //{
        //    get { return this.contract; }
        //}

        // Nam part

        public Gender Gender
        {
            get { return this.gender; }

        }

        public Department Department
        {
            get { return department; }
        }
        public Contract Contract
        {
            get { return this.contract; }
        }
        public double getSalary()
        {
            double salary = 0;

            // Pedezisai part

            //if ((Contract)this.Contract == Contract.FullTime)
            //{
            //    salary = this.Wage * this.ShiftsTaken * 8;
            //}

            //if ((Contract)this.Contract == Contract.PartTime)
            //{
            //    salary = this.Wage * this.ShiftsTaken * 4;
            //}

            //if ((Contract)this.Contract == Contract.Flex)
            //{
            //    salary = this.Wage * this.HoursWorked;
            //}


            // Nam part
            if (contract == Contract.FullTime)
            {
                salary = this.Wage * this.ShiftsTaken * 8;
            }

            if (contract == Contract.PartTime)
            {
                salary = this.Wage * this.ShiftsTaken * 4;
            }

            if (contract == Contract.Flex)
            {
                salary = this.Wage * this.HoursWorked;
            }

            return salary;

        }
        // Nhat Nam do for interface extending
        public int CompareTo(Employee other)
        {
            int compareID = ID.CompareTo(other.ID);

            return compareID;
        }
        public bool Equals(Employee other)
        {
            if (other == null) return false;
            bool compareID = ID.Equals(other.ID);
            return compareID;
        }
        public bool Equals(Employee x, Employee y)
        {
            if (x == null || y == null) return false;
            return x.ID.Equals(y.ID);
        }
        public int GetHashCode(Employee obj)
        {
            return obj.ID.GetHashCode();
        }
    }

}


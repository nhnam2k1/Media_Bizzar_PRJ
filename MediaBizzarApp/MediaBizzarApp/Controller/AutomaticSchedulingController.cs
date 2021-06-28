using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MediaBizzarApp
{
    class AutomaticSchedulingController
    {
        private AvailabilityModel availabilityModel;
        private Employee[] employees;
        private bool[,,] employeesAvailable, solution; // nrEmployees, nrDays, nrShifts
        private int[,] limitRooms, cntEmployeesInShift;
        private int[] ShiftsForEmployees;
        private const int NUM_DAYS = 7;
        private const int NUM_SESSIONS = 3;
        Random rng;
        /*
         * solution string will have length of employees * NUM_DAYS * NUM_SESSIONS
         * 
         */
        public AutomaticSchedulingController()
        {
            availabilityModel = new AvailabilityModel();
            rng = new Random();
        }
        public Shift[] ProcessAutomaticScheduling(DateTime date, Employee[] employees, int[,] limitRooms, int secondsLimit = 10)
        {
            try
            {
                GetTheAvailabilityOfEmployees(employees);
                bool[,,] solution, bestSolution;
                this.limitRooms = limitRooms;
                int cnt, maxConditionMeet = 0, empLength = employees.Length;
                cntEmployeesInShift = new int[NUM_DAYS, NUM_SESSIONS];
                ShiftsForEmployees = new int[empLength];
                this.employees = employees;
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                bestSolution = GenerateRandomSolution(employees);
                maxConditionMeet = ValidateSolution(employees, bestSolution);
                while (true)
                {
                    for(int day = 0; day < NUM_DAYS; day++)
                    {
                        for (int session = 0; session < NUM_SESSIONS; session++)
                            cntEmployeesInShift[day, session] = 0;
                    }
                    for (int i = 0; i < empLength; i++) ShiftsForEmployees[i] = 0;
                    solution = GenerateRandomSolution(employees);
                    cnt = ValidateSolution(employees, solution);
                    if (cnt > maxConditionMeet)
                    {
                        maxConditionMeet = cnt;
                        bestSolution = solution;
                    }
                    if (stopwatch.Elapsed.Seconds > secondsLimit) break;
                }
                stopwatch.Stop();

                Shift[] shifts = ConvertSolutions(date, employees, bestSolution);
                return shifts;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        // Split the task here
        private void GetTheAvailabilityOfEmployees(Employee[] employees)
        {
            int length = employees.Length;
            employeesAvailable = new bool[length, NUM_DAYS, NUM_SESSIONS];

            for (int i = 0; i < length; i++)
            {
                Dictionary<string, int> available = availabilityModel.Get(employees[i].ID);
                int day = 0;
                foreach(var it in available)
                {
                    int value = it.Value;
                    foreach (Session session in Enum.GetValues(typeof(Session)))
                    {
                        int pos = (int)session - 1;
                        int bitmask = (1 << pos);
                        int chk = value & bitmask;
                        employeesAvailable[i, day, pos] = (chk > 0 ? true : false);
                    }
                    day++;
                }
            }
        }
        private bool[,,] GenerateRandomSolution(Employee[] employees)
        {
            int length = employees.Length;
            solution = new bool[length, NUM_DAYS, NUM_SESSIONS];

            for (int day = 0; day < NUM_DAYS; day++) 
            {
                foreach (Session session in Enum.GetValues(typeof(Session)))
                {
                    for (int i = 0; i < length; i++)
                    {
                        // Generate from based from Contract condition
                        switch (employees[i].Contract)
                        {
                            case Contract.FullTime:
                                {
                                    ComputeSolutionForFulltime(i, day, session);
                                    break;
                                }
                            case Contract.PartTime:
                                {
                                    
                                    ComputeSolutionForParttime(i, day, session);
                                    break;
                                }
                            case Contract.Flex:
                                {
                                    ComputeSolutionForFlex(i, day, session);
                                    break;
                                }
                        }
                    }
                }
            }
            return solution;
        }
        private void ComputeSolutionForFulltime(int index, int day, Session session)
        {
            int posSession = (int)session - 1;
            solution[index, day, posSession] = false;
            if (session == Session.MORNING || session == Session.AFTERNOON)
            {
                if (day < 5 
                    && cntEmployeesInShift[day, posSession] < limitRooms[day, posSession]) 
                {
                    solution[index, day, posSession] = true;
                    cntEmployeesInShift[day, posSession]++;
                }
            }
        }
        private void ComputeSolutionForParttime(int index, int day, Session session)
        {
            int cnt = 0;
            int posSession = (int)session - 1;
            foreach (Session s in Enum.GetValues(typeof(Session)))
            {
                int pos = (int)session - 1;
                if (solution[index, day, pos]) cnt++;
            }
            // If the number of shifts in a day below 2
            if (cnt < 2 && ShiftsForEmployees[index] < employees[index].FTE * 10
                && cntEmployeesInShift[day, posSession] < limitRooms[day, posSession])
            {
                double val = rng.NextDouble();
                solution[index, day, posSession] = (val > 0.5 ? true : false);
                if (solution[index, day, posSession]) 
                {
                    cntEmployeesInShift[day, posSession]++;
                    ShiftsForEmployees[index]++;
                }
            }
        }
        private void ComputeSolutionForFlex(int index, int day, Session session)
        {
            int cnt = 0;
            int posSession = (int)session - 1;
            foreach (Session s in Enum.GetValues(typeof(Session)))
            {
                int pos = (int)session - 1;
                if (solution[index, day, pos]) cnt++;
            }
            // If the number of shifts in a day below 2
            if (cnt < 2 && ShiftsForEmployees[index] < employees[index].FTE * 10
                && cntEmployeesInShift[day, posSession] < limitRooms[day, posSession])
            {
                double val = rng.NextDouble();
                solution[index, day, posSession] = (val > 0.5 ? true : false);
                if (solution[index, day, posSession])
                {
                    cntEmployeesInShift[day, posSession]++;
                    ShiftsForEmployees[index]++;
                }
            }
        }
        private int ValidateSolution(Employee[] employees, bool[,,] solution)
        {
            int conditionMeet = 0;
            int length = employees.Length;

            for (int i = 0; i < length; i++)
            {
                int cntShift = 0;
                for (int day = 0; day < NUM_DAYS; day++)
                {
                    foreach (Session session in Enum.GetValues(typeof(Session)))
                    {
                        int posSession = (int)session - 1;
                        if (solution[i, day, posSession]) 
                        {
                            if (employeesAvailable[i, day, posSession]) conditionMeet++;
                            else conditionMeet--;
                            cntShift++;
                        }
                    }
                }
                double fte = cntShift / 10.0;
                if (fte > 0.0 && fte <= employees[i].FTE) conditionMeet++;
            }
            return conditionMeet;
        }
        private Shift[] ConvertSolutions(DateTime datum, Employee[] employees, bool[,,] solution)
        {
            List<Shift> shifts = new List<Shift>();
            int length = employees.Length;
            DateTime mondayNextWeek = GetNextWeekday(datum, DayOfWeek.Monday);

            for (int day = 0; day < NUM_DAYS; day++)
            {
                DateTime date = mondayNextWeek.AddDays(day);
                foreach (Session session in Enum.GetValues(typeof(Session)))
                {
                    List<Employee> choose = new List<Employee>();
                    int posSession = (int)session - 1;

                    for (int i = 0; i < length; i++)
                    {
                        if (solution[i, day, posSession])
                        {
                            choose.Add(employees[i]);
                        }
                    }
                    shifts.Add(new Shift(date, session, choose.ToArray()));
                }
            }
            return shifts.ToArray();
        }
        public DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            DateTime result = start.AddDays(daysToAdd);
            string normalized = result.ToShortDateString();
            return Convert.ToDateTime(normalized);
        }
    }
}

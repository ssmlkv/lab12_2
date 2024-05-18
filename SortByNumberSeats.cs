using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using lab10;

namespace ClassLibrary1
{
    public class SortByNumberSeats : IComparer
    {
        public int Compare(object x, object y)
        {
            PassengerWagon p1 = x as PassengerWagon;
            PassengerWagon p2 = y as PassengerWagon;
            if (p1.NumberSeats < p2.NumberSeats) return -1;
            else if (p1.NumberSeats == p2.NumberSeats) return 0;
            else return 1;
        }
    }
}

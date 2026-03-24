using APBD_TASK2.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Policy
{
    public static class Policy
    {
        public static readonly Dictionary<UserType, int> MaxActiveRentals = new()
        {
            {UserType.Student, 2},
            {UserType.Employee, 5}
        };

        public static readonly Dictionary<UserType, decimal> PenaltyRate = new()
        {
            {UserType.Student, 10m},
            {UserType.Employee, 5m }
        };

        public static int GetMaxRentalCount(UserType userType)
        {
            return MaxActiveRentals[userType];
        }

        public static decimal CalculatePenalty(UserType type, DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate) return 0m;
            int daysLate = (int)Math.Ceiling((returnDate - dueDate).TotalDays);
            decimal rate = PenaltyRate[type];
            return daysLate * rate;
        }

    }
}

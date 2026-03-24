using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Models
{
    public class RentalObject
    {
        public static int _nextId = 1;
        public int Id { get; set; }
        public Equipment Equipment { get; set; }
        public User User { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Penalty { get; set; }
        public RentalObject(User user, Equipment equipment)
        {
            Id = _nextId++;
            User = user;
            Equipment = equipment;
            RentalDate = DateTime.Now;
            DueTime = RentalDate.AddDays(7);
        }
    }
}

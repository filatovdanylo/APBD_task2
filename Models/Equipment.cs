using APBD_TASK2.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Models
{
    public abstract class Equipment
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string Name { get; set; }

        public string Description { get; set; }

        public EquipmentStatus Status { get; set; }

        public DateTime AddedDate { get; set; }

        public Equipment(string name, string description = "")
        {
            Id = _nextId++;
            Name = name;
            Description = description;
            AddedDate = DateTime.Now;
            Status = EquipmentStatus.Available;
        }

    }
}

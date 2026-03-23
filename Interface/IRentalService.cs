using APBD_TASK2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Interface
{
    public interface IRentalService
    {
        void AddEquipment(Equipment equipment);

        List<Equipment> GetEquipment();
        List<Equipment> getAllAvailableEquipment();
        string GenerateReport();
    }
}

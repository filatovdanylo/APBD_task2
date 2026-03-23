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
        RentalObject RentEquipment(int userId, int equipmentId);
        RentalObject ReturnEquipment(int rentalId);
        List<RentalObject> GetActiveRentalsForUser(int userId);
        List<RentalObject> GetOverdueRentals();
        string GenerateReport();
    }
}

using APBD_TASK2.Interface;
using APBD_TASK2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Controller
{
    public class AppController
    {

        private static IRentalService _service = null!;
        public static void Run(RentalService service)
        {
            _service = service;

            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine()?.Trim();
                // do something
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1.  Add user");
            Console.WriteLine("2.  Add equipment");
            Console.WriteLine("3.  List all equipment");
            Console.WriteLine("4.  List available equipment");
            Console.WriteLine("5.  Rent equipment");
            Console.WriteLine("6.  Return equipment");
            Console.WriteLine("7.  Mark equipment unavailable");
            Console.WriteLine("8.  Active rentals for user");
            Console.WriteLine("9.  Overdue rentals");
            Console.WriteLine("10. Generate report");
            Console.WriteLine("0.  Exit");
            Console.WriteLine("Choose: ");
        }
    }
}

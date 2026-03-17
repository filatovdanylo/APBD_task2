using APBD_TASK2.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Models
{
    public class User
    {
        private static int _nextId = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public UserType Type { get; set; }

        public User(string name, string surname, string email, UserType type)
        {
            Id = _nextId++;
            Name = name;
            Surname = surname;
            Email = email;
            Type = type;
        }
    }
}

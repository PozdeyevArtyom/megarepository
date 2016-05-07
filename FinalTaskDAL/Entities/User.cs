using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegDate { get; set; }
        public UserType Type { get; set; } = UserType.User;
        public string Email { get; set; }

        public User() { }

        public User(string name, string pass, DateTime reg, string email)
        {
            Name = name;
            Password = pass;
            RegDate = reg;
            Email = email;
        }
    }
}

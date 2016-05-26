using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace FinalTask.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public DateTime RegDate { get; set; }
        public string Email { get; set; }
        public int RootId { get; set; }

        public UserModel() { }

        public UserModel(string name)
        {
            Name = name;
        }

        public UserModel(User user, int rootid)
        {
            Name = user.Name;
            RegDate = user.RegDate;
            Email = user.Email;
            RootId = rootid;
        }
    }
}
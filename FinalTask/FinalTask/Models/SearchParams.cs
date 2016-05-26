using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalTask.Models
{
    public class SearchParams
    {
        public string NamePattern { get; set; } = "";
        public string OwnerNamePattern { get; set; } = "";
    }
}
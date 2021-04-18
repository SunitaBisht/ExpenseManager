using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.API.Models
{
    public class GroupResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int VillageCode { get; set; }
    }
}
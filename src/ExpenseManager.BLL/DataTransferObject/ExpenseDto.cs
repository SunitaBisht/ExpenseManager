using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.BLL.DataTransferObject
{
    public class ExpenseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }
    }
}

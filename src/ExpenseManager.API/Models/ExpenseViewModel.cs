﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseManager.API.Models
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }
    }
}
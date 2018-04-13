using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace wedding_planner.Models

{
    public class WeddingViewModel
    {
        [Required(ErrorMessage = "Name1 is required")]

        public string Name1 { get; set; }

        [Required(ErrorMessage = "Name2 is required")]
        public string Name2 { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [FutureDate(ErrorMessage="Date cannot be in the past.")]

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value >= DateTime.Now.Date;
        }
    }
}
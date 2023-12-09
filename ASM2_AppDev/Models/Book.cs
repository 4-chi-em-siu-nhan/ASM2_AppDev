﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_AppDev.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public DateOnly PublicationDate { get; set; }
        public string Publisher { get; set; }

        public double Price { get; set; }
        [ValidateNever]

        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}

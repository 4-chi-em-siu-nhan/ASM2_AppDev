using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_AppDev.Models
{
    public class OrderDetails
    {
        
        public int Id { get; set; }
        public string Email { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public string OrderCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_AppDev.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderCode { get; set; }
        

    }
}

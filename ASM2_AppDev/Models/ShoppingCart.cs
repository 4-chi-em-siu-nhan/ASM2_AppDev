using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM2_AppDev.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        
        public string Title { get; set; }
        public string Author { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image {  get; set; }
        public decimal Total
        {
            get { return Quantity*Price; }
        }
        public ShoppingCart()
        {

        }
        public ShoppingCart(Book book)
        {

            BookId = book.Id;
            Title = book.Title;
            Quantity = 1;
            Price = book.Price;
            Author = book.Author;
            
            Image = book.ImageUrl;
        }
    }
}

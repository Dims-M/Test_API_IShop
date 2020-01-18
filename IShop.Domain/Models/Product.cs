
using System.ComponentModel.DataAnnotations;

namespace IShop.Domain.Models
{
    /// <summary>
    /// Класс описывающий сам товар
    /// </summary>
    public class Product : IIdentifiable
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}

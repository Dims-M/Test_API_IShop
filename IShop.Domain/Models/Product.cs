
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

        [Required] //данный атрибут указывает что Это свойство обязательно должно указыватсяс именем.Не может быть пустым
        public string Name { get; set; }

        public double Price { get; set; }
    }
}

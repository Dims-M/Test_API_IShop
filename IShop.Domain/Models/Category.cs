
namespace IShop.Domain.Models
{
    /// <summary>
    /// Класс описвает категорию товара.
    /// Имеет 2 свойства. Id и имя
    /// </summary>
    public class Category : IIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

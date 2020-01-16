using IShop.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IShop.BusinessLogic.Services
{
    public interface IProductService
    {
        void Add(Product category);

        void Update(Product category);

        void Delete(int id);

        List<Product> GetAll();

        Product Get(int id);
    }

    public class ProductService : ServiceBase, IProductService
    {
        private const string FilePath = @"\bin\Data\Products.txt";

        private List<Product> _products;

        public ProductService()
        {
            var savedData = ReadData();

            _products =
                savedData != null
                ? savedData
                : new List<Product>();
        }

        public void Add(Product product)
        {
            int id = GetMaxId(_products
                                    .OfType<IIdentifiable>()
                                    .ToList());

            product.Id = id + 1;

            _products.Add(product);

            SaveChanges();
        }

        public void Delete(int id)
        {
            var product = Get(id);

            if (product != null)
            {
                _products.Remove(product);
            }

            SaveChanges();
        }

        public Product Get(int id)
        {
            return _products
                        .FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public void Update(Product product)
        {
            var oldCategory = Get(product.Id);

            oldCategory.Name = product.Name;

            SaveChanges();
        }

        private List<Product> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Product>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_products);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}

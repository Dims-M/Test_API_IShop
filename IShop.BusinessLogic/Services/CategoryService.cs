using IShop.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IShop.BusinessLogic.Services
{
    /// <summary>
    /// Интерфейс описывающий категорию
    /// </summary>
    public interface ICategoryService
    {
        void Add(Category category);

        void Update(Category category);

        void Delete(int id);

        /// <summary>
        /// Лист с хронящимся в нем категориям
        /// </summary>
        /// <returns></returns>
        List<Category> GetAll();

        Category Get(int id);
    }

    public class CategoryService : ServiceBase, ICategoryService
    {
        private const string FilePath = @"\bin\Data\Categories.txt";

        private List<Category> _categories;

        public CategoryService()
        {
            var savedData = ReadData();

            _categories =
                savedData != null
                ? savedData
                : new List<Category>();
        }

        public void Add(Category category)
        {
            int id = GetMaxId(_categories
                                    .OfType<IIdentifiable>()
                                    .ToList());

            category.Id = id + 1;

            _categories.Add(category);

            SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Get(id);

            if (category != null)
            {
                _categories.Remove(category);
            }

            SaveChanges();
        }

        public Category Get(int id)
        {
            return _categories
                        .FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        public void Update(Category category)
        {
            var oldCategory = Get(category.Id);

            oldCategory.Name = category.Name;

            SaveChanges();
        }

        private List<Category> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Category>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}

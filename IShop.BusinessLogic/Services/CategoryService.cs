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

        /// <summary>
        /// Лист для хранения категорий
        /// </summary>
        private List<Category> _categories;

        public CategoryService()
        {
            var savedData = ReadData();

            _categories =
                savedData != null
                ? savedData
                : new List<Category>();
        }

        /// <summary>
        /// Создание и добавление новой категории
        /// </summary>
        /// <param name="category"></param>
        public void Add(Category category)
        {
            //получаем максимальный Id на текущий момент
            int id = GetMaxId(_categories
                                    .OfType<IIdentifiable>()
                                    .ToList());

            category.Id = id + 1; //увеличиваем на 1

            _categories.Add(category); //и присваеваем новой записи

            SaveChanges();
        }

        /// <summary>
        /// удаляем котегорию по ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var category = Get(id); //получаем 

            if (category != null) 
            {
                _categories.Remove(category);
            }

            SaveChanges(); //сохранение новой категории
        }

       
        /// <summary>
        ///  ищем  нужный id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category Get(int id)
        {
            return _categories 
                        .FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        /// <summary>
        /// Метод обновления категории.
        /// 
        /// </summary>
        /// <param name="category">Нужная катнгория</param>
        public void Update(Category category)
        {
            var oldCategory = Get(category.Id); //получаем id старой котегории

            oldCategory.Name = category.Name; //переименновываем старую на новую

            SaveChanges(); //сохранняем результат
        }

        private List<Category> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(FilePath));

            return JsonConvert.DeserializeObject<List<Category>>(data);
        }

        /// <summary>
        /// Метод сохранениия результата в Json формате 
        /// </summary>
        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(FilePath), data);
        }
    }
}

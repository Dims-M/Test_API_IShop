using IShop.BusinessLogic.Services;
using IShop.Domain.Models;
using IShop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IShop.Controllers
{
    /// <summary>
    /// Контролер Категории
    /// </summary>
    [RoutePrefix("api/categories")] //Атрибут ддля роутинга
    public class CategoriesController : ApiController
    {
        /// <summary>
        /// Переменная интерфейса ICategoryService. С описанием класса категории
        /// </summary>
        private ICategoryService _categoryService
            = new CategoryService();


        /// <summary>
        /// Возвращаем весь список категорий. Который у нас имеется
        /// </summary>
        /// <returns></returns>
        [HttpGet] //Атрибут метода Get
        public IHttpActionResult GetAll()
        {           
            return Ok(_categoryService.GetAll());
        }

        /// <summary>
        /// Получаем одну категории по ID
        /// </summary>
        /// <param name="id">ID нужной категории</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.Get(id); //получаем категорию 

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Метод(Action) создания новой категории
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost] //атрибут пост. Указывает что будет обращение от клиента к серверу
        public IHttpActionResult Add([FromBody] Category category)// атрибут боди [FromBody] и это означает чо атрибут приходит из тела заппоса.. А не изаголовка 
        {
            //проверка на нулл и неиницализированный обьект
            if (string.IsNullOrEmpty(category.Name))
                return Ok("Не может быть пустым");

            _categoryService.Add(category); //созднание новой директории

            return Ok(); //возращаем удачный результат ОК.
        }

        /// <summary>
        /// Метод Обновление текущей котегории товаров
        /// Можем только переименовывать категории
        /// </summary>
        /// <param name="category">Указываем какую категорию нужно обновить </param>
        /// <returns></returns>
        [HttpPut] //атрибут HttpPut 
        public IHttpActionResult Update([FromBody] Category category) // атрибут боди [FromBody] и это означает чо атрибут приходит из тела заппоса.. А не изаголовка 
        {
            _categoryService.Update(category);

            return Ok();
        }

        /// <summary>
        /// Метод удаления категории. По ID
        /// </summary>
        /// <param name="id">ID нужной категории</param>
        /// <returns></returns>
        [HttpDelete] //атрибут. Сам id приходит по ури в запросе к серверу
        public IHttpActionResult Delete(int id)
        {
            _categoryService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string name)
        {
            var categories = _categoryService.GetAll();

            categories = categories.Where(c => c.Name.ToLower().Contains(name)).ToList();

            return Ok(categories);
        }
    }
}

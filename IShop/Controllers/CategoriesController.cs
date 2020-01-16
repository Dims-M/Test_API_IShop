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

        [HttpPost]
        public IHttpActionResult Add([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
                return Ok("Can't be empty");

            _categoryService.Add(category);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] Category category)
        {
            _categoryService.Update(category);

            return Ok();
        }

        [HttpDelete]
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

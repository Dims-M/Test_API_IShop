using IShop.BusinessLogic.Services;
using IShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IShop.Controllers
{
    /// <summary>
    /// Контролер с логикой описывающий Products
    /// Он также выполняет действия над входными данными запросов в контроллеру
    /// И возращае какой либо результат обратно.
    /// </summary>
    public class ProductsController : ApiController
    { /// <summary>
      /// Переменная интерфейса IProductService. С описанием класса продуктов(товаров)
      /// </summary>
        IProductService _productService
            = new ProductService();

        /// <summary>
        /// Получить все товары
        /// </summary>
        /// <returns></returns>
        [OverrideAuthentication]
        public IHttpActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        /// <summary>
        /// получить товар по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            return Ok(_productService.Get(id));
        }


        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add([FromBody] Product product) ////Получаем экземпляр класса Product атрибут боди [FromBody] и это означает чо атрибут приходит из тела заппоса.. А не изаголовка 
        {
            if (!ModelState.IsReadOnly) //проверка на состояние. Если данные не коректны 
            {
                var errorMessage = new HttpResponseMessage(HttpStatusCode.BadRequest); //ответ  клиенту. плохой, невыполнимый запрос

                errorMessage.Content = new StringContent("Имя продукта  не может быть пустым");

                return errorMessage; //отправляем сообщение о ошибке
            }

            _productService.Add(product); //добовляем ноовый продукт

            return new HttpResponseMessage(HttpStatusCode.OK); // отправляем успеный статук ОК
        }
      
        
        /// <summary>
        /// Обновление товара.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update([FromBody] Product product)
        {
            _productService.Update(product); //обновление товара

            return Ok();
        }
     
        
        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _productService.Delete(id);

            return Ok();
        }
    }
}

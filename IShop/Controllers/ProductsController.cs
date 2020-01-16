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


        [HttpPost]
        public HttpResponseMessage Add([FromBody] Product product)
        {
            if (!ModelState.IsReadOnly)
            {
                var errorMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

                errorMessage.Content = new StringContent("Name can't be empty");

                return errorMessage;
            }

            _productService.Add(product);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] Product product)
        {
            _productService.Update(product);

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _productService.Delete(id);

            return Ok();
        }
    }
}

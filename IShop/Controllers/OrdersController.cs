using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IShop.Controllers
{
    public class Order
    {
        public int Number { get; set; }
    }

    public class OrdersController : ApiController
    {
        [NonAction]
        public IEnumerable<Order> Get()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Number = 1
                },
                new Order
                {
                    Number = 2
                }
            };

            return orders;
        }
    }
}

using IShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IShop.Controllers
{
    public class CategoriesViewController : Controller
    {
        // GET: CategoriesView
        public async Task<ActionResult> Index()
        {
            var categories = await GetData();

            return View(categories);
        }

        private async Task<List<Category>> GetData()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:55788/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            List<Category> categories = null;
            HttpResponseMessage response = await client.GetAsync("api/categories");
            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<List<Category>>();
            }
            return categories;
        }
    }
}
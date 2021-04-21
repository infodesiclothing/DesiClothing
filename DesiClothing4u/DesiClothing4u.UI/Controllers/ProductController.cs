using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DesiClothing4u.UI.Controllers
{
    public class ProductController : Controller
    {
        
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product product = new Product();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            /*UriBuilder builder = new UriBuilder("https://localhost:44356/api/Products/GetProductDetail?");*/
            UriBuilder builder = new UriBuilder("https://localhost:44356/api/Products/GetProductDetail?");
            
            builder.Query = "Id=" + id;
            HttpResponseMessage Res = await client.GetAsync(builder.Uri);
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var Product1 = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the SiteUser object 
                Product[] a= JsonConvert.DeserializeObject<Product[]>(Product1);
                ViewBag.Product = product;
                ViewBag.Error = null;
                return View("Single", a);
            }
            return View();
        }
        
       
        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }
   
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Product>> Create(IFormCollection collection)
        {
            try
            {
                Product product = new Product
                {
                    Name = collection["ProductName"],
                    ProductTypeId = 5,
                    VisibleIndividually = true,
                    ShortDescription= collection["ShortDescription"],
                    FullDescription = collection["FullDescription"],
                    ProductTemplateId = 1,
                    AllowCustomerReviews = true,
                    CreatedOnUtc = DateTime.UtcNow
                };

                var output = JsonConvert.SerializeObject(product);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                /*var url = "https://localhost:44356/api/Vendors";*/
                var url = "https://localhost:44356/api/Vendors";
                
                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Vendor = response.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Vendor>(Vendor);
                ViewBag.Product = a;
                return View(a);
            }
            catch
            {
                return View();
            }
        }



        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            
            var value = session.GetString(key);
            return value == null ? default(T) :JsonConvert.DeserializeObject<T>(value);

        }
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

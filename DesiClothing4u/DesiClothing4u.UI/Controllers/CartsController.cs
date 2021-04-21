using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using DesiClothing4u.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace DesiClothing4u.UI.Controllers
{
    public class CartsController : Controller
    {
        
        // GET: CartsController
        public ActionResult Index()
        {
            return View();
        }
        //Added by SM on Nov 25, 2020 for cart
        public ActionResult AddCart(int Id, string Name, decimal Price)
        {
            Cart cart = new Cart
            {
                Id = Id,
                Name = Name,
                Price = Price
            };
            if (HttpContext.Session.GetString("cart") == null)
            {

                List<Cart> li = new List<Cart>();
                li.Add(cart);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                ViewBag.cartcount = li.Count();
                HttpContext.Session.SetInt32("count", 1);
                ViewBag.cart = cart;
                return View("MyCart", (List<Cart>)li);
            }
            else //not empty
            {
                var value = HttpContext.Session.GetString("cart");
                List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                li.Add(cart);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                HttpContext.Session.SetInt32("count", li.Count());
                var a = HttpContext.Session.GetInt32("count");
                return View("MyCart", li);
            }
        }
        // GET: Carts/MyCart
        public async Task<ActionResult> MyCart(string CustId)
        {
            
                if (HttpContext.Session.GetString("cart") != null)
                {
                    var value = HttpContext.Session.GetString("cart");
                    List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                    return View("MyCart", li);
                }
            if (CustId != null)
            {
                //if session["cart"] is empty    
                Cart cart = new Cart();
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource DeleteCart using HttpClient  
                UriBuilder builder = new UriBuilder("https://localhost:44363/api/Carts/GetCartofBuyer?");
                /*UriBuilder builder = new UriBuilder("https://localhost:44356/api/Carts/GetCartofBuyer?");*/
                
                builder.Query = "CustId=" + CustId;
                HttpResponseMessage Res = await client.GetAsync(builder.Uri);
                if (Res.IsSuccessStatusCode)
                {
                    var cart1 = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the cart list
                    Cart[] c = JsonConvert.DeserializeObject<Cart[]>(cart1);
                    ViewBag.Cart = c;

                    if (HttpContext.Session.GetString("cart") != null)
                    {
                        var value = HttpContext.Session.GetString("cart");
                        List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                        return View("MyCart", li);
                    }
                    else
                    {
                        List<Cart> li = new List<Cart>(c);
                        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                        return View("MyCart", li);
                    }
                }//if not successful
                else
                {
                    Error err = new Error();
                    err.ErrorMessage = "Sorry there are no items in your cart  " + CustId;
                    ViewBag.Error = err;
                    ViewBag.Cart = null;
                    return View("Error", err);
                }
            }
            else //if customer not logged in
            {
                Error err = new Error();
                err.ErrorMessage = "Sorry there are no items in your cart  ";
                ViewBag.Error = err;
                ViewBag.Cart = null;
                return View("Error", err);
            }
        }
        // GET: CartsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CartsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartsController/Edit/5
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

        // GET: CartsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartsController/Delete/5
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

        public async Task<ActionResult> CheckOut(string CustId)
        {
            if (HttpContext.Session.GetString("cart") != null)
            {
                var value = HttpContext.Session.GetString("cart");
                List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                return View("checkout", li);
            }
            if (CustId != null)
            {
                //if session["cart"] is empty    
                Cart cart = new Cart();
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource DeleteCart using HttpClient  
                UriBuilder builder = new UriBuilder("https://localhost:44363/api/Carts/GetCartofBuyer?");
                /*UriBuilder builder = new UriBuilder("https://localhost:44356/api/Carts/GetCartofBuyer?");*/
                
                builder.Query = "CustId=" + CustId;
                HttpResponseMessage Res = await client.GetAsync(builder.Uri);
                if (Res.IsSuccessStatusCode)
                {
                    var cart1 = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the cart list
                    Cart[] c = JsonConvert.DeserializeObject<Cart[]>(cart1);
                    ViewBag.Cart = c;

                    if (HttpContext.Session.GetString("cart") != null)
                    {
                        var value = HttpContext.Session.GetString("cart");
                        List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                        return View("checkout", li);
                    }
                    else
                    {
                        List<Cart> li = new List<Cart>(c);
                        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                        return View("checkout", li);
                    }
                }//if not successful
                else
                {
                    Error err = new Error();
                    err.ErrorMessage = "Sorry there are no items in your cart  " + CustId;
                    ViewBag.Error = err;
                    ViewBag.Cart = null;
                    return View("Error", err);
                }
            }
            else //if customer not logged in
            {
                Error err = new Error();
                err.ErrorMessage = "Sorry there are no items in your cart  ";
                ViewBag.Error = err;
                ViewBag.Cart = null;
                return View("Error", err);
            }
           /* return View("checkout");*/
        }
    }
}

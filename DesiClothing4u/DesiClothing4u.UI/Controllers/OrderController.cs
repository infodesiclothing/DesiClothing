using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesiClothing4u.Common.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DesiClothing4u.UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost("CreateOrder")]
        /*[ValidateAntiForgeryToken]*/
        public async Task<ActionResult<Order>> CreateOrder(IFormCollection collection)
        {
            try
            {
                if (collection["CardNumber"] != "6292400011276686")
                {
                    ViewBag.Message = "Payment rejected with Credit Card No. you supplied";
                    return View("Payment");
                }
                string ODate = collection["CardExpiration"];
                int TotalAmount = 0;
                Random rnd = new Random();
                Order order = new Order
                {
                    CustomOrderNumber = Request.Cookies["userid"] + 78,
                    CustomerId = Int32.Parse(Request.Cookies["userid"]),
                    OrderGuid = Guid.Parse("69121893-3AFC-4F92-85F3-40BB5E7C7E29"),
                    StoreId = 1,
                    PickupInStore = false,
                    OrderStatusId = 1,
                    ShippingStatusId = 1,
                    PaymentStatusId = 1,
                    CurrencyRate = 1,
                    CustomerTaxDisplayTypeId = 1,
                    OrderSubtotalInclTax = TotalAmount,
                    OrderSubtotalExclTax = TotalAmount,
                    OrderSubTotalDiscountInclTax = 0,
                    OrderSubTotalDiscountExclTax = 0,
                    PaymentMethodAdditionalFeeInclTax = 0,
                    PaymentMethodAdditionalFeeExclTax = 0,
                    OrderTax = 0,
                    OrderDiscount = 0,
                    OrderTotal = TotalAmount,
                    RefundedAmount = 0,
                    CustomerLanguageId = 1,
                    AffiliateId = 0,
                    AllowStoringCreditCardNumber = false,
                    Deleted = false,
                    CardType = collection["CardType"],
                    CardName = collection["CardName"],
                    CardCvv2 = collection["CardCvv2"],
                    CardExpirationMonth = ODate.Substring(0, 2),
                    CardExpirationYear = ODate.Substring(3),
                    CardNumber = collection["CardNumber"],
                    BillingAddressId = 1106,
                    CreatedOnUtc = DateTime.UtcNow
                };
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                /*client.BaseAddress = new Uri("https://localhost:44356/api/Orders/PostOrder");*/
                client.BaseAddress = new Uri("https://localhost:44356/api/Orders/PostOrder");
                string data = JsonConvert.SerializeObject(order);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync(client.BaseAddress, content);
                var Order = Res.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Order>(Order);

                if (HttpContext.Session.GetString("cart") != null)
                {
                    var value = HttpContext.Session.GetString("cart");
                    List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                    int i = li.Count();
                    for (var j = 0; j <= i-1; j++)
                    {
                        var price = li[j].Price;
                        var name = li[j].Name;
                        OrderItem orderItem = new OrderItem
                        {
                            OrderId = a.Id,
                            ProductId = li[j].Id,
                            OrderItemGuid = Guid.Parse("69121893-3AFC-4F92-85F3-40BB5E7C7E29"),
                            Quantity = 1,
                            UnitPriceInclTax = li[j].Price,
                            UnitPriceExclTax = li[j].Price,
                            PriceInclTax = li[j].Price,
                            PriceExclTax = li[j].Price,
                            DiscountAmountInclTax = 0,
                            DiscountAmountExclTax = 0,
                            OriginalProductCost = 0,
                            DownloadCount = 0,
                            IsDownloadActivated = false,
                        };
                        var client1 = new HttpClient();
                        client1.DefaultRequestHeaders.Clear();
                        /*client1.BaseAddress = new Uri("https://localhost:44356/api/OrderItems/PostOrderItem");*/
                        client1.BaseAddress = new Uri("https://localhost:44356/api/OrderItems/PostOrderItem");
                        
                        data = JsonConvert.SerializeObject(orderItem);
                        content = new StringContent(data, Encoding.UTF8, "application/json");
                        Res = await client1.PostAsync(client1.BaseAddress, content);
                        var OrderItem = Res.Content.ReadAsStringAsync().Result;
                    };

                    /*PostOrderItem*/



                    /*collection["exampleInputEmail1"]*/

                   
                }
                ViewBag.Message = "You order has been placed. You will get confirmation email soon";
                return View("Payment");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Error occured during transaction! Please try again";
                return View("Payment");
            }
        }


        /*// POST: OrderController/Create
        [HttpPost("SendEmail")]
        *//*[ValidateAntiForgeryToken]*//*
        public async Task<ActionResult<Order>> SendEmail(int OrderId)
        {
            try
            {
                var clientC = new HttpClient();
                UriBuilder builderC = new UriBuilder("https://localhost:44356/api/Orders/GetOrders?");
                builderC.Query = "id=" + OrderId;
                HttpResponseMessage responseC = await clientC.GetAsync(builderC.Uri);
                if (responseC.IsSuccessStatusCode)
                {
                    var Order = responseC.Content.ReadAsStringAsync().Result;
                    var order = JsonConvert.DeserializeObject<Order>(Order);
                }

                if (HttpContext.Session.GetString("cart") != null)
                {
                    var value = HttpContext.Session.GetString("cart");
                    List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>(value);
                    int i = li.Count();
                    for (var j = 0; j <= i - 1; j++)
                    {
                        var price = li[j].Price;
                        var name = li[j].Name;
                        OrderItem orderItem = new OrderItem
                        {
                            OrderId = a.Id,
                            ProductId = li[j].Id,
                            OrderItemGuid = Guid.Parse("69121893-3AFC-4F92-85F3-40BB5E7C7E29"),
                            Quantity = 1,
                            UnitPriceInclTax = li[j].Price,
                            UnitPriceExclTax = li[j].Price,
                            PriceInclTax = li[j].Price,
                            PriceExclTax = li[j].Price,
                            DiscountAmountInclTax = 0,
                            DiscountAmountExclTax = 0,
                            OriginalProductCost = 0,
                            DownloadCount = 0,
                            IsDownloadActivated = false,
                        };
                        var client1 = new HttpClient();
                        client1.DefaultRequestHeaders.Clear();
                        client1.BaseAddress = new Uri("https://localhost:44356/api/OrderItems/PostOrderItem");
                        data = JsonConvert.SerializeObject(orderItem);
                        content = new StringContent(data, Encoding.UTF8, "application/json");
                        Res = await client1.PostAsync(client1.BaseAddress, content);
                        var OrderItem = Res.Content.ReadAsStringAsync().Result;
                    };

                    *//*PostOrderItem*/



                    /*collection["exampleInputEmail1"]*//*


                }
                ViewBag.Message = "You order has been placed. You will get confirmation email soon";
                return View("Payment");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Error occured during transaction! Please try again";
                return View("Payment");
            }
        }*/


        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DesiClothing4u.UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Customer Registration
        public ActionResult Index()
        {
            return View("~/Views/Register.cshtml");
        }

        // GET: Vendor Registration
        public ActionResult VendorRegister()
        {
            return View("~/Views/VendorRegister.cshtml");
        }


        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> Create(IFormCollection collection)
        {
            try

            {
                Address address = new Address
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Address1 = collection["StreetAddress1"],
                    Address2 = collection["StreetAddress2"],
                    City = collection["city"],
                    Email = collection["email"],
                    CreatedOnUtc = DateTime.UtcNow,
                    ZipPostalCode = collection["ZipCode"],
                    PhoneNumber = collection["phoneno"]
                };
                string output = JsonConvert.SerializeObject(address);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                var url = "https://localhost:44356/api/Addresses";
                /*var url = "https://localhost:44356/api/Addresses";*/

                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Address = response.Content.ReadAsStringAsync().Result;
                var BillingAddress1 = JsonConvert.DeserializeObject<Address>(Address);
                var BillingAddressId = BillingAddress1.Id;
                //ViewBag.SiteUsers = a;
                //PostAddress

                Customer customer = new Customer
                {
                    Username = collection["Email"],
                    Email = collection["Email"],
                    EmailToRevalidate = "",
                    SystemName = "",
                    BillingAddressId = BillingAddress1.Id,
                    ShippingAddressId = BillingAddress1.Id,
                    AdminComment = "",
                    IsTaxExempt = true,
                    AffiliateId = 0,
                    VendorId = 0,
                    HasShoppingCartItems = false,
                    RequireReLogin = false,
                    FailedLoginAttempts = 0,
                    Active = true,
                    Deleted = false,
                    IsSystemAccount = false,
                    LastIpAddress = "",
                    CreatedOnUtc = DateTime.UtcNow,
                    RegisteredInStoreId = 1,
                    Password = collection["Password"],
                };
                //CustomerPassword customerPassword = new CustomerPassword();
                //customerPassword.Password = collection["password"];
                //customerPassword.CreatedOnUtc = DateTime.UtcNow;
                //customer.CustomerPasswords.Add(customerPassword);
                //CustomerAddress customerAddress = new CustomerAddress();
                //customerAddress.AddressId = BillingAddress1.Id;
                //customerAddress.CustomerId = customer.Id;
                //customer.CustomerAddresses.Add(customerAddress);



                output = JsonConvert.SerializeObject(customer);
                 data = new StringContent(output, Encoding.UTF8, "application/json");
                url = "https://localhost:44356/api/Customers";
                /*url = "https://localhost:44356/api/Customers";*/
            
                client = new HttpClient();
                response = await client.PostAsync(url, data);
                var Customer = response.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Customer>(Customer);
                ViewBag.Customer = a;
                return View("~/Views/Home/Index", a);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Home/Index");
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
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

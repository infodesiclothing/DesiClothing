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
    public class VendorController : Controller
    {
        // GET: VendorController
        public ActionResult Index()
        {
            return View("~/Views/Shared/VendorRegister.cshtml");
            //return View("~/Views/Shared/VendorRegView.cshtml");

            //return View("VendorView");
        }

        // GET: VendorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Vendor>> Create(IFormCollection collection)
        {
            try
            {
                Address address = new Address
                {
                    FirstName = collection["Name"],
                    LastName = collection["Name"],
                    Address1 = collection["StreetAddress1"],
                    Address2 = collection["StreetAddress2"],
                    City = collection["city"],
                    Email = collection["email"],
                    CreatedOnUtc = DateTime.UtcNow,
                    ZipPostalCode = collection["ZipCode"],
                    PhoneNumber = collection["phoneno"]
                };
                //Post Address
                string output = JsonConvert.SerializeObject(address);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                var url = "https://localhost:44356/api/Addresses/PostAddress";
                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Address = response.Content.ReadAsStringAsync().Result;
                var BillingAddress1 = JsonConvert.DeserializeObject<Address>(Address);

                var BillingAddressId = BillingAddress1.Id;

                VendorProduct vendorproduct = new VendorProduct();


                //Post Vendor
                Vendor vendor = new Vendor
                {
                    Name = collection["name"],
                    Email = collection["Email"],
                    AddressId = BillingAddress1.Id,
                    Active = true,
                    Deleted = false,
                    password = collection["password"]
                    //, PictureId = PictureId1.Id
                };
                output = JsonConvert.SerializeObject(vendor);
                data = new StringContent(output, Encoding.UTF8, "application/json");
                url = "https://localhost:44356/api/Vendors";
                client = new HttpClient();
                response = await client.PostAsync(url, data);
                //Load Vendor
                var Vendor = response.Content.ReadAsStringAsync().Result;
                vendorproduct.Vendor = JsonConvert.DeserializeObject<Vendor>(Vendor);
                ViewBag.Vendor = vendorproduct.Vendor;
                ViewBag.VendorId = vendorproduct.Vendor.Id;
                //code to be corrected to insert vendor bank details
                var Vid = vendorproduct.Vendor.Id;
                VendorBankDetail vendorBankDetail = new VendorBankDetail {
                VendorId = Vid,
                AccHolderName = collection["accountholder"],
                BankName = collection["bankname"],
                BankAddress = collection["bankaddress"],
                SwiftCode = collection["swiftcode"],
                AccountNumber = collection["accountno"],
                AccountType = collection["accounttype"],
                    };
                output = JsonConvert.SerializeObject(vendorBankDetail);
                data = new StringContent(output, Encoding.UTF8, "application/json");
                url = "https://localhost:44356/api/Vendors/PostVendorBankDetail";
                client = new HttpClient();
                response = await client.PostAsync(url, data);
                //Load Vendor Bank details
                var vendorBDetail = response.Content.ReadAsStringAsync().Result;
                vendorproduct.VendorBankDetail = JsonConvert.DeserializeObject<VendorBankDetail>(vendorBDetail);
                ViewBag.VendorBankDetails = vendorproduct.VendorBankDetail;
                TempData["Vendormessage"] = "Vendor created, pls. login to add products";

                //Send Email to vendor
                var body = "";
                body = "<table border=0  width ='50%'>";
                body += "<tr><td align='center' colspan='2'><h1>Vendor Registration</h1></td></tr>";
                body += "<tr><td align='center' colspan='2'><hr></td></tr>";
                body += "<tr><td>You have been registered successfuly to <b>DesiClothingOnline.com</b></td><td>&nbsp;</td></tr>";
                body += "<tr><td align='center' colspan='2'><hr></td></tr>";
                body += "</table>";
                Email email = new Email();
                var tobesend = collection["Email"];
                
                email.Send(tobesend, "DesiClothingOnline Registration",body, "");
               

                //ViewBag.Vendormessage = "Vendor created, pls. login and add products";
                return RedirectToAction("Index", "Home");
                //return View("VendorView", vendorproduct);
            }
            catch(Exception e)
            {
                /*TempData["Vendormessage"] = e;*/
                return RedirectToAction("Index", "Home");
                /*return View("VendorView");*/
            }
        }
        [HttpPost]
        public async Task<ActionResult<Vendor>> CheckVendorLogin(IFormCollection collection)
            //string email,string pwd)
        {
            Vendor vendor = new Vendor();
            VendorProduct vendorproduct = new VendorProduct();
            var client = new HttpClient();
            //client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            //Sending request to find web api REST service resource PostSiteUsers using HttpClient  
            /*UriBuilder builder = new UriBuilder("https://localhost:44356/api/Vendors/ValidateVendor?");*/
            UriBuilder builder = new UriBuilder("https://localhost:44356/api/Vendors/ValidateVendor?");
            
            builder.Query = "email=" + collection["exampleInputEmail1"] + "&UserPassword=" + collection["exampleInputPassword1"];
            HttpResponseMessage Res = await client.GetAsync(builder.Uri);
            var Vendor = Res.Content.ReadAsStringAsync().Result;
            //var a = JsonConvert.DeserializeObject<Vendor>(Vendor);
            vendorproduct.Vendor = JsonConvert.DeserializeObject<Vendor>(Vendor);
            ViewBag.Vendor = vendorproduct.Vendor;
            ViewBag.VendorId = vendorproduct.Vendor.Id/* a.Id*/;
            var client1 = new HttpClient();
            //Load Products of that vendor only along with picture once productpicturemapping table is populated
            /*UriBuilder builder1 = new UriBuilder("https://localhost:44356/api/Products/GetProductByVendor1?");*/
            UriBuilder builder1 = new UriBuilder("https://localhost:44356/api/Products/GetProductByVendor1?");
            
            builder1.Query = "VendorId=" + vendorproduct.Vendor.Id;
            HttpResponseMessage Prodresponse = await client1.GetAsync(builder1.Uri);
            var Products = Prodresponse.Content.ReadAsStringAsync().Result;
            vendorproduct.ProductByVendor = JsonConvert.DeserializeObject<ProductByVendor[]>(Products);

            //Load order items of the vendor
            /*UriBuilder builderOrder = new UriBuilder("https://localhost:44356/api/Vendors/GetOrderitemsByVendor?");*/
            UriBuilder builderOrder = new UriBuilder("https://localhost:44356/api/Vendors/GetOrderitemsByVendor?");
            builderOrder.Query = "VendorId=" + vendorproduct.Vendor.Id;
            HttpResponseMessage Orderresponse = await client1.GetAsync(builderOrder.Uri);
            var Orderitems = Orderresponse.Content.ReadAsStringAsync().Result;
            vendorproduct.CustOrderItems = JsonConvert.DeserializeObject<CustOrderItems[]>(Orderitems);

            return View("VendorView", vendorproduct);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesiClothing4u.Common.Models;
using System.Web;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace DesiClothing4u.UI.Controllers
{
    //private autocarrsContext db = new autocarrsContext();
    public class VendorProductController : Controller
    {
        // GET: VendorProductController
        public ActionResult Index()
        {
            return View();
        }
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string output;

        public VendorProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        // This method will upload the image and data to the folder and database
        [HttpPost]
        public async Task<ActionResult> IndexAsync(List<IFormFile> file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string path = Path.Combine(webRootPath, "~/ProductImages/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Getting File Details
            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in file)
            {
                var Extension = Path.GetExtension(postedFile.FileName);
                var fileName = Path.GetFileName(postedFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                
                //Saving file to Folder
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
                //Saving data to database
                VendorProduct vendorproduct = new VendorProduct();
                Picture picture = new Picture
                {
                    MimeType = "Jpg",
                    SeoFilename = fileName,
                    AltAttribute = "",
                    TitleAttribute = "",
                    IsNew = true,
                    VirtualPath = path
                };
                output = JsonConvert.SerializeObject(picture);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                /*var url = "https://localhost:44356/api/Pictures";*/
                var url = "https://localhost:44356/api/Pictures";
                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Pics = response.Content.ReadAsStringAsync().Result;
                vendorproduct.Picture = JsonConvert.DeserializeObject<Picture[]>(Pics);
            }
           
            return RedirectToAction("Index");
            
            
        }
        // GET: VendorProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorProductController/Create
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

        // GET: VendorProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VendorProductController/Edit/5
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

        // GET: VendorProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VendorProductController/Delete/5
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

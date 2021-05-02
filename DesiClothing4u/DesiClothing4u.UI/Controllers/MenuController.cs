using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesiClothing4u.Common.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace DesiClothing4u.UI.Controllers
{
    public class MenuController : Controller
    {
        // GET: MenuController
        public async Task<ActionResult<IEnumerable<productExt>>> Index(int CatId)

        {
            LoadProductExt load = new LoadProductExt();
            //Featured--field name MarkAsNew
            var clientF = new HttpClient();
            var urlF = "https://localhost:44356/api/Products/GetProductsByCat?CatId=" + CatId;
            var responseF = await clientF.GetAsync(urlF);
            var ProductExts = responseF.Content.ReadAsStringAsync().Result;
            load.ProductExts = JsonConvert.DeserializeObject<productExt[]>(ProductExts);
            return View("ProductsByCat",load);
        }
    }
}

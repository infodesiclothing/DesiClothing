using System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Hosting.Internal;
using System.Linq;

namespace DesiClothing4u.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : Controller
    {
        private readonly desiclothingContext _context;
        /*[Obsolete]*/
        private readonly IWebHostEnvironment _hostingEnvironment;
       
        public PicturesController(desiclothingContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: PicturesController
        [HttpGet("Index")]
        public ActionResult Index()
        {
            return View();
        }
        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            return picture;
        }

        // GET: api/Products/5
        [HttpGet("GetProductPicture")]
        public async Task<ActionResult<IEnumerable<Picture>>> GetProductPicture(int Id)
        {
            var picture = await _context.Pictures.Where(a => a.ProductId == Id
                            )
                             .ToListAsync();
            return picture;
        }
        // GET: api/Pictures/5
        [HttpGet("GetPictureByProduct")]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictureByProduct(int ProductId)
        {
            var Picture = await _context.Pictures.Where(a => a.ProductId == ProductId).ToListAsync();
            return Picture;
        }
        //GET: api/PostPicture 
        [HttpPost("PostPicture")]
        [Obsolete]
        public async Task<ActionResult<Picture>> PostPicture(List<IFormFile> file, IFormCollection Id)
        {
            string projectRootPath = _hostingEnvironment.WebRootPath;
            string projectRootPath1 = _hostingEnvironment.ContentRootPath;
            Console.Write(projectRootPath);
            Console.Write(projectRootPath1);
            /*string projectRootPath = _hostingEnvironment.ContentRootPath;*/

            /*projectRootPath = projectRootPath.Replace("DesiClothing4u.API", "DesiClothing4u.UI");*/
            projectRootPath = projectRootPath.Replace("testapi.desiclothingonline.com", "test.desiclothingonline.com");
            
           //if (string.IsNullOrWhiteSpace(projectRootPath))
           // {
           //     projectRootPath = Directory.GetCurrentDirectory();
           // }
            /*string path = "/wwwroot/ProductImages/";*/
            string path = "/ProductImages/";
            string path_virtual= "~/ProductImages/";
            var id = Id["Id"];
            //if (!Directory.Exists(projectRootPath+ path))
            //{
            //    Directory.CreateDirectory(projectRootPath+path);
            //}
            //Getting File Details
            List<string> uploadedFiles = new List<string>();
            int PicId = 0;
            foreach (IFormFile postedFile in file)
            {
                var Extension = Path.GetExtension(postedFile.FileName);
                var fileName = Path.GetFileName(postedFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;

                //Saving file to Folder
                using (FileStream stream = new FileStream(Path.Combine(projectRootPath + path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
                var ProdId = Convert.ToInt32(id.ToString());
                //Saving data to database
                Picture picture = new Picture
                {
                    MimeType = "Jpg",
                    SeoFilename = fileName,
                    AltAttribute = "",
                    TitleAttribute = "",
                    IsNew = true,
                    VirtualPath = path_virtual,
                    ProductId = ProdId
                };
                _context.Pictures.Add(picture);
               
                await _context.SaveChangesAsync();
                PicId = picture.Id;
                //code to insert picture and product mapping
                var pid = Convert.ToInt32(id.ToString());
                ProductPictureMapping map = new ProductPictureMapping
                {
                    ProductId = pid
                };
                map.PictureId = picture.Id;
                map.DisplayOrder = 0;
                _context.ProductPictureMappings.Add(map);
                await _context.SaveChangesAsync();
            }
            return await PostPictureJson(PicId);
        }
        private async Task<ActionResult<Picture>> PostPictureJson(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        //private string GetPathAndFilename(string filename)
        //{
        //    return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
        //}

        // POST: PicturesController/Create
        [HttpPost("Create")]
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

        // POST: PicturesController/Edit/5
        [HttpPost("Edit")]
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

        // Post: api/Pictures
        [HttpPost("DeletePicture1")]
            public async Task<ActionResult<Picture>> DeletePicture1([FromBody] dynamic myproduct)
        {
            var sProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(myproduct.ToString());
            int Id = sProduct.id;
            var picture = await _context.Pictures.FindAsync(Id);
            if (picture == null)
            {
                return NotFound();
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return picture;
        }
    }
}

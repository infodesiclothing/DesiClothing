using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesiClothing4u.Common.Models;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace DesiClothing4u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly desiclothingContext _context;
        //private desiclothingContext db = new desiclothingContext();
        private readonly IHostingEnvironment _hostingEnvironment;
        public OrderItemsController(desiclothingContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("PutOrderItemAccept")]
        public async Task<ActionResult<CompletedOrder>> PutOrderItemAccept([FromBody] OrderItem orderitem)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderitem.Id);
            if (orderItem == null)
            {
                return NotFound();
            }
            
            orderItem.IsAccepted = orderitem.IsAccepted;
            orderItem.ETA = orderitem.ETA;
            await _context.SaveChangesAsync();
            Order order = new Order();
            //Code to send email
            var order1 = _context.CompletedOrder
                .FromSqlRaw("Execute dbo.CheckOrderCompleteStatus {0}", orderitem.Id);
            var data = JsonConvert.SerializeObject(order1);
            CompletedOrder[] a =JsonConvert.DeserializeObject<CompletedOrder[]>(data);

            var OrdersController = new OrdersController(_context);
            var b = OrdersController.GetOrderInvoice(a[0].OrderId);
            string EmailBody = "";

            if (b != null)
            {
                EmailBody = "<table border=0  width ='50%'>";
                EmailBody += "<tr><td align='center' colspan='2'><h1>Order Details</h1></td></tr>";
                EmailBody += "<tr><td align='center' colspan='2'><hr></td></tr>";
                EmailBody += "<tr><td align='left'><b>Order Id</b></td><td align='left'>13</td></tr>";
                EmailBody += "<tr><td align='left'><b>Order Date</b></td><td align='left'>2020-12-29 01:10:16.3124552</td></tr>";
                EmailBody += "<tr><td align='center' colspan='2'><br>";
                EmailBody += "<table border='0' width='98%'>";
                EmailBody += "<tr><td><b>Product</b></td><td><b>Price</b></td><td><b>Qauntity</b></td><td><b>Total</b></td><td><b>ETA</b></td></tr>";
                EmailBody += "<tr><td align='center' colspan='5'><hr></td></tr>";
                //loop here
                decimal Total = 0;
                foreach (var v1 in b.OrderItems)
                {
                    Total = Total + v1.PriceInclTax * v1.Quantity;
                    EmailBody += "<tr><td>" + v1.ProductId + "</td><td>" + v1.PriceInclTax + "</td><td>" + v1.Quantity +"</td><td>" + v1.PriceInclTax * v1.Quantity + "</td><td>" + v1.ETA + "</td></tr>";
                }
                EmailBody += "<tr><td align='center' colspan='5'><hr></td></tr>";
                EmailBody += "<tr><td align='center' colspan='3'>&nbsp;</td><td colspan='2'>"+ Total + "</td></tr>";
                EmailBody += "<tr><td align='center' colspan='5'><hr></td></tr>";
                EmailBody += "</table></td></tr></table>";


                MailMessage message = new MailMessage();
                message.From = new MailAddress("info@desiclothingonline.com");
                message.To.Add("mohtashim.siddiqui74@outlook.com");
                message.Bcc.Add("info@desiclothingonline.com");
                message.Subject = "Order details";
                var Message = EmailBody;
                Message += "<br><br>";

                message.Body = Message;
                message.IsBodyHtml = true;

                //var client = new SmtpClient("win10.tmd.cloud", 465)
                //{
                //    UseDefaultCredentials = true,
                //    Credentials = new NetworkCredential("info@desiclothingonline.com", "Need4speed!@"),
                //    EnableSsl = true,
                    
                //};
                var client = new SmtpClient
                {
                    Host = "win10.tmd.cloud",
                    Port = 465,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(message.From.ToString(), "Need4speed!@")
                };
                client.Send(message);

                //return RedirectToAction("Index");

            }
            return NoContent();
        }


        [HttpPut("PutOrderItemReject")]
        public async Task<IActionResult> PutOrderItemReject([FromBody] OrderItem orderitem)
        {
            /*            if (id != id)
                        {
                            return BadRequest();
                        }
            */
            //_context.Entry(orderitem).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

            var orderItem = await _context.OrderItems.FindAsync(orderitem.Id);
            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.IsAccepted = orderitem.IsAccepted;
            orderItem.RejectedReason = orderitem.RejectedReason;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("PostInvoice")]
        [Obsolete]
        public async Task<ActionResult<OrderItem>> PostInvoice(List<IFormFile> file, IFormCollection collection)
        {
            //string webRootPath = _webHostEnvironment.WebRootPath;
            string projectRootPath = _hostingEnvironment.ContentRootPath;

            projectRootPath = projectRootPath.Replace("DesiClothing4u.API", "DesiClothing4u.UI");
            if (string.IsNullOrWhiteSpace(projectRootPath))
            {
                projectRootPath = Directory.GetCurrentDirectory();
            }
            string path = "/wwwroot/Invoices/";
            string path_virtual = "~/Invoices/";
            var id = collection["Id"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Getting File Details
            List<string> uploadedFiles = new List<string>();
            
            foreach (IFormFile postedFile in file)
            {
                var Extension = Path.GetExtension(postedFile.FileName);
                var fileName = Path.GetFileName(postedFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "OIN" + id  + Extension;

                //Saving file to Folder
                using (FileStream stream = new FileStream(Path.Combine(projectRootPath + path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
                var orderItem = await _context.OrderItems.FindAsync(int.Parse(id));
                if (orderItem == null)
                {
                    return NotFound();
                }
                var dt = collection["ShipmentDate"];
                orderItem.AirWaybilNo = collection["AirWaybilNo"];
                orderItem.ShipmentDate = DateTime.Parse(collection["ShipmentDate"]);
                orderItem.InvoiceUrl = fileName;

                await _context.SaveChangesAsync();
            }
            return NoContent(); 
        }


        // POST: api/OrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("PostOrderItem")]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}

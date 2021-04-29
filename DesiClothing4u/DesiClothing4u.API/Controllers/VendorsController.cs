using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesiClothing4u.Common.Models;
using Microsoft.Data.SqlClient;
//using Microsoft.AspNetCore.Cors;

namespace DesiClothing4u.API.Controllers
{
    //[EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly desiclothingContext _context;

        public VendorsController(desiclothingContext context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }
        // Post: api/VendorBankDetail
        [HttpPost("PostVendorBankDetail")]
        public async Task<ActionResult<VendorBankDetail>> PostVendorBankDetail(VendorBankDetail vendorBankDetails)
        {
            _context.VendorBankDetails.Add(vendorBankDetails);
            await _context.SaveChangesAsync();
           return CreatedAtAction("GetVendorBankDetail", new { id = vendorBankDetails.Id }, vendorBankDetails);
        }

       // GET: api/GetVendorBankDetail/5
        [HttpGet("GetVendorBankDetail")]
        public async Task<ActionResult<VendorBankDetail>> GetVendorBankDetail(int id)
        {
            var vendorBankDetail = await _context.VendorBankDetails.FindAsync(id);

            if (vendorBankDetail == null)
            {
                return NotFound();
            }

            return vendorBankDetail;
        }

        // GET: api/GetVendorBankDetail/5
        [HttpGet("checkvendoremail")]
        public async Task<ActionResult<IEnumerable<Vendor>>> checkvendoremail(string VEmail)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@Email", VEmail);
                var vendor = _context.Vendors
                .FromSqlRaw("Execute dbo.CheckVendorEmail @Email ", param1)
                .ToList();
                return vendor;
            }
            catch (Exception e)
            {
                /*incase of no category*/
                return null;
            }
        }
        // PUT: api/Vendors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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
        
        // POST: api/Vendors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendor>> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return vendor;
        }

        [HttpGet("ValidateVendor")]
        public ActionResult<Vendor> ValidateVendor(string email, string UserPassword)
        {
            bool VendorExists;
            VendorExists = _context.Vendors.Any(e => e.Email == email && e.password == UserPassword);

            if (VendorExists == false)
            {
                return NotFound();
            }

            //var siteUsers = await _context.Vendors.FindAsync(email);

            var siteUsers = _context.Vendors.SingleOrDefault(e => e.Email == email);
            return siteUsers;
           
            
        }
        
        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
        //added by SM on Dec 28, 2020
        [HttpGet("GetOrderitemsByVendor")] 
        public IEnumerable<CustOrderItems> GetOrderitemsByVendor(int VendorId)
        {
            try
            {
                var orderitem = _context.CustOrderItems
                .FromSqlRaw("Execute dbo.GetOrderItems {0}", VendorId)
                .ToList();

                return orderitem;
            }
            catch (Exception e)
            {
                /*if the vendor dont have any product*/
                return null;
            }

        }

    }
}

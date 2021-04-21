using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesiClothing4u.Common.Models;
using DesiClothing4u.Common.Interfaces;

namespace DesiClothing4u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly desiclothingContext _context;
        //private ICustomer _Customer;

        public CustomersController(desiclothingContext context)
        {
            _context = context;
            //_Customer = CustomerService;
        }
        //Added by Mohtashim on Nov 29, 2020
        [HttpGet("ValidateCustomer")]
        public ActionResult<Customer> ValidateCustomer(string email, string UserPassword)
        {
            bool CustomeExists;
            CustomeExists = _context.Customers.Any(e => e.Email == email && e.Password == UserPassword);

            if (CustomeExists == false)
            {
                return NotFound();
            }


            var Customer = _context.Customers.SingleOrDefault(e => e.Email == email);
            return Customer;

        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }
        // GET: api/Customers
        [HttpGet("LoginID")]
        public async Task<ActionResult<Customer>> LoginID(int UserId)
        {
            bool UserExists;
            UserExists = _context.Customers.Any(e => e.Id == UserId);
            if (UserExists == false)
            {
                return NotFound();
            }
            var customers = await _context.Customers.FindAsync(UserId);
            return customers;
        }
        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}

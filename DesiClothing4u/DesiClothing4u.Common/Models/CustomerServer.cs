using System;
using System.Collections.Generic;
using System.Text;
using DesiClothing4u.Common.Interfaces;


namespace DesiClothing4u.Common.Models
{
    public class CustomerServer : ICustomer

    {
        public bool CheckLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Customer Register(Customer cust)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DesiClothing4u.Common.Models;

namespace DesiClothing4u.Common.Interfaces
{
    public interface ICustomer
    {
        bool CheckLogin(string email, string password);
        Customer Register(Customer cust);
    }
}

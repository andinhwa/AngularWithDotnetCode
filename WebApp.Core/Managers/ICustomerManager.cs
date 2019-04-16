using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.Managers {
    public interface ICustomerManager {
        Task<IList<Customer>> GetAllAsync ();
        Task<Customer> GetByIdAsync (Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.DBContexts.Repositories {
    internal interface ICustomerRepository : IGenericRepository<Customer> {
        Task<IList<Customer>> GetAllAsync ();
        Task<Customer> GetByIdAsync (Guid id);
        Task<IList<Customer>> SearchAsync (string searchKey);
    }
}
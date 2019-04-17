using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.Models;

namespace WebApp.Core.DBContexts.Repositories.Impl {
    internal class CustomerRepository : GenericRepository<Customer>, ICustomerRepository {
        public CustomerRepository (WebAppContext context) : base (context) { }

        public async Task<IList<Customer>> GetAllAsync () {
            return await dbSet.ToListAsync ();
        }

        public Task<Customer> GetByIdAsync (Guid id) {
            return dbSet.FirstOrDefaultAsync (c => c.Id == id);
        }

        public async Task<IList<Customer>> SearchAsync (string searchKey) {
            return await dbSet.Where (c => c.FirstName.Contains (searchKey) ||
                    c.Address.Contains (searchKey) ||
                    c.Email.Contains (searchKey))
                .ToListAsync ();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.Models;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class CustomerRepository:  GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(WebAppContext context) : base(context)
        {
        }

        public async Task<IList<Customer>> GetAllAsync(){
            return await dbSet.ToListAsync();
        }

        public async Task<Customer> GetById(Guid id){
            return await dbSet.FirstOrDefaultAsync(c =>c.Id == id);
        }

        public Task<Customer> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}

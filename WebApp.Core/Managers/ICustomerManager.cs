using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;
using WebApp.DataStructure;

namespace WebApp.Core.Managers {
    public interface ICustomerManager {
        Task<IList<CustomerViewModel>> GetAllAsync ();
        Task<CustomerViewModel> GetByIdAsync (Guid id);
        Task<CustomerViewModel> AddAsync (CustomerViewModel customervmd);
        Task<CustomerViewModel> AddOrUpdateAsync(CustomerViewModel customer);
        Task<CustomerViewModel> UpdateAsync (CustomerViewModel customervmd);
        Task<bool> DeleteAsync(Guid id);
        Task<IList<CustomerViewModel>> SearchAsync (string searchKey);
        
    }
}
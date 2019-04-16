using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Core.DBContexts.Repositories;
using WebApp.Core.Models;

namespace WebApp.Core.Managers.Impl {
    internal class CustomerManager : ICustomerManager {
        private readonly ICustomerRepository _customerRepository;
        public CustomerManager (ICustomerRepository customerManager) {
            _customerRepository = customerManager;
        }
        public Task<IList<Customer>> GetAllAsync () {
            return _customerRepository.GetAllAsync ();
        }

        public Task<Customer> GetByIdAsync (Guid id) {
            return _customerRepository.GetByIdAsync (id);
        }
    }

}
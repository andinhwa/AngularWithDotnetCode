using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApp.Core.DBContexts.Repositories;
using WebApp.Core.Models;
using WebApp.DataStructure;

namespace WebApp.Core.Managers.Impl {
    internal class CustomerManager : ICustomerManager {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        public CustomerManager (
            ICustomerRepository customerManager,
            IMapper mapper,
            IUnitOfWorkRepository unitOfWorkRepository) {
            _customerRepository = customerManager;
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<IList<CustomerViewModel>> GetAllAsync () {
            return _mapper.Map<IList<CustomerViewModel>> (await _customerRepository.GetAllAsync ());
        }

        public async Task<CustomerViewModel> GetByIdAsync (Guid id) {
            return _mapper.Map<CustomerViewModel> (await _customerRepository.GetByIdAsync (id));
        }

        public async Task<bool> AddAsync (CustomerViewModel customervmd) {
            Customer customer = _mapper.Map<Customer> (customervmd);
            _customerRepository.Add (customer);
            await _unitOfWorkRepository.SaveChangesAsync ();
            return true;
        }

        public async Task<bool> UpdateAsync (CustomerViewModel customervmd) {
            var customer = await _customerRepository.GetByIdAsync (customervmd.Id.Value);
            customer = _mapper.Map (customervmd, customer);
            _customerRepository.Update (customer);
            await _unitOfWorkRepository.SaveChangesAsync ();
            return true;
        }

        public async Task<IList<CustomerViewModel>> SearchAsync (string searchKey) {

            return _mapper.Map<IList<CustomerViewModel>> (await _customerRepository.SearchAsync (searchKey));
        }

        public async Task<bool> DeleteAsync (Guid id) {
            var customer = await _customerRepository.GetByIdAsync (id);
            _customerRepository.Remove (customer);
            await _unitOfWorkRepository.SaveChangesAsync ();
            return true;
        }
    }

}
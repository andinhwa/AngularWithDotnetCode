using AutoMapper;
using WebApp.Core.Models;
using WebApp.DataStructure;

namespace WebApp.Core.MappingProfiles {
    public class ModelsMappingProfile : Profile {
        public ModelsMappingProfile () {
            CreateMap<Customer, CustomerViewModel> ();
            CreateMap<CustomerViewModel, Customer> ()
               .ForMember(c => c.Id, o => o.Ignore());
        }
    }
}
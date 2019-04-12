using AutoMapper;
using WebApp.API.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.API.MappingProfiles
{
    public class UserMapping: Profile
    {
        public UserMapping(){
            CreateMap<User, UserViewModel>();
        }
    }
}
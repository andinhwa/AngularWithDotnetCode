using Microsoft.AspNetCore.Identity;
using System;
using System.Runtime.Serialization;
namespace WebApp.Core.Models.Identity
{  
    [Serializable]
    public class UserClaim : IdentityUserClaim<Guid>, IBaseEntity, ISerializable
    {
        public UserClaim()
        {
        }

        protected UserClaim(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(UserId), UserId);
            info.AddValue(nameof(ClaimType), ClaimType);
            info.AddValue(nameof(ClaimValue), ClaimValue);            
        }
    }
}
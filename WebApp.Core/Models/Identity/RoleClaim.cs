using Microsoft.AspNetCore.Identity;
using System;
using System.Runtime.Serialization;

namespace WebApp.Core.Models.Identity
{
   [Serializable]
    public class RoleClaim : IdentityRoleClaim<Guid>, IBaseEntity, ISerializable
    {
        public RoleClaim()
        {
        }

        protected RoleClaim(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(RoleId), RoleId);
            info.AddValue(nameof(ClaimType), ClaimType);
            info.AddValue(nameof(ClaimValue), ClaimValue);
        }
    }
}
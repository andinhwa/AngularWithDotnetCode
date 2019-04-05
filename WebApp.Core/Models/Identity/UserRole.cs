using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Core.Models.Identity
{
     [Serializable]
    public class UserRole : IdentityUserRole<Guid>, IBaseEntity, ISerializable
    {
        public UserRole()
        {
        }

        protected UserRole(SerializationInfo info, StreamingContext context)
        {

        }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(UserId), UserId);
            info.AddValue(nameof(RoleId), RoleId);
        }
    }
}
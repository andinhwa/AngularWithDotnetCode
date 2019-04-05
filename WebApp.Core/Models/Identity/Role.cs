using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
namespace WebApp.Core.Models.Identity {
    [Serializable]
    public class Role : IdentityRole<Guid>, IBaseEntity, ISerializable {
        public Role () { }

        public Role (string roleName) : base (roleName) { }

        protected Role (SerializationInfo info, StreamingContext context) {

        }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue (nameof (Id), Id);
            info.AddValue (nameof (Name), Name);
            info.AddValue (nameof (NormalizedName), NormalizedName);
            info.AddValue (nameof (ConcurrencyStamp), ConcurrencyStamp);
        }
    }
}
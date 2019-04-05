using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Core.Models.Identity
{
   [Serializable]
    public class User : IdentityUser<Guid>, IBaseEntity, ISerializable
    {
        public User() : base()
        {

        }

        public User(string userName) : base(userName)
        {

        }

        protected User(SerializationInfo info, StreamingContext context)
        {

        }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public int? TimeOffset { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
       
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(FirstName), FirstName);
            info.AddValue(nameof(LastName), LastName);
            info.AddValue(nameof(TimeOffset), TimeOffset);
            info.AddValue(nameof(LockoutEnd), LockoutEnd);
            info.AddValue(nameof(TwoFactorEnabled), TwoFactorEnabled);
            info.AddValue(nameof(PhoneNumberConfirmed), PhoneNumberConfirmed);
            info.AddValue(nameof(PhoneNumber), PhoneNumber);
            info.AddValue(nameof(ConcurrencyStamp), ConcurrencyStamp);
            info.AddValue(nameof(SecurityStamp), SecurityStamp);
            info.AddValue(nameof(PasswordHash), PasswordHash);
            info.AddValue(nameof(EmailConfirmed), EmailConfirmed);
            info.AddValue(nameof(NormalizedEmail), NormalizedEmail);
            info.AddValue(nameof(Email), Email);
            info.AddValue(nameof(NormalizedUserName), NormalizedUserName);
            info.AddValue(nameof(UserName), UserName);
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(LockoutEnabled), LockoutEnabled);
            info.AddValue(nameof(AccessFailedCount), AccessFailedCount);
        }

    }
}
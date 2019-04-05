using Microsoft.AspNetCore.Identity;
using System;
using System.Runtime.Serialization;


namespace WebApp.Core.Models.Identity
{
   [Serializable]
    public class UserToken : IdentityUserToken<Guid>, IBaseEntity, ISerializable
    {
        public UserToken()
        {
        }

        protected UserToken(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(UserId), UserId);
            info.AddValue(nameof(LoginProvider), LoginProvider);
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Value), Value);
        }
    }
}
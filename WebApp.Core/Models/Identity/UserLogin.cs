using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
namespace WebApp.Core.Models.Identity {
    [Serializable]
    public class UserLogin : IdentityUserLogin<Guid>, IBaseEntity, ISerializable {
        public UserLogin () { }

        protected UserLogin (SerializationInfo info, StreamingContext context) {

        }

        public void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue (nameof (LoginProvider), LoginProvider);
            info.AddValue (nameof (ProviderKey), ProviderKey);
            info.AddValue (nameof (ProviderDisplayName), ProviderDisplayName);
            info.AddValue (nameof (UserId), UserId);
        }
    }
}
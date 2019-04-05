using System;
using System.Collections.Generic;
using WebApp.Core.Enums;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.Models
{
    [Serializable]
    public class Client : IBaseEntity
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }       
        public string AllowedOrigin { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
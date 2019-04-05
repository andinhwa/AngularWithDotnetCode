using System;

namespace WebApp.Core.Models.Identity
{
   [Serializable]
    public class RefreshToken: IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string RefreshTokenId { get; set; }
        public string Subject { get; set; }       
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
        public virtual Client Client { get; set; }
    }
}
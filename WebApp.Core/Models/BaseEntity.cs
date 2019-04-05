using System;
using WebApp.Core.Enums;

namespace WebApp.Core.Models
{
     [Serializable]
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public RecordStatus RecordStatus { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Template.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CreatorId))]
        public virtual ApplicationUser CreatedBy { get; set; }
    }
}

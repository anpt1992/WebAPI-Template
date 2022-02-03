using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Template.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public Guid? CreatorId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }

        [ScaffoldColumn(false)]
        public Guid? UpdaterId { get; set; }

        [ScaffoldColumn(false)]
        public byte SoftDeleteLevel { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public virtual ApplicationUser CreatedBy { get; set; }
        [ForeignKey(nameof(UpdaterId))]
        public virtual ApplicationUser UpdatedBy { get; set; }
    }
}

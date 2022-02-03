using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Template.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string? CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string? UpdatedBy { get; set; }

        [ScaffoldColumn(false)]
        public byte SoftDeleteLevel { get; set; }
    }

}

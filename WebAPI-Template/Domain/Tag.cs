using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Domain
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public virtual ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

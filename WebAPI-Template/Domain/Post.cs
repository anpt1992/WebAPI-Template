using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Domain
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
                
        public virtual List<PostTag> Tags { get; set; }

        [ForeignKey(nameof(UserId))]      
        public virtual ApplicationUser CreatedBy { get; set; }

    }
}

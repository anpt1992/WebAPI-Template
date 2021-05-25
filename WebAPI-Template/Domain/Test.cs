using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Domain
{
    public class Test
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}

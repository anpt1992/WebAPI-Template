using System.Collections.Generic;

namespace WebAPI_Template.Domain
{
    public class Post: BaseEntity
    {        
        public string Name { get; set; }

        public string UserId { get; set; }
                
        public virtual List<PostTag> Tags { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DAL.Model
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [InverseProperty("Post")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogServices.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }
        [Key]
        public int TagID { get; set; }
        [Required]
        [MaxLength(100)]
        public string TagName { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}

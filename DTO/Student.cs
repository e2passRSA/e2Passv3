using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(4000)]
        public string Bio { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        [StringLength(256)]
        public virtual string EmailAddress { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}

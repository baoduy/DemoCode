using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(400)]
        [Required]
        public string Name { get; set; }

        [MaxLength(400)]
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [MaxLength(400)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }


    }
}
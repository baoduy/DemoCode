using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Models
{
    public class ViewModel
    {
        [Key]
        public int Id { get; set; }


        [StringLength(400)]
        [Required]
        public string Name { get; set; }

        [StringLength(400)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(400)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }
}
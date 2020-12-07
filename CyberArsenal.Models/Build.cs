using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CyberArsenal.Models
{
    public class Build
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Build name must be within 1-30 characters")]
        [Display(Name = "Build Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Processor must be within 1-40 characters")]
        [Display(Name = "Processor")]
        public string Cpu { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Graphics card must be within 1-40 characters")]
        [Display(Name = "Graphics Card")]
        public string Gpu { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Memory must be within 1-40 characters")]
        [Display(Name = "Memory")]
        public string Ram { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Storage must be within 1-40 characters")]
        [Display(Name = "Storage")]
        public string Storage { get; set; }

        [MaxLength(40, ErrorMessage = "Secondary Storage is no more than 40 characters")]
        [Display(Name = "Secondary Storage")]
        public string StorageSecondary { get; set; }

        [MaxLength(40, ErrorMessage = "Mother Board is no more than 40 characters")]
        [Display(Name = "Mother Board")]
        public string MotherBoard { get; set; }

        [MaxLength(40, ErrorMessage = "Power Supply is no more than 40 characters")]
        [Display(Name = "Power Supply")]
        public string PowerSupply { get; set; }

        [MaxLength(40, ErrorMessage = "Case is no more than 40 characters")]
        [Display(Name = "Case")]
        public string Case { get; set; }

        [MaxLength(500, ErrorMessage = "Description is no more than 500 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
/*
TODO:
ADD:
    User
    Date created
    Private
    Score
    Droplist for all components
CHANGE:
    Description max size from 500 to 200
 */
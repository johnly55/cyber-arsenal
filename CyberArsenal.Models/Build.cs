using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberArsenal.Models
{
    public class Build
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Build name must be within 1-30 characters")]
        [Display(Name = "Build Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Processor must be within 1-40 characters")]
        [Display(Name = "Processor")]
        public string CpuName { get; set; }

        public int? CpuId { get; set; }

        [ForeignKey("CpuId")]
        public Part Cpu { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Graphics card must be within 1-40 characters")]
        [Display(Name = "Graphics Card")]
        public string GpuName { get; set; }

        public int? GpuId { get; set; }

        [ForeignKey("GpuId")]
        public Part Gpu { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Memory must be within 1-40 characters")]
        [Display(Name = "Memory")]
        public string RamName { get; set; }

        public int? RamId { get; set; }

        [ForeignKey("RamId")]
        public Part Ram { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Storage must be within 1-40 characters")]
        [Display(Name = "Storage")]
        public string StorageName { get; set; }

        public int? StorageId { get; set; }

        [ForeignKey("StorageId")]
        public Part Storage { get; set; }

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

        public int Score { get; set; }

        [Display(Name = "Date Created")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Make Private?")]
        public bool Private { get; set; }

        //prop Authorized
    }
}
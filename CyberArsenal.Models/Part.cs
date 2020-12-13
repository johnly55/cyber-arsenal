using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CyberArsenal.Models
{
    public class Part
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Component Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Component Name")]
        public string Name { get; set; }

        [Display(Name = "Benchmark Type")]
        public string Benchmark { get; set; }

        [Display(Name = "Benchmark Score")]
        public int Score { get; set; }

        [Required]
        [Display(Name = "MSRP Price")]
        public double Price { get; set; }

        [Display(Name = "Release Date")]
        public string ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Reference")]
        public bool Reference { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CyberArsenal.Models.ViewModels
{
    public class BuildViewModel
    {
        public int CpuId { get; set; }

        public int GpuId { get; set; }

        public int RamId { get; set; }

        public int StorageId { get; set; }

        public int Page { get; set; }
    }
}
using LoadBalancer.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Domain.Entity
{
    public class Matrix
    {
        [Key]
        public Guid Guid { get; set; }
        public string? UserEmail { get; set; } = string.Empty;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public double? Progress { get; set; } = 0;
        public string? GeneratedMatrix { get; set; } = string.Empty;
        public HeavyTaskStatus Status { get; set; }
    }
}

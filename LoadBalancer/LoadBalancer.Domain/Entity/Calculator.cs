using LoadBalancer.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Domain.Entity
{
    public class Calculator
    {
        [Key]
        public long Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Result { get; set; }
        public HeavyTaskStatus Status { get; set; }
    }
}

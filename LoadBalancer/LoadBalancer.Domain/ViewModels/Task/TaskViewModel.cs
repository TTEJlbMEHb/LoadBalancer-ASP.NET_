using LoadBalancer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Domain.ViewModels.Task
{
    public class TaskViewModel
    {
        public Matrix Matrix { get; set; }
        public LoadBalancer.Domain.Entity.User User { get; set; }
    }
}

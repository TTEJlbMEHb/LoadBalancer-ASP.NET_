using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Domain.Enum
{
    public enum HeavyTaskStatus
    {
        INIT,
        RUN,
        SUCCESS,
        FAIL
    }
}

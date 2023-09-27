using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class MachineFault
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FaultName { get; set; }
        public double FaultDuration { get; set; }
    }
}

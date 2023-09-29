using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Faults
{
    public class FaultDto
    {
        public int FaultId { get; set; }
        public required string Name { get; set; }
        public string Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Description { get; set; }
        public bool IsResolved { get; set; }
        public int MachineId { get; set; }
    }

}

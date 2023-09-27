using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Faults
    {
        public int FaultId { get; set; }
        public required string Name { get; set; }
        public int Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Description { get; set; }
        public bool IsResloved { get; set; }
        public int MachineId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class MalfunctionDto
    {
        public required string Name { get; set; }
        //public string MachineName { get; set; }
        public int Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public required string Description { get; set; }
        public bool IsResloved { get; set; }

        public Guid MachineId { get; set; }
    }
}

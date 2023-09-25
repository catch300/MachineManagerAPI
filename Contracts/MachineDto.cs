using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class MachineDto
    {
        
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required IEnumerable<MalfunctionDto> Malfunctions { get; set; }
    }
}

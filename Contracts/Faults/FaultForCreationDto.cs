using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Faults
{
    public class FaultForCreationDto
    {
        public required string Name { get; set; }
        public string Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field '{0}' must not be empty!")]
        public required string Description { get; set; }
        public bool IsResolved { get; set; }
        public int MachineId { get; set; }
    }
}

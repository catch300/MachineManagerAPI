using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Faults
{
    public class FaultForUpdatingStatusDto
    {
        [Required]
        public bool IsResolved { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Machines
{
    public class MachineForCreationDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field '{0}' must not be empty!")]
        public string Name { get; set; }
    }
}

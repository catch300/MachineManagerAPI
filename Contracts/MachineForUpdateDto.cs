using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class MachineForUpdateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required field!")]
        public string Name { get; set; }
    }
}

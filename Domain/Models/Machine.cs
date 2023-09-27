using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string? Name { get; set; }
        public List<Faults> Faults{ get; set; } = new List<Faults>();

    }
}

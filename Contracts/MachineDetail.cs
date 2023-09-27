﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class MachineDetail
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public List<string> FaultNames { get; set; }
        public double AverageFaultDuration { get; set; }
    }
}

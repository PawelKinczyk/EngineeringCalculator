using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringCalculator.Objects
{
    public class Duct
    {
        public int? dimension { get; set; }
        public double crossSection { get; set; }
        public double? airSpeed { get; set; }
        public string ductType { get; set; }
    }
}

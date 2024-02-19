using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringCalculator.Objects
{
    public class Duct
    {
        public int? diameter { get; set; }
        public int? high { get; set; }
        public int? width { get; set; }
        public double crossSection { get; set; }
        public double? airSpeed { get; set; }
        public string ductType { get; set; }
        public static List<int> rectangleDuctDimensions { get; } = new List<int>(Enumerable.Range(100, 400).Where(x => x % 10 == 0));

        public double crossSectionRoundDuct()
        {
            return Math.Round((Math.PI * Math.Pow((int)diameter, 2)) / 1000000, 3);
        }
        public double crossSectionRectangleDuct()
        {
            return Math.Round(((double)(high * width) / 1000000), 3);
        }
        public double airSpeedCalculation(double crossSection, double airFlow)
        {
            return Math.Round(airFlow / (crossSection * 3600), 3);
        }
    }

    public class DuctGroup : List<Duct>
    {
        public string ductType { get; private set; }

        public DuctGroup(string type, List<Duct> ducts) : base(ducts)
        {
            ductType = type;
        }
    }


}

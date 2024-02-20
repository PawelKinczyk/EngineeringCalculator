using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringCalculator.Objects
{
    public class Calculations
    {
        public static double crossSectionRoundDuct(int ductDimension)
        {
            return Math.Round((Math.PI * Math.Pow(ductDimension, 2)) / 1000000, 3);
        }
        public static double crossSectionRectangleDuct(int XLength, int YLength)
        {
            return Math.Round(((double)(XLength * YLength) / 1000000), 3);
        }
        public static double airSpeedCalculation(double crossSection, double airFlow)
        {
            return Math.Round(airFlow / (crossSection * 3600), 3);
        }
    }
}

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
        public static (double, double, double) pressureLoss(double crossSection, double airFlow, double materialRoughness
            , double frictionCoefficient, int diameter, double liquidDensity)
        {
            // temporally calculation
            double relativeRoughness = frictionCoefficient / 1000 / diameter;
            double reynoldsValue = airFlow / crossSection * 1 / 3600;
            double coefficientOfFrictionSmallestDifference = double.MaxValue;
            double bestCoefficientOfFriction = 0;


            if (reynoldsValue >= 4000)
            {
                for (double i=0.01; i <=8; i+=0.01)
                {
                    double coefficientOfFriction = Math.Pow((-2 * Math.Log(2.51 / (reynoldsValue * Math.Pow(i, -2)) + relativeRoughness / 3.72)), -2);
                    double coefficientOfFrictionDifference = Math.Abs(i - coefficientOfFriction);

                    if (coefficientOfFrictionDifference < coefficientOfFrictionSmallestDifference)
                    {
                        coefficientOfFrictionSmallestDifference = coefficientOfFrictionDifference;
                        bestCoefficientOfFriction = i;
                    }

                }
            }
            return (relativeRoughness, reynoldsValue, bestCoefficientOfFriction);
        }
    }
}

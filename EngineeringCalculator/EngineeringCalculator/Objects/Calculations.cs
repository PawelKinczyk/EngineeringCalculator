namespace EngineeringCalculator.Objects
{
    public class Calculations
    {
        public static double crossSectionRoundDuct(int? ductDimension)
        {
            if (ductDimension == null)
            {
                return 0;
            }
            else
            {
                return Math.Round((Math.PI * Math.Pow((double)ductDimension, 2)) / 1000000, 3);
            }
            
        }
        public static double crossSectionRectangleDuct(int? XLength, int? YLength)
        {
            if (XLength == null || YLength == null)
            {
                return 0;
            }
            else
            {
                return Math.Round(((double)(XLength * YLength) / 1000000), 3);
            }
            
        }
        public static double airSpeedCalculation(double crossSection, double airFlow)
        {
            return Math.Round(airFlow / (crossSection * 3600), 3);
        }
        public static (double, double, double, double) pressureLoss(double airFlow, double materialRoughness
            , int? diameter, string liquidDensity)
        {
            double coefficientOfFrictionSmallestDifference = double.MaxValue;
            double bestCoefficientOfFriction = 0;
            double pressureLoss = 0;
            double airSpeed = airFlow / Calculations.crossSectionRoundDuct(diameter);
            double reynoldsValue = 0;

            if (Double.TryParse(liquidDensity, out double liquidDensityDouble) == true)
            {
                reynoldsValue = (airFlow / Calculations.crossSectionRoundDuct(diameter) * 1 / 3600) / liquidDensityDouble;




                if (reynoldsValue >= 4000)
                {
                    for (double i = 0.01; i <= 8; i += 0.01)
                    {
                        double coefficientOfFriction = Math.Pow((-2 * Math.Log(2.51 / (reynoldsValue * Math.Pow(i, -2)) + materialRoughness / 3.72)), -2);
                        double coefficientOfFrictionDifference = Math.Abs(i - coefficientOfFriction);

                        if (coefficientOfFrictionDifference < coefficientOfFrictionSmallestDifference)
                        {
                            coefficientOfFrictionSmallestDifference = coefficientOfFrictionDifference;
                            bestCoefficientOfFriction = i;
                        }

                    }
                }

                pressureLoss = (bestCoefficientOfFriction / diameter) * (liquidDensityDouble * Math.Pow(airSpeed, 2) / 2);

                return (pressureLoss, materialRoughness, reynoldsValue, bestCoefficientOfFriction);
            }
            else
            {
                // TODO : add exception when program couldn't parse text
                throw new System.Exception("Liquid density is not the value");
            }
        }
    }
}

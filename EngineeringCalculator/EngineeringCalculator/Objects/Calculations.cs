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
                int? ductRadius = ductDimension/2;
                return Math.Round((Math.PI * Math.Pow((double)ductRadius, 2)) / 1000000, 3);
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
        public static (double, double, double, double, double) pressureLoss(double airFlow, double materialRoughness
            , int? diameter, string liquidDensity)
        {
            double coefficientOfFrictionSmallestDifference = double.MaxValue;
            double bestCoefficientOfFriction = 0;
            double pressureLoss = 0;
            double airSpeed = Calculations.airSpeedCalculation(Calculations.crossSectionRoundDuct(diameter), airFlow);
            double reynoldsValue = 0;

            if (Double.TryParse(liquidDensity, out double liquidDensityDouble) == true || diameter == null)
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
                    // TODO : add option if reynolds is not fewer than 4000
                }

                // TODO : add air density parameter 
                pressureLoss = (bestCoefficientOfFriction / ((double)diameter/1000)) * (1.205 * Math.Pow(airSpeed, 2) / 2);

                return (Math.Round(pressureLoss, 2), Math.Round(materialRoughness,2), Math.Round(reynoldsValue,2), Math.Round(bestCoefficientOfFriction,2), Math.Round(airSpeed, 2));
            }
            else
            {
                // TODO : add exception when program couldn't parse text

                throw new System.Exception("Liquid density is not the value");
            }
        }
    }
}

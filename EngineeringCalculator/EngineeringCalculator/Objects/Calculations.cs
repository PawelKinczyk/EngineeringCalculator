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
                int? ductRadius = ductDimension / 2;
                return Math.Round((Math.PI * Math.Pow((double)ductRadius, 2)) / 1000000, 6);
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
                return Math.Round(((double)(XLength * YLength) / 1000000), 6);
            }
        }

        public static double airSpeedCalculation(double crossSection, double airFlow)
        {
            return Math.Round(airFlow / (crossSection * 3600), 6);
        }

        public static (double, double, double, double, double) pressureLoss(double airFlow, double materialRoughness
            , string liquidDensity, string liquidViscosity, int? diameter = null, int? ductHeight = null, int? ductWidth = null)
        {
            double coefficientOfFrictionSmallestDifference = double.MaxValue;
            double bestCoefficientOfFriction = 0;
            double pressureLoss = 0;
            double airSpeed = 0;
            double reynoldsValue = 0;
            double diameterSubstitute = 0;
            double materialRoughnessRelative = 0;





            if (Double.TryParse(liquidViscosity, out double liquidViscosityDouble) == true && Double.TryParse(liquidDensity, out double liquidDensityDouble) == true && (diameter != null || (ductHeight != null && ductWidth != null)))
            {
                if (diameter != null && (ductHeight == null || ductWidth == null))
                {
                    double dh = (diameter ?? 0) /1000.0;
                    airSpeed = Calculations.airSpeedCalculation(Calculations.crossSectionRoundDuct(diameter), airFlow);
                    reynoldsValue = airSpeed *dh / liquidViscosityDouble;
                    double a = (materialRoughness / 1000.0);
                    double b = (diameter ?? 0) / 1000.0;
                    materialRoughnessRelative = (materialRoughness / 1000.0) / ((diameter ?? 0) /1000.0);
                }
                else if (ductHeight.HasValue && ductWidth.HasValue)
                {
                    //diameter = (int)(1.3 * (Math.Pow((double)(ductWidth * ductHeight), 0.625) / (Math.Pow((double)(ductWidth + ductHeight), 0.250))));
                    diameterSubstitute = (double)(1.27 * (Math.Pow(ductHeight *ductWidth ?? 0, 0.625) / Math.Pow(ductWidth + ductHeight ?? 0, 0.25)));
                    airSpeed = Calculations.airSpeedCalculation(Calculations.crossSectionRoundDuct((int)diameterSubstitute), airFlow);
                    reynoldsValue = (airSpeed * diameterSubstitute/1000.0) / liquidViscosityDouble;
                    
                    materialRoughnessRelative = materialRoughness / 1000.0 / (diameterSubstitute / 1000.0);
                }
                else
                {
                    throw new Exception("No diameter XY size");
                }

                if (reynoldsValue >= 4000)
                {
                    for (double i = 0.01; i <= 8; i += 0.001)
                    {
                        
                        double coefficientOfFriction = Math.Pow((-2 * Math.Log10(2.51 / (reynoldsValue * Math.Pow(i, 0.5)) + materialRoughnessRelative / 3.72)), -2);
                        double coefficientOfFrictionDifference = Math.Abs(i - coefficientOfFriction);

                        if (coefficientOfFrictionDifference < coefficientOfFrictionSmallestDifference)
                        {
                            coefficientOfFrictionSmallestDifference = coefficientOfFrictionDifference;
                            bestCoefficientOfFriction = i;
                        }

                    }

                }
                // TODO : add option if reynolds is not fewer than 4000
                else
                {
                    throw new Exception("Reynolds value under 4000");
                }

                
                if (diameter != null && (ductHeight == null || ductWidth == null))
                {
                    pressureLoss = (bestCoefficientOfFriction / ((double)diameter / 1000.0)) * (liquidDensityDouble * Math.Pow(airSpeed, 2) / 2);
                }
                else if (ductHeight.HasValue && ductWidth.HasValue)
                {
                    pressureLoss = (bestCoefficientOfFriction / ((double)diameterSubstitute / 1000.0)) * (liquidDensityDouble * Math.Pow(airSpeed, 2) / 2);
                    airSpeed = Calculations.airSpeedCalculation(Calculations.crossSectionRectangleDuct(ductHeight,ductWidth), airFlow);
                }
                else
                {
                    throw new Exception("Problem with pressure loss calculations");
                }
                

                return (Math.Round(pressureLoss, 2), Math.Round(materialRoughness, 2), Math.Round(reynoldsValue, 2), Math.Round(bestCoefficientOfFriction, 3), Math.Round(airSpeed, 2));
            }
            else
            {
                // TODO : add exception when program couldn't parse text

                throw new Exception("Problem with inputs");
            }
        }
    }
}

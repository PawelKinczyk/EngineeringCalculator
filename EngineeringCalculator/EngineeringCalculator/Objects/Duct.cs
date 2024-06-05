namespace EngineeringCalculator.Objects
{
    public class Duct
    {
        public int? diameter { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
        public double crossSection { get; set; }
        public double? airSpeed { get; set; }
        public double airVolume { get; set; } // cubic meters per hour
        public double materialRoughness { get; set; }
        public double pressureLossPerMeter { get; set; }
        public string ductType { get; set; }
        public static List<int> rectangleDuctDimensions { get; } = new List<int>(Enumerable.Range(100, 400).Where(x => x % 10 == 0));
        public static List<int> roundDuctDimensions { get; } = new List<int> { 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250 };

    }

    public class DuctGroup : List<Duct>
    {
        public string ductType { get; private set; }

        public DuctGroup(string type, List<Duct> ducts) : base(ducts)
        {
            ductType = type;
        }
    }

    public class MaterialFrictionCoefficient
    {
        private Dictionary<string, double> frictionCoefficient = new Dictionary<string, double>()
        {
            { "Galvanised steel", 0.15 }
        };
    }
}

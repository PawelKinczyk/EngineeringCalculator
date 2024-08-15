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
        public double airLiquidDensity { get; set; }
        public double airLiquidViscosity { get; set; }
        public double coefficientOfFriction { get; set; }
        public double reynoldsValue { get; set; }
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
        private Dictionary<string, double> frictionCoefficients;

        // Constructor to initialize the dictionary
        public MaterialFrictionCoefficient()
        {
            frictionCoefficients = new Dictionary<string, double>()
        {
            { "Galvanised steel", 0.15 }
        };
        }

        // Method to add a new material and its friction coefficient
        public void AddMaterial(string material, double coefficient)
        {
            if (!frictionCoefficients.ContainsKey(material))
            {
                frictionCoefficients.Add(material, coefficient);
            }
            else
            {
                throw new ArgumentException("Material already exists.");
            }
        }

        // Method to get the friction coefficient of a specific material
        public double GetFrictionCoefficient(string material)
        {
            if (frictionCoefficients.TryGetValue(material, out double coefficient))
            {
                // TODO : think about better exception
                return coefficient;
            }
            else
            {
                throw new KeyNotFoundException("Material not found.");
            }
        }

        // Method to retrieve the entire dictionary
        public Dictionary<string, double> GetAllFrictionCoefficients()
        {
            return new Dictionary<string, double>(frictionCoefficients); // Return a copy to prevent modification
        }

        // Method to retrieve the entire dictionary
        public List<string> GetAllFrictionCoefficientsNames()
        {
            return new List<string> (frictionCoefficients.Keys); // Return a copy to prevent modification
        }
    }
}

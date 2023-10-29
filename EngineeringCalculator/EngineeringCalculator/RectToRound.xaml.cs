using System.Security.Cryptography.X509Certificates;

namespace EngineeringCalculator;

public partial class VentilationCalculations : ContentPage
{
    public VentilationCalculations()
    {

        InitializeComponent();

        List<int> predefinedItems = new List<int>();
        predefinedItems.AddRange(Enumerable.Range(100, 400).Where(x => x % 10 == 0));
        XPicker.ItemsSource = predefinedItems;
        YPicker.ItemsSource = predefinedItems;


    }
    private void OnGetEquivalentChannels(object sender, EventArgs e)
    {
        // Calculate properties of input duct
        int activeCrossSection = 0;
        List<Duct> rectangleDuctList = new List<Duct>();
        if (XPicker.SelectedItem!=null && YPicker.SelectedItem!=null)
                // int.TryParse(XPicker.SelectedItem.ToString(), out int XPickerValue) && int.TryParse(YPicker.SelectedItem.ToString(), out int YPickerValue)
        {
            activeCrossSection = (int)XPicker.SelectedItem * (int)YPicker.SelectedItem;
            Duct rectangleDuct = new Duct { dimension = null, crossSection = crossSectionRectangleDuct((int)XPicker.SelectedItem, (int)YPicker.SelectedItem) };
            if (Double.TryParse(AirEntry.Text, out double RectangleAirFlow) == true) { rectangleDuct.airSpeed = airSpeedCalculation(rectangleDuct.crossSection, RectangleAirFlow); }
            else { rectangleDuct.airSpeed = null; }
            rectangleDuctList.Add(rectangleDuct);
        }
        else
        {
            RecalculateSummary.Text = "Wrong input values";
        }

        // List of round ducts
        List<int> roundDuctDimensions = new List<int>
        {
            63,80,100,125,160,200,250,315,400,500,630,800,1000,1250
        };
        // Define index and loop values
        int index = -1;
        int pickedIndex = 0;
        int closestValueToActiveCrossSection = 100000;

        // Loop and find the closest cross section
        foreach (int roundDuct in roundDuctDimensions)
        {
            index++;
            int roundDuctCrossSection = (int)Math.PI * (int)Math.Pow(roundDuct, 2);
            int crossSectionDifference = Math.Abs(activeCrossSection - roundDuctCrossSection);
            if (crossSectionDifference < closestValueToActiveCrossSection)
            {
                pickedIndex = index;
                closestValueToActiveCrossSection = crossSectionDifference;
            }
            else { continue; }
        }
        // Create list with equal round ducts
        List<Duct> selectedRoundDuctsList = new List<Duct>();
        if (pickedIndex >= 2)
        {
            for (int i = pickedIndex - 1; i <= pickedIndex + 1; i++)
            {

                Duct duct = new Duct { dimension = roundDuctDimensions[i], crossSection = crossSectionRoundDuct(roundDuctDimensions[i]) };

                if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                {
                    duct.airSpeed = airSpeedCalculation(duct.crossSection, AirFlow);
                }
                else
                {
                    duct.airSpeed = null;
                }
                selectedRoundDuctsList.Add(duct);
            }
        }
        else if (pickedIndex == 0)
        {
            for (int i = pickedIndex; i <= pickedIndex + 1; i++)
            {

                Duct duct = new Duct { dimension = roundDuctDimensions[i], crossSection = crossSectionRoundDuct(roundDuctDimensions[i]) };

                if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                {
                    duct.airSpeed = airSpeedCalculation(duct.crossSection, AirFlow);
                }
                else
                {
                    duct.airSpeed = null;
                }
                selectedRoundDuctsList.Add(duct);
            }
        }
        else if (pickedIndex == roundDuctDimensions.Count)
        {
            for (int i = pickedIndex - 1; i <= pickedIndex; i++)
            {

                Duct duct = new Duct { dimension = roundDuctDimensions[i], crossSection = crossSectionRoundDuct(roundDuctDimensions[i]) };

                if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                {
                    duct.airSpeed = airSpeedCalculation(duct.crossSection, AirFlow);
                }
                else
                {
                    duct.airSpeed = null;
                }
                selectedRoundDuctsList.Add(duct);
            }
        }
        else { }
        // Update lists of round and rectangle ducts
        RecalculatedDucts.ItemsSource = selectedRoundDuctsList;
        RectangleDuct.ItemsSource = rectangleDuctList;
    }
    public class Duct
    {
        public int? dimension { get; set; }
        public double crossSection { get; set; }
        public double? airSpeed { get; set; }
    }

    public double crossSectionRoundDuct(int ductDimension)
    {
        return Math.Round((Math.PI * Math.Pow(ductDimension, 2)) / 1000000, 3);
    }
    public double crossSectionRectangleDuct(int XLength, int YLength)
    {
        return Math.Round(((double)(XLength * YLength) / 1000000), 3);
    }
    public double airSpeedCalculation(double crossSection, double airFlow)
    {
        return Math.Round(airFlow / (crossSection*3600), 3);
    }
}
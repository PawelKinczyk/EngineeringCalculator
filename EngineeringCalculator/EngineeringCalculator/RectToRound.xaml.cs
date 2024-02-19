using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using EngineeringCalculator.Objects;

namespace EngineeringCalculator;

public partial class RectToRound : ContentPage
{
    public ObservableCollection<DuctGroup> Ducts { get; private set; } = new ObservableCollection<DuctGroup>();
    public RectToRound()
    {

        InitializeComponent();

        XPicker.ItemsSource = Duct.rectangleDuctDimensions;
        YPicker.ItemsSource = Duct.rectangleDuctDimensions;


    }
    private void OnGetEquivalentChannels(object sender, EventArgs e)
    {
        Ducts.Clear();
        // Calculate properties of input duct
        double activeCrossSection = 0;
        List<Duct> rectangleDuctList = new List<Duct>();
        // List of round ducts
        List<int> roundDuctDimensions = new List<int>
        {
            63,80,100,125,160,200,250,315,400,500,630,800,1000,1250
        };
        // Create list with equal round ducts
        List<Duct> selectedRoundDuctsList = new List<Duct>();

        if (XPicker.SelectedItem != null && YPicker.SelectedItem != null)
        // int.TryParse(XPicker.SelectedItem.ToString(), out int XPickerValue) && int.TryParse(YPicker.SelectedItem.ToString(), out int YPickerValue)
        {
            activeCrossSection = double.Parse(XPicker.SelectedItem.ToString()) * double.Parse(YPicker.SelectedItem.ToString());
            Duct rectangleDuct = new Duct { diameter = null, crossSection = crossSectionRectangleDuct((int)XPicker.SelectedItem, (int)YPicker.SelectedItem) };
            if (Double.TryParse(AirEntry.Text, out double RectangleAirFlow) == true) { rectangleDuct.airSpeed = airSpeedCalculation(rectangleDuct.crossSection, RectangleAirFlow); }
            else { rectangleDuct.airSpeed = null; }
            rectangleDuctList.Add(rectangleDuct);
            Ducts.Add(new DuctGroup("Rectangle duct", rectangleDuctList));


            // Define index and loop values
            int index = -1;
            int pickedIndex = 0;
            double closestValueToActiveCrossSection = 1000000000;

            // Loop and find the closest cross section
            foreach (double roundDuct in roundDuctDimensions)
            {
                index++;
                double roundDuctCrossSection = Math.PI * (double)Math.Pow(roundDuct, 2);
                double crossSectionDifference = Math.Abs(activeCrossSection - roundDuctCrossSection);
                if (crossSectionDifference < closestValueToActiveCrossSection)
                {
                    pickedIndex = index;
                    closestValueToActiveCrossSection = crossSectionDifference;
                }
                else { }
            }

            if (pickedIndex >= 1 && pickedIndex <= roundDuctDimensions.Count)
            {
                for (int i = pickedIndex - 1; i <= pickedIndex + 1; i++)
                {

                    Duct duct = new Duct { diameter = roundDuctDimensions[i], crossSection = crossSectionRoundDuct(roundDuctDimensions[i]) };

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

                    Duct duct = new Duct { diameter = roundDuctDimensions[i], crossSection = crossSectionRoundDuct(roundDuctDimensions[i]) };

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

                    Duct duct = new Duct { diameter = roundDuctDimensions[i], crossSection = crossSectionRoundDuct(roundDuctDimensions[i]) };

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
            OnPropertyChanged(nameof(Ducts));
            Ducts.Add(new DuctGroup("Equal round duct", selectedRoundDuctsList));
            RecalculatedDucts.ItemsSource = Ducts;

        }
        else
        {
            // Pass if inputs are null
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
        return Math.Round(airFlow / (crossSection * 3600), 3);
    }
}
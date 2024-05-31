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
    private void GetBestRoundDuct(object sender, EventArgs e)
    {
        Ducts.Clear();
        // Calculate properties of input duct
        List<Duct> rectangleDuctList = new List<Duct>();
        // List of round ducts
        List<int> roundDuctDimensions = Duct.roundDuctDimensions;
        // Create list with equal round ducts
        List<Duct> selectedRoundDuctsList = new List<Duct>();

        if (XPicker.SelectedItem != null && YPicker.SelectedItem != null)
        {
            Duct rectangleDuct = new Duct { diameter = null, crossSection = Calculations.crossSectionRectangleDuct((int)XPicker.SelectedItem, (int)YPicker.SelectedItem) };
            if (Double.TryParse(AirEntry.Text, out double RectangleAirFlow) == true) { rectangleDuct.airSpeed = Calculations.airSpeedCalculation(rectangleDuct.crossSection, RectangleAirFlow); }
            else { rectangleDuct.airSpeed = null; }
            rectangleDuctList.Add(rectangleDuct);
            Ducts.Add(new DuctGroup("Rectangle duct", rectangleDuctList));


            // Define index and loop values
            int index = 0;
            int pickedIndex = 0;
            double closestValueToActiveCrossSection = 1000000000;

            // Loop and find the closest cross section
            foreach (int roundDuct in roundDuctDimensions)
            {
                
                double roundDuctCrossSection = Calculations.crossSectionRoundDuct(roundDuct);
                double crossSectionDifference = Math.Abs(rectangleDuct.crossSection - roundDuctCrossSection);
                if (crossSectionDifference < closestValueToActiveCrossSection)
                {
                    pickedIndex = index;
                    closestValueToActiveCrossSection = crossSectionDifference;
                }
                else { }
                index++;
            }

            if (pickedIndex >= 1 && pickedIndex <= roundDuctDimensions.Count - 1)
            {
                for (int i = pickedIndex - 1; i <= pickedIndex + 1; i++)
                {

                    Duct duct = new Duct { diameter = roundDuctDimensions[i], crossSection = Calculations.crossSectionRoundDuct(roundDuctDimensions[i]) };

                    if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                    {
                        duct.airSpeed = Calculations.airSpeedCalculation(duct.crossSection, AirFlow);
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

                    Duct duct = new Duct { diameter = roundDuctDimensions[i], crossSection = Calculations.crossSectionRoundDuct(roundDuctDimensions[i]) };

                    if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                    {
                        duct.airSpeed = Calculations.airSpeedCalculation(duct.crossSection, AirFlow);
                    }
                    else
                    {
                        duct.airSpeed = null;
                    }
                    selectedRoundDuctsList.Add(duct);
                }
            }
            else if (pickedIndex == roundDuctDimensions.Count-1)
            {
                for (int i = pickedIndex - 1; i <= pickedIndex; i++)
                {

                    Duct duct = new Duct { diameter = roundDuctDimensions[i], crossSection = Calculations.crossSectionRoundDuct(roundDuctDimensions[i]) };

                    if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                    {
                        duct.airSpeed = Calculations.airSpeedCalculation(duct.crossSection, AirFlow);
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
}
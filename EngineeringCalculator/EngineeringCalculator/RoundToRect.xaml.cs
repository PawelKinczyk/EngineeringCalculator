
using EngineeringCalculator.Objects;
using System.Collections.ObjectModel;

namespace EngineeringCalculator;

public partial class RoundToRect : ContentPage
{
    public ObservableCollection<DuctGroup> Ducts { get; private set; } = new ObservableCollection<DuctGroup>();
    public RoundToRect()
	{
		InitializeComponent();

        XPicker.ItemsSource = Duct.rectangleDuctDimensions;
        DPicker.ItemsSource = Duct.roundDuctDimensions;
    }
	private void GetBestRectangleDuct(object sender, EventArgs e)
	{
        Ducts.Clear();
        // Calculate properties of input duct
        List<Duct> roundDuctList = new List<Duct>();
        // List of round ducts
        List<int> rectangleDuctDimensions = Duct.rectangleDuctDimensions;
        // Create list with equal round ducts
        List<Duct> selectedRectangleDuctsList = new List<Duct>();
        try
        {
            if (XPicker.SelectedItem != null && DPicker.SelectedItem != null)
            {
                Duct roundDuct = new Duct { diameter = null, crossSection = Calculations.crossSectionRoundDuct((int)DPicker.SelectedItem) };
                if (Double.TryParse(AirEntry.Text, out double RoundAirFlow) == true) { roundDuct.airSpeed = Calculations.airSpeedCalculation(roundDuct.crossSection, RoundAirFlow); }
                else { roundDuct.airSpeed = null; }
                roundDuctList.Add(roundDuct);
                Ducts.Add(new DuctGroup("Round duct", roundDuctList));

                // Define index and loop values
                int index = 0;
                int pickedIndex = 0;
                double closestValueToActiveCrossSection = 1000000000;


                // Loop and find the closest cross section
                foreach (int rectangleDuct in rectangleDuctDimensions)
                {

                    double roundDuctCrossSection = Calculations.crossSectionRectangleDuct(rectangleDuct, (int)XPicker.SelectedItem);
                    double crossSectionDifference =
                        Math.Abs(roundDuct.crossSection - roundDuctCrossSection);
                    if (crossSectionDifference < closestValueToActiveCrossSection)
                    {
                        pickedIndex = index;
                        closestValueToActiveCrossSection = crossSectionDifference;
                    }
                    else { }
                    index++;
                }


                if (pickedIndex >= 1 && pickedIndex < rectangleDuctDimensions.Count - 1)
                {
                    for (int i = pickedIndex - 1; i <= pickedIndex + 1; i++)
                    {

                        Duct duct = new Duct { height = rectangleDuctDimensions[i], width = (int)XPicker.SelectedItem, crossSection = Calculations.crossSectionRectangleDuct(rectangleDuctDimensions[i], (int)XPicker.SelectedItem) };

                        if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                        {
                            duct.airSpeed = Calculations.airSpeedCalculation(duct.crossSection, AirFlow);
                        }
                        else
                        {
                            duct.airSpeed = null;
                        }
                        selectedRectangleDuctsList.Add(duct);
                    }
                }
                else if (pickedIndex == 0)
                {
                    for (int i = pickedIndex; i <= pickedIndex + 1; i++)
                    {

                        Duct duct = new Duct { height = rectangleDuctDimensions[i], width = (int)XPicker.SelectedItem, crossSection = Calculations.crossSectionRectangleDuct(rectangleDuctDimensions[i], (int)XPicker.SelectedItem) };

                        if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                        {
                            duct.airSpeed = Calculations.airSpeedCalculation(duct.crossSection, AirFlow);
                        }
                        else
                        {
                            duct.airSpeed = null;
                        }
                        selectedRectangleDuctsList.Add(duct);
                    }
                }
                else if (pickedIndex == rectangleDuctDimensions.Count - 1)
                {
                    for (int i = pickedIndex - 1; i <= pickedIndex; i++)
                    {

                        Duct duct = new Duct { height = rectangleDuctDimensions[i], width = (int)XPicker.SelectedItem, crossSection = Calculations.crossSectionRectangleDuct(rectangleDuctDimensions[i], (int)XPicker.SelectedItem) };

                        if (Double.TryParse(AirEntry.Text, out double AirFlow) == true)
                        {
                            duct.airSpeed = Calculations.airSpeedCalculation(duct.crossSection, AirFlow);
                        }
                        else
                        {
                            duct.airSpeed = null;
                        }
                        selectedRectangleDuctsList.Add(duct);
                    }
                }
                else { }
                // Update lists of round and rectangle ducts
                OnPropertyChanged(nameof(Ducts));
                Ducts.Add(new DuctGroup("Equal Rectangle duct", selectedRectangleDuctsList));
                RecalculatedDucts.ItemsSource = Ducts;

            }
            else
            {
                // Pass if inputs are null
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Ok");
        }
    }
    
}
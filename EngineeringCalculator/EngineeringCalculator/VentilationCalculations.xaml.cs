namespace EngineeringCalculator;

public partial class VentilationCalculations : ContentPage
{
	public VentilationCalculations()
	{

        InitializeComponent();

        List<int> predefinedItems = new List<int> ();
        predefinedItems.AddRange(Enumerable.Range(100, 400).Where(x => x%10 ==0));
        XPicker.ItemsSource = predefinedItems;
        YPicker.ItemsSource = predefinedItems;
        
    }
    private void OnGetEquivalentChannels(object sender, EventArgs e)
    {
        // Calculate cross section of input duct
        int activeCrossSection = (int)XPicker.SelectedItem * (int)YPicker.SelectedItem;
        // List of round ducts
        List<int> roundDuctDimensions = new List<int>
        {
            63,80,100,125,160,200,250,315,400,500,630,800,1000,1250
        };
        // Define index and 
        int index = -1;
        int pickedIndex = 0;
        int closestValueToActiveCrossSection = 100000;
        foreach (int roundDuct in roundDuctDimensions) {
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
        
        List<Duct> selectedRoundDucts = new List<Duct>();
        for (int i = pickedIndex-1; i <= pickedIndex+1; i++) {
            selectedRoundDucts.Add(new Duct { dimension = roundDuctDimensions[i], crossSection = (int)Math.PI * (int)Math.Pow(roundDuctDimensions[i], 2) });
        }
        RecalculatedDucts.ItemsSource = selectedRoundDucts;
    }
    public class Duct
    {
        public int dimension { get; set; }
        public int crossSection { get; set; }
    }
}
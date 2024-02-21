
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
    }
	private void GetBestRectangleDuct(object sender, EventArgs e)
	{
        Ducts.Clear();
        // Calculate properties of input duct
        double activeCrossSection = 0;
        List<Duct> rectangleDuctList = new List<Duct>();
        // List of round ducts
        List<int> roundDuctDimensions = Duct.roundDuctDimensions;
        // Create list with equal round ducts
        List<Duct> selectedRoundDuctsList = new List<Duct>();

        if (XPicker.SelectedItem != null)
        {

        }
    }
}
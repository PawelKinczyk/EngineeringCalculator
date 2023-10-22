namespace EngineeringCalculator;

public partial class VentilationCalculations : ContentPage
{
	public VentilationCalculations()
	{

        InitializeComponent();

        List<string> predefinedItems = new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3"
                // Add more items as needed
            };
        myPicker.ItemsSource = predefinedItems;

    }

	
}
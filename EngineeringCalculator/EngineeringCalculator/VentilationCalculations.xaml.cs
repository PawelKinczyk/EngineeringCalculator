namespace EngineeringCalculator;

public partial class VentilationCalculations : ContentPage
{
	public VentilationCalculations()
	{

        InitializeComponent();

        List<int> predefinedItems = new List<int> { };
        predefinedItems.AddRange(Enumerable.Range(100, 400).Where(x => x%10 ==0));
        XPicker.ItemsSource = predefinedItems;
        YPicker.ItemsSource = predefinedItems;
    }

	
}
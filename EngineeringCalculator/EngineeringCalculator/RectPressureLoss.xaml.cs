
using EngineeringCalculator.Objects;
using System.Collections.ObjectModel;

namespace EngineeringCalculator;

public partial class RectPressureLoss : ContentPage
{
    public ObservableCollection<Duct> DuctList { get; private set; } = new ObservableCollection<Duct>();
    MaterialFrictionCoefficient materials = new MaterialFrictionCoefficient();
    public RectPressureLoss()
    {
        InitializeComponent();

        XPicker.ItemsSource = Duct.rectangleDuctDimensions;
        YPicker.ItemsSource = Duct.rectangleDuctDimensions;
        MaterialFrictionPicker.ItemsSource = materials.GetAllFrictionCoefficientsNames();
        AirDensityEntry.Text = "1.205";
        AirViscosityEntry.Text = "0.000015";
    }
    private void GetPressureLossRound(object sender, EventArgs e)
    {
        try
        {
            DuctList.Clear();
            if (XPicker.SelectedItem != null && YPicker.SelectedItem != null && AirEntry.Text != null && Double.TryParse(AirEntry.Text, out double AirFlow) == true)
            {
                // TODO : Clean code below
                string materialName = (string)MaterialFrictionPicker.SelectedItem;
                double materialRoughness = materials.GetFrictionCoefficient(materialName);
                Duct rectDuct = new Duct { height = (int)YPicker.SelectedItem, width = (int)XPicker.SelectedItem, crossSection = Calculations.crossSectionRectangleDuct((int)XPicker.SelectedItem, (int)YPicker.SelectedItem), materialRoughness = materialRoughness, airVolume = Double.Parse(AirEntry.Text) };
                (rectDuct.pressureLossPerMeter, rectDuct.materialRoughness, rectDuct.reynoldsValue, rectDuct.airLiquidDensity, rectDuct.airSpeed) = Calculations.pressureLoss(rectDuct.airVolume, rectDuct.materialRoughness, ductHeight: rectDuct.height, ductWidth: rectDuct.width, liquidDensity: AirDensityEntry.Text, liquidViscosity: AirViscosityEntry.Text);

                DuctList.Add(rectDuct);
                DuctPressureLoss.ItemsSource = DuctList;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}
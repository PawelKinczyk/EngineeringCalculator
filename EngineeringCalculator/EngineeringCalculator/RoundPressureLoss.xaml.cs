
using EngineeringCalculator.Objects;
using System.Collections.ObjectModel;

namespace EngineeringCalculator;

public partial class RoundPressureLoss : ContentPage
{
    public ObservableCollection<Duct> DuctList { get; private set; } = new ObservableCollection<Duct>();
    MaterialFrictionCoefficient materials = new MaterialFrictionCoefficient();
    public RoundPressureLoss()
    {
        InitializeComponent();

        DPicker.ItemsSource = Duct.roundDuctDimensions;
        MaterialFrictionPicker.ItemsSource = materials.GetAllFrictionCoefficientsNames();
        AirDensityEntry.Text = "0.0000015";
    }
    private void GetPressureLossRound(object sender, EventArgs e)
    {
        DuctList.Clear();
        if (DPicker.SelectedItem != null && AirEntry.Text != null && Double.TryParse(AirEntry.Text, out double AirFlow) == true)
        {
            // TODO : Clean code below
            string materialName = (string)MaterialFrictionPicker.SelectedItem;
            double materialRoughness = materials.GetFrictionCoefficient(materialName);
            Duct roundDuct = new Duct { diameter = (int)DPicker.SelectedItem, crossSection = Calculations.crossSectionRoundDuct((int)DPicker.SelectedItem), materialRoughness = materialRoughness , airVolume= Double.Parse(AirEntry.Text) };
            (roundDuct.pressureLossPerMeter, roundDuct.materialRoughness, roundDuct.reynoldsValue, roundDuct.airLiquidDensity, roundDuct.airSpeed) = Calculations.pressureLoss(airFlow: roundDuct.airVolume, 
                materialRoughness: roundDuct.materialRoughness, diameter: roundDuct.diameter, liquidDensity: AirDensityEntry.Text);
            
            DuctList.Add(roundDuct);
            DuctPressureLoss.ItemsSource = DuctList;
        }
    }
}
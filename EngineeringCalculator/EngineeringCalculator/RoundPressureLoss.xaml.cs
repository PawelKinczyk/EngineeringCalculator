
using EngineeringCalculator.Objects;
using System.Collections.ObjectModel;

namespace EngineeringCalculator;

public partial class RoundPressureLoss : ContentPage
{
    private MaterialFrictionCoefficient materials;

    public RoundPressureLoss()
    {
        InitializeComponent();

        DPicker.ItemsSource = Duct.roundDuctDimensions;
        MaterialFrictionCoefficient materials = new MaterialFrictionCoefficient();
        MaterialFrictionPicker.ItemsSource = materials.GetAllFrictionCoefficientsNames();
        AirDensityEntry.Text = "0.0000015";
    }
    private void GetPressureLossRound(object sender, EventArgs e)
    {
        if (DPicker.SelectedItem != null && AirEntry.Text != null && Double.TryParse(AirEntry.Text, out double AirFlow) == true)
        {
            
            Duct roundDuct = new Duct { diameter = (int)DPicker.SelectedItem, crossSection = Calculations.crossSectionRoundDuct((int)DPicker.SelectedItem), materialRoughness = materials.GetFrictionCoefficient((string)MaterialFrictionPicker.SelectedItem), airVolume= Double.Parse(AirEntry.Text) };
            // TODO : change parameters in pressure loss calculations
            PressureLossValue.Text = Calculations.pressureLoss(roundDuct.airVolume, roundDuct.materialRoughness, roundDuct.diameter,  AirDensityEntry.Text).ToString();
        }
    }
}
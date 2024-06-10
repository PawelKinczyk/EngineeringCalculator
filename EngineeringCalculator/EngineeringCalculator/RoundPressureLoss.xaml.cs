
using EngineeringCalculator.Objects;
using System.Collections.ObjectModel;

namespace EngineeringCalculator;

public partial class RoundPressureLoss : ContentPage
{
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
        if (DPicker.SelectedItem != null && AirEntry.Text != null && Double.TryParse(AirEntry.Text, out double AirFlow) == true)
        {
            string materialName = (string)MaterialFrictionPicker.SelectedItem;
            double materialRoughness = materials.GetFrictionCoefficient(materialName);
            Duct roundDuct = new Duct { diameter = (int)DPicker.SelectedItem, crossSection = Calculations.crossSectionRoundDuct((int)DPicker.SelectedItem), materialRoughness = materialRoughness , airVolume= Double.Parse(AirEntry.Text) };
            PressureLossValue.Text = Calculations.pressureLoss(roundDuct.airVolume, roundDuct.materialRoughness, roundDuct.diameter,  AirDensityEntry.Text).ToString();
        }
    }
}
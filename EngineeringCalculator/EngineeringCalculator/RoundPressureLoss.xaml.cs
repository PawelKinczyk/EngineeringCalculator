
using EngineeringCalculator.Objects;
using System.Collections.ObjectModel;

namespace EngineeringCalculator;

public partial class RoundPressureLoss : ContentPage
{
    public RoundPressureLoss()
    {
        InitializeComponent();

        DPicker.ItemsSource = Duct.roundDuctDimensions;
    }
    private void GetPressureLossRound(object sender, EventArgs e)
    {
        if (DPicker.SelectedItem != null && AirEntry.Text != null && Double.TryParse(AirEntry.Text, out double AirFlow) == true)
        {
            Duct roundDuct = new Duct { diameter = (int)DPicker.SelectedItem, crossSection = Calculations.crossSectionRoundDuct((int)DPicker.SelectedItem), materialRoughness = 1, airVolume= Double.Parse(AirEntry.Text) };
            PressureLossValue.Text = Calculations.pressureLoss(roundDuct.crossSection, roundDuct.airVolume, roundDuct.materialRoughness).ToString();
        }
    }
}
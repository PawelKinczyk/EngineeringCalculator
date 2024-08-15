
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
        AirDensityEntry.Text = "1.205";
        AirViscosityEntry.Text = "0.000015";
    }
    private void GetPressureLossRound(object sender, EventArgs e)
    {
        try
        {
            DuctList.Clear();
            if (DPicker.SelectedItem != null && AirEntry.Text != null && Double.TryParse(AirEntry.Text, out double AirFlow) == true)
            {
                // TODO : Clean code below
                string materialName = (string)MaterialFrictionPicker.SelectedItem;
                double materialRoughness = materials.GetFrictionCoefficient(materialName);
                Duct roundDuct = new Duct { diameter = (int)DPicker.SelectedItem, crossSection = Calculations.crossSectionRoundDuct((int)DPicker.SelectedItem), materialRoughness = materialRoughness, airVolume = Double.Parse(AirEntry.Text) };
                (roundDuct.pressureLossPerMeter, roundDuct.materialRoughness, roundDuct.reynoldsValue, roundDuct.coefficientOfFriction, roundDuct.airSpeed) = Calculations.pressureLoss(airFlow: roundDuct.airVolume,
                    materialRoughness: roundDuct.materialRoughness, diameter: roundDuct.diameter, liquidDensity: AirDensityEntry.Text, liquidViscosity: AirViscosityEntry.Text);

                DuctList.Add(roundDuct);
                DuctPressureLoss.ItemsSource = DuctList;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}
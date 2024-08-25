using System.Diagnostics;

namespace EngineeringCalculator;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		//count++;

		//if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}
    private void InstructionClicked(object sender, EventArgs e)
    {
		Process.Start(new ProcessStartInfo("https://www.youtube.com/watch?v=C4MpzSMkinw&t=2881s") { UseShellExecute = true});
    }

}


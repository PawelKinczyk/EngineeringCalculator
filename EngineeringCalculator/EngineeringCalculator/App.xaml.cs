namespace EngineeringCalculator;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
	}
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = new Window();
        window.MaximumHeight = 600;
        window.MaximumWidth = 800;
        window.MinimumHeight = 500;
        window.MinimumWidth = 500;
        window.Page = new AppShell();
        return window;
    }
}

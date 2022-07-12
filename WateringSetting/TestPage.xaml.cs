using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace WateringSetting;

public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
	}



	private void Button_Clicked_1(object sender, EventArgs e)
	{
		DisplayAlert("Value",Language.languageType,"OK");
	}

	private async void EnhancedButton_Clicked(object sender, EventArgs e)
	{
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        string text = "Test Mesage";
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }

	private void EnhancedButton_Clicked_1(object sender, EventArgs e)
	{
		Assets.devices.Clear();
		Assets.refreshList();
	}

	private void GC_Btn(object sender, EventArgs e)
	{
		System.GC.Collect();
        GC.WaitForPendingFinalizers();
        System.GC.Collect();
    }
}
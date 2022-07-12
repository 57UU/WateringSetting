
using System.Reflection;
namespace WateringSetting;
public partial class AppSetting : ContentPage
{

	public AppSetting()
	{
        InitializeComponent();
    }

	private async  void Button_Clicked(object sender, EventArgs e)
	{
		
		await Task.Delay(200);
		await DisplayAlert(Language.successful, Language.cache_cleaned, Language.great);
	}

	private async  void EnhancedButton_Clicked(object sender, EventArgs e)
	{
		//SetLanguage
		await Navigation.PushAsync(new ChooseLanguage());
		


	}
}
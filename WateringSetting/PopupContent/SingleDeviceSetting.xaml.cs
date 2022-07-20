namespace WateringSetting.PopupContent;

public partial class SingleDeviceSetting : ContentPage
{
	private SmartDeviceInfo deviceInfo;
	public SingleDeviceSetting(in SmartDeviceInfo smartDeviceInfo)
	{
		InitializeComponent();
		deviceInfo= smartDeviceInfo;
	}

	private void EnhancedButton_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new ParameterSetting(deviceInfo));
	}

	private async void EnhancedButton_Clicked_1(object sender, EventArgs e)
	{
        string result = await DisplayPromptAsync(Language.new_name,Language.input,
			initialValue:deviceInfo.name);
		if(result == null)
			return;
		deviceInfo.name = result;
		Assets.refreshList();
    }

	private async void DeleteThis(object sender, EventArgs e)
	{

        bool answer = await DisplayAlert(Language.confirm, Language.delete+"?", "Yes", "No");
		if(!answer)
			return;
        Assets.devices.Remove(deviceInfo.name);
		Assets.refreshList();
		await Navigation.PopAsync();
	}
}
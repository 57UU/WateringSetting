
using System.Text.Json;

namespace WateringSetting.PopupContent;


/// <summary>
/// When neding connect to a device,this class need invoking
/// </summary>
public partial class Connect : ContentPage
{
	public Connect()
	{
		InitializeComponent(); 
        advancedOptions.IsVisible = true;
    }

	private void connect_Clicked(object sender, EventArgs e)
	{
		if (ip.Text == "test" && port.Text == "0000")
		{
			Utilities.tryDo(async() =>
			{
                string result = await DisplayPromptAsync("input number", null,
					initialValue: "5", maxLength: 3, keyboard: Keyboard.Numeric);
				int max = Convert.ToInt32(result);

                for (int i = 0; i < max; i++)
				{
					addTestDevices($"Test:{i.ToString()}");
				}
                Assets.refreshList();
                await Navigation.PopAsync();
            });

            
			
		}
	}
	public void addTestDevices(string name)
	{
		if(!Assets.addDevice(new SmartDeviceInfo() { ip = "test", port = 0000, name = name }))
		{
			Utilities.popupMessage(Language.faild);
		}
	}

	
}
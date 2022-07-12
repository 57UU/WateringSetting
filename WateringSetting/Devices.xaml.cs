using System.Globalization;
using System.Text;

namespace WateringSetting;


/// <summary>
/// Show devices connected here
/// </summary>
public partial class Devices : ContentPage
{
	private bool isSelectingMode = false;
	public Devices()
	{
		
		InitializeComponent();
        this.Title = Language.devices_Connected;
        Assets.refreshList = refreshList;
		Utilities.readDevices();
		
    }

	private void addDeviceBtn(object sender, EventArgs e)
	{
		if (isSelectingMode)
		{
			Utilities.popupMessage(Language.faild);
			return;
		}
		Navigation.PushAsync(new Connect());
	}
	public void refreshList() {
		if (isSelectingMode)
		{
			Utilities.popupMessage(Language.faild);
			return;	
		}
		forceRefreshList();

    }
	private void forceRefreshList()
	{
        layout.Clear();
        foreach (var i in Assets.devices)
        {
            layout.Add(i.Value);
        }
        Utilities.writeDevices();
    }

	private void EnhancedButton_Clicked(object sender, EventArgs e)
	{
		refreshList();	
	}

	private async void Delete_Clicked_1(object sender, EventArgs e)
	{
        
        if (!isSelectingMode)
		{
			//start delete
			foreach(SingleDevice singleDevice in layout.Children)
			{
				singleDevice.StartSelectDeleteDevice();
			}
            
        }
		else
		{
			//stop delete
			foreach(SingleDevice singleDevice in layout.Children)
			{
				singleDevice.stopSelectDeleteDevice();
			}
			if(Assets.beingdeletedDevices.Count > 0)
			{
                StringBuilder sb = new();
                Assets.beingdeletedDevices.ForEach((device) => {
                    sb.Append(device.name);
                    sb.Append("\n");
                });
                bool answer = await DisplayAlert
                    (Language.confirm, Language.following_devices_will_be_deleted + $":\n{sb}", "Yes", "No");
                if (answer)
                {
					foreach(var device in Assets.beingdeletedDevices)
					{
                        Assets.devices.Remove(device.name);
                    }
                    forceRefreshList();
                }

                Assets.beingdeletedDevices.Clear();
            }

        }
        isSelectingMode = !isSelectingMode;




    }
}
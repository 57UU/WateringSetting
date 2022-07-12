namespace WateringSetting;

public partial class SingleDevice : ContentView
{
	public SingleDevice(in SmartDeviceInfo info)
	{
		InitializeComponent();
		deviceInfo = info;
		title.Text = deviceInfo.name;
	}
	public readonly SmartDeviceInfo deviceInfo;

	private void EnhancedButton_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new SingleDeviceSetting(deviceInfo));
	}
	public void StartSelectDeleteDevice()
	{
		settingBtn.IsVisible = false;
		checkBox.IsVisible = true;
        //UpdateChildrenLayout();
    }
	public void stopSelectDeleteDevice()
	{
		if (checkBox.IsChecked == true) {
            Assets.beingdeletedDevices.Add(deviceInfo);
        }
		checkBox.IsChecked = false;
        settingBtn.IsVisible = true;
        checkBox.IsVisible = false;
		//UpdateChildrenLayout();
    }
}
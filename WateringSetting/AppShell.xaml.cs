

namespace WateringSetting;



public partial class AppShell : Shell
{

	public AppShell()
	{
        InitializeComponent();
        
        try
        {

            if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
            {
                DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            }
            else
            {
                FlyoutBehavior = FlyoutBehavior.Locked;
            }
               
        }catch
        {     }
       


    }

    private void DeviceDisplay_MainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        if(e.DisplayInfo.Orientation==DisplayOrientation.Portrait)
        {
            //screen vertical
            FlyoutBehavior = FlyoutBehavior.Flyout;
        }else if (e.DisplayInfo.Orientation == DisplayOrientation.Landscape)
        {
            //screen horizon
            FlyoutBehavior = FlyoutBehavior.Locked;
        }
        else
        {
            FlyoutBehavior = FlyoutBehavior.Flyout;
        }
    }
}

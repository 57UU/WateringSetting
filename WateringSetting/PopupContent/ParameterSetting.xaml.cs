
namespace WateringSetting.PopupContent;



public partial class ParameterSetting : ContentPage
{
	private SmartDeviceInfo info;
	public ParameterSetting(SmartDeviceInfo deviceInfo)
	{
		InitializeComponent();
		info = deviceInfo;


        Title = Language.parameter_Config;
		use_senser.Text = Language.use_senser;
        hor_routateText = Language.horizon + " " + Language.rotate + ":";
		vtcText = Language.vertical + " " + Language.rotate + ":";
		pressureText = Language.pressure + ":";


        horizon_label.Text = hor_routateText;
        vertical_label.Text = vtcText;
        pressure_label.Text = pressureText;


        if (orientation.IsSupported)
        {
            isUseSenser_Toggled(null, new ToggledEventArgs(true));
            this.Unfocused += (sender, e) =>
            {
                //close reading orientation
                try
                {
                    orientation.Stop();
                }
                catch (Exception ignored)
                {

                }
                try
                {
                    orientation.ReadingChanged -= Orientation_ReadingChanged;
                }
                catch (Exception ignored)
                {

                }
            };
        }
        else
        {
            isUseSenser.IsToggled = false;
            isUseSenser.IsEnabled=false;
            use_senser.Text = Language.unsupported;
        }

        
    }
	private string hor_routateText;
    private void horizon_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		horizon_label.Text = hor_routateText + e.NewValue;
    }
	private string vtcText ;
	private void vertical_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		vertical_label.Text= vtcText+e.NewValue;

    }
	private string pressureText;
	private void pressure_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		pressure_label.Text= pressureText + e.NewValue;

    }


    IOrientationSensor orientation = OrientationSensor.Default;
    int halfMAx1 = 360;
    int halfMax2 = 50;
    private void Orientation_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
    {
        // Update UI Label with orientation state
        detail.Text = $"Orientation: {e.Reading}";
        float x, y, z;
        (x,y,z)= Utilities.transformQuaternion(e.Reading.Orientation);
        horizon.Value = (x/2 + 0.5) * halfMAx1;
        vertical.Value = (y / 2 + 0.5) * halfMAx1;
        pressure.Value =( z / 2 + 0.5) * halfMax2;


    }


    private void isUseSenser_Toggled(object sender, ToggledEventArgs e)
    {
        if(!orientation.IsSupported)
            return;
        if (e.Value) {
            try
            {
                // Turn on compass
                orientation.Start(SensorSpeed.UI);
            }
            catch{}
            orientation.ReadingChanged += Orientation_ReadingChanged;
            
            pressure.IsEnabled=vertical.IsEnabled= horizon.IsEnabled = false;

        }
        else
        {
            // Turn off compass
            try {
                pressure.IsEnabled = vertical.IsEnabled = horizon.IsEnabled = true;
                orientation.Stop();
                orientation.ReadingChanged -= Orientation_ReadingChanged;
            }catch { 
            
            }
            
            
        }
    }
}


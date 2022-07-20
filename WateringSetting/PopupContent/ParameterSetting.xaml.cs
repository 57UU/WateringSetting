
namespace WateringSetting.PopupContent;



public partial class ParameterSetting : ContentPage
{
	private SmartDeviceInfo info;
    private ValueSetter horizon, vertical, pressure;
	public ParameterSetting(SmartDeviceInfo deviceInfo)
	{
		InitializeComponent();
		info = deviceInfo;


        Title = Language.parameter_Config;
		use_senser.Text = Language.use_senser;
        var hor_routateText = Language.horizon + " " + Language.rotate;
		var vtcText = Language.vertical + " " + Language.rotate;
		var pressureText = Language.pressure;


        horizon = new ValueSetter(hor_routateText, info.horizonRotation) { max = max1 };
        vertical = new ValueSetter(vtcText, info.verticalRotation) { max = max1 };
        pressure = new ValueSetter(pressureText, info.pressure) { max = max2 };

        horizon.ValueChanged += (o,e) => info.horizonRotation= (float)e.NewValue;
        vertical.ValueChanged += (o, e) => info.verticalRotation= (float)e.NewValue;
        pressure.ValueChanged += (o, e) => info.pressure= (float)e.NewValue ;


        layout.Add(horizon);
        layout.Add(vertical);
        layout.Add(pressure);


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
                catch
                {   }
                try
                {
                    orientation.ReadingChanged -= Orientation_ReadingChanged;
                }
                catch
                {     }
            };
        }
        else
        {
            isUseSenser.IsToggled = false;
            isUseSenser.IsEnabled=false;
            use_senser.Text = Language.unsupported;
        }

        
    }
    IOrientationSensor orientation = OrientationSensor.Default;
    int max1 = 360;
    int max2 = 100;

    private void EnhancedButton_Clicked(object sender, EventArgs e)
    {
        isUseSenser.IsToggled = false;
    }

    private void Orientation_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
    {
        // Update UI Label with orientation state
        detail.Text = $"Orientation: {e.Reading}";
        float a, b;
        (a,b,_)= Utilities.transformQuaternion(e.Reading.Orientation);
        //horizon.Value = (x/2 + 0.5) * max1;
        vertical.Value = (a / 2 + 0.5) * max1;
        pressure.Value =( b / 2 + 0.5) * max2;
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
            
            pressure.IsEnabled=vertical.IsEnabled= false;

        }
        else
        {
            // Turn off compass
            try {
                pressure.IsEnabled = vertical.IsEnabled =  true;
                orientation.Stop();
                orientation.ReadingChanged -= Orientation_ReadingChanged;
            }catch { 
            
            }
            
            
        }
    }


}


namespace WateringSetting;

public partial class ValueSetter : ContentView
{
	private float _initvalue;

	public ValueSetter(in string tag,in float initValue)
	{
        InitializeComponent();
		this.tag = tag;
		_initvalue = initValue;	
		this.Loaded += ValueSetter_Loaded;
	}

	private async void ValueSetter_Loaded(object? sender, EventArgs e)
	{
        this.Loaded -= ValueSetter_Loaded;
        await Task.Delay(10);
		//Utilities.popupMessage(_initvalue.ToString());
        stepper.Value = _initvalue;

    }

	public double max { set { slider.Maximum = value; } get { return slider.Maximum; } }
	private string tag;
	private void slider_ValueChanged(object sender,ValueChangedEventArgs e)
	{
		label.Text =$"{tag}:{e.NewValue:F1}";
		if(ValueChanged!=null)
			ValueChanged(this, e);
	}
	public event EventHandler<ValueChangedEventArgs> ValueChanged;
	public new bool IsEnabled{ set { slider.IsEnabled = value; } 
		get {return slider.	IsEnabled;} }
	public  double Value { set { slider.Value = value; }
		get { return slider.Value; }
	}
}
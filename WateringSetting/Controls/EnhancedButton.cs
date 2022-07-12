namespace WateringSetting;

public class EnhancedButton : Button
{
	public EnhancedButton()
	{
		this.Clicked += (s, e) => Utilities.freezeControl(s);
	}
	
}
using System.Reflection;

namespace WateringSetting.PopupContent;

public partial class ChooseLanguage : ContentPage
{
	public ChooseLanguage()
	{
		InitializeComponent();
        MethodInfo[] methods = typeof(Language).GetMethods(BindingFlags.Public | BindingFlags.Static);
		foreach (MethodInfo method in methods)
		{
			Button button = new Button() { Text = method.Name };
			button.Clicked += (s, e) =>
			{
				method.Invoke(null, null);
				Utilities.StoreValue(Utilities.LanguageKey, method.Name);
				Navigation.PopAsync();
				

			};
			layout.Add(button);
		}
    }
}
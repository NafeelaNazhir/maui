using System;
using Microsoft.Maui.Controls;

namespace Maui.Controls.Sample;

public class ShellFeaturePage : NavigationPage
{
	public ShellFeaturePage()
	{
		PushAsync(new ShellFeatureMainPage());
	}
}

public partial class ShellFeatureMainPage : ContentPage
{
	public ShellFeatureMainPage()
	{
		InitializeComponent();
	}

	private void OnShellSearchButtonClicked(object sender, EventArgs e)
	{
		this.Window.Page = new ShellSearchControlPage();
	}
}

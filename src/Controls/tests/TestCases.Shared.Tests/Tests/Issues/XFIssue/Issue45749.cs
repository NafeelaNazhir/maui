using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;
public class Issue45749 : _IssuesUITest
{
	public Issue45749(TestDevice testDevice) : base(testDevice)
	{
	}

	public override string Issue => "Disable horizontal scroll in the custom listview in android";

	[Test]
	[Category(UITestCategories.ListView)]
	public void DisableScrollingOnCustomHorizontalListView()
	{
		App.WaitForElement("Button");
		App.WaitForElement("True");
		App.Screenshot("Custom HorizontalListView Scrolling Enabled Default");
		App.Tap("Button");
		App.WaitForElement("False");
		App.Screenshot("Custom HorizontalListView Scrolling Disabled");
	}
}
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;

public class Issue4714 : _IssuesUITest
{
	const string InitialText ="IncrementLabel";
	public Issue4714(TestDevice testDevice) : base(testDevice)
	{
	}
	public override string Issue => "SingleTapGesture called once on DoubleTap";

	[Test]
	[Category(UITestCategories.Gestures)]
	public void Issue4714Test()
	{
		App.WaitForElement("IncrementLabel");
		App.DoubleTap("IncrementLabel");
		App.Tap("IncrementLabel");
		App.Tap("IncrementLabel");
		Assert.That(App.FindElement("IncrementLabel").GetText(), Is.EqualTo("IncrementLabel: 4"));
	}
}
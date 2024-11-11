#if TEST_FAILS_ON_ANDROID // timed out exception for PopModel
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;

public class Issue7886 : _IssuesUITest
{

	const string TriggerModalAutomationId = "TriggerModal";
	const string PopModalAutomationId = "PopModal";

	public Issue7886(TestDevice testDevice) : base(testDevice)
	{
	}

	public override string Issue => "PushModalAsync modal page with Entry crashes on close for MacOS (NRE)";

	[Test]
	[Category(UITestCategories.Navigation)]
	public void NoNREOnPushModalAsyncAndBack()
	{
		App.WaitForElement("TriggerModal");
		App.Tap("TriggerModal");
		App.WaitForElement("PopModal");
		App.Tap("PopModal");
	}
}
#endif
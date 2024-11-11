#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST  //didn't render into the ui
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;

public class Issue5470 : _IssuesUITest
{
	public Issue5470(TestDevice testDevice) : base(testDevice)
	{
	}

	public override string Issue => "ApplinkEntry Thumbnail required after upgrading to 3.5/3.6";

	[Test]
	[Category(UITestCategories.AppLinks)]
	public void Issue5470Test()
	{
		App.WaitForElement("IssuePageLabel");
	}
}
#endif 
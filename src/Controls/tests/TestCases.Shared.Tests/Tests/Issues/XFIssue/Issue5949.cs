#if TEST_FAILS_ON_CATALYST // Timed out exception
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;

public class Issue5949 : _IssuesUITest
{
	public const string BackButton = "5949GoBack";
	public const string ToolBarItem = "Login";

	public Issue5949(TestDevice testDevice) : base(testDevice)
	{
	}

	public override string Issue => "CollectionView cannot access a disposed object.";

	[Test]
	[Category(UITestCategories.CollectionView)]
	public void DoNotAccessDisposedCollectionView()
	{
		App.WaitForElement("Login");
		App.Tap("Login");
		App.WaitForElement("5949GoBack");
		App.Tap("5949GoBack");
		App.WaitForElement("Login");
	}
}
#endif
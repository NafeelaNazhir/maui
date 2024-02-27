﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace UITests
{
    internal class Issue2777 : IssuesUITest
	{
		const string SwipeViewId = "SwipeViewId";

		public Issue2777(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "When add GroupHeaderTemplate in XAML the group header does not show up";

		[Test]
		public void Issue2777Test()
		{
			App.Screenshot("I am at Issue 2965");
			App.WaitForNoElement("The letter A");
		}
	}
}
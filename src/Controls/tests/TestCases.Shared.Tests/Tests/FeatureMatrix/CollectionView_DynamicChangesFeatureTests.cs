using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests;
public class CollectionView_DynamicChangesFeatureTests : _GalleryUITest
{
	public const string DynamicChangesFeatureMatrix = "CollectionView Feature Matrix";
	public const string Options = "Options";
	public const string Apply = "Apply";

	public override string GalleryPageName => DynamicChangesFeatureMatrix;
	protected override string GallerySubPageButton => "DynamicButton";

	public CollectionView_DynamicChangesFeatureTests(TestDevice device)
		: base(device)
	{
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void ValidateDynamicItemTemplateDisplayed()
	{
		ApplyOptions("ItemTemplateBasic", "ItemsSourceNone", "ItemsSourceObservableCollection");
		App.WaitForElement("DynamicItemTemplateLabel");
		App.WaitForElement("Banana");
		App.WaitForElement("Orange");

		WaitAndTap("ItemTemplateButton");
		App.WaitForElement("Template 1 - Apple");
		App.WaitForElement("Template 1 - Orange");
		App.WaitForNoElement("Banana");
		App.WaitForNoElement("Orange");

		WaitAndTap("ItemTemplateButton");
		App.WaitForElement("Template 2 - Apple");
		App.WaitForElement("Template 2 - Orange");
		App.WaitForNoElement("Template 1 - Apple");
		App.WaitForNoElement("Template 1 - Orange");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicHeaderStringDisplayed()
	{
		ApplyOptions("HeaderString");
		VerifyDynamicUpdates("HeaderStringButton", "Default Header String", "Header String1", "Header String2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicHeaderGridDisplayed()
	{
		ApplyOptions("HeaderGrid");
		VerifyDynamicUpdates("HeaderGridButton", "Default Header Grid", "Header Grid1", "Header Grid2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void ValidateDynamicHeaderTemplateDisplayed()
	{
		ApplyOptions("HeaderTemplateGrid");
		VerifyDynamicUpdates("HeaderTemplateButton", "Default HeaderTemplate", "Header Template1", "Header Template2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicFooterStringDisplayed()
	{
		ApplyOptions("FooterString");
		VerifyDynamicUpdates("FooterStringButton", "Default Footer String", "Footer String1", "Footer String2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicFooterGridDisplayed()
	{
		ApplyOptions("FooterGrid");
		VerifyDynamicUpdates("FooterGridButton", "Default Footer Grid", "Footer Grid1", "Footer Grid2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void ValidateDynamicFooterTemplateDisplayed()
	{
		ApplyOptions("FooterTemplateGrid");
		VerifyDynamicUpdates("FooterTemplateButton", "Default FooterTemplate", "Footer Template1", "Footer Template2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicGroupHeaderTemplateDisplayed()
	{
		ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "GroupHeaderTemplateGrid");
		VerifyDynamicUpdates("GroupHeaderTemplateButton", "Default GroupHeaderTemplate", "GroupHeaderTemplate1", "GroupHeaderTemplate2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void ValidateDynamicGroupFooterTemplateDisplayed()
	{
		ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "GroupFooterTemplateGrid");
		VerifyDynamicUpdates("GroupFooterTemplateButton", "Default GroupFooterTemplate", "GroupFooterTemplate1", "GroupFooterTemplate2");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicHeaderSelectionChanges()
	{
		ApplyOptions("HeaderString", "HeaderGrid");
		App.WaitForElement("Default Header Grid");

		WaitAndTap("HeaderStringButton");
		App.WaitForNoElement("Header String1");
		VerifyDynamicUpdates("HeaderGridButton", "Default Header Grid", "Header Grid1", "Header Grid2");

		ApplyOptions("HeaderString", "HeaderGrid", "HeaderNone");
		App.WaitForElement("Apple");
		App.WaitForNoElement("Default Header String");
		App.WaitForNoElement("Default Header Grid");
		App.WaitForNoElement("Header Grid2");

		WaitAndTap("HeaderStringButton");
		App.WaitForNoElement("Header String1");
		WaitAndTap("HeaderGridButton");
		App.WaitForNoElement("Header Grid1");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void ValidateDynamicFooterSelectionChanges()
	{
		ApplyOptions("FooterString", "FooterGrid");
		App.WaitForElement("Default Footer Grid");

		WaitAndTap("FooterStringButton");
		App.WaitForNoElement("Footer String1");
		VerifyDynamicUpdates("FooterGridButton", "Default Footer Grid", "Footer Grid1", "Footer Grid2");

		ApplyOptions("FooterString", "FooterGrid", "FooterNone");
		App.WaitForElement("Apple");
		App.WaitForNoElement("Default Footer String");
		App.WaitForNoElement("Default Footer Grid");
		App.WaitForNoElement("Footer Grid2");

		WaitAndTap("FooterStringButton");
		App.WaitForNoElement("Footer String1");
		WaitAndTap("FooterGridButton");
		App.WaitForNoElement("Footer Grid1");
	}

#if TEST_FAILS_ON_WINDOWS
	//[Testing] EmptyView(null ItemsSource) elements Not Accessible via Automation on Windows Platform Issue Link:  https://github.com/dotnet/maui/issues/28022
	//EmptyViewTemplate not shown in Windows Issue Link: https://github.com/dotnet/maui/issues/28334
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void ValidateDynamicEmptyViewStringDisplayed()
	{
		ApplyOptions("EmptyViewString", "ItemsSourceNone");
		VerifyDynamicUpdates("EmptyViewStringButton", "No Items Available(String)",
			"EmptyView String1: No items available", "EmptyView String2: No items available");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void ValidateDynamicEmptyViewGridDisplayed()
	{
		ApplyOptions("EmptyViewGrid", "ItemsSourceNone");
		VerifyDynamicUpdates("EmptyViewGridButton", "No Items Available(Grid View)",
			"EmptyView Grid1: No items available", "EmptyView Grid2: No items available");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void ValidateDynamicEmptyViewTemplateDisplayed()
	{
		ApplyOptions("EmptyViewTemplateGrid", "ItemsSourceNone");
		VerifyDynamicUpdates("EmptyViewTemplateButton", "No Template Items Available(Grid View)",
			"EmptyViewTemplate1: No items available", "EmptyViewTemplate2: No items available");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void ValidateDynamicEmptyViewSelectionChanges()
	{
		ApplyOptions("EmptyViewString", "EmptyViewGrid", "ItemsSourceNone");
		App.WaitForElement("No Items Available(Grid View)");

		WaitAndTap("EmptyViewStringButton");
		App.WaitForNoElement("EmptyView String1: No items available");
		VerifyDynamicUpdates("EmptyViewGridButton", "No Items Available(Grid View)",
			"EmptyView Grid1: No items available", "EmptyView Grid2: No items available");

		ApplyOptions("EmptyViewString", "EmptyViewGrid", "EmptyViewNone", "ItemsSourceNone");
		App.WaitForElement("EmptyViewGridButton");
		App.WaitForNoElement("No Items Available(String)");
		App.WaitForNoElement("No Items Available(Grid View)");
		App.WaitForNoElement("EmptyView Grid2: No items available");

		WaitAndTap("EmptyViewStringButton");
		App.WaitForNoElement("EmptyView String1: No items available");
		WaitAndTap("EmptyViewGridButton");
		App.WaitForNoElement("EmptyView Grid1: No items available");
	}
#endif

	private void ApplyOptions(params string[] optionIds)
	{
		WaitAndTap(Options);

		foreach (var optionId in optionIds)
		{
			WaitAndTap(optionId);
		}

		WaitAndTap(Apply);
	}

	private void VerifyDynamicUpdates(string buttonId, string initialText, string firstUpdateText, string secondUpdateText)
	{
		App.WaitForElement(initialText);

		WaitAndTap(buttonId);
		App.WaitForElement(firstUpdateText);
		App.WaitForNoElement(initialText);

		WaitAndTap(buttonId);
		App.WaitForElement(secondUpdateText);
		App.WaitForNoElement(firstUpdateText);
	}

	private void WaitAndTap(string automationId)
	{
		App.WaitForElement(automationId);
		App.Tap(automationId);
	}
}
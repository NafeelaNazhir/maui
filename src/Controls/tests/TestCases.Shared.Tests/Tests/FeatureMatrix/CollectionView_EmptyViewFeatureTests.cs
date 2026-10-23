#if TEST_FAILS_ON_WINDOWS // EmptyView Elements not accessible via Automation on Windows, Issue Link: https://github.com/dotnet/maui/issues/28022
// EmptyViewTemplate not displayed on Windows Issue Link: https://github.com/dotnet/maui/issues/28334
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;


namespace Microsoft.Maui.TestCases.Tests
{
	public class CollectionView_EmptyViewFeatureTests : _GalleryUITest
	{
		public const string CollectionViewFeatureMatrix = "CollectionView Feature Matrix";

		public override string GalleryPageName => CollectionViewFeatureMatrix;
		protected override string GallerySubPageButton => "EmptyViewButton";

		public CollectionView_EmptyViewFeatureTests(TestDevice device)
			: base(device)
		{
		}

		void ApplyOptions(params string[] optionIds)
		{
			App.WaitForElement("Options");
			App.Tap("Options");

			foreach (var optionId in optionIds)
			{
				App.WaitForElement(optionId);
				App.Tap(optionId);
			}

			App.WaitForElement("Apply");
			App.Tap("Apply");
		}

		void ScrollToAndWaitForFooter(string footer, bool isHorizontal)
		{
			if (isHorizontal)
				App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
			else
				App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);

			App.WaitForElement(footer);
		}

		void AssertEmptyViewSize(string automationId)
		{
			var scale = App switch
			{
				AppiumAndroidApp => App.GetDisplayDensity(),
				// The HostApp uses Catalyst's iPad idiom, which scales UIKit points to 77%.
				// https://developer.apple.com/videos/play/wwdc2020/10143/
				AppiumCatalystApp => 0.77,
				_ => 1.0
			};

			App.RetryAssert(() =>
			{
				var bounds = App.WaitForElement(automationId).GetRect();
				Assert.That(bounds.Width, Is.EqualTo(300 * scale).Within(5),
					$"{automationId} should have a width of 300 logical units (native scale: {scale}).");
				Assert.That(bounds.Height, Is.EqualTo(200 * scale).Within(5),
					$"{automationId} should have a height of 200 logical units (native scale: {scale}).");
			});
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewStringDisplayed()
		{
			ApplyOptions("EmptyViewString");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateCustomEmptyViewDisplayed()
		{
			ApplyOptions("EmptyViewGrid");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomEmptyViewSizeDisplayed()
		{
			ApplyOptions("EmptyViewCustomSize");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateEmptyViewStringDisplayed_AndItemSourceEmptyObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyObservableCollection", "EmptyViewString");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateEmptyViewStringSetFirst_AndEmptyObservableCollection()
		{
			ApplyOptions("EmptyViewString", "ItemsSourceEmptyObservableCollection");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewStringDisplayed_AndObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceObservableCollection", "EmptyViewString");
			App.WaitForNoElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateEmptyViewStringSetFirst_AndObservableCollection()
		{
			ApplyOptions("EmptyViewString", "ItemsSourceObservableCollection");
			App.WaitForNoElement("No Items Available(String)");
		}

		[TestCase("EmptyViewString", "No Items Available(String)")]
		[TestCase("EmptyViewGrid", "No Items Available(Grid View)")]
		[TestCase("EmptyViewTemplateGrid", "No Template Items Available(Grid View)")]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateEmptyViewReappears_WhenObservableCollectionIsReplacedWithNull(string emptyViewOption, string emptyViewText)
		{
			ApplyOptions(emptyViewOption, "ItemsSourceObservableCollection", "ItemsSourceNone");
			App.WaitForElement(emptyViewText);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateEmptyViewStringDisplayed_AndEmptyGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyGroupedList", "EmptyViewString");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewStringDisplayedSetFirst_AndItemSourceEmptyGroupedList()
		{
			ApplyOptions("EmptyViewString", "ItemsSourceEmptyGroupedList");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateEmptyViewStringDisplayed_GroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "EmptyViewString");
			App.WaitForNoElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateEmptyViewStringDisplayedSetFirst_AndGroupedList()
		{
			ApplyOptions("EmptyViewString", "ItemsSourceGroupedList", "IsGroupedTrue");
			App.WaitForNoElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void VerifyCustomEmptyViewDisplaysCorrectly_WithLeftToRightFlowDirection()
		{
			ApplyOptions("EmptyViewGrid", "FlowDirectionLeftToRight");
			App.WaitForElement("No Items Available(Grid View)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void VerifyCustomEmptyViewDisplaysCorrectly_WithRightToLeftFlowDirection()
		{
			ApplyOptions("EmptyViewGrid", "FlowDirectionRightToLeft");
			App.WaitForElement("No Items Available(Grid View)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void VerifyCustomSizedEmptyViewDisplaysCorrectly_WithLeftToRightFlowDirection()
		{
			ApplyOptions("EmptyViewCustomSize", "FlowDirectionLeftToRight");
			App.WaitForElement("Custom Empty View (Sized)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void VerifyCustomSizedEmptyViewDisplaysCorrectly_WithRightToLeftFlowDirection()
		{
			ApplyOptions("EmptyViewCustomSize", "FlowDirectionRightToLeft");
			App.WaitForElement("Custom Empty View (Sized)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomEmptyViewDisplayed_AndEmptyObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyObservableCollection", "EmptyViewGrid");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateCustomEmptyViewDisplayedSetFirst_AndEmptyObservableCollection()
		{
			ApplyOptions("EmptyViewGrid", "ItemsSourceEmptyObservableCollection");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateCustomEmptyViewDisplayed_AndObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceObservableCollection", "EmptyViewGrid");
			App.WaitForNoElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateCustomEmptyViewDisplayedSetFirst_AndObservableCollection()
		{
			ApplyOptions("EmptyViewGrid", "ItemsSourceObservableCollection");
			App.WaitForNoElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomEmptyViewDisplayed_EmptyGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyGroupedList", "EmptyViewGrid");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateCustomEmptyViewDisplayedSetFirst_AndEmptyGroupedList()
		{
			ApplyOptions("EmptyViewGrid", "ItemsSourceEmptyGroupedList");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomEmptyViewDisplayed_GroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "EmptyViewGrid");
			App.WaitForNoElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateCustomEmptyViewDisplayedSetFirst_AndGroupedList()
		{
			ApplyOptions("EmptyViewGrid", "ItemsSourceGroupedList", "IsGroupedTrue");
			App.WaitForNoElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndEmptyObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyObservableCollection", "EmptyViewCustomSize");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomEmptyViewSizeDisplayedSetFirst_AndEmptyObservableCollection()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsSourceEmptyObservableCollection");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceObservableCollection", "EmptyViewCustomSize");
			App.WaitForNoElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomEmptyViewSizeDisplayedSetFirst_AndObservableCollection()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsSourceObservableCollection");
			App.WaitForNoElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateCustomEmptyViewSizeDisplayed_EmptyGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyGroupedList", "EmptyViewCustomSize");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateCustomEmptyViewSizeDisplayedSetFirst_AndEmptyGroupedList()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsSourceEmptyGroupedList");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomEmptyViewSizeDisplayed_GroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "EmptyViewCustomSize");
			App.WaitForNoElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateCustomEmptyViewSizeDisplayedSetFirst_AndGroupedList()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsSourceGroupedList", "IsGroupedTrue");
			App.WaitForNoElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateEmptyViewStringDisplayed_AndEmptyViewTemplate()
		{
			ApplyOptions("EmptyViewString", "EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyViewDisplayed_AndEmptyViewTemplate()
		{
			ApplyOptions("EmptyViewGrid", "EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndEmptyViewTemplate()
		{
			ApplyOptions("EmptyViewCustomSize", "EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndCustomEmptyViewTemplateSize()
		{
			ApplyOptions("EmptyViewCustomSize", "EmptyViewTemplateCustomSize");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateEmptyViewStringDisplayed_AndBasicItemTemplateSetFirst()
		{
			ApplyOptions("ItemTemplateBasic", "EmptyViewString");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateEmptyViewStringDisplayedSetFirst_AndBasicItemTemplate()
		{
			ApplyOptions("EmptyViewString", "ItemTemplateBasic");
			App.WaitForElement("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomEmptyViewDisplayed_WhenBasicItemTemplateSetFirst()
		{
			ApplyOptions("ItemTemplateBasic", "EmptyViewGrid");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateCustomEmptyViewDisplayedSetFirst_WhenBasicItemTemplate()
		{
			ApplyOptions("EmptyViewGrid", "ItemTemplateBasic");
			App.WaitForElement("No Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomEmptyViewSizeDisplayed_WhenBasicItemTemplateSetFirst()
		{
			ApplyOptions("ItemTemplateBasic", "EmptyViewCustomSize");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateCustomEmptyViewSizeDisplayedSetFirst_WhenBasicItemTemplate()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemTemplateBasic");
			App.WaitForElement("Custom Empty View (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateEmptyViewTemplateDisplayed()
		{
			ApplyOptions("EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed()
		{
			ApplyOptions("EmptyViewTemplateCustomSize");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void VerifyEmptyViewTemplateDisplaysCorrectly_WithLeftToRightFlowDirection()
		{
			ApplyOptions("EmptyViewTemplateGrid", "FlowDirectionLeftToRight");
			App.WaitForElement("No Template Items Available(Grid View)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void VerifyEmptyViewTemplateDisplaysCorrectly_WithRightToLeftFlowDirection()
		{
			ApplyOptions("EmptyViewTemplateGrid", "FlowDirectionRightToLeft");
			App.WaitForElement("No Template Items Available(Grid View)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void VerifyCustomSizedEmptyViewTemplateDisplaysCorrectly_WithLeftToRightFlowDirection()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "FlowDirectionLeftToRight");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void VerifyCustomSizedEmptyViewTemplateDisplaysCorrectly_WithRightToLeftFlowDirection()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "FlowDirectionRightToLeft");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			VerifyScreenshot();
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewTemplateDisplayed_EmptyObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyObservableCollection", "EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewTemplateDisplayedSetFirst_AndEmptyObservableCollection()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsSourceEmptyObservableCollection");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateEmptyViewTemplateDisplayed_AndeObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceObservableCollection", "EmptyViewTemplateGrid");
			App.WaitForNoElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateEmptyViewTemplateDisplayedSetFirst_AndObservableCollection()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsSourceObservableCollection");
			App.WaitForNoElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewTemplateDisplayed_AndEmptyGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyGroupedList", "EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplateDisplayedSetFirst_AndEmptyGroupedList()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsSourceEmptyGroupedList");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplateDisplayed_AndGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "EmptyViewTemplateGrid");
			App.WaitForNoElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateEmptyViewTemplateDisplayedSetFirst_AndGroupedList()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsSourceGroupedList", "IsGroupedTrue");
			App.WaitForNoElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_EmptyObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyObservableCollection", "EmptyViewTemplateCustomSize");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayedSetFirst_AndEmptyObservableCollection()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsSourceEmptyObservableCollection");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndeObservableCollectionSetFirst()
		{
			ApplyOptions("ItemsSourceObservableCollection", "EmptyViewTemplateCustomSize");
			App.WaitForNoElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayedSetFirst_AndObservableCollection()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsSourceObservableCollection");
			App.WaitForNoElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndEmptyGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceEmptyGroupedList", "EmptyViewTemplateCustomSize");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayedSetFirst_AndEmptyGroupedList()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsSourceEmptyGroupedList");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndGroupedListSetFirst()
		{
			ApplyOptions("ItemsSourceGroupedList", "IsGroupedTrue", "EmptyViewTemplateCustomSize");
			App.WaitForNoElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayedSetFirst_AndGroupedList()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsSourceGroupedList", "IsGroupedTrue");
			App.WaitForNoElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateEmptyViewTemplateDisplayed_AndEmptyViewString()
		{
			ApplyOptions("EmptyViewTemplateGrid", "EmptyViewString");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewTemplateDisplayed_AndCustomEmptyView()
		{
			ApplyOptions("EmptyViewTemplateGrid", "EmptyViewGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndEmptyViewString()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "EmptyViewString");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndCustomEmptyView()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "EmptyViewGrid");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndCustomSizeEmptyView()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "EmptyViewCustomSize");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateEmptyViewTemplateDisplayedSetFirst_AndBasicItemTemplate()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemTemplateBasic");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplateDisplayed_AndBasicTemplateSetFirst()
		{
			ApplyOptions("ItemTemplateBasic", "EmptyViewTemplateGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayedSetFirst_AndBasicItemTemplate()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemTemplateBasic");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomSizeEmptyViewTemplateDisplayed_AndBasicTemplateSetFirst()
		{
			ApplyOptions("ItemTemplateBasic", "EmptyViewTemplateCustomSize");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateEmptyViewSize()
		{
			ApplyOptions("EmptyViewCustomSize");
			AssertEmptyViewSize("EmptyViewLabel");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateEmptyViewTemplateSize()
		{
			ApplyOptions("EmptyViewTemplateCustomSize");
			AssertEmptyViewSize("EmptyViewTemplateLabel");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateEmptyViewStringDisplayed_AndHeaderString()
		{
			ApplyOptions("EmptyViewString", "HeaderString");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateEmptyViewStringDisplayed_AndCustomViewHeader()
		{
			ApplyOptions("EmptyViewString", "HeaderGrid");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndHeaderString()
		{
			ApplyOptions("EmptyViewCustomSize", "HeaderString");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndCustomViewHeader()
		{
			ApplyOptions("EmptyViewCustomSize", "HeaderGrid");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void ValidateEmptyViewStringDisplayed_AndFooterString()
		{
			ApplyOptions("EmptyViewString", "FooterString");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Footer(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateEmptyViewStringDisplayed_AndCustomViewFooter()
		{
			ApplyOptions("EmptyViewString", "FooterGrid");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Footer(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
		public void ValidateCustomEmptyViewDisplayed_AndFooterString()
		{
			ApplyOptions("EmptyViewGrid", "FooterString");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Footer(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void ValidateCustomEmptyViewDisplayed_AndCustomViewFooter()
		{
			ApplyOptions("EmptyViewGrid", "FooterGrid");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Footer(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndFooterString()
		{
			ApplyOptions("EmptyViewCustomSize", "FooterString");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Footer(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void ValidateCustomEmptyViewSizeDisplayed_AndCustomViewFooter()
		{
			ApplyOptions("EmptyViewCustomSize", "FooterGrid");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Footer(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void ValidateCustomEmptyViewDisplayed_AndHeaderString()
		{
			ApplyOptions("EmptyViewGrid", "HeaderString");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void ValidateCustomEmptyViewDisplayed_AndCustomViewHeader()
		{
			ApplyOptions("EmptyViewGrid", "HeaderGrid");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
		}

#if TEST_FAILS_ON_WINDOWS
//ItemsLayout does not change its default value on windows Issue Link: https://github.com/dotnet/maui/issues/27946
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterString_WhenVerticalList()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutVerticalList", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}
        
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterString_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutHorizontalList", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterString_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutVerticalGrid", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterString_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutHorizontalGrid", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterString_WhenVerticalList()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutVerticalList", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterString_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutHorizontalList", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterString_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutVerticalGrid", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterString_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutHorizontalGrid", "HeaderString", "FooterString");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterString_WhenVerticalList()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutVerticalList", "HeaderString", "FooterString");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterString_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutHorizontalList", "HeaderString", "FooterString");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterString_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutVerticalGrid", "HeaderString", "FooterString");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterString_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutHorizontalGrid", "HeaderString", "FooterString");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterString_WhenVerticalList()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutVerticalList", "HeaderString", "FooterString");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}
        
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterString_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutHorizontalList", "HeaderString", "FooterString");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterString_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutVerticalGrid", "HeaderString", "FooterString");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterString_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutHorizontalGrid", "HeaderString", "FooterString");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterString_WhenVerticalList()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutVerticalList", "HeaderString", "FooterString");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}
        
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterString_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutHorizontalList", "HeaderString", "FooterString");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterString_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutVerticalGrid", "HeaderString", "FooterString");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterString_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutHorizontalGrid", "HeaderString", "FooterString");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(String)");
			ScrollToAndWaitForFooter("CollectionView Footer(String)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterView_WhenVerticalList()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutVerticalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}
        
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterView_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutHorizontalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterView_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutVerticalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewString_WithHeaderFooterView_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewString", "ItemsLayoutHorizontalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(String)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterView_WhenVerticalList()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutVerticalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterView_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutHorizontalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterView_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutVerticalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomEmptyView_WithHeaderFooterView_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewGrid", "ItemsLayoutHorizontalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterView_WhenVerticalList()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutVerticalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterView_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutHorizontalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterView_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutVerticalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyView_WithHeaderFooterView_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewCustomSize", "ItemsLayoutHorizontalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom Empty View (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterView_WhenVerticalList()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutVerticalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}
        
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterView_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutHorizontalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterView_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutVerticalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateEmptyViewTemplate_WithHeaderFooterView_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewTemplateGrid", "ItemsLayoutHorizontalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("No Template Items Available(Grid View)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

#endif

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterView_WhenVerticalList()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutVerticalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}
        
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterView_WhenHorizontalList()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutHorizontalList", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterView_WhenVerticalGrid()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutVerticalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: false);
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void ValidateCustomSizeEmptyViewTemplate_WithHeaderFooterView_WhenHorizontalGrid()
		{
			ApplyOptions("EmptyViewTemplateCustomSize", "ItemsLayoutHorizontalGrid", "HeaderGrid", "FooterGrid");
			App.WaitForElement("Custom EmptyViewTemplate (Sized)");
			App.WaitForElement("CollectionView Header(Grid View)");
			ScrollToAndWaitForFooter("CollectionView Footer(Grid View)", isHorizontal: true);
		}
#endif
#endif
	}
}
#endif
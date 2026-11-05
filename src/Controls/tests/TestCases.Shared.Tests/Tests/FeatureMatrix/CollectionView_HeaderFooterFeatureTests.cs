using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests;
public class CollectionView_HeaderFooterFeatureTests : _GalleryUITest
{
	public const string HeaderFooterFeatureMatrix = "CollectionView Feature Matrix";
	public const string Options = "Options";
	public const string Apply = "Apply";
	public const string EmptyViewString = "EmptyViewString";
	public const string HeaderString = "HeaderString";
	public const string HeaderGrid = "HeaderGrid";
	public const string FooterString = "FooterString";
	public const string FooterGrid = "FooterGrid";
	public const string HeaderTemplateGrid = "HeaderTemplateGrid";
	public const string FooterTemplateGrid = "FooterTemplateGrid";
	public const string ItemsSourceGroupedList = "ItemsSourceGroupedList";
	public const string ItemsSourceObservableCollection5 = "ItemsSourceObservableCollection5";
	public const string ItemsSourceObservableCollection25 = "ItemsSourceObservableCollection25";
	public const string ItemsSourceNone = "ItemsSourceNone";
	public const string IsGroupedTrue = "IsGroupedTrue";
	public const string IsGroupedFalse = "IsGroupedFalse";
	public const string ItemTemplateBasic = "ItemTemplateBasic";
	public const string ItemsLayoutHorizontalList = "ItemsLayoutHorizontalList";
	public const string ItemsLayoutHorizontalGrid = "ItemsLayoutHorizontalGrid";
	public const string ItemsLayoutVerticalGrid = "ItemsLayoutVerticalGrid";
	public const string GroupHeaderTemplateGrid = "GroupHeaderTemplateGrid";
	public const string GroupFooterTemplateGrid = "GroupFooterTemplateGrid";

	public override string GalleryPageName => HeaderFooterFeatureMatrix;
	protected override string GallerySubPageButton => "HeaderFooterViewButton";

	public CollectionView_HeaderFooterFeatureTests(TestDevice device)
		: base(device)
	{
	}

	private void ApplyOptions(params string[] optionIds)
	{
		App.WaitForElementTillPageNavigationSettled(Options);
		App.Tap(Options);

		foreach (var optionId in optionIds)
		{
			App.WaitForElementTillPageNavigationSettled(optionId);
			App.Tap(optionId);
		}

		App.WaitForElementTillPageNavigationSettled(Apply);
		App.Tap(Apply);
	}

	private double GetFirstItemTopInset()
	{
		var collectionView = App.WaitForElementTillPageNavigationSettled("CollectionViewControl").GetRect();
		var apple = App.WaitForElementTillPageNavigationSettled("Apple").GetRect();
		return apple.Y - collectionView.Y;
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyFeatureOptionsResetWhenReopeningOptions()
	{
		ApplyOptions(ItemsSourceNone);
		App.WaitForNoElement("Apple");

		ApplyOptions();
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[TestCase(ItemTemplateBasic, ItemsSourceObservableCollection25)]
	[TestCase(ItemsSourceObservableCollection25, ItemTemplateBasic)]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyBasicItemTemplatePreservedWhenChangingItemsSource(string firstOption, string secondOption)
	{
		ApplyOptions();
		var defaultInset = GetFirstItemTopInset();

		ApplyOptions(firstOption, secondOption);
		Assert.That(GetFirstItemTopInset(), Is.LessThan(defaultInset));
	}

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST //In CV2, unintended synchronization between the HeaderTemplate/FooterTemplate and Header/Footer views, related issue: https://github.com/dotnet/maui/issues/28504
	[TestCase(HeaderString, FooterString, "CollectionView Header(String)", "CollectionView Footer(String)")]
	[TestCase(HeaderGrid, FooterGrid, "HeaderViewLabel", "FooterViewLabel")]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyHeaderFooterResetWhenReopeningOptions(string headerOption, string footerOption, string headerId, string footerId)
	{
		ApplyOptions(headerOption, footerOption);
		App.WaitForElementTillPageNavigationSettled(headerId);
		App.WaitForElementTillPageNavigationSettled(footerId);

		ApplyOptions();
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForNoElement(headerId);
		App.WaitForNoElement(footerId);
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderStringWithItemsSourceObservableCollection25()
	{
		ApplyOptions(HeaderString, ItemsSourceObservableCollection25);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Pepper");
		App.WaitForNoElement("CollectionView Header(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyHeaderStringWithItemsSourceObservableCollection5()
	{
		ApplyOptions(HeaderString);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyHeaderStringWithItemsSourceNone()
	{
		ApplyOptions(HeaderString, ItemsSourceNone);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderViewWithItemsSourceObservableCollection25()
	{
		ApplyOptions(HeaderGrid, ItemsSourceObservableCollection25);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Pepper");
		App.WaitForNoElement("CollectionView Header(Grid View)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderViewWithItemsSourceObservableCollection5()
	{
		ApplyOptions(HeaderGrid, ItemsSourceObservableCollection5);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyHeaderViewWithItemsSourceNone()
	{
		ApplyOptions(HeaderGrid, ItemsSourceNone);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
	}

#if TEST_FAILS_ON_WINDOWS //related issues:https://github.com/dotnet/maui/issues/28022
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderStringWithEmptyViewString()
	{
		ApplyOptions(HeaderString, ItemsSourceNone, EmptyViewString);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("No Items Available(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
	public void VerifyHeaderViewWithEmptyViewString()
	{
		ApplyOptions(HeaderGrid, ItemsSourceNone, EmptyViewString);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("No Items Available(String)");
	}
#endif

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderStringWithFooterString()
	{
		ApplyOptions(HeaderString, FooterString);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyHeaderViewWithFooterView()
	{
		ApplyOptions(HeaderGrid, FooterGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderStringWithFooterView()
	{
		ApplyOptions(HeaderString, FooterGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyHeaderViewWithFooterString()
	{
		ApplyOptions(HeaderGrid, FooterString);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}

#if TEST_FAILS_ON_ANDROID //related issue: https://github.com/dotnet/maui/issues/28334
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void VerifyHeaderStringWhenFooterTemplateView()
		{
			ApplyOptions(HeaderString, FooterTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
		public void VerifyHeaderViewWhenFooterTemplateView()
		{
			ApplyOptions(HeaderGrid, FooterTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}
#endif

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In CV2 related issue: https://github.com/dotnet/maui/issues/28509, In windows related issue: https://github.com/dotnet/maui/issues/28824
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyHeaderStringWhenGroupHeaderTemplateView()
	{
		ApplyOptions(HeaderString, IsGroupedTrue, ItemsSourceGroupedList, GroupHeaderTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("GroupHeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Potato");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyHeaderViewWhenGroupHeaderTemplateView()
	{
		ApplyOptions(HeaderGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupHeaderTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("GroupHeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyHeaderStringWhenGroupFooterTemplateView()
	{
		ApplyOptions(HeaderString, IsGroupedTrue, ItemsSourceGroupedList, GroupFooterTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("GroupFooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void VerifyHeaderViewWhenGroupFooterTemplateView()
	{
		ApplyOptions(HeaderGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupFooterTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("GroupFooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
	}
#endif

#if TEST_FAILS_ON_WINDOWS  //related issue: https://github.com/dotnet/maui/issues/28337
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyHeaderStringWhenHeaderTemplateView()
	{
		ApplyOptions(HeaderString, HeaderTemplateGrid);
		App.WaitForNoElement("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyHeaderViewWhenHeaderTemplateView()
	{
		ApplyOptions(HeaderGrid, HeaderTemplateGrid);
		App.WaitForNoElement("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}
#endif

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //relate issue: https://github.com/dotnet/maui/issues/28824
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyHeaderStringWhenIsGroupedTrueOrFalse()
		{
			ApplyOptions(HeaderString, IsGroupedTrue);
			App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
			App.WaitForElementTillPageNavigationSettled("Apple");
			ApplyOptions(HeaderString, IsGroupedFalse);
			App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
			App.WaitForElementTillPageNavigationSettled("Apple");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyHeaderViewWhenIsGroupedTrueOrFalse()
		{
			ApplyOptions(HeaderGrid, IsGroupedTrue);
			App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			ApplyOptions(HeaderGrid, IsGroupedFalse);
			App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
		}
#endif

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyHeaderStringWhenBasicDataTemplateView()
	{
		ApplyOptions(HeaderString, ItemTemplateBasic);
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderViewWhenBasicDataTemplateView()
	{
		ApplyOptions(HeaderGrid, ItemTemplateBasic);
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
	}

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In windows, related issue: https://github.com/dotnet/maui/issues/27946 and In CV2, related issue: https://github.com/dotnet/maui/issues/28678
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyHeaderStringWithItemsLayoutVerticalGrid()
	{
		ApplyOptions(HeaderString, ItemsLayoutVerticalGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyHeaderViewWithItemsLayoutVerticalGrid()
	{
		ApplyOptions(HeaderGrid, ItemsLayoutVerticalGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderStringWithItemsLayoutHorizontalList()
	{
		ApplyOptions(HeaderString, ItemsLayoutHorizontalList);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderViewWithItemsLayoutHorizontalList()
	{
		ApplyOptions(HeaderGrid, ItemsLayoutHorizontalList);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyHeaderViewWithItemsLayoutHorizontalGrid()
	{
		ApplyOptions(HeaderGrid, ItemsLayoutHorizontalGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyHeaderStringWithItemsLayoutHorizontalGrid()
	{
		ApplyOptions(HeaderString, ItemsLayoutHorizontalGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
	}
#endif


#if TEST_FAILS_ON_ANDROID //related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void VerifyHeaderTemplateWithItemsSourceObserableCollection5()
		{
			ApplyOptions(HeaderTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void VerifyHeaderTemplateWithItemsSourceObserableCollection25()
		{
			ApplyOptions(HeaderTemplateGrid, ItemsSourceObservableCollection25);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
			App.WaitForElementTillPageNavigationSettled("Pepper");
			App.WaitForNoElement("Header Template(Grid View)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
		public void VerifyHeaderTemplateWithItemsSourceNone()
		{
			ApplyOptions(HeaderTemplateGrid, ItemsSourceNone);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		}
#endif

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_WINDOWS //In windows related issue:https://github.com/dotnet/maui/issues/28022, In related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void VerifyHeaderTemplateWhenEmptyViewString()
		{
			ApplyOptions(HeaderTemplateGrid, ItemsSourceNone, EmptyViewString);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("No Items Available(String)");
		}
#endif

#if TEST_FAILS_ON_ANDROID //related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
		public void VerifyHeaderTempalteWhenFooterTemplateView()
		{
			ApplyOptions(HeaderTemplateGrid, FooterTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}
#endif

#if TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_WINDOWS //In CV2: related issue: https://github.com/dotnet/maui/issues/28824 and In windows: https://github.com/dotnet/maui/issues/28824
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyHeaderTemplateWhenGroupFooterTemplate()
	{
		ApplyOptions(HeaderTemplateGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupFooterTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("GroupFooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
	public void VerifyHeaderTemplateWhenGroupHeaderTemplate()
	{
		ApplyOptions(HeaderTemplateGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupHeaderTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("GroupHeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
	}
#endif
#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_WINDOWS  //In CV2, unintended synchronization between the HeaderTemplate/FooterTemplate and Header/Footer views, related issue: https://github.com/dotnet/maui/issues/28504
	//In windows, related issue: https://github.com/dotnet/maui/issues/28337
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
	public void VerifyHeaderTemplateWhenHeaderString()
	{
		ApplyOptions(HeaderTemplateGrid, HeaderString);
		App.WaitForNoElement("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyHeaderTemplateWhenHeaderView()
	{
		ApplyOptions(HeaderTemplateGrid, HeaderGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		App.WaitForNoElement("HeaderViewLabel");
	}
#endif

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_WINDOWS //In all platforms, issue related: https://github.com/dotnet/maui/issues/28824 and CV2, related issues:https://github.com/dotnet/maui/issues/28504
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyHeaderTemplateWhenIsGroupedTrueOrFalse()
		{
			ApplyOptions(HeaderTemplateGrid, IsGroupedTrue);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			ApplyOptions(HeaderTemplateGrid, IsGroupedFalse);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
		}
#endif

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyHeaderTemplateWhenBasicDataTemplateView()
	{
		ApplyOptions(HeaderTemplateGrid, ItemTemplateBasic);
		App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_ANDROID //In windows, related issue: https://github.com/dotnet/maui/issues/27946, In CV2, related issue: https://github.com/dotnet/maui/issues/28678 and In android related issue:https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyHeaderTemplateWithItemsLayoutVerticalGrid()
		{
			ApplyOptions(HeaderTemplateGrid, ItemsLayoutVerticalGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyHeaderTemplateWithItemsLayoutHorizontalList()
		{
			ApplyOptions(HeaderTemplateGrid, ItemsLayoutHorizontalList);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
			App.WaitForElementTillPageNavigationSettled("Mango");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyHeaderTemplateWithItemsLayoutHorizontalGrid()
		{
			ApplyOptions(HeaderTemplateGrid, ItemsLayoutHorizontalGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
			App.WaitForElementTillPageNavigationSettled("Mango");
		}
#endif

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST //In CV2, unintended synchronization between the HeaderTemplate/FooterTemplate and Header/Footer views, related issue: https://github.com/dotnet/maui/issues/28504
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
	public void VerifyFooterStringWithItemsSourceObservableCollection5()
	{
		ApplyOptions(FooterString);
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterStringWithItemsSourceNone()
	{
		ApplyOptions(FooterString, ItemsSourceNone);
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}


	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterStringWithItemsSourceObservableCollection25()
	{
		ApplyOptions(FooterString, ItemsSourceObservableCollection25);
		App.WaitForNoElement("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Pepper");
		App.WaitForElement("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterViewWithItemsSourceObservableCollection5()
	{
		ApplyOptions(FooterGrid);
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterViewWithItemsSourceObservableCollection25()
	{
		ApplyOptions(FooterGrid, ItemsSourceObservableCollection25);
		App.WaitForNoElement("CollectionView Footer(Grid View)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Pepper");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(Grid View)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterViewWithItemsSourceNone()
	{
		ApplyOptions(FooterGrid, ItemsSourceNone);
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In android related issue:https://github.com/dotnet/maui/issues/28622, In windows related issue:https://github.com/dotnet/maui/issues/28022 and In CV2, related issue: https://github.com/dotnet/maui/issues/28604
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterStringWhenEmptyViewString()
		{
			ApplyOptions(FooterString, ItemsSourceNone, EmptyViewString);
			App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
			App.WaitForElementTillPageNavigationSettled("No Items Available(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterViewWhenEmptyViewString()
		{
			ApplyOptions(FooterGrid, ItemsSourceNone, EmptyViewString);
			App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
			App.WaitForElementTillPageNavigationSettled("No Items Available(String)");
		}
#endif

#if TEST_FAILS_ON_WINDOWS //In Windows related issue: https://github.com/dotnet/maui/issues/28337
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyFooterStringWhenFooterTemplateView()
	{
		ApplyOptions(FooterString, FooterTemplateGrid);
		App.WaitForNoElement("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterViewWhenFooterTemplateView()
	{
		ApplyOptions(FooterGrid, FooterTemplateGrid);
		App.WaitForNoElement("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
	}
#endif

#if TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_WINDOWS//In CV2 related issues:https://github.com/dotnet/maui/issues/28509
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void VerifyFooterStringWhenGroupFooterTemplateView()
	{
		ApplyOptions(FooterString, IsGroupedTrue, ItemsSourceGroupedList, GroupFooterTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Fruits");
		App.WaitForElementTillPageNavigationSettled("Vegetables");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
		App.WaitForElementTillPageNavigationSettled("GroupFooterTemplateLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyFooterViewWhenGroupFooterTemplateView()
	{
		ApplyOptions(FooterGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupFooterTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Fruits");
		App.WaitForElementTillPageNavigationSettled("Vegetables");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
		App.WaitForElementTillPageNavigationSettled("GroupFooterTemplateLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void VerifyFooterStringWhenGroupHeaderTemplateView()
	{
		ApplyOptions(FooterString, IsGroupedTrue, ItemsSourceGroupedList, GroupHeaderTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
		App.WaitForElementTillPageNavigationSettled("GroupHeaderTemplateLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyFooterViewWhenGroupHeaderTemplateView()
	{
		ApplyOptions(FooterGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupHeaderTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
		App.WaitForElementTillPageNavigationSettled("GroupHeaderTemplateLabel");
	}
#endif

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyFooterStringWhenHeaderString()
	{
		ApplyOptions(FooterString, HeaderString);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void VerifyFooterViewWhenHeaderString()
	{
		ApplyOptions(FooterGrid, HeaderString);
		App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyFooterStringWhenHeaderView()
	{
		ApplyOptions(FooterString, HeaderGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyFooterViewWhenHeaderView()
	{
		ApplyOptions(FooterGrid, HeaderGrid);
		App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}

#if TEST_FAILS_ON_ANDROID //related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void VerifyFooterStringWhenHeaderTemplate()
		{
			ApplyOptions(FooterString, HeaderTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void VerifyFooterViewWhenHeaderTemplate()
		{
			ApplyOptions(FooterGrid, HeaderTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		}
#endif

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_WINDOWS //related isssue: https://github.com/dotnet/maui/issues/28824
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterStringWhenIsGroupedTrueOrFalse()
		{
			ApplyOptions(FooterString, IsGroupedTrue);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
			ApplyOptions(FooterString, IsGroupedFalse);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterViewWhenIsGroupedTrueOrFalse()
		{
			ApplyOptions(FooterGrid, IsGroupedTrue);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
			ApplyOptions(FooterGrid, IsGroupedFalse);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		}
#endif
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyFooterStringWhenBasicDataTemplateView()
	{
		ApplyOptions(FooterString, ItemTemplateBasic);
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyFooterViewWhenBasicDataTemplateView()
	{
		ApplyOptions(FooterGrid, ItemTemplateBasic);
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
	}

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In windows, related issue: https://github.com/dotnet/maui/issues/27946 and In CV2, related issue: https://github.com/dotnet/maui/issues/28678
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
	public void VerifyFooterStringWithItemsLayoutVerticalGrid()
	{
		ApplyOptions(FooterString, ItemsLayoutVerticalGrid);
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
		App.WaitForElementTillPageNavigationSettled("Apple");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifyFooterViewWithItemsLayoutVerticalGrid()
	{
		ApplyOptions(FooterGrid, ItemsLayoutVerticalGrid);
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterStringWithItemsLayoutHorizontalList()
	{
		ApplyOptions(FooterString, ItemsLayoutHorizontalList);
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
	public void VerifyFooterViewWithItemsLayoutHorizontalList()
	{
		ApplyOptions(FooterGrid, ItemsLayoutHorizontalList);
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterStringWithItemsLayoutHorizontalGrid()
	{
		ApplyOptions(FooterString, ItemsLayoutHorizontalGrid);
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyFooterViewWithItemsLayoutHorizontalGrid()
	{
		ApplyOptions(FooterGrid, ItemsLayoutHorizontalGrid);
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterViewLabel");
	}
#endif
#endif

#if TEST_FAILS_ON_ANDROID //related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void VerifyFooterTemplateWithItemsSourceObservableCollections5()
		{
			ApplyOptions(FooterTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
		}
#endif
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterTemplateWithItemsSourceObservableCollections25()
	{
		ApplyOptions(FooterTemplateGrid, ItemsSourceObservableCollection25);
		App.WaitForNoElement("Footer Template(Grid View)");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.ScrollDown("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
		App.WaitForElementTillPageNavigationSettled("Pepper");
		App.WaitForElementTillPageNavigationSettled("Footer Template(Grid View)");
	}
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyFooterTemplateWithItemsSourceNone()
	{
		ApplyOptions(FooterTemplateGrid, ItemsSourceNone);
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
	}

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In windows related issue:https://github.com/dotnet/maui/issues/28022, In android: https://github.com/dotnet/maui/issues/28101 and In CV2, related issue: https://github.com/dotnet/maui/issues/28604 and https://github.com/dotnet/maui/issues/28504

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterTemplateWhenEmptyViewString()
		{
			ApplyOptions(FooterTemplateGrid, ItemsSourceNone, EmptyViewString);
			App.WaitForElementTillPageNavigationSettled("No Items Available(String)");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}
#endif

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In Windows, related issue: https://github.com/dotnet/maui/issues/28337 and In CV2, related issue: https://github.com/dotnet/maui/issues/28504
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyFooterTemplateWhenFooterString()
	{
		ApplyOptions(FooterTemplateGrid, FooterString);
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForNoElement("CollectionView Footer(String)");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyFooterTemplateWhenFooterView()
	{
		ApplyOptions(FooterTemplateGrid, FooterGrid);
		App.WaitForNoElement("FooterViewLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Mango");
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
	}
#endif

#if TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_WINDOWS //In CV2 related issues: https://github.com/dotnet/maui/issues/28509 and In windows, related issue: https://github.com/dotnet/maui/issues/28824
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 7)]
	public void VerifyFooterTemplateWhenGroupFooterTemplateView()
	{
		ApplyOptions(FooterTemplateGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupFooterTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Fruits");
		App.WaitForElementTillPageNavigationSettled("Vegetables");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
		App.WaitForElementTillPageNavigationSettled("GroupFooterTemplateLabel");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 5)]
	public void VerifyFooterTemplateWhenGroupHeaderTemplateView()
	{
		ApplyOptions(FooterTemplateGrid, IsGroupedTrue, ItemsSourceGroupedList, GroupHeaderTemplateGrid);
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
		App.WaitForElementTillPageNavigationSettled("Potato");
		App.WaitForElementTillPageNavigationSettled("GroupHeaderTemplateLabel");
	}
#endif

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS //In android,related issue: https://github.com/dotnet/maui/issues/28337 and In CV2, reltaed issue:https://github.com/dotnet/maui/issues/28504
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 4)]
		public void VerifyFooterTemplateWhenHeaderString()
		{
			ApplyOptions(FooterTemplateGrid, HeaderString);
			App.WaitForElementTillPageNavigationSettled("CollectionView Header(String)");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void VerifyFooterTemplateWhenHeaderView()
		{
			ApplyOptions(FooterTemplateGrid, HeaderGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderViewLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}
#endif

#if TEST_FAILS_ON_ANDROID //In android,related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
		public void VerifyFooterTemplateWhenHeaderTemplate()
		{
			ApplyOptions(FooterTemplateGrid, HeaderTemplateGrid);
			App.WaitForElementTillPageNavigationSettled("HeaderTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}
#endif

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_WINDOWS //related issue: https://github.com/dotnet/maui/issues/28824
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterTemplateWhenIsGroupedTrueOrFalse()
		{
			ApplyOptions(FooterTemplateGrid, IsGroupedTrue);
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
			ApplyOptions(FooterTemplateGrid, IsGroupedFalse);
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("Mango");
		}
#endif

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 3)]
	public void VerifyFooterTemplateWhenBasicDataTemplateView()
	{
		ApplyOptions(FooterTemplateGrid, ItemTemplateBasic);
		App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		App.WaitForElementTillPageNavigationSettled("Apple");
	}

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_CATALYST && TEST_FAILS_ON_IOS && TEST_FAILS_ON_ANDROID //In windows, related issue: https://github.com/dotnet/maui/issues/27946, In CV2, related issue: https://github.com/dotnet/maui/issues/28678, In android related issue: https://github.com/dotnet/maui/issues/28337
		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterTemplateWithItemsLayoutVerticalGrid()
		{
			ApplyOptions(FooterTemplateGrid, ItemsLayoutVerticalGrid);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterTemplateWithItemsLayoutHorizontalList()
		{
			ApplyOptions(FooterTemplateGrid, ItemsLayoutHorizontalList);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");
		}

		[Test]
		[ShardedTestCategory(UITestCategories.CollectionView, shard: 2)]
		public void VerifyFooterTemplateWithItemsLayoutHorizontalGrid()
		{
			ApplyOptions(FooterTemplateGrid, ItemsLayoutHorizontalGrid);
			App.WaitForElementTillPageNavigationSettled("Apple");
			App.ScrollRight("CollectionViewControl", ScrollStrategy.Gesture, 0.9, 500);
			App.WaitForElementTillPageNavigationSettled("Mango");
			App.WaitForElementTillPageNavigationSettled("FooterTemplateLabel");

		}
#endif
#endif
}
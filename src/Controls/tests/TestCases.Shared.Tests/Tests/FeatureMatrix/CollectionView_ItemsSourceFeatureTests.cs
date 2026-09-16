using NUnit.Framework;
using UITest.Appium;
using UITest.Core;


namespace Microsoft.Maui.TestCases.Tests;

public class CollectionView_ItemsSourceFeatureTests : _GalleryUITest
{
	public const string ItemsSourceFeatureMatrix = "CollectionView Feature Matrix";
	public const string Options = "Options";
	public const string Apply = "Apply";
	public const string ModelItem = "ModelItem";
	public const string ItemsSourceObservableCollection = "ItemsSourceObservableCollection";
	public const string ItemsSourceList = "ItemsSourceList";
	public const string ItemsSourceGroupedList = "ItemsSourceGroupedList";
	public const string EmptyGroupedListT = "EmptyGroupedList";
	public const string EmptyObservableCollectionT = "EmptyObservableCollection";
	public const string IsGroupedTrue = "IsGroupedTrue";
	public const string ItemsLayoutVerticalList = "ItemsLayoutVerticalList";
	public const string ItemsLayoutHorizontalList = "ItemsLayoutHorizontalList";
	public const string ItemsLayoutVerticalGrid = "ItemsLayoutVerticalGrid";
	public const string ItemsLayoutHorizontalGrid = "ItemsLayoutHorizontalGrid";
	public const string AddItems = "AddItems";
	public const string RemoveItems = "RemoveItems";
	public const string ReplaceItemsSource = "ReplaceItemsSource";
	public const string IndexEntry = "IndexEntry";
	public const string CurrentSelectionTextLabel = "CurrentSelectionTextLabel";
	public const string MultipleModePreselection = "MultipleModePreselection";
	public const string SingleModePreselection = "SingleModePreselection";

	public override string GalleryPageName => ItemsSourceFeatureMatrix;
	protected override string GallerySubPageButton => "ItemsSourceButton";

	public CollectionView_ItemsSourceFeatureTests(TestDevice device)
		: base(device)
	{
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
	}
	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Broccoli");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Broccoli");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenItemsSourceIsReplaced()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Banana");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated Item 1");
		App.WaitForNoElement("Banana");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenItemsSourceIsReplacedAndMutated()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Apple");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated Item 1");
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
		App.Tap(RemoveItems);
		App.WaitForNoElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenItemsSourceIsReplaced()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated dotnet_bot.png");
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsListWhenItemsSourceIsReplaced()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Banana");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated Item 1");
		App.WaitForNoElement("Banana");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsListWhenItemsSourceIsReplaced()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated dotnet_bot.png");
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenItemsSourceIsReplaced()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Banana");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated Fruits");
		App.WaitForElement("Updated Vegetables");
		App.WaitForElement("Updated Item 1");
		App.WaitForNoElement("Fruits");
		App.WaitForNoElement("Vegetables");
		App.WaitForNoElement("Banana");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenItemsSourceIsReplaced()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement(ReplaceItemsSource);
		App.Tap(ReplaceItemsSource);
		App.WaitForElement("Updated Group A");
		App.WaitForElement("Updated Group B");
		App.WaitForElement("Updated dotnet_bot.png");
		App.WaitForNoElement("Group A");
		App.WaitForNoElement("Group B");
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsListWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsListWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Broccoli");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Broccoli");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsEmptyGroupedListWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(EmptyGroupedListT);
		App.Tap(EmptyGroupedListT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForNoElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsEmptyGroupedListWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(EmptyGroupedListT);
		App.Tap(EmptyGroupedListT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Broccoli");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsEmptyObservableCollectionWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(EmptyObservableCollectionT);
		App.Tap(EmptyObservableCollectionT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForNoElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsEmptyObservableCollectionWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(EmptyObservableCollectionT);
		App.Tap(EmptyObservableCollectionT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Broccoli");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsItemsSourceNoneWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForNoElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsItemsSourceNoneWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Broccoli");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("green.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("avatar.png");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("avatar.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsListWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("green.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsListWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("calculator.png");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("calculator.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsEmptyGroupedListWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(EmptyGroupedListT);
		App.Tap(EmptyGroupedListT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForNoElement("green.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsEmptyGroupedListWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(EmptyGroupedListT);
		App.Tap(EmptyGroupedListT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("calculator.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsEmptyObservableCollectionWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(EmptyObservableCollectionT);
		App.Tap(EmptyObservableCollectionT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForNoElement("green.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsEmptyObservableCollectionWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(EmptyObservableCollectionT);
		App.Tap(EmptyObservableCollectionT);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("calculator.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsItemsSourceNoneWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForNoElement("green.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsItemsSourceNoneWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("calculator.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenAddIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(AddItems);
		App.WaitForElement("Chikoo");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenRemoveIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Carrot");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(RemoveItems);
		App.WaitForNoElement("Carrot");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenAddIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(AddItems);
		App.WaitForElement("oasis.jpg");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenRemoveIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.Tap(RemoveItems);
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsListWhenAddIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(AddItems);
		App.WaitForElement("Chikoo");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsListWhenRemoveIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Carrot");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(RemoveItems);
		App.WaitForNoElement("Carrot");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsListWhenAddIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "1");
		App.Tap(AddItems);
		App.WaitForElement("groceries.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsListWhenRemoveIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceList);
		App.Tap(ItemsSourceList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.Tap(RemoveItems);
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement("Kiwi");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement("Orange");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Orange");
	}

	[TestCase(ItemsLayoutVerticalList, TestName = "VerifyStringItemsGroupedListWhenAddItemsWithVerticalListItemsLayout")]
	[TestCase(ItemsLayoutHorizontalList, TestName = "VerifyStringItemsGroupedListWhenAddItemsWithHorizontalListItemsLayout")]
	[TestCase(ItemsLayoutVerticalGrid, TestName = "VerifyStringItemsGroupedListWhenAddItemsWithVerticalGridItemsLayout")]
	[TestCase(ItemsLayoutHorizontalGrid, TestName = "VerifyStringItemsGroupedListWhenAddItemsWithHorizontalGridItemsLayout")]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenAddItemsWithItemsLayout(string itemsLayout)
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(itemsLayout);
		App.Tap(itemsLayout);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
	}

	[TestCase(ItemsLayoutVerticalList, TestName = "VerifyStringItemsGroupedListWhenRemoveItemsWithVerticalListItemsLayout")]
	[TestCase(ItemsLayoutHorizontalList, TestName = "VerifyStringItemsGroupedListWhenRemoveItemsWithHorizontalListItemsLayout")]
	[TestCase(ItemsLayoutVerticalGrid, TestName = "VerifyStringItemsGroupedListWhenRemoveItemsWithVerticalGridItemsLayout")]
	[TestCase(ItemsLayoutHorizontalGrid, TestName = "VerifyStringItemsGroupedListWhenRemoveItemsWithHorizontalGridItemsLayout")]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenRemoveItemsWithItemsLayout(string itemsLayout)
	{
		if (Device == TestDevice.Android && itemsLayout == ItemsLayoutVerticalGrid)
			Assert.Ignore("Grouped CollectionView item removal with a vertical grid is not supported on Android.");

		if (Device == TestDevice.Android && itemsLayout == ItemsLayoutHorizontalGrid)
			Assert.Ignore("Grouped CollectionView item removal with a horizontal grid is not supported on Android.");

		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(itemsLayout);
		App.Tap(itemsLayout);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement("Orange");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("Orange");
	}

	[TestCase(ItemsLayoutVerticalList, TestName = "VerifyStringItemsObservableCollectionWhenMutatingWithVerticalListItemsLayout")]
	[TestCase(ItemsLayoutHorizontalList, TestName = "VerifyStringItemsObservableCollectionWhenMutatingWithHorizontalListItemsLayout")]
	[TestCase(ItemsLayoutVerticalGrid, TestName = "VerifyStringItemsObservableCollectionWhenMutatingWithVerticalGridItemsLayout")]
	[TestCase(ItemsLayoutHorizontalGrid, TestName = "VerifyStringItemsObservableCollectionWhenMutatingWithHorizontalGridItemsLayout")]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenMutatingWithItemsLayout(string itemsLayout)
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(itemsLayout);
		App.Tap(itemsLayout);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Apple");
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
		App.Tap(RemoveItems);
		App.WaitForNoElement("Kiwi");
	}

	[TestCase(ItemsLayoutVerticalList, TestName = "VerifyStringItemsGroupedListWhenMutatingByIndexWithVerticalListItemsLayout")]
	[TestCase(ItemsLayoutHorizontalList, TestName = "VerifyStringItemsGroupedListWhenMutatingByIndexWithHorizontalListItemsLayout")]
	[TestCase(ItemsLayoutVerticalGrid, TestName = "VerifyStringItemsGroupedListWhenMutatingByIndexWithVerticalGridItemsLayout")]
	[TestCase(ItemsLayoutHorizontalGrid, TestName = "VerifyStringItemsGroupedListWhenMutatingByIndexWithHorizontalGridItemsLayout")]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenMutatingByIndexWithItemsLayout(string itemsLayout)
	{
		if (Device == TestDevice.Android && itemsLayout == ItemsLayoutVerticalGrid )
			Assert.Ignore("Grouped CollectionView index mutation with a vertical grid is not supported on Android.");

		if (Device == TestDevice.Android && itemsLayout == ItemsLayoutHorizontalGrid)
			Assert.Ignore("Grouped CollectionView index mutation with a horizontal grid is not supported on Android.");

		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(itemsLayout);
		App.Tap(itemsLayout);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Apple");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.Tap(AddItems);
		App.WaitForElement("Kiwi");
		App.EnterText(IndexEntry, "0");
		App.Tap(RemoveItems);
		App.WaitForNoElement("Kiwi");
		App.WaitForElement("Apple");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenAddItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Group A");
		App.WaitForElement("Group B");
		App.WaitForElement(AddItems);
		App.Tap(AddItems);
		App.WaitForElement("green.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenRemoveItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement("Group A");
		App.WaitForElement("Group B");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenAddIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(AddItems);
		App.WaitForElement("Chikoo");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenRemoveIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement("Orange");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "2");
		App.Tap(RemoveItems);
		App.WaitForNoElement("Orange");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenAddIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Group A");
		App.WaitForElement("Group B");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "1");
		App.Tap(AddItems);
		App.WaitForElement("groceries.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenRemoveIndexAtItems()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Group A");
		App.WaitForElement("Group B");
		App.WaitForElement("dotnet_bot.png");
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.Tap(RemoveItems);
		App.WaitForNoElement("dotnet_bot.png");
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenSingleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(SingleModePreselection);
		App.Tap(SingleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("Apple"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsObservableCollectionWhenMultipleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(MultipleModePreselection);
		App.Tap(MultipleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("Apple, Carrot"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenSingleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(SingleModePreselection);
		App.Tap(SingleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("dotnet_bot.png"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsObservableCollectionWhenMultipleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(ItemsSourceObservableCollection);
		App.Tap(ItemsSourceObservableCollection);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement(MultipleModePreselection);
		App.Tap(MultipleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("dotnet_bot.png, avatar.png"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenSingleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement(SingleModePreselection);
		App.Tap(SingleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("Apple"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("No current items"));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyStringItemsGroupedListWhenMultipleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Fruits");
		App.WaitForElement("Vegetables");
		App.WaitForElement(MultipleModePreselection);
		App.Tap(MultipleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("Apple, Carrot"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenSingleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Group A");
		App.WaitForElement("Group B");
		App.WaitForElement(SingleModePreselection);
		App.Tap(SingleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("dotnet_bot.png"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 1)]
	public void VerifyModelItemsGroupedListWhenMultipleModePreSelection()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ModelItem);
		App.Tap(ModelItem);
		App.WaitForElement(IsGroupedTrue);
		App.Tap(IsGroupedTrue);
		App.WaitForElement(ItemsSourceGroupedList);
		App.Tap(ItemsSourceGroupedList);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("Group A");
		App.WaitForElement("Group B");
		App.WaitForElement(MultipleModePreselection);
		App.Tap(MultipleModePreselection);
		Assert.That(App.WaitForElement(CurrentSelectionTextLabel).GetText(), Is.EqualTo("dotnet_bot.png, avatar.png"));
		App.WaitForElement(IndexEntry);
		App.EnterText(IndexEntry, "0");
		App.WaitForElement(RemoveItems);
		App.Tap(RemoveItems);
		VerifyScreenshot(tolerance: 0.5, retryTimeout: TimeSpan.FromSeconds(2));
	}
}
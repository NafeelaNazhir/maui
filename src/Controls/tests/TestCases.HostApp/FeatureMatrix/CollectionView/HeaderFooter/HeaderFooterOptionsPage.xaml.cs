namespace Maui.Controls.Sample;

public partial class HeaderFooterOptionsPage : ContentPage
{
	private readonly CollectionViewViewModel _viewModel;

	public HeaderFooterOptionsPage(CollectionViewViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
		BindingContext = _viewModel;
	}

	private async void ApplyButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private void OnEmptyViewChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == EmptyViewNone)
			_viewModel.EmptyView = null;
		else if (sender == EmptyViewString)
			_viewModel.EmptyView = "No Items Available(String)";
	}

	private void OnHeaderChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == HeaderNone)
			_viewModel.Header = null;
		else if (sender == HeaderString)
			_viewModel.Header = "CollectionView Header(String)";
		else if (sender == HeaderGrid)
			_viewModel.Header = CreateHeaderFooterView("CollectionView Header(Grid View)", Colors.Blue, "HeaderViewLabel");
	}

	private void OnFooterChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == FooterNone)
			_viewModel.Footer = null;
		else if (sender == FooterString)
			_viewModel.Footer = "CollectionView Footer(String)";
		else if (sender == FooterGrid)
			_viewModel.Footer = CreateHeaderFooterView("CollectionView Footer(Grid View)", Colors.Red, "FooterViewLabel");
	}

	private void OnHeaderTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == HeaderTemplateNone)
			_viewModel.HeaderTemplate = null;
		else if (sender == HeaderTemplateGrid)
		{
			_viewModel.HeaderTemplate = new DataTemplate(() =>
				CreateHeaderFooterView("Header Template(Grid View)", Colors.Blue, "HeaderTemplateLabel", FontAttributes.Bold));
		}
	}

	private void OnFooterTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == FooterTemplateNone)
			_viewModel.FooterTemplate = null;
		else if (sender == FooterTemplateGrid)
		{
			_viewModel.FooterTemplate = new DataTemplate(() =>
				CreateHeaderFooterView("Footer Template(Grid View)", Colors.Green, "FooterTemplateLabel", FontAttributes.Bold));
		}
	}

	private void OnGroupHeaderTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == GroupHeaderTemplateNone)
			_viewModel.GroupHeaderTemplate = null;
		else if (sender == GroupHeaderTemplateGrid)
		{
			_viewModel.GroupHeaderTemplate = new DataTemplate(() =>
				CreateHeaderFooterView("Group Header Template(Grid View)", Colors.Green, "GroupHeaderTemplateLabel", FontAttributes.Bold));
		}
	}

	private void OnGroupFooterTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == GroupFooterTemplateNone)
			_viewModel.GroupFooterTemplate = null;
		else if (sender == GroupFooterTemplateGrid)
		{
			_viewModel.GroupFooterTemplate = new DataTemplate(() =>
				CreateHeaderFooterView("Group Footer Template(Grid View)", Colors.Red, "GroupFooterTemplateLabel", FontAttributes.Bold));
		}
	}

	private void OnIsGroupedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == IsGroupedFalse)
			_viewModel.IsGrouped = false;
		else if (sender == IsGroupedTrue)
			_viewModel.IsGrouped = true;
	}

	private void OnItemTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == ItemTemplateNone)
			_viewModel.ItemTemplate = null;
		else if (sender == ItemTemplateBasic)
		{
			_viewModel.ItemTemplate = new DataTemplate(() =>
			{
				var label = new Label();
				label.SetBinding(Label.TextProperty, new Binding("Caption"));
				return label;
			});
		}
	}

	private void OnItemsLayoutChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == ItemsLayoutVerticalList)
			_viewModel.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
		else if (sender == ItemsLayoutHorizontalList)
			_viewModel.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal);
		else if (sender == ItemsLayoutVerticalGrid)
			_viewModel.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical);
		else if (sender == ItemsLayoutHorizontalGrid)
			_viewModel.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Horizontal);
	}

	private void OnItemsSourceChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		var itemTemplate = _viewModel.ItemTemplate;

		if (sender == ItemsSourceObservableCollection25)
			_viewModel.ItemsSourceType = ItemsSourceType.ObservableCollection25T;
		else if (sender == ItemsSourceObservableCollection5)
			_viewModel.ItemsSourceType = ItemsSourceType.ObservableCollection5T;
		else if (sender == ItemsSourceGroupedList)
			_viewModel.ItemsSourceType = ItemsSourceType.GroupedListT;
		else if (sender == ItemsSourceNone)
			_viewModel.ItemsSourceType = ItemsSourceType.None;

		if (_viewModel.ItemTemplate != itemTemplate)
			_viewModel.ItemTemplate = itemTemplate;
	}

	private static Grid CreateHeaderFooterView(string text, Color textColor, string automationId, FontAttributes fontAttributes = FontAttributes.None)
	{
		var grid = new Grid
		{
			BackgroundColor = Colors.LightGray,
			Padding = new Thickness(10)
		};
		grid.Children.Add(new Label
		{
			Text = text,
			FontSize = 18,
			FontAttributes = fontAttributes,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			TextColor = textColor,
			AutomationId = automationId
		});
		return grid;
	}
}

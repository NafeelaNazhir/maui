namespace Maui.Controls.Sample;

public partial class CollectionViewDynamicPage : ContentPage
{
	private readonly CollectionViewViewModel _viewModel;

	public CollectionViewDynamicPage()
	{
		InitializeComponent();
		_viewModel = new CollectionViewViewModel
		{
			ItemsSourceType = ItemsSourceType.ObservableCollection5T
		};
		BindingContext = _viewModel;
	}

	private async void NavigateToOptionsPage_Clicked(object sender, EventArgs e)
	{
		_viewModel.ResetDynamicProperties();
		ResetToggleState();
		await Navigation.PushAsync(new CollectionViewDynamicOptionsPage(_viewModel));
	}

	private bool _isHeaderString1 = true;
	private bool _isFooterString1 = true;
	private bool _isFooterGrid1 = true;
	private bool _isHeaderGrid1 = true;
	private bool _isHeaderTemplate1 = true;
	private bool _isFooterTemplate1 = true;
	private bool _isGroupFooterTemplate1 = true;
	private bool _isGroupHeaderTemplate1 = true;
	private bool _isEmptyViewGrid1 = true;
	private bool _isEmptyViewString1 = true;
	private bool _isEmptyViewTemplate1 = true;
	private bool _isItemTemplate1 = true;

	private void ResetToggleState()
	{
		_isHeaderString1 = true;
		_isFooterString1 = true;
		_isFooterGrid1 = true;
		_isHeaderGrid1 = true;
		_isHeaderTemplate1 = true;
		_isFooterTemplate1 = true;
		_isGroupFooterTemplate1 = true;
		_isGroupHeaderTemplate1 = true;
		_isEmptyViewGrid1 = true;
		_isEmptyViewString1 = true;
		_isEmptyViewTemplate1 = true;
		_isItemTemplate1 = true;
	}

	private void OnHeaderStringButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsHeaderStringSelected)
			return;

		_viewModel.Header = _isHeaderString1 ? "Header String1" : "Header String2";
		_isHeaderString1 = !_isHeaderString1;
	}

	private void OnFooterStringButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsFooterStringSelected)
			return;

		_viewModel.Footer = _isFooterString1 ? "Footer String1" : "Footer String2";
		_isFooterString1 = !_isFooterString1;
	}

	private void OnHeaderGridButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsHeaderGridSelected)
			return;

		_viewModel.Header = _isHeaderGrid1
			? CreateFirstView("Header Grid1", Colors.Blue)
			: CreateSecondView("Header Grid2", height: 150);
		_isHeaderGrid1 = !_isHeaderGrid1;
	}

	private void OnFooterGridButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsFooterGridSelected)
			return;

		_viewModel.Footer = _isFooterGrid1
			? CreateFirstView("Footer Grid1", Colors.Blue)
			: CreateSecondView("Footer Grid2", height: 150);
		_isFooterGrid1 = !_isFooterGrid1;
	}

	private void OnHeaderTemplateButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsHeaderTemplateViewSelected)
			return;

		_viewModel.HeaderTemplate = _isHeaderTemplate1
			? new DataTemplate(() => CreateFirstView("Header Template1", Colors.Blue))
			: new DataTemplate(() => CreateSecondView("Header Template2", height: 150));
		_isHeaderTemplate1 = !_isHeaderTemplate1;
	}

	private void OnFooterTemplateButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsFooterTemplateViewSelected)
			return;

		_viewModel.FooterTemplate = _isFooterTemplate1
			? new DataTemplate(() => CreateFirstView("Footer Template1", Colors.Blue))
			: new DataTemplate(() => CreateSecondView("Footer Template2", height: 150));
		_isFooterTemplate1 = !_isFooterTemplate1;
	}

	private void OnGroupHeaderTemplateButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsGroupHeaderTemplateViewSelected)
			return;

		_viewModel.GroupHeaderTemplate = _isGroupHeaderTemplate1
			? new DataTemplate(() => CreateFirstView("GroupHeaderTemplate1", Colors.Green))
			: new DataTemplate(() => CreateSecondView("GroupHeaderTemplate2"));
		_isGroupHeaderTemplate1 = !_isGroupHeaderTemplate1;
	}

	private void OnGroupFooterTemplateButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsGroupFooterTemplateViewSelected)
			return;

		_viewModel.GroupFooterTemplate = _isGroupFooterTemplate1
			? new DataTemplate(() => CreateFirstView("GroupFooterTemplate1", Colors.Green))
			: new DataTemplate(() => CreateSecondView("GroupFooterTemplate2"));
		_isGroupFooterTemplate1 = !_isGroupFooterTemplate1;
	}

	private void OnEmptyViewStringButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsEmptyViewStringSelected)
			return;

		_viewModel.EmptyView = _isEmptyViewString1
			? "EmptyView String1: No items available"
			: "EmptyView String2: No items available";
		_isEmptyViewString1 = !_isEmptyViewString1;
	}

	private void OnEmptyViewGridButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsEmptyViewGridSelected)
			return;

		_viewModel.EmptyView = _isEmptyViewGrid1
			? CreateFirstView("EmptyView Grid1: No items available", Colors.Blue, backgroundColor: Colors.LightBlue)
			: CreateSecondView("EmptyView Grid2: No items available", fontSize: 14);
		_isEmptyViewGrid1 = !_isEmptyViewGrid1;
	}

	private void OnEmptyViewTemplateButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsEmptyViewTemplateSelected)
			return;

		_viewModel.EmptyViewTemplate = _isEmptyViewTemplate1
			? new DataTemplate(() => CreateFirstView("EmptyViewTemplate1: No items available", Colors.Green))
			: new DataTemplate(() => CreateSecondView("EmptyViewTemplate2: No items available", fontSize: 14));
		_isEmptyViewTemplate1 = !_isEmptyViewTemplate1;
	}

	private void OnItemTemplateButtonClicked(object sender, EventArgs e)
	{
		if (!_viewModel.IsItemTemplateSelected)
			return;

		_viewModel.ItemTemplate = _isItemTemplate1
			? new DataTemplate(() => CreateItemView(isFirstTemplate: true))
			: new DataTemplate(() => CreateItemView(isFirstTemplate: false));
		_isItemTemplate1 = !_isItemTemplate1;
	}

	private static Grid CreateFirstView(string text, Color textColor, Color backgroundColor = null)
	{
		return CreateGrid(new Label
		{
			Text = text,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			TextColor = textColor,
			FontSize = 18,
			FontAttributes = FontAttributes.Bold
		}, backgroundColor ?? Colors.LightGray, padding: 10);
	}

	private static Grid CreateSecondView(string text, double fontSize = 20, double height = -1)
	{
		return CreateGrid(new Label
		{
			Text = text,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			TextColor = Colors.Red,
			FontSize = fontSize,
			HeightRequest = height
		}, Colors.LightYellow, padding: 20);
	}

	private static Grid CreateItemView(bool isFirstTemplate)
	{
		var label = new Label
		{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			TextColor = isFirstTemplate ? Colors.Blue : Colors.Red,
			FontSize = isFirstTemplate ? 18 : 14
		};
		label.SetBinding(Label.TextProperty, new Binding("Caption",
			stringFormat: isFirstTemplate ? "Template 1 - {0}" : "Template 2 - {0}"));

		return CreateGrid(label,
			isFirstTemplate ? Colors.LightGray : Colors.LightYellow,
			padding: isFirstTemplate ? 10 : 20);
	}

	private static Grid CreateGrid(Label label, Color backgroundColor, double padding)
	{
		var grid = new Grid
		{
			BackgroundColor = backgroundColor,
			Padding = new Thickness(padding)
		};
		grid.Children.Add(label);
		return grid;
	}
}
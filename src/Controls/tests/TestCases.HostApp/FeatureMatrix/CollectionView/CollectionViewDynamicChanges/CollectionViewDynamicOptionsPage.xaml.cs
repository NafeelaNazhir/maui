namespace Maui.Controls.Sample;

public partial class CollectionViewDynamicOptionsPage : ContentPage
{
	private readonly CollectionViewViewModel _viewModel;

	public CollectionViewDynamicOptionsPage(CollectionViewViewModel viewModel)
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

		_viewModel.IsEmptyViewStringSelected = sender == EmptyViewString;
		_viewModel.IsEmptyViewGridSelected = sender == EmptyViewGrid;

		if (sender == EmptyViewNone)
		{
			_viewModel.EmptyView = null;
		}
		else if (sender == EmptyViewString)
		{
			_viewModel.EmptyView = "No Items Available(String)";
		}
		else
		{
			_viewModel.EmptyView = CreateView(new Label
			{
				Text = "No Items Available(Grid View)",
				TextColor = Colors.Blue
			});
		}
	}

	private void OnHeaderChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsHeaderStringSelected = sender == HeaderString;
		_viewModel.IsHeaderGridSelected = sender == HeaderGrid;

		if (sender == HeaderNone)
		{
			_viewModel.Header = null;
		}
		else if (sender == HeaderString)
		{
			_viewModel.Header = "Default Header String";
		}
		else
		{
			_viewModel.Header = CreateView(new Label
			{
				Text = "Default Header Grid",
				HorizontalOptions = LayoutOptions.Center,
				TextColor = Colors.Blue
			});
		}
	}

	private void OnFooterChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsFooterStringSelected = sender == FooterString;
		_viewModel.IsFooterGridSelected = sender == FooterGrid;

		if (sender == FooterNone)
		{
			_viewModel.Footer = null;
		}
		else if (sender == FooterString)
		{
			_viewModel.Footer = "Default Footer String";
		}
		else
		{
			_viewModel.Footer = CreateView(new Label
			{
				Text = "Default Footer Grid",
				HorizontalOptions = LayoutOptions.Center,
				TextColor = Colors.Red
			});
		}
	}

	private void OnEmptyViewTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsEmptyViewTemplateSelected = sender == EmptyViewTemplateGrid;
		_viewModel.EmptyViewTemplate = _viewModel.IsEmptyViewTemplateSelected
			? new DataTemplate(() => CreateView(new Label
			{
				Text = "No Template Items Available(Grid View)",
				TextColor = Colors.Blue
			}))
			: null;
	}

	private void OnHeaderTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsHeaderTemplateViewSelected = sender == HeaderTemplateGrid;
		_viewModel.HeaderTemplate = _viewModel.IsHeaderTemplateViewSelected
			? CreateTemplate("Default HeaderTemplate", Colors.Blue, "HeaderTemplateLabel")
			: null;
	}

	private void OnFooterTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsFooterTemplateViewSelected = sender == FooterTemplateGrid;
		_viewModel.FooterTemplate = _viewModel.IsFooterTemplateViewSelected
			? CreateTemplate("Default FooterTemplate", Colors.Green, "FooterTemplateLabel")
			: null;
	}

	private void OnGroupHeaderTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsGroupHeaderTemplateViewSelected = sender == GroupHeaderTemplateGrid;
		_viewModel.GroupHeaderTemplate = _viewModel.IsGroupHeaderTemplateViewSelected
			? CreateTemplate("Default GroupHeaderTemplate", Colors.Green, "GroupHeaderTemplateLabel")
			: null;
	}

	private void OnGroupFooterTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsGroupFooterTemplateViewSelected = sender == GroupFooterTemplateGrid;
		_viewModel.GroupFooterTemplate = _viewModel.IsGroupFooterTemplateViewSelected
			? CreateTemplate("Default GroupFooterTemplate", Colors.Red, "GroupFooterTemplateLabel")
			: null;
	}

	private void OnItemTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsItemTemplateSelected = sender == ItemTemplateBasic;

		if (sender == ItemTemplateNone)
		{
			_viewModel.ItemTemplate = null;
		}
		else
		{
			_viewModel.ItemTemplate = new DataTemplate(() =>
			{
				var label = new Label
				{
					AutomationId = "DynamicItemTemplateLabel"
				};
				label.SetBinding(Label.TextProperty, new Binding("Caption"));
				return label;
			});
		}
	}

	private void OnIsGroupedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsGrouped = sender == IsGroupedTrue;
	}

	private void OnItemsSourceChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		// The shared source setter chooses a template, but this page selects it independently.
		var itemTemplate = _viewModel.ItemTemplate;
		if (sender == ItemsSourceObservableCollection5)
			_viewModel.ItemsSourceType = ItemsSourceType.ObservableCollection5T;
		else if (sender == ItemsSourceGroupedList)
			_viewModel.ItemsSourceType = ItemsSourceType.GroupedListT;
		else
			_viewModel.ItemsSourceType = ItemsSourceType.None;

		if (_viewModel.ItemTemplate != itemTemplate)
			_viewModel.ItemTemplate = itemTemplate;
	}

	private static DataTemplate CreateTemplate(string text, Color textColor, string automationId)
	{
		return new DataTemplate(() => CreateView(new Label
		{
			Text = text,
			TextColor = textColor,
			AutomationId = automationId,
			FontSize = 18,
			FontAttributes = FontAttributes.Bold,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center
		}));
	}

	private static Grid CreateView(Label label)
	{
		var grid = new Grid
		{
			BackgroundColor = Colors.LightGray,
			Padding = new Thickness(10)
		};
		grid.Children.Add(label);
		return grid;
	}
}

namespace Maui.Controls.Sample;

public partial class CollectionViewDynamicOptionsPage : ContentPage
{
	private CollectionViewViewModel _viewModel;

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

		if (EmptyViewNone.IsChecked)
		{
			_viewModel.EmptyView = null;
		}
		else if (EmptyViewString.IsChecked)
		{
			_viewModel.EmptyView = "No Items Available(String)";
		}
		else if (EmptyViewGrid.IsChecked)
		{
			Grid grid = new Grid
			{
				BackgroundColor = Colors.LightGray,
				Padding = new Thickness(10),
			};
			grid.Children.Add(new Label
			{
				Text = "No Items Available(Grid View)",

				TextColor = Colors.Blue
			});
			_viewModel.EmptyView = grid;

		}
	}

	private void OnHeaderChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsHeaderStringSelected = sender == HeaderString;
		_viewModel.IsHeaderGridSelected = sender == HeaderGrid;

		if (HeaderNone.IsChecked)
		{
			_viewModel.Header = null;
		}
		else if (HeaderString.IsChecked)
		{
			_viewModel.Header = "Default Header String";
		}
		else if (HeaderGrid.IsChecked)
		{
			Grid grid = new Grid
			{
				BackgroundColor = Colors.LightGray,
				Padding = new Thickness(10)
			};
			grid.Children.Add(new Label
			{
				Text = "Default Header Grid",
				HorizontalOptions = LayoutOptions.Center,
				TextColor = Colors.Blue
			});
			_viewModel.Header = grid;
		}
	}

	private void OnFooterChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsFooterStringSelected = sender == FooterString;
		_viewModel.IsFooterGridSelected = sender == FooterGrid;

		if (FooterNone.IsChecked)
		{
			_viewModel.Footer = null;
		}
		else if (FooterString.IsChecked)
		{
			_viewModel.Footer = "Default Footer String";
		}
		else if (FooterGrid.IsChecked)
		{
			Grid grid = new Grid
			{
				BackgroundColor = Colors.LightGray,
				Padding = new Thickness(10)
			};
			grid.Children.Add(new Label
			{
				Text = "Default Footer Grid",
				HorizontalOptions = LayoutOptions.Center,
				TextColor = Colors.Red
			});
			_viewModel.Footer = grid;
		}
	}
	private void OnEmptyViewTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsEmptyViewTemplateSelected = sender == EmptyViewTemplateGrid;

		if (EmptyViewTemplateNone.IsChecked)
		{
			_viewModel.EmptyViewTemplate = null;
		}
		else if (EmptyViewTemplateGrid.IsChecked)
		{
			_viewModel.EmptyViewTemplate = new DataTemplate(() =>
			{
				Grid grid = new Grid
				{
					BackgroundColor = Colors.LightGray,
					Padding = new Thickness(10)
				};
				grid.Children.Add(new Label
				{
					Text = "No Template Items Available(Grid View)",
					TextColor = Colors.Blue
				});
				return grid;

			});
		}
	}

	private void OnHeaderTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsHeaderTemplateViewSelected = sender == HeaderTemplateGrid;

		if (HeaderTemplateNone.IsChecked)
		{
			_viewModel.HeaderTemplate = null;

		}
		else if (HeaderTemplateGrid.IsChecked)
		{
			_viewModel.HeaderTemplate = new DataTemplate(() =>
			{
				Grid grid = new Grid
				{
					BackgroundColor = Colors.LightGray,
					Padding = new Thickness(10)
				};
				grid.Children.Add(new Label
				{
					Text = "Default HeaderTemplate",
					FontSize = 18,
					FontAttributes = FontAttributes.Bold,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					TextColor = Colors.Blue,
					AutomationId = "HeaderTemplateLabel"
				});
				return grid;

			});
		}
	}

	private void OnFooterTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsFooterTemplateViewSelected = sender == FooterTemplateGrid;

		if (FooterTemplateNone.IsChecked)
		{
			_viewModel.FooterTemplate = null;
		}
		else if (FooterTemplateGrid.IsChecked)
		{
			_viewModel.FooterTemplate = new DataTemplate(() =>
			{
				Grid grid = new Grid
				{
					BackgroundColor = Colors.LightGray,
					Padding = new Thickness(10)
				};
				grid.Children.Add(new Label
				{
					Text = "Default FooterTemplate",
					FontSize = 18,
					FontAttributes = FontAttributes.Bold,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					TextColor = Colors.Green,
					AutomationId = "FooterTemplateLabel"
				});
				return grid;
			});
		}
	}

	private void OnGroupHeaderTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsGroupHeaderTemplateViewSelected = sender == GroupHeaderTemplateGrid;

		if (GroupHeaderTemplateNone.IsChecked)
		{
			_viewModel.GroupHeaderTemplate = null;


		}
		else if (GroupHeaderTemplateGrid.IsChecked)
		{
			_viewModel.GroupHeaderTemplate = new DataTemplate(() =>
			{
				Grid grid = new Grid
				{
					BackgroundColor = Colors.LightGray,
					Padding = new Thickness(10)
				};
				grid.Children.Add(new Label
				{
					Text = "Default GroupHeaderTemplate",
					FontSize = 18,
					FontAttributes = FontAttributes.Bold,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					TextColor = Colors.Green,
					AutomationId = "GroupHeaderTemplateLabel"
				});
				return grid;
			});
		}
	}
	private void OnGroupFooterTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsGroupFooterTemplateViewSelected = sender == GroupFooterTemplateGrid;

		if (GroupFooterTemplateNone.IsChecked)
		{
			_viewModel.GroupFooterTemplate = null;

		}
		else if (GroupFooterTemplateGrid.IsChecked)
		{
			_viewModel.GroupFooterTemplate = new DataTemplate(() =>
			{
				Grid grid = new Grid
				{
					BackgroundColor = Colors.LightGray,
					Padding = new Thickness(10)
				};
				grid.Children.Add(new Label
				{
					Text = "Default GroupFooterTemplate",
					FontSize = 18,
					FontAttributes = FontAttributes.Bold,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					TextColor = Colors.Red,
					AutomationId = "GroupFooterTemplateLabel"
				});
				return grid;
			});
		}
	}

	private void OnItemTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		_viewModel.IsItemTemplateSelected = sender == ItemTemplateBasic;

		if (ItemTemplateNone.IsChecked)
		{
			_viewModel.ItemTemplate = null;
		}
		else if (ItemTemplateBasic.IsChecked)
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

		if (IsGroupedFalse.IsChecked)
		{
			_viewModel.IsGrouped = false;
		}
		else if (IsGroupedTrue.IsChecked)
		{
			_viewModel.IsGrouped = true;
		}
	}

	private void OnItemsSourceChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!(sender is RadioButton radioButton) || !e.Value)
			return;

		// The shared source setter chooses a template, but this page selects it independently.
		var itemTemplate = _viewModel.ItemTemplate;
		if (radioButton == ItemsSourceObservableCollection5)
			_viewModel.ItemsSourceType = ItemsSourceType.ObservableCollection5T;
		else if (radioButton == ItemsSourceGroupedList)
			_viewModel.ItemsSourceType = ItemsSourceType.GroupedListT;
		else if (radioButton == ItemsSourceNone)
			_viewModel.ItemsSourceType = ItemsSourceType.None;

		if (_viewModel.ItemTemplate != itemTemplate)
			_viewModel.ItemTemplate = itemTemplate;
	}
}

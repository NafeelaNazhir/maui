namespace Maui.Controls.Sample;

public partial class EmptyViewOptionsPage : ContentPage
{
	private readonly CollectionViewViewModel _viewModel;

	public EmptyViewOptionsPage(CollectionViewViewModel viewModel)
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
		{
			_viewModel.EmptyView = null;
		}
		else if (sender == EmptyViewString)
		{
			_viewModel.EmptyView = "No Items Available(String)";
		}
		else if (sender == EmptyViewGrid)
		{
			_viewModel.EmptyView = CreateView(new Label
			{
				Text = "No Items Available(Grid View)",
				VerticalOptions = LayoutOptions.Center,
				TextColor = Colors.Blue
			});
		}
		else if (sender == EmptyViewCustomSize)
		{
			_viewModel.EmptyView = CreateSizedView("Custom Empty View (Sized)", "EmptyViewLabel");
		}
	}

	private void OnHeaderChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == HeaderNone)
		{
			_viewModel.Header = null;
		}
		else if (sender == HeaderString)
		{
			_viewModel.Header = "CollectionView Header(String)";
		}
		else if (sender == HeaderGrid)
		{
			_viewModel.Header = CreateHeaderFooterView("CollectionView Header(Grid View)", Colors.Blue);
		}
	}

	private void OnFooterChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == FooterNone)
		{
			_viewModel.Footer = null;
		}
		else if (sender == FooterString)
		{
			_viewModel.Footer = "CollectionView Footer(String)";
		}
		else if (sender == FooterGrid)
		{
			_viewModel.Footer = CreateHeaderFooterView("CollectionView Footer(Grid View)", Colors.Red);
		}
	}

	private void OnEmptyViewTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == EmptyViewTemplateNone)
		{
			_viewModel.EmptyViewTemplate = null;
		}
		else if (sender == EmptyViewTemplateGrid)
		{
			_viewModel.EmptyViewTemplate = new DataTemplate(() => CreateView(new Label
			{
				Text = "No Template Items Available(Grid View)",
				VerticalOptions = LayoutOptions.Center,
				TextColor = Colors.Blue
			}));
		}
		else if (sender == EmptyViewTemplateCustomSize)
		{
			_viewModel.EmptyViewTemplate = new DataTemplate(() =>
				CreateSizedView("Custom EmptyViewTemplate (Sized)", "EmptyViewTemplateLabel", Colors.Green));
		}
	}

	private void OnItemTemplateChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == ItemTemplateNone)
		{
			_viewModel.ItemTemplate = null;
		}
		else if (sender == ItemTemplateBasic)
		{
			_viewModel.ItemTemplate = new DataTemplate(CreateItemLabel);
		}
		else if (sender == ItemTemplateGrid)
		{
			_viewModel.ItemTemplate = new DataTemplate(() =>
			{
				var grid = new Grid
				{
					ColumnDefinitions =
					{
						new ColumnDefinition { Width = GridLength.Star },
						new ColumnDefinition { Width = GridLength.Star }
					},
					Padding = new Thickness(10),
					BackgroundColor = Colors.LightBlue
				};

				var label = CreateItemLabel();
				label.VerticalOptions = LayoutOptions.Center;
				label.HorizontalOptions = LayoutOptions.Start;
				label.FontAttributes = FontAttributes.Bold;
				label.TextColor = Colors.Green;
				grid.Children.Add(label);
				return grid;
			});
		}
	}

	private void OnIsGroupedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == IsGroupedFalse)
		{
			_viewModel.IsGrouped = false;
		}
		else if (sender == IsGroupedTrue)
		{
			_viewModel.IsGrouped = true;
		}
	}

	private void OnItemsSourceChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == ItemsSourceObservableCollection5)
		{
			_viewModel.ItemsSourceType = ItemsSourceType.ObservableCollection5T;
		}
		else if (sender == ItemsSourceEmptyObservableCollection)
		{
			_viewModel.ItemsSourceType = ItemsSourceType.EmptyObservableCollectionT;
		}
		else if (sender == ItemsSourceGroupedList)
		{
			_viewModel.ItemsSourceType = ItemsSourceType.GroupedListT;
		}
		else if (sender == ItemsSourceEmptyGroupedList)
		{
			_viewModel.ItemsSourceType = ItemsSourceType.EmptyGroupedListT;
		}
		else if (sender == ItemsSourceNone)
		{
			_viewModel.ItemsSourceType = ItemsSourceType.None;
		}
	}

	private void OnItemsLayoutChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == ItemsLayoutVerticalList)
		{
			_viewModel.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
		}
		else if (sender == ItemsLayoutHorizontalList)
		{
			_viewModel.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal);
		}
		else if (sender == ItemsLayoutVerticalGrid)
		{
			_viewModel.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical);
		}
		else if (sender == ItemsLayoutHorizontalGrid)
		{
			_viewModel.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Horizontal);
		}
	}

	private void OnFlowDirectionChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value)
			return;

		if (sender == FlowDirectionLeftToRight)
		{
			_viewModel.FlowDirection = FlowDirection.LeftToRight;
		}
		else if (sender == FlowDirectionRightToLeft)
		{
			_viewModel.FlowDirection = FlowDirection.RightToLeft;
		}
	}

	private static Grid CreateHeaderFooterView(string text, Color textColor)
	{
		return CreateView(new Label
		{
			Text = text,
			HorizontalOptions = LayoutOptions.Center,
			TextColor = textColor
		});
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

	private static Frame CreateSizedView(string text, string automationId, Color borderColor = null)
	{
		var view = new Frame
		{
			BackgroundColor = Colors.LightBlue,
			HeightRequest = 200,
			WidthRequest = 300,
			AutomationId = automationId,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			Content = new Label
			{
				Text = text,
				VerticalOptions = LayoutOptions.Center
			}
		};

		if (borderColor is not null)
		{
			view.BorderColor = borderColor;
		}

		return view;
	}

	private static Label CreateItemLabel()
	{
		var label = new Label();
		label.SetBinding(Label.TextProperty, new Binding("Caption"));
		return label;
	}
}
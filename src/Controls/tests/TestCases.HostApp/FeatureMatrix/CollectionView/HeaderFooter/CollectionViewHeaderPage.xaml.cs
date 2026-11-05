namespace Maui.Controls.Sample;

public partial class CollectionViewHeaderPage : ContentPage
{
	private readonly CollectionViewViewModel _viewModel;

	public CollectionViewHeaderPage()
	{
		InitializeComponent();
		_viewModel = new CollectionViewViewModel();
		_viewModel.ItemsSourceType = ItemsSourceType.ObservableCollection5T;
		BindingContext = _viewModel;
	}

	private async void NavigateToOptionsPage_Clicked(object sender, EventArgs e)
	{
		_viewModel.ResetHeaderFooterProperties();
		await Navigation.PushAsync(new HeaderFooterOptionsPage(_viewModel));
	}
}
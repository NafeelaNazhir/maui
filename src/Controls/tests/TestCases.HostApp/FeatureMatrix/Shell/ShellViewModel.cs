
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Maui.Controls.Sample;

public class ShellViewModel : INotifyPropertyChanged
{
	// ── Visual ───────────────────────────────────────────────────────────

	private Color _backgroundColor;
	private Color _cancelButtonColor;
	private Color _textColor;
	private Color _placeholderColor;

	// ── Text / Font ──────────────────────────────────────────────────────

	private double _characterSpacing = 0d;
	private FontAttributes _fontAttributes = FontAttributes.None;
	private bool _fontAutoScalingEnabled = true;
	private string _fontFamily;
	private double _fontSize = 16d;
	private TextAlignment _horizontalTextAlignment = TextAlignment.Start;
	private TextAlignment _verticalTextAlignment = TextAlignment.Center;
	private TextTransform _textTransform = TextTransform.Default;
	private Keyboard _keyboard = Keyboard.Default;
	private string _placeholder = "Search fruits or birds…";

	// ── State ────────────────────────────────────────────────────────────

	private bool _isSearchEnabled = true;
	private bool _showsResults = true;
	private SearchBoxVisibility _searchBoxVisibility = SearchBoxVisibility.Expanded;
	private string _query = string.Empty;
	private object _selectedItem;

	// ── Items ────────────────────────────────────────────────────────────

	private DataTemplate _itemTemplate;
	private string _itemsSourceMode = "Query";
	// ── Icons ────────────────────────────────────────────────────────────

	private ImageSource _clearIcon;
	private ImageSource _queryIcon;

	// ── ClearPlaceholder ─────────────────────────────────────────────────

	private bool _clearPlaceholderEnabled = false;
	private ImageSource _clearPlaceholderIcon;
	private string _clearPlaceholderCommandParameter = string.Empty;

	// ── Command / CommandParameter ───────────────────────────────────────

	private string _commandParameter = string.Empty;

	// ────────────────────────────────────────────────────────────────────
	// Constructor
	// ────────────────────────────────────────────────────────────────────

	public ShellViewModel()
	{
		_itemTemplate = BuildSimpleTemplate();

		SearchCommand = new Command<object>(p =>
		{
			CommandFired = $"QueryConfirmed:{_query}";
		},
		_ => IsSearchEnabled);

		ClearPlaceholderCommand = new Command<object>(p =>
		{
			ClearPlaceholderCommandFired = $"ClearPlaceholder:{p}";
		});
	}

	// ────────────────────────────────────────────────────────────────────
	// Reset
	// ────────────────────────────────────────────────────────────────────

	/// <summary>
	/// Resets all properties to their initial defaults without creating a new ViewModel instance.
	/// This preserves all PropertyChanged subscriptions (SearchHandler, code-behind, bindings).
	/// </summary>
	public void Reset()
	{
		// Visual
		BackgroundColor = null;
		CancelButtonColor = null;
		TextColor = null;
		PlaceholderColor = null;

		// Text / Font
		CharacterSpacing = 0d;
		FontAttributes = FontAttributes.None;
		FontAutoScalingEnabled = true;
		FontFamily = null;
		FontSize = 16d;
		HorizontalTextAlignment = TextAlignment.Start;
		VerticalTextAlignment = TextAlignment.Center;
		TextTransform = TextTransform.Default;
		Keyboard = Keyboard.Default;
		Placeholder = "Search fruits or birds…";

		// State
		IsSearchEnabled = true;
		ShowsResults = true;
		SearchBoxVisibility = SearchBoxVisibility.Expanded;
		Query = string.Empty;
		SelectedItem = null;

		// Items
		ItemTemplate = BuildSimpleTemplate();
		ItemsSourceMode = "Query";

		// Icons
		ClearIcon = null;
		QueryIcon = null;

		// ClearPlaceholder
		ClearPlaceholderEnabled = false;
		ClearPlaceholderIcon = null;
		ClearPlaceholderCommandParameter = string.Empty;

		// Command parameter
		CommandParameter = string.Empty;

		// Status / event logs
		QueryChangedLog = string.Empty;
		FocusStatus = string.Empty;
		IsFocused = false;
		CommandFired = string.Empty;
		ClearPlaceholderCommandFired = string.Empty;
	}

	// ────────────────────────────────────────────────────────────────────
	// Properties
	// ────────────────────────────────────────────────────────────────────
	public Color BackgroundColor
	{
		get => _backgroundColor;
		set { _backgroundColor = value; OnPropertyChanged(); }
	}
	public Color CancelButtonColor
	{
		get => _cancelButtonColor;
		set { _cancelButtonColor = value; OnPropertyChanged(); }
	}
	public Color TextColor
	{
		get => _textColor;
		set { _textColor = value; OnPropertyChanged(); }
	}
	public Color PlaceholderColor
	{
		get => _placeholderColor;
		set { _placeholderColor = value; OnPropertyChanged(); }
	}
	public double CharacterSpacing
	{
		get => _characterSpacing;
		set { _characterSpacing = value; OnPropertyChanged(); }
	}
	public FontAttributes FontAttributes
	{
		get => _fontAttributes;
		set { _fontAttributes = value; OnPropertyChanged(); }
	}
	public bool FontAutoScalingEnabled
	{
		get => _fontAutoScalingEnabled;
		set { _fontAutoScalingEnabled = value; OnPropertyChanged(); }
	}
	public string FontFamily
	{
		get => _fontFamily;
		set { _fontFamily = value; OnPropertyChanged(); }
	}
	public double FontSize
	{
		get => _fontSize;
		set { _fontSize = value; OnPropertyChanged(); }
	}
	public TextAlignment HorizontalTextAlignment
	{
		get => _horizontalTextAlignment;
		set { _horizontalTextAlignment = value; OnPropertyChanged(); }
	}
	public TextAlignment VerticalTextAlignment
	{
		get => _verticalTextAlignment;
		set { _verticalTextAlignment = value; OnPropertyChanged(); }
	}
	public TextTransform TextTransform
	{
		get => _textTransform;
		set { _textTransform = value; OnPropertyChanged(); }
	}

	public Keyboard Keyboard
	{
		get => _keyboard;
		set { _keyboard = value; OnPropertyChanged(); }
	}
	public string Placeholder
	{
		get => _placeholder;
		set { _placeholder = value; OnPropertyChanged(); }
	}

	public bool IsSearchEnabled
	{
		get => _isSearchEnabled;
		set
		{
			_isSearchEnabled = value;
			OnPropertyChanged();
			(SearchCommand as Command)?.ChangeCanExecute();
		}
	}
	public bool ShowsResults
	{
		get => _showsResults;
		set { _showsResults = value; OnPropertyChanged(); }
	}
	public SearchBoxVisibility SearchBoxVisibility
	{
		get => _searchBoxVisibility;
		set { _searchBoxVisibility = value; OnPropertyChanged(); }
	}
	public string Query
	{
		get => _query;
		set { _query = value; OnPropertyChanged(); }
	}
	public object SelectedItem
	{
		get => _selectedItem;
		set { _selectedItem = value; OnPropertyChanged(); }
	}

	public DataTemplate ItemTemplate
	{
		get => _itemTemplate;
		set { _itemTemplate = value; OnPropertyChanged(); }
	}

	public string ItemsSourceMode
	{
		get => _itemsSourceMode;
		set { _itemsSourceMode = value; OnPropertyChanged(); }
	}

	public static DataTemplate BuildSimpleTemplate()
	{
		return new DataTemplate(() =>
		{
			var label = new Label
			{
				AutomationId = "SearchResultName",
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(10, 5),
				FontSize = 20,
			};
			label.SetBinding(Label.TextProperty, static (string s) => s);
			return label;
		});
	}

	public static DataTemplate BuildCustomTemplate()
	{
		return new DataTemplate(() =>
		{
			var image = new Image
			{
				Source = ImageSource.FromFile("dotnet_bot.png"),
				WidthRequest = 24,
				HeightRequest = 24,
				VerticalOptions = LayoutOptions.Center,
			};

			var label = new Label
			{
				AutomationId = "SearchResultName",
				VerticalOptions = LayoutOptions.Center,
				TextColor = Colors.MediumVioletRed,
				FontAttributes = FontAttributes.Bold,
			};
			label.SetBinding(Label.TextProperty, static (string s) => s);

			return new HorizontalStackLayout
			{
				Spacing = 10,
				Padding = new Thickness(10, 5),
				Children =
				{
					image,
					label
				}
			};
		});
	}

	public ImageSource ClearIcon
	{
		get => _clearIcon;
		set { _clearIcon = value; OnPropertyChanged(); }
	}

	public ImageSource QueryIcon
	{
		get => _queryIcon;
		set { _queryIcon = value; OnPropertyChanged(); }
	}

	public bool ClearPlaceholderEnabled
	{
		get => _clearPlaceholderEnabled;
		set { _clearPlaceholderEnabled = value; OnPropertyChanged(); }
	}
	public ImageSource ClearPlaceholderIcon
	{
		get => _clearPlaceholderIcon;
		set { _clearPlaceholderIcon = value; OnPropertyChanged(); }
	}
	public string ClearPlaceholderCommandParameter
	{
		get => _clearPlaceholderCommandParameter;
		set { _clearPlaceholderCommandParameter = value; OnPropertyChanged(); }
	}
	public string CommandParameter
	{
		get => _commandParameter;
		set { _commandParameter = value; OnPropertyChanged(); }
	}
	public ICommand SearchCommand { get; }
	public ICommand ClearPlaceholderCommand { get; }

	// ── Event-fired status strings (updated by code-behind, observed by tests) ──

	private string _commandFired = string.Empty;
	/// <summary>Set when SearchCommand fires; shown in CommandFired label.</summary>
	public string CommandFired
	{
		get => _commandFired;
		set { _commandFired = value; OnPropertyChanged(); }
	}
	private string _clearPlaceholderCommandFired = string.Empty;
	/// <summary>Set when ClearPlaceholderCommand fires; shown in ClearPlaceholderCommandFired label.</summary>
	public string ClearPlaceholderCommandFired
	{
		get => _clearPlaceholderCommandFired;
		set { _clearPlaceholderCommandFired = value; OnPropertyChanged(); }
	}
	private string _queryChangedLog = string.Empty;
	public string QueryChangedLog
	{
		get => _queryChangedLog;
		set { _queryChangedLog = value; OnPropertyChanged(); }
	}
	private string _focusStatus = string.Empty;
	public string FocusStatus
	{
		get => _focusStatus;
		set { _focusStatus = value; OnPropertyChanged(); }
	}
	private bool _isFocused;
	public bool IsFocused
	{
		get => _isFocused;
		set { _isFocused = value; OnPropertyChanged(); }
	}

	// ────────────────────────────────────────────────────────────────────
	// INotifyPropertyChanged
	// ────────────────────────────────────────────────────────────────────

	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

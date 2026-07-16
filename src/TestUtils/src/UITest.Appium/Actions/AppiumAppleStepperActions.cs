using OpenQA.Selenium.Appium;
using UITest.Core;

namespace UITest.Appium;

class AppiumAppleStepperActions : ICommandExecutionGroup
{
	const string IncreaseCommand = "increaseStepper";
	const string DecreaseCommand = "decreaseStepper";

	readonly List<string> _commands = new()
	{
		IncreaseCommand,
		DecreaseCommand
	};

	readonly AppiumApp _appiumApp;

	public AppiumAppleStepperActions(AppiumApp appiumApp)
	{
		_appiumApp = appiumApp;
	}

	public bool IsCommandSupported(string commandName)
	{
		return _commands.Contains(commandName, StringComparer.OrdinalIgnoreCase);
	}

	public CommandResponse Execute(string commandName, IDictionary<string, object> parameters)
	{
		return commandName switch
		{
			IncreaseCommand => Increase(parameters),
			DecreaseCommand => Decrease(parameters),
			_ => CommandResponse.FailedEmptyResponse,
		};
	}

	CommandResponse Increase(IDictionary<string, object> parameters)
	{
		string? elementId = parameters["elementId"].ToString();

		if (elementId is null)
			return CommandResponse.FailedEmptyResponse;

		var element = _appiumApp.FindElement(elementId);
		var stepper = GetAppiumElement(element);

		if (stepper is null)
			return CommandResponse.FailedEmptyResponse;

		var buttons = GetOrderedStepperButtons(stepper);

		if (buttons is not null && buttons.Count > 1)
		{
			// Increase (+) button is always the rightmost one
			var increaseButton = buttons[buttons.Count - 1];
			increaseButton.Tap();
		}

		return CommandResponse.SuccessEmptyResponse;
	}

	CommandResponse Decrease(IDictionary<string, object> parameters)
	{
		string? elementId = parameters["elementId"].ToString();

		if (elementId is null)
			return CommandResponse.FailedEmptyResponse;

		var element = _appiumApp.FindElement(elementId);
		var stepper = GetAppiumElement(element);

		if (stepper is null)
			return CommandResponse.FailedEmptyResponse;

		var buttons = GetOrderedStepperButtons(stepper);

		if (buttons is not null && buttons.Count > 1)
		{
			// Decrease (-) button is always the leftmost one
			var decreaseButton = buttons[0];
			decreaseButton.Tap();
		}

		return CommandResponse.SuccessEmptyResponse;
	}

	// Sorts the stepper's buttons left-to-right by screen position,
	// since Appium doesn't always return them in a reliable order.
	List<IUIElement>? GetOrderedStepperButtons(AppiumElement stepper)
	{
		var buttons = AppiumQuery.ByClass("XCUIElementTypeButton").FindElements(stepper, _appiumApp);

		if (buttons is null)
			return null;

		return buttons
			.OrderBy(b => b.GetRect().X)
			.ToList();
	}

	static AppiumElement? GetAppiumElement(object element) =>
		element switch
		{
			AppiumElement appiumElement => appiumElement,
			AppiumDriverElement driverElement => driverElement.AppiumElement,
			_ => null
		};
}
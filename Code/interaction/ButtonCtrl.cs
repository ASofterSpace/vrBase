/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * The button controller controls all the buttons - who would have thunk!
 */
public class ButtonCtrl {

	// all buttons' names need to start with "btn-", such that the TriggerCtrl can target them
	public const string BTN_FLIPPERQND_START = "btn-fqnd-start";
	public const string BTN_FLIPPERQND_TRIGGER = "btn-fqnd-trigger";
	public const string BTN_FIREFIGHTING_BA_CHECK = "btn-ff-ba-check";
	public const string BTN_FIREFIGHTING_BA_CROSS = "btn-ff-ba-cross";
	public const string BTN_NOSTALGICCONSOLE_BIG_RED = "btn-nc-red";
	public const string BTN_NOSTALGICCONSOLE_BIG_WHITE = "btn-nc-white";
	public const string BTN_NOSTALGICCONSOLE_UP = "btn-nc-up";
	public const string BTN_NOSTALGICCONSOLE_DOWN = "btn-nc-down";
	public const string BTN_TICTACTOE_FIELD = "btn-ttt-";
	public const string BTN_TICTACTOE_RESTART = "btn-tttrestart";

	private static Dictionary<string, Button> buttons;


	public static void init() {

		buttons = new Dictionary<string, Button>();
	}

	public static void add(Button button) {

		buttons.Add(button.getName(), button);
	}

	public static Button get(string buttonName) {

		return buttons[buttonName];
	}

}

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
	public const string BTN_NOSTALGICCONSOLE_BIG_RED = "btn-1";
	public const string BTN_NOSTALGICCONSOLE_BIG_WHITE = "btn-2";
	public const string BTN_TICTACTOE_FIELD = "btn-ttt-";

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

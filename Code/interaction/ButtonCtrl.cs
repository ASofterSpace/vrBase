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

	public const string BUTTON_IDENTIFIER = "btn-";

	private static MainCtrl mainCtrl;

	private static int nextBtn = 0;

	private static Dictionary<string, Button> buttons;


	public static void init(MainCtrl mainCtrl) {

		ButtonCtrl.mainCtrl = mainCtrl;

		ButtonCtrl.buttons = new Dictionary<string, Button>();
	}

	public static void add(Button button) {

		nextBtn++;

		button.setName(BUTTON_IDENTIFIER + nextBtn);

		buttons.Add(button.getName(), button);

		if (button is UpdateableCtrl) {
			mainCtrl.addUpdateableCtrl((UpdateableCtrl) button);
		}
		if (button is ResetteableCtrl) {
			mainCtrl.addResetteableCtrl((ResetteableCtrl) button);
		}
	}

	public static Button get(string buttonName) {

		return buttons[buttonName];
	}

}

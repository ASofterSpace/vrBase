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

	private static int nextBtn = 0;

	private static Dictionary<string, Button> buttons;


	public static void init() {

		buttons = new Dictionary<string, Button>();
	}

	public static void add(Button button) {

		nextBtn++;

		button.setName("btn-" + nextBtn);

		buttons.Add(button.getName(), button);
	}

	public static Button get(string buttonName) {

		return buttons[buttonName];
	}

}

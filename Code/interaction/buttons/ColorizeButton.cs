/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A button that colorizes the environment when it is being pressed
 */
public class ColorizeButton : Button {

	public ColorizeButton(GameObject obj, string buttonName) : base(obj, buttonName) {

	}

	public override void trigger() {
		MaterialCtrl.setColor(
			MaterialCtrl.PLASTIC_WHITE,
			new Color(1.0f, 0.0f, 0.0f, 1.0f)
		);
	}

}

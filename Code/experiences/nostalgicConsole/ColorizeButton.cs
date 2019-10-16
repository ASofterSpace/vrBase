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

	private Color targetColor;


	public ColorizeButton(GameObject obj, string buttonName, Color targetColor) : base(obj, buttonName) {

		this.targetColor = targetColor;
	}

	public override void trigger() {
		MaterialCtrl.setColor(MaterialCtrl.BUILDING_BEAM_WHITE, targetColor);
	}

}

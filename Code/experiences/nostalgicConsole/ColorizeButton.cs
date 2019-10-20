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

	private Color beamColor;

	private Color wallColor;


	public ColorizeButton(GameObject obj, Color beamColor, Color wallColor) : base(obj) {

		this.beamColor = beamColor;

		this.wallColor = wallColor;
	}

	public override void trigger() {

		MaterialCtrl.setColor(MaterialCtrl.BUILDING_BEAM_WHITE, beamColor);

		MaterialCtrl.setColor(MaterialCtrl.BUILDING_WALL, wallColor);
	}

}

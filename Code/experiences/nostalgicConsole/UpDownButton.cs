/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A button that sets the camera up or down
 */
public class UpDownButton : Button {

	private MainCtrl mainCtrl;

	private GameObject gameObject2;

	private float upDownAmount;


	public UpDownButton(GameObject obj1, GameObject obj2, string buttonName, MainCtrl mainCtrl, float upDownAmount) :
		base(obj1, buttonName) {

		this.mainCtrl = mainCtrl;

		this.upDownAmount = upDownAmount;

		obj2.name = buttonName;

		this.gameObject2 = obj2;
	}

	public override void hover() {
		MaterialCtrl.setMaterial(gameObject, MaterialCtrl.INTERACTION_BUTTON_HOVER);
		MaterialCtrl.setMaterial(gameObject2, MaterialCtrl.INTERACTION_BUTTON_HOVER);
	}

	public override void blur() {
		gameObject.GetComponent<Renderer>().material = defaultMaterial;
		gameObject2.GetComponent<Renderer>().material = defaultMaterial;
	}

	public override void trigger() {
		VrSpecificCtrl vrSpecificCtrl = mainCtrl.getVrSpecificCtrl();
		if (vrSpecificCtrl != null) {
			vrSpecificCtrl.adjustCameraHeightBy(upDownAmount);
		}
	}

}

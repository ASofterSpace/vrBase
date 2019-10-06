/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class VrSpecificCtrl
{
	private MainCtrl mainCtrl;

	private String vrKindInUse;


	public VrSpecificCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;

		this.vrKindInUse = detectVrKind();

		adjustCameraHeight();
	}

	private String detectVrKind() {

		// TODO :: actually figure out if we run on a vive (or something even different) and act accordingly

		return "OCULUS_QUEST";
	}

	private void adjustCameraHeight() {

		GameObject cam = mainCtrl.getMainCamera();
		Vector3 curPos = cam.transform.localPosition;

		if (vrKindInUse.StartsWith("VIVE_")) {
			// set camera to height 0, as the height is automatically taken into account by the vive
			cam.transform.localPosition = new Vector3(curPos.x, curPos.y, 0.0f);
		}

		if (vrKindInUse.StartsWith("OCULUS_")) {
			// set camera to a reasonable height (1.75m maybe for now?)
			// TODO :: we could be using Oculus Utilities, then we could get the player height from
			//   OVRManager -> OVRProfile - but if different people are playing, then it will not be
			//   correct either...
			cam.transform.localPosition = new Vector3(curPos.x, curPos.y, 1.75f);
		}
	}

/*
	private void gatherInput() {
		// see: https://docs.unity3d.com/Manual/OculusControllers.html

		string[] joystickNames = Input.GetJoystickNames();

		for (int i = 0; i < joystickNames.Length; i++) {
			// do something?
		}
	}
*/

}

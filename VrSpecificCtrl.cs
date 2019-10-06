/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class VrSpecificCtrl : MonoBehaviour
{
	private MainCtrl mainCtrl;

	private GameObject mainCamera;
	private GameObject mainCameraHolder;

	private String vrKindInUse;


	void Start() {
	}

	void Update() {
	}

	public void init(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;

		this.vrKindInUse = detectVrKind();

		adjustCameraHeight();
	}

	private String detectVrKind() {

		// TODO :: actually figure out if we run on a vive (or something even different) and act accordingly

		return "OCULUS_QUEST";
	}

	private void adjustCameraHeight() {

		mainCamera = mainCtrl.getMainCamera();
		mainCameraHolder = mainCtrl.getMainCameraHolder();
		Vector3 curPos = mainCameraHolder.transform.localPosition;

		if (vrKindInUse.StartsWith("VIVE_")) {
			// set camera to height 0, as the height is automatically taken into account by the vive
			mainCameraHolder.transform.localPosition = new Vector3(curPos.x, 0.0f, curPos.z);
		}

		if (vrKindInUse.StartsWith("OCULUS_")) {
			// set camera to a reasonable height (1.75m maybe for now?)
			// TODO :: we could be using Oculus Utilities, then we could get the player height from
			//   OVRManager -> OVRProfile - but if different people are playing, then it will not be
			//   correct either...
			// TODO :: actually actually, a reasonable height like 1.75m is way too much...
			// something much less (reasonable) seems to be needed - why? will this always work?
			mainCameraHolder.transform.localPosition = new Vector3(curPos.x, 0.7f, curPos.z);
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

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine.XR;
using UnityEngine;


public class VrSpecificCtrl {

	private MainCtrl mainCtrl;

	private GameObject mainCamera;
	private GameObject mainCameraHolder;
	private GameObject world;

	private String vrKindInUse;

	private List<InputDevice> camInputDevices;
	private List<InputDevice> leftInputDevices;
	private List<InputDevice> rightInputDevices;

	private VrInput previousInput;

	private GameObject leftController;
	private GameObject rightController;


	public VrSpecificCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;
		mainCamera = mainCtrl.getMainCamera();
		mainCameraHolder = mainCtrl.getMainCameraHolder();
		world = mainCtrl.getWorld();

		this.vrKindInUse = detectVrKind();

		adjustCameraHeight();

		camInputDevices = new List<InputDevice>();
		leftInputDevices = new List<InputDevice>();
		rightInputDevices = new List<InputDevice>();

		// by providing a dummy object here we avoid checking for null ALL THE TIME later :)
		previousInput = new VrInput();

		leftController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		leftController.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		rightController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		rightController.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
	}

	/**
	 * The heavyUpdate() function is called less often, such that more resource-
	 * hungry things can be done here
	 */
	public void heavyUpdate() {

		InputDevices.GetDevicesWithRole(InputDeviceRole.Generic, camInputDevices);
		InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, leftInputDevices);
		InputDevices.GetDevicesWithRole(InputDeviceRole.RightHanded, rightInputDevices);
	}

	/**
	 * The update() function of this class is special; it gathers input for all
	 * other classes to use during their update() call
	 */
	public VrInput update() {

		VrInput result = new VrInput();

		foreach (InputDevice inputDevice in camInputDevices) {
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out result.camPosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out result.camRotation);
		}
		foreach (InputDevice inputDevice in leftInputDevices) {
			inputDevice.TryGetFeatureValue(CommonUsages.trigger, out result.leftTrigger);
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out result.leftPosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out result.leftRotation);
			leftController.transform.position = result.leftPosition;
		}
		foreach (InputDevice inputDevice in rightInputDevices) {
			inputDevice.TryGetFeatureValue(CommonUsages.trigger, out result.rightTrigger);
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out result.rightPosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out result.rightRotation);
			rightController.transform.position = result.rightPosition;
		}

		/* test code /
		result.leftTrigger = 1.0f;
		result.leftPosition = new Vector3(1, 2, 1);
		result.leftRotation = leftController.transform.rotation;
		leftController.transform.position = result.leftPosition;
		// /* */

		// TODO :: this will be wonky if we have several left or several right controllers,
		// as we only update trigger-ness once in the end instead of during the for loops...
		result.consolidate(previousInput);

		previousInput = result;

		return result;
	}

	private String detectVrKind() {

		// TODO :: actually figure out if we run on a vive (or something even different) and act accordingly

		return "OCULUS_QUEST";
	}

	private void adjustCameraHeight() {

		Vector3 curPos = world.transform.localPosition;

		if (vrKindInUse.StartsWith("VIVE_")) {
			// set camera to height 0, as the height is automatically taken into account by the vive
			world.transform.localPosition = new Vector3(curPos.x, 0.0f, curPos.z);
		}

		if (vrKindInUse.StartsWith("OCULUS_")) {
			// set camera to a reasonable height (1.75m maybe for now?)
			// TODO :: we could be using Oculus Utilities, then we could get the player height from
			//   OVRManager -> OVRProfile - but if different people are playing, then it will not be
			//   correct either...
			// TODO :: actually actually, a reasonable height like 1.75m is way too much...
			// something much less (reasonable) seems to be needed - why? will this always work?
			// mainCameraHolder.transform.localPosition = new Vector3(curPos.x, 0.7f, curPos.z);
			world.transform.localPosition = new Vector3(curPos.x, -0.7f, curPos.z);
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

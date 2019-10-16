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
	private float worldY;

	private int vrKindInUse;

	private List<InputDevice> camInputDevices;
	private List<InputDevice> leftInputDevices;
	private List<InputDevice> rightInputDevices;

	private VrInput previousInput;

	private GameObject leftController;
	private GameObject rightController;

	public const int VIVE = 0;
	public const int VIVE_COSMOS = 1;
	public const int OCULUS_RIFT = 2;
	public const int OCULUS_GO = 3;
	public const int OCULUS_QUEST = 4;


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

		// we create the controller objects but we do NOT have them visible at first, as we
		// do not want to show them at the wrong place - only show them somewhere when we
		// get an update telling us where they are :)
		leftController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		leftController.name = "Left Controller";
		leftController.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		leftController.transform.localPosition = new Vector3(0, -1000, 0);
		rightController = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		rightController.name = "Right Controller";
		rightController.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		rightController.transform.localPosition = new Vector3(0, -1000, 0);
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
			adjustRoomForPlausibility(result.camPosition);
		}
		foreach (InputDevice inputDevice in leftInputDevices) {
			inputDevice.TryGetFeatureValue(CommonUsages.trigger, out result.leftTrigger);
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out result.leftPosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out result.leftRotation);
			leftController.transform.position = result.leftPosition;
			adjustRoomForPlausibility(result.leftPosition);
		}
		foreach (InputDevice inputDevice in rightInputDevices) {
			inputDevice.TryGetFeatureValue(CommonUsages.trigger, out result.rightTrigger);
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out result.rightPosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out result.rightRotation);
			rightController.transform.position = result.rightPosition;
			adjustRoomForPlausibility(result.rightPosition);
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

	private void adjustRoomForPlausibility(Vector3 inputPosition) {

		// if we are using an Oculus Quest, on which the room height seems to be wrongly detected
		// quite often, even after room setup has just been performed...
		if (vrKindInUse == OCULUS_QUEST) {
			// ... then adjust room height on the fly by realizing that if we detect something under the
			// floor, it was apparently set wrongly... in principle a fun idea, but should be replaced
			// by a proper room setup, as this here is also error-prone: first of all, we start out with
			// a wrong floor height and only adjust it when we actively notice it being impossible;
			// secondly, a controller or vr goggle might get registered as underneath the floor when it
			// actually is not, as things might go wrong - things always go wrong! so, I dunno, at least
			// only assign this if several consecutive updates say it or something like that?
			if ((inputPosition != null) && (inputPosition.y < worldY)) {
				worldY = inputPosition.y;
				adjustCameraHeight();
			}
		}
	}
	private int detectVrKind() {

		// TODO :: actually figure out if we run on a vive (or something even different) and act accordingly

		return OCULUS_QUEST;
	}

	private void adjustCameraHeightForVrKind() {

		worldY = 0.0f;

		if (vrKindInUse == VIVE) {
			// set camera to height 0, as the height is automatically taken into account by the vive
			worldY = 0.0f;
		}

		if (vrKindInUse == OCULUS_QUEST) {
			// set camera to a reasonable height (1.75m maybe for now?)
			// TODO :: we could be using Oculus Utilities, then we could get the player height from
			//   OVRManager -> OVRProfile - but if different people are playing, then it will not be
			//   correct either...
			// TODO :: actually actually, a reasonable height like 1.75m is way too much...
			// something much less (reasonable) seems to be needed - why? will this always work?
			// mainCameraHolder.transform.localPosition = new Vector3(curPos.x, 0.7f, curPos.z);
			// worldY = -0.7f;
			// actually actually actually, use default height anyway - as we will adapt the room height
			// upon startup, and don't want to overcompensate now!
			worldY = 0.0f;
		}

		adjustCameraHeight();
	}

	private void adjustCameraHeight() {

		Vector3 curPos = world.transform.localPosition;
		world.transform.localPosition = new Vector3(curPos.x, worldY, curPos.z);
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

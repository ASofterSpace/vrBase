/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This class takes care of the normal operation of the triggers, mostly
 * for teleporting, but also for interacting with buttons, objects, etc.
 */
public class TriggerCtrl {

	private MainCtrl mainCtrl;

	private GameObject[] ray;
	private GameObject[] targetMarker;
	private GameObject faderHolder;
	private Transform worldTransform;

	private Vector3[] targetPoint;
	private Vector3[] potentialTeleportVector;
	private Vector3 teleportVector;

	private bool teleportInProgress;
	private bool[] targetingTeleportableArea;
	private Button[] targetingButton;

	private float fade;
	private float fadeStartTime;
	private bool fadingOut;

	private ControllerBehaviour[] controllers;

	public const string FLOOR_NAME = "floor";


	public TriggerCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;
		this.worldTransform = mainCtrl.getWorld().transform;

		this.teleportInProgress = false;
		this.targetingTeleportableArea = new bool[2];
		this.targetingTeleportableArea[VrInput.LEFT] = false;
		this.targetingTeleportableArea[VrInput.RIGHT] = false;
		this.targetingButton = new Button[2];
		this.targetingButton[VrInput.LEFT] = null;
		this.targetingButton[VrInput.RIGHT] = null;

		this.targetPoint = new Vector3[2];
		this.potentialTeleportVector = new Vector3[2];

		ray = new GameObject[2];
		ray[VrInput.LEFT] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		ray[VrInput.LEFT].name = "leftTeleportRay";
		ray[VrInput.LEFT].transform.parent = this.worldTransform;
		ray[VrInput.LEFT].GetComponent<Collider>().enabled = false;
		MaterialCtrl.setMaterial(ray[VrInput.LEFT], MaterialCtrl.INTERACTION_TELEPORT_RAY);
		ray[VrInput.RIGHT] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		ray[VrInput.RIGHT].name = "rightTeleportRay";
		ray[VrInput.RIGHT].transform.parent = this.worldTransform;
		ray[VrInput.RIGHT].GetComponent<Collider>().enabled = false;
		MaterialCtrl.setMaterial(ray[VrInput.RIGHT], MaterialCtrl.INTERACTION_TELEPORT_RAY);

		targetMarker = new GameObject[2];
		targetMarker[VrInput.LEFT] = GameObject.CreatePrimitive(PrimitiveType.Quad);
		targetMarker[VrInput.LEFT].name = "leftTeleportMarker";
		targetMarker[VrInput.LEFT].transform.parent = this.worldTransform;
		targetMarker[VrInput.LEFT].GetComponent<Collider>().enabled = false;
		MaterialCtrl.setMaterial(targetMarker[VrInput.LEFT], MaterialCtrl.INTERACTION_TELEPORT_TARGET);
		targetMarker[VrInput.RIGHT] = GameObject.CreatePrimitive(PrimitiveType.Quad);
		targetMarker[VrInput.RIGHT].name = "rightTeleportMarker";
		targetMarker[VrInput.RIGHT].transform.parent = this.worldTransform;
		targetMarker[VrInput.RIGHT].GetComponent<Collider>().enabled = false;
		MaterialCtrl.setMaterial(targetMarker[VrInput.RIGHT], MaterialCtrl.INTERACTION_TELEPORT_TARGET);

		// create a box made of fader objects
		faderHolder = new GameObject("faderHolder");
		ObjectFactory.createInvertedCube(faderHolder, 0.1f, MaterialCtrl.FADEABLE_BLACK);
		faderHolder.SetActive(false);
	}

	public void update(VrInput input) {

		if (input.camPosition != null) {
			faderHolder.transform.position = input.camPosition;
		}

		// slowly rotate the transport target point, because... well... it looks fun! :D
		if (targetMarker[VrInput.LEFT].activeSelf) {
			targetMarker[VrInput.LEFT].transform.localEulerAngles = new Vector3(90, 0, 20 * Time.time);
		}
		if (targetMarker[VrInput.RIGHT].activeSelf) {
			targetMarker[VrInput.RIGHT].transform.localEulerAngles = new Vector3(90, 0, -20 * Time.time);
		}

		if (teleportInProgress) {

			if (fadingOut) {
				// do the fade out quite quickly
				fade = 2 * (Time.time - fadeStartTime);
				if (fade > 1.0f) {
					performTeleportation();
				}
			} else {
				// do the fade in nice and calm and slow
				fade = fadeStartTime - Time.time;
				if (fade < 0.0f) {
					stopTeleportation();
				}
			}

			// update the fader color based on the calculated value
			Color fadeColor = MaterialCtrl.getMaterial(MaterialCtrl.FADEABLE_BLACK).color;
			fadeColor.a = fade;
			MaterialCtrl.setColor(MaterialCtrl.FADEABLE_BLACK, fadeColor);

		} else {

			checkInput(input, VrInput.LEFT);

			checkInput(input, VrInput.RIGHT);
		}
	}

	private void checkInput(VrInput input, int leftOrRight) {

		ControllerBehaviour ctrl = controllers[leftOrRight];

		// keep track of the last targeted button, such that we can send an on blur
		// event in case the button is no longer targeted
		Button unhighlightButton = null;
		if (targetingButton[leftOrRight] != null) {
			unhighlightButton = targetingButton[leftOrRight];
		}

		bool rayActive = false;
		targetingTeleportableArea[leftOrRight] = false;
		targetingButton[leftOrRight] = null;

		// check if we are holding a takeable object
		if (ctrl.isGrabbingObject()) {
			if (input.getTriggerReleased(leftOrRight)) {
				ctrl.drop(input.getVelocity(leftOrRight));
			} else {
				ctrl.stillGrabbing();
			}
		} else {
			// check if we are close to a takeable object - as taking objects with the
			// trigger key has precedence over pressing buttons and performing teleports
			if (input.getLastHoveredObject(leftOrRight) != null) {
				if (input.getTriggerClicked(leftOrRight)) {
					ctrl.drop(input.getVelocity(leftOrRight));
					ctrl.grab(input.getLastHoveredObject(leftOrRight));
				}
			} else {
				// if the trigger is being pressed, check the direction in which we are pointing
				if (input.getTriggerPressed(leftOrRight) || input.getTriggerReleased(leftOrRight)) {
					checkTriggeringDirection(input, leftOrRight);
					rayActive = true;
				}

				// if the trigger has been released now...
				if (input.getTriggerReleased(leftOrRight)) {
					// ... and if we are pointing somewhere teleport-y, then actually teleport!
					if (targetingTeleportableArea[leftOrRight]) {
						SoundCtrl.playMainCamSound(SoundCtrl.WHOOSH_1);
						teleportVector = potentialTeleportVector[leftOrRight];
						startTeleportation();
					}
					// ... and if we are targeting a button, push it!
					if (targetingButton[leftOrRight] != null) {
						targetingButton[leftOrRight].trigger();
						targetingButton[leftOrRight].blur();
					}
				}
			}
		}

		ray[leftOrRight].SetActive(rayActive);
		targetMarker[leftOrRight].SetActive(targetingTeleportableArea[leftOrRight]);

		// if there is a button that we can unhighlight (as it was targeted before)...
		if (unhighlightButton != null) {
			// ... and this button is no longer targeted (because the current target is
			// null or a different button)...
			if (targetingButton[leftOrRight] != unhighlightButton) {
				// ... then send an on blur event to this button!
				unhighlightButton.blur();
			}
		}
	}

	/**
	 * Check the direction in which we are pointing and either show a teleportation
	 * indicator (if we can teleport there), or elsewise at least a ray
	 */
	private void checkTriggeringDirection(VrInput input, int leftOrRight) {

		Vector3 origin = input.getPosition(leftOrRight);
		Vector3 direction = input.getRotation(leftOrRight) * Vector3.forward;

		RaycastHit target;

		bool targetingSomething = Physics.Raycast(
			origin,
			direction,
			out target,
			10000
		);

		// if we are targeting something then we are VERY interested in the following:
		if (targetingSomething) {

			// are we targeting an area that we can teleport into?
			targetingTeleportableArea[leftOrRight] = FLOOR_NAME.Equals(target.transform.gameObject.name);

			if (targetingTeleportableArea[leftOrRight]) {

				targetPoint[leftOrRight] = target.point;

				targetMarker[leftOrRight].transform.localPosition = new Vector3(
					targetPoint[leftOrRight].x - worldTransform.position.x,
					0.01f,
					targetPoint[leftOrRight].z - worldTransform.position.z
				);

				potentialTeleportVector[leftOrRight] = targetPoint[leftOrRight] - origin;
				potentialTeleportVector[leftOrRight].y = 0;
			}

			// or are we maybe targeting a button that we can click?
			string btnName = target.transform.gameObject.name;
			if (btnName != null) {
				if (btnName.StartsWith(ButtonCtrl.BUTTON_IDENTIFIER)) {
					targetingButton[leftOrRight] = ButtonCtrl.get(btnName);
					if (targetingButton[leftOrRight] != null) {
						targetingButton[leftOrRight].hover();
					}
				}
			}

			// show a ray even if the floor is not target to show people that they CAN do something
			drawRayFromTo(origin, target.point, leftOrRight);

		} else {

			// show a ray even if literally nothing at all was hit
			drawRayFromAlong(origin, direction, leftOrRight);
		}
	}

	/**
	 * Draw the ray between our controller and the floor (or wall, or whereever we are pointing)
	 */
	private void drawRayFromTo(Vector3 origin, Vector3 target, int leftOrRight) {

		ray[leftOrRight].transform.position = Vector3.Lerp(origin, target, 0.5f);

		ray[leftOrRight].transform.LookAt(target);
		Vector3 ang = ray[leftOrRight].transform.eulerAngles;
		ray[leftOrRight].transform.eulerAngles = new Vector3(ang.x - 90, ang.y, ang.z);

		ray[leftOrRight].transform.localScale = new Vector3(
			0.01f,
			Vector3.Distance(origin, target) / 2,
			0.01f
		);
	}

	/**
	 * Draw the ray between our controller and INFINITYYYY! :D
	 */
	private void drawRayFromAlong(Vector3 origin, Vector3 direction, int leftOrRight) {

		Vector3 target = origin + 1000 * direction.normalized;

		drawRayFromTo(origin, target, leftOrRight);
	}

	/**
	 * Initiate the teleportation by fading out
	 */
	private void startTeleportation() {

		fade = 0.0f;

		fadingOut = true;

		fadeStartTime = Time.time;

		teleportInProgress = true;

		faderHolder.SetActive(true);
	}

	/**
	 * Actually perform the teleportation - that is, reset the user's position in the world
	 * (we should be at full fade out when this is called)
	 */
	private void performTeleportation() {

		// adjust fade variables
		fade = 1.0f;
		fadingOut = false;
		fadeStartTime = Time.time + 1.0f;

		// perform the relocation
		worldTransform.position = worldTransform.position - teleportVector;

		ray[VrInput.LEFT].SetActive(false);
		ray[VrInput.RIGHT].SetActive(false);
		targetMarker[VrInput.LEFT].SetActive(false);
		targetMarker[VrInput.RIGHT].SetActive(false);
	}

	/**
	 * We are done! :D
	 */
	private void stopTeleportation() {

		fade = 0.0f;

		teleportInProgress = false;

		faderHolder.SetActive(false);
	}

	public void setControllers(ControllerBehaviour[] controllers) {
		this.controllers = controllers;
	}

	public void reset() {
		// on reset, drop everything - such that it can go back to where it belongs!
		controllers[VrInput.LEFT].drop(new Vector3(0, 0, 0));
		controllers[VrInput.RIGHT].drop(new Vector3(0, 0, 0));
	}

}

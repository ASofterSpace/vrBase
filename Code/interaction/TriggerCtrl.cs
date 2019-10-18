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

	private GameObject ray;
	private GameObject targetMarker;
	private GameObject faderHolder;
	private Transform worldTransform;

	private Vector3 targetPoint;
	private Vector3 potentialTeleportVector;

	private bool teleportInProgress;
	private bool targetingTeleportableArea;
	private Button targetingButton;

	private float fade;
	private float fadeStartTime;
	private bool fadingOut;

	public const string FLOOR_NAME = "floor";


	public TriggerCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;
		this.worldTransform = mainCtrl.getWorld().transform;

		this.teleportInProgress = false;
		this.targetingTeleportableArea = false;
		this.targetingButton = null;

		ray = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		ray.name = "teleportRay";
		ray.transform.parent = this.worldTransform;
		ray.GetComponent<Collider>().enabled = false;
		MaterialCtrl.setMaterial(ray, MaterialCtrl.INTERACTION_TELEPORT_RAY);

		targetMarker = GameObject.CreatePrimitive(PrimitiveType.Quad);
		targetMarker.name = "teleportMarker";
		targetMarker.transform.parent = this.worldTransform;
		targetMarker.GetComponent<Collider>().enabled = false;
		MaterialCtrl.setMaterial(targetMarker, MaterialCtrl.INTERACTION_TELEPORT_TARGET);

		// create a box made of fader objects
		faderHolder = new GameObject("faderHolder");
		ObjectFactory.createInvertedCube(faderHolder, 0.1f, MaterialCtrl.FADEABLE_BLACK);
		faderHolder.SetActive(false);
	}

	public void update(VrInput input) {

		if (input.camPosition != null) {
			faderHolder.transform.position = input.camPosition;
		}

		if (targetMarker.activeSelf) {
			// slowly rotate the transport target point, because... well... it looks fun! :D
			targetMarker.transform.localEulerAngles = new Vector3(90, 0, 20 * Time.time);
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

			// keep track of the last targeted button, such that we can send an on blur
			// event in case the button is no longer targeted
			Button unhighlightButton = null;
			if (targetingButton != null) {
				unhighlightButton = targetingButton;
			}

			// if a key is being pressed, check the direction in which we are pointing
			if (input.someTriggerPressed) {
				checkTriggeringDirection(input);
			} else {
				ray.SetActive(false);
				targetMarker.SetActive(false);
			}

			// if there is a button that we can unhighlight (as it was targeted before)...
			if (unhighlightButton != null) {
				// ... and this button is no longer targeted (because the current target is
				// null or a different button)...
				if (targetingButton != unhighlightButton) {
					// ... then send an on blur event to this button!
					unhighlightButton.blur();
				}
			}

			// if the trigger has been released now...
			if (input.someTriggerReleased) {
				// ... and if we are pointing somewhere teleport-y, then actually teleport!
				if (input.someTriggerReleased && targetingTeleportableArea) {
					SoundCtrl.playMainCamSound(SoundCtrl.WHOOSH_1);
					startTeleportation();
				}
				// ... and if we are targeting a button, push it!
				if (targetingButton != null) {
					targetingButton.trigger();
					targetingButton.blur();
				}
			}
		}
	}

	/**
	 * Check the direction in which we are pointing and either show a teleportation
	 * indicator (if we can teleport there), or elsewise at least a ray
	 */
	private void checkTriggeringDirection(VrInput input) {

		Vector3 origin = input.leftPosition;
		Vector3 direction = input.leftRotation * Vector3.forward;
		if (input.rightTriggerPressed) {
			origin = input.rightPosition;
			direction = input.rightRotation * Vector3.forward;
		}

		RaycastHit target;

		targetingTeleportableArea = false;
		targetingButton = null;

		bool targetingSomething = Physics.Raycast(
			origin,
			direction,
			out target,
			10000
		);

		// if we are targeting something then we are VERY interested in the following:
		if (targetingSomething) {

			// are we targeting an area that we can teleport into?
			targetingTeleportableArea = FLOOR_NAME.Equals(target.transform.gameObject.name);

			if (targetingTeleportableArea) {

				targetPoint = target.point;

				targetMarker.transform.localPosition = new Vector3(
					targetPoint.x - worldTransform.position.x,
					0.01f,
					targetPoint.z - worldTransform.position.z
				);

				potentialTeleportVector = targetPoint - origin;
				potentialTeleportVector.y = 0;
			}

			// or are we maybe targeting a button that we can click?
			string btnName = target.transform.gameObject.name;
			if (btnName != null) {
				if (btnName.StartsWith("btn-")) {
					targetingButton = ButtonCtrl.get(btnName);
					if (targetingButton != null) {
						targetingButton.hover();
					}
				}
			}

/*
			// show a ray even if the floor is not target to show people that they CAN do something
			drawRayFromTo(origin, target);
		}

		ray.SetActive(targetingTeleportableArea || targetingSomething);

		targetMarker.SetActive(targetingTeleportableArea);
*/

			// show a ray even if the floor is not target to show people that they CAN do something
			drawRayFromTo(origin, target.point);

		} else {

			// show a ray even if literally nothing at all was hit
			drawRayFromAlong(origin, direction);
		}

		targetMarker.SetActive(targetingTeleportableArea);
	}

	/**
	 * Draw the ray between our controller and the floor (or wall, or whereever we are pointing)
	 */
	private void drawRayFromTo(Vector3 origin, Vector3 target) {

		ray.SetActive(true);

		ray.transform.position = Vector3.Lerp(origin, target, 0.5f);

		ray.transform.LookAt(target);
		Vector3 ang = ray.transform.eulerAngles;
		ray.transform.localEulerAngles = new Vector3(ang.x - 90, ang.y, ang.z);

		ray.transform.localScale = new Vector3(
			0.01f,
			Vector3.Distance(origin, target) / 2,
			0.01f
		);
	}

	/**
	 * Draw the ray between our controller and INFINITYYYY! :D
	 */
	private void drawRayFromAlong(Vector3 origin, Vector3 direction) {

		Vector3 target = origin + 1000 * direction.normalized;

		drawRayFromTo(origin, target);
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

		// hide the funny targetMarker
		targetMarker.SetActive(false);

		// perform the relocation
		// mainCameraHolderTransform.position = mainCameraHolderTransform.position + potentialTeleportVector;
		worldTransform.position = worldTransform.position - potentialTeleportVector;
	}

	/**
	 * We are done! :D
	 */
	private void stopTeleportation() {

		fade = 0.0f;

		teleportInProgress = false;

		faderHolder.SetActive(false);
	}

}

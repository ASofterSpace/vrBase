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

	private float fade;
	private float fadeStartTime;
	private bool fadingOut;

	public const string FLOOR_NAME = "floor";


	public TriggerCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;
		this.worldTransform = mainCtrl.getWorld().transform;

		this.teleportInProgress = false;
		this.targetingTeleportableArea = false;

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
			targetMarker.transform.eulerAngles = new Vector3(90, 0, 20 * Time.time);
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

			// TODO :: use some other button to perform teleportation ;)
			if (input.someTriggerPressed) {
				checkTeleportDirection(input);
			} else {
				ray.SetActive(false);
				targetMarker.SetActive(false);
			}

			if (input.someTriggerReleased && targetingTeleportableArea) {
				startTeleportation();
			}
		}
	}

	/**
	 * Check the direction in which we are pointing and either show a teleportation
	 * indicator (if we can teleport there), or elsewise at least a ray
	 */
	private void checkTeleportDirection(VrInput input) {

		Vector3 origin = input.leftPosition;
		Vector3 direction = input.leftRotation * Vector3.forward;
		if (input.rightTriggerPressed) {
			origin = input.rightPosition;
			direction = input.rightRotation * Vector3.forward;
		}

		RaycastHit target;

		targetingTeleportableArea = false;

		bool targetingSomething = Physics.Raycast(
			origin,
			direction,
			out target,
			10000
		);

		if (targetingSomething) {

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

			// show a ray even if the floor is not target to show people that they CAN do something
			drawRayFromTo(origin, target);
		}

		ray.SetActive(targetingTeleportableArea || targetingSomething);

		targetMarker.SetActive(targetingTeleportableArea);
	}

	/**
	 * Draw the ray between our controller and the floor (or wall, or whereever we are pointing)
	 */
	private void drawRayFromTo(Vector3 origin, RaycastHit target) {

		ray.SetActive(true);

		ray.transform.position = Vector3.Lerp(origin, target.point, 0.5f);

		ray.transform.LookAt(target.point);
		Vector3 ang = ray.transform.eulerAngles;
		ray.transform.eulerAngles = new Vector3(ang.x - 90, ang.y, ang.z);

		ray.transform.localScale = new Vector3(
			0.01f,
			target.distance / 2,
			0.01f
		);
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

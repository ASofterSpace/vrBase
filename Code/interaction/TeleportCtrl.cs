/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class TeleportCtrl {

	private MainCtrl mainCtrl;
	private MaterialCtrl materialCtrl;

	private GameObject ray;
	private GameObject targetMarker;
	private GameObject faderHolder;
	private GameObject[] faders;
	private Transform worldTransform;

	private Vector3 targetPoint;
	private Vector3 potentialTeleportVector;

	private bool teleportInProgress;
	private bool targetingTeleportableArea;

	private float fade;
	private float fadeStartTime;
	private bool fadingOut;

	public const string FLOOR_NAME = "floor";


	public TeleportCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;
		this.materialCtrl = mainCtrl.getMaterialCtrl();
		this.worldTransform = mainCtrl.getWorld().transform;

		this.teleportInProgress = false;
		this.targetingTeleportableArea = false;

		ray = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		ray.name = "teleportRay";
		ray.transform.parent = this.worldTransform;
		ray.GetComponent<Collider>().enabled = false;
		materialCtrl.setMaterial(ray, MaterialCtrl.INTERACTION_TELEPORT_RAY);

		targetMarker = GameObject.CreatePrimitive(PrimitiveType.Quad);
		targetMarker.name = "teleportMarker";
		targetMarker.transform.parent = this.worldTransform;
		targetMarker.GetComponent<Collider>().enabled = false;
		materialCtrl.setMaterial(targetMarker, MaterialCtrl.INTERACTION_TELEPORT_TARGET);

		// create two boxes made of fader objects, one a bit further inside
		// that is better (but might miss the camera), and one a bit further
		// outside that is worse (because objects might slip inside), but is
		// more definitely actually surrounding the camera :D
		faderHolder = new GameObject("faderHolder");
		faders = new GameObject[12];
		float small = 0.1f;
		float big = 0.5f;
		for (int i = 0; i < 6; i++) {
			faders[i] = createFader(small);
		}
		for (int i = 6; i < 12; i++) {
			faders[i] = createFader(big);
		}
		faders[0].transform.localPosition = new Vector3(0, 0, small);
		faders[0].transform.eulerAngles = new Vector3(0, 0, 0);
		faders[1].transform.localPosition = new Vector3(0, -small, 0);
		faders[1].transform.eulerAngles = new Vector3(90, 0, 0);
		faders[2].transform.localPosition = new Vector3(0, 0, -small);
		faders[2].transform.eulerAngles = new Vector3(180, 0, 0);
		faders[3].transform.localPosition = new Vector3(0, small, 0);
		faders[3].transform.eulerAngles = new Vector3(270, 0, 0);
		faders[4].transform.localPosition = new Vector3(-small, 0, 0);
		faders[4].transform.eulerAngles = new Vector3(0, -90, 0);
		faders[5].transform.localPosition = new Vector3(small, 0, 0);
		faders[5].transform.eulerAngles = new Vector3(0, 90, 0);
		faders[6].transform.localPosition = new Vector3(0, 0, big);
		faders[6].transform.eulerAngles = new Vector3(0, 0, 0);
		faders[7].transform.localPosition = new Vector3(0, -big, 0);
		faders[7].transform.eulerAngles = new Vector3(90, 0, 0);
		faders[8].transform.localPosition = new Vector3(0, 0, -big);
		faders[8].transform.eulerAngles = new Vector3(180, 0, 0);
		faders[9].transform.localPosition = new Vector3(0, big, 0);
		faders[9].transform.eulerAngles = new Vector3(270, 0, 0);
		faders[10].transform.localPosition = new Vector3(-big, 0, 0);
		faders[10].transform.eulerAngles = new Vector3(0, -90, 0);
		faders[11].transform.localPosition = new Vector3(big, 0, 0);
		faders[11].transform.eulerAngles = new Vector3(0, 90, 0);
	}

	public GameObject createFader(float dist) {

		GameObject fader = GameObject.CreatePrimitive(PrimitiveType.Quad);
		fader.name = "fader";
		fader.transform.parent = faderHolder.transform;
		fader.transform.localScale = new Vector3(dist*2, dist*2, dist*2);
		materialCtrl.setMaterial(fader, MaterialCtrl.FADEABLE_BLACK);
		fader.SetActive(false);
		return fader;
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
			Color fadeColor = materialCtrl.getMaterial(MaterialCtrl.FADEABLE_BLACK).color;
			fadeColor.a = fade;
			materialCtrl.setColor(MaterialCtrl.FADEABLE_BLACK, fadeColor);

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

		for (int i = 0; i < faders.Length; i++) {
			faders[i].SetActive(true);
		}
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

		for (int i = 0; i < faders.Length; i++) {
			faders[i].SetActive(false);
		}
	}

}

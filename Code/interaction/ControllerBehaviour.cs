/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This takes care of the behaviour of a single controller
 */
public class ControllerBehaviour : MonoBehaviour {

	private GameObject controller;

	private TakeableObject lastHoveredObject;

	private TakeableObject grabbedObject;

	private Vector3 grabbedPosition;
	private Quaternion grabbedRotation;


	public void init(GameObject controller) {

		this.controller = controller;
	}

	void OnTriggerEnter(Collider collider) {

		// ignore further objects if we actually already grabbed one
		if (grabbedObject != null) {
			return;
		}

		string colName = collider.gameObject.name;

		if (colName != null) {
			if (colName.StartsWith(ObjectCtrl.OBJECT_IDENTIFIER) || colName.StartsWith(ButtonCtrl.BUTTON_IDENTIFIER)) {

				if (lastHoveredObject != null) {
					lastHoveredObject.blur();
				}

				TakeableObject obj = ObjectCtrl.get(collider.gameObject.name);
				obj.hover();

				lastHoveredObject = obj;
			}
		}
	}

	void OnTriggerExit(Collider collider) {

		string colName = collider.gameObject.name;

		if (colName != null) {
			if (colName.StartsWith(ObjectCtrl.OBJECT_IDENTIFIER)) {

				TakeableObject obj = ObjectCtrl.get(collider.gameObject.name);
				obj.blur();

				lastHoveredObject = null;
			}
		}
	}

	public void grab(TakeableObject obj) {

		grabbedObject = obj;

		// blur the object we just grabbed so that we can enjoy how nice it looks
		grabbedObject.blur();

		grabbedObject.grab(controller);

		grabbedPosition = grabbedObject.transform.localPosition;
		grabbedRotation = grabbedObject.transform.localRotation;
	}

	public void drop(Vector3 velocity) {

		if (grabbedObject != null) {
			grabbedObject.drop(velocity);
		}

		grabbedObject = null;
	}

	public TakeableObject getLastHoveredObject() {
		return lastHoveredObject;
	}

	public bool isGrabbingObject() {
		return grabbedObject != null;
	}

	/**
	 * The object is still being grabbed, every grabby frame!
	 */
	public void stillGrabbing() {

		grabbedObject.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		grabbedObject.transform.localPosition = grabbedPosition;
		grabbedObject.transform.localRotation = grabbedRotation;
	}

	public GameObject getController() {
		return controller;
	}

}

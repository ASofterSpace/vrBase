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


	public void init(GameObject controller) {
		this.controller = controller;
	}

	void OnCollisionEnter(Collision collision) {

		string colName = collision.gameObject.name;

		if (colName != null) {
			if (colName.StartsWith(ObjectCtrl.OBJECT_IDENTIFIER)) {

				if (lastHoveredObject != null) {
					lastHoveredObject.blur();
				}

				TakeableObject obj = ObjectCtrl.get(collision.gameObject.name);
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

	public TakeableObject getLastHoveredObject() {
		return lastHoveredObject;
	}

}

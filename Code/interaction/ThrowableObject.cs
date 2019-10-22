/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This corresponds to a single object that the user can take and throw with their controller
 */
public class ThrowableObject : TakeableObject {

	public ThrowableObject(GameObject obj) : base(obj) {

	}

	public override void grab(GameObject controller) {

		rigidbody.useGravity = false;
		rigidbody.velocity = new Vector3(0, 0, 0);

		if (originalParent == null) {
			originalParent = gameObject.transform.parent.gameObject;
			resetParent = originalParent;
		}
		gameObject.transform.parent = controller.transform;

		base.grab(controller);
	}

	public override void drop(Vector3 velocity) {

		gameObject.transform.parent = originalParent.transform;

		rigidbody.useGravity = true;
		rigidbody.velocity = velocity;

		base.drop(velocity);
	}

}

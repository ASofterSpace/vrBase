/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This corresponds to a single object that the user can take and throw with their controller,
 * which however until there are interactions with the user stays RIGHT WHERE IT IS NOW,
 * without moving
 */
public class ThrowableBoundObject : ThrowableObject, ResetteableCtrl {

	private Vector3 origPosition;
	private Quaternion origRotation;
	private Vector3 origScale;
	private bool isStillBound;


	public ThrowableBoundObject(GameObject obj) : base(obj) {

		rigidbody.isKinematic = true;

		origPosition = gameObject.transform.localPosition;
		origRotation = gameObject.transform.localRotation;
		origScale = gameObject.transform.localScale;

		isStillBound = true;
	}

	public override void grab(GameObject controller) {

		isStillBound = false;

		gameObject.GetComponent<Rigidbody>().isKinematic = false;

		base.grab(controller);
	}

	public override void drop(Vector3 velocity) {

		originalParent = mainCtrl.getWorld();

		base.drop(velocity);
	}

	public void reset() {

		rigidbody.isKinematic = true;

		if (resetParent != null) {
			originalParent = resetParent;
			transform.parent = resetParent.transform;
		}

		gameObject.transform.localPosition = origPosition;
		gameObject.transform.localRotation = origRotation;
		gameObject.transform.localScale = origScale;

		isStillBound = true;
	}

	public bool isBound() {
		return isStillBound;
	}

}

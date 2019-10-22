/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This corresponds to a single button
 */
public class Button: TakeableObject {

	public Button(GameObject obj): base(obj) {

		rigidbody.isKinematic = true;
		rigidbody.useGravity = false;
	}

	/**
	 * This button is being pressed - whoop whoop!
	 */
	public virtual void trigger() {
		// actually do something!
	}

	/**
	 * This object is being grabbed - whoop whoop!
	 */
	public override void grab(GameObject controller) {
		trigger();
	}

	/**
	 * This object is being dropped again - whoopsie!
	 */
	public override void drop(Vector3 veloctiy) {
		// actually do something!
	}

}

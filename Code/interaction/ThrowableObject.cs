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
public class ThrowableObject : TakeableObject {

	public ThrowableObject(GameObject obj) : base(obj) {

	}

	public override void drop(Vector3 velocity) {

		gameObject.transform.parent = originalParent.transform;

		gameObject.GetComponent<Rigidbody>().useGravity = true;

		gameObject.GetComponent<Rigidbody>().velocity = velocity;
	}

}

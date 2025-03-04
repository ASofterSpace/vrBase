﻿/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A very generic factory that can help us create objects
 * more easily
 */
public class ObjectMultiplier {

	/**
	 * You have created the first object, and realize that you want three more just like that,
	 * arranged in a point-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into four! :D
	 *
	 * so this:    turns into:
	 *
	 *      /              /
	 *   O              O
	 *
	 *                   \
	 */
	public static GameObject[] pointDuplize90(GameObject first) {

		GameObject[] objs = new GameObject[1];

		for (int i = 0; i < objs.Length; i++) {
			objs[i] = Object.Instantiate(first, first.transform.parent);
			objs[i].name = first.name + (i+2);
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.localRotation;

		objs[0].transform.localPosition = new Vector3(z, y, -x);
		objs[0].transform.localRotation = Quaternion.Euler(Vector3.up * 90) * rot;

		return objs;
	}

	/**
	 * You have created the first object, and realize that you want three more just like that,
	 * arranged in a point-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into four! :D
	 *
	 * so this:    turns into:
	 *
	 *      /              /
	 *   O              O
	 *               /
	 *
	 */
	public static GameObject[] pointDuplize180(GameObject first) {

		GameObject[] objs = new GameObject[1];

		for (int i = 0; i < objs.Length; i++) {
			objs[i] = Object.Instantiate(first, first.transform.parent);
			objs[i].name = first.name + (i+2);
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.localRotation;

		objs[0].transform.localPosition = new Vector3(-x, y, -z);
		objs[0].transform.localRotation = Quaternion.Euler(Vector3.up * 180) * rot;

		return objs;
	}

	/**
	 * You have created the first object, and realize that you want three more just like that,
	 * arranged in a point-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into four! :D
	 *
	 * so this:    turns into:
	 *                 \
	 *      /              /
	 *   O              O
	 *               /
	 *                   \
	 */
	public static GameObject[] pointQuadruplize(GameObject first) {

		GameObject[] objs = new GameObject[3];

		for (int i = 0; i < objs.Length; i++) {
			objs[i] = Object.Instantiate(first, first.transform.parent);
			objs[i].name = first.name + (i+2);
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.localRotation;

		objs[0].transform.localPosition = new Vector3(z, y, -x);
		objs[0].transform.localRotation = Quaternion.Euler(Vector3.up * 90) * rot;

		objs[1].transform.localPosition = new Vector3(-x, y, -z);
		objs[1].transform.localRotation = Quaternion.Euler(Vector3.up * 180) * rot;

		objs[2].transform.localPosition = new Vector3(-z, y, x);
		objs[2].transform.localRotation = Quaternion.Euler(Vector3.up * 270) * rot;

		return objs;
	}

	/**
	 * You have created the first object, and realize that you want three more just like that,
	 * arranged in an axis-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into four! :D
	 *
	 * so this:    turns into:
	 *
	 *      /        \     /
	 *   O              O
	 *               /     \
	 */
	public static GameObject[] axisQuadruplize(GameObject first) {

		GameObject[] objs = new GameObject[3];

		for (int i = 0; i < objs.Length; i++) {
			objs[i] = Object.Instantiate(first, first.transform.parent);
			objs[i].name = first.name + (i+2);
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.localRotation;
		float qx = rot.x;
		float qy = rot.y;
		float qz = rot.z;
		float qw = rot.w;

		objs[0].transform.localPosition = new Vector3(x, y, -z);
		objs[0].transform.localRotation = new Quaternion(qw, qz, qy, qx);

		objs[1].transform.localPosition = new Vector3(-x, y, -z);
		objs[1].transform.localRotation = new Quaternion(qy, qx, qw, qz);

		objs[2].transform.localPosition = new Vector3(-x, y, z);
		objs[2].transform.localRotation = new Quaternion(qz, qw, qx, qy);

		return objs;
	}

	/**
	 * You have created the first object, and realize that you want seven more just like that,
	 * arranged in a point-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into eight! :D
	 *
	 * so this:    turns into:
	 *                 | /
	 *     -          \   -
	 *   O              O
	 *                -   \
	 *                 / |
	 */
	public static GameObject[] pointOctuplize(GameObject first) {

		GameObject[] objs = new GameObject[7];

		for (int i = 0; i < objs.Length; i++) {
			objs[i] = Object.Instantiate(first, first.transform.parent);
			objs[i].name = first.name + (i+2);
		}

		Quaternion rot = first.transform.localRotation;

		objs[0].transform.localPosition = Quaternion.Euler(Vector3.up * 45) * first.transform.localPosition;
		objs[0].transform.localRotation = Quaternion.Euler(Vector3.up * 45) * rot;

		objs[1].transform.localPosition = Quaternion.Euler(Vector3.up * 90) * first.transform.localPosition;
		objs[1].transform.localRotation = Quaternion.Euler(Vector3.up * 90) * rot;

		objs[2].transform.localPosition = Quaternion.Euler(Vector3.up * 135) * first.transform.localPosition;
		objs[2].transform.localRotation = Quaternion.Euler(Vector3.up * 135) * rot;

		objs[3].transform.localPosition = Quaternion.Euler(Vector3.up * 180) * first.transform.localPosition;
		objs[3].transform.localRotation = Quaternion.Euler(Vector3.up * 180) * rot;

		objs[4].transform.localPosition = Quaternion.Euler(Vector3.up * 225) * first.transform.localPosition;
		objs[4].transform.localRotation = Quaternion.Euler(Vector3.up * 225) * rot;

		objs[5].transform.localPosition = Quaternion.Euler(Vector3.up * 270) * first.transform.localPosition;
		objs[5].transform.localRotation = Quaternion.Euler(Vector3.up * 270) * rot;

		objs[6].transform.localPosition = Quaternion.Euler(Vector3.up * 315) * first.transform.localPosition;
		objs[6].transform.localRotation = Quaternion.Euler(Vector3.up * 315) * rot;

		return objs;
	}

	/**
	 * You have created the first object, and realize that you want seven more just like that,
	 * arranged in an axis-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into eight! :D
	 *
	 * so this:    turns into:
	 *                 | |
	 *     -          -   -
	 *   O              O
	 *                -   -
	 *                 | |
	 */
	public static GameObject[] axisOctuplize(GameObject first) {

		GameObject[] objs = new GameObject[7];

		// basically, we first quadruplize...
		GameObject[] objs012 = axisQuadruplize(first);

		// ... then copy the input object and rotate the copy by 90 degrees...
		GameObject obj3 = Object.Instantiate(first, first.transform.parent);
		obj3.transform.localPosition = new Vector3(
			 first.transform.localPosition.z,
			 first.transform.localPosition.y,
			-first.transform.localPosition.x
		);
		obj3.transform.localRotation = Quaternion.Euler(Vector3.up * 90) * first.transform.localRotation;

		// ... and quadruplize again! Wheee! \o/
		GameObject[] objs456 = axisQuadruplize(obj3);

		objs[0] = objs012[0];
		objs[1] = objs012[1];
		objs[2] = objs012[2];
		objs[3] = obj3;
		objs[4] = objs456[0];
		objs[5] = objs456[1];
		objs[6] = objs456[2];

		return objs;
	}

	/**
	 * You have created the first object, and realize that you want fifteen more just like that,
	 * arranged in an axis-symmetrical(!) shape in the X-Z-axis around their parent?
	 * WELL LUCKY YOU! I take one object, and make it into sixteen! :D
	 *
	 * so this:    turns into:
	 *               .\| |/.
	 *      -        -     -
	 *   O              O
	 *               -     -
	 *               '/| |\'
	 */
	public static GameObject[] axisHexadeciplize(GameObject first) {

		GameObject[] objs = new GameObject[15];

		// basically, we first quadruplize...
		GameObject[] objs012 = axisQuadruplize(first);

		// ... then copy the input object and rotate the copy by 45 degrees...
		GameObject obj3 = Object.Instantiate(first, first.transform.parent);
		obj3.transform.localPosition = Quaternion.Euler(Vector3.up * 45) * first.transform.localPosition;
		obj3.transform.localRotation = Quaternion.Euler(Vector3.up * 45) * first.transform.localRotation;

		// ... and quadruplize again...
		GameObject[] objs456 = axisQuadruplize(obj3);

		// ... then copy the input object and rotate the copy by 90 degrees...
		GameObject obj7 = Object.Instantiate(first, first.transform.parent);
		obj7.transform.localPosition = new Vector3(
			 first.transform.localPosition.z,
			 first.transform.localPosition.y,
			-first.transform.localPosition.x
		);
		obj7.transform.localRotation = Quaternion.Euler(Vector3.up * 90) * first.transform.localRotation;

		// ... and quadruplize again...
		GameObject[] objs8910 = axisQuadruplize(obj7);

		// ... then copy the input object and rotate the copy by 135 degrees...
		GameObject obj11 = Object.Instantiate(first, first.transform.parent);
		obj11.transform.localPosition = Quaternion.Euler(Vector3.up * 135) * first.transform.localPosition;
		obj11.transform.localRotation = Quaternion.Euler(Vector3.up * 135) * first.transform.localRotation;

		// ... and quadruplize again! Wheee! \o/
		GameObject[] objs121314 = axisQuadruplize(obj11);

		objs[0] = objs012[0];
		objs[1] = objs012[1];
		objs[2] = objs012[2];
		objs[3] = obj3;
		objs[4] = objs456[0];
		objs[5] = objs456[1];
		objs[6] = objs456[2];
		objs[7] = obj7;
		objs[8] = objs8910[0];
		objs[9] = objs8910[1];
		objs[10] = objs8910[2];
		objs[11] = obj11;
		objs[12] = objs121314[0];
		objs[13] = objs121314[1];
		objs[14] = objs121314[2];

		return objs;
	}

	public static void axisTetraduplize(GameObject first) {

		axisHexadeciplize(first);

		GameObject obj = Object.Instantiate(first, first.transform.parent);
		obj.transform.localPosition = Quaternion.Euler(Vector3.up * 22.5f) * first.transform.localPosition;
		obj.transform.localRotation = Quaternion.Euler(Vector3.up * 22.5f) * first.transform.localRotation;

		axisHexadeciplize(obj);
	}

}

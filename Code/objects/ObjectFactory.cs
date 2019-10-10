/**
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
public class ObjectFactory {

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
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.rotation;

		objs[0].transform.localPosition = new Vector3(z, y, -x);
		objs[0].transform.rotation = Quaternion.Euler(Vector3.up * 90) * rot;

		objs[1].transform.localPosition = new Vector3(-x, y, -z);
		objs[1].transform.rotation = Quaternion.Euler(Vector3.up * 180) * rot;

		objs[2].transform.localPosition = new Vector3(-z, y, x);
		objs[2].transform.rotation = Quaternion.Euler(Vector3.up * 270) * rot;

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
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.rotation;
		float qx = rot.x;
		float qy = rot.y;
		float qz = rot.z;
		float qw = rot.w;

		objs[0].transform.localPosition = new Vector3(x, y, -z);
		objs[0].transform.rotation = new Quaternion(qw, qz, qy, qx);

		objs[1].transform.localPosition = new Vector3(-x, y, -z);
		objs[1].transform.rotation = new Quaternion(qy, qx, qw, qz);

		objs[2].transform.localPosition = new Vector3(-x, y, z);
		objs[2].transform.rotation = new Quaternion(qz, qw, qx, qy);

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
		}

		float x = first.transform.localPosition.x;
		float z = first.transform.localPosition.z;
		float y = first.transform.localPosition.y;
		Quaternion rot = first.transform.rotation;

		objs[0].transform.localPosition = new Vector3(-z, y, -x);
		objs[0].transform.rotation = Quaternion.Euler(Vector3.up * 45) * rot;

		objs[1].transform.localPosition = new Vector3(z, y, -x);
		objs[1].transform.rotation = Quaternion.Euler(Vector3.up * 90) * rot;

		objs[2].transform.localPosition = new Vector3(-x, y, z);
		objs[2].transform.rotation = Quaternion.Euler(Vector3.up * 135) * rot;

		objs[3].transform.localPosition = new Vector3(-x, y, -z);
		objs[3].transform.rotation = Quaternion.Euler(Vector3.up * 180) * rot;

		objs[4].transform.localPosition = new Vector3(z, y, x);
		objs[4].transform.rotation = Quaternion.Euler(Vector3.up * 225) * rot;

		objs[5].transform.localPosition = new Vector3(-z, y, x);
		objs[5].transform.rotation = Quaternion.Euler(Vector3.up * 270) * rot;

		objs[6].transform.localPosition = new Vector3(x, y, -z);
		objs[6].transform.rotation = Quaternion.Euler(Vector3.up * 315) * rot;

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
		obj3.transform.rotation = Quaternion.Euler(Vector3.up * 90) * first.transform.rotation;

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
	 * Create an inverted Cube centered around the parent
	 * (inverted meaning that the textures are shown inwards, rather than outwards)
	 * TODO :: actually do this via mesh instead of six game objects!
	 */
	public static void createInvertedCube(GameObject parent, float radius, int material) {

		GameObject[] objs = new GameObject[6];

		for (int i = 0; i < objs.Length; i++) {
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
			obj.transform.parent = parent.transform;
			obj.transform.localScale = new Vector3(radius*2, radius*2, radius*2);
			MaterialCtrl.setMaterial(obj, material);
			objs[i] = obj;
		}

		objs[0].transform.localPosition = new Vector3(0, 0, radius);
		objs[0].transform.eulerAngles = new Vector3(0, 0, 0);
		objs[1].transform.localPosition = new Vector3(0, -radius, 0);
		objs[1].transform.eulerAngles = new Vector3(90, 0, 0);
		objs[2].transform.localPosition = new Vector3(0, 0, -radius);
		objs[2].transform.eulerAngles = new Vector3(180, 0, 0);
		objs[3].transform.localPosition = new Vector3(0, radius, 0);
		objs[3].transform.eulerAngles = new Vector3(270, 0, 0);
		objs[4].transform.localPosition = new Vector3(-radius, 0, 0);
		objs[4].transform.eulerAngles = new Vector3(0, -90, 0);
		objs[5].transform.localPosition = new Vector3(radius, 0, 0);
		objs[5].transform.eulerAngles = new Vector3(0, 90, 0);
	}

}

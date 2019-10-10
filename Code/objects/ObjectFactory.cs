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
	public static void pointQuadruplize(GameObject first) {

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
	public static void axisQuadruplize(GameObject first) {

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
	public static void pointOctuplize(GameObject first) {

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
	public static void axisOctuplize(GameObject first) {

		axisQuadruplize(first);

		GameObject fifth = Object.Instantiate(first, first.transform.parent);
		fifth.transform.localPosition = new Vector3(
			 first.transform.localPosition.z,
			 first.transform.localPosition.y,
			-first.transform.localPosition.x
		);
		fifth.transform.rotation = Quaternion.Euler(Vector3.up * 90) * first.transform.rotation;

		axisQuadruplize(fifth);
	}

	/**
	 * Create an inverted Cube centered around the parent
	 * (inverted meaning that the textures are shown inwards, rather than outwards)
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

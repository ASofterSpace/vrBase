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
	 * Create an inverted Cube centered around the parent
	 * (inverted meaning that the textures are shown inwards, rather than outwards)
	 */
	public static void createInvertedCube(GameObject parent, float radius, int material) {

		GameObject[] objs = new GameObject[6];

		float small = 0.1f;

		for (int i = 0; i < 6; i++) {
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
			obj.transform.parent = parent.transform;
			obj.transform.localScale = new Vector3(radius*2, radius*2, radius*2);
			MaterialCtrl.setMaterial(obj, material);
			objs[i] = obj;
		}

		objs[0].transform.localPosition = new Vector3(0, 0, small);
		objs[0].transform.eulerAngles = new Vector3(0, 0, 0);
		objs[1].transform.localPosition = new Vector3(0, -small, 0);
		objs[1].transform.eulerAngles = new Vector3(90, 0, 0);
		objs[2].transform.localPosition = new Vector3(0, 0, -small);
		objs[2].transform.eulerAngles = new Vector3(180, 0, 0);
		objs[3].transform.localPosition = new Vector3(0, small, 0);
		objs[3].transform.eulerAngles = new Vector3(270, 0, 0);
		objs[4].transform.localPosition = new Vector3(-small, 0, 0);
		objs[4].transform.eulerAngles = new Vector3(0, -90, 0);
		objs[5].transform.localPosition = new Vector3(small, 0, 0);
		objs[5].transform.eulerAngles = new Vector3(0, 90, 0);
	}

}

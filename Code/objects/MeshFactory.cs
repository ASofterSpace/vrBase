/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class MeshFactory {

	public static void finalizeMesh(Mesh mesh) {
		mesh.Optimize();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	public static void drawRayFromTo(GameObject parent, Vector3 origin, Vector3 target) {

		GameObject ray = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

		GameObject.Destroy(ray.GetComponent<Collider>());

		ray.transform.parent = parent.transform;

		ray.transform.position = Vector3.Lerp(origin, target, 0.5f);

		ray.transform.LookAt(target);
		Vector3 ang = ray.transform.localEulerAngles;
		ray.transform.localEulerAngles = new Vector3(ang.x - 90, ang.y, ang.z);

		ray.transform.localScale = new Vector3(
			0.05f,
			Vector3.Distance(origin, target) / 2,
			0.05f
		);

		MaterialCtrl.setMaterial(ray, MaterialCtrl.PLASTIC_WHITE);
	}
}

﻿/**
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
		drawRayFromTo(parent, origin, target, MaterialCtrl.PLASTIC_WHITE);
	}

	public static void drawRayFromTo(GameObject parent, Vector3 origin, Vector3 target, int rayMaterial) {

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

		MaterialCtrl.setMaterial(ray, rayMaterial);
	}

	public static void drawPoints(GameObject parent, Vector3[] vertices) {
		drawPoints(parent, vertices, MaterialCtrl.PLASTIC_WHITE);
	}

	public static void drawPoints(GameObject parent, Vector3[] vertices, int pointMaterial) {

		foreach (Vector3 vertex in vertices) {

			GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);

			point.transform.parent = parent.transform;

			point.transform.position = vertex;

			point.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);

			MaterialCtrl.setMaterial(point, pointMaterial);
		}
	}
}

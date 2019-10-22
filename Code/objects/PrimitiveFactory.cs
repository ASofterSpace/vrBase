/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A factory that can help us create primitive objects more easily
 */
public class PrimitiveFactory {

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
		objs[0].transform.localEulerAngles = new Vector3(0, 0, 0);
		objs[1].transform.localPosition = new Vector3(0, -radius, 0);
		objs[1].transform.localEulerAngles = new Vector3(90, 0, 0);
		objs[2].transform.localPosition = new Vector3(0, 0, -radius);
		objs[2].transform.localEulerAngles = new Vector3(180, 0, 0);
		objs[3].transform.localPosition = new Vector3(0, radius, 0);
		objs[3].transform.localEulerAngles = new Vector3(270, 0, 0);
		objs[4].transform.localPosition = new Vector3(-radius, 0, 0);
		objs[4].transform.localEulerAngles = new Vector3(0, -90, 0);
		objs[5].transform.localPosition = new Vector3(radius, 0, 0);
		objs[5].transform.localEulerAngles = new Vector3(0, 90, 0);
	}

	/**
	 * Create a cone with as many sides as you want (20 is useful if you want it to fit snugly
	 * on a cylinder), and optionally with or without a bottom, and with or without the interior
	 * also rendering
	 * Btw., the resulting size of the gameobject will correspond exactly to the side of a
	 * cylinder with the same measure
	 */
	public static GameObject createCone(int sides, bool hasBottom, bool renderInterior, int material) {

		GameObject outsideCone = _createCone(sides, hasBottom, false);
		if (renderInterior) {
			GameObject insideCone = _createCone(sides, hasBottom, true);
			insideCone.name = "Cone (Interior)";
			insideCone.transform.parent = outsideCone.transform;
			MaterialCtrl.setMaterial(insideCone, material);
		}
		MaterialCtrl.setMaterial(outsideCone, material);
		return outsideCone;
	}

	private static GameObject _createCone(int sides, bool hasBottom, bool insideOut) {

		GameObject cone = new GameObject("Cone");

		// create the mesh
		MeshFilter meshFilter = cone.AddComponent<MeshFilter>();
		cone.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		// (e.g. if we want to have 20 sides, we need 20 base points plus one top point)
		Vector3[] vertices = new Vector3[sides + 1];

		// base vertices
		for (int i = 0; i < sides; i++) {
			vertices[i] = new Vector3(Mathf.Sin(2 * Mathf.PI * i / sides) / 2, -1, Mathf.Cos(2 * Mathf.PI * i / sides) / 2);
		}

		// top vertex
		vertices[sides] = new Vector3(0, 1, 0);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles;

		if (hasBottom) {
			// if we have a bottom, we need sides - 2 triangles at the bottom additionally
			triangles = new int[(sides*3 + (sides - 1)*3)];
		} else {
			triangles = new int[(sides*3)];
		}

		int offset = 0;

		if (insideOut) {
			for (int i = 0; i < sides; i++) {
				triangles[(i*3)  ] = i;
				if (i == sides-1) {
					triangles[(i*3)+2] = 0;
				} else {
					triangles[(i*3)+2] = i+1;
				}
				triangles[(i*3)+1] = sides;
			}
			offset += sides * 3;

			if (hasBottom) {
				for (int i = 0; i < sides - 2; i++) {
					triangles[offset+(i*3)  ] = i;
					triangles[offset+(i*3)+2] = sides-1;
					triangles[offset+(i*3)+1] = i+1;
				}
				offset += (sides - 1) * 3;
			}
		} else {
			for (int i = 0; i < sides; i++) {
				triangles[(i*3)  ] = i;
				if (i == sides-1) {
					triangles[(i*3)+1] = 0;
				} else {
					triangles[(i*3)+1] = i+1;
				}
				triangles[(i*3)+2] = sides;
			}
			offset += sides * 3;

			if (hasBottom) {
				for (int i = 0; i < sides - 2; i++) {
					triangles[offset+(i*3)  ] = i;
					triangles[offset+(i*3)+1] = sides-1;
					triangles[offset+(i*3)+2] = i+1;
				}
				offset += (sides - 1) * 3;
			}
		}

		mesh.triangles = triangles;

		MeshFactory.finalizeMesh(mesh);

		return cone;
	}

	/**
	 * Create a cylinder with as many sides as you want (20 is the amount that in-built unity
	 * cylinders use), optionally with a MeshCollider attached (this is a bit more expensive
	 * than the CapsuleCollider that Unity uses, but works much more accurately) - to get one,
	 * set meshColliderSides to a value above 1 (for a value of 0, no mesh collider will be
	 * created)
	 */
	public static GameObject createCylinder(int sides, int meshColliderSides, bool renderInterior, int material) {

		GameObject outsideCylinder = _createCylinder(sides, false);
		if (renderInterior) {
			GameObject insideCylinder = _createCylinder(sides, true);
			insideCylinder.name = "Cylinder (Interior)";
			insideCylinder.transform.parent = outsideCylinder.transform;
			MaterialCtrl.setMaterial(insideCylinder, material);
		}
		MaterialCtrl.setMaterial(outsideCylinder, material);
		if (meshColliderSides > 1) {
			MeshCollider col = outsideCylinder.AddComponent<MeshCollider>();
			Mesh colMesh = new Mesh();
			_createCylinderMesh(colMesh, meshColliderSides, false, false);
			col.sharedMesh = colMesh;
			col.convex = true;
		}
		return outsideCylinder;
	}

	private static GameObject _createCylinder(int sides, bool insideOut) {

		GameObject cylinder = new GameObject("Cylinder");

		// create the mesh
		MeshFilter meshFilter = cylinder.AddComponent<MeshFilter>();
		cylinder.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		_createCylinderMesh(mesh, sides, insideOut, true);

		return cylinder;
	}

	private static void _createCylinderMesh(Mesh mesh, int sides, bool insideOut, bool createUV) {

		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[sides * 2];

		for (int i = 0; i < sides; i++) {
			// base vertices
			vertices[i      ] = new Vector3(Mathf.Sin(2 * Mathf.PI * i / sides) / 2, -1, Mathf.Cos(2 * Mathf.PI * i / sides) / 2);
			// top vertices
			vertices[i+sides] = new Vector3(Mathf.Sin(2 * Mathf.PI * i / sides) / 2,  1, Mathf.Cos(2 * Mathf.PI * i / sides) / 2);
		}

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles;

		triangles = new int[(sides*6) + (2*(sides-1)*3)];

		int offset = 0;

		if (insideOut) {
			// sides
			for (int i = 0; i < sides; i++) {
				triangles[(i*6)  ] = i;
				if (i == sides-1) {
					triangles[(i*6)+2] = 0;
				} else {
					triangles[(i*6)+2] = i+1;
				}
				triangles[(i*6)+1] = sides+i;
				triangles[(i*6)+3] = sides+i;
				if (i == sides-1) {
					triangles[(i*6)+5] = 0;
					triangles[(i*6)+4] = sides;
				} else {
					triangles[(i*6)+5] = i+1;
					triangles[(i*6)+4] = sides+i+1;
				}
			}
			offset += sides * 6;

			// bottom
			for (int i = 0; i < sides - 2; i++) {
				triangles[offset+(i*3)  ] = i;
				triangles[offset+(i*3)+2] = sides-1;
				triangles[offset+(i*3)+1] = i+1;
			}
			offset += (sides - 1) * 3;

			// top
			for (int i = 0; i < sides - 2; i++) {
				triangles[offset+(i*3)  ] = sides+i;
				triangles[offset+(i*3)+1] = sides+sides-1;
				triangles[offset+(i*3)+2] = sides+i+1;
			}
			offset += (sides - 1) * 3;
		} else {
			// sides
			for (int i = 0; i < sides; i++) {
				triangles[(i*6)  ] = i;
				if (i == sides-1) {
					triangles[(i*6)+1] = 0;
				} else {
					triangles[(i*6)+1] = i+1;
				}
				triangles[(i*6)+2] = sides+i;
				triangles[(i*6)+3] = sides+i;
				if (i == sides-1) {
					triangles[(i*6)+4] = 0;
					triangles[(i*6)+5] = sides;
				} else {
					triangles[(i*6)+4] = i+1;
					triangles[(i*6)+5] = sides+i+1;
				}
			}
			offset += sides * 6;

			// bottom
			for (int i = 0; i < sides - 2; i++) {
				triangles[offset+(i*3)  ] = i;
				triangles[offset+(i*3)+1] = sides-1;
				triangles[offset+(i*3)+2] = i+1;
			}
			offset += (sides - 1) * 3;

			// top
			for (int i = 0; i < sides - 2; i++) {
				triangles[offset+(i*3)  ] = sides+i;
				triangles[offset+(i*3)+2] = sides+sides-1;
				triangles[offset+(i*3)+1] = sides+i+1;
			}
			offset += (sides - 1) * 3;
		}

		mesh.triangles = triangles;

		// if wanted: create the UV Vector2 array to be able to also display textures on this mesh
		Vector2[] uv = new Vector2[vertices.Length];
		for (int i = 0; i < vertices.Length; i++) {
			uv[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
		mesh.uv = uv;

		// assign our resulting results
		MeshFactory.finalizeMesh(mesh);
	}

}

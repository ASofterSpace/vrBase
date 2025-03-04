﻿/**
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

	/**
	 * Create a prism with a triangular base, with or without the interior also rendering
	 */
	public static GameObject createTrianglePrism(bool renderInterior, int material) {

		GameObject outsidePrism = _createTriangularPrism(false);
		if (renderInterior) {
			GameObject insidePrism = _createTriangularPrism(true);
			insidePrism.name = "Triangular Prism (Interior)";
			insidePrism.transform.parent = outsidePrism.transform;
			MaterialCtrl.setMaterial(insidePrism, material);
		}
		MaterialCtrl.setMaterial(outsidePrism, material);
		return outsidePrism;
	}

	private static GameObject _createTriangularPrism(bool insideOut) {

		GameObject cone = new GameObject("Triangular Prism");

		// create the mesh
		MeshFilter meshFilter = cone.AddComponent<MeshFilter>();
		cone.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[6];

		vertices[0] = new Vector3( 0.5f, -0.5f,  0.5f);
		vertices[1] = new Vector3(-0.5f, -0.5f,  0.5f);
		vertices[2] = new Vector3( 0   , -0.5f, -0.5f);
		vertices[3] = new Vector3( 0.5f,  0.5f,  0.5f);
		vertices[4] = new Vector3(-0.5f,  0.5f,  0.5f);
		vertices[5] = new Vector3( 0   ,  0.5f, -0.5f);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles;

		triangles = new int[3*8];

		if (!insideOut) {
			triangles[0] = 0;
			triangles[1] = 1;
			triangles[2] = 2;
			triangles[3] = 3;
			triangles[4] = 5;
			triangles[5] = 4;
			triangles[6] = 0;
			triangles[7] = 3;
			triangles[8] = 1;
			triangles[9] = 1;
			triangles[10] = 3;
			triangles[11] = 4;
			triangles[12] = 1;
			triangles[13] = 4;
			triangles[14] = 2;
			triangles[15] = 2;
			triangles[16] = 4;
			triangles[17] = 5;
			triangles[18] = 2;
			triangles[19] = 5;
			triangles[20] = 0;
			triangles[21] = 0;
			triangles[22] = 5;
			triangles[23] = 3;
		} else {
			triangles[0] = 0;
			triangles[1] = 2;
			triangles[2] = 1;
			triangles[3] = 3;
			triangles[4] = 4;
			triangles[5] = 5;
			triangles[6] = 0;
			triangles[7] = 1;
			triangles[8] = 3;
			triangles[9] = 1;
			triangles[10] = 4;
			triangles[11] = 3;
			triangles[12] = 1;
			triangles[13] = 2;
			triangles[14] = 4;
			triangles[15] = 2;
			triangles[16] = 5;
			triangles[17] = 4;
			triangles[18] = 2;
			triangles[19] = 0;
			triangles[20] = 5;
			triangles[21] = 0;
			triangles[22] = 3;
			triangles[23] = 5;
		}

		mesh.triangles = triangles;

		MeshFactory.finalizeMesh(mesh);

		return cone;
	}

	public static GameObject createTaperedCube(bool addLines, bool addPoints, int material, int lineMaterial, int pointMaterial) {

		GameObject outsideCube = _createTaperedCube(false, addLines, addPoints, lineMaterial, pointMaterial);
		MaterialCtrl.setMaterial(outsideCube, material);
		return outsideCube;
	}

	private static GameObject _createTaperedCube(bool insideOut, bool addLines, bool addPoints, int lineMaterial, int pointMaterial) {

		GameObject obj = new GameObject("Tapered Cube");

		// create the mesh
		MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[16];

		vertices[0] = new Vector3(0.5f, -0.5f, 0.5f);
		vertices[1] = new Vector3(0.5f, -0.5f, -0.5f);
		vertices[2] = new Vector3(-0.5f, -0.5f, -0.5f);
		vertices[3] = new Vector3(-0.5f, -0.5f, 0.5f);
		vertices[4] = new Vector3(0.5f, 0.5f, 0.5f);
		vertices[5] = new Vector3(0.5f, 0.5f, -0.5f);
		vertices[6] = new Vector3(-0.5f, 0.5f, -0.5f);
		vertices[7] = new Vector3(-0.5f, 0.5f, 0.5f);
		vertices[8] = new Vector3(0.75f, 0, 0.75f);
		vertices[9] = new Vector3(0.75f, 0, -0.75f);
		vertices[10] = new Vector3(-0.75f, 0, -0.75f);
		vertices[11] = new Vector3(-0.75f, 0, 0.75f);
		vertices[12] = new Vector3(0.75f, 0, 0.75f);
		vertices[13] = new Vector3(0.75f, 0, -0.75f);
		vertices[14] = new Vector3(-0.75f, 0, -0.75f);
		vertices[15] = new Vector3(-0.75f, 0, 0.75f);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = new int[2*2*3 + 2*4*2*3];

		// bottom
		triangles[0] = 0;
		triangles[1] = 2;
		triangles[2] = 1;
		triangles[3] = 2;
		triangles[4] = 0;
		triangles[5] = 3;

		// top
		triangles[6] = 4;
		triangles[7] = 5;
		triangles[8] = 6;
		triangles[9] = 6;
		triangles[10] = 7;
		triangles[11] = 4;

		int i = 12;

		// lower sides
		for (int j = 0; j < 4; j++) {
			triangles[i++] = j;
			if (j == 3) {
				triangles[i++] = 0;
			} else {
				triangles[i++] = 1+j;
			}
			triangles[i++] = 8+j;
			triangles[i++] = 8+j;
			if (j == 3) {
				triangles[i++] = 0;
				triangles[i++] = 8;
			} else {
				triangles[i++] = 1+j;
				triangles[i++] = 9+j;
			}
		}

		// upper sides
		for (int j = 0; j < 4; j++) {
			if (j == 3) {
				triangles[i++] = 4;
			} else {
				triangles[i++] = 5+j;
			}
			triangles[i++] = 4+j;
			triangles[i++] = 8+j;
			if (j == 3) {
				triangles[i++] = 4;
				triangles[i++] = 8+j;
				triangles[i++] = 8;
			} else {
				triangles[i++] = 5+j;
				triangles[i++] = 8+j;
				triangles[i++] = 9+j;
			}
		}




		mesh.triangles = triangles;

		MeshFactory.finalizeMesh(mesh);

		if (addPoints) {
			MeshFactory.drawPoints(obj, vertices, pointMaterial);
		}

		if (addLines) {
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[1], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[2], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[3], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[0], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[5], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[6], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[7], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[4], lineMaterial);

			for (int j = 0; j < 4; j++) {
				MeshFactory.drawRayFromTo(obj, vertices[j], vertices[8+j], lineMaterial);
				MeshFactory.drawRayFromTo(obj, vertices[8+j], vertices[4+j], lineMaterial);
			}
			MeshFactory.drawRayFromTo(obj, vertices[8], vertices[9], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[9], vertices[10], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[10], vertices[11], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[11], vertices[12], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[12], vertices[13], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[13], vertices[14], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[14], vertices[15], lineMaterial);
			MeshFactory.drawRayFromTo(obj, vertices[15], vertices[8], lineMaterial);
		}

		return obj;
	}

}

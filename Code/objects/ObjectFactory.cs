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

		finalizeMesh(mesh);

		return cone;
	}

	public static GameObject createTetrahedron(int material) {

		GameObject outsideTetra = _createTetrahedron(false);
		MaterialCtrl.setMaterial(outsideTetra, material);
		return outsideTetra;
	}

	private static GameObject _createTetrahedron(bool insideOut) {

		GameObject obj = new GameObject("Tetrahedron");

		// create the mesh
		MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[4];

		vertices[0] = new Vector3( Mathf.Sqrt(8f/9), -1f/3,                0);
		vertices[1] = new Vector3(-Mathf.Sqrt(2f/9), -1f/3,  Mathf.Sqrt(2f/3));
		vertices[2] = new Vector3(-Mathf.Sqrt(2f/9), -1f/3, -Mathf.Sqrt(2f/3));
		vertices[3] = new Vector3(                0,    1,                 0);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = new int[4*3];

		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = 2;
		triangles[3] = 1;
		triangles[4] = 3;
		triangles[5] = 2;
		triangles[6] = 0;
		triangles[7] = 2;
		triangles[8] = 3;
		triangles[9] = 0;
		triangles[10] = 3;
		triangles[11] = 1;

		mesh.triangles = triangles;

		finalizeMesh(mesh);

		return obj;
	}

	public static GameObject createCube(int material) {

		GameObject outsideCube = _createCube(false);
		MaterialCtrl.setMaterial(outsideCube, material);
		return outsideCube;
	}

	private static GameObject _createCube(bool insideOut) {

		GameObject obj = new GameObject("Cube");

		// create the mesh
		MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[8];

		vertices[0] = new Vector3(0.5f, -0.5f, 0.5f);
		vertices[1] = new Vector3(0.5f, -0.5f, -0.5f);
		vertices[2] = new Vector3(-0.5f, -0.5f, -0.5f);
		vertices[3] = new Vector3(-0.5f, -0.5f, 0.5f);
		vertices[4] = new Vector3(0.5f, 0.5f, 0.5f);
		vertices[5] = new Vector3(0.5f, 0.5f, -0.5f);
		vertices[6] = new Vector3(-0.5f, 0.5f, -0.5f);
		vertices[7] = new Vector3(-0.5f, 0.5f, 0.5f);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = new int[6*2*3];

		// bottom
		triangles[0] = 0;
		triangles[1] = 2;
		triangles[2] = 1;
		triangles[3] = 2;
		triangles[4] = 0;
		triangles[5] = 3;

		// side 1
		triangles[6] = 3;
		triangles[7] = 6;
		triangles[8] = 2;
		triangles[9] = 6;
		triangles[10] = 3;
		triangles[11] = 7;

		// side 2
		triangles[12] = 2;
		triangles[13] = 5;
		triangles[14] = 1;
		triangles[15] = 5;
		triangles[16] = 2;
		triangles[17] = 6;

		// side 3
		triangles[18] = 1;
		triangles[19] = 4;
		triangles[20] = 0;
		triangles[21] = 4;
		triangles[22] = 1;
		triangles[23] = 5;

		// side 4
		triangles[24] = 0;
		triangles[25] = 7;
		triangles[26] = 3;
		triangles[27] = 7;
		triangles[28] = 0;
		triangles[29] = 4;

		// top
		triangles[30] = 4;
		triangles[31] = 5;
		triangles[32] = 6;
		triangles[33] = 6;
		triangles[34] = 7;
		triangles[35] = 4;

		mesh.triangles = triangles;

		finalizeMesh(mesh);

		return obj;
	}

	public static GameObject createOctahedron(int material) {

		GameObject outsideOcta = _createOctahedron(false);
		MaterialCtrl.setMaterial(outsideOcta, material);
		return outsideOcta;
	}

	private static GameObject _createOctahedron(bool insideOut) {

		GameObject obj = new GameObject("Octahedron");

		// create the mesh
		MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[6];

		vertices[0] = new Vector3(0, -Mathf.Sqrt(2)/2, 0);
		vertices[1] = new Vector3(0.5f, 0, 0.5f);
		vertices[2] = new Vector3(0.5f, 0, -0.5f);
		vertices[3] = new Vector3(-0.5f, 0, -0.5f);
		vertices[4] = new Vector3(-0.5f, 0, 0.5f);
		vertices[5] = new Vector3(0, Mathf.Sqrt(2)/2, 0);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = new int[8*3];

		// bottom half
		triangles[0] = 0;
		triangles[1] = 2;
		triangles[2] = 1;
		triangles[3] = 0;
		triangles[4] = 3;
		triangles[5] = 2;
		triangles[6] = 0;
		triangles[7] = 4;
		triangles[8] = 3;
		triangles[9] = 0;
		triangles[10] = 1;
		triangles[11] = 4;

		// top half
		triangles[12] = 5;
		triangles[13] = 1;
		triangles[14] = 2;
		triangles[15] = 5;
		triangles[16] = 2;
		triangles[17] = 3;
		triangles[18] = 5;
		triangles[19] = 3;
		triangles[20] = 4;
		triangles[21] = 5;
		triangles[22] = 4;
		triangles[23] = 1;

		mesh.triangles = triangles;

		finalizeMesh(mesh);

		return obj;
	}

	public static GameObject createIcosahedron(int material) {

		GameObject outsideIco = _createIcosahedron(false);
		MaterialCtrl.setMaterial(outsideIco, material);
		return outsideIco;
	}

	private static GameObject _createIcosahedron(bool insideOut) {

		GameObject obj = new GameObject("Icosahedron");

		// create the mesh
		MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[12];

		float phi = (1f + Mathf.Sqrt(5)) / 4;

		vertices[0] = new Vector3(0.5f, phi, 0);
		vertices[1] = new Vector3(-0.5f, phi, 0);
		vertices[2] = new Vector3(0,  0.5f, -phi);
		vertices[3] = new Vector3(0,  0.5f, phi);
		vertices[4] = new Vector3(phi, 0, -0.5f);
		vertices[5] = new Vector3(phi, 0, 0.5f);
		vertices[6] = new Vector3(-phi, 0, 0.5f);
		vertices[7] = new Vector3(-phi, 0, -0.5f);
		vertices[8] = new Vector3(0,  -0.5f, -phi);
		vertices[9] = new Vector3(0,  -0.5f, phi);
		vertices[10] = new Vector3(0.5f, -phi, 0);
		vertices[11] = new Vector3(-0.5f, -phi, 0);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = new int[5*4*3];

		triangles[0] = 0;
		triangles[1] = 2;
		triangles[2] = 1;
		triangles[3] = 1;
		triangles[4] = 3;
		triangles[5] = 0;
		triangles[6] = 3;
		triangles[7] = 1;
		triangles[8] = 6;
		triangles[9] = 6;
		triangles[10] = 1;
		triangles[11] = 7;
		triangles[12] = 7;
		triangles[13] = 1;
		triangles[14] = 2;
		triangles[15] = 7;
		triangles[16] = 2;
		triangles[17] = 8;
		triangles[18] = 7;
		triangles[19] = 8;
		triangles[20] = 11;
		triangles[21] = 7;
		triangles[22] = 11;
		triangles[23] = 6;
		triangles[24] = 6;
		triangles[25] = 11;
		triangles[26] = 9;
		triangles[27] = 6;
		triangles[28] = 9;
		triangles[29] = 3;
		triangles[30] = 3;
		triangles[31] = 9;
		triangles[32] = 5;
		triangles[33] = 3;
		triangles[34] = 5;
		triangles[35] = 0;
		triangles[36] = 0;
		triangles[37] = 5;
		triangles[38] = 4;
		triangles[39] = 0;
		triangles[40] = 4;
		triangles[41] = 2;
		triangles[42] = 4;
		triangles[43] = 5;
		triangles[44] = 10;
		triangles[45] = 10;
		triangles[46] = 5;
		triangles[47] = 9;
		triangles[48] = 10;
		triangles[49] = 9;
		triangles[50] = 11;
		triangles[51] = 10;
		triangles[52] = 11;
		triangles[53] = 8;
		triangles[54] = 10;
		triangles[55] = 8;
		triangles[56] = 4;
		triangles[57] = 4;
		triangles[58] = 8;
		triangles[59] = 2;

		mesh.triangles = triangles;

		finalizeMesh(mesh);

		return obj;
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
		finalizeMesh(mesh);
	}

	public static void finalizeMesh(Mesh mesh) {
		mesh.Optimize();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

}

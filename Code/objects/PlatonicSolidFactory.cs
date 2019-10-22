/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A dedicated factory for creating platonic solids...
 * Very similar to the ObjectFactory, but with a clearer purpose ;)
 */
public class PlatonicSolidFactory {

	public static GameObject createTetrahedron(int material) {

		GameObject outsideTetra = _createTetrahedron(false, true);
		MaterialCtrl.setMaterial(outsideTetra, material);
		return outsideTetra;
	}

	private static GameObject _createTetrahedron(bool insideOut, bool addLines) {

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

		MeshFactory.finalizeMesh(mesh);

		if (addLines) {
			MeshFactory.drawPoints(obj, vertices);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[3]);
			/*
			int i = 0;
			LineRenderer line = new GameObject("l" + (i++)).AddComponent<LineRenderer>();
			line.transform.parent = obj.transform;
			line.SetWidth(0.025f, 0.025f);
			line.SetColors(Color.red, Color.green);
			line.SetVertexCount(2);
			line.useWorldSpace = false;
			line.SetPosition(0, obj.transform.position+vertices[0]);
			line.SetPosition(1, obj.transform.position+vertices[1]);
			*/
		}

		return obj;
	}

	public static GameObject createCube(int material) {

		GameObject outsideCube = _createCube(false, true);
		MaterialCtrl.setMaterial(outsideCube, material);
		return outsideCube;
	}

	private static GameObject _createCube(bool insideOut, bool addLines) {

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

		MeshFactory.finalizeMesh(mesh);

		if (addLines) {
			MeshFactory.drawPoints(obj, vertices);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[0]);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[6]);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[7]);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[6]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[7]);
		}

		return obj;
	}

	public static GameObject createOctahedron(int material) {

		GameObject outsideOcta = _createOctahedron(false, true);
		MaterialCtrl.setMaterial(outsideOcta, material);
		return outsideOcta;
	}

	private static GameObject _createOctahedron(bool insideOut, bool addLines) {

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

		MeshFactory.finalizeMesh(mesh);

		if (addLines) {
			MeshFactory.drawPoints(obj, vertices);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[1]);
		}

		return obj;
	}

	public static GameObject createIcosahedron(int material) {

		GameObject outsideIco = _createIcosahedron(false, true);
		MaterialCtrl.setMaterial(outsideIco, material);
		return outsideIco;
	}

	private static GameObject _createIcosahedron(bool insideOut, bool addLines) {

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

		MeshFactory.finalizeMesh(mesh);

		if (addLines) {
			MeshFactory.drawPoints(obj, vertices);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[0]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[6]);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[3]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[7]);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[7]);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[11]);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[11]);
			MeshFactory.drawRayFromTo(obj, vertices[8], vertices[11]);
			MeshFactory.drawRayFromTo(obj, vertices[9], vertices[11]);
			MeshFactory.drawRayFromTo(obj, vertices[10], vertices[11]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[10]);
			MeshFactory.drawRayFromTo(obj, vertices[5], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[10], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[4], vertices[10]);
			MeshFactory.drawRayFromTo(obj, vertices[8], vertices[10]);
		}

		return obj;
	}

	public static GameObject createDodecahedron(int material) {

		GameObject outsideDode = _createDodecahedron(false, true);
		MaterialCtrl.setMaterial(outsideDode, material);
		return outsideDode;
	}

	private static GameObject _createDodecahedron(bool insideOut, bool addLines) {

		GameObject obj = new GameObject("Dodecahedron");

		// create the mesh
		MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		Vector3[] vertices = new Vector3[20];

		float phi = (1f + Mathf.Sqrt(5)) / 4;
		float invphi = 1 / (1f + Mathf.Sqrt(5));

		vertices[0]  = new Vector3(0,  phi,  invphi);
		vertices[1]  = new Vector3(0,  phi, -invphi);
		vertices[2]  = new Vector3( 0.5f,  0.5f,  0.5f);
		vertices[3]  = new Vector3( 0.5f,  0.5f, -0.5f);
		vertices[4]  = new Vector3(-0.5f,  0.5f, -0.5f);
		vertices[5]  = new Vector3(-0.5f,  0.5f,  0.5f);
		vertices[6]  = new Vector3( phi, invphi, 0);
		vertices[7]  = new Vector3(-phi, invphi, 0);
		vertices[8]  = new Vector3( invphi, 0,  phi);
		vertices[9]  = new Vector3( invphi, 0, -phi);
		vertices[10] = new Vector3(-invphi, 0, -phi);
		vertices[11] = new Vector3(-invphi, 0,  phi);
		vertices[12] = new Vector3( phi, -invphi, 0);
		vertices[13] = new Vector3(-phi, -invphi, 0);
		vertices[14] = new Vector3( 0.5f, -0.5f,  0.5f);
		vertices[15] = new Vector3( 0.5f, -0.5f, -0.5f);
		vertices[16] = new Vector3(-0.5f, -0.5f, -0.5f);
		vertices[17] = new Vector3(-0.5f, -0.5f,  0.5f);
		vertices[18] = new Vector3(0, -phi,  invphi);
		vertices[19] = new Vector3(0, -phi, -invphi);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = new int[12*3*3];

		triangles[0] = 1;
		triangles[1] = 5;
		triangles[2] = 0;
		triangles[3] = 1;
		triangles[4] = 7;
		triangles[5] = 5;
		triangles[6] = 1;
		triangles[7] = 4;
		triangles[8] = 7;

		triangles[9] = 0;
		triangles[10] = 5;
		triangles[11] = 11;
		triangles[12] = 0;
		triangles[13] = 11;
		triangles[14] = 8;
		triangles[15] = 0;
		triangles[16] = 8;
		triangles[17] = 2;

		triangles[18] = 0;
		triangles[19] = 2;
		triangles[20] = 6;
		triangles[21] = 0;
		triangles[22] = 6;
		triangles[23] = 3;
		triangles[24] = 0;
		triangles[25] = 3;
		triangles[26] = 1;

		triangles[27] = 1;
		triangles[28] = 3;
		triangles[29] = 9;
		triangles[30] = 1;
		triangles[31] = 9;
		triangles[32] = 10;
		triangles[33] = 1;
		triangles[34] = 10;
		triangles[35] = 4;

		triangles[36] = 4;
		triangles[37] = 10;
		triangles[38] = 16;
		triangles[39] = 4;
		triangles[40] = 16;
		triangles[41] = 13;
		triangles[42] = 4;
		triangles[43] = 13;
		triangles[44] = 7;

		triangles[45] = 10;
		triangles[46] = 19;
		triangles[47] = 16;
		triangles[48] = 10;
		triangles[49] = 15;
		triangles[50] = 19;
		triangles[51] = 10;
		triangles[52] = 9;
		triangles[53] = 15;

		triangles[54] = 3;
		triangles[55] = 15;
		triangles[56] = 9;
		triangles[57] = 3;
		triangles[58] = 12;
		triangles[59] = 15;
		triangles[60] = 3;
		triangles[61] = 6;
		triangles[62] = 12;

		triangles[63] = 2;
		triangles[64] = 12;
		triangles[65] = 6;
		triangles[66] = 2;
		triangles[67] = 14;
		triangles[68] = 12;
		triangles[69] = 2;
		triangles[70] = 8;
		triangles[71] = 14;

		triangles[72] = 8;
		triangles[73] = 18;
		triangles[74] = 14;
		triangles[75] = 8;
		triangles[76] = 17;
		triangles[77] = 18;
		triangles[78] = 8;
		triangles[79] = 11;
		triangles[80] = 17;

		triangles[81] = 5;
		triangles[82] = 17;
		triangles[83] = 11;
		triangles[84] = 5;
		triangles[85] = 13;
		triangles[86] = 17;
		triangles[87] = 5;
		triangles[88] = 7;
		triangles[89] = 13;

		triangles[90] = 13;
		triangles[91] = 16;
		triangles[92] = 19;
		triangles[93] = 13;
		triangles[94] = 19;
		triangles[95] = 18;
		triangles[96] = 13;
		triangles[97] = 18;
		triangles[98] = 17;

		triangles[99] = 18;
		triangles[100] = 19;
		triangles[101] = 15;
		triangles[102] = 18;
		triangles[103] = 15;
		triangles[104] = 12;
		triangles[105] = 18;
		triangles[106] = 12;
		triangles[107] = 14;

		mesh.triangles = triangles;

		MeshFactory.finalizeMesh(mesh);

		if (addLines) {
			MeshFactory.drawPoints(obj, vertices);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[2]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[6]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[6]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[1]);
			MeshFactory.drawRayFromTo(obj, vertices[0], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[7], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[1], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[10], vertices[4]);
			MeshFactory.drawRayFromTo(obj, vertices[10], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[3], vertices[9]);
			MeshFactory.drawRayFromTo(obj, vertices[2], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[11], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[11], vertices[5]);
			MeshFactory.drawRayFromTo(obj, vertices[11], vertices[17]);
			MeshFactory.drawRayFromTo(obj, vertices[13], vertices[17]);
			MeshFactory.drawRayFromTo(obj, vertices[13], vertices[7]);
			MeshFactory.drawRayFromTo(obj, vertices[10], vertices[16]);
			MeshFactory.drawRayFromTo(obj, vertices[13], vertices[16]);
			MeshFactory.drawRayFromTo(obj, vertices[6], vertices[12]);
			MeshFactory.drawRayFromTo(obj, vertices[14], vertices[12]);
			MeshFactory.drawRayFromTo(obj, vertices[14], vertices[8]);
			MeshFactory.drawRayFromTo(obj, vertices[17], vertices[18]);
			MeshFactory.drawRayFromTo(obj, vertices[14], vertices[18]);
			MeshFactory.drawRayFromTo(obj, vertices[19], vertices[18]);
			MeshFactory.drawRayFromTo(obj, vertices[19], vertices[15]);
			MeshFactory.drawRayFromTo(obj, vertices[9], vertices[15]);
			MeshFactory.drawRayFromTo(obj, vertices[19], vertices[16]);
			MeshFactory.drawRayFromTo(obj, vertices[12], vertices[15]);
		}

		return obj;
	}
}

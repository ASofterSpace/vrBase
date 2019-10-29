/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class ArcadeRoomCtrl : PrettyDome2Ctrl {

	private FlipperQnDCtrl flipperQnDCtrl;

	private BowlingAlleyCtrl bowlingAlleyCtrl;

	private TicTacToeCtrl ticTacToeCtrl;

	private BlobFlyerCtrl blobFlyerCtrl;


	public ArcadeRoomCtrl(MainCtrl mainCtrl, GameObject thisRoom) : base(mainCtrl, thisRoom) {

		createRoom();
	}

	protected override void createRoom() {

		createFloor();

		createBeams();

		createMeshedWall();

		createDoors();

		createObjects();
	}

	protected override void createBeams() {

		base.createBeams();

		GameObject curBeam;
		int curAngle = 90;

		// make room for the door to the control room
		GameObject.Destroy(beams[248]);
		GameObject.Destroy(beams[223]);
		GameObject.Destroy(beams[224]);
		GameObject.Destroy(beams[198]);
		GameObject.Destroy(beams[199]);
		GameObject.Destroy(beams[173]);
		GameObject.Destroy(beams[174]);
		GameObject.Destroy(spheres[224]);
		GameObject.Destroy(spheres[199]);
		GameObject.Destroy(spheres[174]);

		// add new beams to each side of the purple door
		connectBeam(new Vector3(-1.492112f, 0.024f, 5.811457f), new Vector3(-0.39f, 0.01f, 5.87f));
		connectBeam(new Vector3(-1.484048f, 0.624f, 5.779985f), new Vector3(-0.731f, 0.61f, 5.87f));
		connectBeam(new Vector3(-1.460754f, 1.224f, 5.68929f), new Vector3(-0.74f, 1.21f, 5.907f));
		connectBeam(new Vector3(-1.421505f, 1.824f, 5.536457f), new Vector3(-0.728f, 1.81f, 5.89f));
		connectBeam(new Vector3(1.425289f, 1.776f, 5.55107f), new Vector3(0.762f, 1.778f, 5.902f));
		connectBeam(new Vector3(1.463206f, 1.176f, 5.698776f), new Vector3(0.761f, 1.178f, 5.93f));
		connectBeam(new Vector3(1.485249f, 0.576f, 5.784657f), new Vector3(0.76f, 0.578f, 5.89f));
	}

	protected override GameObject createMeshedWallSide() {

		// create the mesh
		GameObject meshWall = new GameObject("meshWall");
		MeshFilter meshFilter = meshWall.AddComponent<MeshFilter>();
		meshWall.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		int i = 0;
		Vector3[] vertices = new Vector3[300];

		// lowest level
		int loStart = 226;
		for (int j = 0; j < 24; j++) {
			Vector3 v = levelListi[loStart + j];
			vertices[i++] = v;
			vertices[i++] = new Vector3(v.x, 0, v.z);
		}
		// middle level
		loStart = 201;
		int hiStart = 226;
		for (int j = 0; j < 24; j++) {
			vertices[i++] = levelListi[loStart + j];
			vertices[i++] = levelListi[hiStart + j];
		}
		// highest level
		loStart = 176;
		hiStart = 201;
		for (int j = 0; j < 24; j++) {
			vertices[i++] = levelListi[loStart + j];
			vertices[i++] = levelListi[hiStart + j];
		}

		// arcade-room-specific - left side
		vertices[i++] = new Vector3(-1.492112f, 0.024f, 5.811457f);
		vertices[i++] = new Vector3(-0.731f, 0, 5.87f);
		vertices[i++] = new Vector3(-1.484048f, 0.624f, 5.779985f);
		vertices[i++] = new Vector3(-0.731f, 0.61f, 5.87f);
		vertices[i++] = new Vector3(-1.460754f, 1.224f, 5.68929f);
		vertices[i++] = new Vector3(-0.74f, 1.21f, 5.907f);
		vertices[i++] = new Vector3(-0.39f, 0.01f, 5.87f);
		vertices[i++] = new Vector3(-0.731f, 0.305f, 5.87f);

		// arcade-room-specific - right side
		vertices[i++] = new Vector3(0.761f, 0, 5.93f);
		vertices[i++] = new Vector3(1.485249f, 0, 5.784657f);
		vertices[i++] = new Vector3(0.761f, 0.576f, 5.93f);
		vertices[i++] = new Vector3(1.485249f, 0.576f, 5.784657f);
		vertices[i++] = new Vector3(0.761f, 1.178f, 5.93f);
		vertices[i++] = new Vector3(1.463206f, 1.176f, 5.698776f);
		vertices[i++] = new Vector3(0.762f, 1.778f, 5.902f);
		vertices[i++] = new Vector3(1.425289f, 1.776f, 5.55107f);
		vertices[i++] = new Vector3(0.76f, 0.283f, 5.89f);
		vertices[i++] = new Vector3(0.401f, 0, 5.886f);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = createMeshedWallTriangles();

		mesh.triangles = triangles;

		MeshFactory.finalizeMesh(mesh);

		return meshWall;
	}

	protected override int[] createMeshedWallTriangles() {

		int[] triangles = new int[900];
		int i = 0;

		for (int j = 0; j < 23; j++) {
			i = addTriangleWallBlock(triangles, i, j*2);
		}
		for (int j = 0; j < 23; j++) {
			i = addTriangleWallBlock(triangles, i, 48+j*2);
		}
		for (int j = 0; j < 23; j++) {
			i = addTriangleWallBlock(triangles, i, 96+j*2);
		}

		// arcade-room-specific - left side
		i = addTriangleWallBlock(triangles, i, 144);
		i = addTriangleWallBlock(triangles, i, 146);
		i = addTriangle(triangles, i, 145, 151, 150);

		// arcade-room-specific - right side
		i = addTriangleWallBlock(triangles, i, 152);
		i = addTriangleWallBlock(triangles, i, 154);
		i = addTriangleWallBlock(triangles, i, 156);
		i = addTriangle(triangles, i, 153, 161, 160);

		return triangles;
	}

	// we add each triangle twice, once inwards facing, once outwards facing
	protected int addTriangle(int[] triangles, int i, int a, int b, int c) {
		if (insideOutWallMesh) {
			triangles[i]   = a;
			triangles[i+1] = b;
			triangles[i+2] = c;
		} else {
			triangles[i]   = a;
			triangles[i+1] = c;
			triangles[i+2] = b;
		}
		return i + 3;
	}

	/**
	 * i is the index into triangles that we are currently at
	 * start is the index into vertices that we are currently at
	 */
	protected int addTriangleWallBlock(int[] triangles, int i, int start) {
		i = addTriangle(triangles, i, start, start + 3, start + 1);
		i = addTriangle(triangles, i, start, start + 2, start + 3);
		return i;
	}

	private void createDoors() {

		createDoor(0, 5.89f);
	}

	private void createObjects() {

		flipperQnDCtrl = new FlipperQnDCtrl(
			mainCtrl, thisRoom, new Vector3(2.92f, 0, 3.16f), new Vector3(0, -127, 0)
		);

		bowlingAlleyCtrl = new BowlingAlleyCtrl(
			mainCtrl, thisRoom, new Vector3(-2.95f, 0, -0.71f), new Vector3(0, -168.4f, 0)
		);

		ticTacToeCtrl = new TicTacToeCtrl(
			mainCtrl, thisRoom, new Vector3(2.32f, 0, -2.61f), new Vector3(0, -7.385f, 0)
		);

		/*
		blobFlyerCtrl = new BlobFlyerCtrl(
			mainCtrl, thisRoom, new Vector3(-3.2f, 0, 1), new Vector3(0, 100, 0));
		*/

		GameObject poster = createPoster(MaterialCtrl.OBJECTS_POSTERS_FLIPPERQND);
		poster.name = "FlipperQnD Poster";
		poster.transform.localPosition = new Vector3(5.563f, 1.355f, 1.654f);
		poster.transform.localEulerAngles = new Vector3(-12.95f, -284.716f, -2.452f);
	}

}

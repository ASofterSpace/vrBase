/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class ControlRoomCtrl : GenericRoomCtrl {

	private NostalgicConsoleCtrl nostalgicConsoleCtrl;

	private DioramaCtrl dioramaCtrl;

	private RocketLaunchCtrl rocketLaunchCtrl;

	private RobotCuddleCtrl robotCuddleCtrl;

	private RobotFarmCtrl robotFarmCtrl;


	public ControlRoomCtrl(MainCtrl mainCtrl, GameObject thisRoom) : base(mainCtrl, thisRoom) {

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

		// to Arcade Room:
		// make room for the purple door
		GameObject.Destroy(beams[21]); // remove cross beam
		GameObject.Destroy(beams[37]); // remove floor beam
		GameObject.Destroy(beams[65]); // remove head beam
		// add two new floor beams to each side of the purple door
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-2.5f, 0, -5);
		curBeam.transform.localEulerAngles = new Vector3(90, 0, 90);
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-4.45f, 0, -5);
		curBeam.transform.localEulerAngles = new Vector3(90, 0, 90);
		// add upper beams around the purple door
		curBeam = createBeam(0.4479682f);
		curBeam.transform.localPosition = new Vector3(-2.379f, 0.98f, -4.886f);
		curBeam.transform.localEulerAngles = new Vector3(70.35201f, -108.364f, -1.268f);

		// to Science Room:
		// make room for the purple door
		GameObject.Destroy(beams[29]); // remove cross beam
		GameObject.Destroy(beams[45]); // remove floor beam
		GameObject.Destroy(beams[69]); // remove head beam
	}

	protected override int getAdditionalWallVertexAmount() {
		return 12;
	}

	protected override void createAdditionalWallVertices(Vector3[] vertices, int i) {
		float x = thisRoom.transform.position.x;
		float y = thisRoom.transform.position.y;
		float z = thisRoom.transform.position.z;

		// create the wall around the door - to the left of the door...
		vertices[i++] = new Vector3(x-2.05f, y, z-5);
		vertices[i++] = new Vector3(x-1.85f, y+0.825f, z-4.88f);
		vertices[i++] = new Vector3(x+0.74f-3.5f, y+1.15f, z-5);
		vertices[i++] = new Vector3(x+0.74f-3.5f, y, z-5);
		vertices[i++] = new Vector3(x+0.74f-3.5f, y+0.3f, z-5);
		vertices[i++] = new Vector3(x+0.4f-3.5f, y, z-5);

		// ... and to the right of the door
		vertices[i++] = new Vector3(x-0.74f-3.5f, y+1.65f, z-5);
		vertices[i++] = new Vector3(x-4.6f, y+1.85f, z-4.6f);
		vertices[i++] = new Vector3(x-5f, y, z-5);
		vertices[i++] = new Vector3(x-0.74f-3.5f, y, z-5);
		vertices[i++] = new Vector3(x-0.74f-3.5f, y+0.3f, z-5);
		vertices[i++] = new Vector3(x-0.4f-3.5f, y, z-5);
	}

	protected override int[] createMeshedWallTriangles() {

		// 6 full wall blocks (each 6*4),
		// two half wall blocks (each 6*4/2),
		// one wall around the door
		// (each call to addTriangle requires 6 points, each call to addTriangleWallBlock four times that many)
		int[] triangles = new int[6*4*6 + 6*2*2 + 6*6];
		int i = 0;

		// block 1 - North
		i = addTriangleWallBlock(triangles, i, 0);
		// block 2 - South
		i = addTriangleWallBlock(triangles, i, 6);
		// block 3 - East
		i = addTriangleWallBlock(triangles, i, 12);
		// block 4 - West
		i = addTriangleWallBlock(triangles, i, 18);
		// block 5 - North-East
		i = addTriangleWallBlock(triangles, i, 24);
		// block 6 - South-East
		i = addTriangleWallBlock(triangles, i, 30);
		// block 7 - South-West
//		i = addTriangleWallBlock(triangles, i, 36);
		i = addTriangle(triangles, i, 36, 36 + 1, 36 + 2);
		i = addTriangle(triangles, i, 36, 36 + 2, 36 + 3);
		// block 8 - North-West
		i = addTriangle(triangles, i, 42, 42 + 1, 42 + 2);
		i = addTriangle(triangles, i, 42, 42 + 2, 42 + 3);

		// block 9 - wall around the door
		i = addTriangle(triangles, i, 48, 48 + 1, 48 + 2);
		i = addTriangle(triangles, i, 48, 48 + 2, 48 + 3);
		i = addTriangle(triangles, i, 48 + 3, 48 + 4, 48 + 5);

		i = addTriangle(triangles, i, 54, 54 + 1, 54 + 2);
		i = addTriangle(triangles, i, 54, 54 + 2, 54 + 3);
		i = addTriangle(triangles, i, 54 + 4, 54 + 3, 54 + 5);

		return triangles;
	}

	private void createDoors() {

		GameObject doorToArcadeRoom = createDoor(-3.5f, -5.0f);

		GameObject doorToScienceRoom = createDoor(5, -3.5f);
		doorToScienceRoom.transform.localEulerAngles = new Vector3(0, 90, 0);
	}

	private void createObjects() {

		createTank("waterTank", 4.53f, -6.91f);
		createTank("heliumTank", 2.67f, -7.88f);

		nostalgicConsoleCtrl = new NostalgicConsoleCtrl(
			mainCtrl, thisRoom, new Vector3(-2.57f, 0, 2.73f), new Vector3(0, -50, 0));

		dioramaCtrl = new DioramaCtrl(
			mainCtrl, thisRoom, new Vector3(-0.25f, 0, -3.48f), new Vector3(0, 0, 0));

		rocketLaunchCtrl = new RocketLaunchCtrl(
			mainCtrl, thisRoom, nostalgicConsoleCtrl, new Vector3(-3, 0, 13.6f), new Vector3(0, 170, 0));

		robotCuddleCtrl = new RobotCuddleCtrl(
			mainCtrl, thisRoom, new Vector3(4.04f, 0, 1.48f), new Vector3(0, 65, 0));

		robotFarmCtrl = new RobotFarmCtrl(
			mainCtrl, thisRoom, new Vector3(-18, 0, 3.45f), new Vector3(0, -33, 0));
	}

}

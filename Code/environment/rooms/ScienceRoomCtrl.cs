/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class ScienceRoomCtrl : GenericRoomCtrl {

	private GameObject thisRoomInterior;

	private MathWorldCtrl mathWorldCtrl;


	public ScienceRoomCtrl(MainCtrl mainCtrl, GameObject thisRoom) : base(mainCtrl, thisRoom) {

		thisRoomInterior = new GameObject("Science Room Interior");
		thisRoomInterior.transform.parent = thisRoom.transform;

		createRoom();

		float minify = 0.8f;
		float normalify = 1 / minify;

		thisRoom.transform.localScale = new Vector3(minify, minify, minify);

		thisRoomInterior.transform.localPosition = new Vector3(0, 0, 0);
		thisRoomInterior.transform.localEulerAngles = new Vector3(0, 0, 0);
		thisRoomInterior.transform.localScale = new Vector3(normalify, normalify, normalify);
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

		// make room for the purple door
		GameObject.Destroy(beams[31]); // remove cross beam
		GameObject.Destroy(beams[47]); // remove floor beam
		GameObject.Destroy(beams[71]); // remove head beam
/*
		// add two new floor beams to each side of the purple door
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-2.5f, 0, -5);
		curBeam.transform.localEulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-4.45f, 0, -5);
		curBeam.transform.localEulerAngles = new Vector3(90, 0, curAngle);
*/
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

		// 7 full wall blocks (each 6*4),
		// one half wall block (so 6*4/2),
		// one wall around the door
		// (each call to addTriangle requires 6 points, each call to addTriangleWallBlock four times that many)
		int[] triangles = new int[6*4*7 + 6*2 + 6*6];
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
		i = addTriangle(triangles, i, 24, 24 + 1, 24 + 2);
		i = addTriangle(triangles, i, 24, 24 + 2, 24 + 3);
		// block 6 - South-East
		i = addTriangleWallBlock(triangles, i, 30);
		// block 7 - South-West
		i = addTriangleWallBlock(triangles, i, 36);
		// block 8 - North-West
		i = addTriangleWallBlock(triangles, i, 42);

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

		GameObject door = createDoor(-5, 3.39f);
		door.transform.localEulerAngles = new Vector3(0, 90, 0);
		door.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
	}

	private void createObjects() {

		mathWorldCtrl = new MathWorldCtrl(
			mainCtrl, thisRoomInterior, new Vector3(2.3f, 0, -2.5f), new Vector3(0, -50, 0)
		);
	}

}

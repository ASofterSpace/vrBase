﻿/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class ControlRoomCtrl : GenericRoomCtrl {

	public ControlRoomCtrl(MainCtrl mainCtrl, GameObject thisRoom) : base(mainCtrl, thisRoom) {

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

		// make room for the purple door
		GameObject.Destroy(beams[21]); // remove cross beam
		GameObject.Destroy(beams[37]); // remove floor beam
		GameObject.Destroy(beams[65]); // remove head beam
		// add two new floor beams to each side of the purple door
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-2.5f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-4.45f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
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
		i = addTriangleWallBlock(triangles, i, 24);
		// block 6 - South-East
		i = addTriangleWallBlock(triangles, i, 30);
		// block 7 - South-West
		i = addTriangleWallBlock(triangles, i, 36);
		// block 8 - North-West
		i = addTriangle(triangles, i, 42, 42 + 1, 42 + 4);
		i = addTriangle(triangles, i, 42, 42 + 4, 42 + 5);

		// block 9 - wall around the door
		i = addTriangle(triangles, i, 48, 48 + 1, 48 + 2);
		i = addTriangle(triangles, i, 48, 48 + 2, 48 + 3);
		i = addTriangle(triangles, i, 48 + 3, 48 + 4, 48 + 5);

		i = addTriangle(triangles, i, 54, 54 + 1, 54 + 2);
		i = addTriangle(triangles, i, 54, 54 + 2, 54 + 3);
		i = addTriangle(triangles, i, 54 + 3, 54 + 4, 54 + 5);

		return triangles;
	}

	private void createDoors() {

		createDoor(-3.5f, -5.0f);
	}

	private void createObjects() {

		createTank("waterTank", 7, -6);
		createTank("heliumTank", 8, -5);

		createNostalgicConsole();

		createBowlingAlley();

		createBlobFlyer();
	}

	private void createNostalgicConsole() {

		// TODO
	}

	private void createBowlingAlley() {

		GameObject bowlingAlley = new GameObject("Bowling Alley");
		bowlingAlley.transform.parent = thisRoom.transform;
		bowlingAlley.transform.localPosition = new Vector3(3.5f, 0, 0);

		GameObject bowlingFloor = GameObject.CreatePrimitive(PrimitiveType.Quad);
		bowlingFloor.name = TeleportCtrl.FLOOR_NAME;
		bowlingFloor.transform.parent = bowlingAlley.transform;
		bowlingFloor.transform.localPosition = new Vector3(0, 0.001f, 0);
		bowlingFloor.transform.eulerAngles = new Vector3(90, 0, 0);
		bowlingFloor.transform.localScale = new Vector3(1, 6, 1);
		MaterialCtrl.setMaterial(bowlingFloor, MaterialCtrl.BUILDING_FLOOR_WOOD);

		GameObject bowlingBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		bowlingBall.name = "redBowlingBall";
		bowlingBall.transform.parent = bowlingAlley.transform;
		bowlingBall.transform.localPosition = new Vector3(0, 0.1f, -3);
		bowlingBall.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		MaterialCtrl.setMaterial(bowlingBall, MaterialCtrl.OBJECTS_BOWLING_BALL_RED);

		GameObject pin = createBowlingPin();
		pin.transform.parent = bowlingAlley.transform;
		pin.transform.localPosition = new Vector3(0, 0, 2);
	}

	private GameObject createBowlingPin() {

		GameObject pin = new GameObject("bowlingPin");

		GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		capsule.transform.parent = pin.transform;
		capsule.transform.localPosition = new Vector3(0, 0.0719f, 0);
		capsule.transform.localScale = new Vector3(0.05f, 0.08f, 0.05f);
		MaterialCtrl.setMaterial(capsule, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject neck = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		neck.transform.parent = pin.transform;
		neck.transform.localPosition = new Vector3(0, 0.1631f, 0);
		neck.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		MaterialCtrl.setMaterial(neck, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		head.transform.parent = pin.transform;
		head.transform.localPosition = new Vector3(0, 0.1961f, 0);
		head.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
		MaterialCtrl.setMaterial(head, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject redBand = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		redBand.transform.parent = pin.transform;
		redBand.transform.localPosition = new Vector3(0, 0.1009f, 0);
		redBand.transform.localScale = new Vector3(0.051f, 0.01f, 0.051f);
		MaterialCtrl.setMaterial(redBand, MaterialCtrl.OBJECTS_BOWLING_PIN_RED);

		return pin;
	}

	private void createBlobFlyer() {

		GameObject blobFlyer = new GameObject("BlobFlyer");
		blobFlyer.transform.parent = thisRoom.transform;
		blobFlyer.transform.localPosition = new Vector3(0, 0, -4);

		GameObject seat = new GameObject("Seat");
		seat.transform.parent = blobFlyer.transform;
		seat.transform.localPosition = new Vector3(0, 0, 0);

		GameObject cushion = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cushion.transform.parent = seat.transform;
		cushion.transform.localPosition = new Vector3(0, 0.3f, 0);
		cushion.transform.eulerAngles = new Vector3(0, 0, 0);
		cushion.transform.localScale = new Vector3(0.5f, 0.05f, 0.4f);
		MaterialCtrl.setMaterial(cushion, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject back = GameObject.CreatePrimitive(PrimitiveType.Cube);
		back.transform.parent = seat.transform;
		back.transform.localPosition = new Vector3(0, 0.5f, 0.22f);
		back.transform.eulerAngles = new Vector3(10, 0, 0);
		back.transform.localScale = new Vector3(0.5f, 0.5f, 0.05f);
		MaterialCtrl.setMaterial(back, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject stand = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		stand.transform.parent = seat.transform;
		stand.transform.localPosition = new Vector3(0, 0.1f, 0);
		stand.transform.eulerAngles = new Vector3(0, 0, 0);
		stand.transform.localScale = new Vector3(0.4f, 0.18f, 0.4f);
		MaterialCtrl.setMaterial(stand, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject console = new GameObject("Console");
		console.transform.parent = blobFlyer.transform;
		console.transform.localPosition = new Vector3(0, 0, -1);

		GameObject mainKeyboard = GameObject.CreatePrimitive(PrimitiveType.Cube);
		mainKeyboard.transform.parent = console.transform;
		mainKeyboard.transform.localPosition = new Vector3(0, 0.5f, 0);
		mainKeyboard.transform.eulerAngles = new Vector3(60, 0, 0);
		mainKeyboard.transform.localScale = new Vector3(0.8f, 0.1f, 0.8f);
		MaterialCtrl.setMaterial(mainKeyboard, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject chassis = new GameObject("Chassis");
		chassis.transform.parent = blobFlyer.transform;
		chassis.transform.localPosition = new Vector3(0, 0, 0);
	}
}

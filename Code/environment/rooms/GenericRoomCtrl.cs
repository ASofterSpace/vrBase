using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public abstract class GenericRoomCtrl {

	protected MainCtrl mainCtrl;
	protected MaterialCtrl materialCtrl;
	protected string roomName;
	protected GameObject thisRoom;
	protected GameObject[] beams;
	private int curBeamNum;


	public GenericRoomCtrl(MainCtrl mainCtrl, GameObject thisRoom) {

		this.mainCtrl = mainCtrl;

		this.materialCtrl = mainCtrl.getMaterialCtrl();

		this.thisRoom = thisRoom;

		this.roomName = thisRoom.name;

		// let's have more slots available than should be necessary:
		// we create 48 for a standard room here, but special rooms
		// (such as the control room) add extra beams afterwards
		this.beams = new GameObject[200];

		createRoom();
	}

	protected abstract void createRoom();

	protected GameObject createPrimitive(PrimitiveType type) {
		GameObject result = GameObject.CreatePrimitive(type);
		result.transform.parent = thisRoom.transform;
		return result;
	}

	protected virtual void createFloor() {

		GameObject floor = createPrimitive(PrimitiveType.Quad);
		floor.name = TeleportCtrl.FLOOR_NAME;
		floor.transform.localPosition = new Vector3(0, 0, 0);
		floor.transform.eulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(10, 10, 1);
		materialCtrl.setMaterial(floor, MaterialCtrl.BUILDING_FLOOR_CONCRETE);

		GameObject floor2 = createPrimitive(PrimitiveType.Quad);
		floor2.name = TeleportCtrl.FLOOR_NAME;
		floor2.transform.localPosition = new Vector3(0, -0.001f, 0);
		floor2.transform.eulerAngles = new Vector3(90, 45, 0);
		floor2.transform.localScale = new Vector3(10, 10, 1);
		materialCtrl.setMaterial(floor2, MaterialCtrl.BUILDING_FLOOR_CONCRETE);
	}

	protected virtual void createBeams() {

		int curAngle = -45;
		curBeamNum = 0;

		GameObject curBeam;

		// create lowest level:
		// 1) alternatingly a tall and a little vertically standing beam,
		// 2) a diagonal-ish crossbeam,
		// 3) and finally a beam on the floor,
		// all of this done for each of the 16 sides

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(-6.7f, 0.9f, 0);
		curBeam.transform.eulerAngles = new Vector3(0, 0, -20);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-5.56f, 1.32f, 0.98f);
		curBeam.transform.eulerAngles = new Vector3(-68, 50, -10);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-6, 0, 1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(-4.88f, 0.4f, 2);
		curBeam.transform.eulerAngles = new Vector3(-5, 0, -15);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-4.7f, 1.33f, 3.3f);
		curBeam.transform.eulerAngles = new Vector3(50, -58.5f, 125);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-5, 0, 3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(-4.815f, 0.9f, 4.815f);
		curBeam.transform.eulerAngles = new Vector3(-10.8f, 0, -10.8f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-3.3f, 1.33f, 4.7f);
		curBeam.transform.eulerAngles = new Vector3(-66, 58.5f, 26.5f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-3.5f, 0, 5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(-2f, 0.4f, 4.88f);
		curBeam.transform.eulerAngles = new Vector3(-15, 0, -5);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-0.983f, 1.319f, 5.563f);
		curBeam.transform.eulerAngles = new Vector3(68.175f, 53, 1.923f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-1, 0, 6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(0, 0.9f, 6.7f);
		curBeam.transform.eulerAngles = new Vector3(-20, 0, 0);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(0.983f, 1.319f, 5.563f);
		curBeam.transform.eulerAngles = new Vector3(68.175f, -53, -1.923f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(1, 0, 6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(2, 0.4f, 4.88f);
		curBeam.transform.eulerAngles = new Vector3(-15, 0, 5);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(3.3f, 1.33f, 4.7f);
		curBeam.transform.eulerAngles = new Vector3(-66, -58.5f, -26.5f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(3.5f, 0, 5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(4.815f, 0.9f, 4.815f);
		curBeam.transform.eulerAngles = new Vector3(-10.8f, 0, 10.8f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(4.7f, 1.33f, 3.3f);
		curBeam.transform.eulerAngles = new Vector3(50, 58.5f, -125);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(5, 0, 3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(4.88f, 0.4f, -2);
		curBeam.transform.eulerAngles = new Vector3(5, 0, 15);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(5.56f, 1.32f, 0.98f);
		curBeam.transform.eulerAngles = new Vector3(-68, -50, 10);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(6, 0, 1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(6.7f, 0.9f, 0);
		curBeam.transform.eulerAngles = new Vector3(0, 0, 20);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(5.56f, 1.32f, -0.98f);
		curBeam.transform.eulerAngles = new Vector3(68, 50, 10);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(6, 0, -1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(4.88f, 0.4f, 2);
		curBeam.transform.eulerAngles = new Vector3(-5, 0, 15);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(4.7f, 1.33f, -3.3f);
		curBeam.transform.eulerAngles = new Vector3(-50, -58.5f, -125);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(5, 0, -3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(4.815f, 0.9f, -4.815f);
		curBeam.transform.eulerAngles = new Vector3(10.8f, 0, 10.8f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(3.3f, 1.33f, -4.7f);
		curBeam.transform.eulerAngles = new Vector3(66, 58.5f, -26.5f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(3.5f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(2, 0.4f, -4.88f);
		curBeam.transform.eulerAngles = new Vector3(15, 0, 5);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(0.983f, 1.319f, -5.563f);
		curBeam.transform.eulerAngles = new Vector3(-68.175f, 53, -1.923f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(1, 0, -6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(0, 0.9f, -6.7f);
		curBeam.transform.eulerAngles = new Vector3(20, 0, 0);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-0.983f, 1.319f, -5.563f);
		curBeam.transform.eulerAngles = new Vector3(-68.175f, -53, 1.923f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-1, 0, -6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(-2, 0.4f, -4.88f);
		curBeam.transform.eulerAngles = new Vector3(15, 0, -5);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-3.3f, 1.33f, -4.7f);
		curBeam.transform.eulerAngles = new Vector3(66, -58.5f, 26.5f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-3.5f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(-4.815f, 0.9f, -4.815f);
		curBeam.transform.eulerAngles = new Vector3(10.8f, 0, -10.8f);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-4.7f, 1.33f, -3.3f);
		curBeam.transform.eulerAngles = new Vector3(-50, 58.5f, 125);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-5, 0, -3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(-4.88f, 0.4f, -2);
		curBeam.transform.eulerAngles = new Vector3(5, 0, -15);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-5.56f, 1.32f, -0.98f);
		curBeam.transform.eulerAngles = new Vector3(68, -50, -10);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-6, 0, -1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		// create second level:
		// 2 V-shaped diagonal beams on top of each of the short vertical ones
		// 2 very wide V-shaped diagonal beams on top of each other of the long vertical ones

		float curY = 1.55f;
		float curDistL1 = 4.87f;
		float curDistL12 = 4.9f;
		float curDistS1 = 1.7f;
		float curDistL2 = 4.684f;
		float curDistL22 = 4.68f;
		float curDistS2 = 2.266f;
		float curDistS22 = 2.225f;
		float curLen = 0.8f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(-20, 20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL2, 1.597f, curDistS2);
		curBeam.transform.eulerAngles = new Vector3(20, 20, 0);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS22, curY, curDistL22);
		curBeam.transform.eulerAngles = new Vector3(0, -20, 20);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS1, curY, curDistL12);
		curBeam.transform.eulerAngles = new Vector3(0, -20, -20);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS1, curY, curDistL12);
		curBeam.transform.eulerAngles = new Vector3(0, 20, 20);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS22, curY, curDistL22);
		curBeam.transform.eulerAngles = new Vector3(0, 20, -20);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL2, 1.597f, curDistS2);
		curBeam.transform.eulerAngles = new Vector3(20, -20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(-20, -20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(20, 20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL2, 1.597f, -curDistS2);
		curBeam.transform.eulerAngles = new Vector3(-20, 20, 0);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS22, curY, -curDistL22);
		curBeam.transform.eulerAngles = new Vector3(0, -20, -20);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS1, curY, -curDistL12);
		curBeam.transform.eulerAngles = new Vector3(0, -20, 20);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS1, curY, -curDistL12);
		curBeam.transform.eulerAngles = new Vector3(0, 20, -20);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS22, curY, -curDistL22);
		curBeam.transform.eulerAngles = new Vector3(0, 20, 20);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL2, 1.597f, -curDistS2);
		curBeam.transform.eulerAngles = new Vector3(-20, -20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(20, -20, 0);

		curY = 2.094f;
		float curY2 = 2.077f;
		curDistL1 = 4.607f;
		curDistL2 = 4.612f;
		curDistS1 = 3.588f;
		curDistS2 = 3.549f;
		curLen = 1.1f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(59.803f, -116.362f, 60.538f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS2, curY2, curDistL2);
		curBeam.transform.eulerAngles = new Vector3(-71.008f, -134.677f, 45.078f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS2, curY2, curDistL2);
		curBeam.transform.eulerAngles = new Vector3(-71.008f, 134.677f, -45.078f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(59.803f, -116.362f, 60.538f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(-59.803f, 116.362f, 60.538f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS2, curY2, -curDistL2);
		curBeam.transform.eulerAngles = new Vector3(71.008f, -134.677f, -45.078f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS2, curY2, -curDistL2);
		curBeam.transform.eulerAngles = new Vector3(71.008f, 134.677f, 45.078f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(-59.803f, 116.362f, 60.538f);

		// create third level:
		// short horizontal beams between the 2V-shaped ones on top of the short vertical ones

		curY = 2.325f;
		curDistL1 = 4.78f;
		curDistS1 = 1.97f;
		curLen = 0.56f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(0, 72, -92.5f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(0, -72, -92.5f);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS1, curY, -curDistL1);
		curBeam.transform.eulerAngles = new Vector3(92.5f, 0, -68);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS1, curY, -curDistL1);
		curBeam.transform.eulerAngles = new Vector3(92.5f, 0, 68);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(0, 72, 92.5f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(0, -72, 92.5f);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS1, curY, curDistL1);
		curBeam.transform.eulerAngles = new Vector3(92.5f, 0, -68);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS1, curY, curDistL1);
		curBeam.transform.eulerAngles = new Vector3(92.5f, 0, 68);

		// create fourth level:
		// long beams upwards from the long ones of the 1st level,
		// inverted-V-beams upwards from the triangles from the 2nd and 3rd level

		curY = 2.54f;
		float curDistL = 5.65f;
		curLen = 1.0f;

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, 0);
		curBeam.transform.eulerAngles = new Vector3(0, 0, 45);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(0, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(45, 0, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, 0);
		curBeam.transform.eulerAngles = new Vector3(0, 0, -45);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(0, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(-45, 0, 0);

		curY = 2.77f;
		curDistL = 4.97f;
		float curDistS = 0.72f;
		curLen = 0.9f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(-13, 10, 57);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(-13, -10, -57);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(-57, -13, 10);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(57, 13, 10);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(13, 10, -57);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(13, -10, 57);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(57, 13, 10);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(-57, -13, 10);

		curY = 3.6f;
		curDistL = 3.63f;
		curDistS = 0.46f;
		curLen = 1.46f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(50, 0, 67);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(-50, 0, 67);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(68.97f, -29.221f, -46.068f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(68.97f, 29.221f, 46.068f);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(-50, 0, -67);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(50, 0, -67);

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(-68.97f, -29.221f, 46.068f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(-68.97f, 29.221f, -46.068f);

		// create mid level:
		// long beams connecting the 2nd/3rd-level triangles with the 3rd-highest-level long beams

		curY = 3.27f;
		curDistL1 = 3.72f;
		curDistL2 = 3.76f;
		curDistS1 = 2.67f;
		curDistS2 = 2.7f;
		curLen = 1.28f;

		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS1, curY, curDistL1);
		curBeam.transform.eulerAngles = new Vector3(-42, -32, 13);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL2, curY, curDistS2);
		curBeam.transform.eulerAngles = new Vector3(38, -42, 25);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL2, curY, -curDistS2);
		curBeam.transform.eulerAngles = new Vector3(-38, 42, 25);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS1, curY, -curDistL1);
		curBeam.transform.eulerAngles = new Vector3(42, 32, 13);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS1, curY, -curDistL1);
		curBeam.transform.eulerAngles = new Vector3(42, -32, -13);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL2, curY, -curDistS2);
		curBeam.transform.eulerAngles = new Vector3(-38, -42, -25);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL2, curY, curDistS2);
		curBeam.transform.eulerAngles = new Vector3(38, 42, -25);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS1, curY, curDistL1);
		curBeam.transform.eulerAngles = new Vector3(-42, 32, -13);

		// create third-highest level:
		// 8 diagonal beams that sprout from 4 points,
		// then 4 horizontal beams that connect always 2 each of these 8 beams,
		// then 8 horizontal beams to the left and right of those,
		// then 4 long downwards beams between them

		curY = 4.62f;
		curDistL = 1.85f;
		curDistS = 0.45f;
		curLen = 0.9f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(45, 45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(-45, -45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(-45, -45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(-45, 45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(45, -45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(-45, 45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(45, 45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(45, -45, 0);

		curY = 4;
		float curDist = 2.3f;
		curLen = 0.9f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(0, curY, curDist);
		curBeam.transform.eulerAngles = new Vector3(0, 0, 90);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDist, curY);
		curBeam.transform.eulerAngles = new Vector3(90, 0, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(0, curY, -curDist);
		curBeam.transform.eulerAngles = new Vector3(0, 0, 90);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDist, curY);
		curBeam.transform.eulerAngles = new Vector3(90, 0, 0);

		curY = 4.083f;
		curDistL = 2.593f;
		curDistS = 1.875f;
		curLen = 1.0444f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(83.777f, 1.847f, 19.293f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(81.32201f, -30.069f, 43.058f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(81.32201f, 30.069f, -43.058f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(83.777f, -1.847f, -19.293f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(-83.777f, 1.847f, -19.293f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(-81.32201f, -30.069f, -43.058f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(-81.32201f, 30.069f, 43.058f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(-83.777f, -1.847f, 19.293f);

		curY = 4.52f;
		curDist = 2.0f;
		curLen = 1.3f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDist, curY, -curDist);
		curBeam.transform.eulerAngles = new Vector3(75, -45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDist, curY, -curDist);
		curBeam.transform.eulerAngles = new Vector3(75, 45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDist, curY, curDist);
		curBeam.transform.eulerAngles = new Vector3(-75, -45, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDist, curY, curDist);
		curBeam.transform.eulerAngles = new Vector3(-75, 45, 0);

		// create second-highest level: 8 horizontal beams that form the basis of highest level

		curY = 5.05f;
		curDistL = 1.25f;
		curDistS = 0.55f;
		curLen = 0.6f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(-40, 0, 65);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, -curDistL);
		curBeam.transform.eulerAngles = new Vector3(-40, 0, -65);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(70, 0, 15);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(-70, 0, 15);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(40, 0, -65);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistS, curY, curDistL);
		curBeam.transform.eulerAngles = new Vector3(40, 0, 65);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, -curDistS);
		curBeam.transform.eulerAngles = new Vector3(70, 0, -15);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL, curY, curDistS);
		curBeam.transform.eulerAngles = new Vector3(-70, 0, -15);

		// create highest level: 8 beams that come together in a single point

		curY = 5.5f;
		curDist = 0.7f;
		curLen = 0.75f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(0, curY, -curDist);
		curBeam.transform.eulerAngles = new Vector3(70, 0, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDist, curY, 0);
		curBeam.transform.eulerAngles = new Vector3(70, -90, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(0, curY, curDist);
		curBeam.transform.eulerAngles = new Vector3(-70, 0, 180);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDist, curY, 0);
		curBeam.transform.eulerAngles = new Vector3(-70, -90, 0);

		curY = 5.3f;
		curDist = 0.55f;
		curLen = 0.9f;
		curAngle = 45;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDist, curY, -curDist);
		curBeam.transform.eulerAngles = new Vector3(60, curAngle, 0);
		curAngle += 90;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDist, curY, curDist);
		curBeam.transform.eulerAngles = new Vector3(60, curAngle, 0);
		curAngle += 90;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDist, curY, curDist);
		curBeam.transform.eulerAngles = new Vector3(60, curAngle, 0);
		curAngle += 90;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDist, curY, -curDist);
		curBeam.transform.eulerAngles = new Vector3(60, curAngle, 0);
	}

	protected GameObject createBeam(float length) {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.name = "beam" + curBeamNum;
		curBeam.transform.localScale = new Vector3(0.1f, length, 0.1f);
		materialCtrl.setMaterial(curBeam, MaterialCtrl.PLASTIC_WHITE);
		beams[curBeamNum++] = curBeam;
		return curBeam;
	}

	protected void createMeshedWall() {

		// get the position of the parent explicitly, to add it to all points,
		// as we seem to specify the mesh in world coordinates...
		// TODO :: in case we want to rotate the parent object, actually figure
		// out how to specify a mesh in local coordinates!
		float x = thisRoom.transform.position.x;
		float y = thisRoom.transform.position.y;
		float z = thisRoom.transform.position.z;

		// create the mesh
		GameObject meshWall = new GameObject("meshWall");
		meshWall.transform.parent = thisRoom.transform;
		meshWall.AddComponent<MeshFilter>();
		meshWall.AddComponent<MeshRenderer>();
		Mesh mesh = meshWall.GetComponent<MeshFilter>().mesh;
		materialCtrl.setMaterial(meshWall, MaterialCtrl.PLASTIC_WHITE);
		mesh.Clear();

		// create vertices that are available to create the mesh
		int i = 0;
		Vector3[] vertices = new Vector3[6*8 + getAdditionalWallVertexAmount()];

		// vertices for the lowest level
		// block 1 - North
		vertices[i++] = new Vector3(-7.05f, 0, 0);
		vertices[i++] = new Vector3(-6.4f, 1.84f, 0);
		vertices[i++] = new Vector3(-4.8f, 0.8f, 1.95f);
		vertices[i++] = new Vector3(-5, 0, 2);
		vertices[i++] = new Vector3(-4.8f, 0.8f, -1.95f);
		vertices[i++] = new Vector3(-5, 0, -2);

		// blocks 2, 3, 4 - South, East, West
		i = createWallBlockVertices(vertices, 6, i, x, y, z);

		// block 5 - North-East
		vertices[i++] = new Vector3(-5.02f, 0, 5.02f);
		vertices[i++] = new Vector3(-4.65f, 1.84f, 4.65f);
		vertices[i++] = new Vector3(-4.8f, 0.8f, 1.95f);
		vertices[i++] = new Vector3(-5, 0, 2);
		vertices[i++] = new Vector3(-1.95f, 0.8f, 4.8f);
		vertices[i++] = new Vector3(-2, 0, 5);

		// blocks 6, 7, 8 - South-East, South-West, North-West
		i = createWallBlockVertices45(vertices, 6, i, x, y, z);

		createAdditionalWallVertices(vertices, i);

		mesh.vertices = vertices;

		/*
		// add some color!
		Vector2[] uv = new Vector2[vertices.Length];
		for (i = 0; i < vertices.Length;) {
			uv[i++] = new Vector2(0, 0);
			uv[i++] = new Vector2(0, 1);
			uv[i++] = new Vector2(1, 1);
		}
		mesh.uv = uv;
		*/

		// create triangles using the previously set vertices
		int[] triangles = createMeshedWallTriangles();

		mesh.triangles = triangles;

		// assign our resulting results
		mesh.RecalculateNormals();
	}

	protected virtual int getAdditionalWallVertexAmount() {
		return 0;
	}

	protected virtual void createAdditionalWallVertices(Vector3[] vertices, int i) {
	}

	protected virtual int[] createMeshedWallTriangles() {

		int[] triangles = new int[6*4*8];
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
		i = addTriangleWallBlock(triangles, i, 42);

		return triangles;
	}

	// we add each triangle twice, once inwards facing, once outwards facing
	protected int addTriangle(int[] triangles, int i, int a, int b, int c) {
		triangles[i]   = a;
		triangles[i+1] = b;
		triangles[i+2] = c;
		triangles[i+3] = a;
		triangles[i+4] = c;
		triangles[i+5] = b;
		return i + 6;
	}

	/**
	 * i is the index into triangles that we are currently at
	 * start is the index into vertices that we are currently at
	 */
	protected int addTriangleWallBlock(int[] triangles, int i, int start) {
		i = addTriangle(triangles, i, start, start + 1, start + 2);
		i = addTriangle(triangles, i, start, start + 2, start + 3);
		i = addTriangle(triangles, i, start, start + 1, start + 4);
		i = addTriangle(triangles, i, start, start + 4, start + 5);
		return i;
	}

	protected int createWallBlockVertices(Vector3[] vertices, int howManyVertices, int i, float x, float y, float z) {
		int startI = i;

		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.x, y + orig.y, z + orig.z);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x + orig.z, y + orig.y, z + orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x + orig.z, y + orig.y, z - orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			int origIndex = j + startI - howManyVertices;
			Vector3 orig = vertices[origIndex];
			vertices[origIndex] = new Vector3(x + orig.x, y + orig.y, z + orig.z);
		}

		return i;
	}

	protected int createWallBlockVertices45(Vector3[] vertices, int howManyVertices, int i, float x, float y, float z) {
		int startI = i;

		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x + orig.z, y + orig.y, z - orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.x, y + orig.y, z - orig.z);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.z, y + orig.y, z + orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			int origIndex = j + startI - howManyVertices;
			Vector3 orig = vertices[origIndex];
			vertices[origIndex] = new Vector3(x + orig.x, y + orig.y, z + orig.z);
		}

		return i;
	}

	protected GameObject createDoor(float x, float z) {

		GameObject door = new GameObject("door");
		door.transform.parent = thisRoom.transform;
		door.transform.localPosition = new Vector3(x, 0, z);

		GameObject bottomRightBeam = createDoorBeam(door, 0.29f);
		bottomRightBeam.transform.localPosition = new Vector3(-0.54f, 0.15f, 0);
		bottomRightBeam.transform.eulerAngles = new Vector3(0, 0, 45);

		GameObject rightBeam = createDoorBeam(door, 0.77f);
		rightBeam.transform.localPosition = new Vector3(-0.74f, 1.08f, 0);
		rightBeam.transform.eulerAngles = new Vector3(0, 0, 0);

		GameObject topRightBeam = createDoorBeam(door, 0.28f);
		topRightBeam.transform.localPosition = new Vector3(-0.545f, 2.02f, 0);
		topRightBeam.transform.eulerAngles = new Vector3(0, 0, -45);

		GameObject topBeam = createDoorBeam(door, 0.4f);
		topBeam.transform.localPosition = new Vector3(0, 2.2f, 0);
		topBeam.transform.eulerAngles = new Vector3(0, 0, 90);

		GameObject topLeftBeam = createDoorBeam(door, 0.28f);
		topLeftBeam.transform.localPosition = new Vector3(0.545f, 2.02f, 0);
		topLeftBeam.transform.eulerAngles = new Vector3(0, 0, 45);

		GameObject leftBeam = createDoorBeam(door, 0.77f);
		leftBeam.transform.localPosition = new Vector3(0.74f, 1.08f, 0);
		leftBeam.transform.eulerAngles = new Vector3(0, 0, 0);

		GameObject bottomLeftBeam = createDoorBeam(door, 0.29f);
		bottomLeftBeam.transform.localPosition = new Vector3(0.54f, 0.15f, 0);
		bottomLeftBeam.transform.eulerAngles = new Vector3(0, 0, -45);

		return door;
	}

	private GameObject createDoorBeam(GameObject door, float length) {
		GameObject result = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		result.transform.parent = door.transform;
		result.transform.localScale = new Vector3(0.15f, length, 0.15f);
		materialCtrl.setMaterial(result, MaterialCtrl.PLASTIC_PURPLE);
		return result;
	}

	protected void createTank(String name, int x, int z) {

		GameObject tank = new GameObject(name);
		tank.transform.parent = thisRoom.transform;

		GameObject tankMain = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		tankMain.transform.parent = tank.transform;
		tankMain.transform.localPosition = new Vector3(x, 0.9f, z);
		tankMain.transform.localScale = new Vector3(1, 1, 1);
		materialCtrl.setMaterial(tankMain, MaterialCtrl.PLASTIC_WHITE);

		GameObject tankFoot = createTankFoot(tank);
		tankFoot.transform.eulerAngles = new Vector3(0, 0, -30);
		tankFoot.transform.localPosition = new Vector3(x - 0.4f, 0, z);
		tankFoot = createTankFoot(tank);
		tankFoot.transform.eulerAngles = new Vector3(0, 0, 30);
		tankFoot.transform.localPosition = new Vector3(x + 0.4f, 0, z);
		tankFoot = createTankFoot(tank);
		tankFoot.transform.eulerAngles = new Vector3(-30, 0, 0);
		tankFoot.transform.localPosition = new Vector3(x, 0, z + 0.4f);
		tankFoot = createTankFoot(tank);
		tankFoot.transform.eulerAngles = new Vector3(30, 0, 0);
		tankFoot.transform.localPosition = new Vector3(x, 0, z - 0.4f);
	}

	protected GameObject createTankFoot(GameObject tank) {
		GameObject tankFoot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		tankFoot.transform.parent = tank.transform;
		tankFoot.transform.localScale = new Vector3(0.1f, 0.15f, 0.1f);
		materialCtrl.setMaterial(tankFoot, MaterialCtrl.PLASTIC_WHITE);
		return tankFoot;
	}

}

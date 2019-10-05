using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public abstract class GenericRoomCtrl : MonoBehaviour
{
	protected MainCtrl mainCtrl;
	protected MaterialCtrl materialCtrl;
	protected string roomName;
	protected GameObject thisRoom;
	protected GameObject[] beams;
	private int curBeamNum;


	public void init(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;

		this.materialCtrl = mainCtrl.getMaterialCtrl();

		this.roomName = this.gameObject.name;

		this.thisRoom = this.gameObject;

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
		curBeam.transform.localPosition = new Vector3(-4.8f, 0.9f, 4.8f);
		curBeam.transform.eulerAngles = new Vector3(-10, 0, -10);
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
		curBeam.transform.localPosition = new Vector3(-0.98f, 1.32f, 5.6f);
		curBeam.transform.eulerAngles = new Vector3(68, 50, 1);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-1, 0, 6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(0, 0.9f, 6.7f);
		curBeam.transform.eulerAngles = new Vector3(-20, 0, 0);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(0.98f, 1.32f, 5.6f);
		curBeam.transform.eulerAngles = new Vector3(68, -50, 1);
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
		curBeam.transform.localPosition = new Vector3(4.8f, 0.9f, 4.8f);
		curBeam.transform.eulerAngles = new Vector3(-10, 0, 10);
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
		curBeam.transform.localPosition = new Vector3(4.8f, 0.9f, -4.8f);
		curBeam.transform.eulerAngles = new Vector3(10, 0, 10);
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
		curBeam.transform.localPosition = new Vector3(0.98f, 1.32f, -5.6f);
		curBeam.transform.eulerAngles = new Vector3(-68, 50, 1);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(1, 0, -6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(0, 0.9f, -6.7f);
		curBeam.transform.eulerAngles = new Vector3(20, 0, 0);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-0.98f, 1.32f, -5.6f);
		curBeam.transform.eulerAngles = new Vector3(-68, -50, 1);
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
		curBeam.transform.localPosition = new Vector3(-4.8f, 0.9f, -4.8f);
		curBeam.transform.eulerAngles = new Vector3(10, 0, -10);
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

		float curY = 1.55f;
		float curDistL1 = 4.87f;
		float curDistL12 = 4.9f;
		float curDistS1 = 1.7f;
		float curDistL2 = 4.69f;
		float curDistL22 = 4.68f;
		float curDistS2 = 2.25f;
		float curDistS22 = 2.225f;
		float curLen = 0.8f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(-20, 20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL2, curY, curDistS2);
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
		curBeam.transform.localPosition = new Vector3(curDistL2, curY, curDistS2);
		curBeam.transform.eulerAngles = new Vector3(20, -20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(-20, -20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(20, 20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL2, curY, -curDistS2);
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
		curBeam.transform.localPosition = new Vector3(-curDistL2, curY, -curDistS2);
		curBeam.transform.eulerAngles = new Vector3(-20, -20, 0);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(-curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(20, -20, 0);

		// create third level:
		// short horizontal beams between the 2V-shaped ones on top of the short vertical ones

		curY = 2.325f;
		curDistL1 = 4.78f;
		curDistS1 = 1.97f;
		curLen = 0.56f;
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, curDistS1);
		curBeam.transform.eulerAngles = new Vector3(0, 72, 92.5f);
		curBeam = createBeam(curLen);
		curBeam.transform.localPosition = new Vector3(curDistL1, curY, -curDistS1);
		curBeam.transform.eulerAngles = new Vector3(0, -72, 92.5f);

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

	protected void createFloor() {

		GameObject floor = createPrimitive(PrimitiveType.Quad);
		floor.transform.localPosition = new Vector3(0, 0, 0);
		floor.transform.eulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(10, 10, 1);
		materialCtrl.setMaterial(floor, MaterialCtrl.BUILDING_FLOOR_CONCRETE);

		GameObject floor2 = createPrimitive(PrimitiveType.Quad);
		floor2.transform.localPosition = new Vector3(0, -0.01f, 0);
		floor2.transform.eulerAngles = new Vector3(90, 45, 0);
		floor2.transform.localScale = new Vector3(10, 10, 1);
		materialCtrl.setMaterial(floor2, MaterialCtrl.BUILDING_FLOOR_CONCRETE);
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

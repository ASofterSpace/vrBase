﻿using System.Collections.Generic;
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
		this.beams = new GameObject[100];

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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
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

		curBeam = createBeam(0.4f);
		curBeam.transform.localPosition = new Vector3(-4.88f, 0.4f, -2);
		curBeam.transform.eulerAngles = new Vector3(5, 0, -15);
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-5.56f, 1.32f, -0.98f);
		curBeam.transform.eulerAngles = new Vector3(68, -50, -10);
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-6, 0, -1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		// create next level
		// TODO

		// create third-highest level: 8 diagonal beams that sprout from 4 points,
		// then 4 horizontal beams that connect always 2 each of these 8 beams,
		// then 4 long downwards beams between them

		float curY = 4.62f;
		float curDistL = 1.85f;
		float curDistS = 0.45f;
		float curLen = 0.9f;
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

		curY = 4.64f;
		curDist = 1.7f;
		curLen = 0.9f;
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
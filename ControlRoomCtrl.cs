/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class ControlRoomCtrl : MonoBehaviour
{
	private MainCtrl mainCtrl;
	private MaterialCtrl materialCtrl;
	private String roomName;
	private GameObject thisRoom;


	void Start()
	{

	}

	void Update()
	{

	}

	public void init(MainCtrl mainCtrl) {
		this.mainCtrl = mainCtrl;
		this.materialCtrl = mainCtrl.getMaterialCtrl();
		this.roomName = this.gameObject.name;
		this.thisRoom = this.gameObject;

		createFloor();

		createBeams();

		createDoors();

		createObjects();
	}

	private GameObject createPrimitive(PrimitiveType type) {
		GameObject result = GameObject.CreatePrimitive(type);
		result.transform.parent = thisRoom.transform;
		return result;
	}

	private void createFloor() {

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

	private void createBeams() {

		int curAngle = -45;

		GameObject curBeam;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-6.7f, 0.9f, 0);
		curBeam.transform.eulerAngles = new Vector3(0, 0, -20);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(-5.56f, 1.32f, 0.98f);
		curBeam.transform.eulerAngles = new Vector3(-68, 50, -10);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-6, 0, 1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-4.88f, 0.4f, 2);
		curBeam.transform.eulerAngles = new Vector3(-5, 0, -15);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(-4.7f, 1.33f, 3.3f);
		curBeam.transform.eulerAngles = new Vector3(50, -58.5f, 125);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-5, 0, 3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-4.8f, 0.9f, 4.8f);
		curBeam.transform.eulerAngles = new Vector3(-10, 0, -10);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(-3.3f, 1.33f, 4.7f);
		curBeam.transform.eulerAngles = new Vector3(-66, 58.5f, 26.5f);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-3.5f, 0, 5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-2f, 0.4f, 4.88f);
		curBeam.transform.eulerAngles = new Vector3(-15, 0, -5);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(-0.98f, 1.32f, 5.6f);
		curBeam.transform.eulerAngles = new Vector3(68, 50, 1);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-1, 0, 6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(0, 0.9f, 6.7f);
		curBeam.transform.eulerAngles = new Vector3(-20, 0, 0);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(0.98f, 1.32f, 5.6f);
		curBeam.transform.eulerAngles = new Vector3(68, -50, 1);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(1, 0, 6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(2, 0.4f, 4.88f);
		curBeam.transform.eulerAngles = new Vector3(-15, 0, 5);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(3.3f, 1.33f, 4.7f);
		curBeam.transform.eulerAngles = new Vector3(-66, -58.5f, -26.5f);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(3.5f, 0, 5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(4.8f, 0.9f, 4.8f);
		curBeam.transform.eulerAngles = new Vector3(-10, 0, 10);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(4.7f, 1.33f, 3.3f);
		curBeam.transform.eulerAngles = new Vector3(50, 58.5f, -125);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(5, 0, 3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(4.88f, 0.4f, -2);
		curBeam.transform.eulerAngles = new Vector3(5, 0, 15);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(5.56f, 1.32f, 0.98f);
		curBeam.transform.eulerAngles = new Vector3(-68, -50, 10);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(6, 0, 1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(6.7f, 0.9f, 0);
		curBeam.transform.eulerAngles = new Vector3(0, 0, 20);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(5.56f, 1.32f, -0.98f);
		curBeam.transform.eulerAngles = new Vector3(68, 50, 10);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(6, 0, -1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(4.88f, 0.4f, 2);
		curBeam.transform.eulerAngles = new Vector3(-5, 0, 15);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(4.7f, 1.33f, -3.3f);
		curBeam.transform.eulerAngles = new Vector3(-50, -58.5f, -125);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(5, 0, -3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(4.8f, 0.9f, -4.8f);
		curBeam.transform.eulerAngles = new Vector3(10, 0, 10);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(3.3f, 1.33f, -4.7f);
		curBeam.transform.eulerAngles = new Vector3(66, 58.5f, -26.5f);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(3.5f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(2, 0.4f, -4.88f);
		curBeam.transform.eulerAngles = new Vector3(15, 0, 5);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(0.98f, 1.32f, -5.6f);
		curBeam.transform.eulerAngles = new Vector3(-68, 50, 1);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(1, 0, -6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(0, 0.9f, -6.7f);
		curBeam.transform.eulerAngles = new Vector3(20, 0, 0);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(-0.98f, 1.32f, -5.6f);
		curBeam.transform.eulerAngles = new Vector3(-68, -50, 1);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-1, 0, -6);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-2, 0.4f, -4.88f);
		curBeam.transform.eulerAngles = new Vector3(15, 0, -5);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(-3.3f, 1.33f, -4.7f);
		curBeam.transform.eulerAngles = new Vector3(66, -58.5f, 26.5f);
		// this one has the purple door
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-2.5f, 0, -5);
		curBeam.transform.localScale = new Vector3(0.1f, 0.45f, 0.1f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-4.4f, 0, -5);
		curBeam.transform.localScale = new Vector3(0.1f, 0.55f, 0.1f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;

		curBeam = createLowVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-4.8f, 0.9f, -4.8f);
		curBeam.transform.eulerAngles = new Vector3(10, 0, -10);
		curBeam = createLowCrossBeam(true);
		curBeam.transform.localPosition = new Vector3(-4.7f, 1.33f, -3.3f);
		curBeam.transform.eulerAngles = new Vector3(-50, 58.5f, 125);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-5, 0, -3.5f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle += 45;

		curBeam = createLowLittleVerticalBeam();
		curBeam.transform.localPosition = new Vector3(-4.88f, 0.4f, -2);
		curBeam.transform.eulerAngles = new Vector3(5, 0, -15);
		curBeam = createLowCrossBeam(false);
		curBeam.transform.localPosition = new Vector3(-5.56f, 1.32f, -0.98f);
		curBeam.transform.eulerAngles = new Vector3(68, -50, -10);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-6, 0, -1);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curAngle -= 90;
	}

	private GameObject createFloorBeam() {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.transform.localScale = new Vector3(0.1f, 1.45f, 0.1f);
		materialCtrl.setMaterial(curBeam, MaterialCtrl.PLASTIC_WHITE);
		return curBeam;
	}

	private GameObject createLowVerticalBeam() {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.transform.localScale = new Vector3(0.1f, 1.0f, 0.1f);
		materialCtrl.setMaterial(curBeam, MaterialCtrl.PLASTIC_WHITE);
		return curBeam;
	}

	private GameObject createLowLittleVerticalBeam() {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.transform.localScale = new Vector3(0.1f, 0.4f, 0.1f);
		materialCtrl.setMaterial(curBeam, MaterialCtrl.PLASTIC_WHITE);
		return curBeam;
	}

	private GameObject createLowCrossBeam(bool longBeam) {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		if (longBeam) {
			curBeam.transform.localScale = new Vector3(0.1f, 1.45f, 0.1f);
		} else {
			curBeam.transform.localScale = new Vector3(0.1f, 1.37f, 0.1f);
		}
		materialCtrl.setMaterial(curBeam, MaterialCtrl.PLASTIC_WHITE);
		return curBeam;
	}

	private void createDoors() {

		// TODO
	}

	private void createObjects() {

		createTank("waterTank", 7, -6);
		createTank("heliumTank", 8, -5);

		createNostalgicConsole();

		createBowlingAlley();

		createBlobFlyer();
	}

	private void createTank(String name, int x, int z) {

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

	private GameObject createTankFoot(GameObject tank) {
		GameObject tankFoot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		tankFoot.transform.parent = tank.transform;
		tankFoot.transform.localScale = new Vector3(0.1f, 0.15f, 0.1f);
		materialCtrl.setMaterial(tankFoot, MaterialCtrl.PLASTIC_WHITE);
		return tankFoot;
	}

	private void createNostalgicConsole() {

		// TODO
	}

	private void createBowlingAlley() {

		// TODO
	}

	private void createBlobFlyer() {

		// TODO
	}
}

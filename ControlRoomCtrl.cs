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

		GameObject floorBeam1 = createFloorBeam();
		floorBeam1.transform.localPosition = new Vector3(-6, 0, 1);
		floorBeam1.transform.eulerAngles = new Vector3(90, 0, -45);

		GameObject floorBeam2 = createFloorBeam();
		floorBeam2.transform.localPosition = new Vector3(-5, 0, 3.5f);
		floorBeam2.transform.eulerAngles = new Vector3(90, 0, 0);

		GameObject floorBeam3 = createFloorBeam();
		floorBeam3.transform.localPosition = new Vector3(-3.5f, 0, 5);
		floorBeam3.transform.eulerAngles = new Vector3(90, 0, -90);

		GameObject floorBeam4 = createFloorBeam();
		floorBeam4.transform.localPosition = new Vector3(-1, 0, 6);
		floorBeam4.transform.eulerAngles = new Vector3(90, 0, -45);

		GameObject floorBeam5 = createFloorBeam();
		floorBeam5.transform.localPosition = new Vector3(1, 0, 6);
		floorBeam5.transform.eulerAngles = new Vector3(90, 0, -135);
	}

	private GameObject createFloorBeam() {

		GameObject floorBeam = createPrimitive(PrimitiveType.Cylinder);
		floorBeam.transform.localScale = new Vector3(0.1f, 1.45f, 0.1f);
		materialCtrl.setMaterial(floorBeam, MaterialCtrl.PLASTIC_WHITE);
		return floorBeam;
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

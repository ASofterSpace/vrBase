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
		this.roomName = this.gameObject.name;
		this.thisRoom = this.gameObject;

		createFloor();

		createBeams();

		createDoors();

		createObjects();
	}

	public void createFloor() {

		GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Quad);
		floor.transform.parent = thisRoom.transform;
		floor.transform.localPosition = new Vector3(0, 0, 0);
		floor.transform.eulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(10, 10, 1);
		// TODO :: set material

		GameObject floor2 = GameObject.CreatePrimitive(PrimitiveType.Quad);
		floor2.transform.parent = thisRoom.transform;
		floor2.transform.localPosition = new Vector3(0, -0.01f, 0);
		floor2.transform.eulerAngles = new Vector3(90, 45, 0);
		floor2.transform.localScale = new Vector3(10, 10, 1);
		// TODO :: set material
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

		GameObject floorBeam = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		floorBeam.transform.parent = thisRoom.transform;
		floorBeam.transform.localScale = new Vector3(0.1f, 1.45f, 0.1f);
		// TODO :: set material
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
		// TODO :: set material

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
		// TODO :: set material
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

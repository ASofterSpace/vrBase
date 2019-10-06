/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class ControlRoomCtrl : GenericRoomCtrl
{
	void Start() {

	}

	void Update() {

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
		Destroy(beams[40]); // remove cross beam
		Destroy(beams[41]); // remove floor beam
		Destroy(beams[70]); // remove head beam
		// add two new floor beams to each side of the purple door
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-2.5f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(-4.45f, 0, -5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
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

		// TODO
	}

	private void createBlobFlyer() {

		// TODO
	}
}

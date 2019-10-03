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
		// add two new floor beams to each side of the purple door
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-2.5f, 0, -5);
		curBeam.transform.localScale = new Vector3(0.1f, 0.45f, 0.1f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(-4.4f, 0, -5);
		curBeam.transform.localScale = new Vector3(0.1f, 0.55f, 0.1f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
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

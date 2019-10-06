using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class ArcadeRoomCtrl : GenericRoomCtrl
{
	void Start() {

	}

	void Update() {

	}

	protected override void createRoom() {

		createFloor();

		createBeams();

		createDoors();
	}

	protected override void createBeams() {

		base.createBeams();

		GameObject curBeam;
		int curAngle = 90;

		// make room for the door to the control room
		Destroy(beams[16]); // remove cross beam
		Destroy(beams[17]); // remove floor beam
		Destroy(beams[66]); // remove head beam
		// add two new floor beams to each side of the purple door
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(2.5f, 0, 5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createBeam(0.5f);
		curBeam.transform.localPosition = new Vector3(4.45f, 0, 5);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
	}

	private void createDoors() {

		createDoor(3.5f, 5.0f);
	}

}

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
	}

	protected override void createBeams() {

		base.createBeams();

		GameObject curBeam;
		int curAngle = 90;

		// make room for the door to the control room
		Destroy(beams[16]); // remove cross beam
		Destroy(beams[17]); // remove floor beam
		// add two new floor beams to each side of the purple door
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(2.5f, 0, 5);
		curBeam.transform.localScale = new Vector3(0.1f, 0.45f, 0.1f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
		curBeam = createFloorBeam();
		curBeam.transform.localPosition = new Vector3(4.4f, 0, 5);
		curBeam.transform.localScale = new Vector3(0.1f, 0.55f, 0.1f);
		curBeam.transform.eulerAngles = new Vector3(90, 0, curAngle);
	}

}

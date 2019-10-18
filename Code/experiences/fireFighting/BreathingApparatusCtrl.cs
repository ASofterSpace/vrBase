/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class BreathingApparatusCtrl {

	private GameObject hostRoom;


	public BreathingApparatusCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.hostRoom = hostRoom;
	}

	public void createQuickTester(Vector3 position, Vector3 angles) {

		GameObject quickTester = new GameObject("BreathingApparatusQuickTester");
		quickTester.transform.parent = hostRoom.transform;
		quickTester.transform.localPosition = position;
		quickTester.transform.localEulerAngles = angles;

		GameObject oxygenBottle = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		oxygenBottle.name = "Oxygen Bottle";
		oxygenBottle.transform.parent = quickTester.transform;
		oxygenBottle.transform.localPosition = new Vector3(0, 0.1f, -2.8f);
		oxygenBottle.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottle.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		MaterialCtrl.setMaterial(oxygenBottle, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_YELLOW);
	}
}

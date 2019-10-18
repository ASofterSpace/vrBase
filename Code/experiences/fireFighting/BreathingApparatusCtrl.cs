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

		GameObject apparatus = new GameObject("apparatus");
		apparatus.transform.parent = quickTester.transform;
		apparatus.transform.localPosition = new Vector3(0, 1, 0);

		GameObject oxygenBottleMain = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		oxygenBottleMain.name = "Oxygen Bottle Main";
		oxygenBottleMain.transform.parent = apparatus.transform;
		oxygenBottleMain.transform.localPosition = new Vector3(0, 0, 0);
		oxygenBottleMain.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottleMain.transform.localScale = new Vector3(0.15f, 0.192f, 0.15f);
		MaterialCtrl.setMaterial(oxygenBottleMain, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_YELLOW);

		GameObject oxygenBottleTop = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		oxygenBottleTop.name = "Oxygen Bottle Top";
		oxygenBottleTop.transform.parent = apparatus.transform;
		oxygenBottleTop.transform.localPosition = new Vector3(0, 0.1965f, 0);
		oxygenBottleTop.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottleTop.transform.localScale = new Vector3(0.15f, 0.1f, 0.15f);
		MaterialCtrl.setMaterial(oxygenBottleTop, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_YELLOW);

		GameObject oxygenBottleBottom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		oxygenBottleBottom.name = "Oxygen Bottle Bottom";
		oxygenBottleBottom.transform.parent = apparatus.transform;
		oxygenBottleBottom.transform.localPosition = new Vector3(0, -0.1965f, 0);
		oxygenBottleBottom.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottleBottom.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		MaterialCtrl.setMaterial(oxygenBottleBottom, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_YELLOW);
	}
}

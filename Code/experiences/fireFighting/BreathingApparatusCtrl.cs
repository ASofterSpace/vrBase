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

		GameObject oxygenBottleLogo = GameObject.CreatePrimitive(PrimitiveType.Quad);
		oxygenBottleLogo.name = "Oxygen Bottle Logo";
		oxygenBottleLogo.transform.parent = apparatus.transform;
		oxygenBottleLogo.transform.localPosition = new Vector3(0, 0, -0.075f);
		oxygenBottleLogo.transform.localEulerAngles = new Vector3(0, 0, 90);
		oxygenBottleLogo.transform.localScale = new Vector3(0.18f, 0.04f, 1);
		MaterialCtrl.setMaterial(oxygenBottleLogo, MaterialCtrl.OBJECTS_LOGOS_ASOFTERSPACE);

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
		MaterialCtrl.setMaterial(oxygenBottleBottom, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_GREEN);

		GameObject curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe";
		curObj.transform.parent = apparatus.transform;
		curObj.transform.localPosition = new Vector3(0, -0.2772f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.01f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_GREEN);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Metal Top";
		curObj.transform.parent = apparatus.transform;
		curObj.transform.localPosition = new Vector3(0, -0.2972f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.038f, 0.01f, 0.038f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Metal Middle";
		curObj.transform.parent = apparatus.transform;
		curObj.transform.localPosition = new Vector3(0, -0.3137f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.03f, 0.007f, 0.03f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Metal Bottom";
		curObj.transform.parent = apparatus.transform;
		curObj.transform.localPosition = new Vector3(0, -0.329f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.039f, 0.012f, 0.039f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		GameObject knob = new GameObject("Oxygen Bottle Bottom Knob");
		knob.transform.parent = apparatus.transform;
		knob.transform.localPosition = new Vector3(0, -0.3538f, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Knob Main";
		curObj.transform.parent = knob.transform;
		curObj.transform.localPosition = new Vector3(0, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.045f, 0.013f, 0.045f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_GRAY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Bottom Knob Bar 1";
		curObj.transform.parent = knob.transform;
		curObj.transform.localPosition = new Vector3(0, -0.0039f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 45, 0);
		curObj.transform.localScale = new Vector3(0.015f, 0.015f, 0.06f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_GRAY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Bottom Knob Bar 2";
		curObj.transform.parent = knob.transform;
		curObj.transform.localPosition = new Vector3(0, -0.0039f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, -45, 0);
		curObj.transform.localScale = new Vector3(0.015f, 0.015f, 0.06f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_GRAY);

	}
}

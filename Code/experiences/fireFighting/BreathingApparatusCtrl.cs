/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class BreathingApparatusCtrl : UpdateableCtrl {

	private GameObject hostRoom;

	private int state;

	private GameObject apparatus;
	private GameObject console;
	private GameObject oxygenBottle;
	private GameObject backHolder;
	private GameObject oxygenBottleHolderStrapClosed;
	private GameObject oxygenBottleHolderStrapClosedWraparound;
	private GameObject oxygenBottleHolderStrapOpenedPart1;
	private GameObject oxygenBottleHolderStrapOpenedPart2;
	private GameObject oxygenBottleHolderStrapOpenedPart3;
	private GameObject oxygenBottleHolderStrapOpenedPart4;
	private GameObject oxygenBottleHolderStrapOpenedPart5;
	private GameObject oxygenBottleHolderStrapOpenedPart6;
	private GameObject oxygenBottleHolderStrapOpenedPart7;
	private GameObject oxygenBottleHolderStrapOpenedPart8;


	public BreathingApparatusCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);

		createQuickTester(position, angles);
	}

	void UpdateableCtrl.update(VrInput input) {
		apparatus.transform.localEulerAngles = new Vector3(20, Time.time * 2, 0);
	}

	private void createQuickTester(Vector3 position, Vector3 angles) {

		GameObject quickTester = new GameObject("BreathingApparatusQuickTester");
		quickTester.transform.parent = hostRoom.transform;
		quickTester.transform.localPosition = position;
		quickTester.transform.localEulerAngles = angles;

		apparatus = new GameObject("apparatus");
		apparatus.transform.parent = quickTester.transform;
		apparatus.transform.localPosition = new Vector3(0, 1, 0);
		apparatus.transform.localEulerAngles = new Vector3(20, 0, 0);

		oxygenBottle = new GameObject("Oxygen Bottle");
		oxygenBottle.transform.parent = apparatus.transform;
		oxygenBottle.transform.localPosition = new Vector3(0, 0, 0);
		oxygenBottle.transform.localEulerAngles = new Vector3(0, 0, 0);

		GameObject oxygenBottleMain = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		oxygenBottleMain.name = "Oxygen Bottle Main";
		oxygenBottleMain.transform.parent = oxygenBottle.transform;
		oxygenBottleMain.transform.localPosition = new Vector3(0, 0, 0);
		oxygenBottleMain.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottleMain.transform.localScale = new Vector3(0.15f, 0.192f, 0.15f);
		MaterialCtrl.setMaterial(oxygenBottleMain, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_YELLOW);

		GameObject oxygenBottleLogo = GameObject.CreatePrimitive(PrimitiveType.Quad);
		oxygenBottleLogo.name = "Oxygen Bottle Logo";
		oxygenBottleLogo.transform.parent = oxygenBottle.transform;
		oxygenBottleLogo.transform.localPosition = new Vector3(0, -0.076f, -0.075f);
		oxygenBottleLogo.transform.localEulerAngles = new Vector3(0, 0, 90);
		oxygenBottleLogo.transform.localScale = new Vector3(0.18f, 0.04f, 1);
		MaterialCtrl.setMaterial(oxygenBottleLogo, MaterialCtrl.OBJECTS_LOGOS_ASOFTERSPACE);

		GameObject oxygenBottleTop = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		oxygenBottleTop.name = "Oxygen Bottle Top";
		oxygenBottleTop.transform.parent = oxygenBottle.transform;
		oxygenBottleTop.transform.localPosition = new Vector3(0, 0.1965f, 0);
		oxygenBottleTop.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottleTop.transform.localScale = new Vector3(0.15f, 0.1f, 0.15f);
		MaterialCtrl.setMaterial(oxygenBottleTop, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_YELLOW);

		GameObject oxygenBottleBottom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		oxygenBottleBottom.name = "Oxygen Bottle Bottom";
		oxygenBottleBottom.transform.parent = oxygenBottle.transform;
		oxygenBottleBottom.transform.localPosition = new Vector3(0, -0.1965f, 0);
		oxygenBottleBottom.transform.localEulerAngles = new Vector3(0, 0, 0);
		oxygenBottleBottom.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		MaterialCtrl.setMaterial(oxygenBottleBottom, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_GREEN);

		GameObject curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe";
		curObj.transform.parent = oxygenBottle.transform;
		curObj.transform.localPosition = new Vector3(0, -0.2772f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.01f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_FIREFIGHTING_OXYGEN_GREEN);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Metal Top";
		curObj.transform.parent = oxygenBottle.transform;
		curObj.transform.localPosition = new Vector3(0, -0.2972f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.038f, 0.01f, 0.038f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Metal Middle";
		curObj.transform.parent = oxygenBottle.transform;
		curObj.transform.localPosition = new Vector3(0, -0.3137f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.03f, 0.007f, 0.03f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Metal Bottom";
		curObj.transform.parent = oxygenBottle.transform;
		curObj.transform.localPosition = new Vector3(0, -0.329f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.039f, 0.012f, 0.039f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		GameObject knob = new GameObject("Oxygen Bottle Bottom Knob");
		knob.transform.parent = oxygenBottle.transform;
		knob.transform.localPosition = new Vector3(0, -0.3538f, 0);
		knob.transform.localEulerAngles = new Vector3(0, 0, 0);

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

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Bottom Pipe Horz";
		curObj.transform.parent = oxygenBottle.transform;
		curObj.transform.localPosition = new Vector3(0, -0.3137f, 0.0215f);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.025f, 0.02f, 0.025f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		backHolder = new GameObject("Back Holder");
		backHolder.transform.parent = apparatus.transform;
		backHolder.transform.localPosition = new Vector3(0, 0, 0.084f);
		backHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Receptacle";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0, -0.3137f, -0.028f);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.03f, 0.02f, 0.03f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Main Back Plate";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0, -0.102f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.12f, 0.45f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Back Plate Top";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0, 0.12f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.12f, 0.01f, 0.12f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Back Plate Bottom";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0, -0.3237f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.12f, 0.01f, 0.12f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Back Plate Holder Right";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.093f, -0.102f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.02f, 0.22f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Back Plate Holder Right Top";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.0708f, 0.0248f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 45);
		curObj.transform.localScale = new Vector3(0.02f, 0.07f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Back Plate Holder Right Bottom";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.0703f, -0.2299f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, -45);
		curObj.transform.localScale = new Vector3(0.02f, 0.07f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Back Plate Holder Left";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.093f, -0.102f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.02f, 0.22f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Back Plate Holder Left Top";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.0708f, 0.0248f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, -45);
		curObj.transform.localScale = new Vector3(0.02f, 0.07f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Back Plate Holder Left Bottom";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.0703f, -0.2299f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 45);
		curObj.transform.localScale = new Vector3(0.02f, 0.07f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Holder Strap Closed";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0, 0.045f, -0.084f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.16f, 0.02f, 0.16f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapClosed = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Closed Wraparound";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.054f, 0.0452f, -0.0288f);
		curObj.transform.localEulerAngles = new Vector3(0, 40, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapClosedWraparound = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 1";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.054f, 0.0452f, -0.0288f);
		curObj.transform.localEulerAngles = new Vector3(0, -40, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart1 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 2";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.0898f, 0.0431f, -0.054f);
		curObj.transform.localEulerAngles = new Vector3(0, -29.067f, 5.725f);
		curObj.transform.localScale = new Vector3(0.03887f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart2 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 3";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.1168f, 0.0351f, -0.069f);
		curObj.transform.localEulerAngles = new Vector3(0, -29.067f, 23.255f);
		curObj.transform.localScale = new Vector3(0.03887f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart3 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 4";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.148f, 0.0209f, -0.0809f);
		curObj.transform.localEulerAngles = new Vector3(-6.216f, -14.385f, 24.167f);
		curObj.transform.localScale = new Vector3(0.03887f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart4 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 5";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.1783f, 0.004f, -0.0848f);
		curObj.transform.localEulerAngles = new Vector3(-9.809f, -8.548f, 31.435f);
		curObj.transform.localScale = new Vector3(0.03887f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart5 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 6";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.2084f, -0.0162f, -0.0833f);
		curObj.transform.localEulerAngles = new Vector3(-13.586f, -2.081f, 35.259f);
		curObj.transform.localScale = new Vector3(0.03887f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart6 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 7";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.2334f, -0.0375f, -0.0775f);
		curObj.transform.localEulerAngles = new Vector3(-18.18f, 2.566f, 45.129f);
		curObj.transform.localScale = new Vector3(0.03887f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart7 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Oxygen Bottle Holder Strap Opened Part 8";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(-0.2604f, -0.0665f, -0.0657f);
		curObj.transform.localEulerAngles = new Vector3(-20.407f, 4.521f, 50.025f);
		curObj.transform.localScale = new Vector3(0.04833874f, 0.04f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		oxygenBottleHolderStrapOpenedPart8 = curObj;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Holder Strap Closing Mechanism";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.032f, 0.0452f, -0.0146f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.005f, 0.028f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Holder Strap Closing Mechanism Top";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.032f, 0.0754f, -0.0118f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 0);
		curObj.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Oxygen Bottle Holder Strap Closing Mechanism Bottom";
		curObj.transform.parent = backHolder.transform;
		curObj.transform.localPosition = new Vector3(0.032f, 0.0149f, -0.01106f);
		curObj.transform.localEulerAngles = new Vector3(135, 0, 0);
		curObj.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		console = new GameObject("Console");
		console.transform.parent = quickTester.transform;
		console.transform.localPosition = new Vector3(-0.398f, 0, 0.503f);
		console.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Console Leg";
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0, 0.45f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.4f, 0.45f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Console Top";
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0, 0.89f, -0.1f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.4f, 0.02f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Console Front";
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0, 0.823f, -0.266f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 0);
		curObj.transform.localScale = new Vector3(0.4f, 0.2f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		GameObject[] btnParts = new GameObject[2];
		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.131f, 0.813f, -0.302f);
		curObj.transform.localEulerAngles = new Vector3(20.703f, 112.717f, 50.274f);
		curObj.transform.localScale = new Vector3(0.02f, 0.02f, 0.06f);
		btnParts[0] = curObj;
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_FIREFIGHTING_BTN_GREEN);
		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.09f, 0.8325f, -0.2818001f);
		curObj.transform.localEulerAngles = new Vector3(20.703f, 112.717f, 50.274f);
		curObj.transform.localScale = new Vector3(0.02f, 0.11f, 0.02f);
		btnParts[1] = curObj;
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_FIREFIGHTING_BTN_GREEN);
		Button curBtn = new DefaultMultiButton(
			btnParts,
			() => {
				if (state == 0) {
					// SUCCESS!
				} else {
					// FAILURE...
				}
				setRandomState();
			}
		);
		ButtonCtrl.add(curBtn);

		GameObject[] crossBtnParts = new GameObject[2];
		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0.095f, 0.8325f, -0.2818001f);
		curObj.transform.localEulerAngles = new Vector3(31.309f, 127.299f, 54.434f);
		curObj.transform.localScale = new Vector3(0.02f, 0.02f, 0.11f);
		crossBtnParts[0] = curObj;
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_FIREFIGHTING_BTN_RED);
		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0.095f, 0.8325f, -0.2818001f);
		curObj.transform.localEulerAngles = new Vector3(31.309f, 127.299f, 54.434f);
		curObj.transform.localScale = new Vector3(0.02f, 0.11f, 0.02f);
		crossBtnParts[1] = curObj;
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_FIREFIGHTING_BTN_RED);
		curBtn = new DefaultMultiButton(
			crossBtnParts,
			() => {
				if (state != 0) {
					// SUCCESS!
				} else {
					// FAILURE...
				}
				setRandomState();
			}
		);
		ButtonCtrl.add(curBtn);

		makeEverythingAlright();
	}

	private void makeEverythingAlright() {
		oxygenBottle.SetActive(true);
		backHolder.SetActive(true);
		closeStrap();
	}

	/**
	 * State 0 is a-okay,
	 * State 1 is back holder missing,
	 * State 2 is oxygen bottle missing,
	 * State 3 is strap being open,
	 * Higher states exist during randomization to get a-okay more often, but are mapped to 0
	 */
	private void setRandomState() {

		state = Random.Range(0, 6);

		makeEverythingAlright();

		switch (state) {
			case 1:
				backHolder.SetActive(false);
				break;
			case 2:
				oxygenBottle.SetActive(false);
				openStrap();
				break;
			case 3:
				openStrap();
				break;
			default:
				state = 0;
				break;
		}
	}

	private void openStrap() {
		oxygenBottleHolderStrapClosed.SetActive(false);
		oxygenBottleHolderStrapClosedWraparound.SetActive(false);
		oxygenBottleHolderStrapOpenedPart1.SetActive(true);
		oxygenBottleHolderStrapOpenedPart2.SetActive(true);
		oxygenBottleHolderStrapOpenedPart3.SetActive(true);
		oxygenBottleHolderStrapOpenedPart4.SetActive(true);
		oxygenBottleHolderStrapOpenedPart5.SetActive(true);
		oxygenBottleHolderStrapOpenedPart6.SetActive(true);
		oxygenBottleHolderStrapOpenedPart7.SetActive(true);
		oxygenBottleHolderStrapOpenedPart8.SetActive(true);
	}

	private void closeStrap() {
		oxygenBottleHolderStrapClosed.SetActive(true);
		oxygenBottleHolderStrapClosedWraparound.SetActive(true);
		oxygenBottleHolderStrapOpenedPart1.SetActive(false);
		oxygenBottleHolderStrapOpenedPart2.SetActive(false);
		oxygenBottleHolderStrapOpenedPart3.SetActive(false);
		oxygenBottleHolderStrapOpenedPart4.SetActive(false);
		oxygenBottleHolderStrapOpenedPart5.SetActive(false);
		oxygenBottleHolderStrapOpenedPart6.SetActive(false);
		oxygenBottleHolderStrapOpenedPart7.SetActive(false);
		oxygenBottleHolderStrapOpenedPart8.SetActive(false);
	}

}

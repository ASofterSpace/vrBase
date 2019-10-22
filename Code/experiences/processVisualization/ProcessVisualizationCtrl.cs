/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class ProcessVisualizationCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject processVisualizer;

	private GameObject rocket;


	public ProcessVisualizationCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createProcessVisualizer(position, angles);

		reset();
	}

	public void update(VrInput input) {

	}

	public void reset() {

	}

	private void createProcessVisualizer(Vector3 position, Vector3 angles) {

		processVisualizer = new GameObject("Process Visualizer");
		processVisualizer.transform.parent = hostRoom.transform;
		processVisualizer.transform.localPosition = position;
		processVisualizer.transform.localEulerAngles = angles;

		GameObject curObj;
		BoxCollider col;

		GameObject blackboard = new GameObject("blackboard");
		blackboard.transform.parent = processVisualizer.transform;
		blackboard.transform.localPosition = new Vector3(0, 0, -0.5f);
		blackboard.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Side";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(1.5f, 0.85f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 1.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Foot";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(1.5f, 0.05f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Side";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(-1.5f, 0.85f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 1.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Foot";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(-1.5f, 0.05f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		GameObject frame = new GameObject("Frame");
		frame.transform.parent = blackboard.transform;
		frame.transform.localPosition = new Vector3(0, 1.5f, 0);
		frame.transform.localEulerAngles = new Vector3(-5, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Holder Left";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(1.45f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.025f, 0.05f, 0.025f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Holder Right";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-1.45f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.025f, 0.05f, 0.025f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Left";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(1.4f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 1.5f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Right";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-1.4f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 1.5f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Top";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, 0.775f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 2.85f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Bottom";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, -0.775f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 2.85f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "Blackboard Front";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, 0, 0.01f);
		curObj.transform.localEulerAngles = new Vector3(0, 180, 0);
		curObj.transform.localScale = new Vector3(2.75f, 1.5f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "Blackboard Back";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, 0, -0.01f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2.75f, 1.5f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Bottom Wooden Holder";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-0.5f, -0.775f, 0.125f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 1, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);


		GameObject timePickShelf = new GameObject("timePickShelf");
		timePickShelf.transform.parent = processVisualizer.transform;
		timePickShelf.transform.localPosition = new Vector3(0, 0, 0.5f);
		timePickShelf.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Side";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(-0.475f, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.8f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Side";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0.475f, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.8f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Top Front Side";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.8f, 0);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 90);
		curObj.transform.localScale = new Vector3(0.24f, 0.999f, 0.24f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Mid Shelf";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 0.9f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);


		rocket = ObjectFactory.createRocket();
		rocket.transform.parent = processVisualizer.transform;
		rocket.transform.localPosition = new Vector3(1.15f, 0.336f, 0);
		rocket.transform.localEulerAngles = new Vector3(0, 45, 0);
		rocket.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
	}

}

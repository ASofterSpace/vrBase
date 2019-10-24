/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class RobotCuddleCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject robotAndConsoleHolder;
	private GameObject robot;


	public RobotCuddleCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createRobot(position, angles);

		reset();
	}

	public void update(VrInput input) {

	}

	public void reset() {
		robot.transform.localPosition = new Vector3(0, 0, 5);
		robot.transform.localEulerAngles = new Vector3(0, -110, 0);
	}

	private void createRobot(Vector3 position, Vector3 angles) {

		GameObject curObj;

		robotAndConsoleHolder = new GameObject("Robot and Console Holder");
		robotAndConsoleHolder.transform.localPosition = position;
		robotAndConsoleHolder.transform.localEulerAngles = angles;

		robot = ObjectFactory.createRobot();
		robot.transform.parent = robotAndConsoleHolder.transform;
		robot.transform.localPosition = new Vector3(0, 0, 5);
		robot.transform.localEulerAngles = new Vector3(0, -110, 0);

		GameObject console = new GameObject("Robo Console");
		console.transform.parent = robotAndConsoleHolder.transform;
		console.transform.localPosition = new Vector3(0, 0, 0);
		console.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Console Leg";
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0, 0.45f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.6f, 0.45f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Console Top";
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0, 0.89f, -0.1f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.6f, 0.02f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Console Front";
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(0, 0.823f, -0.266f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 0);
		curObj.transform.localScale = new Vector3(0.6f, 0.2f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);
	}

}

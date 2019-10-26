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

	private bool move;
	private float goForward;
	private float goRight;


	public RobotCuddleCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createRobot(position, angles);

		reset();
	}

	public void update(VrInput input) {

		if (move) {
			if (goForward > 0) {
				float timeStep = Time.deltaTime / 2;
				goForward -= timeStep;
				if (goForward < 0) {
					goForward = 0;
					move = false;
				}
				robot.transform.localPosition = robot.transform.localPosition + robot.transform.localRotation * new Vector3(0, 0, -timeStep);
			}
			if (goForward < 0) {
				float timeStep = Time.deltaTime / 2;
				goForward += timeStep;
				if (goForward > 0) {
					goForward = 0;
					move = false;
				}
				robot.transform.localPosition = robot.transform.localPosition + robot.transform.localRotation * new Vector3(0, 0, timeStep);
			}
			if (goRight > 0) {
				float timeStep = Time.deltaTime * 15;
				goRight -= timeStep;
				if (goRight < 0) {
					goRight = 0;
					move = false;
				}
				Vector3 lEA = robot.transform.localEulerAngles;
				robot.transform.localEulerAngles = new Vector3(lEA.x, lEA.y + timeStep, lEA.z);
			}
			if (goRight < 0) {
				float timeStep = Time.deltaTime * 15;
				goRight += timeStep;
				if (goRight > 0) {
					goRight = 0;
					move = false;
				}
				Vector3 lEA = robot.transform.localEulerAngles;
				robot.transform.localEulerAngles = new Vector3(lEA.x, lEA.y - timeStep, lEA.z);
			}
		}
	}

	public void reset() {

		stop();

		robot.transform.localPosition = new Vector3(0, 0, 5);
		robot.transform.localEulerAngles = new Vector3(0, -110, 0);
	}

	private void createRobot(Vector3 position, Vector3 angles) {

		GameObject curObj;
		BoxCollider col;

		robotAndConsoleHolder = new GameObject("Robot and Console Holder");
		robotAndConsoleHolder.transform.parent = hostRoom.transform;
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
		curObj.transform.localPosition = new Vector3(0, 0.787f, -0.298f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 0);
		curObj.transform.localScale = new Vector3(0.6f, 0.3f, 0.02f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		float offset = 0.14f;

		// turn right button
		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_PURPLE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.05f + offset, 0.8152f, -0.3152f);
		curObj.transform.localEulerAngles = new Vector3(180, 90, 45);
		curObj.transform.localScale = new Vector3(0.07f, 0.04f, 0.07f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		DefaultButton turnRightButton = new DefaultButton(
			curObj,
			() => { turn(90); }
		);
		ButtonCtrl.add(turnRightButton);

		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_WHITE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.05f + offset, 0.8006f, -0.3006f);
		curObj.transform.localEulerAngles = new Vector3(180, 90, 45);
		curObj.transform.localScale = new Vector3(0.085f, 0.005f, 0.085f);

		// turn left button
		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_PURPLE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.23f + offset, 0.8152f, -0.3152f);
		curObj.transform.localEulerAngles = new Vector3(0, 90, 135);
		curObj.transform.localScale = new Vector3(0.07f, 0.04f, 0.07f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		DefaultButton turnLeftButton = new DefaultButton(
			curObj,
			() => { turn(-90); }
		);
		ButtonCtrl.add(turnLeftButton);

		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_WHITE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.23f + offset, 0.8006f, -0.3006f);
		curObj.transform.localEulerAngles = new Vector3(0, 90, 135);
		curObj.transform.localScale = new Vector3(0.085f, 0.005f, 0.085f);

		// go forward button
		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_PURPLE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.14f + offset, 0.876f, -0.255f);
		curObj.transform.localEulerAngles = new Vector3(135, 0, 0);
		curObj.transform.localScale = new Vector3(0.07f, 0.04f, 0.07f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		DefaultButton goForwardButton = new DefaultButton(
			curObj,
			() => { go(1); }
		);
		ButtonCtrl.add(goForwardButton);

		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_WHITE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.14f + offset, 0.864f, -0.237f);
		curObj.transform.localEulerAngles = new Vector3(135, 0, 0);
		curObj.transform.localScale = new Vector3(0.085f, 0.005f, 0.085f);

		// go backward button
		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_PURPLE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.14f + offset, 0.7474f, -0.3836f);
		curObj.transform.localEulerAngles = new Vector3(-45, 0, 0);
		curObj.transform.localScale = new Vector3(0.07f, 0.04f, 0.07f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		DefaultButton goBackwardButton = new DefaultButton(
			curObj,
			() => { go(-1); }
		);
		ButtonCtrl.add(goBackwardButton);

		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_WHITE);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.14f + offset, 0.7316f, -0.3694f);
		curObj.transform.localEulerAngles = new Vector3(-45, 0, 0);
		curObj.transform.localScale = new Vector3(0.085f, 0.005f, 0.085f);

		// stop button
		curObj = PlatonicSolidFactory.createCube(false, false, MaterialCtrl.PLASTIC_PURPLE, 0, 0);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.14f + offset, 0.8125f, -0.3185f);
		curObj.transform.localEulerAngles = new Vector3(-45, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.04f, 0.05f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		DefaultButton stopButton = new DefaultButton(
			curObj,
			() => { stop(); }
		);
		ButtonCtrl.add(stopButton);

		curObj = PlatonicSolidFactory.createCube(false, false, MaterialCtrl.PLASTIC_WHITE, 0, 0);
		curObj.transform.parent = console.transform;
		curObj.transform.localPosition = new Vector3(-0.14f + offset, 0.7996f, -0.3014f);
		curObj.transform.localEulerAngles = new Vector3(-45, 0, 0);
		curObj.transform.localScale = new Vector3(0.065f, 0.005f, 0.065f);
	}

	private void go(float direction) {

		stop();

		move = true;
		goForward = direction;
	}

	private void turn(float angle) {

		stop();

		move = true;
		goRight = angle;
	}

	private void stop() {

		move = false;
		goForward = 0;
		goRight = 0;
	}

}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class AdditiveManufacturerCtrl : UpdateableCtrl, ResetteableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;

	private GameObject roboArm;
	private GameObject roboJoint;
	private GameObject roboPointer;
	private GameObject roboLowerArm;

	// robot rotation up and down
	private float robotTargetRotX;

	// robot rotation left and right
	private float robotTargetRotY;

	// robot rotation lower arm against upper arm
	private float robotTargetRotT;

	// is the robot moving?
	private bool robotMoving;

	private Action callAfterReachingTarget;

	// the blocks from which the object to be built is made up
	private GameObject[] objBlocks;

	private int curBlock = 0;


	public AdditiveManufacturerCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;

		createAdditiveManufacturer(position, angles);

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		// start moving such that the robot comes to its natural rest position
		// and such that we have the internal robo state initialized to useful
		// values, and overall just start the entire game from a fresh state :)
		reset();
	}

	private void createAdditiveManufacturer(Vector3 position, Vector3 angles) {

		GameObject additiveManufacturer = new GameObject("Additive Manufacturer");
		additiveManufacturer.transform.parent = hostRoom.transform;
		additiveManufacturer.transform.localPosition = position;
		additiveManufacturer.transform.localEulerAngles = angles;

		GameObject roboBase = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboBase.name = "roboBase";
		roboBase.transform.parent = additiveManufacturer.transform;
		roboBase.transform.localPosition = new Vector3(0, -0.05f, -1.65f);
		roboBase.transform.localEulerAngles = new Vector3(0, 0, 0);
		roboBase.transform.localScale = new Vector3(0.6f, 0.2f, 0.6f);
		MaterialCtrl.setMaterial(roboBase, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		GameObject roboHorzBasePart = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboHorzBasePart.name = "roboHorzBasePart";
		roboHorzBasePart.transform.parent = additiveManufacturer.transform;
		roboHorzBasePart.transform.localPosition = new Vector3(0, 0.2f, -1.65f);
		roboHorzBasePart.transform.localEulerAngles = new Vector3(0, 0, 0);
		roboHorzBasePart.transform.localScale = new Vector3(0.41f, 0.05f, 0.41f);
		MaterialCtrl.setMaterial(roboHorzBasePart, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		roboArm = new GameObject("roboArm");
		roboArm.transform.parent = additiveManufacturer.transform;
		roboArm.transform.localPosition = new Vector3(0, 0.2f, -1.65f);
		roboArm.transform.localEulerAngles = new Vector3(0, 0, 0);

		GameObject roboVertBasePart = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboVertBasePart.name = "roboVertBasePart";
		roboVertBasePart.transform.parent = roboArm.transform;
		roboVertBasePart.transform.localPosition = new Vector3(0, 0, 0);
		roboVertBasePart.transform.localEulerAngles = new Vector3(90, 90, 0);
		roboVertBasePart.transform.localScale = new Vector3(0.38f, 0.05f, 0.38f);
		MaterialCtrl.setMaterial(roboVertBasePart, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		GameObject roboUpperArm = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboUpperArm.name = "roboUpperArm";
		roboUpperArm.transform.parent = roboArm.transform;
		roboUpperArm.transform.localPosition = new Vector3(0, 1.025f, 0);
		roboUpperArm.transform.localEulerAngles = new Vector3(0, 0, 0);
		roboUpperArm.transform.localScale = new Vector3(0.1f, 0.84f, 0.1f);
		MaterialCtrl.setMaterial(roboUpperArm, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		roboJoint = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboJoint.name = "roboJoint";
		roboJoint.transform.parent = roboArm.transform;
		roboJoint.transform.localPosition = new Vector3(0, 1.98f, 0);
		roboJoint.transform.localEulerAngles = new Vector3(90, 90, 0);
		roboJoint.transform.localScale = new Vector3(0.3f, 0.05f, 0.3f);
		MaterialCtrl.setMaterial(roboJoint, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		roboLowerArm = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboLowerArm.name = "roboLowerArm";
		roboLowerArm.transform.parent = roboArm.transform;
		roboLowerArm.transform.localPosition = new Vector3(0, 1.332f, 0.605f);
		roboLowerArm.transform.localEulerAngles = new Vector3(-45, 0, 0);
		roboLowerArm.transform.localScale = new Vector3(0.1f, 0.84f, 0.1f);
		MaterialCtrl.setMaterial(roboLowerArm, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		roboPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		roboPointer.name = "roboPointer";
		roboPointer.transform.parent = roboArm.transform;
		roboPointer.transform.localPosition = new Vector3(0, 0.7f, 1.236f);
		roboPointer.transform.localEulerAngles = new Vector3(0, 0, 0);
		roboPointer.transform.localScale = new Vector3(0.2f, 0.3f, 0.3f);
		MaterialCtrl.setMaterial(roboPointer, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		GameObject restartConsole = new GameObject("restartConsole");
		restartConsole.transform.parent = additiveManufacturer.transform;
		restartConsole.transform.localPosition = new Vector3(1.3f, 0, 1.2f);
		restartConsole.transform.localEulerAngles = new Vector3(0, 35, 0);

		GameObject restartConsoleBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
		restartConsoleBase.name = "restartConsoleBase";
		restartConsoleBase.transform.parent = restartConsole.transform;
		restartConsoleBase.transform.localPosition = new Vector3(0, 0.4f, 0);
		restartConsoleBase.transform.localEulerAngles = new Vector3(0, 0, 0);
		restartConsoleBase.transform.localScale = new Vector3(0.4f, 0.8f, 0.4f);
		MaterialCtrl.setMaterial(restartConsoleBase, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		GameObject restartConsoleTop = GameObject.CreatePrimitive(PrimitiveType.Cube);
		restartConsoleTop.name = "restartConsoleTop";
		restartConsoleTop.transform.parent = restartConsole.transform;
		restartConsoleTop.transform.localPosition = new Vector3(0, 0.8f, 0);
		restartConsoleTop.transform.localEulerAngles = new Vector3(0, 0, 45);
		restartConsoleTop.transform.localScale = new Vector3(0.28f, 0.28f, 0.395f);
		MaterialCtrl.setMaterial(restartConsoleTop, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		GameObject restartConsoleBtn = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		restartConsoleBtn.transform.parent = restartConsole.transform;
		restartConsoleBtn.transform.localPosition = new Vector3(-0.1495f, 0.8832f, 0);
		restartConsoleBtn.transform.localEulerAngles = new Vector3(0, 0, 45);
		restartConsoleBtn.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(restartConsoleBtn, MaterialCtrl.PLASTIC_RED);
		ButtonCtrl.add(
			new DefaultButton(
				restartConsoleBtn,
				() => {
					reset();
				}
			)
		);

		GameObject restartConsoleBtnFrame = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		restartConsoleBtnFrame.name = "restartConsoleBtnFrame";
		restartConsoleBtnFrame.transform.parent = restartConsole.transform;
		restartConsoleBtnFrame.transform.localPosition = new Vector3(-0.1314f, 0.8672f, 0);
		restartConsoleBtnFrame.transform.localEulerAngles = new Vector3(0, 0, 45);
		restartConsoleBtnFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(restartConsoleBtnFrame, MaterialCtrl.PLASTIC_WHITE);

		GameObject restartConsoleSign = GameObject.CreatePrimitive(PrimitiveType.Quad);
		restartConsoleSign.name = "restartConsoleSign";
		restartConsoleSign.transform.parent = restartConsole.transform;
		restartConsoleSign.transform.localPosition = new Vector3(-0.0552f, 0.957f, 0);
		restartConsoleSign.transform.localEulerAngles = new Vector3(45, 90, 0);
		restartConsoleSign.transform.localScale = new Vector3(0.1921978f, 0.065866f, 1);
		MaterialCtrl.setMaterial(restartConsoleSign, MaterialCtrl.OBJECTS_TICTACTOE_LABELS_RESTART);

		objBlocks = new GameObject[250];

		float blockSize = 0.02f;

		for (int i = 0; i < objBlocks.Length; i++) {
			int x = i % 10;
			int z = i / 10;
			GameObject curBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
			curBlock.name = "restartConsoleSign";
			curBlock.transform.parent = additiveManufacturer.transform;
			curBlock.transform.localPosition = new Vector3((-5 * blockSize) + (blockSize * x), blockSize / 2, blockSize * z);
			curBlock.transform.localEulerAngles = new Vector3(0, 0, 0);
			curBlock.transform.localScale = new Vector3(blockSize, blockSize, blockSize);
			MaterialCtrl.setMaterial(curBlock, MaterialCtrl.OBJECTS_TICTACTOE_GRAY);
			objBlocks[i] = curBlock;
		}
	}

	public void reset() {

		this.curBlock = 0;

		for (int i = 0; i < objBlocks.Length; i++) {
			objBlocks[i].SetActive(false);
		}

		moveRobotBack();

		callAfterReachingTarget = () => {
			Vector3 pos = objBlocks[this.curBlock].transform.localPosition;
			moveRobotTo(pos.x, pos.y);
		};
	}

	public void update(VrInput input) {

		if (!robotMoving) {
			return;
		}

		float threshold = 0.001f;
		float speed = 20;

		bool moved = false;

		float x = Utils.clampRot(roboArm.transform.localEulerAngles.x);
		float y = Utils.clampRot(roboArm.transform.localEulerAngles.y);
		float z = 0;

		if ((robotTargetRotY < 0) || (y < 0)) {

			// start with movement to the right
			if (y > robotTargetRotY + threshold) {
				y = y - speed * Time.deltaTime;
				if (y < robotTargetRotY) {
					y = robotTargetRotY;
				}
				moved = true;
			}

			// continue with movement down, then later up
			if (!moved) {
				if (x > robotTargetRotX + threshold) {
					x = x - speed * Time.deltaTime;
					if (x < robotTargetRotX) {
						x = robotTargetRotX;
					}
					moved = true;
				}
				if (x < robotTargetRotX - threshold) {
					x = x + speed * Time.deltaTime;
					if (x > robotTargetRotX) {
						x = robotTargetRotX;
					}
					moved = true;
				}
			}

			// finally, move backwards to the left
			if (!moved) {
				if (y < robotTargetRotY - threshold) {
					y = y + speed * Time.deltaTime;
					if (y > robotTargetRotY) {
						y = robotTargetRotY;
					}
					moved = true;
				}
			}

		} else {

			// start with movement to the left
			if (y < robotTargetRotY - threshold) {
				y = y + speed * Time.deltaTime;
				if (y > robotTargetRotY) {
					y = robotTargetRotY;
				}
				moved = true;
			}

			// continue with movement down, then later up
			if (!moved) {
				if (x > robotTargetRotX + threshold) {
					x = x - speed * Time.deltaTime;
					if (x < robotTargetRotX) {
						x = robotTargetRotX;
					}
					moved = true;
				}
				if (x < robotTargetRotX - threshold) {
					x = x + speed * Time.deltaTime;
					if (x > robotTargetRotX) {
						x = robotTargetRotX;
					}
					moved = true;
				}
			}

			// finally, move backwards to the right
			if (!moved) {
				if (y > robotTargetRotY + threshold) {
					y = y - speed * Time.deltaTime;
					if (y < robotTargetRotY) {
						y = robotTargetRotY;
					}
					moved = true;
				}
			}
		}

		// while all the other movements at the base are happening,
		// also move the arm at the top joint
		float t = Utils.clampRot(roboLowerArm.transform.localEulerAngles.x);

		if (t < robotTargetRotT - threshold) {
			t = t + speed * Time.deltaTime;
			if (t > robotTargetRotT) {
				t = robotTargetRotT;
			}
			moved = true;
		}
		if (t > robotTargetRotT + threshold) {
			t = t - speed * Time.deltaTime;
			if (t < robotTargetRotT) {
				t = robotTargetRotT;
			}
			moved = true;
		}

		roboLowerArm.transform.localEulerAngles = new Vector3(t, 0, 0);
		roboLowerArm.transform.localPosition =
			roboJoint.transform.localPosition - roboLowerArm.transform.localRotation * (0.84f * Vector3.up);
		roboPointer.transform.localPosition =
			roboJoint.transform.localPosition - roboLowerArm.transform.localRotation * (1.68f * Vector3.up);

		if (moved) {
			roboArm.transform.localEulerAngles = new Vector3(x, y, z);
		} else {
			robotMoving = false;
			if (callAfterReachingTarget != null) {
				callAfterReachingTarget();
			}
		}
	}

	private void moveRobotTo(float x, float y) {

		// TODO :: figoure out how to go from x to robotTargetRotX, and from y to robotTargetRotY
		robotTargetRotX = 32;
		robotTargetRotY = 0;
		robotTargetRotT = -30 + (10 * y);

		/*
		if ((x == 0) && (y == 0)) {
			robotTargetRotX = 33.5f;
			robotTargetRotY = -35;
			robotTargetRotT = -37;
		}
		if ((x == 1) && (y == 0)) {
			robotTargetRotX = 32;
			robotTargetRotY = 0;
			robotTargetRotT = -30;
		}
		if ((x == 2) && (y == 0)) {
			robotTargetRotX = 33.5f;
			robotTargetRotY = 35;
			robotTargetRotT = -37;
		}
		if ((x == 0) && (y == 1)) {
			robotTargetRotX = 39;
			robotTargetRotY = -25.5f;
			robotTargetRotT = -60;
		}
		if ((x == 1) && (y == 1)) {
			robotTargetRotX = 35.25f;
			robotTargetRotY = 0;
			robotTargetRotT = -50;
		}
		if ((x == 2) && (y == 1)) {
			robotTargetRotX = 39;
			robotTargetRotY = 25.5f;
			robotTargetRotT = -60;
		}
		if ((x == 0) && (y == 2)) {
			robotTargetRotX = 48;
			robotTargetRotY = -20;
			robotTargetRotT = -85;
		}
		if ((x == 1) && (y == 2)) {
			robotTargetRotX = 45.8f;
			robotTargetRotY = 0;
			robotTargetRotT = -80;
		}
		if ((x == 2) && (y == 2)) {
			robotTargetRotX = 48;
			robotTargetRotY = 20;
			robotTargetRotT = -85;
		}
		*/
		robotMoving = true;

		callAfterReachingTarget = () => {

			objBlocks[this.curBlock].SetActive(true);

			this.curBlock++;

			if (!checkForDone()) {
				Vector3 pos = objBlocks[this.curBlock].transform.localPosition;
				moveRobotTo(pos.x, pos.y);
			}
		};
	}

	private void moveRobotBack() {

		robotTargetRotX = 0;
		robotTargetRotY = 0;
		robotTargetRotT = -50;
		robotMoving = true;

		callAfterReachingTarget = null;
	}

	/**
	 * Returns true if the manufacturing process is done
	 */
	private bool checkForDone() {

		return curBlock == objBlocks.Length;
	}
}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class TicTacToeCtrl : UpdateableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;

	private GameObject roboArm;
	private GameObject roboJoint;
	private GameObject roboPointer;
	private GameObject roboLowerArm;

	private TicTacToeButton[][] buttons;

	private bool humansTurn;

	// robot rotation up and down
	private float robotTargetRotX;

	// robot rotation left and right
	private float robotTargetRotY;

	// robot rotation lower arm against upper arm
	private float robotTargetRotT;

	private Action callAfterReachingTarget;

	// is the robot moving?
	private bool moving;


	public TicTacToeCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;

		this.humansTurn = true;

		this.robotTargetRotX = 0;
		this.robotTargetRotY = 0;
		this.robotTargetRotT = 0;

		this.callAfterReachingTarget = null;

		this.moving = false;
	}

	public void createPlayingField(Vector3 position, Vector3 angles) {

		GameObject ticTacToe = new GameObject("TicTacToe");
		ticTacToe.transform.parent = hostRoom.transform;

		GameObject[][] fields = new GameObject[3][];
		buttons = new TicTacToeButton[3][];

		for (int x = 0; x < 3; x++) {
			fields[x] = new GameObject[3];
			buttons[x] = new TicTacToeButton[3];
			for (int y = 0; y < 3; y++) {
				fields[x][y] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				fields[x][y].transform.parent = ticTacToe.transform;
				fields[x][y].transform.localPosition = new Vector3((x - 1) * 0.7f, 0.025f, (y - 1) * 0.7f);
				fields[x][y].transform.eulerAngles = new Vector3(0, 0, 0);
				fields[x][y].transform.localScale = new Vector3(0.55f, 0.025f, 0.55f);
				buttons[x][y] = new TicTacToeButton(
					fields[x][y],
					ButtonCtrl.BTN_TICTACTOE_FIELD + x + "-" + y,
					this
				);
				ButtonCtrl.add(buttons[x][y]);
			}
		}

		ticTacToe.transform.localPosition = position;
		ticTacToe.transform.eulerAngles = angles;

		GameObject roboBase = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboBase.name = "roboBase";
		roboBase.transform.parent = ticTacToe.transform;
		roboBase.transform.localPosition = new Vector3(0, -0.05f, -1.65f);
		roboBase.transform.eulerAngles = new Vector3(0, 0, 0);
		roboBase.transform.localScale = new Vector3(0.6f, 0.2f, 0.6f);
		MaterialCtrl.setMaterial(roboBase, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		GameObject roboHorzBasePart = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboHorzBasePart.name = "roboHorzBasePart";
		roboHorzBasePart.transform.parent = ticTacToe.transform;
		roboHorzBasePart.transform.localPosition = new Vector3(0, 0.2f, -1.65f);
		roboHorzBasePart.transform.eulerAngles = new Vector3(0, 0, 0);
		roboHorzBasePart.transform.localScale = new Vector3(0.41f, 0.05f, 0.41f);
		MaterialCtrl.setMaterial(roboHorzBasePart, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		roboArm = new GameObject("roboArm");
		roboArm.transform.parent = ticTacToe.transform;

		GameObject roboVertBasePart = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboVertBasePart.name = "roboVertBasePart";
		roboVertBasePart.transform.parent = roboArm.transform;
		roboVertBasePart.transform.localPosition = new Vector3(0, 0, 0);
		roboVertBasePart.transform.eulerAngles = new Vector3(90, 90, 0);
		roboVertBasePart.transform.localScale = new Vector3(0.38f, 0.05f, 0.38f);
		MaterialCtrl.setMaterial(roboVertBasePart, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		GameObject roboUpperArm = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboUpperArm.name = "roboUpperArm";
		roboUpperArm.transform.parent = roboArm.transform;
		roboUpperArm.transform.localPosition = new Vector3(0, 1.025f, 0);
		roboUpperArm.transform.eulerAngles = new Vector3(0, 0, 0);
		roboUpperArm.transform.localScale = new Vector3(0.1f, 0.84f, 0.1f);
		MaterialCtrl.setMaterial(roboUpperArm, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		roboJoint = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboJoint.name = "roboJoint";
		roboJoint.transform.parent = roboArm.transform;
		roboJoint.transform.localPosition = new Vector3(0, 1.98f, 0);
		roboJoint.transform.eulerAngles = new Vector3(90, 90, 0);
		roboJoint.transform.localScale = new Vector3(0.3f, 0.05f, 0.3f);
		MaterialCtrl.setMaterial(roboJoint, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		roboLowerArm = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboLowerArm.name = "roboLowerArm";
		roboLowerArm.transform.parent = roboArm.transform;
		roboLowerArm.transform.localPosition = new Vector3(0, 1.332f, 0.605f);
		roboLowerArm.transform.eulerAngles = new Vector3(-45, 0, 0);
		roboLowerArm.transform.localScale = new Vector3(0.1f, 0.84f, 0.1f);
		MaterialCtrl.setMaterial(roboLowerArm, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		roboPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		roboPointer.name = "roboPointer";
		roboPointer.transform.parent = roboArm.transform;
		roboPointer.transform.localPosition = new Vector3(0, 0.7f, 1.236f);
		roboPointer.transform.eulerAngles = new Vector3(0, 0, 0);
		roboPointer.transform.localScale = new Vector3(0.2f, 0.3f, 0.3f);
		MaterialCtrl.setMaterial(roboPointer, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		roboArm.transform.localPosition = new Vector3(0, 0.2f, -1.65f);
		roboArm.transform.eulerAngles = new Vector3(0, 0, 0);

		mainCtrl.addUpdateableCtrl(this);

		moveRobotToField(0, 0); // DEBUG
	}

	public void restartGame() {

		for (int x = 0; x < 3; x++) {
			for (int y = 0; y < 3; y++) {
				buttons[x][y].reset();
			}
		}

		humansTurn = true;

		moveRobotBack();
	}

	void UpdateableCtrl.update(VrInput input) {

		if (!moving) {
			return;
		}

		float threshold = 0.001f;
		float speed = 10;

		bool moved = false;

		float x = Utils.clampRot(roboArm.transform.eulerAngles.x);
		float y = Utils.clampRot(roboArm.transform.eulerAngles.y);
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
			roboArm.transform.eulerAngles = new Vector3(x, y, z);
		} else {
			moving = false;
			if (callAfterReachingTarget != null) {
				callAfterReachingTarget();
			}
		}
	}

	public bool isHumansTurn() {
		return humansTurn;
	}

	/**
	 * It is the robot's turn to move - so let's! :D
	 */
	public void makeRoboMove() {

		humansTurn = false;

		int x, y;

		findNextRobotTarget(out x, out y);

		moveRobotToField(x, y);
	}

	private void findNextRobotTarget(out int x, out int y) {

		// first of all, check if we have any immediately winning moves - if yes, make one of them!
		// TODO

		// then, check if the human has any immediately winning moves - if yes, prevent them!
		// TODO

		// now, check if the middle field is free... if yes, take it!
		x = 1;
		y = 1;
		if (buttons[x][y].isFree()) {
			return;
		}

		// the middle field is not free, we have no winning moves and nothing to prevent things either...
		// just take a leftover button at random :)

		for (x = 0; x < 3; x++) {
			for (y = 0; y < 3; y++) {
				if (buttons[x][y].isFree()) {
					return;
				}
			}
		}
	}

	private void moveRobotToField(int x, int y) {

		if ((x == 0) && (y == 0)) {
			robotTargetRotX = 33.5f;
			robotTargetRotY = -35;
			robotTargetRotT = -37;
		}
		if ((x == 2) && (y == 0)) {
			robotTargetRotX = 33.5f;
			robotTargetRotY = 35;
			robotTargetRotT = -37;
		}
		moving = true;

		callAfterReachingTarget = () => {

			buttons[x][y].setRobo();

			// TODO :: sleep

			moveRobotBack();
		};
	}

	private void moveRobotBack() {

		robotTargetRotX = 0;
		robotTargetRotY = 0;
		robotTargetRotT = -50;
		moving = true;

		callAfterReachingTarget = () => {
			humansTurn = true;
			moveRobotToField(0, 0); // DEBUG
		};
	}
}

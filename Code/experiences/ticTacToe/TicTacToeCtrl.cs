/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class TicTacToeCtrl : UpdateableCtrl, ResetteableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;

	private GameObject roboArm;
	private GameObject roboJoint;
	private GameObject roboPointer;
	private GameObject roboLowerArm;

	private TicTacToeButton[][] buttons;
	private TicTacToeButton[] buttonsThatWon;

	private bool humansTurn;

	// robot rotation up and down
	private float robotTargetRotX;

	// robot rotation left and right
	private float robotTargetRotY;

	// robot rotation lower arm against upper arm
	private float robotTargetRotT;

	private Action callAfterReachingTarget;

	// is the robot moving?
	private bool robotMoving;


	public TicTacToeCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;

		this.humansTurn = true;

		this.buttonsThatWon = new TicTacToeButton[3];

		createPlayingField(position, angles);

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		// start moving such that the robot comes to its natural rest position
		// and such that we have the internal robo state initialized to useful
		// values, and overall just start the entire game from a fresh state :)
		reset();
	}

	private void createPlayingField(Vector3 position, Vector3 angles) {

		GameObject ticTacToe = new GameObject("TicTacToe");
		ticTacToe.transform.parent = hostRoom.transform;
		ticTacToe.transform.localPosition = position;
		ticTacToe.transform.localEulerAngles = angles;

		GameObject[][] fields = new GameObject[3][];
		buttons = new TicTacToeButton[3][];

		for (int x = 0; x < 3; x++) {
			fields[x] = new GameObject[3];
			buttons[x] = new TicTacToeButton[3];
			for (int y = 0; y < 3; y++) {
				fields[x][y] = PrimitiveFactory.createCylinder(40, 10, false, MaterialCtrl.OBJECTS_TICTACTOE_GRAY);
				fields[x][y].transform.parent = ticTacToe.transform;
				fields[x][y].transform.localPosition = new Vector3((x - 1) * 0.7f, 0.025f, (y - 1) * 0.7f);
				fields[x][y].transform.localEulerAngles = new Vector3(0, 135, 0);
				fields[x][y].transform.localScale = new Vector3(0.55f, 0.025f, 0.55f);
				buttons[x][y] = new TicTacToeButton(
					fields[x][y],
					this
				);
				ButtonCtrl.add(buttons[x][y]);
			}
		}

		GameObject roboBase = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboBase.name = "roboBase";
		roboBase.transform.parent = ticTacToe.transform;
		roboBase.transform.localPosition = new Vector3(0, -0.05f, -1.65f);
		roboBase.transform.localEulerAngles = new Vector3(0, 0, 0);
		roboBase.transform.localScale = new Vector3(0.6f, 0.2f, 0.6f);
		MaterialCtrl.setMaterial(roboBase, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT);

		GameObject roboHorzBasePart = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		roboHorzBasePart.name = "roboHorzBasePart";
		roboHorzBasePart.transform.parent = ticTacToe.transform;
		roboHorzBasePart.transform.localPosition = new Vector3(0, 0.2f, -1.65f);
		roboHorzBasePart.transform.localEulerAngles = new Vector3(0, 0, 0);
		roboHorzBasePart.transform.localScale = new Vector3(0.41f, 0.05f, 0.41f);
		MaterialCtrl.setMaterial(roboHorzBasePart, MaterialCtrl.OBJECTS_TICTACTOE_ROBOT_GRAY);

		roboArm = new GameObject("roboArm");
		roboArm.transform.parent = ticTacToe.transform;
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
		restartConsole.transform.parent = ticTacToe.transform;
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
		Button btnRestart = new TicTacToeButtonRestart(
			restartConsoleBtn,
			this
		);
		ButtonCtrl.add(btnRestart);

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
	}

	public void reset() {

		moveRobotBack();

		// do this...
		buttonsThatWon[0] = null;
		buttonsThatWon[1] = null;
		buttonsThatWon[2] = null;

		// ... before this (as the reset will set visible, and
		// while buttonsThatWon are still assigned, buttons might
		// still be blinked into invisibility)
		for (int x = 0; x < 3; x++) {
			for (int y = 0; y < 3; y++) {
				buttons[x][y].reset();
			}
		}

		humansTurn = true;
	}

	public void update(VrInput input) {

		if ((buttonsThatWon[0] != null) && (buttonsThatWon[1] != null) && (buttonsThatWon[2] != null)) {
			bool visible = (Mathf.RoundToInt(Time.time * 3) % 2) == 0;
			buttonsThatWon[0].setActive(visible);
			buttonsThatWon[1].setActive(visible);
			buttonsThatWon[2].setActive(visible);
		}

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

	public bool isHumansTurn() {
		return humansTurn;
	}

	/**
	 * It is the robot's turn to move - so let's! :D
	 */
	public void makeRoboMove() {

		humansTurn = false;

		if (checkForGameOver()) {
			return;
		}

		int x, y;

		findNextRobotTarget(out x, out y);

		moveRobotToField(x, y);
	}

	private void findNextRobotTarget(out int x, out int y) {

		// first of all, check if we have any immediately winning moves - if yes, make one of them!
		for (y = 0; y < 3; y++) {
			if (buttons[0][y].isRobo() && buttons[1][y].isRobo() && buttons[2][y].isFree()) {
				x = 2;
				return;
			}
			if (buttons[0][y].isRobo() && buttons[2][y].isRobo() && buttons[1][y].isFree()) {
				x = 1;
				return;
			}
			if (buttons[1][y].isRobo() && buttons[2][y].isRobo() && buttons[0][y].isFree()) {
				x = 0;
				return;
			}
		}
		for (x = 0; x < 3; x++) {
			if (buttons[x][0].isRobo() && buttons[x][1].isRobo() && buttons[x][2].isFree()) {
				y = 2;
				return;
			}
			if (buttons[x][0].isRobo() && buttons[x][2].isRobo() && buttons[x][1].isFree()) {
				y = 1;
				return;
			}
			if (buttons[x][1].isRobo() && buttons[x][2].isRobo() && buttons[x][0].isFree()) {
				y = 0;
				return;
			}
		}
		// diagonals
		if (buttons[0][0].isRobo() && buttons[1][1].isRobo() && buttons[2][2].isFree()) {
			x = 2;
			y = 2;
			return;
		}
		if (buttons[0][0].isRobo() && buttons[2][2].isRobo() && buttons[1][1].isFree()) {
			x = 1;
			y = 1;
			return;
		}
		if (buttons[1][1].isRobo() && buttons[2][2].isRobo() && buttons[0][0].isFree()) {
			x = 0;
			y = 0;
			return;
		}
		if (buttons[0][2].isRobo() && buttons[1][1].isRobo() && buttons[2][0].isFree()) {
			x = 2;
			y = 0;
			return;
		}
		if (buttons[0][2].isRobo() && buttons[2][0].isRobo() && buttons[1][1].isFree()) {
			x = 1;
			y = 1;
			return;
		}
		if (buttons[1][1].isRobo() && buttons[2][0].isRobo() && buttons[0][2].isFree()) {
			x = 0;
			y = 2;
			return;
		}

		// then, check if the human has any immediately winning moves - if yes, prevent them!
		for (y = 0; y < 3; y++) {
			if (buttons[0][y].isHuman() && buttons[1][y].isHuman() && buttons[2][y].isFree()) {
				x = 2;
				return;
			}
			if (buttons[0][y].isHuman() && buttons[2][y].isHuman() && buttons[1][y].isFree()) {
				x = 1;
				return;
			}
			if (buttons[1][y].isHuman() && buttons[2][y].isHuman() && buttons[0][y].isFree()) {
				x = 0;
				return;
			}
		}
		for (x = 0; x < 3; x++) {
			if (buttons[x][0].isHuman() && buttons[x][1].isHuman() && buttons[x][2].isFree()) {
				y = 2;
				return;
			}
			if (buttons[x][0].isHuman() && buttons[x][2].isHuman() && buttons[x][1].isFree()) {
				y = 1;
				return;
			}
			if (buttons[x][1].isHuman() && buttons[x][2].isHuman() && buttons[x][0].isFree()) {
				y = 0;
				return;
			}
		}
		// diagonals
		if (buttons[0][0].isHuman() && buttons[1][1].isHuman() && buttons[2][2].isFree()) {
			x = 2;
			y = 2;
			return;
		}
		if (buttons[0][0].isHuman() && buttons[2][2].isHuman() && buttons[1][1].isFree()) {
			x = 1;
			y = 1;
			return;
		}
		if (buttons[1][1].isHuman() && buttons[2][2].isHuman() && buttons[0][0].isFree()) {
			x = 0;
			y = 0;
			return;
		}
		if (buttons[0][2].isHuman() && buttons[1][1].isHuman() && buttons[2][0].isFree()) {
			x = 2;
			y = 0;
			return;
		}
		if (buttons[0][2].isHuman() && buttons[2][0].isHuman() && buttons[1][1].isFree()) {
			x = 1;
			y = 1;
			return;
		}
		if (buttons[1][1].isHuman() && buttons[2][0].isHuman() && buttons[0][2].isFree()) {
			x = 0;
			y = 2;
			return;
		}

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
		robotMoving = true;

		callAfterReachingTarget = () => {

			buttons[x][y].setRobo();

			// let the human player already make a new selection while
			// the robot is still on its way back - only while the game
			// is still going on!
			humansTurn = !checkForGameOver();

			moveRobotBack();
		};
	}

	private void moveRobotBack() {

		robotTargetRotX = 0;
		robotTargetRotY = 0;
		robotTargetRotT = -50;
		robotMoving = true;

		callAfterReachingTarget = null;
	}

	private bool checkForHumanWon() {

		// horizontals and verticals
		for (int y = 0; y < 3; y++) {
			if (buttons[0][y].isHuman() && buttons[1][y].isHuman() && buttons[2][y].isHuman()) {
				buttonsThatWon[0] = buttons[0][y];
				buttonsThatWon[1] = buttons[1][y];
				buttonsThatWon[2] = buttons[2][y];
				return true;
			}
		}
		for (int x = 0; x < 3; x++) {
			if (buttons[x][0].isHuman() && buttons[x][1].isHuman() && buttons[x][2].isHuman()) {
				buttonsThatWon[0] = buttons[x][0];
				buttonsThatWon[1] = buttons[x][1];
				buttonsThatWon[2] = buttons[x][2];
				return true;
			}
		}

		// diagonals
		if (buttons[0][0].isHuman() && buttons[1][1].isHuman() && buttons[2][2].isHuman()) {
			buttonsThatWon[0] = buttons[0][0];
			buttonsThatWon[1] = buttons[1][1];
			buttonsThatWon[2] = buttons[2][2];
			return true;
		}
		if (buttons[0][2].isHuman() && buttons[1][1].isHuman() && buttons[2][0].isHuman()) {
			buttonsThatWon[0] = buttons[0][2];
			buttonsThatWon[1] = buttons[1][1];
			buttonsThatWon[2] = buttons[2][0];
			return true;
		}

		return false;
	}

	private bool checkForRoboWon() {

		// horizontals and verticals
		for (int y = 0; y < 3; y++) {
			if (buttons[0][y].isRobo() && buttons[1][y].isRobo() && buttons[2][y].isRobo()) {
				buttonsThatWon[0] = buttons[0][y];
				buttonsThatWon[1] = buttons[1][y];
				buttonsThatWon[2] = buttons[2][y];
				return true;
			}
		}
		for (int x = 0; x < 3; x++) {
			if (buttons[x][0].isRobo() && buttons[x][1].isRobo() && buttons[x][2].isRobo()) {
				buttonsThatWon[0] = buttons[x][0];
				buttonsThatWon[1] = buttons[x][1];
				buttonsThatWon[2] = buttons[x][2];
				return true;
			}
		}

		// diagonals
		if (buttons[0][0].isRobo() && buttons[1][1].isRobo() && buttons[2][2].isRobo()) {
			buttonsThatWon[0] = buttons[0][0];
			buttonsThatWon[1] = buttons[1][1];
			buttonsThatWon[2] = buttons[2][2];
			return true;
		}
		if (buttons[0][2].isRobo() && buttons[1][1].isRobo() && buttons[2][0].isRobo()) {
			buttonsThatWon[0] = buttons[0][2];
			buttonsThatWon[1] = buttons[1][1];
			buttonsThatWon[2] = buttons[2][0];
			return true;
		}

		return false;
	}

	/**
	 * Returns true if any one player won, or all fields are taken up
	 */
	private bool checkForGameOver() {

		if (checkForHumanWon()) {
			SoundCtrl.playMainCamSound(SoundCtrl.KLACK_KLACK_METAL_2);
			return true;
		}

		if (checkForRoboWon()) {
			SoundCtrl.playMainCamSound(SoundCtrl.KLACK_KLACK_METAL_2);
			return true;
		}

		// no one won yet...
		// now check if all fields are taken - if yes, the game is over anyway;
		// if not (so if at least one is free), the game is still going on
		for (int x = 0; x < 3; x++) {
			for (int y = 0; y < 3; y++) {
				if (buttons[x][y].isFree()) {
					return false;
				}
			}
		}
		return true;
	}
}

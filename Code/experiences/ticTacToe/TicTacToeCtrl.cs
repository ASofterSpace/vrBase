/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class TicTacToeCtrl {

	private GameObject hostRoom;

	private TicTacToeButton[][] buttons;

	private bool humansTurn;


	public TicTacToeCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.hostRoom = hostRoom;

		humansTurn = true;
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
				fields[x][y].transform.localScale = new Vector3(0.55f, 0.05f, 0.55f);
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
	}

	public void restartGame() {

		for (int x = 0; x < 3; x++) {
			for (int y = 0; y < 3; y++) {
				buttons[x][y].reset();
			}
		}

		humansTurn = true;

		// TODO :: finish robo movement, if movement is ongoing
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

		// TODO :: move some sort of robo-arm or something to click on a field, then have it light up,
		// then move back
		buttons[x][y].setRobo();

		humansTurn = true;
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
}

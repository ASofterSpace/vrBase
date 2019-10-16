/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A button / pedestol / control surface for the interactive TicTacToe game
 */
public class TicTacToeButton : Button {

	private TicTacToeCtrl ctrl;

	private int state = 0; // can be: 0 .. gray, 1 .. red, 2 .. blue


	public TicTacToeButton(GameObject obj, string buttonName, TicTacToeCtrl ctrl) : base(obj, buttonName) {

		this.ctrl = ctrl;

		recolorize();
	}

	public void reset() {
		state = 0;
		recolorize();
	}

	public override void trigger() {

		// if it is our turn and the button has not yet been pressed by anyone...
		if (ctrl.isHumansTurn() && (state == 0)) {

			// ... then press it!
			state = 2;

			// and tell the controller that it is the robot's turn now!
			ctrl.makeRoboMove();
		}

		// recolorize this button... in any case, just in case, you never know ;)
		recolorize();
	}

	public override void hover() {
		if (ctrl.isHumansTurn() && (state == 0)) {
			MaterialCtrl.setMaterial(gameObject, MaterialCtrl.INTERACTION_BUTTON_HOVER);
		}
	}

	public override void blur() {
		recolorize();
	}
	public void setRobo() {
		state = 1;
		recolorize();
	}

	private void recolorize() {
		switch (state) {
			case 0:
				MaterialCtrl.setMaterial(gameObject, MaterialCtrl.OBJECTS_TICTACTOE_GRAY);
				break;
			case 1:
				MaterialCtrl.setMaterial(gameObject, MaterialCtrl.OBJECTS_TICTACTOE_RED);
				break;
			case 2:
				MaterialCtrl.setMaterial(gameObject, MaterialCtrl.OBJECTS_TICTACTOE_BLUE);
				break;
		}
	}

	public bool isFree() {
		return state == 0;
	}

	public bool isRobo() {
		return state == 1;
	}

	public bool isHuman() {
		return state == 2;
	}

}

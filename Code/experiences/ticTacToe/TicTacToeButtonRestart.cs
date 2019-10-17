/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A button to restart the TicTacToe game
 */
public class TicTacToeButtonRestart : Button {

	private TicTacToeCtrl ctrl;


	public TicTacToeButtonRestart(GameObject obj, string buttonName, TicTacToeCtrl ctrl) : base(obj, buttonName) {

		this.ctrl = ctrl;
	}

	public override void trigger() {

		ctrl.restartGame();
	}

}

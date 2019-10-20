/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * I know, I know, a bowling ball is not really a "button" per se...
 * but you can click it, and then you have it in your hand.
 * So it IS a button - a round button that you can take and throw around! :D
 */
public class BowlingBallButton : Button {

	private BowlingAlleyCtrl ctrl;


	public BowlingBallButton(GameObject obj, BowlingAlleyCtrl ctrl) : base(obj) {

		this.ctrl = ctrl;
	}

	public override void hover() {
		// actually take the bowling ball in your hand, if the distance is short enough
	}

	public override void trigger() {
		// let go of the bowling ball in your hand
	}

}

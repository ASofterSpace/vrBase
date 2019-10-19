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

	private GameObject xPart1;
	private GameObject xPart2;
	private GameObject oPart1;
	private GameObject[] oParts;


	public TicTacToeButton(GameObject obj, string buttonName, TicTacToeCtrl ctrl) : base(obj, buttonName) {

		this.ctrl = ctrl;

		xPart1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		xPart1.transform.parent = obj.transform;
		xPart1.transform.localPosition = new Vector3(0, 1.5f, 0);
		xPart1.transform.localEulerAngles = new Vector3(0, 45, 0);
		xPart1.transform.localScale = new Vector3(0.1f, 1, 0.8f);
		MaterialCtrl.setMaterial(xPart1, MaterialCtrl.PLASTIC_BLACK);

		xPart2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		xPart2.transform.parent = obj.transform;
		xPart2.transform.localPosition = new Vector3(0, 1.5f, 0);
		xPart2.transform.localEulerAngles = new Vector3(0, -45, 0);
		xPart2.transform.localScale = new Vector3(0.1f, 1, 0.8f);
		MaterialCtrl.setMaterial(xPart2, MaterialCtrl.PLASTIC_BLACK);

		oPart1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		oPart1.transform.parent = obj.transform;
		oPart1.transform.localPosition = new Vector3(-0.3f, 1.5f, -0.06f);
		oPart1.transform.localEulerAngles = new Vector3(0, -11.25f, 0);
		oPart1.transform.localScale = new Vector3(0.1f, 1, 0.14f);
		MaterialCtrl.setMaterial(oPart1, MaterialCtrl.PLASTIC_BLACK);

		oParts = ObjectFactory.axisHexadeciplize(oPart1);

		recolorize();
	}

	public void reset() {

		state = 0;

		// we set active in case we were blinking before as we were one of the buttons
		// that won the game, and now we are being reset... so that we want to stay visible
		gameObject.SetActive(true);

		recolorize();
	}

	public void setActive(bool visible) {
		gameObject.SetActive(visible);
	}

	public override void trigger() {

		// if it is our turn and the button has not yet been pressed by anyone...
		if (ctrl.isHumansTurn() && (state == 0)) {

			// ... then press it!
			setHuman();

			// and tell the controller that it is the robot's turn now!
			ctrl.makeRoboMove();

		}
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

		SoundCtrl.playSound(gameObject, SoundCtrl.KLACK_9);

		state = 1;

		recolorize();
	}

	public void setHuman() {

		SoundCtrl.playSound(gameObject, SoundCtrl.KLACK_9);

		state = 2;

		recolorize();
	}

	private void recolorize() {

		xPart1.SetActive(false);
		xPart2.SetActive(false);
		oPart1.SetActive(false);
		foreach (GameObject oPart in oParts) {
			oPart.SetActive(false);
		}

		switch (state) {
			case 0:
				MaterialCtrl.setMaterial(gameObject, MaterialCtrl.OBJECTS_TICTACTOE_GRAY);
				break;
			case 1:
				MaterialCtrl.setMaterial(gameObject, MaterialCtrl.OBJECTS_TICTACTOE_RED);
				xPart1.SetActive(true);
				xPart2.SetActive(true);
				break;
			case 2:
				MaterialCtrl.setMaterial(gameObject, MaterialCtrl.OBJECTS_TICTACTOE_BLUE);
				oPart1.SetActive(true);
				foreach (GameObject oPart in oParts) {
					oPart.SetActive(true);
				}
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

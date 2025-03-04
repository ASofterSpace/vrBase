﻿/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class NostalgicConsoleCtrl : UpdateableCtrl, ResetteableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;
	private GameObject[] countdownLabels;

	private RocketLaunchCtrl rocketLaunchCtrl;

	// -2 is no countdown, -1 is countdown soon over, 0 is liftoff, 1 .. 10 are the numbers
	private int countdown;
	private float lastCountdownStart;


	public NostalgicConsoleCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createNostalgicConsole(position, angles);

		reset();
	}

	public void update(VrInput input) {

		if (countdown >= -1) {

			if (Time.time - lastCountdownStart > 1) {

				if (countdown + 1 <= 10) {
					countdownLabels[countdown + 1].SetActive(false);
				}
				if (countdown >= 0) {
					countdownLabels[countdown].SetActive(true);
				}
				if (countdown == 0) {
					if (rocketLaunchCtrl != null) {
						rocketLaunchCtrl.launchRocket();
					}
				}

				countdown--;

				lastCountdownStart += 1;
			}
		}
	}

	private void createNostalgicConsole(Vector3 position, Vector3 angles) {

		GameObject curObj;

		GameObject nostalgicConsole = new GameObject("Nostalgic Console");
		nostalgicConsole.transform.parent = hostRoom.transform;

		GameObject walls = new GameObject("walls");
		walls.transform.parent = nostalgicConsole.transform;
		walls.transform.localPosition = new Vector3(0, 0, 0);

		GameObject wallForwardBottom = createNostalgicConsoleWallPanel(walls);
		wallForwardBottom.transform.localPosition = new Vector3(0, 0.3f, -0.04f);
		wallForwardBottom.transform.localEulerAngles = new Vector3(0, 0, 0);
		wallForwardBottom.transform.localScale = new Vector3(2, 0.6f, 0.05f);

		GameObject wallForwardSlanted = createNostalgicConsoleWallPanel(walls);
		wallForwardSlanted.transform.localPosition = new Vector3(0, 0.7f, -0.13f);
		wallForwardSlanted.transform.localEulerAngles = new Vector3(-45, 0, 0);
		wallForwardSlanted.transform.localScale = new Vector3(2, 0.5f, 0.05f);

		GameObject wallForwardFront = createNostalgicConsoleWallPanel(walls);
		wallForwardFront.transform.localPosition = new Vector3(0, 0.9f, -0.3f);
		wallForwardFront.transform.localEulerAngles = new Vector3(0, 0, 0);
		wallForwardFront.transform.localScale = new Vector3(2, 0.1f, 0.05f);

		GameObject wallKeyboards = createNostalgicConsoleWallPanel(walls);
		wallKeyboards.transform.localPosition = new Vector3(0, 1, 0);
		wallKeyboards.transform.localEulerAngles = new Vector3(75, 0, 0);
		wallKeyboards.transform.localScale = new Vector3(2, 0.6f, 0.05f);

		GameObject wallScreens = createNostalgicConsoleWallPanel(walls);
		wallScreens.transform.localPosition = new Vector3(0, 1.39f, 0.3f);
		wallScreens.transform.localEulerAngles = new Vector3(10, 0, 0);
		wallScreens.transform.localScale = new Vector3(2, 0.65f, 0.05f);

		GameObject wallTop = createNostalgicConsoleWallPanel(walls);
		wallTop.transform.localPosition = new Vector3(0, 1.7f, 0.65f);
		wallTop.transform.localEulerAngles = new Vector3(90, 0, 0);
		wallTop.transform.localScale = new Vector3(2, 0.6f, 0.05f);

		GameObject wallBack = createNostalgicConsoleWallPanel(walls);
		wallBack.transform.localPosition = new Vector3(0, 0.85f, 0.95f);
		wallBack.transform.localEulerAngles = new Vector3(0, 0, 0);
		wallBack.transform.localScale = new Vector3(2, 1.7f, 0.05f);

		GameObject wallSideRight = createNostalgicConsoleWallSide(walls, "wallSideRight");
		wallSideRight.transform.localPosition = new Vector3(1, 0, 0);

		GameObject wallSideLeft = createNostalgicConsoleWallSide(walls, "wallSideLeft");
		wallSideLeft.transform.localPosition = new Vector3(-1, 0, 0);

		GameObject screenLeft = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenLeft.name = "Screen Left";
		screenLeft.transform.parent = nostalgicConsole.transform;
		screenLeft.transform.localPosition = new Vector3(-0.65f, 1.4f, 0.27f);
		screenLeft.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenLeft.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenLeft, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject screenLeftLogo = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenLeftLogo.name = "Screen Left Logo";
		screenLeftLogo.transform.parent = nostalgicConsole.transform;
		screenLeftLogo.transform.localPosition = new Vector3(-0.76f, 1.592f, 0.303f);
		screenLeftLogo.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenLeftLogo.transform.localScale = new Vector3(0.25f, 0.07f, 1);
		MaterialCtrl.setMaterial(screenLeftLogo, MaterialCtrl.OBJECTS_LOGOS_ASOFTERSPACE_DARK);

		GameObject screenMiddle = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenMiddle.name = "Screen Middle";
		screenMiddle.transform.parent = nostalgicConsole.transform;
		screenMiddle.transform.localPosition = new Vector3(0, 1.4f, 0.27f);
		screenMiddle.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenMiddle.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenMiddle, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject screenRight = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenRight.name = "Screen Right";
		screenRight.transform.parent = nostalgicConsole.transform;
		screenRight.transform.localPosition = new Vector3(0.65f, 1.4f, 0.27f);
		screenRight.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenRight.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenRight, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		countdownLabels = new GameObject[11];
		for (int i = 0; i < 11; i++) {
			countdownLabels[i] = createCountdownLabel(nostalgicConsole, i);
		}

		// create buttons
		GameObject buttons = new GameObject("buttons");
		buttons.transform.parent = nostalgicConsole.transform;
		buttons.transform.localPosition = new Vector3(0, 0, 0);
		buttons.transform.localEulerAngles = new Vector3(0, 0, 0);

		// create red button
		GameObject buttonRedAlert = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonRedAlert.transform.parent = buttons.transform;
		buttonRedAlert.transform.localPosition = new Vector3(0.8f, 1.05f, 0);
		buttonRedAlert.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonRedAlert.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonRedAlert, MaterialCtrl.PLASTIC_RED);
		Button btnRedAlert = new DefaultButton(
			buttonRedAlert,
			() => {
				startRocketLaunchCountdown();
			}
		);
		ButtonCtrl.add(btnRedAlert);

		GameObject buttonRedAlertFrame = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonRedAlertFrame.transform.parent = buttons.transform;
		buttonRedAlertFrame.transform.localPosition = new Vector3(0.8f, 1.03f, 0.006f);
		buttonRedAlertFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonRedAlertFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonRedAlertFrame, MaterialCtrl.PLASTIC_WHITE);

		// create color buttons
		createMidColorButton(buttons, -0.2779f, new Color(1.0f, 1.0f, 1.0f, 1.0f), new Color(0.6771f, 0.5327f, 0.83015f, 1.0f));
		createMidColorButton(buttons, -0.1679f, new Color(1.0f, 0.0f, 0.0f, 1.0f), new Color(0.3542f, 0.0654f, 0.6603f, 1.0f));
		createMidColorButton(buttons, -0.0579f, new Color(0.6f, 0.0f, 0.7f, 1.0f), new Color(0.6771f, 0.5327f, 0.83015f, 1.0f));
		createMidColorButton(buttons,  0.0521f, new Color(0.0f, 0.0f, 0.0f, 1.0f), new Color(0.3542f, 0.0654f, 0.6603f, 1.0f));
		createLowColorButton(buttons, -0.2779f, new Color(0.1f, 0.6f, 0.3f, 1.0f), new Color(0.6771f, 0.5327f, 0.83015f, 1.0f));
		createLowColorButton(buttons, -0.1679f, new Color(0.0f, 0.2f, 0.9f, 1.0f), new Color(0.3542f, 0.0654f, 0.6603f, 1.0f));
		createLowColorButton(buttons, -0.0579f, new Color(0.5f, 0.5f, 0.5f, 1.0f), new Color(0.6771f, 0.5327f, 0.83015f, 1.0f));
		createLowColorButton(buttons,  0.0521f, new Color(0.6f, 0.4f, 0.1f, 1.0f), new Color(0.3542f, 0.0654f, 0.6603f, 1.0f));

		// create up arrow button
		GameObject buttonUpArrowHolder = new GameObject();
		buttonUpArrowHolder.transform.parent = buttons.transform;
		GameObject buttonUpArrowLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpArrowLeft.transform.parent = buttonUpArrowHolder.transform;
		buttonUpArrowLeft.transform.localPosition = new Vector3(-0.8176f, 1.0542f, 0.0166f);
		buttonUpArrowLeft.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonUpArrowLeft.transform.localScale = new Vector3(0.03f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonUpArrowLeft, MaterialCtrl.PLASTIC_BLACK);
		GameObject buttonUpArrowRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpArrowRight.transform.parent = buttonUpArrowHolder.transform;
		buttonUpArrowRight.transform.localPosition = new Vector3(-0.7827f, 1.0543f, 0.0173f);
		buttonUpArrowRight.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonUpArrowRight.transform.localScale = new Vector3(0.08f, 0.03f, 0.03f);
		MaterialCtrl.setMaterial(buttonUpArrowRight, MaterialCtrl.PLASTIC_BLACK);
		GameObject buttonUpNeck = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpNeck.transform.parent = buttonUpArrowHolder.transform;
		buttonUpNeck.transform.localPosition = new Vector3(-0.8f, 1.0386f, -0.0446f);
		buttonUpNeck.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonUpNeck.transform.localScale = new Vector3(0.03f, 0.03f, 0.1307552f);
		MaterialCtrl.setMaterial(buttonUpNeck, MaterialCtrl.PLASTIC_BLACK);
		Button btnUp = new DefaultButton(
			buttonUpArrowHolder,
			() => {
				VrSpecificCtrl vrSpecificCtrl = mainCtrl.getVrSpecificCtrl();
				if (vrSpecificCtrl != null) {
					vrSpecificCtrl.adjustCameraHeightBy(0.1f);
				}
			}
		);
		ButtonCtrl.add(btnUp);

		GameObject buttonUpArrowLeftFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpArrowLeftFrame.transform.parent = buttons.transform;
		buttonUpArrowLeftFrame.transform.localPosition = new Vector3(-0.8177f, 1.0342f, 0.0226f);
		buttonUpArrowLeftFrame.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonUpArrowLeftFrame.transform.localScale = new Vector3(0.05f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonUpArrowLeftFrame, MaterialCtrl.PLASTIC_WHITE);
		GameObject buttonUpArrowRightFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpArrowRightFrame.transform.parent = buttons.transform;
		buttonUpArrowRightFrame.transform.localPosition = new Vector3(-0.7829f, 1.0342f, 0.0231f);
		buttonUpArrowRightFrame.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonUpArrowRightFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.05f);
		MaterialCtrl.setMaterial(buttonUpArrowRightFrame, MaterialCtrl.PLASTIC_WHITE);
		GameObject buttonUpNeckFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpNeckFrame.transform.parent = buttons.transform;
		buttonUpNeckFrame.transform.localPosition = new Vector3(-0.8f, 1.0155f, -0.0527f);
		buttonUpNeckFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonUpNeckFrame.transform.localScale = new Vector3(0.05f, 0.005f, 0.12386f);
		MaterialCtrl.setMaterial(buttonUpNeckFrame, MaterialCtrl.PLASTIC_WHITE);

		// create down arrow button
		GameObject buttonDownArrowHolder = new GameObject();
		buttonDownArrowHolder.transform.parent = buttons.transform;
		GameObject buttonDownArrowLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownArrowLeft.transform.parent = buttonDownArrowHolder.transform;
		buttonDownArrowLeft.transform.localPosition = new Vector3(-0.6667f, 1.0299f, -0.0807f);
		buttonDownArrowLeft.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonDownArrowLeft.transform.localScale = new Vector3(0.08f, 0.03f, 0.03f);
		MaterialCtrl.setMaterial(buttonDownArrowLeft, MaterialCtrl.PLASTIC_BLACK);
		GameObject buttonDownArrowRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownArrowRight.transform.parent = buttonDownArrowHolder.transform;
		buttonDownArrowRight.transform.localPosition = new Vector3(-0.6309f, 1.0298f, -0.0806f);
		buttonDownArrowRight.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonDownArrowRight.transform.localScale = new Vector3(0.03f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonDownArrowRight, MaterialCtrl.PLASTIC_BLACK);
		GameObject buttonDownNeck = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownNeck.transform.parent = buttonDownArrowHolder.transform;
		buttonDownNeck.transform.localPosition = new Vector3(-0.65f, 1.0433f, -0.0254f);
		buttonDownNeck.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonDownNeck.transform.localScale = new Vector3(0.03f, 0.03f, 0.126144f);
		MaterialCtrl.setMaterial(buttonDownNeck, MaterialCtrl.PLASTIC_BLACK);
		Button btnDown = new DefaultButton(
			buttonDownArrowHolder,
			() => {
				VrSpecificCtrl vrSpecificCtrl = mainCtrl.getVrSpecificCtrl();
				if (vrSpecificCtrl != null) {
					vrSpecificCtrl.adjustCameraHeightBy(-0.1f);
				}
			}
		);
		ButtonCtrl.add(btnDown);

		GameObject buttonDownArrowLeftFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownArrowLeftFrame.transform.parent = buttons.transform;
		buttonDownArrowLeftFrame.transform.localPosition = new Vector3(-0.6668f, 1.0103f, -0.0727f);
		buttonDownArrowLeftFrame.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonDownArrowLeftFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.05f);
		MaterialCtrl.setMaterial(buttonDownArrowLeftFrame, MaterialCtrl.PLASTIC_WHITE);
		GameObject buttonDownArrowRightFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownArrowRightFrame.transform.parent = buttons.transform;
		buttonDownArrowRightFrame.transform.localPosition = new Vector3(-0.6314f, 1.0101f, -0.0726f);
		buttonDownArrowRightFrame.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonDownArrowRightFrame.transform.localScale = new Vector3(0.05f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonDownArrowRightFrame, MaterialCtrl.PLASTIC_WHITE);
		GameObject buttonDownNeckFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownNeckFrame.transform.parent = buttons.transform;
		buttonDownNeckFrame.transform.localPosition = new Vector3(-0.65f, 1.0271f, -0.0093f);
		buttonDownNeckFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonDownNeckFrame.transform.localScale = new Vector3(0.05f, 0.005f, 0.13366f);
		MaterialCtrl.setMaterial(buttonDownNeckFrame, MaterialCtrl.PLASTIC_WHITE);

		// create reset button - hidden inside the console
		GameObject resetButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
		resetButton.transform.parent = buttons.transform;
		resetButton.transform.localPosition = new Vector3(0, 0, 0.59f);
		resetButton.transform.localEulerAngles = new Vector3(0, 0, 0);
		resetButton.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		MaterialCtrl.setMaterial(resetButton, MaterialCtrl.PLASTIC_RED);
		Button btnReset = new DefaultButton(
			resetButton,
			() => {
				mainCtrl.reset();
			}
		);
		ButtonCtrl.add(btnReset);

		GameObject resetButtonFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		resetButtonFrame.transform.parent = buttons.transform;
		resetButtonFrame.transform.localPosition = new Vector3(0, 0, 0.59f);
		resetButtonFrame.transform.localEulerAngles = new Vector3(0, 0, 0);
		resetButtonFrame.transform.localScale = new Vector3(0.12f, 0.01f, 0.12f);
		MaterialCtrl.setMaterial(resetButtonFrame, MaterialCtrl.PLASTIC_WHITE);


		nostalgicConsole.transform.localPosition = position;
		nostalgicConsole.transform.localEulerAngles = angles;
	}

	private GameObject createCountdownLabel(GameObject parent, int i) {

		GameObject curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "Label " + i;
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(0.65f, 1.4f, 0.2699f);
		curObj.transform.localEulerAngles = new Vector3(10, 0, 0);
		curObj.transform.localScale = new Vector3(0.345f, 0.172f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_SCREENS_LABELS_LIFTOFF + i);
		curObj.SetActive(false);

		return curObj;
	}

	private void createMidColorButton(GameObject parent, float x, Color beamColor, Color wallColor) {

		GameObject curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(x, 1.0433f, -0.0254f);
		curObj.transform.localEulerAngles = new Vector3(-15, 0, 0);
		curObj.transform.localScale = new Vector3(0.08f, 0.03f, 0.04f);
		MaterialCtrl.setColor(curObj, beamColor);
		ButtonCtrl.add(new ColorizeButton(curObj, beamColor, wallColor));

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Button Frame";
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(x, 1.0235f, -0.0201f);
		curObj.transform.localEulerAngles = new Vector3(-15, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 0.005f, 0.06f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);
	}

	private void createLowColorButton(GameObject parent, float x, Color beamColor, Color wallColor) {

		GameObject curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(x, 1.02f, -0.111f);
		curObj.transform.localEulerAngles = new Vector3(-15, 0, 0);
		curObj.transform.localScale = new Vector3(0.08f, 0.03f, 0.04f);
		MaterialCtrl.setColor(curObj, beamColor);
		ButtonCtrl.add(new ColorizeButton(curObj, beamColor, wallColor));

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Button Frame";
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(x, 1.0002f, -0.1057f);
		curObj.transform.localEulerAngles = new Vector3(-15, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 0.005f, 0.06f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);
	}

	private GameObject createNostalgicConsoleWallSide(GameObject wallParent, string wallSideName) {

		GameObject wallSideMain = new GameObject(wallSideName);
		wallSideMain.transform.parent = wallParent.transform;
		wallSideMain.transform.localEulerAngles = new Vector3(0, 0, 0);

		GameObject wallSide = createNostalgicConsoleWallPanel(wallSideMain);
		wallSide.transform.localPosition = new Vector3(0, 0.85f, 0.65f);
		wallSide.transform.localEulerAngles = new Vector3(0, 90, 0);
		wallSide.transform.localScale = new Vector3(0.6f, 1.7f, 0.05f);

		GameObject wallSideForward = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForward.transform.localPosition = new Vector3(0, 0.49f, 0.17f);
		wallSideForward.transform.localEulerAngles = new Vector3(0, 90, 0);
		wallSideForward.transform.localScale = new Vector3(0.4f, 0.98f, 0.05f);

		GameObject wallSideTop = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideTop.transform.localPosition = new Vector3(0, 1.18f, 0.36f);
		wallSideTop.transform.localEulerAngles = new Vector3(0, 90, 10);
		wallSideTop.transform.localScale = new Vector3(0.2f, 1, 0.05f);

		GameObject wallSideForwardTop = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForwardTop.transform.localPosition = new Vector3(0, 0.95f, 0);
		wallSideForwardTop.transform.localEulerAngles = new Vector3(0, 90, 75);
		wallSideForwardTop.transform.localScale = new Vector3(0.1f, 0.6f, 0.05f);

		GameObject wallSideForwardBottom = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForwardBottom.transform.localPosition = new Vector3(0, 0.7f, -0.05f);
		wallSideForwardBottom.transform.localEulerAngles = new Vector3(0, 90, 45);
		wallSideForwardBottom.transform.localScale = new Vector3(0.5f, 0.13f, 0.05f);

		GameObject wallSideForwardMiddle = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForwardMiddle.transform.localPosition = new Vector3(0, 0.85f, -0.05f);
		wallSideForwardMiddle.transform.localEulerAngles = new Vector3(0, 90, 0);
		wallSideForwardMiddle.transform.localScale = new Vector3(0.2f, 0.2f, 0.05f);

		return wallSideMain;
	}

	private GameObject createNostalgicConsoleWallPanel(GameObject wallParent) {
		GameObject wallPanel = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wallPanel.transform.parent = wallParent.transform;
		MaterialCtrl.setMaterial(wallPanel, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_GREEN);
		return wallPanel;
	}

	public void setRocketLaunchCtrl(RocketLaunchCtrl rocketLaunchCtrl) {
		this.rocketLaunchCtrl = rocketLaunchCtrl;
	}

	public void reset() {

		MaterialCtrl.setColor(MaterialCtrl.BUILDING_BEAM_WHITE, new Color(1.0f, 1.0f, 1.0f, 1.0f));

		MaterialCtrl.setColor(MaterialCtrl.BUILDING_WALL, new Color(0.6771f, 0.5327f, 0.83015f, 1.0f));

		countdown = -2;

		foreach (GameObject countdownLabel in countdownLabels) {
			countdownLabel.SetActive(false);
		}
	}

	private void startRocketLaunchCountdown() {

		if (rocketLaunchCtrl != null) {

			if (rocketLaunchCtrl.startRocketNext()) {

				lastCountdownStart = Time.time;

				countdown = 10;

			} else {

				// if we are not starting, but instead landing, then just do that without countdown :)
				rocketLaunchCtrl.launchRocket();
			}
		}
	}

}

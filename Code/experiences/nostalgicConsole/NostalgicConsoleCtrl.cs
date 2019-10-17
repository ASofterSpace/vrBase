/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class NostalgicConsoleCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;


	public NostalgicConsoleCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;
	}

	public void createNostalgicConsole(Vector3 position, Vector3 angles) {

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
		screenLeft.transform.parent = nostalgicConsole.transform;
		screenLeft.transform.localPosition = new Vector3(-0.65f, 1.4f, 0.27f);
		screenLeft.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenLeft.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenLeft, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject screenMiddle = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenMiddle.transform.parent = nostalgicConsole.transform;
		screenMiddle.transform.localPosition = new Vector3(0, 1.4f, 0.27f);
		screenMiddle.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenMiddle.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenMiddle, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject screenRight = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenRight.transform.parent = nostalgicConsole.transform;
		screenRight.transform.localPosition = new Vector3(0.65f, 1.4f, 0.27f);
		screenRight.transform.localEulerAngles = new Vector3(10, 0, 0);
		screenRight.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenRight, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject buttons = new GameObject("buttons");
		buttons.transform.parent = nostalgicConsole.transform;
		buttons.transform.localPosition = new Vector3(0, 0, 0);
		buttons.transform.localEulerAngles = new Vector3(0, 0, 0);

		GameObject buttonRedAlert = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonRedAlert.transform.parent = buttons.transform;
		buttonRedAlert.transform.localPosition = new Vector3(0.8f, 1.05f, 0);
		buttonRedAlert.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonRedAlert.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonRedAlert, MaterialCtrl.PLASTIC_RED);
		Button btnRedAlert = new ColorizeButton(
			buttonRedAlert,
			ButtonCtrl.BTN_NOSTALGICCONSOLE_BIG_RED,
			new Color(1.0f, 0.0f, 0.0f, 1.0f)
		);
		ButtonCtrl.add(btnRedAlert);

		GameObject buttonRedAlertFrame = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonRedAlertFrame.transform.parent = buttons.transform;
		buttonRedAlertFrame.transform.localPosition = new Vector3(0.8f, 1.03f, 0.006f);
		buttonRedAlertFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonRedAlertFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonRedAlertFrame, MaterialCtrl.PLASTIC_WHITE);

		GameObject buttonColorizeWhite = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonColorizeWhite.transform.parent = buttons.transform;
		buttonColorizeWhite.transform.localPosition = new Vector3(0.6f, 1.05f, 0);
		buttonColorizeWhite.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonColorizeWhite.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonColorizeWhite, MaterialCtrl.PLASTIC_WHITE);
		Button btnWhite = new ColorizeButton(
			buttonColorizeWhite,
			ButtonCtrl.BTN_NOSTALGICCONSOLE_BIG_WHITE,
			new Color(1.0f, 1.0f, 1.0f, 1.0f)
		);
		ButtonCtrl.add(btnWhite);

		GameObject buttonColorizeWhiteFrame = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonColorizeWhiteFrame.transform.parent = buttons.transform;
		buttonColorizeWhiteFrame.transform.localPosition = new Vector3(0.6f, 1.03f, 0.006f);
		buttonColorizeWhiteFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonColorizeWhiteFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonColorizeWhiteFrame, MaterialCtrl.PLASTIC_WHITE);

		GameObject buttonUpArrow = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpArrow.transform.parent = buttons.transform;
		buttonUpArrow.transform.localPosition = new Vector3(-0.8f, 1.05f, 0);
		buttonUpArrow.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonUpArrow.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonUpArrow, MaterialCtrl.PLASTIC_BLACK);
		GameObject buttonUpNeck = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpNeck.transform.parent = buttons.transform;
		buttonUpNeck.transform.localPosition = new Vector3(-0.8f, 1.03175f, -0.07f);
		buttonUpNeck.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonUpNeck.transform.localScale = new Vector3(0.04f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonUpNeck, MaterialCtrl.PLASTIC_BLACK);
		Button btnUp = new UpDownButton(
			buttonUpArrow,
			buttonUpNeck,
			ButtonCtrl.BTN_NOSTALGICCONSOLE_UP,
			mainCtrl,
			0.1f
		);
		ButtonCtrl.add(btnUp);

		GameObject buttonUpArrowFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpArrowFrame.transform.parent = buttons.transform;
		buttonUpArrowFrame.transform.localPosition = new Vector3(-0.8f, 1.03f, 0.006f);
		buttonUpArrowFrame.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonUpArrowFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonUpArrowFrame, MaterialCtrl.PLASTIC_WHITE);
		GameObject buttonUpNeckFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonUpNeckFrame.transform.parent = buttons.transform;
		buttonUpNeckFrame.transform.localPosition = new Vector3(-0.8f, 1.0125f, -0.064f);
		buttonUpNeckFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonUpNeckFrame.transform.localScale = new Vector3(0.06f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonUpNeckFrame, MaterialCtrl.PLASTIC_WHITE);

		GameObject buttonDownArrow = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownArrow.transform.parent = buttons.transform;
		buttonDownArrow.transform.localPosition = new Vector3(-0.6493f, 1.0342f, -0.0633f);
		buttonDownArrow.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonDownArrow.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonDownArrow, MaterialCtrl.PLASTIC_BLACK);
		GameObject buttonDownNeck = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownNeck.transform.parent = buttons.transform;
		buttonDownNeck.transform.localPosition = new Vector3(-0.65f, 1.049f, -0.004f);
		buttonDownNeck.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonDownNeck.transform.localScale = new Vector3(0.04f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonDownNeck, MaterialCtrl.PLASTIC_BLACK);
		Button btnDown = new UpDownButton(
			buttonDownArrow,
			buttonDownNeck,
			ButtonCtrl.BTN_NOSTALGICCONSOLE_DOWN,
			mainCtrl,
			-0.1f
		);
		ButtonCtrl.add(btnDown);

		GameObject buttonDownArrowFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownArrowFrame.transform.parent = buttons.transform;
		buttonDownArrowFrame.transform.localPosition = new Vector3(-0.6496f, 1.0145f, -0.0555f);
		buttonDownArrowFrame.transform.localEulerAngles = new Vector3(-10, 45, -10);
		buttonDownArrowFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonDownArrowFrame, MaterialCtrl.PLASTIC_WHITE);
		GameObject buttonDownNeckFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
		buttonDownNeckFrame.transform.parent = buttons.transform;
		buttonDownNeckFrame.transform.localPosition = new Vector3(-0.65f, 1.0318f, 0.0083f);
		buttonDownNeckFrame.transform.localEulerAngles = new Vector3(-15, 0, 0);
		buttonDownNeckFrame.transform.localScale = new Vector3(0.06f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonDownNeckFrame, MaterialCtrl.PLASTIC_WHITE);

		nostalgicConsole.transform.localPosition = position;
		nostalgicConsole.transform.localEulerAngles = angles;
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

}

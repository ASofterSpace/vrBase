/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class NostalgicConsoleCtrl {

	private GameObject hostRoom;


	public NostalgicConsoleCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

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
		wallForwardBottom.transform.eulerAngles = new Vector3(0, 0, 0);
		wallForwardBottom.transform.localScale = new Vector3(2, 0.6f, 0.05f);

		GameObject wallForwardSlanted = createNostalgicConsoleWallPanel(walls);
		wallForwardSlanted.transform.localPosition = new Vector3(0, 0.7f, -0.13f);
		wallForwardSlanted.transform.eulerAngles = new Vector3(-45, 0, 0);
		wallForwardSlanted.transform.localScale = new Vector3(2, 0.5f, 0.05f);

		GameObject wallForwardFront = createNostalgicConsoleWallPanel(walls);
		wallForwardFront.transform.localPosition = new Vector3(0, 0.9f, -0.3f);
		wallForwardFront.transform.eulerAngles = new Vector3(0, 0, 0);
		wallForwardFront.transform.localScale = new Vector3(2, 0.1f, 0.05f);

		GameObject wallKeyboards = createNostalgicConsoleWallPanel(walls);
		wallKeyboards.transform.localPosition = new Vector3(0, 1, 0);
		wallKeyboards.transform.eulerAngles = new Vector3(75, 0, 0);
		wallKeyboards.transform.localScale = new Vector3(2, 0.6f, 0.05f);

		GameObject wallScreens = createNostalgicConsoleWallPanel(walls);
		wallScreens.transform.localPosition = new Vector3(0, 1.39f, 0.3f);
		wallScreens.transform.eulerAngles = new Vector3(10, 0, 0);
		wallScreens.transform.localScale = new Vector3(2, 0.65f, 0.05f);

		GameObject wallTop = createNostalgicConsoleWallPanel(walls);
		wallTop.transform.localPosition = new Vector3(0, 1.7f, 0.65f);
		wallTop.transform.eulerAngles = new Vector3(90, 0, 0);
		wallTop.transform.localScale = new Vector3(2, 0.6f, 0.05f);

		GameObject wallBack = createNostalgicConsoleWallPanel(walls);
		wallBack.transform.localPosition = new Vector3(0, 0.85f, 0.95f);
		wallBack.transform.eulerAngles = new Vector3(0, 0, 0);
		wallBack.transform.localScale = new Vector3(2, 1.7f, 0.05f);

		GameObject wallSideRight = createNostalgicConsoleWallSide(walls, "wallSideRight");
		wallSideRight.transform.localPosition = new Vector3(1, 0, 0);

		GameObject wallSideLeft = createNostalgicConsoleWallSide(walls, "wallSideLeft");
		wallSideLeft.transform.localPosition = new Vector3(-1, 0, 0);

		GameObject screenLeft = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenLeft.transform.parent = nostalgicConsole.transform;
		screenLeft.transform.localPosition = new Vector3(-0.65f, 1.4f, 0.27f);
		screenLeft.transform.eulerAngles = new Vector3(10, 0, 0);
		screenLeft.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenLeft, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject screenMiddle = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenMiddle.transform.parent = nostalgicConsole.transform;
		screenMiddle.transform.localPosition = new Vector3(0, 1.4f, 0.27f);
		screenMiddle.transform.eulerAngles = new Vector3(10, 0, 0);
		screenMiddle.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenMiddle, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject screenRight = GameObject.CreatePrimitive(PrimitiveType.Quad);
		screenRight.transform.parent = nostalgicConsole.transform;
		screenRight.transform.localPosition = new Vector3(0.65f, 1.4f, 0.27f);
		screenRight.transform.eulerAngles = new Vector3(10, 0, 0);
		screenRight.transform.localScale = new Vector3(0.55f, 0.55f, 1);
		MaterialCtrl.setMaterial(screenRight, MaterialCtrl.OBJECTS_NOSTALGICCONSOLE_SCREEN);

		GameObject buttons = new GameObject("buttons");
		buttons.transform.parent = nostalgicConsole.transform;
		buttons.transform.localPosition = new Vector3(0, 0, 0);
		buttons.transform.eulerAngles = new Vector3(0, 0, 0);

		GameObject buttonRedAlert = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		ButtonCtrl.add(buttonRedAlert, ButtonCtrl.BTN_NOSTALGICCONSOLE_BIG_RED);
		buttonRedAlert.transform.parent = buttons.transform;
		buttonRedAlert.transform.localPosition = new Vector3(0.8f, 1.05f, 0);
		buttonRedAlert.transform.eulerAngles = new Vector3(-15, 0, 0);
		buttonRedAlert.transform.localScale = new Vector3(0.08f, 0.03f, 0.08f);
		MaterialCtrl.setMaterial(buttonRedAlert, MaterialCtrl.PLASTIC_RED);

		GameObject buttonRedAlertFrame = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		buttonRedAlertFrame.transform.parent = buttons.transform;
		buttonRedAlertFrame.transform.localPosition = new Vector3(0.8f, 1.03f, 0.006f);
		buttonRedAlertFrame.transform.eulerAngles = new Vector3(-15, 0, 0);
		buttonRedAlertFrame.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
		MaterialCtrl.setMaterial(buttonRedAlertFrame, MaterialCtrl.PLASTIC_WHITE);

		nostalgicConsole.transform.localPosition = position;
		nostalgicConsole.transform.eulerAngles = angles;
	}

	private GameObject createNostalgicConsoleWallSide(GameObject wallParent, string wallSideName) {

		GameObject wallSideMain = new GameObject(wallSideName);
		wallSideMain.transform.parent = wallParent.transform;
		wallSideMain.transform.eulerAngles = new Vector3(0, 0, 0);

		GameObject wallSide = createNostalgicConsoleWallPanel(wallSideMain);
		wallSide.transform.localPosition = new Vector3(0, 0.85f, 0.65f);
		wallSide.transform.eulerAngles = new Vector3(0, 90, 0);
		wallSide.transform.localScale = new Vector3(0.6f, 1.7f, 0.05f);

		GameObject wallSideForward = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForward.transform.localPosition = new Vector3(0, 0.49f, 0.17f);
		wallSideForward.transform.eulerAngles = new Vector3(0, 90, 0);
		wallSideForward.transform.localScale = new Vector3(0.4f, 0.98f, 0.05f);

		GameObject wallSideTop = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideTop.transform.localPosition = new Vector3(0, 1.18f, 0.36f);
		wallSideTop.transform.eulerAngles = new Vector3(0, 90, 10);
		wallSideTop.transform.localScale = new Vector3(0.2f, 1, 0.05f);

		GameObject wallSideForwardTop = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForwardTop.transform.localPosition = new Vector3(0, 0.95f, 0);
		wallSideForwardTop.transform.eulerAngles = new Vector3(0, 90, 75);
		wallSideForwardTop.transform.localScale = new Vector3(0.1f, 0.6f, 0.05f);

		GameObject wallSideForwardBottom = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForwardBottom.transform.localPosition = new Vector3(0, 0.7f, -0.05f);
		wallSideForwardBottom.transform.eulerAngles = new Vector3(0, 90, 45);
		wallSideForwardBottom.transform.localScale = new Vector3(0.5f, 0.13f, 0.05f);

		GameObject wallSideForwardMiddle = createNostalgicConsoleWallPanel(wallSideMain);
		wallSideForwardMiddle.transform.localPosition = new Vector3(0, 0.85f, -0.05f);
		wallSideForwardMiddle.transform.eulerAngles = new Vector3(0, 90, 0);
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

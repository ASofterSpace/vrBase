/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class BowlingAlleyCtrl {

	private GameObject hostRoom;


	public BowlingAlleyCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.hostRoom = hostRoom;
	}

	public void createBowlingAlley(Vector3 position, Vector3 angles) {

		GameObject bowlingAlley = new GameObject("Bowling Alley");
		bowlingAlley.transform.parent = hostRoom.transform;

		GameObject bowlingFloor = GameObject.CreatePrimitive(PrimitiveType.Quad);
		bowlingFloor.name = TriggerCtrl.FLOOR_NAME;
		bowlingFloor.transform.parent = bowlingAlley.transform;
		bowlingFloor.transform.localPosition = new Vector3(0, 0.001f, 0);
		bowlingFloor.transform.eulerAngles = new Vector3(90, 0, 0);
		bowlingFloor.transform.localScale = new Vector3(1, 6, 1);
		MaterialCtrl.setMaterial(bowlingFloor, MaterialCtrl.BUILDING_FLOOR_WOOD);

		GameObject bowlingBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		bowlingBall.name = "redBowlingBall";
		bowlingBall.transform.parent = bowlingAlley.transform;
		bowlingBall.transform.localPosition = new Vector3(0, 0.1f, -3);
		bowlingBall.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		MaterialCtrl.setMaterial(bowlingBall, MaterialCtrl.OBJECTS_BOWLING_BALL_RED);

		GameObject pin = createBowlingPin();
		pin.transform.parent = bowlingAlley.transform;
		pin.transform.localPosition = new Vector3(0, 0, 2);

		bowlingAlley.transform.localPosition = position;
		bowlingAlley.transform.eulerAngles = angles;
	}

	private GameObject createBowlingPin() {

		GameObject pin = new GameObject("bowlingPin");

		GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		capsule.transform.parent = pin.transform;
		capsule.transform.localPosition = new Vector3(0, 0.0719f, 0);
		capsule.transform.localScale = new Vector3(0.05f, 0.08f, 0.05f);
		MaterialCtrl.setMaterial(capsule, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject neck = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		neck.transform.parent = pin.transform;
		neck.transform.localPosition = new Vector3(0, 0.1631f, 0);
		neck.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		MaterialCtrl.setMaterial(neck, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		head.transform.parent = pin.transform;
		head.transform.localPosition = new Vector3(0, 0.1961f, 0);
		head.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
		MaterialCtrl.setMaterial(head, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject redBand = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		redBand.transform.parent = pin.transform;
		redBand.transform.localPosition = new Vector3(0, 0.1009f, 0);
		redBand.transform.localScale = new Vector3(0.051f, 0.01f, 0.051f);
		MaterialCtrl.setMaterial(redBand, MaterialCtrl.OBJECTS_BOWLING_PIN_RED);

		return pin;
	}
}

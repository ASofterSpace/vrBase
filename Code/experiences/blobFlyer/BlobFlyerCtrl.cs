/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class BlobFlyerCtrl {

	private GameObject hostRoom;


	public BlobFlyerCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.hostRoom = hostRoom;
	}

	public void createBlobFlyer(Vector3 position, Vector3 angles) {

		GameObject blobFlyer = new GameObject("BlobFlyer");
		blobFlyer.transform.parent = hostRoom.transform;

		GameObject seat = new GameObject("Seat");
		seat.transform.parent = blobFlyer.transform;
		seat.transform.localPosition = new Vector3(0, 0, 0);

		GameObject cushion = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cushion.transform.parent = seat.transform;
		cushion.transform.localPosition = new Vector3(0, 0.3f, 0);
		cushion.transform.eulerAngles = new Vector3(0, 0, 0);
		cushion.transform.localScale = new Vector3(0.5f, 0.05f, 0.4f);
		MaterialCtrl.setMaterial(cushion, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject back = GameObject.CreatePrimitive(PrimitiveType.Cube);
		back.transform.parent = seat.transform;
		back.transform.localPosition = new Vector3(0, 0.5f, 0.22f);
		back.transform.eulerAngles = new Vector3(10, 0, 0);
		back.transform.localScale = new Vector3(0.5f, 0.5f, 0.05f);
		MaterialCtrl.setMaterial(back, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject stand = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		stand.transform.parent = seat.transform;
		stand.transform.localPosition = new Vector3(0, 0.1f, 0);
		stand.transform.eulerAngles = new Vector3(0, 0, 0);
		stand.transform.localScale = new Vector3(0.4f, 0.18f, 0.4f);
		MaterialCtrl.setMaterial(stand, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject console = new GameObject("Console");
		console.transform.parent = blobFlyer.transform;
		console.transform.localPosition = new Vector3(0, 0, -1);

		GameObject mainKeyboard = GameObject.CreatePrimitive(PrimitiveType.Cube);
		mainKeyboard.transform.parent = console.transform;
		mainKeyboard.transform.localPosition = new Vector3(0, 0.5f, 0);
		mainKeyboard.transform.eulerAngles = new Vector3(60, 0, 0);
		mainKeyboard.transform.localScale = new Vector3(0.8f, 0.1f, 0.8f);
		MaterialCtrl.setMaterial(mainKeyboard, MaterialCtrl.OBJECTS_BLOBFLYER_BLACK);

		GameObject chassis = new GameObject("Chassis");
		chassis.transform.parent = blobFlyer.transform;
		chassis.transform.localPosition = new Vector3(0, 0, 0);

		blobFlyer.transform.localPosition = position;
		blobFlyer.transform.eulerAngles = angles;
	}
}

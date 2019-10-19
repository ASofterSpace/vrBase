/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class RocketLaunchCtrl {

	private GameObject hostRoom;


	public RocketLaunchCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		createRocket(position, angles);
	}

	private void createRocket(Vector3 position, Vector3 angles) {

		GameObject curObj;

		GameObject rocketLauncher = new GameObject("Rocket Launcher");
		rocketLauncher.transform.parent = hostRoom.transform;
		rocketLauncher.transform.localPosition = position;
		rocketLauncher.transform.localEulerAngles = angles;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Launchpad";
		curObj.transform.parent = rocketLauncher.transform;
		curObj.transform.localPosition = new Vector3(0, 0.05f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(10, 0.05f, 10);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		GameObject rocket = new GameObject("Rocket");
		rocket.transform.parent = rocketLauncher.transform;
		rocket.transform.localPosition = new Vector3(0, 7, 0);
		rocket.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(3, 6, 3);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Upper Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 10.5f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(3, 2, 3);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Strut";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(-0.004f, 7.26f, -1.362f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, -18.161f);
		curObj.transform.localScale = new Vector3(0.1f, 1.33f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		ObjectFactory.axisHexadeciplize(curObj);
	}
}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class RocketLaunchCtrl : UpdateableCtrl {

	private GameObject hostRoom;
	private GameObject rocket;

	private bool startingRocket;
	private bool rocketGone;
	private float startTime;


	public RocketLaunchCtrl(MainCtrl mainCtrl, GameObject hostRoom,
		NostalgicConsoleCtrl nostalgicConsoleCtrl, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		nostalgicConsoleCtrl.setRocketLaunchCtrl(this);

		mainCtrl.addUpdateableCtrl(this);

		rocketGone = false;

		createRocket(position, angles);
	}

	void UpdateableCtrl.update(VrInput input) {
		if (startingRocket) {
			float t = (Time.time - startTime);
			rocket.transform.localPosition = new Vector3(0, 17 + (t * t), 0);
			if (t * t > 3000) {
				rocket.SetActive(false);
				rocketGone = true;
				startingRocket = false;
			}
		}
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
		curObj.transform.localScale = new Vector3(13, 0.05f, 13);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_ROCKETLAUNCH_LAUNCHPAD);

		rocket = new GameObject("Rocket");
		rocket.transform.parent = rocketLauncher.transform;
		rocket.transform.localPosition = new Vector3(0, 17, 0);
		rocket.transform.localEulerAngles = new Vector3(0, -45, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 15, 6);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Upper Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 25.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 8, 6);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Strut";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(-0.004f, 16.2f, -2.8f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, -18.161f);
		curObj.transform.localScale = new Vector3(0.2f, 1.33f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);
		ObjectFactory.axisTetraduplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		curObj.name = "Top Knobble";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 28.9f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(7, 7, 7);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		curObj.name = "The Round Things";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(2.75f, 22, 2.75f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_ROCKETLAUNCH_YELLOW);
		ObjectFactory.pointOctuplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(1.1f, -15.95f, 1.1f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2, 0.95f, 2);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);
		ObjectFactory.pointQuadruplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "A Softer Space Logo";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 0, -3);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(6.4f, 1.6f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_LOGOS_ASOFTERSPACE);
	}

	public void startCountdown() {

		if (!rocketGone) {
			// TODO :: actually call a countdown from 10 to lift-off

			startTime = Time.time;

			startingRocket = true;
		}
	}
}

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
	private bool landingRocket;
	private bool rocketGone;
	private float startTime;


	public RocketLaunchCtrl(MainCtrl mainCtrl, GameObject hostRoom,
		NostalgicConsoleCtrl nostalgicConsoleCtrl, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		nostalgicConsoleCtrl.setRocketLaunchCtrl(this);

		mainCtrl.addUpdateableCtrl(this);

		startingRocket = false;
		landingRocket = false;
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
		} else if (landingRocket) {
			float y = rocket.transform.localPosition.y;
			float t = (Time.time - startTime);
			y -= t * t;
			if (y < 17) {
				y = 17;
				landingRocket = false;
				rocketGone = false;
			}
			rocket.transform.localPosition = new Vector3(0, y, 0);
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
		rocket.transform.localEulerAngles = new Vector3(0, 180, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 1, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 14, 6);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Upper Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 25.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 8, 6);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = ObjectFactory.createCone(16, false, true, MaterialCtrl.PLASTIC_BLACK);
		curObj.name = "Upper Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 17.41f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2, 2, 2);

		curObj = ObjectFactory.createCone(16, false, false, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Rocket White Cone";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 36.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 3, 6);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Rocket Red Cone Base";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 36.35f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(4, 1, 4);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_RED);

		curObj = ObjectFactory.createCone(16, false, false, MaterialCtrl.PLASTIC_RED);
		curObj.name = "Rocket Red Cone";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 39.36f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(4, 2, 4);

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

		curObj = ObjectFactory.createCone(16, true, false, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Wing";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, -4.61f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1, 8, 16);
		ObjectFactory.pointDuplize90(curObj);

		curObj = ObjectFactory.createCone(16, false, true, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Side Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, -11, -3.31f);
		curObj.transform.localEulerAngles = new Vector3(13, 0, 0);
		curObj.transform.localScale = new Vector3(3, 3, 3);
		ObjectFactory.pointQuadruplize(curObj);

		curObj = ObjectFactory.createCone(16, false, true, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Main Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(1.74f, -12.8f, 1.74f);
		curObj.transform.localEulerAngles = new Vector3(5, 180, -5);
		curObj.transform.localScale = new Vector3(4, 4, 4);
		ObjectFactory.pointQuadruplize(curObj);

		curObj = ObjectFactory.createCone(16, false, false, MaterialCtrl.PLASTIC_RED);
		curObj.name = "Main Engine Stripe";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(1.87f, -14.3f, 1.87f);
		curObj.transform.localEulerAngles = new Vector3(5, 180, -5);
		curObj.transform.localScale = new Vector3(4.1f, 2.5f, 4.1f);
		ObjectFactory.pointQuadruplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "A Softer Space Logo";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 5.5f, -3);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(6.4f, 1.6f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_LOGOS_ASOFTERSPACE);
	}

	public void startCountdown() {

		if (!rocketGone && !startingRocket) {
			// TODO :: actually call a countdown from 10 to lift-off

			startTime = Time.time;

			landingRocket = false;
			startingRocket = true;

		} else {

			// get the rocket back!
			rocket.SetActive(true);

			startTime = Time.time;

			startingRocket = false;
			landingRocket = true;
		}
	}
}

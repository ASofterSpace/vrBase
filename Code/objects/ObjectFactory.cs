﻿/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * A very generic factory that can help us create objects
 * more easily
 */
public class ObjectFactory {

	public static GameObject createRocket() {

		GameObject rocketHolder = new GameObject("RocketHolder");

		GameObject firstStage = createRocketFirstStage();
		firstStage.transform.parent = rocketHolder.transform;
		firstStage.transform.localPosition = new Vector3(0, 0, 0);
		firstStage.transform.localEulerAngles = new Vector3(0, 135, 0);

		GameObject struts = createRocketStruts();
		struts.transform.parent = rocketHolder.transform;
		struts.transform.localPosition = new Vector3(0, 0, 0);
		struts.transform.localEulerAngles = new Vector3(0, 135, 0);

		GameObject secondStage = createRocketSecondStageWithoutCone();
		secondStage.transform.parent = rocketHolder.transform;
		secondStage.transform.localPosition = new Vector3(0, 0, 0);
		secondStage.transform.localEulerAngles = new Vector3(0, 135, 0);

		GameObject secondStageCone = createRocketSecondStageCone();
		secondStageCone.transform.parent = rocketHolder.transform;
		secondStageCone.transform.localPosition = new Vector3(0, 0, 0);
		secondStageCone.transform.localEulerAngles = new Vector3(0, 135, 0);

		return rocketHolder;
	}

	public static GameObject createRocketFirstStage() {

		GameObject curObj;

		GameObject rocket = new GameObject("Rocket");

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 1, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 14, 6);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = PrimitiveFactory.createCone(16, true, false, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Wing";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, -4.61f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1, 8, 16);
		ObjectMultiplier.pointDuplize90(curObj);

		curObj = PrimitiveFactory.createCone(16, false, true, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Side Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, -11, -3.31f);
		curObj.transform.localEulerAngles = new Vector3(13, 0, 0);
		curObj.transform.localScale = new Vector3(3, 3, 3);
		ObjectMultiplier.pointQuadruplize(curObj);

		curObj = PrimitiveFactory.createCone(16, false, true, MaterialCtrl.PLASTIC_WHITE);
		curObj.name = "Main Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(1.74f, -12.8f, 1.74f);
		curObj.transform.localEulerAngles = new Vector3(5, 180, -5);
		curObj.transform.localScale = new Vector3(4, 4, 4);
		ObjectMultiplier.pointQuadruplize(curObj);

		curObj = PrimitiveFactory.createCone(16, false, false, MaterialCtrl.PLASTIC_RED);
		curObj.name = "Main Engine Stripe";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(1.87f, -14.3f, 1.87f);
		curObj.transform.localEulerAngles = new Vector3(5, 180, -5);
		curObj.transform.localScale = new Vector3(4.1f, 2.5f, 4.1f);
		ObjectMultiplier.pointQuadruplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "A Softer Space Logo";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 5.5f, -3);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(6.4f, 1.6f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_LOGOS_ASOFTERSPACE);

		return rocket;
	}

	public static GameObject createRocketStruts() {

		GameObject curObj;

		GameObject rocket = new GameObject("Rocket");

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Strut";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(-0.004f, 16.2f, -2.8f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, -18.161f);
		curObj.transform.localScale = new Vector3(0.2f, 1.33f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);
		ObjectMultiplier.axisTetraduplize(curObj);

		return rocket;
	}

	public static GameObject createRocketSecondStageWithoutCone() {

		GameObject curObj;

		GameObject rocket = new GameObject("Rocket");

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Upper Rocket Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 25.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(6, 8, 6);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = PrimitiveFactory.createCone(16, false, true, MaterialCtrl.PLASTIC_BLACK);
		curObj.name = "Upper Engine";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 17.41f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2, 2, 2);

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
		ObjectMultiplier.pointOctuplize(curObj);

		return rocket;
	}

	public static GameObject createRocketSecondStageCone() {

		GameObject curObj;

		GameObject rocket = new GameObject("Rocket");

		curObj = PrimitiveFactory.createCone(20, false, false, MaterialCtrl.PLASTIC_WHITE);
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

		curObj = PrimitiveFactory.createCone(20, false, false, MaterialCtrl.PLASTIC_RED);
		curObj.name = "Rocket Red Cone";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 39.3f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(4, 2, 4);

		return rocket;
	}

}

/**
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

	public static GameObject createRocket(out ParticleSystem[] firstStageParticleSystems) {

		GameObject rocketHolder = new GameObject("RocketHolder");

		GameObject firstStage = createRocketFirstStage(out firstStageParticleSystems);
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

	public static GameObject createRocketFirstStage(out ParticleSystem[] particleSystems) {

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

		particleSystems = new ParticleSystem[4];

		particleSystems[0] = createParticles(rocket, 1.74f, 1.74f);
		particleSystems[1] = createParticles(rocket, 1.74f, -1.74f);
		particleSystems[2] = createParticles(rocket, -1.74f, -1.74f);
		particleSystems[3] = createParticles(rocket, -1.74f, 1.74f);

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

	private static ParticleSystem createParticles(GameObject rocket, float x, float z) {

		GameObject curObj = new GameObject("ps holder");
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(x, -16.2f, z);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(1, 1, 1);
		curObj.AddComponent<MeshRenderer>();
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		ParticleSystem result = curObj.AddComponent<ParticleSystem>();
		ParticleSystem.MainModule resultMain = result.main;
		resultMain.startLifetime = 2.0f;
		resultMain.startSpeed = 10;
		result.Stop();

		return result;
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

		// show the interior here for the case of actually separating the cone from the second stage
		curObj = PrimitiveFactory.createCone(20, false, true, MaterialCtrl.PLASTIC_WHITE);
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

	public static GameObject createRocketSatellitePayload() {

		GameObject curObj;

		GameObject rocket = new GameObject("Satllite");

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Satellite Body";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 36.35f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2, 2, 2);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Solar Panels";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 36.35f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 2, 3.95f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLUE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		curObj.name = "Round Things";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(-0.074f, 34.232f, -0.914f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1, 1, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);
		ObjectMultiplier.pointQuadruplize(curObj);

		curObj = PrimitiveFactory.createCone(20, false, false, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);
		curObj.name = "Satellite Cone";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 39.36f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2, 1, 2);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		curObj.name = "Docking Port";
		curObj.transform.parent = rocket.transform;
		curObj.transform.localPosition = new Vector3(0, 39.87f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1, 1, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		return rocket;
	}

	public static GameObject createRobot() {

		GameObject curObj;

		GameObject robot = new GameObject("Robot");

		curObj = PrimitiveFactory.createTaperedCube(
			true,
			false,
			MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY,
			MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK,
			MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK
		);
		curObj.name = "Body";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.5f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.35f, 0.2f, 0.5f);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Front Right Wheel";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(-0.2f, 0.1f, -0.3f);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Front Left Wheel";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0.2f, 0.1f, -0.3f);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Mid Right Wheel";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(-0.25f, 0.1f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Mid Left Wheel";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0.25f, 0.1f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Back Right Wheel";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(-0.2f, 0.1f, 0.3f);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Back Left Wheel";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0.2f, 0.1f, 0.3f);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Front Wheel Bar Vert";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.295f, -0.3f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.2f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Front Wheel Bar Horz";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.095f, -0.3f);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.2f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Mid Wheel Bar Vert";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.295f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.2f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Mid Wheel Bar Horz";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.095f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.25f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Back Wheel Bar Vert";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.295f, 0.3f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.2f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Back Wheel Bar Horz";
		curObj.transform.parent = robot.transform;
		curObj.transform.localPosition = new Vector3(0, 0.095f, 0.3f);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.04f, 0.2f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		GameObject neckAttachmentPoint = new GameObject("Neck Attachment Point");
		neckAttachmentPoint.transform.parent = robot.transform;
		neckAttachmentPoint.transform.localPosition = new Vector3(0, 0.4f, -0.15f);
		neckAttachmentPoint.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Neck";
		curObj.transform.parent = neckAttachmentPoint.transform;
		curObj.transform.localPosition = new Vector3(0, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.08f, 0.2f, 0.08f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Head Horz";
		curObj.transform.parent = neckAttachmentPoint.transform;
		curObj.transform.localPosition = new Vector3(0, 0.6f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 90, 0);
		curObj.transform.localScale = new Vector3(0.08f, 0.1f, 0.08f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Head Eyepiece Right";
		curObj.transform.parent = neckAttachmentPoint.transform;
		curObj.transform.localPosition = new Vector3(-0.1f, 0.6f, -0.0252f);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.08f, 0.07f, 0.08f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		curObj.name = "Eye Right";
		curObj.transform.parent = neckAttachmentPoint.transform;
		curObj.transform.localPosition = new Vector3(-0.1f, 0.6f, -0.095f);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.07f, 0.02f, 0.07f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_RED);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Head Eyepiece Left";
		curObj.transform.parent = neckAttachmentPoint.transform;
		curObj.transform.localPosition = new Vector3(0.1f, 0.6f, -0.0252f);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.08f, 0.07f, 0.08f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		curObj.name = "Eye Left";
		curObj.transform.parent = neckAttachmentPoint.transform;
		curObj.transform.localPosition = new Vector3(0.1f, 0.6f, -0.095f);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.07f, 0.02f, 0.07f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_RED);

		return robot;
	}
}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class RocketLaunchCtrl : UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;
	private GameObject rocket;
	private ParticleSystem[] particleSystems;

	private bool startingRocket;
	private bool landingRocket;
	private bool rocketGone;
	private float startTime;


	public RocketLaunchCtrl(MainCtrl mainCtrl, GameObject hostRoom,
		NostalgicConsoleCtrl nostalgicConsoleCtrl, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		nostalgicConsoleCtrl.setRocketLaunchCtrl(this);

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createRocket(position, angles);

		reset();
	}

	public void reset() {

		startingRocket = false;
		landingRocket = false;
		rocketGone = false;

		rocket.transform.localPosition = new Vector3(0, 17, 0);
		rocket.SetActive(true);

		stopParticles();
	}

	public void update(VrInput input) {

		if (startingRocket) {
			float t = (Time.time - startTime);
			rocket.transform.localPosition = new Vector3(0, 17 + (t * t), 0);
			if (t * t > 3000) {
				rocket.SetActive(false);
				rocketGone = true;
				startingRocket = false;
				stopParticles();
			}
		} else if (landingRocket) {
			float y = rocket.transform.localPosition.y;
			float t = (Time.time - startTime);
			y -= t * t;
			if (y < 17) {
				y = 17;
				landingRocket = false;
				rocketGone = false;
				stopParticles();
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
		rocketLauncher.transform.parent = hostRoom.transform.parent;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Launchpad";
		curObj.transform.parent = rocketLauncher.transform;
		curObj.transform.localPosition = new Vector3(0, 0.05f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(13, 0.05f, 13);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_ROCKETLAUNCH_LAUNCHPAD);

		rocket = ObjectFactory.createRocket(out particleSystems);
		rocket.transform.parent = rocketLauncher.transform;
		rocket.transform.localPosition = new Vector3(0, 0, 0);
		rocket.transform.localEulerAngles = new Vector3(0, 45, 0);
	}

	public bool startRocketNext() {
		return !rocketGone && !startingRocket;
	}

	public void launchRocket() {

		if (startRocketNext()) {

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

		playParticles();
	}

	public void playParticles() {

		for (int i = 0; i < 4; i++) {
			ParticleSystem.ShapeModule psShape = particleSystems[i].shape;
			psShape.radius = 1.0f;
			particleSystems[i].Play();
			particleSystems[i].gameObject.GetComponent<ParticleSystemRenderer>().material =
				MaterialCtrl.getMaterial(MaterialCtrl.PARTICLES_FIREBALL);
		}
	}

	public void stopParticles() {

		for (int i = 0; i < 4; i++) {
			particleSystems[i].Stop();
		}
	}
}

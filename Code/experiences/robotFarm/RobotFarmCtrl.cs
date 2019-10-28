/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class RobotFarmCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject robot;

	private Vector3 origPosition;
	private Vector3 origAngles;

	private float firstTime = 0;


	public RobotFarmCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		this.origPosition = position;
		this.origAngles = angles;

		createRobot(position, angles);

		reset();
	}

	public void update(VrInput input) {

		if (firstTime < 0.00001f) {
			firstTime = Time.time;
		}

		robot.transform.localPosition = robot.transform.localPosition + robot.transform.localRotation * new Vector3(0, 0, -Time.deltaTime);
		robot.transform.localEulerAngles = new Vector3(0, Time.time - firstTime , 0);
	}

	public void reset() {

		robot.transform.localPosition = origPosition;
		robot.transform.localEulerAngles = origAngles;

		firstTime = 0;
	}

	private void createRobot(Vector3 position, Vector3 angles) {

		robot = ObjectFactory.createRobot();
		robot.transform.parent = hostRoom.transform;
		robot.transform.localPosition = position;
		robot.transform.localEulerAngles = angles;
	}

}

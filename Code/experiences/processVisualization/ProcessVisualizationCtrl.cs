/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class ProcessVisualizationCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject processVisualizer;


	public ProcessVisualizationCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createProcessVisualizer(position, angles);

		reset();
	}

	public void update(VrInput input) {

	}

	public void reset() {

	}

	private void createProcessVisualizer(Vector3 position, Vector3 angles) {

		processVisualizer = new GameObject("Process Visualizer");
		processVisualizer.transform.parent = hostRoom.transform;

		GameObject curObj;
		BoxCollider col;

		processVisualizer.transform.localPosition = position;
		processVisualizer.transform.localEulerAngles = angles;
	}

}

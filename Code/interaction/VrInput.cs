/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class VrInput {

	public Vector3 camPosition;
	public Quaternion camRotation;

	public float leftTrigger = 0.0f;
	public float rightTrigger = 0.0f;
	public bool leftTriggerPressed = false;
	public bool rightTriggerPressed = false;
	public bool someTriggerPressed = false;
	public bool leftTriggerReleased = false;
	public bool rightTriggerReleased = false;
	public bool someTriggerReleased = false;

	public Vector3 leftPosition;
	public Vector3 rightPosition;
	public Quaternion leftRotation;
	public Quaternion rightRotation;


	public void consolidate(VrInput previous) {

		leftTriggerPressed = leftTrigger > 0.5f;
		rightTriggerPressed = rightTrigger > 0.5f;

		if (leftTriggerPressed || rightTriggerPressed) {
			someTriggerPressed = true;
		}

		if (previous.leftTriggerPressed && !leftTriggerPressed) {
			leftTriggerReleased = true;
		}
		if (previous.rightTriggerPressed && !rightTriggerPressed) {
			rightTriggerReleased = true;
		}
		if (previous.someTriggerPressed && !someTriggerPressed) {
			someTriggerReleased = true;
		}
	}
}

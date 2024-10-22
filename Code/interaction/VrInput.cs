﻿/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * Key-based input in split between the left and right controller; however, there is
 * also a synthetic "some" controller, which mentions one or the other.
 * For each key-based input we have:
 * Clicked - only the first frame that the thing has been clicked
 * Pressed - every frame that the thing is being clicked (including the "clicked" frame)
 * Released - the first frame that the thing is no longer being clicked
 */
public class VrInput {

	public const int LEFT = 0;
	public const int RIGHT = 1;

	public float generatedAt;

	public Vector3 camPosition;
	public Quaternion camRotation;

	public float leftTrigger = 0.0f;
	public float rightTrigger = 0.0f;
	public bool leftTriggerClicked = false;
	public bool rightTriggerClicked = false;
	public bool someTriggerClicked = false;
	public bool leftTriggerPressed = false;
	public bool rightTriggerPressed = false;
	public bool someTriggerPressed = false;
	public bool leftTriggerReleased = false;
	public bool rightTriggerReleased = false;
	public bool someTriggerReleased = false;

	public float leftGrip = 0.0f;
	public float rightGrip = 0.0f;
	public bool leftGripClicked = false;
	public bool rightGripClicked = false;
	public bool someGripClicked = false;
	public bool leftGripPressed = false;
	public bool rightGripPressed = false;
	public bool someGripPressed = false;
	public bool leftGripReleased = false;
	public bool rightGripReleased = false;
	public bool someGripReleased = false;

	public TakeableObject leftLastHoveredObject;
	public TakeableObject rightLastHoveredObject;
	public TakeableObject[] lastHoveredObject;

	public Vector3 leftPosition;
	public Vector3 rightPosition;
	public Quaternion leftRotation;
	public Quaternion rightRotation;

	private float prevGeneratedAt;
	private Vector3 prevLeftPosition;
	private Vector3 prevRightPosition;


	public VrInput() {
		generatedAt = Time.time;
	}

	public void consolidate(VrInput previous) {

		// trigger button (main one on the underside of the controller)
		leftTriggerPressed = leftTrigger > 0.5f;
		rightTriggerPressed = rightTrigger > 0.5f;

		if (leftTriggerPressed || rightTriggerPressed) {
			someTriggerPressed = true;
		}

		if (leftTriggerPressed && !previous.leftTriggerPressed) {
			leftTriggerClicked = true;
		}
		if (rightTriggerPressed && !previous.rightTriggerPressed) {
			rightTriggerClicked = true;
		}
		if (leftTriggerClicked || rightTriggerClicked) {
			someTriggerClicked = true;
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

		// grip button (trigger-like button on the side of the controller)
		leftGripPressed = leftGrip > 0.5f;
		rightGripPressed = rightGrip > 0.5f;

		if (leftGripPressed || rightGripPressed) {
			someGripPressed = true;
		}

		if (leftGripPressed && !previous.leftGripPressed) {
			leftGripClicked = true;
		}
		if (rightGripPressed && !previous.rightGripPressed) {
			rightGripClicked = true;
		}
		if (leftGripClicked || rightGripClicked) {
			someGripClicked = true;
		}

		if (previous.leftGripPressed && !leftGripPressed) {
			leftGripReleased = true;
		}
		if (previous.rightGripPressed && !rightGripPressed) {
			rightGripReleased = true;
		}
		if (previous.someGripPressed && !someGripPressed) {
			someGripReleased = true;
		}

		prevGeneratedAt = previous.generatedAt;
		prevLeftPosition = previous.leftPosition;
		prevRightPosition = previous.rightPosition;
	}

	public TakeableObject getLastHoveredObject(int leftOrRight) {
		if (leftOrRight == LEFT) {
			return leftLastHoveredObject;
		}
		return rightLastHoveredObject;
	}

	public bool getTriggerClicked(int leftOrRight) {
		if (leftOrRight == LEFT) {
			return leftTriggerClicked;
		}
		return rightTriggerClicked;
	}

	public bool getTriggerPressed(int leftOrRight) {
		if (leftOrRight == LEFT) {
			return leftTriggerPressed;
		}
		return rightTriggerPressed;
	}

	public bool getTriggerReleased(int leftOrRight) {
		if (leftOrRight == LEFT) {
			return leftTriggerReleased;
		}
		return rightTriggerReleased;
	}

	public Vector3 getPosition(int leftOrRight) {
		if (leftOrRight == LEFT) {
			return leftPosition;
		}
		return rightPosition;
	}

	public Quaternion getRotation(int leftOrRight) {
		if (leftOrRight == LEFT) {
			return leftRotation;
		}
		return rightRotation;
	}

	public Vector3 getVelocity(int leftOrRight) {

		float divi = (generatedAt - prevGeneratedAt);

		if (divi <= 0) {
			divi = 0.01f;
		}

		if (leftOrRight == LEFT) {
			return (leftPosition - prevLeftPosition) / divi;
		}
		return (rightPosition - prevRightPosition) / divi;
	}
}

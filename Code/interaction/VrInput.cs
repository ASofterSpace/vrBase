using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class VrInput {

	public float leftTrigger = 0.0f;
	public float rightTrigger = 0.0f;
	public bool leftTriggerPressed = false;
	public bool rightTriggerPressed = false;
	public bool someTriggerPressed = false;

	public void consolidate() {
		if (leftTriggerPressed || rightTriggerPressed) {
			someTriggerPressed = true;
		}
	}
}

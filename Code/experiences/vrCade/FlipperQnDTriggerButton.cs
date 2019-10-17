/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class FlipperQnDTriggerButton : Button {

	private FlipperQnDCtrl ctrl;


	public FlipperQnDTriggerButton(GameObject obj, string buttonName, FlipperQnDCtrl ctrl) : base(obj, buttonName) {

		this.ctrl = ctrl;
	}

	public override void hover() {
		if (!ctrl.isPullingTrigger()) {
			ctrl.startPullingTrigger();
		}
		base.hover();
	}

	public override void blur() {
	}

	public override void trigger() {
	}

	public void unhover() {
		base.blur();
	}

}

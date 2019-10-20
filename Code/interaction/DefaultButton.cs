/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


/**
 * This corresponds to a very usual button
 */
public class DefaultButton : Button {

	protected Action onTriggerFunction;


	public DefaultButton(GameObject obj, Action onTriggerFunction) : base(obj) {

		this.onTriggerFunction = onTriggerFunction;
	}

	/**
	 * This button is being pressed - whoop whoop!
	 */
	public override void trigger() {
		onTriggerFunction();
	}

}

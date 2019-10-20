/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


/**
 * A button that is composed of several gameobjects instead of just one
 */
public class DefaultMultiButton : Button {

	private GameObject[] gameObjects;

	protected Action onTriggerFunction;


	public DefaultMultiButton(GameObject[] objs, Action onTriggerFunction) :
		base(objs[0]) {

		this.gameObjects = objs;

		this.onTriggerFunction = onTriggerFunction;
	}

	public override void hover() {

		foreach (GameObject gameObject in gameObjects) {
			MaterialCtrl.setMaterial(gameObject, MaterialCtrl.INTERACTION_BUTTON_HOVER);
		}
	}

	public override void blur() {

		foreach (GameObject gameObject in gameObjects) {
			gameObject.GetComponent<Renderer>().material = defaultMaterial;
		}
	}

	public override void trigger() {
		onTriggerFunction();
	}

	public override void setName(string newName) {
		foreach (GameObject gameObject in gameObjects) {
			gameObject.name = newName;
		}
	}

}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This corresponds to a single button
 */
public class Button {

	protected GameObject gameObject;

	protected Material defaultMaterial;

	protected bool enabled;


	public Button(GameObject obj) {

		this.gameObject = obj;

		this.defaultMaterial = obj.GetComponent<Renderer>().material;
	}

	/**
	 * Some controller is hovering over this button...
	 */
	public virtual void hover() {
		if (enabled) {
			MaterialCtrl.setMaterial(gameObject, MaterialCtrl.INTERACTION_BUTTON_HOVER);
		}
	}

	/**
	 * The controller formerly hovering over this button is moving away again,
	 * no longer hovering...
	 */
	public virtual void blur() {
		gameObject.GetComponent<Renderer>().material = defaultMaterial;
	}

	/**
	 * This button is being pressed - whoop whoop!
	 */
	public virtual void trigger() {
		// actually do something!
	}

	public virtual void setName(string newName) {
		gameObject.name = newName;
	}

	public virtual string getName() {
		return gameObject.name;
	}

	/**
	 * Register clicks and light up on hover again!
	 */
	public virtual void enable() {
		enabled = true;
	}

	/**
	 * Do not register clicks and do not light up on hover anymore!
	 */
	public virtual void disable() {
		enabled = false;
		blur();
	}

}

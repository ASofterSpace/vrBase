/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This corresponds to a single object that the user can take with their controller
 */
public class TakeableObject {

	protected GameObject gameObject;

	protected Material defaultMaterial;

	protected bool enabled;


	public TakeableObject(GameObject obj) {

		this.gameObject = obj;

		this.defaultMaterial = obj.GetComponent<Renderer>().material;

		this.enabled = true;

		obj.AddComponent<Rigidbody>();
	}

	/**
	 * Some controller is hovering close to this object...
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
	 * This object is being taken - whoop whoop!
	 */
	public virtual void take() {
		// actually do something!
	}

	/**
	 * This object is being dropped again - whoopsie!
	 */
	public virtual void drop() {
		// actually do something!
	}

	public virtual void setName(string newName) {
		gameObject.name = newName;
	}

	public virtual string getName() {
		return gameObject.name;
	}

	/**
	 * Register being taken and light up on hover again!
	 */
	public virtual void enable() {
		enabled = true;
	}

	/**
	 * Do not register being taken and do not light up on hover anymore!
	 */
	public virtual void disable() {
		enabled = false;
		blur();
	}

}

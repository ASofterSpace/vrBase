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

	public GameObject gameObject;

	public Transform transform;

	public Rigidbody rigidbody;

	protected MainCtrl mainCtrl;

	protected Renderer[] renderers;

	protected Material[] defaultMaterials;

	// which parent should we get after the controller let's go of us?
	protected GameObject originalParent;

	// which parent did we have originally, so should be be reset to?
	protected GameObject resetParent;

	protected bool enabled;


	public TakeableObject(GameObject obj) {

		this.gameObject = obj;
		this.transform = obj.transform;

		this.renderers = obj.GetComponentsInChildren<Renderer>();

		this.defaultMaterials = new Material[this.renderers.Length];
		for (int i = 0; i < this.renderers.Length; i++) {
			defaultMaterials[i] = renderers[i].material;
		}

		this.enabled = true;

		rigidbody = obj.GetComponent<Rigidbody>();
		if (rigidbody == null) {
			rigidbody = obj.AddComponent<Rigidbody>();
		}
		rigidbody.useGravity = true;
	}

	public void setMainCtrl(MainCtrl mainCtrl) {
		this.mainCtrl = mainCtrl;
	}

	/**
	 * Some controller is hovering close to this object...
	 */
	public virtual void hover() {
		if (enabled) {
			Material hoverMaterial = MaterialCtrl.getMaterial(MaterialCtrl.INTERACTION_BUTTON_HOVER);
			for (int i = 0; i < this.renderers.Length; i++) {
				renderers[i].material = hoverMaterial;
			}
		}
	}

	/**
	 * The controller formerly hovering over this button is moving away again,
	 * no longer hovering...
	 */
	public virtual void blur() {
		for (int i = 0; i < this.renderers.Length; i++) {
			renderers[i].material = defaultMaterials[i];
		}
	}

	/**
	 * This object is being grabbed - whoop whoop!
	 */
	public virtual void grab(GameObject controller) {
		// actually do something!
	}

	/**
	 * This object is being dropped again - whoopsie!
	 */
	public virtual void drop(Vector3 veloctiy) {
		// actually do something!
	}

	public virtual void setName(string newName) {
		gameObject.name = newName;

		// we also set the name of all children, such that controller collisions with
		// children get reported to us
		Transform[] children = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform child in children) {
			child.gameObject.name = newName;
		}
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

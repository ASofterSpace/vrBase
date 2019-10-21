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

	protected Renderer[] renderers;

	protected Material[] defaultMaterials;

	protected GameObject originalParent;

	protected bool enabled;


	public TakeableObject(GameObject obj) {

		this.gameObject = obj;

		this.renderers = obj.GetComponentsInChildren<Renderer>();

		this.defaultMaterials = new Material[this.renderers.Length];
		for (int i = 0; i < this.renderers.Length; i++) {
			defaultMaterials[i] = renderers[i].material;
		}

		this.enabled = true;

		Rigidbody rb = obj.GetComponent<Rigidbody>();
		if (rb == null) {
			rb = obj.AddComponent<Rigidbody>();
		}
		rb.useGravity = true;
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
		Material hoverMaterial = MaterialCtrl.getMaterial(MaterialCtrl.INTERACTION_BUTTON_HOVER);
		for (int i = 0; i < this.renderers.Length; i++) {
			renderers[i].material = defaultMaterials[i];
		}
	}

	/**
	 * This object is being grabbed - whoop whoop!
	 */
	public virtual void grab(GameObject controller) {

		gameObject.GetComponent<Rigidbody>().useGravity = false;

		stillGrabbing();

		if (originalParent == null) {
			originalParent = gameObject.transform.parent.gameObject;
		}
		gameObject.transform.parent = controller.transform;
	}

	/**
	 * The object is still being grabbed, every grabby frame!
	 */
	public virtual void stillGrabbing() {

		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
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

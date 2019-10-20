/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class DioramaCtrl : UpdateableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;

	private GameObject diorama;
	private GameObject dioramaHolder;
	private GameObject dioramaSurface;


	public DioramaCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);

		createDiorama(position, angles);
	}

	void UpdateableCtrl.update(VrInput input) {

		if (dioramaSurface == null) {
			// add the diorama itself by copying ALL surface objects
			dioramaSurface = Object.Instantiate(mainCtrl.getSurface(), dioramaHolder.transform, false);

			// I heard you like diorama's... so let's put a diorama inside your diorama! :D
			GameObject innerDioramaHolder = GameObject.Find("/World/Surface/ControlRoom/Diorama/Diorama Holder/Surface(Clone)/ControlRoom/Diorama/Diorama Holder");
			GameObject innerDioramaSurface = Object.Instantiate(dioramaSurface, innerDioramaHolder.transform, false);

			// destroy everything we don't need in the copy
			Joint[] joints = dioramaSurface.GetComponentsInChildren<Joint>();
			foreach (Joint joint in joints) {
				Object.Destroy(joint);
			}
			Rigidbody[] rigidbodies = dioramaSurface.GetComponentsInChildren<Rigidbody>();
			foreach (Rigidbody rb in rigidbodies) {
				Object.Destroy(rb);
			}
			AudioSource[] audiosources = dioramaSurface.GetComponentsInChildren<AudioSource>();
			foreach (AudioSource audioSource in audiosources) {
				Object.Destroy(audioSource);
			}
			Collider[] colliders = dioramaSurface.GetComponentsInChildren<Collider>();
			foreach (Collider collider in colliders) {
				Object.Destroy(collider);
			}
		}

		diorama.transform.localEulerAngles = new Vector3(0, Time.time * 1.5f, 0);
	}

	private void createDiorama(Vector3 position, Vector3 angles) {

		GameObject curObj;

		diorama = new GameObject("Diorama");
		diorama.transform.parent = hostRoom.transform;
		diorama.transform.localPosition = position;
		diorama.transform.localEulerAngles = angles;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.49f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.49f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol Holder";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(-0.29f, 0.772f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 45);
		curObj.transform.localScale = new Vector3(0.025f, 0.3f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);
		ObjectFactory.pointQuadruplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol Holder Base";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.596f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, -1.283f, 0);
		curObj.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol Top";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.98f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1.1f, 0.005f, 1.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol Moon Floor";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.994f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1.05f, 0.005f, 1.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.SPACE_MOON_FLOOR_INNER);

		dioramaHolder = new GameObject("Diorama Holder");
		dioramaHolder.transform.parent = diorama.transform;
		dioramaHolder.transform.localPosition = new Vector3(0, 1, 0);
		dioramaHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
		dioramaHolder.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
	}

}

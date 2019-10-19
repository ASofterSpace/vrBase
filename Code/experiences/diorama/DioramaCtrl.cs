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
			dioramaSurface = Object.Instantiate(mainCtrl.getSurface(), dioramaHolder.transform, false);
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
		curObj.name = "Diorama Pedestol Top";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.994f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1.1f, 0.005f, 1.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		dioramaHolder = new GameObject("Diorama Holder");
		dioramaHolder.transform.parent = diorama.transform;
		dioramaHolder.transform.localPosition = new Vector3(0, 1, 0);
		dioramaHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
		dioramaHolder.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
	}

}

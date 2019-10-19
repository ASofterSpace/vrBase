/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class DioramaCtrl {

	private GameObject hostRoom;

	private GameObject diorama;


	public DioramaCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		createDiorama(position, angles);
	}

	private void createDiorama(Vector3 position, Vector3 angles) {

		diorama = new GameObject("Diorama");
		diorama.transform.parent = hostRoom.transform;
		diorama.transform.localPosition = position;
		diorama.transform.localEulerAngles = angles;
	}
}

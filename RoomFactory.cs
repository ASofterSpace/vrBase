/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class RoomFactory
{
	private MainCtrl mainCtrl;

	private ControlRoomCtrl controlRoomCtrl;

	private GameObject controlRoom;


	public RoomFactory(MainCtrl mainCtrl) {
		this.mainCtrl = mainCtrl;
	}

	public void createRooms() {
		createControlRoom();
	}

	private void createControlRoom() {
		controlRoom = new GameObject("ControlRoom");

		// every room is a child of the overall surface of the moon
		controlRoom.transform.parent = mainCtrl.getSurface().transform;

		controlRoomCtrl = controlRoom.AddComponent<ControlRoomCtrl>();
		controlRoomCtrl.init(mainCtrl);
	}
}

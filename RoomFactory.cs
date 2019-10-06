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

	private ArcadeRoomCtrl arcadeRoomCtrl;
	private GameObject arcadeRoom;


	public RoomFactory(MainCtrl mainCtrl) {
		this.mainCtrl = mainCtrl;
	}

	public void createRooms() {
		createControlRoom();
		createArcadeRoom();
		createBridges();
	}

	private void createControlRoom() {
		controlRoom = new GameObject("ControlRoom");

		// every room is a child of the overall surface of the moon
		controlRoom.transform.parent = mainCtrl.getSurface().transform;
		controlRoom.transform.localPosition = new Vector3(0, 0, 0);

		controlRoomCtrl = controlRoom.AddComponent<ControlRoomCtrl>();
		controlRoomCtrl.init(mainCtrl);
	}

	private void createArcadeRoom() {
		arcadeRoom = new GameObject("ArcadeRoom");

		// every room is a child of the overall surface of the moon
		arcadeRoom.transform.parent = mainCtrl.getSurface().transform;
		arcadeRoom.transform.localPosition = new Vector3(-7, 0, -12);

		arcadeRoomCtrl = arcadeRoom.AddComponent<ArcadeRoomCtrl>();
		arcadeRoomCtrl.init(mainCtrl);
	}

	private void createBridges() {
		GameObject controlArcadeBridge = new GameObject("ControlArcadeBridge");

		controlArcadeBridge.transform.parent = mainCtrl.getSurface().transform;
		controlArcadeBridge.transform.localPosition = new Vector3(-3.5f, 0, -6);

		BridgeCtrl bridgeCtrl = controlArcadeBridge.AddComponent<BridgeCtrl>();
		bridgeCtrl.init(mainCtrl);
	}
}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class RoomFactory {

	private MainCtrl mainCtrl;

	private ControlRoomCtrl controlRoomCtrl;
	private GameObject controlRoom;

	private ArcadeRoomCtrl arcadeRoomCtrl;
	private GameObject arcadeRoom;

	private ScienceRoomCtrl scienceRoomCtrl;
	private GameObject scienceRoom;


	public RoomFactory(MainCtrl mainCtrl) {
		this.mainCtrl = mainCtrl;
	}

	public void createRooms() {
		createControlRoom();
		createArcadeRoom();
		createScienceRoom();
		createBridges();
	}

	private void createControlRoom() {
		controlRoom = new GameObject("ControlRoom");

		// every room is a child of the overall surface of the moon
		controlRoom.transform.parent = mainCtrl.getSurface().transform;

		controlRoomCtrl = new ControlRoomCtrl(mainCtrl, controlRoom);

		controlRoom.transform.localPosition = new Vector3(0, 0, 0);
	}

	private void createArcadeRoom() {
		arcadeRoom = new GameObject("ArcadeRoom");

		// every room is a child of the overall surface of the moon
		arcadeRoom.transform.parent = mainCtrl.getSurface().transform;

		arcadeRoomCtrl = new ArcadeRoomCtrl(mainCtrl, arcadeRoom);

		arcadeRoom.transform.localPosition = new Vector3(-3.47f, 0, -12.92f);
	}

	private void createScienceRoom() {
		scienceRoom = new GameObject("ScienceRoom");

		// every room is a child of the overall surface of the moon
		scienceRoom.transform.parent = mainCtrl.getSurface().transform;

		scienceRoomCtrl = new ScienceRoomCtrl(mainCtrl, scienceRoom);

		scienceRoom.transform.localPosition = new Vector3(11.77f, 0, -6.22f);
	}

	private void createBridges() {

		GameObject controlArcadeBridge = new GameObject("ControlArcadeBridge");
		controlArcadeBridge.transform.parent = mainCtrl.getSurface().transform;
		controlArcadeBridge.transform.localPosition = new Vector3(-3.5f, 0, -6);
		BridgeCtrl bridgeCtrl = new BridgeCtrl(mainCtrl, controlArcadeBridge, 2, true);

		GameObject controlScienceBridge = new GameObject("ControlScienceBridge");
		controlScienceBridge.transform.parent = mainCtrl.getSurface().transform;
		controlScienceBridge.transform.localPosition = new Vector3(6.38f, 0, -3.5f);
		controlScienceBridge.transform.localEulerAngles = new Vector3(0, 90, 0);
		bridgeCtrl = new BridgeCtrl(mainCtrl, controlScienceBridge, 2.75f, false);
	}
}

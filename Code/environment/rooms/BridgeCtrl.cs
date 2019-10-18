/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class BridgeCtrl : GenericRoomCtrl {

	public BridgeCtrl(MainCtrl mainCtrl, GameObject thisRoom) : base(mainCtrl, thisRoom) {

	}

	protected override void createRoom() {

		createFloor();

		createWalls();

		createObjects();
	}

	protected override void createFloor() {

		GameObject floor = createPrimitive(PrimitiveType.Quad);
		floor.name = TriggerCtrl.FLOOR_NAME;
		floor.transform.localPosition = new Vector3(0, 0, 0);
		floor.transform.localEulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(0.8f, 2, 1);
		MaterialCtrl.setMaterial(floor, MaterialCtrl.PLASTIC_GRAY);
	}

	private GameObject createWallPanel() {
		GameObject result = createPrimitive(PrimitiveType.Quad);
		MaterialCtrl.setMaterial(result, MaterialCtrl.PLASTIC_WHITE);
		return result;
	}

	protected void createWalls() {

		// we create every wall panel twice - once facing inwards,
		// once facing outwards, as we do not want them to be transparent
		// when seen from the outside ^^

		GameObject curWallPanel;

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(-0.55f, 0.18f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(135, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(-0.55f, 0.18f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(-45, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(-0.75f, 1.08f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(180, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 1.5f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(-0.75f, 1.08f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(0, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 1.5f, 1);

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(-0.55f, 2.01f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(-135, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(-0.55f, 2.01f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(45, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0, 2.2f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(-90, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.72f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0, 2.2f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(90, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.72f, 1);

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0.55f, 2.01f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(-45, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0.55f, 2.01f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(135, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0.75f, 1.08f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(0, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 1.5f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0.75f, 1.08f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(180, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 1.5f, 1);

		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0.55f, 0.18f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(45, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);
		curWallPanel = createWallPanel();
		curWallPanel.transform.localPosition = new Vector3(0.55f, 0.18f, 0);
		curWallPanel.transform.localEulerAngles = new Vector3(-135, 90, 0);
		curWallPanel.transform.localScale = new Vector3(2, 0.55f, 1);
	}

	private void createObjects() {

		GameObject poster = createPoster(MaterialCtrl.OBJECTS_POSTERS_VR);
		poster.name = "VR Poster";
		poster.transform.localPosition = new Vector3(-0.749f, 1.2f, 0.3f);
		poster.transform.localEulerAngles = new Vector3(0, -90, -2.95f);
		poster.transform.localScale = new Vector3(0.45f, 0.6f, 1);
	}

}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class MainCtrl : MonoBehaviour {

	private GameObject world;
	private GameObject surface;
	private GameObject skybox;
	private GameObject mainCamera;
	private GameObject mainCameraHolder;

	private FarAwayCtrl farAwayCtrl;
	private MaterialCtrl materialCtrl;

	private VrSpecificCtrl vrSpecificCtrl;
	private TeleportCtrl teleportCtrl;

	private bool initDone = false;

	// we do not want to perform some actions ALL the time,
	// but instead only sometimes... so we keep track of
	// the last time of a full update and perform another
	// one in case a whole second elapsed since then
	private float lastFullUpdateTime;


	void Start() {

		// main objects
		initMainGameObjects();

		// faraway things / skybox
		farAwayCtrl = new FarAwayCtrl(this);

		// close objects / rooms
		RoomFactory roomFactory = new RoomFactory(this);
		roomFactory.createRooms();

		// interactions
		teleportCtrl = new TeleportCtrl(this);

		// VR equipment specific code
		vrSpecificCtrl = new VrSpecificCtrl(this);

		lastFullUpdateTime = -100.0f;

		initDone = true;
	}

	void Update() {

		// ensure that everything has been created and initialized before we call
		// update on anything, ever
		if (!initDone) {
			return;
		}

		float currentUpdateTime = Time.time;
		if (lastFullUpdateTime + 1 < currentUpdateTime) {
			lastFullUpdateTime = currentUpdateTime;
			vrSpecificCtrl.heavyUpdate();
		}

		VrInput input = vrSpecificCtrl.update();

		teleportCtrl.update(input);

		farAwayCtrl.update(input);

		/*
		change the color upon input :)

		materialCtrl.setColor(
			MaterialCtrl.PLASTIC_WHITE,
			new Color(1.0f - inputValue, 1.0f, 1.0f, 1.0f)
		);
		*/
	}

	private void initMainGameObjects() {

		world = GameObject.Find("/World");

		surface = new GameObject("Surface");
		surface.transform.parent = world.transform;

		skybox = new GameObject("Skybox");
		skybox.transform.parent = world.transform;

		mainCameraHolder = GameObject.Find("/MainCameraHolder");
		mainCamera = GameObject.Find("/MainCameraHolder/MainCamera");
	}

	public MaterialCtrl getMaterialCtrl() {
		if (materialCtrl == null) {
			materialCtrl = new MaterialCtrl(this);
		}
		return materialCtrl;
	}

	public GameObject getWorld() {
		return world;
	}

	public GameObject getSurface() {
		return surface;
	}

	public GameObject getSkybox() {
		return skybox;
	}

	public GameObject getMainCamera() {
		return mainCamera;
	}

	public GameObject getMainCameraHolder() {
		return mainCameraHolder;
	}
}

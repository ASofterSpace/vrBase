/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class MainCtrl : MonoBehaviour
{
	private GameObject world;
	private GameObject surface;
	private GameObject skybox;
	private GameObject mainCamera;
	private GameObject mainCameraHolder;

	private FarAwayCtrl farAwayCtrl;
	private MaterialCtrl materialCtrl;

	private VrSpecificCtrl vrSpecificCtrl;


	void Start()
	{
		// main objects
		initMainGameObjects();

		// faraway things / skybox
		farAwayCtrl = skybox.AddComponent<FarAwayCtrl>();
		farAwayCtrl.init(this);

		// close objects / rooms
		RoomFactory roomFactory = new RoomFactory(this);
		roomFactory.createRooms();

		// VR equipment specific code
		vrSpecificCtrl = world.AddComponent<VrSpecificCtrl>();
		vrSpecificCtrl.init(this);
	}

	void Update()
	{

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

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

	private FarAwayCtrl farAwayCtrl;
	private MaterialCtrl materialCtrl;

	private VrSpecificCtrl vrSpecificCtrl;


	void Start()
	{
		// main objects
		initMainGameObjects();

		// VR equipment specific code
		vrSpecificCtrl = new VrSpecificCtrl(this);

		// faraway things / skybox
		farAwayCtrl = skybox.AddComponent<FarAwayCtrl>();
		farAwayCtrl.init(this);

		// close objects / rooms
		RoomFactory roomFactory = new RoomFactory(this);
		roomFactory.createRooms();
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

		mainCamera = GameObject.Find("/MainCamera");
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
}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * This is the main entrypoint into the program which handles both
 * the startup and initialization of all other parts, as well as
 * handling the updating each frame by gathering inputs from the
 * VR specific controller and delivering it to all parts that want
 * to be updated.
 */
public class MainCtrl : MonoBehaviour {

	private GameObject world;
	private GameObject surface;
	private GameObject skybox;
	private GameObject mainCamera;
	private GameObject mainCameraHolder;

	private FarAwayCtrl farAwayCtrl;

	private VrSpecificCtrl vrSpecificCtrl;
	private TriggerCtrl triggerCtrl;

	private bool initDone = false;

	// we do not want to perform some actions ALL the time,
	// but instead only sometimes... so we keep track of
	// the last time of a full update and perform another
	// one in case a whole second elapsed since then
	private float lastFullUpdateTime;

	private List<UpdateableCtrl> updateableCtrls;
	private List<ResetteableCtrl> resetteableCtrls;


	/**
	 * Main function, basically ;)
	 * Called once at startup by Unity
	 */
	void Start() {

		// mainCtrl internal setup
		lastFullUpdateTime = -100.0f;
		updateableCtrls = new List<UpdateableCtrl>();
		resetteableCtrls = new List<ResetteableCtrl>();

		// main objects
		initMainGameObjects();

		// static helpers
		MaterialCtrl.init();
		SoundCtrl.init(mainCamera);
		ObjectCtrl.init();
		ButtonCtrl.init();

		// faraway things / skybox
		farAwayCtrl = new FarAwayCtrl(this);

		// close objects / rooms
		RoomFactory roomFactory = new RoomFactory(this);
		roomFactory.createRooms();

		// interactions
		triggerCtrl = new TriggerCtrl(this);

		// VR equipment specific code
		vrSpecificCtrl = new VrSpecificCtrl(this);

		initDone = true;
	}

	/**
	 * Called on every frame by Unity
	 */
	void Update() {

		// ensure that everything has been created and initialized before we call
		// update on anything, ever
		if (!initDone) {
			return;
		}

		// this check should not be necessary, but seems to be - as otherwise we sometimes
		// get debug errors during game previews...
		if (vrSpecificCtrl == null) {
			return;
		}

		float currentUpdateTime = Time.time;
		if (lastFullUpdateTime + 1 < currentUpdateTime) {
			lastFullUpdateTime = currentUpdateTime;
			vrSpecificCtrl.heavyUpdate();
		}

		VrInput input = vrSpecificCtrl.update();

		triggerCtrl.update(input);

		foreach (UpdateableCtrl ctrl in updateableCtrls) {
			ctrl.update(input);
		}
	}

	/**
	 * Reset the entire game - wheee! :D
	 */
	public void reset() {

		triggerCtrl.reset();

		foreach (ResetteableCtrl ctrl in resetteableCtrls) {
			ctrl.reset();
		}
	}

	private void initMainGameObjects() {

		world = GameObject.Find("/World");

		surface = new GameObject("Surface");
		surface.transform.parent = world.transform;

		skybox = new GameObject("Skybox");
		skybox.transform.parent = world.transform;

		mainCameraHolder = GameObject.Find("/MainCameraHolder");
		mainCamera = GameObject.Find("/MainCameraHolder/MainCamera");

		// move the shader holders out of the way
		GameObject shaders = GameObject.Find("/Shaders");
		shaders.transform.localPosition = new Vector3(0, -10000, 0);
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

	public TriggerCtrl getTriggerCtrl() {
		return triggerCtrl;
	}

	public VrSpecificCtrl getVrSpecificCtrl() {
		return vrSpecificCtrl;
	}

	public void addUpdateableCtrl(UpdateableCtrl updateableCtrl) {
		updateableCtrls.Add(updateableCtrl);
	}

	public void addResetteableCtrl(ResetteableCtrl resetteableCtrl) {
		resetteableCtrls.Add(resetteableCtrl);
	}
}

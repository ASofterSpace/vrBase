/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class ProcessVisualizationCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject processVisualizer;

	private GameObject rocket;
	private GameObject firstStage;
	private GameObject struts;
	private GameObject secondStage;
	private GameObject secondStageCone;
	private GameObject satellite;
	private GameObject label1;
	private GameObject label2;
	private GameObject label3;
	private GameObject label4;

	private GameObject timeSelector;

	private bool playing;
	private bool playingBackwards;
	private float playTime;

	private static float STOP_TIME = 1.25f;


	public ProcessVisualizationCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createProcessVisualizer(position, angles);

		reset();
	}

	public void update(VrInput input) {

		if (playing) {

			playTime += Time.deltaTime / 20;

			if (playTime >= STOP_TIME) {
				playing = false;
			}

			renderTimeStep(playTime);

		} else if (playingBackwards) {

			playTime -= Time.deltaTime / 20;

			if (playTime <= 0) {
				playing = false;
			}

			renderTimeStep(playTime);
		}
	}

	private void renderTimeStep(float playTime) {

		rocket.transform.localPosition = new Vector3(1.15f - (playTime * playTime), 0.336f + 1.15f*playTime, 0);
		rocket.transform.localEulerAngles = new Vector3(0, 0, 80 * playTime);

		if (playTime > 5 * STOP_TIME / 6) {
			float step = 10 * (playTime - (2 * STOP_TIME / 3));
			float step2 = 10 * (playTime - (5 * STOP_TIME / 6));
			secondStage.transform.localPosition = new Vector3(-10*step2, 0, 0);
			secondStage.transform.localEulerAngles = new Vector3(0, 135, 0);
			secondStageCone.transform.localPosition = new Vector3(-10*step2, 3*step, 0);
			secondStageCone.transform.localEulerAngles = new Vector3(0, 135, 0);
			satellite.transform.localPosition = new Vector3(0, 0.5f*step, 0);
			satellite.transform.localEulerAngles = new Vector3(0, 135-10*step, 0);
			label4.SetActive(true);
			label3.SetActive(true);
		} else if (playTime > 2 * STOP_TIME / 3) {
			// deployment
			float step = 10 * (playTime - (2 * STOP_TIME / 3));
			secondStage.transform.localPosition = new Vector3(0, 0, 0);
			secondStage.transform.localEulerAngles = new Vector3(0, 135, 0);
			secondStageCone.transform.localPosition = new Vector3(0, 3*step, 0);
			secondStageCone.transform.localEulerAngles = new Vector3(0, 135, 0);
			satellite.transform.localPosition = new Vector3(0, 0.5f*step, 0);
			satellite.transform.localEulerAngles = new Vector3(0, 135-10*step, 0);
			label4.SetActive(false);
			label3.SetActive(true);
		} else {
			secondStage.transform.localPosition = new Vector3(0, 0, 0);
			secondStage.transform.localEulerAngles = new Vector3(0, 135, 0);
			secondStageCone.transform.localPosition = new Vector3(0, 0, 0);
			secondStageCone.transform.localEulerAngles = new Vector3(0, 135, 0);
			satellite.transform.localPosition = new Vector3(0, 0, 0);
			satellite.transform.localEulerAngles = new Vector3(0, 135, 0);
			label4.SetActive(false);
			label3.SetActive(false);
		}

		if (playTime > STOP_TIME / 3) {
			// stage separation
			float step = 10 * (playTime - (STOP_TIME / 3));
			firstStage.transform.localPosition = new Vector3(-1.1f*step*step, -2.2f*step, 0);
			firstStage.transform.localEulerAngles = new Vector3(0, 135, 0);
			struts.transform.localPosition = new Vector3(-step*step, -2f*step, 0);
			struts.transform.localEulerAngles = new Vector3(0, 135, 0);
			label2.SetActive(true);
		} else {
			firstStage.transform.localPosition = new Vector3(0, 0, 0);
			firstStage.transform.localEulerAngles = new Vector3(0, 135, 0);
			struts.transform.localPosition = new Vector3(0, 0, 0);
			struts.transform.localEulerAngles = new Vector3(0, 135, 0);
			label2.SetActive(false);
		}

		if (playTime > STOP_TIME / 48) {
			label1.SetActive(true);
		} else {
			label1.SetActive(false);
		}

		timeSelector.transform.localPosition = new Vector3(0.45f - ((0.9f * playTime) / STOP_TIME), 0.86f, 0.11f);
	}

	public void reset() {

		playing = false;
		playingBackwards = false;
		playTime = 0;

		label1.SetActive(false);
		label2.SetActive(false);
		label3.SetActive(false);
		label4.SetActive(false);

		renderTimeStep(playTime);
	}

	public void resetToEnd() {

		reset();

		playTime = STOP_TIME;

		label1.SetActive(true);
		label2.SetActive(true);
		label3.SetActive(true);
		label4.SetActive(true);
	}

	private void createProcessVisualizer(Vector3 position, Vector3 angles) {

		processVisualizer = new GameObject("Process Visualizer");
		processVisualizer.transform.parent = hostRoom.transform;
		processVisualizer.transform.localPosition = position;
		processVisualizer.transform.localEulerAngles = angles;

		GameObject curObj;
		BoxCollider col;

		GameObject blackboard = new GameObject("blackboard");
		blackboard.transform.parent = processVisualizer.transform;
		blackboard.transform.localPosition = new Vector3(0, 0, -0.5f);
		blackboard.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Side";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(1.5f, 0.85f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 1.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Foot";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(1.5f, 0.05f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Side";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(-1.5f, 0.85f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 1.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Foot";
		curObj.transform.parent = blackboard.transform;
		curObj.transform.localPosition = new Vector3(-1.5f, 0.05f, 0);
		curObj.transform.localEulerAngles = new Vector3(90, 0, 0);
		curObj.transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		GameObject frame = new GameObject("Frame");
		frame.transform.parent = blackboard.transform;
		frame.transform.localPosition = new Vector3(0, 1.5f, 0);
		frame.transform.localEulerAngles = new Vector3(-5, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Holder Left";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(1.45f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.025f, 0.05f, 0.025f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Holder Right";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-1.45f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.025f, 0.05f, 0.025f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Left";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(1.4f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 1.5f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Right";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-1.4f, 0, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 1.5f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Top";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, 0.775f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 2.85f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Frame Bottom";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, -0.775f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 2.85f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "Blackboard Front";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, 0, 0.01f);
		curObj.transform.localEulerAngles = new Vector3(0, 180, 0);
		curObj.transform.localScale = new Vector3(2.75f, 1.5f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		label1 = curObj;
		curObj.name = "Label 1";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(1.152f, -0.091f, 0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 180, 0);
		curObj.transform.localScale = new Vector3(0.349f, 0.112f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_PROCESSVISUALIZATION_LABELS_STAGE_1);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		label2 = curObj;
		curObj.name = "Label 2";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0.65f, 0.197f, 0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 180, 0);
		curObj.transform.localScale = new Vector3(0.748f, 0.116f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_PROCESSVISUALIZATION_LABELS_STAGE_2);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		label3 = curObj;
		curObj.name = "Label 3";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-0.703f, 0.546f, 0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 180, 0);
		curObj.transform.localScale = new Vector3(0.885f, 0.116f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_PROCESSVISUALIZATION_LABELS_STAGE_3);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		label4 = curObj;
		curObj.name = "Label 4";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-0.974f, -0.367f, 0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 180, 0);
		curObj.transform.localScale = new Vector3(0.448f, 0.108f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_PROCESSVISUALIZATION_LABELS_STAGE_4);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "Blackboard Back";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(0, 0, -0.01f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(2.75f, 1.5f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_BLACK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Bottom Wooden Holder";
		curObj.transform.parent = frame.transform;
		curObj.transform.localPosition = new Vector3(-0.5f, -0.775f, 0.125f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 1, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);


		GameObject timePickShelf = new GameObject("timePickShelf");
		timePickShelf.transform.parent = processVisualizer.transform;
		timePickShelf.transform.localPosition = new Vector3(0, 0, 0.5f);
		timePickShelf.transform.localEulerAngles = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Side";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(-0.475f, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.8f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Side";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0.475f, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 0.8f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Mid Shelf";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.4f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 0.9f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Top Front Side";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.8f, 0);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 90);
		curObj.transform.localScale = new Vector3(0.24f, 0.999f, 0.24f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Time Selector Bar";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.86f, 0.11f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 0.45f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		timeSelector = curObj;
		curObj.name = "Time Selector";
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0.45f, 0.86f, 0.11f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 90);
		curObj.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_PURPLE);

		// play button
		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_PURPLE);
		GameObject playButton = curObj;
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0.2f, 0.94f, 0.06f);
		curObj.transform.localEulerAngles = new Vector3(0, 90, 45);
		curObj.transform.localScale = new Vector3(0.07f, 0.04f, 0.07f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_WHITE);
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(0.2f, 0.927f, 0.047f);
		curObj.transform.localEulerAngles = new Vector3(0, 90, 45);
		curObj.transform.localScale = new Vector3(0.085f, 0.005f, 0.085f);

		DefaultButton playBtn = new DefaultButton(
			playButton,
			() => { play(); }
		);
		ButtonCtrl.add(playBtn);

		// pause button
		GameObject pauseButton = new GameObject("Pause Button");
		pauseButton.transform.parent = timePickShelf.transform;
		pauseButton.transform.localPosition = new Vector3(0, 0.94f, 0.06f);
		pauseButton.transform.localEulerAngles = new Vector3(0, 90, 45);
		col = pauseButton.AddComponent<BoxCollider>();
		col.size = new Vector3(0.08f, 0.05f, 0.07f);
		col.center = new Vector3(0, 0, 0);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = pauseButton.transform;
		curObj.transform.localPosition = new Vector3(0, 0, 0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.075f, 0.03f, 0.02f);
		Object.Destroy(curObj.GetComponent<BoxCollider>());
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_PURPLE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = pauseButton.transform;
		curObj.transform.localPosition = new Vector3(0, -0.0189f, 0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.087f, 0.005f, 0.0345f);
		Object.Destroy(curObj.GetComponent<BoxCollider>());
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = pauseButton.transform;
		curObj.transform.localPosition = new Vector3(0, 0, -0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.075f, 0.03f, 0.02f);
		Object.Destroy(curObj.GetComponent<BoxCollider>());
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_PURPLE);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = pauseButton.transform;
		curObj.transform.localPosition = new Vector3(0, -0.0189f, -0.02f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.087f, 0.005f, 0.0345f);
		Object.Destroy(curObj.GetComponent<BoxCollider>());
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);

		DefaultButton pauseBtn = new DefaultButton(
			pauseButton,
			() => { pause(); }
		);
		ButtonCtrl.add(pauseBtn);

		// play backwards button
		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_PURPLE);
		GameObject playBackButton = curObj;
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(-0.2f, 0.94f, 0.06f);
		curObj.transform.localEulerAngles = new Vector3(0, -90, -45);
		curObj.transform.localScale = new Vector3(0.07f, 0.04f, 0.07f);
		col = curObj.AddComponent<BoxCollider>();
		col.size = new Vector3(1, 1, 1);
		col.center = new Vector3(0, 0, 0);

		curObj = PrimitiveFactory.createTrianglePrism(false, MaterialCtrl.PLASTIC_WHITE);
		curObj.transform.parent = timePickShelf.transform;
		curObj.transform.localPosition = new Vector3(-0.2f, 0.927f, 0.047f);
		curObj.transform.localEulerAngles = new Vector3(0, -90, -45);
		curObj.transform.localScale = new Vector3(0.085f, 0.005f, 0.085f);

		DefaultButton playBackBtn = new DefaultButton(
			playBackButton,
			() => { playBack(); }
		);
		ButtonCtrl.add(playBackBtn);


		rocket = new GameObject("RocketHolder");
		rocket.transform.parent = processVisualizer.transform;
		rocket.transform.localPosition = new Vector3(1.15f, 0.336f, 0);
		rocket.transform.localEulerAngles = new Vector3(0, 0, 0);

		firstStage = ObjectFactory.createRocketFirstStage();
		firstStage.transform.parent = rocket.transform;
		firstStage.transform.localPosition = new Vector3(0, 0, 0);
		firstStage.transform.localEulerAngles = new Vector3(0, 135, 0);

		struts = ObjectFactory.createRocketStruts();
		struts.transform.parent = rocket.transform;
		struts.transform.localPosition = new Vector3(0, 0, 0);
		struts.transform.localEulerAngles = new Vector3(0, 135, 0);

		secondStage = ObjectFactory.createRocketSecondStageWithoutCone();
		secondStage.transform.parent = rocket.transform;
		secondStage.transform.localPosition = new Vector3(0, 0, 0);
		secondStage.transform.localEulerAngles = new Vector3(0, 135, 0);

		secondStageCone = ObjectFactory.createRocketSecondStageCone();
		secondStageCone.transform.parent = rocket.transform;
		secondStageCone.transform.localPosition = new Vector3(0, 0, 0);
		secondStageCone.transform.localEulerAngles = new Vector3(0, 135, 0);

		satellite = ObjectFactory.createRocketSatellitePayload();
		satellite.transform.parent = rocket.transform;
		satellite.transform.localPosition = new Vector3(0, 0, 0);
		satellite.transform.localEulerAngles = new Vector3(0, 135, 0);

		rocket.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
	}

	private void play() {

		if (playTime >= STOP_TIME) {
			reset();
		}

		playing = true;
	}

	private void playBack() {

		if (playTime <= 0) {
			resetToEnd();
		}

		playingBackwards = true;
	}

	private void pause() {

		playing = false;
		playingBackwards = false;
	}

}

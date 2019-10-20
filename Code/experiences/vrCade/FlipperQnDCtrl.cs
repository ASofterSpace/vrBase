/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * Controls the quick-and-dirty flipper - our fist flipper ever \o/
 */
public class FlipperQnDCtrl : UpdateableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;

	private GameObject flipperMachine;
	private GameObject pinball;
	private GameObject flipperLeft;
	private GameObject flipperRight;
	private GameObject barrierLeft;
	private GameObject barrierRight;
	private GameObject trigger;
	private FlipperQnDTriggerButton btnTrigger;

	private GameObject scoresDigit1;
	private GameObject scoresDigit10;
	private GameObject scoresDigit100;
	private GameObject scoresDigit1000;
	private GameObject scoresDigit10000;

	private GameObject ballsDigit1;
	private GameObject ballsDigit10;

	private PinballCollisionScript pinballCollisionScript;

	private bool gameRunning = false;

	private bool pullingTrigger = false;

	private int score = 0;

	private int balls = 3;

	// for some wonky reason 0 appears centered when we are at 6° xD
	private static float ROTATION_BASE_POS = 6;

	// for some wonky reason we always have 20 digits on a disk, not 10
	private static float ROTATION_DIGITS = 20;

	private float scoresDigit1TargetRotation = ROTATION_BASE_POS;
	private float scoresDigit1CurrentRotation = ROTATION_BASE_POS;
	private float scoresDigit10TargetRotation = ROTATION_BASE_POS;
	private float scoresDigit10CurrentRotation = ROTATION_BASE_POS;
	private float scoresDigit100TargetRotation = ROTATION_BASE_POS;
	private float scoresDigit100CurrentRotation = ROTATION_BASE_POS;
	private float scoresDigit1000TargetRotation = ROTATION_BASE_POS;
	private float scoresDigit1000CurrentRotation = ROTATION_BASE_POS;
	private float scoresDigit10000TargetRotation = ROTATION_BASE_POS;
	private float scoresDigit10000CurrentRotation = ROTATION_BASE_POS;

	private float ballsDigit1TargetRotation = ROTATION_BASE_POS;
	private float ballsDigit1CurrentRotation = ROTATION_BASE_POS;
	private float ballsDigit10TargetRotation = ROTATION_BASE_POS;
	private float ballsDigit10CurrentRotation = ROTATION_BASE_POS;

	private AudioSource triggerAudioSource;
	private AudioSource flipperLeftAudioSource;
	private AudioSource flipperRightAudioSource;

	private Vector3[] lastPositions = new Vector3[3];
	private int lastPositionIndex = 0;


	public FlipperQnDCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.mainCtrl = mainCtrl;
		this.hostRoom = hostRoom;

		buildModel(position, angles);

		initGame();

		mainCtrl.addUpdateableCtrl(this);
	}

	void UpdateableCtrl.update(VrInput input) {

		pinballCollisionScript.update(input);

		if (pullingTrigger) {
			setTriggerTo(curTriggerSpeed());

			if (input.someTriggerReleased) {
				letGoTrigger();
			}
		}

		if (input.someTriggerReleased) {
			btnTrigger.unhover();
		}

		if (!gameRunning) {
			return;
		}

		// if the pinball is lost, that is, if it is lower than the bottom barricade...
		if (pinball.transform.localPosition.z > 0.55f) {
			// ... and if it is not the in the start channel on the right, but in the field on the left...
			if (pinball.transform.localPosition.x > -0.3f) {
				// ... call the appropriate function!
				pinballLost();
			}
		}

		if (input.leftGripClicked) {
			flipLeft();
		}
		if (input.leftGripReleased) {
			unflipLeft();
		}
		if (input.rightGripClicked) {
			flipRight();
		}
		if (input.rightGripReleased) {
			unflipRight();
		}

		scoresDigit1CurrentRotation = (scoresDigit1TargetRotation + scoresDigit1CurrentRotation) / 2;
		scoresDigit1.transform.localEulerAngles = new Vector3(scoresDigit1CurrentRotation, 180, 90);

		scoresDigit10CurrentRotation = (scoresDigit10TargetRotation + scoresDigit10CurrentRotation) / 2;
		scoresDigit10.transform.localEulerAngles = new Vector3(scoresDigit10CurrentRotation, 180, 90);

		scoresDigit100CurrentRotation = (scoresDigit100TargetRotation + scoresDigit100CurrentRotation) / 2;
		scoresDigit100.transform.localEulerAngles = new Vector3(scoresDigit100CurrentRotation, 180, 90);

		scoresDigit1000CurrentRotation = (scoresDigit1000TargetRotation + scoresDigit1000CurrentRotation) / 2;
		scoresDigit1000.transform.localEulerAngles = new Vector3(scoresDigit1000CurrentRotation, 180, 90);

		scoresDigit10000CurrentRotation = (scoresDigit10000TargetRotation + scoresDigit10000CurrentRotation) / 2;
		scoresDigit10000.transform.localEulerAngles = new Vector3(scoresDigit10000CurrentRotation, 180, 90);

		ballsDigit1CurrentRotation = (ballsDigit1TargetRotation + ballsDigit1CurrentRotation) / 2;
		ballsDigit1.transform.localEulerAngles = new Vector3(ballsDigit1CurrentRotation, 180, 90);

		ballsDigit10CurrentRotation = (ballsDigit10TargetRotation + ballsDigit10CurrentRotation) / 2;
		ballsDigit10.transform.localEulerAngles = new Vector3(ballsDigit10CurrentRotation, 180, 90);


		// check if we are stuck
		lastPositionIndex++;
		if (lastPositionIndex >= lastPositions.Length) {
			lastPositionIndex = 0;
		}
		lastPositions[lastPositionIndex] = pinball.transform.position;

		float accumulatedDiff = 0.0f;

		for (int i = 0; i < lastPositions.Length - 1; i++) {
			accumulatedDiff += (lastPositions[i] - lastPositions[i+1]).magnitude;
		}

		// if we have no really moved anywhere lately...
		if (accumulatedDiff < 0.01f) {

			// ... and we are not in the starting position...
			if (!pinballIsInStartPosition()) {

				// ... then maybe we are stuck and need a little kick :D
				pinball.GetComponent<Rigidbody>().AddForce(new Vector3(
					Random.Range(-0.1f, 0.1f),
					Random.Range(-0.1f, 0.1f),
					Random.Range(-0.1f, 0.1f)
				));

				/*
				pinball.GetComponent<Rigidbody>().velocity = new Vector3(
					Random.Range(-0.1f, 0.1f),
					Random.Range(-0.1f, 0.1f),
					Random.Range(-0.1f, 0.1f)
				);
				*/
			}
		}
	}

	private void initGame() {

		// create scripts
		pinballCollisionScript = new PinballCollisionScript(pinball, this);

		// init audio
		pinball.AddComponent<AudioSource>();
		triggerAudioSource = trigger.AddComponent<AudioSource>();
		flipperLeftAudioSource = flipperLeft.AddComponent<AudioSource>();
		flipperRightAudioSource = flipperRight.AddComponent<AudioSource>();

		// move flippers to rest position
		unflipLeft();
		unflipRight();

		// move pinball to rest position
		movePinballToStartPos();

		// rest trigger to rest position
		resetTriggerPosition();

		resetGame();

		stopGame();

		// fill the last positions with fake data
		for (int i = 0; i < lastPositions.Length; i++) {
			lastPositions[i] = new Vector3(0.0f, 0.0f, 0.0f);
		}
	}

	private void buildModel(Vector3 position, Vector3 angles) {

		flipperMachine = new GameObject("FlipperQnD");
		flipperMachine.transform.parent = hostRoom.transform;

		GameObject top = new GameObject("top");
		top.transform.parent = flipperMachine.transform;
		GameObject startLabel = createLabel(top, MaterialCtrl.OBJECTS_VRCADE_LABELS_START);
		startLabel.name = "startLabel";
		startLabel.transform.localPosition = new Vector3(-0.14f, 0.99f, 0.399f);
		startLabel.transform.localEulerAngles = new Vector3(0, 180, 0);
		startLabel.transform.localScale = new Vector3(0.0792f, 0.025f, 1);
		GameObject startButton = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		startButton.name = "startButton";
		startButton.transform.parent = top.transform;
		startButton.transform.localPosition = new Vector3(-0.22f, 0.99f, 0.4f);
		startButton.transform.localEulerAngles = new Vector3(90, 0, 0);
		startButton.transform.localScale = new Vector3(0.04f, 0.02f, 0.04f);
		MaterialCtrl.setMaterial(startButton, MaterialCtrl.PLASTIC_PURPLE);
		Button btnStart = new DefaultButton(
			startButton,
			() => {
				startGame();
			}
		);
		ButtonCtrl.add(btnStart);
		GameObject glassPanel = GameObject.CreatePrimitive(PrimitiveType.Cube);
		glassPanel.name = "glassPanel";
		glassPanel.transform.parent = top.transform;
		glassPanel.transform.localPosition = new Vector3(0, 1.045926f, -0.258819f);
		glassPanel.transform.localEulerAngles = new Vector3(0, 0, 0);
		glassPanel.transform.localScale = new Vector3(0.8f, 0.01f, 1.3f);
		glassPanel.GetComponent<Renderer>().enabled = false;
		GameObject topPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
		topPlane.name = "topPlane";
		topPlane.transform.parent = top.transform;
		topPlane.transform.localPosition = new Vector3(0, 0.97f, -0.26f);
		topPlane.transform.localEulerAngles = new Vector3(90, 180, 0);
		topPlane.transform.localScale = new Vector3(0.8f, 1.3f, 1);
		MaterialCtrl.setMaterial(topPlane, MaterialCtrl.OBJECTS_VRCADE_FLIPPERQND_LAYOUT);
		GameObject bottomPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
		bottomPlane.name = "bottomPlane";
		bottomPlane.transform.parent = top.transform;
		bottomPlane.transform.localPosition = new Vector3(0, 0.94f, -0.26f);
		bottomPlane.transform.localEulerAngles = new Vector3(-90, 0, 180);
		bottomPlane.transform.localScale = new Vector3(0.784f, 1.274f, 1);
		MaterialCtrl.setMaterial(bottomPlane, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		GameObject shellBottom = createLog(top);
		shellBottom.name = "shellBottom";
		shellBottom.transform.localPosition = new Vector3(0, 0.9659258f, 0.3911809f);
		shellBottom.transform.localEulerAngles = new Vector3(0, 0, 0);
		shellBottom.transform.localScale = new Vector3(0.8f, 0.15f, 0.013f);

		GameObject shellBottomPanel = createLog(top);
		shellBottomPanel.name = "shellBottomPanel";
		shellBottomPanel.transform.localPosition = new Vector3(0.045f, 1.04f, 0.36f);
		shellBottomPanel.transform.localScale = new Vector3(0.72f, 0.05f, 0.013f);
		GameObject scoreLabel = createLabel(shellBottomPanel, MaterialCtrl.OBJECTS_VRCADE_LABELS_SCORE);
		scoreLabel.name = "scoreLabel";
		scoreLabel.transform.localPosition = new Vector3(-0.3f, 0, -0.51f);
		scoreLabel.transform.localEulerAngles = new Vector3(0, 0, 180);
		scoreLabel.transform.localScale = new Vector3(0.11f, 0.5f, 1);
		GameObject ballsLabel = createLabel(shellBottomPanel, MaterialCtrl.OBJECTS_VRCADE_LABELS_BALLS);
		ballsLabel.name = "ballsLabel";
		ballsLabel.transform.localPosition = new Vector3(0.4f, 0, -0.51f);
		ballsLabel.transform.localEulerAngles = new Vector3(0, 0, 180);
		ballsLabel.transform.localScale = new Vector3(0.09f, 0.5f, 1);
		GameObject scoreHolderBottom = createDigitHolder(shellBottomPanel);
		scoreHolderBottom.name = "scoreHolderBottom";
		scoreHolderBottom.transform.localPosition = new Vector3(-0.422f, 0.25f, -0.6f);
		scoreHolderBottom.transform.localEulerAngles = new Vector3(45, 0, 0);
		scoreHolderBottom.transform.localScale = new Vector3(0.11f, 0.3f, 1);
		GameObject scoreHolderTop = createDigitHolder(shellBottomPanel);
		scoreHolderTop.name = "scoreHolderTop";
		scoreHolderTop.transform.localPosition = new Vector3(-0.422f, -0.22f, -0.6f);
		scoreHolderTop.transform.localEulerAngles = new Vector3(-135, 180, 0);
		scoreHolderTop.transform.localScale = new Vector3(0.11f, 0.3f, 1);
		GameObject ballHolderBottom = createDigitHolder(shellBottomPanel);
		ballHolderBottom.name = "ballHolderBottom";
		ballHolderBottom.transform.localPosition = new Vector3(0.325f, 0.25f, -0.6f);
		ballHolderBottom.transform.localEulerAngles = new Vector3(45, 0, 0);
		ballHolderBottom.transform.localScale = new Vector3(0.06f, 0.3f, 1);
		GameObject ballHolderTop = createDigitHolder(shellBottomPanel);
		ballHolderTop.name = "ballHolderTop";
		ballHolderTop.transform.localPosition = new Vector3(0.325f, -0.22f, -0.6f);
		ballHolderTop.transform.localEulerAngles = new Vector3(-135, 180, 0);
		ballHolderTop.transform.localScale = new Vector3(0.06f, 0.3f, 1);

		shellBottomPanel.transform.localEulerAngles = new Vector3(90, 0, 0);

		scoresDigit1 = createDigitWheel(top);
		scoresDigit1.name = "scoresDigit1";
		scoresDigit1.transform.localPosition = new Vector3(-0.29f, 1.025f, 0.36f);
		scoresDigit10 = createDigitWheel(top);
		scoresDigit10.name = "scoresDigit10";
		scoresDigit10.transform.localPosition = new Vector3(-0.275f, 1.025f, 0.36f);
		scoresDigit100 = createDigitWheel(top);
		scoresDigit100.name = "scoresDigit100";
		scoresDigit100.transform.localPosition = new Vector3(-0.26f, 1.025f, 0.36f);
		scoresDigit1000 = createDigitWheel(top);
		scoresDigit1000.name = "scoresDigit1000";
		scoresDigit1000.transform.localPosition = new Vector3(-0.245f, 1.025f, 0.36f);
		scoresDigit10000 = createDigitWheel(top);
		scoresDigit10000.name = "scoresDigit10000";
		scoresDigit10000.transform.localPosition = new Vector3(-0.23f, 1.025f, 0.36f);
		GameObject scoresDigitBar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		scoresDigitBar.name = "scoresDigitBar";
		scoresDigitBar.transform.parent = top.transform;
		scoresDigitBar.transform.localPosition = new Vector3(-0.26f, 1.023f, 0.36f);
		scoresDigitBar.transform.localEulerAngles = new Vector3(90, 0, 90);
		scoresDigitBar.transform.localScale = new Vector3(0.01f, 0.06f, 0.01f);
		MaterialCtrl.setMaterial(scoresDigitBar, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);
		ballsDigit1 = createDigitWheel(top);
		ballsDigit1.name = "ballsDigit1";
		ballsDigit1.transform.localPosition = new Vector3(0.265f, 1.025f, 0.36f);
		ballsDigit10 = createDigitWheel(top);
		ballsDigit10.name = "ballsDigit10";
		ballsDigit10.transform.localPosition = new Vector3(0.28f, 1.025f, 0.36f);
		GameObject ballsDigitBar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		ballsDigitBar.name = "ballsDigitBar";
		ballsDigitBar.transform.parent = top.transform;
		ballsDigitBar.transform.localPosition = new Vector3(0.323f, 1.023f, 0.36f);
		ballsDigitBar.transform.localEulerAngles = new Vector3(90, 0, 90);
		ballsDigitBar.transform.localScale = new Vector3(0.01f, 0.075f, 0.01f);
		MaterialCtrl.setMaterial(ballsDigitBar, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);
		GameObject shellTop = createLog(top);
		shellTop.name = "shellTop";
		shellTop.transform.localPosition = new Vector3(0, 0.9659258f, -0.908819f);
		shellTop.transform.localEulerAngles = new Vector3(0, 0, 0);
		shellTop.transform.localScale = new Vector3(0.8f, 0.15f, 0.013f);
		GameObject shellLeft = createLog(top);
		shellLeft.name = "shellLeft";
		shellLeft.transform.localPosition = new Vector3(0.4f, 0.9659259f, -0.258819f);
		shellLeft.transform.localEulerAngles = new Vector3(0, 90, 0);
		shellLeft.transform.localScale = new Vector3(1.3f, 0.15f, 0.008f);
		GameObject shellRight = createLog(top);
		shellRight.name = "shellRight";
		shellRight.transform.localPosition = new Vector3(-0.4f, 0.9659259f, -0.258819f);
		shellRight.transform.localEulerAngles = new Vector3(0, 90, 0);
		shellRight.transform.localScale = new Vector3(1.3f, 0.15f, 0.008f);
		GameObject targetTopLeft = createShroomTarget(top);
		targetTopLeft.transform.localPosition = new Vector3(0.16f, 0.9909258f, -0.5188189f);
		GameObject targetTopRight = createShroomTarget(top);
		targetTopRight.transform.localPosition = new Vector3(-0.16f, 0.9909258f, -0.5188189f);
		GameObject targetTopMiddle = createShroomTarget(top);
		targetTopMiddle.transform.localPosition = new Vector3(0, 0.9909257f, -0.583819f);
		GameObject targetRotatorBar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		targetRotatorBar.name = "targetRotatorBar";
		targetRotatorBar.transform.parent = top.transform;
		targetRotatorBar.transform.localPosition = new Vector3(0.3f, 1.044f, -0.205f);
		targetRotatorBar.transform.localEulerAngles = new Vector3(90, -25, 0);
		targetRotatorBar.transform.localScale = new Vector3(0.01f, 0.07f, 0.01f);
		MaterialCtrl.setMaterial(targetRotatorBar, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);
		GameObject targetRotator = GameObject.CreatePrimitive(PrimitiveType.Cube);
		targetRotator.name = "targetRotator";
		targetRotator.transform.parent = top.transform;
		targetRotator.transform.localPosition = new Vector3(0.298f, 1.01f, -0.2f);
		targetRotator.transform.localEulerAngles = new Vector3(0, 64, 0);
		targetRotator.transform.localScale = new Vector3(0.05f, 0.05f, 0.01f);
		MaterialCtrl.setMaterial(targetRotator, MaterialCtrl.OBJECTS_VRCADE_TARGET_WHITE);
		GameObject curLog;
		curLog = createLog(top);
		curLog.name = "barrierStart";
		curLog.transform.localPosition = new Vector3(-0.32f, 1.000926f, -0.128819f);
		curLog.transform.localEulerAngles = new Vector3(0, 0, 0);
		curLog.transform.localScale = new Vector3(0.008123358f, 0.075f, 1.04f);
		curLog = createLog(top);
		curLog.name = "barrierTopLeft";
		curLog.transform.localPosition = new Vector3(0.335f, 1, -0.82f);
		curLog.transform.localEulerAngles = new Vector3(0, 35, 0);
		curLog.transform.localScale = new Vector3(0.01147027f, 0.075f, 0.2226209f);
		curLog = createLog(top);
		curLog.name = "barrierTopRight";
		curLog.transform.localPosition = new Vector3(-0.34f, 1, -0.82f);
		curLog.transform.localEulerAngles = new Vector3(0, -35, 0);
		curLog.transform.localScale = new Vector3(0.0108218f, 0.075f, 0.21f);
		flipperLeft = createFlipper(top, "flipper");
		flipperLeft.transform.localPosition = new Vector3(0.119f, 1.005f, 0.272f);
		flipperLeft.transform.localEulerAngles = new Vector3(0, 25, 0);
		flipperRight = createFlipper(top, "flipper");
		flipperRight.transform.localPosition = new Vector3(-0.037f, 1.005f, 0.272f);
		flipperRight.transform.localEulerAngles = new Vector3(0, -25, 0);
		GameObject flipperLeftBar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		flipperLeftBar.name = "flipperLeftBar";
		flipperLeftBar.transform.parent = top.transform;
		flipperLeftBar.transform.localPosition = new Vector3(0.166f, 1.005f, 0.25f);
		flipperLeftBar.transform.localEulerAngles = new Vector3(0, -25, 0);
		flipperLeftBar.transform.localScale = new Vector3(0.01f, 0.035f, 0.01f);
		MaterialCtrl.setMaterial(flipperLeftBar, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);
		GameObject flipperRightBar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		flipperRightBar.name = "flipperRightBar";
		flipperRightBar.transform.parent = top.transform;
		flipperRightBar.transform.localPosition = new Vector3(-0.0845f, 1.005f, 0.25f);
		flipperRightBar.transform.localEulerAngles = new Vector3(0, -25, 0);
		flipperRightBar.transform.localScale = new Vector3(0.01f, 0.035f, 0.01f);
		MaterialCtrl.setMaterial(flipperRightBar, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);
		barrierLeft = createBarrier(top, "barrierBottomLeft");
		barrierRight = createBarrier(top, "barrierBottomRight");
		curLog = createLog(top);
		curLog.name = "barrierMiddleLeftTT";
		curLog.transform.localPosition = new Vector3(0.34f, 1.000926f, -0.38f);
		curLog.transform.localEulerAngles = new Vector3(0, -30, 0);
		curLog.transform.localScale = new Vector3(0.01215932f, 0.075f, 0.2359991f);
		curLog = createLog(top);
		curLog.name = "barrierMiddleLeftTB";
		curLog.transform.localPosition = new Vector3(0.336f, 1.000926f, -0.245819f);
		curLog.transform.localEulerAngles = new Vector3(0, 65, 0);
		curLog.transform.localScale = new Vector3(0.01486366f, 0.075f, 0.1422886f);
		curLog = createLog(top);
		curLog.name = "barrierMiddleLeftBT";
		curLog.transform.localPosition = new Vector3(0.2675f, 1, -0.154f);
		curLog.transform.localEulerAngles = new Vector3(0, 60, 0);
		curLog.transform.localScale = new Vector3(0.01538406f, 0.075f, 0.08003725f);
		curLog = createLog(top);
		curLog.name = "barrierMiddleLeftBR";
		curLog.transform.localPosition = new Vector3(0.21f, 1.000926f, -0.08f);
		curLog.transform.localEulerAngles = new Vector3(0, -15, 0);
		curLog.transform.localScale = new Vector3(0.011f, 0.075f, 0.18f);
		curLog = createLog(top);
		curLog.name = "barrierMiddleLeftBL";
		curLog.transform.localPosition = new Vector3(0.25f, 1, -0.07f);
		curLog.transform.localEulerAngles = new Vector3(0, -40, 0);
		curLog.transform.localScale = new Vector3(0.015f, 0.075f, 0.18f);
		// actually make this into something more fun, like a really twisted coil or somesuch!
		trigger = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		trigger.name = "trigger";
		trigger.transform.parent = top.transform;
		trigger.transform.localPosition = new Vector3(-0.36f, 0.98f, 0.455f);
		trigger.transform.localEulerAngles = new Vector3(90, 0, 0);
		trigger.transform.localScale = new Vector3(0.08f, 0.06f, 0.08f);
		MaterialCtrl.setMaterial(trigger, MaterialCtrl.OBJECTS_VRCADE_TRIGGER_SILVER);
		btnTrigger = new FlipperQnDTriggerButton(
			trigger,
			this
		);
		ButtonCtrl.add(btnTrigger);

		top.transform.localEulerAngles = new Vector3(15, 0, 0);

		pinball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		pinball.name = "pinball";
		pinball.transform.parent = flipperMachine.transform;
		pinball.transform.localPosition = new Vector3(0.01f, 2, -0.4f);
		pinball.transform.localEulerAngles = new Vector3(0, 0, 0);
		pinball.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
		MaterialCtrl.setMaterial(pinball, MaterialCtrl.OBJECTS_VRCADE_PINBALL_SILVER);
		Rigidbody rb = pinball.AddComponent<Rigidbody>();
		rb.mass = 1;
		rb.drag = 0;
		rb.angularDrag = 0;
		rb.useGravity = true;
		// TODO :: set freeze position and freeze rotation to false for all coordinates

		GameObject legs = new GameObject("legs");
		legs.transform.parent = flipperMachine.transform;
		GameObject legBottomLeft = createLog(legs);
		legBottomLeft.name = "legBottomLeft";
		legBottomLeft.transform.localPosition = new Vector3(0.36f, 0.4f, 0.55f);
		legBottomLeft.transform.localEulerAngles = new Vector3(0, 0, 0);
		legBottomLeft.transform.localScale = new Vector3(0.05f, 0.84f, 0.1f);
		GameObject legBottomRight = createLog(legs);
		legBottomRight.name = "legBottomRight";
		legBottomRight.transform.localPosition = new Vector3(-0.36f, 0.4f, 0.55f);
		legBottomRight.transform.localEulerAngles = new Vector3(0, 0, 0);
		legBottomRight.transform.localScale = new Vector3(0.05f, 0.84f, 0.1f);
		GameObject legTopLeft = createLog(legs);
		legTopLeft.name = "legTopLeft";
		legTopLeft.transform.localPosition = new Vector3(0.36f, 0.55f, -0.52f);
		legTopLeft.transform.localEulerAngles = new Vector3(0, 0, 0);
		legTopLeft.transform.localScale = new Vector3(0.05f, 1.11f, 0.1f);
		GameObject legTopRight = createLog(legs);
		legTopRight.name = "legTopRight";
		legTopRight.transform.localPosition = new Vector3(-0.36f, 0.55f, -0.52f);
		legTopRight.transform.localEulerAngles = new Vector3(0, 0, 0);
		legTopRight.transform.localScale = new Vector3(0.05f, 1.11f, 0.1f);

		flipperMachine.transform.localPosition = position;
		flipperMachine.transform.localEulerAngles = angles;
	}

	/**
	 * Create a log of wood
	 */
	private GameObject createLog(GameObject parent) {
		GameObject log = GameObject.CreatePrimitive(PrimitiveType.Cube);
		log.transform.parent = parent.transform;
		MaterialCtrl.setMaterial(log, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);
		return log;
	}

	/**
	 * Create a label
	 */
	private GameObject createLabel(GameObject parent, int material) {
		GameObject label = GameObject.CreatePrimitive(PrimitiveType.Quad);
		label.transform.parent = parent.transform;
		MaterialCtrl.setMaterial(label, material);
		return label;
	}

	/**
	 * Create a digit holder: a small piece of metal that sits above and below
	 * a revolving digit indicator wheel
	 */
	private GameObject createDigitHolder(GameObject parent) {
		GameObject digitHolder = GameObject.CreatePrimitive(PrimitiveType.Quad);
		digitHolder.transform.parent = parent.transform;
		MaterialCtrl.setMaterial(digitHolder, MaterialCtrl.OBJECTS_MATERIALS_METAL_DARK);
		return digitHolder;
	}

	private GameObject createDigitWheel(GameObject parent) {
		GameObject digit = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		digit.transform.parent = parent.transform;
		digit.transform.localEulerAngles = new Vector3(6, 180, 90);
		digit.transform.localScale = new Vector3(0.05f, 0.006f, 0.05f);
		MaterialCtrl.setMaterial(digit, MaterialCtrl.OBJECTS_VRCADE_DIGITWHEEL);
		return digit;
	}

	private GameObject createShroomTarget(GameObject parent) {
		GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		target.name = "targetShroom";
		target.transform.parent = parent.transform;
		target.transform.localEulerAngles = new Vector3(0, 0, 0);
		target.transform.localScale = new Vector3(0.032f, 0.025f, 0.0325f);
		MaterialCtrl.setMaterial(target, MaterialCtrl.OBJECTS_VRCADE_TARGET_WHITE);
		GameObject targetTop = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		targetTop.name = "targetShroom";
		targetTop.transform.parent = target.transform;
		targetTop.transform.localPosition = new Vector3(0, 1, 0);
		targetTop.transform.localEulerAngles = new Vector3(0, 0, 0);
		targetTop.transform.localScale = new Vector3(2, 1, 2);
		MaterialCtrl.setMaterial(targetTop, MaterialCtrl.OBJECTS_VRCADE_TARGET_WHITE);
		return target;
	}

	private GameObject createFlipper(GameObject parent, string name) {

		GameObject flipper = GameObject.CreatePrimitive(PrimitiveType.Cube);
		flipper.name = name;
		flipper.transform.parent = parent.transform;
		flipper.transform.localScale = new Vector3(0.1f, 0.065f, 0.01f);
		Rigidbody rb = flipper.AddComponent<Rigidbody>();
		rb.mass = 10;
		rb.drag = 0;
		rb.angularDrag = 0.05f;
		rb.useGravity = true;
		// TODO :: set freezeposition and freezerotation all false
		MaterialCtrl.setMaterial(flipper, MaterialCtrl.OBJECTS_VRCADE_TARGET_WHITE);
		return flipper;
	}

	private GameObject createBarrier(GameObject parent, string name) {

		GameObject barrier = createLog(parent);
		barrier.name = name;

		Rigidbody rigidBody = barrier.AddComponent<Rigidbody>();
		rigidBody.mass = 1000;
		rigidBody.drag = 0;
		rigidBody.angularDrag = 0;
		rigidBody.useGravity = false;
		// TODO :: set freeze position x, y, z true and freeze rotation x, y, z true

		HingeJoint hingeJoint = barrier.AddComponent<HingeJoint>();
		hingeJoint.anchor = new Vector3(0, 0, 0.5f);
		/* TODO ::
		hingeJoint.motor.targetVelocity = 2000;
		hingeJoint.motor.force = 10000;
		hingeJoint.motor.freeSpin = false;
		hingeJoint.motor.useLimits = true;
		hingeJoint.limits.min = 0;
		hingeJoint.limits.max = 60;
		hingeJoint.limits.bounciness = 0;
		hingeJoint.limits.bounceMinVelocity = 0;
		hingeJoint.limits.contactDistance = 0;
		hingeJoint.breakForce = Infinity;
		hingeJoint.breakTorque = Infinity;
		*/
		hingeJoint.enableCollision = false;
		hingeJoint.enablePreprocessing = true;
		hingeJoint.massScale = 1;
		hingeJoint.connectedMassScale = 1;

		if (name.EndsWith("Left")) {
			barrier.transform.localPosition = new Vector3(0.28f, 1.000926f, 0.196181f);
			barrier.transform.localEulerAngles = new Vector3(0, -65, 0);
			barrier.transform.localScale = new Vector3(0.01266507f, 0.075f, 0.2500482f);
			hingeJoint.axis = new Vector3(0, 1, 0);
			hingeJoint.connectedAnchor = new Vector3(0.55f, 0, 0);
			hingeJoint.connectedBody = flipperLeft.GetComponent<Rigidbody>();
		} else {
			barrier.transform.localPosition = new Vector3(-0.2f, 1.000926f, 0.196181f);
			barrier.transform.localEulerAngles = new Vector3(0, 65, 0);
			barrier.transform.localScale = new Vector3(0.01286037f, 0.075f, 0.2500482f);
			hingeJoint.axis = new Vector3(0, -1, 0);
			hingeJoint.connectedAnchor = new Vector3(-0.55f, 0, 0);
			hingeJoint.connectedBody = flipperRight.GetComponent<Rigidbody>();
		}

		return barrier;
	}

	public void setScore(int newScore) {

		score = newScore;

		scoresDigit1TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * (score % ROTATION_DIGITS);
		scoresDigit10TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * ((int)(score / 10) % ROTATION_DIGITS);
		scoresDigit100TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * ((int)(score / 100) % ROTATION_DIGITS);
		scoresDigit1000TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * ((int)(score / 1000) % ROTATION_DIGITS);
		scoresDigit10000TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * ((int)(score / 10000) % ROTATION_DIGITS);
	}

	public void addToScore(int summand) {

		setScore(score + summand);
	}

	public void setBalls(int newBallAmount) {

		if (newBallAmount < 0) {
			stopGame();
			// TODO :: create gameCtrl also here, or maybe put this into mainCtrl:
			// gameCtrl.gameOver();
			return;
		}

		balls = newBallAmount;

		ballsDigit1TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * (balls % ROTATION_DIGITS);
		ballsDigit10TargetRotation = ROTATION_BASE_POS + (360/ROTATION_DIGITS) * ((int)(balls / 10) % ROTATION_DIGITS);
	}

	private bool pinballIsInStartPosition() {
		// is it low?
		if (pinball.transform.localPosition.z > 0.55f) {
			// is it far right?
			if (pinball.transform.localPosition.x < -0.3f) {
				return true;
			}
		}

		return false;
	}

	private void flipLeft() {

		var hinge = barrierLeft.GetComponent<HingeJoint>();

		var motor = hinge.motor;
		motor.force = 10000;
		motor.targetVelocity = 2000;
		hinge.useMotor = true;

		flipperLeftAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_2), 1.0f);
	}

	private void unflipLeft() {

		var hinge = barrierLeft.GetComponent<HingeJoint>();

		var motor = hinge.motor;
		hinge.useMotor = false;

		flipperLeftAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.KLACK_1), 1.0f);
	}

	private void flipRight() {

		var hinge = barrierRight.GetComponent<HingeJoint>();

		var motor = hinge.motor;
		motor.force = 10000;
		motor.targetVelocity = 2000;
		hinge.useMotor = true;

		flipperRightAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_2), 1.0f);
	}

	private void unflipRight() {

		var hinge = barrierRight.GetComponent<HingeJoint>();

		var motor = hinge.motor;
		hinge.useMotor = false;

		flipperRightAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.KLACK_1), 1.0f);
	}

	private void pinballLost() {

		countDownLives();

		movePinballToStartPos();
	}

	private void countDownLives() {

		setBalls(balls - 1);
	}

	private void movePinballToStartPos() {

		pinball.transform.localPosition = new Vector3(-0.36f, 0.86f, 0.6f);

		// set the velocity - so it does not fly off into randomness - and give it actually
		// a little bit (not just 0) so that it jumps funnily
		pinball.GetComponent<Rigidbody>().velocity = new Vector3(0, -2.0f, -4.0f);
	}

	private float triggerStartTime = 0;

	public void startPullingTrigger() {

		triggerStartTime = Time.time;

		pullingTrigger = true;

		triggerAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.WHOOSH_5), 1.0f);
	}

	private float curTriggerSpeed() {

		// maximum triggering reached after three seconds
		// TODO :: add sound effects to show this more properly!
		float triggerSpeed = (Time.time - triggerStartTime) / 3;

		// no more triggering after maximum is reached
		if (triggerSpeed > 1.0f) {
			triggerSpeed = 1.0f;
		}

		return triggerSpeed;
	}

	private void letGoTrigger() {

		if (pullingTrigger) {

			triggerAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.KLACK_11), 1.0f);

			float triggerSpeed = curTriggerSpeed();

			// give that ball that velocity, darn it! :D
			// btw. - this velocity here is in world coordinates!
			Vector3 newVelocity = new Vector3(0, -10 * triggerSpeed, -20 * triggerSpeed);

			// because it is in world coordinates, but someone might have turned around the flipper
			// automaton, we rotate the velocity along the rotation of the flipper machine ^^
			newVelocity = flipperMachine.transform.rotation * newVelocity;

			// ... aaaand assign the result, now licely localized
			// (tested for flipper machine rotation 180° and -90°)
			pinball.GetComponent<Rigidbody>().velocity = newVelocity;

			resetTriggerPosition();
		}
	}

	private void resetTriggerPosition() {

		pullingTrigger = false;

		setTriggerTo(0.0f);
	}

	public bool isPullingTrigger() {
		return pullingTrigger;
	}

	private void setTriggerTo(float triggerSpeed) {

		trigger.transform.localScale = new Vector3(0.08f, 0.01f + (triggerSpeed / 20), 0.08f);

		trigger.transform.localPosition = new Vector3(-0.36f, 0.98f, 0.405f + (triggerSpeed / 20));
	}

	private void resetGame() {

		// reset score counter
		setScore(0);

		// reset balls counter
		setBalls(3);

		// reset ball position
		movePinballToStartPos();
	}

	public void startGame() {

		gameRunning = true;

		pinball.GetComponent<Renderer>().enabled = true;

		resetGame();
	}

	public void stopGame() {

		gameRunning = false;

		pinball.GetComponent<Renderer>().enabled = false;
	}

}

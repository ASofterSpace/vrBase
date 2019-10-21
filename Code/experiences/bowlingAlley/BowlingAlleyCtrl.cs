/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class BowlingAlleyCtrl: ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject bowlingAlley;

	private GameObject[] bowlingBalls;
	private GameObject[] pins;


	public BowlingAlleyCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addResetteableCtrl(this);

		createBowlingAlley(position, angles);

		reset();
	}

	public void reset() {

		foreach (GameObject bowlingBall in bowlingBalls) {
			bowlingBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			bowlingBall.transform.localEulerAngles = new Vector3(0, 0, 0);
		}

		bowlingBalls[0].transform.localPosition = new Vector3(0, 0.1f, -2.8f);
		bowlingBalls[1].transform.localPosition = new Vector3(0.75f, 0.1f, -2.8f);
		bowlingBalls[2].transform.localPosition = new Vector3(0.75f, 0.1f, -3.2f);

		foreach (GameObject pin in pins) {
			pin.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			pin.transform.localEulerAngles = new Vector3(0, 0, 0);
		}

		pins[0].transform.localPosition = new Vector3(0, 0, 2.3f);
		pins[1].transform.localPosition = new Vector3(0.12f, 0, 2.45f);
		pins[2].transform.localPosition = new Vector3(-0.12f, 0, 2.45f);
		pins[3].transform.localPosition = new Vector3(-0.24f, 0, 2.6f);
		pins[4].transform.localPosition = new Vector3(0, 0, 2.6f);
		pins[5].transform.localPosition = new Vector3(0.24f, 0, 2.6f);
		pins[6].transform.localPosition = new Vector3(-0.36f, 0, 2.75f);
		pins[7].transform.localPosition = new Vector3(-0.12f, 0, 2.75f);
		pins[8].transform.localPosition = new Vector3(0.12f, 0, 2.75f);
		pins[9].transform.localPosition = new Vector3(0.36f, 0, 2.75f);
	}

	private void createBowlingAlley(Vector3 position, Vector3 angles) {

		bowlingAlley = new GameObject("Bowling Alley");
		bowlingAlley.transform.parent = hostRoom.transform;

		GameObject bowlingFloor = GameObject.CreatePrimitive(PrimitiveType.Quad);
		bowlingFloor.name = TriggerCtrl.FLOOR_NAME;
		bowlingFloor.transform.parent = bowlingAlley.transform;
		bowlingFloor.transform.localPosition = new Vector3(0, 0.001f, 0);
		bowlingFloor.transform.localEulerAngles = new Vector3(90, 0, 0);
		bowlingFloor.transform.localScale = new Vector3(1, 6, 1);
		MaterialCtrl.setMaterial(bowlingFloor, MaterialCtrl.BUILDING_FLOOR_WOOD);

		bowlingBalls = new GameObject[3];
		bowlingBalls[0] = createBowlingBall(MaterialCtrl.OBJECTS_BOWLING_BALL_RED);
		bowlingBalls[1] = createBowlingBall(MaterialCtrl.PLASTIC_PURPLE);
		bowlingBalls[2] = createBowlingBall(MaterialCtrl.PLASTIC_BLUE);

		pins = new GameObject[10];
		for (int i = 0; i < pins.Length; i++) {
			pins[i] = createBowlingPin();
		}

		bowlingAlley.transform.localPosition = position;
		bowlingAlley.transform.localEulerAngles = angles;
	}

	private GameObject createBowlingBall(int material) {

		GameObject bowlingBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		bowlingBall.transform.parent = bowlingAlley.transform;
		bowlingBall.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

		MaterialCtrl.setMaterial(bowlingBall, material);

		ThrowableObject curThrowable = new ThrowableObject(bowlingBall);
		ObjectCtrl.add(curThrowable);

		Rigidbody rb = bowlingBall.GetComponent<Rigidbody>();
		rb.mass = 50;

		PhysicMaterial physicsMat = new PhysicMaterial();
		physicsMat.dynamicFriction = 0.2f;
		physicsMat.staticFriction = 0.5f;
		physicsMat.bounciness = 0.1f;
		physicsMat.frictionCombine = PhysicMaterialCombine.Minimum;
		physicsMat.bounceCombine = PhysicMaterialCombine.Average;
		bowlingBall.GetComponent<Collider>().material = physicsMat;

		return bowlingBall;
	}

	private GameObject createBowlingPin() {

		GameObject pin = new GameObject("bowlingPin");
		BoxCollider col = pin.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0.105f, 0);
		col.size = new Vector3(0.04f, 0.21f, 0.04f);

		GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		capsule.transform.parent = pin.transform;
		capsule.transform.localPosition = new Vector3(0, 0.08f, 0);
		capsule.transform.localScale = new Vector3(0.05f, 0.08f, 0.05f);
		Object.Destroy(capsule.GetComponent<Collider>());
		MaterialCtrl.setMaterial(capsule, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject neck = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		neck.transform.parent = pin.transform;
		neck.transform.localPosition = new Vector3(0, 0.1631f, 0);
		neck.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		Object.Destroy(neck.GetComponent<Collider>());
		MaterialCtrl.setMaterial(neck, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		head.transform.parent = pin.transform;
		head.transform.localPosition = new Vector3(0, 0.1961f, 0);
		head.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
		Object.Destroy(head.GetComponent<Collider>());
		MaterialCtrl.setMaterial(head, MaterialCtrl.OBJECTS_BOWLING_PIN_WHITE);

		GameObject redBand = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		redBand.transform.parent = pin.transform;
		redBand.transform.localPosition = new Vector3(0, 0.1f, 0);
		redBand.transform.localScale = new Vector3(0.051f, 0.01f, 0.051f);
		Object.Destroy(redBand.GetComponent<Collider>());
		MaterialCtrl.setMaterial(redBand, MaterialCtrl.OBJECTS_BOWLING_PIN_RED);

		// if we want to, we can also take a pin and throw it around ^^
		ThrowableObject curThrowable = new ThrowableObject(pin);
		ObjectCtrl.add(curThrowable);

		pin.transform.parent = bowlingAlley.transform;

		return pin;
	}
}

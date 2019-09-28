/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */
using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class FarAwayCtrl : MonoBehaviour
{
	private MainCtrl mainCtrl;
	private GameObject skybox;
	private int angle;


	void Start()
	{

	}

	void Update()
	{

	}

	public void init(MainCtrl mainCtrl) {
		this.mainCtrl = mainCtrl;
		this.skybox = mainCtrl.getSkybox();

		createMoon();

		createEarth();

		createSun();

		createStars();
	}

	private void createMoon() {

		GameObject moon = new GameObject("Moon");
		moon.transform.parent = skybox.transform;

		GameObject moonFloor = GameObject.CreatePrimitive(PrimitiveType.Quad);
		moonFloor.transform.parent = moon.transform;
		moonFloor.transform.localPosition = new Vector3(0, -0.1f, 0);
		moonFloor.transform.eulerAngles = new Vector3(90, 0, 0);
		moonFloor.transform.localScale = new Vector3(4000, 4000, 1);
		// TODO - add material

		angle = 0;
		GameObject moonSouth = createMoonWall(moon);
		moonSouth.transform.localPosition = new Vector3(0, -1000, -2000);
		GameObject moonWest = createMoonWall(moon);
		moonWest.transform.localPosition = new Vector3(-2000, -1000, 0);
		GameObject moonNorth = createMoonWall(moon);
		moonNorth.transform.localPosition = new Vector3(0, -1000, 2000);
		GameObject moonEast = createMoonWall(moon);
		moonEast.transform.localPosition = new Vector3(2000, -1000, 0);
	}

	private GameObject createMoonWall(GameObject moon) {

		GameObject moonWall = GameObject.CreatePrimitive(PrimitiveType.Quad);

		moonWall.transform.parent = moon.transform;

		moonWall.transform.eulerAngles = new Vector3(90, angle, 0);
		angle += 90;

		moonWall.transform.localScale = new Vector3(400, 1, 228.3f);

		// TODO - add material

		return moonWall;
	}

	private void createEarth() {

		GameObject earth = GameObject.CreatePrimitive(PrimitiveType.Quad);
		earth.transform.parent = skybox.transform;
		earth.transform.localPosition = new Vector3(-3000, 500, 0);
		earth.transform.eulerAngles = new Vector3(90, 90, 0);
		earth.transform.localScale = new Vector3(80, 1, 80);
		// TODO - add material
	}

	private void createSun() {
		// TODO
	}

	private void createStars() {
		// TODO
	}
}

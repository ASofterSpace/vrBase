/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class FarAwayCtrl : UpdateableCtrl {

	private MainCtrl mainCtrl;
	private GameObject skybox;
	private GameObject earth;
	private GameObject[] satellite;
	private float[] satDirX;
	private float[] satDirZ;
	private int curIterator;
	private const int SATELLITE_AMOUNT = 3;


	public FarAwayCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;
		this.skybox = mainCtrl.getSkybox();

		mainCtrl.addUpdateableCtrl(this);

		createMoon();

		createEarth();

		createSun();

		createStars();

		createSatellites();
	}

	public void update(VrInput input) {

		// let the Earth rise, but slowly, unnoticeably! :)
		// (up to 500+3600/5=1220 after one hour...)
		earth.transform.localPosition = new Vector3(-3000, 500 + (Time.time / 5), 0);

		// let the satellites fly overhead
		for (int i = 0; i < SATELLITE_AMOUNT; i++) {

			Vector3 prevPos = satellite[i].transform.localPosition;
			satellite[i].transform.localPosition = new Vector3(
				prevPos.x + satDirX[i] * Time.deltaTime * (10 + 3*i),
				250 * (i + 1),
				prevPos.z + satDirZ[i] * Time.deltaTime * (10 + 3*i));
			if ((prevPos.x < -1000) || (prevPos.x > 1000) || (prevPos.z < -1000) || (prevPos.z > 1000)) {
				randomizeSatellite(i);
			}
		}
	}

	private void randomizeSatellites() {

		for (int i = 0; i < SATELLITE_AMOUNT; i++) {
			randomizeSatellite(i);
		}
	}

	private void randomizeSatellite(int i) {

		float rand = Random.value;
		satDirX[i] = rand;
		satDirZ[i] = 1 - rand;
		if (Random.value > 0.5f) {
			satDirX[i] = -satDirX[i];
		}
		if (Random.value > 0.5f) {
			satDirZ[i] = -satDirZ[i];
		}

		satellite[i].transform.localPosition = new Vector3(
			-990 * satDirX[i],
			250 * (i + 1),
			-990 * satDirZ[i]
		);
		satellite[i].transform.localEulerAngles = new Vector3(0, -90 - (180 * Mathf.Asin(satDirX[i]) / Mathf.PI), 90);
	}

	private void createMoon() {

		GameObject moon = new GameObject("Moon");
		moon.transform.parent = skybox.transform;

		GameObject moonFloor = GameObject.CreatePrimitive(PrimitiveType.Quad);
		moonFloor.name = "moonFloor";
		moonFloor.transform.parent = moon.transform;
		moonFloor.transform.localPosition = new Vector3(0, -0.03f, 0);
		moonFloor.transform.localEulerAngles = new Vector3(90, 0, 0);
		moonFloor.transform.localScale = new Vector3(4000, 4000, 1);
		MaterialCtrl.setMaterial(moonFloor, MaterialCtrl.SPACE_MOON_FLOOR);

		GameObject moonFloorPlate = GameObject.CreatePrimitive(PrimitiveType.Quad);
		moonFloorPlate.name = "moonFloorPlate";
		moonFloorPlate.transform.parent = moon.transform;
		moonFloorPlate.transform.localPosition = new Vector3(0, -5.03f, 0);
		moonFloorPlate.transform.localEulerAngles = new Vector3(90, 0, 0);
		moonFloorPlate.transform.localScale = new Vector3(1000, 1000, 10);
		Object.Destroy(moonFloorPlate.GetComponent<Renderer	>());
		Object.Destroy(moonFloorPlate.GetComponent<Collider>());
		BoxCollider bc = moonFloorPlate.AddComponent<BoxCollider>();
		bc.center = new Vector3(0, 0, 0);
		bc.size = new Vector3(1, 1, 1);

		curIterator = 0;
		GameObject moonSouth = createMoonWall(moon);
		moonSouth.name = "moonSouth";
		moonSouth.transform.localPosition = new Vector3(0, -1000, -2000);
		GameObject moonWest = createMoonWall(moon);
		moonWest.name = "moonWest";
		moonWest.transform.localPosition = new Vector3(-2000, -1000, 0);
		GameObject moonNorth = createMoonWall(moon);
		moonNorth.name = "moonNorth";
		moonNorth.transform.localPosition = new Vector3(0, -1000, 2000);
		GameObject moonEast = createMoonWall(moon);
		moonEast.name = "moonEast";
		moonEast.transform.localPosition = new Vector3(2000, -1000, 0);
	}

	private GameObject createMoonWall(GameObject moon) {

		GameObject moonWall = GameObject.CreatePrimitive(PrimitiveType.Quad);

		moonWall.transform.parent = moon.transform;

		moonWall.transform.localEulerAngles = new Vector3(0, 180 + (curIterator * 90), 0);

		moonWall.transform.localScale = new Vector3(4000, 2283, 1);

		MaterialCtrl.setMaterial(moonWall, MaterialCtrl.SPACE_MOON_SOUTH + curIterator);

		curIterator++;

		return moonWall;
	}

	private void createEarth() {

		earth = GameObject.CreatePrimitive(PrimitiveType.Quad);
		earth.name = "Earth";
		earth.transform.parent = skybox.transform;
		earth.transform.localPosition = new Vector3(-3000, 500, 0);
		earth.transform.localEulerAngles = new Vector3(180, 90, 180);
		earth.transform.localScale = new Vector3(800, 800, 1);
		MaterialCtrl.setMaterial(earth, MaterialCtrl.SPACE_EARTH);
	}

	private void createSun() {

		GameObject sun = new GameObject("Sun");
		sun.transform.parent = skybox.transform;
		sun.transform.localPosition = new Vector3(1000, 3000, 0);

		GameObject sunImage = GameObject.CreatePrimitive(PrimitiveType.Quad);
		sunImage.name = "sunImage";
		sunImage.transform.parent = sun.transform;
		sunImage.transform.localPosition = new Vector3(0, 0, 0);
		sunImage.transform.localEulerAngles = new Vector3(275, 0, 0);
		sunImage.transform.localScale = new Vector3(750, 750, 1);
		MaterialCtrl.setMaterial(sunImage, MaterialCtrl.SPACE_SUN);

		GameObject sunLight = new GameObject();
		sunLight.name = "sunLight";
		sunLight.transform.parent = sun.transform;
		sunLight.transform.localPosition = new Vector3(0, 0, 0);
		sunLight.transform.localEulerAngles = new Vector3(60, -100, 0);
		Light sunLightLight = sunLight.AddComponent<Light>();
		sunLightLight.color = new Color(1.0f, 0.9568f, 0.8392f, 1.0f);
		sunLightLight.type = LightType.Directional;
		sunLightLight.intensity = 0.75f;
		// TODO :: set mixed mode instead of realtime mode, if possible?
	}

	private void createStars() {

		GameObject stars = new GameObject("Stars");
		stars.transform.parent = skybox.transform;
		stars.transform.localPosition = new Vector3(0, 3500, 0);

		int starAmount = 150;

		for (int i = 0; i < starAmount; i++) {
			GameObject star = GameObject.CreatePrimitive(PrimitiveType.Quad);
			star.transform.parent = stars.transform;
			star.transform.localPosition = new Vector3(20000 * Random.value - 10000, 0, 20000 * Random.value - 10000);
			star.transform.localEulerAngles = new Vector3(270, 0, 0);
			float size = 10 + (20 * Random.value);
			star.transform.localScale = new Vector3(size, size, 1);
			MaterialCtrl.setMaterial(star, MaterialCtrl.SPACE_STAR);
		}
	}

	private void createSatellites() {

		satellite = new GameObject[SATELLITE_AMOUNT];
		satDirX = new float[SATELLITE_AMOUNT];
		satDirZ = new float[SATELLITE_AMOUNT];

		for (int i = 0; i < SATELLITE_AMOUNT; i++) {
			satellite[i] = ObjectFactory.createRocketSatellitePayload();
			satellite[i].transform.parent = skybox.transform;
			satellite[i].transform.localPosition = new Vector3(0, 250 * (i + 1), 0);
			satellite[i].transform.localEulerAngles = new Vector3(0, 0, 90);
			satellite[i].transform.localScale = new Vector3(1, 1, 1);
		}

		randomizeSatellites();
	}
}

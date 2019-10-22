/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class MathWorldCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	// used to indicate the teslating-status of our solids, one number for each;
	// any negative number means that no update is required, with 0 we start and tesselate up
	private float[] teslator;
	private int[] teslatedLast;
	private MeshRenderer[][] teslateRenderers;

	private GameObject platonicSolidShelf;

	private ThrowableBoundObject[] solids;


	public MathWorldCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		this.teslator = new float[5];
		this.teslatedLast = new int[5];
		this.teslateRenderers = new MeshRenderer[5][];
		for (int i = 0; i < 5; i++) {
			teslator[i] = -1;
		}

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createPlatonicSolidShelf(position, angles);

		reset();
	}

	public void update(VrInput input) {

		for (int i = 0; i < 5; i++) {

			// rotate as long as it is untouched
			if (solids[i].isBound()) {
				solids[i].transform.localEulerAngles = new Vector3(0, 25 * Time.time, 0);
			}

			if (teslator[i] >= 0) {
				teslator[i] += Time.deltaTime * 2;

				if (teslator[i] > teslatedLast[i]) {
					teslatedLast[i] = teslatedLast[i] + 1;
					if (teslatedLast[i] > teslateRenderers[i].Length) {
						teslator[i] = -1;
					} else {
						for (int j = 0; j < teslatedLast[i]; j++) {
							teslateRenderers[i][j].enabled = true;
						}
					}
				}
			}
		}
	}

	public void reset() {

		solids[0].transform.localPosition = new Vector3(0.4f, 1.084191f, -0.075f);
		solids[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		solids[1].transform.localPosition = new Vector3(0.2f, 1.1f, -0.055f);
		solids[1].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		solids[2].transform.localPosition = new Vector3(0, 1.12f, -0.055f);
		solids[2].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		solids[3].transform.localPosition = new Vector3(-0.2f, 1.115f, -0.055f);
		solids[3].transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);

		solids[4].transform.localPosition = new Vector3(-0.4f, 1.123f, -0.055f);
		solids[4].transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);

		foreach (ThrowableBoundObject solid in solids) {
			solid.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			solid.transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

	private void createPlatonicSolidShelf(Vector3 position, Vector3 angles) {

		platonicSolidShelf = new GameObject("Platonic Solid Shelf");
		platonicSolidShelf.transform.parent = hostRoom.transform;

		GameObject curObj;
		BoxCollider col;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Left Side";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(-0.475f, 0.5f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 1, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Right Side";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(0.475f, 0.5f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.05f, 1, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Top Side";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 1.025f, -0.075f);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 1, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Top Front Side";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.964f, 0.08f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 0.9f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		curObj.name = "Top Front Label";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.98f, 0.1f);
		curObj.transform.localEulerAngles = new Vector3(135, 0, 180);
		curObj.transform.localScale = new Vector3(0.9f, 0.2f, 1);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATHWORLD_LABELS_PLATONICSOLIDS);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Mid Shelf";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.5f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 0.9f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		solids = new ThrowableBoundObject[5];

		curObj = PlatonicSolidFactory.createTetrahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Tetrahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		MeshCollider meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[0] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[0]);

		curObj = PlatonicSolidFactory.createCube(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Cube";
		curObj.transform.parent = platonicSolidShelf.transform;
		col = curObj.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0, 0);
		col.size = new Vector3(1, 1, 1);
		solids[1] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[1]);

		curObj = PlatonicSolidFactory.createOctahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Octahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[2] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[2]);

		curObj = PlatonicSolidFactory.createIcosahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Icosahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[3] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[3]);

		curObj = PlatonicSolidFactory.createDodecahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Dodecahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[4] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[4]);

		createTesselateButton(platonicSolidShelf,  0.35f , 0);
		createTesselateButton(platonicSolidShelf,  0.175f, 1);
		createTesselateButton(platonicSolidShelf,  0     , 2);
		createTesselateButton(platonicSolidShelf, -0.175f, 3);
		createTesselateButton(platonicSolidShelf, -0.35f , 4);

		platonicSolidShelf.transform.localPosition = position;
		platonicSolidShelf.transform.localEulerAngles = angles;
	}

	private void createTesselateButton(GameObject parent, float x, int solidNum) {

		GameObject curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(x, 0.937f, 0.155f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 0);
		curObj.transform.localScale = new Vector3(0.06f, 0.02f, 0.03f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_PURPLE);
		ButtonCtrl.add(
			new DefaultButton(
				curObj,
				() => {
					initTesselation(solidNum);
				}
			)
		);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Button Frame";
		curObj.transform.parent = parent.transform;
		curObj.transform.localPosition = new Vector3(x, 0.9329f, 0.1509f);
		curObj.transform.localEulerAngles = new Vector3(45, 0, 0);
		curObj.transform.localScale = new Vector3(0.07f, 0.005f, 0.04f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.PLASTIC_WHITE);
	}

	private void initTesselation(int solidNum) {

		teslator[solidNum] = 0.0001f;

		// get the purple parent renderer separately
		MeshRenderer parentRen = solids[solidNum].gameObject.GetComponent<MeshRenderer>();

		// get all child renderers - including the purple parent renderer again, sadly...
		MeshRenderer[] allRenderers = solids[solidNum].gameObject.GetComponentsInChildren<MeshRenderer>();

		teslateRenderers[solidNum] = new MeshRenderer[allRenderers.Length];
		teslatedLast[solidNum] = 0;

		// ... but then filter it out, and only assign the white line renderers...
		int i = 0;
		foreach (MeshRenderer renderer in allRenderers) {
			if (renderer != parentRen) {
				teslateRenderers[solidNum][i++] = renderer;
			}
			renderer.enabled = false;
		}

		// ... and then explicitly assign it as the last element!
		teslateRenderers[solidNum][i++] = parentRen;
	}

}

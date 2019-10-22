/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class MathWorldCtrl: ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject platonicSolidShelf;

	private GameObject[] solids;


	public MathWorldCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addResetteableCtrl(this);

		createPlatonicSolidShelf(position, angles);

		reset();
	}

	public void reset() {

		solids[0].transform.localPosition = new Vector3(0.4f, 1.084191f, -0.075f);
		solids[0].transform.localEulerAngles = new Vector3(0, 33, 0);
		solids[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		solids[1].transform.localPosition = new Vector3(0.2f, 1.1f, -0.055f);
		solids[1].transform.localEulerAngles = new Vector3(0, 0, 0);
		solids[1].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		solids[2].transform.localPosition = new Vector3(0, 1.1f, -0.055f);
		solids[2].transform.localEulerAngles = new Vector3(0, 0, 0);
		solids[2].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		solids[3].transform.localPosition = new Vector3(-0.2f, 1.1f, -0.055f);
		solids[3].transform.localEulerAngles = new Vector3(0, 0, 0);
		solids[3].transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);

		solids[4].transform.localPosition = new Vector3(-0.4f, 1.1f, -0.055f);
		solids[4].transform.localEulerAngles = new Vector3(0, 0, 0);
		solids[4].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

/*
		foreach (GameObject bowlingBall in bowlingBalls) {
			bowlingBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			bowlingBall.transform.localEulerAngles = new Vector3(0, 0, 0);
		}

		bowlingBalls[0].transform.localPosition = new Vector3(0, 0.1f, -2.8f);
		bowlingBalls[1].transform.localPosition = new Vector3(0.75f, 0.1f, -2.8f);
		bowlingBalls[2].transform.localPosition = new Vector3(0.75f, 0.1f, -3.2f);
*/
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

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		curObj.name = "Mid Shelf";
		curObj.transform.parent = platonicSolidShelf.transform;
		curObj.transform.localPosition = new Vector3(0, 0.5f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 90);
		curObj.transform.localScale = new Vector3(0.05f, 0.9f, 0.35f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_PARTICLEBOARD);

		solids = new GameObject[5];

		curObj = ObjectFactory.createTetrahedron(MaterialCtrl.PLASTIC_PURPLE);
		solids[0] = curObj;
		curObj.name = "Platonic Tetrahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		MeshCollider meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		ObjectCtrl.add(new ThrowableObject(curObj));

		curObj = ObjectFactory.createCube(MaterialCtrl.PLASTIC_PURPLE);
		solids[1] = curObj;
		curObj.name = "Platonic Cube";
		curObj.transform.parent = platonicSolidShelf.transform;
		col = curObj.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0, 0);
		col.size = new Vector3(1, 1, 1);
		ObjectCtrl.add(new ThrowableObject(curObj));

		curObj = ObjectFactory.createOctahedron(MaterialCtrl.PLASTIC_PURPLE);
		solids[2] = curObj;
		curObj.name = "Platonic Octahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		col = curObj.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0, 0);
		col.size = new Vector3(1, 1.4f, 1);
		ObjectCtrl.add(new ThrowableObject(curObj));

		curObj = ObjectFactory.createIcosahedron(MaterialCtrl.PLASTIC_PURPLE);
		solids[3] = curObj;
		curObj.name = "Platonic Icosahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		col = curObj.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0, 0);
		col.size = new Vector3(1.55f, 1.55f, 1.55f);
		ObjectCtrl.add(new ThrowableObject(curObj));

		curObj = ObjectFactory.createDodecahedron(MaterialCtrl.PLASTIC_PURPLE);
		solids[4] = curObj;
		curObj.name = "Platonic Dodecahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		col = curObj.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0, 0);
		col.size = new Vector3(1.55f, 1.55f, 1.55f);
		ObjectCtrl.add(new ThrowableObject(curObj));

		platonicSolidShelf.transform.localPosition = position;
		platonicSolidShelf.transform.localEulerAngles = angles;
	}
}

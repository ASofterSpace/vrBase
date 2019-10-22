/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class MathWorldCtrl: UpdateableCtrl, ResetteableCtrl {

	private GameObject hostRoom;

	private GameObject platonicSolidShelf;

	private ThrowableBoundObject[] solids;


	public MathWorldCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.hostRoom = hostRoom;

		mainCtrl.addUpdateableCtrl(this);
		mainCtrl.addResetteableCtrl(this);

		createPlatonicSolidShelf(position, angles);

		reset();
	}

	public void update(VrInput input) {

		foreach (ThrowableBoundObject solid in solids) {
			if (solid.isBound()) {
				solid.transform.localEulerAngles = new Vector3(0, 25 * Time.time, 0);
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

		curObj = ObjectFactory.createTetrahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Tetrahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		MeshCollider meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[0] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[0]);

		curObj = ObjectFactory.createCube(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Cube";
		curObj.transform.parent = platonicSolidShelf.transform;
		col = curObj.AddComponent<BoxCollider>();
		col.center = new Vector3(0, 0, 0);
		col.size = new Vector3(1, 1, 1);
		solids[1] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[1]);

		curObj = ObjectFactory.createOctahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Octahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[2] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[2]);

		curObj = ObjectFactory.createIcosahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Icosahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[3] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[3]);

		curObj = ObjectFactory.createDodecahedron(MaterialCtrl.PLASTIC_PURPLE);
		curObj.name = "Platonic Dodecahedron";
		curObj.transform.parent = platonicSolidShelf.transform;
		meshCol = curObj.AddComponent<MeshCollider>();
		meshCol.convex = true;
		solids[4] = new ThrowableBoundObject(curObj);
		ObjectCtrl.add(solids[4]);

		platonicSolidShelf.transform.localPosition = position;
		platonicSolidShelf.transform.localEulerAngles = angles;
	}
}

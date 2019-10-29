

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public abstract class PrettyDomeCtrl: GenericRoomCtrl {

	protected float beamThickness = 0.1f;

	protected float cornerSphereThickness;


	public PrettyDomeCtrl(MainCtrl mainCtrl, GameObject thisRoom): base(mainCtrl, thisRoom) {

		cornerSphereThickness = 2 * beamThickness;
	}

	protected virtual void createFloor() {

		// create floor from the top
		GameObject floor = createPrimitive(PrimitiveType.Quad);
		floor.name = TriggerCtrl.FLOOR_NAME;
		floor.transform.localPosition = new Vector3(0, 0, 0);
		floor.transform.eulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(12, 12, 1);
		MaterialCtrl.setMaterial(floor, MaterialCtrl.BUILDING_FLOOR_CONCRETE_ROUND);

		// create floor from the bottom
		// (such that we can look at it from the bottom when we grab it in the diorama)
		GameObject floorBtm = createPrimitive(PrimitiveType.Quad);
		floorBtm.transform.localPosition = new Vector3(0, -0.01f, 0);
		floorBtm.transform.eulerAngles = new Vector3(-90, 0, 0);
		floorBtm.transform.localScale = new Vector3(12, 12, 1);
		MaterialCtrl.setMaterial(floorBtm, MaterialCtrl.BUILDING_FLOOR_CONCRETE_ROUND);
	}

	protected List<Vector3> prettyDomeCoords(float DomeRadius, int corners) {
		float offset = 0.1f;

		float Levels = 9;
		//int j = 1;
		var LevelList = new List<Vector3>();
		for (int j=0; j<=Levels; j++ ) {
			int LevelStartIndex = LevelList.Count;
			for (int i=0; i<=corners; i++ ) {
				float phi = Mathf.PI * (i*(360/corners) + offset)/ 180.0f;
				float theta = Mathf.Acos((1-j/Levels)) ;

				LevelList.Add(new Vector3(DomeRadius*Mathf.Sin(phi)*Mathf.Sin(theta),
										  DomeRadius*(1-j/Levels),
										  DomeRadius*Mathf.Cos(phi)*Mathf.Sin(theta)));
			}
			LevelList.Add(LevelList[LevelStartIndex]);
		}
		//closing each circle on each level
		return LevelList;
	}


	protected virtual void createBeams() {

		GameObject curBeam;

		List<Vector3> LevelListi = prettyDomeCoords(6.0f, 6);

		for (int i=1; i < LevelListi.Count-1; i++ ) {
			curBeam = connectBeam(LevelListi[i], LevelListi[i+1]);

		}


		/* Test cases for my connectBeam method
		Vector3 a = new Vector3(0, 0, 1);
		Vector3 b = new Vector3(1, 3, 1);
		Vector3 c = new Vector3(0, 5, 0);
		GameObject Cube_a = createPrimitive(PrimitiveType.Sphere);
		Cube_a.transform.localPosition = a;
		Cube_a.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		GameObject Cube_b = createPrimitive(PrimitiveType.Sphere);
		Cube_b.transform.localPosition = b;
		Cube_b.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		GameObject Cube_c = createPrimitive(PrimitiveType.Sphere);
		Cube_c.transform.localPosition = c;
		Cube_c.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		curBeam = connectBeam(a, b);
		curBeam = connectBeam(b, c);
		curBeam = connectBeam(c, a);
		curBeam = connectBeam(a, c);
		*/
	}

	/*
	* Create and connect one beam to its start & endpoints
	*/
	protected GameObject connectBeam(Vector3 startpoint, Vector3 endpoint) {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.name = "beam" + curBeamNum;

		//move cylinder to middlepoint between startpoint and endpoint
		Vector3 directionVector = (endpoint-startpoint);
		curBeam.transform.localPosition = (endpoint + startpoint)/2.0F;

		//rotate cylinder
		Vector3 cylDefaultOrientation = new Vector3(0, 1 , 0);
		curBeam.transform.localRotation = Quaternion.FromToRotation(cylDefaultOrientation, directionVector);

		/*scale cylinder*/
		float dist = Vector3.Distance(endpoint, startpoint);
		curBeam.transform.localScale = new Vector3(beamThickness, dist/2, beamThickness);

		MaterialCtrl.setMaterial(curBeam, MaterialCtrl.BUILDING_BEAM_WHITE);
		beams[curBeamNum++] = curBeam;

		GameObject curSphere = createPrimitive(PrimitiveType.Sphere);
		curSphere.transform.localPosition = startpoint;
		curSphere.transform.localScale = new Vector3(cornerSphereThickness, cornerSphereThickness, cornerSphereThickness);
		MaterialCtrl.setMaterial(curSphere, MaterialCtrl.BUILDING_BEAM_WHITE);

		return curBeam;
	}

}

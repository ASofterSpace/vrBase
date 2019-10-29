

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public abstract class PrettyDome2Ctrl: PrettyDomeCtrl {

	public PrettyDome2Ctrl(MainCtrl mainCtrl, GameObject thisRoom): base(mainCtrl, thisRoom) {

		// this pretty room needs looooots of beams!
		this.beams = new GameObject[1000];

		cornerSphereThickness = beamThickness;
	}

	protected List<Vector3> prettyDome2Coords(float DomeRadius, int spiralturns) {
		/*a spiral dome
		has $resolution points per turn of the spiral*/

		float offset = 0.0f;

		float resolution = 500/(2*spiralturns); // how many points are being calculated per spiralturn

		float Levels = resolution*spiralturns;
		var LevelList = new List<Vector3>();
		for (int j=0; j<=Levels; j++ ) {
			int LevelStartIndex = LevelList.Count;
			float phi = Mathf.PI * (j*(360/resolution) + offset)/ 180.0f;
			float theta = Mathf.Acos((1-j/Levels)) ;

			LevelList.Add(new Vector3(DomeRadius*Mathf.Sin(phi)*Mathf.Sin(theta),
									  DomeRadius*(1-j/Levels),
									  DomeRadius*Mathf.Cos(phi)*Mathf.Sin(theta)));
			//Debug.Log(LevelList[LevelList.Count-1]);

			//closing each circle on each level
			LevelList.Add(LevelList[LevelStartIndex]);
		}

		Debug.Log(LevelList.Count);

		return LevelList;
	}


	protected virtual void createBeams() {

		GameObject curBeam;

		List<Vector3> LevelListi = prettyDome2Coords(6.0f, 10);

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

}



using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public abstract class PrettyDomeCtrl {

	protected MainCtrl mainCtrl;
	protected string roomName;
	protected GameObject thisRoom;
	protected GameObject[] beams;
	private int curBeamNum;


	public PrettyDomeCtrl(MainCtrl mainCtrl, GameObject thisRoom) {

		this.mainCtrl = mainCtrl;

		this.thisRoom = thisRoom;

		this.roomName = thisRoom.name;

		// let's have more slots available than should be necessary:
		// we create 48 for a standard room here, but special rooms
		// (such as the control room) add extra beams afterwards
		this.beams = new GameObject[200];

		createRoom();
	}

	protected abstract void createRoom();

	protected GameObject createPrimitive(PrimitiveType type) {
		GameObject result = GameObject.CreatePrimitive(type);
		result.transform.parent = thisRoom.transform;
		return result;
	}

	protected virtual void createFloor() {

		GameObject floor = createPrimitive(PrimitiveType.Quad);
		floor.name = TriggerCtrl.FLOOR_NAME;
		floor.transform.localPosition = new Vector3(0, 0, 0);
		floor.transform.eulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(15, 15, 1);
		MaterialCtrl.setMaterial(floor, MaterialCtrl.BUILDING_FLOOR_CONCRETE);

	}

	protected List<Vector3> prettyDomeCoords(float DomeRadius, int corners) {
		float offset = 0.1f;

		float Levels = 9;
		//int j = 1;
		var LevelList = new List<Vector3>();
		for (int j=0; j<=Levels; j++ ) {
			int LevelStartIndex = LevelList.Count;
			for (int i=0; i<=corners; i++ ) {
				double phi = Math.PI * (i*(360/corners) + offset)/ 180.0f;
				double theta = Math.Acos((1-j/Levels)) ;

				LevelList.Add(new Vector3(System.Convert.ToSingle(DomeRadius*Math.Sin(phi)*Math.Sin(theta)),
																	System.Convert.ToSingle(DomeRadius*(1-j/Levels)),
																	System.Convert.ToSingle(DomeRadius*Math.Cos(phi)*Math.Sin(theta))
																	 ) );
			 /*LevelList.Add(new Vector3(DomeRadius*MathF.Sin(phi)*MathF.Sin(theta),
																	DomeRadius*(1-j/Levels),
																	DomeRadius*MathF.Cos(phi)*MathF.Sin(theta)
																	 ) );*/
			}
			LevelList.Add(LevelList[LevelStartIndex]);
			}
			 //closing each circle on each level
		return LevelList;
	}


	protected virtual void createBeams() {

		curBeamNum = 0;

		GameObject curBeam;

		var LevelListi = new List<Vector3>( prettyDomeCoords(6.0f, 6)  );

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

	/**
	  * Create one beam
  */
	protected GameObject createBeam(float length) {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.name = "beam" + curBeamNum;
		curBeam.transform.localScale = new Vector3(0.1f, length, 0.1f);
		MaterialCtrl.setMaterial(curBeam, MaterialCtrl.BUILDING_BEAM_WHITE);
		beams[curBeamNum++] = curBeam;
		return curBeam;
	}

	/*
	* Create and connect one beam to its start & endpoints
	*/
	protected GameObject connectBeam(Vector3 startpoint, Vector3 endpoint) {
		GameObject curBeam = createPrimitive(PrimitiveType.Cylinder);
		curBeam.name = "beam" + curBeamNum;
		float BeamThickness = 0.1f;

		//move cylinder to middlepoint between startpoint and endpoint
		Vector3 directionVector = (endpoint-startpoint);
		curBeam.transform.position = (endpoint + startpoint)/2.0F;

		//rotate cylinder
		Vector3 cylDefaultOrientation = new Vector3(0, 1 , 0);
		curBeam.transform.rotation = Quaternion.FromToRotation(cylDefaultOrientation, directionVector);

		/*scale cylinder*/
		float dist = Vector3.Distance(endpoint, startpoint);
		curBeam.transform.localScale = new Vector3(BeamThickness, dist/2, BeamThickness);

		MaterialCtrl.setMaterial(curBeam, MaterialCtrl.BUILDING_BEAM_WHITE);
		beams[curBeamNum++] = curBeam;

		GameObject curSphere = createPrimitive(PrimitiveType.Sphere);
		curSphere.transform.position = startpoint;
		float CornerSphereThickness = 2*BeamThickness;
		curSphere.transform.localScale = new Vector3(CornerSphereThickness, CornerSphereThickness, CornerSphereThickness);
		MaterialCtrl.setMaterial(curSphere, MaterialCtrl.BUILDING_BEAM_WHITE);

		return curBeam;
	}

	/**
	 * Add beams that have been created as copies by the ObjectFactory
	 * to our internal model of all available beams
	 */
	protected void addBeams(GameObject[] curBeams) {
		foreach (GameObject curBeam in curBeams) {
			curBeam.name = "beam" + curBeamNum;
			beams[curBeamNum++] = curBeam;
		}
	}

	protected void createMeshedWall() {
		return;
	}

	protected virtual int getAdditionalWallVertexAmount() {
		return 0;
	}

	protected virtual void createAdditionalWallVertices(Vector3[] vertices, int i) {
	}

	protected virtual int[] createMeshedWallTriangles() {

		int[] triangles = new int[6*4*8];
		int i = 0;

		// block 1 - North
		i = addTriangleWallBlock(triangles, i, 0);
		// block 2 - South
		i = addTriangleWallBlock(triangles, i, 6);
		// block 3 - East
		i = addTriangleWallBlock(triangles, i, 12);
		// block 4 - West
		i = addTriangleWallBlock(triangles, i, 18);
		// block 5 - North-East
		i = addTriangleWallBlock(triangles, i, 24);
		// block 6 - South-East
		i = addTriangleWallBlock(triangles, i, 30);
		// block 7 - South-West
		i = addTriangleWallBlock(triangles, i, 36);
		// block 8 - North-West
		i = addTriangleWallBlock(triangles, i, 42);

		return triangles;
	}

	// we add each triangle twice, once inwards facing, once outwards facing
	protected int addTriangle(int[] triangles, int i, int a, int b, int c) {
		triangles[i]   = a;
		triangles[i+1] = b;
		triangles[i+2] = c;
		triangles[i+3] = a;
		triangles[i+4] = c;
		triangles[i+5] = b;
		return i + 6;
	}

	/**
	 * i is the index into triangles that we are currently at
	 * start is the index into vertices that we are currently at
	 */
	protected int addTriangleWallBlock(int[] triangles, int i, int start) {
		i = addTriangle(triangles, i, start, start + 1, start + 2);
		i = addTriangle(triangles, i, start, start + 2, start + 3);
		i = addTriangle(triangles, i, start, start + 1, start + 4);
		i = addTriangle(triangles, i, start, start + 4, start + 5);
		return i;
	}

	protected int createWallBlockVertices(Vector3[] vertices, int howManyVertices, int i, float x, float y, float z) {
		int startI = i;

		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.x, y + orig.y, z + orig.z);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x + orig.z, y + orig.y, z + orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x + orig.z, y + orig.y, z - orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			int origIndex = j + startI - howManyVertices;
			Vector3 orig = vertices[origIndex];
			vertices[origIndex] = new Vector3(x + orig.x, y + orig.y, z + orig.z);
		}

		return i;
	}

	protected int createWallBlockVertices45(Vector3[] vertices, int howManyVertices, int i, float x, float y, float z) {
		int startI = i;

		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x + orig.z, y + orig.y, z - orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.x, y + orig.y, z - orig.z);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.z, y + orig.y, z + orig.x);
		}
		for (int j = 0; j < howManyVertices; j++) {
			int origIndex = j + startI - howManyVertices;
			Vector3 orig = vertices[origIndex];
			vertices[origIndex] = new Vector3(x + orig.x, y + orig.y, z + orig.z);
		}

		return i;
	}

	protected GameObject createDoor(float x, float z) {

		GameObject door = new GameObject("door");
		door.transform.parent = thisRoom.transform;
		door.transform.localPosition = new Vector3(x, 0, z);

		GameObject bottomRightBeam = createDoorBeam(door, 0.29f);
		bottomRightBeam.transform.localPosition = new Vector3(-0.54f, 0.15f, 0);
		bottomRightBeam.transform.eulerAngles = new Vector3(0, 0, 45);

		GameObject rightBeam = createDoorBeam(door, 0.77f);
		rightBeam.transform.localPosition = new Vector3(-0.74f, 1.08f, 0);
		rightBeam.transform.eulerAngles = new Vector3(0, 0, 0);

		GameObject topRightBeam = createDoorBeam(door, 0.28f);
		topRightBeam.transform.localPosition = new Vector3(-0.545f, 2.02f, 0);
		topRightBeam.transform.eulerAngles = new Vector3(0, 0, -45);

		GameObject topBeam = createDoorBeam(door, 0.4f);
		topBeam.transform.localPosition = new Vector3(0, 2.2f, 0);
		topBeam.transform.eulerAngles = new Vector3(0, 0, 90);

		GameObject topLeftBeam = createDoorBeam(door, 0.28f);
		topLeftBeam.transform.localPosition = new Vector3(0.545f, 2.02f, 0);
		topLeftBeam.transform.eulerAngles = new Vector3(0, 0, 45);

		GameObject leftBeam = createDoorBeam(door, 0.77f);
		leftBeam.transform.localPosition = new Vector3(0.74f, 1.08f, 0);
		leftBeam.transform.eulerAngles = new Vector3(0, 0, 0);

		GameObject bottomLeftBeam = createDoorBeam(door, 0.29f);
		bottomLeftBeam.transform.localPosition = new Vector3(0.54f, 0.15f, 0);
		bottomLeftBeam.transform.eulerAngles = new Vector3(0, 0, -45);

		return door;
	}

	private GameObject createDoorBeam(GameObject door, float length) {
		GameObject result = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		result.transform.parent = door.transform;
		result.transform.localScale = new Vector3(0.15f, length, 0.15f);
		MaterialCtrl.setMaterial(result, MaterialCtrl.PLASTIC_PURPLE);
		return result;
	}

	protected void createTank(String name, float x, float z) {

		GameObject tank = new GameObject(name);
		tank.transform.parent = thisRoom.transform;
		tank.transform.localPosition = new Vector3(x, 0, z);

		GameObject tankMain = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		tankMain.transform.parent = tank.transform;
		tankMain.transform.localPosition = new Vector3(0, 0.9f, 0);
		tankMain.transform.localScale = new Vector3(1, 1, 1);
		MaterialCtrl.setMaterial(tankMain, MaterialCtrl.PLASTIC_WHITE);

		GameObject tankFoot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		tankFoot.transform.parent = tank.transform;
		tankFoot.transform.localPosition = new Vector3(-0.4f, 0, 0);
		tankFoot.transform.eulerAngles = new Vector3(0, 0, -30);
		tankFoot.transform.localScale = new Vector3(0.1f, 0.15f, 0.1f);
		MaterialCtrl.setMaterial(tankFoot, MaterialCtrl.PLASTIC_WHITE);
		ObjectMultiplier.pointQuadruplize(tankFoot);
	}

}

/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public abstract class GenericRoomCtrl {

	protected MainCtrl mainCtrl;
	protected string roomName;
	protected GameObject thisRoom;
	protected GameObject[] beams;

	private int curBeamNum;

	private bool insideOutWallMesh = false;


	public GenericRoomCtrl(MainCtrl mainCtrl, GameObject thisRoom) {

		this.mainCtrl = mainCtrl;

		this.thisRoom = thisRoom;

		this.roomName = thisRoom.name;

		// let's have more slots available than should be necessary:
		// we create 48 for a standard room here, but special rooms
		// (such as the control room) add extra beams afterwards
		this.beams = new GameObject[200];
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
		floor.transform.localEulerAngles = new Vector3(90, 0, 0);
		floor.transform.localScale = new Vector3(10, 10, 1);
		MaterialCtrl.setMaterial(floor, MaterialCtrl.BUILDING_FLOOR_CONCRETE);

		GameObject floor2 = createPrimitive(PrimitiveType.Quad);
		floor2.name = TriggerCtrl.FLOOR_NAME;
		floor2.transform.localPosition = new Vector3(0, -0.001f, 0);
		floor2.transform.localEulerAngles = new Vector3(90, 45, 0);
		floor2.transform.localScale = new Vector3(10, 10, 1);
		MaterialCtrl.setMaterial(floor2, MaterialCtrl.BUILDING_FLOOR_CONCRETE);
	}

	protected virtual void createBeams() {

		curBeamNum = 0;

		GameObject curBeam;

		// create lowest level:
		// 1) alternatingly a tall and a little vertically standing beam,
		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(-6.7f, 0.9f, 0);
		curBeam.transform.localEulerAngles = new Vector3(0, 0, -20);
		addBeams(ObjectFactory.pointOctuplize(curBeam));
		curBeam = createBeam(0.425f);
		curBeam.transform.localPosition = new Vector3(-4.88f, 0.4f, 2);
		curBeam.transform.localEulerAngles = new Vector3(-5, 0, -15);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// 2) a diagonal-ish crossbeam,
		curBeam = createBeam(1.37f);
		curBeam.transform.localPosition = new Vector3(-5.555f, 1.32f, 0.996f);
		curBeam.transform.localEulerAngles = new Vector3(-68, 50, -10);
		addBeams(ObjectFactory.axisHexadeciplize(curBeam));

		// 3) and finally a beam on the floor,
		// all of this done for each of the 16 sides
		curBeam = createBeam(1.45f);
		curBeam.transform.localPosition = new Vector3(-5.985f, 0, 1.03f);
		curBeam.transform.localEulerAngles = new Vector3(90, 0, -45);
		addBeams(ObjectFactory.axisHexadeciplize(curBeam));

		// create second level:
		// 2 V-shaped diagonal beams on top of each of the short vertical ones
		curBeam = createBeam(0.8f);
		curBeam.transform.localPosition = new Vector3(-4.87f, 1.55f, 1.7f);
		curBeam.transform.localEulerAngles = new Vector3(-20, 20, 0);
		addBeams(ObjectFactory.axisOctuplize(curBeam));
		curBeam = createBeam(0.8f);
		curBeam.transform.localPosition = new Vector3(-4.677f, 1.597f, 2.266f);
		curBeam.transform.localEulerAngles = new Vector3(20, 20, 0);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// 2 very wide V-shaped diagonal beams on top of each other of the long vertical ones
		curBeam = createBeam(1.019898f);
		curBeam.transform.localPosition = new Vector3(-3.491f, 2.077f, 4.552f);
		curBeam.transform.localEulerAngles = new Vector3(-73.006f, -122.638f, 28.424f);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// create third level:
		// short horizontal beams between the 2V-shaped ones on top of the short vertical ones
		curBeam = createBeam(0.56f);
		curBeam.transform.localPosition = new Vector3(4.78f, 2.325f, 1.97f);
		curBeam.transform.localEulerAngles = new Vector3(0, 72, -92.5f);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// create fourth level:
		// long beams upwards from the long ones of the 1st level,
		curBeam = createBeam(1.0f);
		curBeam.transform.localPosition = new Vector3(5.65f, 2.54f, 0);
		curBeam.transform.localEulerAngles = new Vector3(0, 0, 45);
		addBeams(ObjectFactory.pointQuadruplize(curBeam));

		// inverted-V-beams upwards from the triangles from the 2nd and 3rd level
		curBeam = createBeam(0.9f);
		curBeam.transform.localPosition = new Vector3(0.72f, 2.77f, -4.97f);
		curBeam.transform.localEulerAngles = new Vector3(-13, 10, 57);
		addBeams(ObjectFactory.axisOctuplize(curBeam));
		curBeam = createBeam(1.46f);
		curBeam.transform.localPosition = new Vector3(3.63f, 3.6f, 0.46f);
		curBeam.transform.localEulerAngles = new Vector3(50, 0, 67);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// create mid level:
		// long beams connecting the 2nd/3rd-level triangles with the 3rd-highest-level long beams
		curBeam = createBeam(1.28f);
		curBeam.transform.localPosition = new Vector3(3.76f, 3.27f, 2.7f);
		curBeam.transform.localEulerAngles = new Vector3(38, -42, 25);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// create third-highest level:
		// 8 diagonal beams that sprout from 4 points,
		curBeam = createBeam(0.9f);
		curBeam.transform.localPosition = new Vector3(-1.85f, 4.62f, -0.45f);
		curBeam.transform.localEulerAngles = new Vector3(45, 45, 0);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// then 4 horizontal beams that connect always 2 each of these 8 beams,
		curBeam = createBeam(0.9f);
		curBeam.transform.localPosition = new Vector3(0, 4, 2.3f);
		curBeam.transform.localEulerAngles = new Vector3(0, 0, 90);
		addBeams(ObjectFactory.pointQuadruplize(curBeam));

		// then 8 horizontal beams to the left and right of those,
		curBeam = createBeam(1.0444f);
		curBeam.transform.localPosition = new Vector3(-1.875f, 4.083f, 2.593f);
		curBeam.transform.localEulerAngles = new Vector3(81.32201f, -30.069f, 43.058f);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// then 4 long downwards beams between them
		curBeam = createBeam(1.3f);
		curBeam.transform.localPosition = new Vector3(2.0f, 4.52f, -2.0f);
		curBeam.transform.localEulerAngles = new Vector3(75, -45, 0);
		addBeams(ObjectFactory.axisQuadruplize(curBeam));

		// create second-highest level: 8 horizontal beams that form the basis of highest level
		curBeam = createBeam(0.6f);
		curBeam.transform.localPosition = new Vector3(0.55f, 5.05f, -1.25f);
		curBeam.transform.localEulerAngles = new Vector3(-40, 0, 65);
		addBeams(ObjectFactory.axisOctuplize(curBeam));

		// create highest level: 8 beams that come together in a single point
		curBeam = createBeam(0.75f);
		curBeam.transform.localPosition = new Vector3(0, 5.5f, -0.7f);
		curBeam.transform.localEulerAngles = new Vector3(70, 0, 0);
		addBeams(ObjectFactory.pointQuadruplize(curBeam));
		curBeam = createBeam(0.9f);
		curBeam.transform.localPosition = new Vector3(-0.55f, 5.3f, -0.55f);
		curBeam.transform.localEulerAngles = new Vector3(60, 45, 0);
		addBeams(ObjectFactory.axisQuadruplize(curBeam));
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

		insideOutWallMesh = false;
		GameObject meshWall = createMeshedWallSide();

		insideOutWallMesh = true;
		GameObject meshWallInside = createMeshedWallSide();
		meshWallInside.name = "meshWall (Inside)";

		meshWallInside.transform.parent = meshWall.transform;
		meshWall.transform.parent = thisRoom.transform;

		MaterialCtrl.setMaterial(meshWall, MaterialCtrl.BUILDING_WALL);
		MaterialCtrl.setMaterial(meshWallInside, MaterialCtrl.BUILDING_WALL);
	}

	protected GameObject createMeshedWallSide() {

		// get the position of the parent explicitly, to add it to all points,
		// as we seem to specify the mesh in world coordinates...
		// TODO :: in case we want to rotate the parent object, actually figure
		// out how to specify a mesh in local coordinates!
		float x = thisRoom.transform.position.x;
		float y = thisRoom.transform.position.y;
		float z = thisRoom.transform.position.z;

		// create the mesh
		GameObject meshWall = new GameObject("meshWall");
		MeshFilter meshFilter = meshWall.AddComponent<MeshFilter>();
		meshWall.AddComponent<MeshRenderer>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		// create vertices that are available to create the mesh
		int i = 0;
		Vector3[] vertices = new Vector3[6*8 + getAdditionalWallVertexAmount()];

		// vertices for the lowest level
		// block 1 - North
		vertices[i++] = new Vector3(-7.05f, 0, 0);
		vertices[i++] = new Vector3(-6.4f, 1.84f, 0);
		vertices[i++] = new Vector3(-4.8f, 0.8f, 1.95f);
		vertices[i++] = new Vector3(-5, 0, 2);
		vertices[i++] = new Vector3(-4.8f, 0.8f, -1.95f);
		vertices[i++] = new Vector3(-5, 0, -2);

		// blocks 2, 3, 4 - South, East, West
		i = createWallBlockVertices(vertices, 6, i, x, y, z);

		// block 5 - North-East
		vertices[i++] = new Vector3(-4.96f, 0, 4.96f);
		vertices[i++] = new Vector3(-4.5f, 1.82f, 4.5f);
		vertices[i++] = new Vector3(-1.95f, 0.8f, 4.8f);
		vertices[i++] = new Vector3(-2, 0, 5);
		vertices[i++] = new Vector3(-4.8f, 0.8f, 1.95f);
		vertices[i++] = new Vector3(-5, 0, 2);

		// blocks 6, 7, 8 - South-East, South-West, North-West
		i = createWallBlockVertices45(vertices, 6, i, x, y, z);

		createAdditionalWallVertices(vertices, i);

		mesh.vertices = vertices;

		// create triangles using the previously set vertices
		int[] triangles = createMeshedWallTriangles();

		mesh.triangles = triangles;

		ObjectFactory.finalizeMesh(mesh);

		return meshWall;
	}

	protected virtual int getAdditionalWallVertexAmount() {
		return 0;
	}

	protected virtual void createAdditionalWallVertices(Vector3[] vertices, int i) {
	}

	protected virtual int[] createMeshedWallTriangles() {

		int[] triangles = new int[3*4*8];
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
		if (insideOutWallMesh) {
			triangles[i]   = a;
			triangles[i+1] = b;
			triangles[i+2] = c;
		} else {
			triangles[i]   = a;
			triangles[i+1] = c;
			triangles[i+2] = b;
		}
		return i + 3;
	}

	/**
	 * i is the index into triangles that we are currently at
	 * start is the index into vertices that we are currently at
	 */
	protected int addTriangleWallBlock(int[] triangles, int i, int start) {
		i = addTriangle(triangles, i, start, start + 1, start + 2);
		i = addTriangle(triangles, i, start, start + 2, start + 3);
		i = addTriangle(triangles, i, start, start + 4, start + 1);
		i = addTriangle(triangles, i, start, start + 5, start + 4);
		return i;
	}

	protected int createWallBlockVertices(Vector3[] vertices, int howManyVertices, int i, float x, float y, float z) {
		int startI = i;

		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.x, y + orig.y, z - orig.z);
		}
		for (int j = 0; j < howManyVertices; j++) {
			Vector3 orig = vertices[j + startI - howManyVertices];
			vertices[i++] = new Vector3(x - orig.z, y + orig.y, z + orig.x);
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
		bottomRightBeam.transform.localEulerAngles = new Vector3(0, 0, 45);

		GameObject rightBeam = createDoorBeam(door, 0.77f);
		rightBeam.transform.localPosition = new Vector3(-0.74f, 1.08f, 0);
		rightBeam.transform.localEulerAngles = new Vector3(0, 0, 0);

		GameObject topRightBeam = createDoorBeam(door, 0.28f);
		topRightBeam.transform.localPosition = new Vector3(-0.545f, 2.02f, 0);
		topRightBeam.transform.localEulerAngles = new Vector3(0, 0, -45);

		GameObject topBeam = createDoorBeam(door, 0.4f);
		topBeam.transform.localPosition = new Vector3(0, 2.2f, 0);
		topBeam.transform.localEulerAngles = new Vector3(0, 0, 90);

		GameObject topLeftBeam = createDoorBeam(door, 0.28f);
		topLeftBeam.transform.localPosition = new Vector3(0.545f, 2.02f, 0);
		topLeftBeam.transform.localEulerAngles = new Vector3(0, 0, 45);

		GameObject leftBeam = createDoorBeam(door, 0.77f);
		leftBeam.transform.localPosition = new Vector3(0.74f, 1.08f, 0);
		leftBeam.transform.localEulerAngles = new Vector3(0, 0, 0);

		GameObject bottomLeftBeam = createDoorBeam(door, 0.29f);
		bottomLeftBeam.transform.localPosition = new Vector3(0.54f, 0.15f, 0);
		bottomLeftBeam.transform.localEulerAngles = new Vector3(0, 0, -45);

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
		tankFoot.transform.localEulerAngles = new Vector3(0, 0, -30);
		tankFoot.transform.localScale = new Vector3(0.1f, 0.15f, 0.1f);
		MaterialCtrl.setMaterial(tankFoot, MaterialCtrl.PLASTIC_WHITE);
		ObjectFactory.pointQuadruplize(tankFoot);
	}

	protected GameObject createPoster(int material) {
		GameObject poster = createPrimitive(PrimitiveType.Quad);
		poster.transform.localScale = new Vector3(0.45f, 0.6f, 1);
		MaterialCtrl.setMaterial(poster, material);
		return poster;
	}

}

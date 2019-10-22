/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class DioramaCtrl : UpdateableCtrl {

	private MainCtrl mainCtrl;

	private GameObject hostRoom;

	private Transform worldTransform;
	private Transform dioramaHolderTransform;

	private GameObject diorama;
	private GameObject dioramaHolder;
	private GameObject dioramaSurface;
	private GameObject dioramaPedestolTop;


	public DioramaCtrl(MainCtrl mainCtrl, GameObject hostRoom, Vector3 position, Vector3 angles) {

		this.mainCtrl = mainCtrl;

		this.hostRoom = hostRoom;

		this.worldTransform = mainCtrl.getWorld().transform;

		mainCtrl.addUpdateableCtrl(this);

		createDiorama(position, angles);
	}

	public void update(VrInput input) {

		if (dioramaSurface == null) {
			// add the diorama itself by copying ALL surface objects
			dioramaSurface = Object.Instantiate(mainCtrl.getSurface(), dioramaHolder.transform, false);

			// I heard you like diorama's... so let's put a diorama inside your diorama! :D
			GameObject innerDioramaHolder = GameObject.Find("/World/Surface/ControlRoom/Diorama/Diorama Holder/Surface(Clone)/ControlRoom/Diorama/Diorama Holder");
			GameObject innerDioramaSurface = Object.Instantiate(dioramaSurface, innerDioramaHolder.transform, false);

			// destroy everything we don't need in the copy
			Joint[] joints = dioramaSurface.GetComponentsInChildren<Joint>();
			foreach (Joint joint in joints) {
				Object.Destroy(joint);
			}
			Rigidbody[] rigidbodies = dioramaSurface.GetComponentsInChildren<Rigidbody>();
			foreach (Rigidbody rb in rigidbodies) {
				Object.Destroy(rb);
			}
			AudioSource[] audiosources = dioramaSurface.GetComponentsInChildren<AudioSource>();
			foreach (AudioSource audioSource in audiosources) {
				Object.Destroy(audioSource);
			}
			Collider[] colliders = dioramaSurface.GetComponentsInChildren<Collider>();
			foreach (Collider collider in colliders) {
				Object.Destroy(collider);
			}

			BoxCollider col;
			PhysicMaterial physicsMat;
			Rigidbody rby;
			ThrowableObject curThrowable;

			GameObject dioramaRocket = GameObject.Find("/World/Surface/ControlRoom/Diorama/Diorama Holder/Surface(Clone)/Rocket Launcher/RocketHolder");
			col = dioramaRocket.AddComponent<BoxCollider>();
			col.center = new Vector3(0, 27.6f, 0);
			col.size = new Vector3(6, 55, 6);
			physicsMat = new PhysicMaterial();
			physicsMat.dynamicFriction = 1;
			physicsMat.staticFriction = 0.6f;
			physicsMat.bounciness = 0.05f;
			physicsMat.frictionCombine = PhysicMaterialCombine.Average;
			physicsMat.bounceCombine = PhysicMaterialCombine.Average;
			col.material = physicsMat;
			rby = dioramaRocket.AddComponent<Rigidbody>();
			rby.mass = 5;
			curThrowable = new ThrowableBoundObject(dioramaRocket);
			ObjectCtrl.add(curThrowable);
//			dioramaRocket.transform.parent = mainCtrl.getWorld().transform;

			GameObject dioramaControlRoom = GameObject.Find("/World/Surface/ControlRoom/Diorama/Diorama Holder/Surface(Clone)/ControlRoom");
			col = dioramaControlRoom.AddComponent<BoxCollider>();
			col.center = new Vector3(0, 2.475f, 0);
			col.size = new Vector3(10.5f, 5, 10.5f);
			physicsMat = new PhysicMaterial();
			physicsMat.dynamicFriction = 1;
			physicsMat.staticFriction = 0.6f;
			physicsMat.bounciness = 0.05f;
			physicsMat.frictionCombine = PhysicMaterialCombine.Average;
			physicsMat.bounceCombine = PhysicMaterialCombine.Average;
			col.material = physicsMat;
			rby = dioramaControlRoom.AddComponent<Rigidbody>();
			rby.mass = 2;
			curThrowable = new ThrowableBoundObject(dioramaControlRoom);
			ObjectCtrl.add(curThrowable);
//			dioramaControlRoom.transform.parent = mainCtrl.getWorld().transform;

			GameObject dioramaArcadeRoom = GameObject.Find("/World/Surface/ControlRoom/Diorama/Diorama Holder/Surface(Clone)/ArcadeRoom");
			col = dioramaArcadeRoom.AddComponent<BoxCollider>();
			col.center = new Vector3(0, 2.475f, 0);
			col.size = new Vector3(10.5f, 5, 10.5f);
			physicsMat = new PhysicMaterial();
			physicsMat.dynamicFriction = 1;
			physicsMat.staticFriction = 0.6f;
			physicsMat.bounciness = 0.05f;
			physicsMat.frictionCombine = PhysicMaterialCombine.Average;
			physicsMat.bounceCombine = PhysicMaterialCombine.Average;
			col.material = physicsMat;
			rby = dioramaArcadeRoom.AddComponent<Rigidbody>();
			rby.mass = 2;
			curThrowable = new ThrowableBoundObject(dioramaArcadeRoom);
			ObjectCtrl.add(curThrowable);
//			dioramaArcadeRoom.transform.parent = mainCtrl.getWorld().transform;

			GameObject dioramaScienceRoom = GameObject.Find("/World/Surface/ControlRoom/Diorama/Diorama Holder/Surface(Clone)/ScienceRoom");
			col = dioramaScienceRoom.AddComponent<BoxCollider>();
			col.center = new Vector3(0, 1.975f, 0);
			col.size = new Vector3(9.5f, 4, 9.5f);
			physicsMat = new PhysicMaterial();
			physicsMat.dynamicFriction = 1;
			physicsMat.staticFriction = 0.6f;
			physicsMat.bounciness = 0.05f;
			physicsMat.frictionCombine = PhysicMaterialCombine.Average;
			physicsMat.bounceCombine = PhysicMaterialCombine.Average;
			col.material = physicsMat;
			rby = dioramaScienceRoom.AddComponent<Rigidbody>();
			rby.mass = 1.8f;
			curThrowable = new ThrowableBoundObject(dioramaScienceRoom);
			ObjectCtrl.add(curThrowable);
//			dioramaScienceRoom.transform.parent = mainCtrl.getWorld().transform;
		}

		/*
		NOPE, this did not work... whoopsie!

		// check all throwables (or even all takeables), and for each calculate the 3D-distance
		// to the center point of the table... if it is smaller than the radius of the table, and y is
		// equal or higher, then assume that the thingen is lying on the table, so set the table as
		// parent - and if not, then set world as parent (unless a controller is actually parent currently)

		Dictionary<string, TakeableObject> objs = ObjectCtrl.getAll();
		Vector3 dioPos = dioramaPedestolTop.transform.position;
		float dioY = dioPos.y;

		// keep track of controller being parent!
		GameObject controllerLeft = mainCtrl.getVrSpecificCtrl().getControllerBehaviour(VrInput.LEFT).getController();
		GameObject controllerRight = mainCtrl.getVrSpecificCtrl().getControllerBehaviour(VrInput.RIGHT).getController();

		foreach (TakeableObject obj in objs.Values) {
			Vector3 objPos = obj.transform.position;
			if ((objPos.y > dioY) && (Vector3.Distance(objPos, dioPos) < 1.1f)) {
				if ((obj.transform.parent != controllerLeft) && (obj.transform.parent != controllerRight)) {
					obj.transform.parent = dioramaHolderTransform;
				}
			} else {
				if (obj.transform.parent == dioramaHolderTransform) {
					obj.transform.parent = worldTransform;
				}
			}
		}
		*/

		diorama.transform.localEulerAngles = new Vector3(0, Time.time * 1.5f, 0);
	}

	private void createDiorama(Vector3 position, Vector3 angles) {

		GameObject curObj;

		diorama = new GameObject("Diorama");
		diorama.transform.parent = hostRoom.transform;
		diorama.transform.localPosition = position;
		diorama.transform.localEulerAngles = angles;

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.49f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(0.2f, 0.49f, 0.2f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol Holder";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(-0.29f, 0.772f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 45);
		curObj.transform.localScale = new Vector3(0.025f, 0.306f, 0.05f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);
		ObjectFactory.pointQuadruplize(curObj);

		curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		curObj.name = "Diorama Pedestol Holder Base";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.596f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, -1.283f, 0);
		curObj.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
		MaterialCtrl.setMaterial(curObj, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);

		curObj = ObjectFactory.createCylinder(40, 0, false, MaterialCtrl.OBJECTS_MATERIALS_METAL_SHINY);
		curObj.name = "Diorama Pedestol Top";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.9845f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1.1f, 0.005f, 1.1f);
		dioramaPedestolTop = curObj;

		curObj = ObjectFactory.createCylinder(40, 10, false, MaterialCtrl.SPACE_MOON_FLOOR_INNER);
		curObj.name = "Diorama Pedestol Moon Floor";
		curObj.transform.parent = diorama.transform;
		curObj.transform.localPosition = new Vector3(0, 0.994f, 0);
		curObj.transform.localEulerAngles = new Vector3(0, 0, 0);
		curObj.transform.localScale = new Vector3(1.05f, 0.005f, 1.05f);
		GameObject dioramaMoonFloor = curObj;
/*
		Rigidbody rigidBody = dioramaMoonFloor.AddComponent<Rigidbody>();
		rigidBody.mass = 1;
		rigidBody.drag = 0;
		rigidBody.angularDrag = 0;
		rigidBody.useGravity = false;
		rigidBody.constraints = RigidbodyConstraints.FreezeAll;
*/
		dioramaHolder = new GameObject("Diorama Holder");
		dioramaHolder.transform.parent = diorama.transform;
		dioramaHolder.transform.localPosition = new Vector3(0, 1, 0);
		dioramaHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
		dioramaHolder.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
		dioramaHolderTransform = dioramaHolder.transform;


		/*
		Rigidbody rigidBody = barrier.AddComponent<Rigidbody>();
		rigidBody.mass = 1000;
		rigidBody.drag = 0;
		rigidBody.angularDrag = 0;
		rigidBody.useGravity = false;
		// set freeze position x, y, z true and freeze rotation x, y, z true
		rigidBody.constraints = RigidbodyConstraints.FreezeAll;
*/

/*
		rigidBody = diorama.AddComponent<Rigidbody>();
		rigidBody.mass = 1000;
		rigidBody.drag = 0;
		rigidBody.angularDrag = 0;
		rigidBody.useGravity = false;
		rigidBody.constraints = RigidbodyConstraints.FreezeAll;

		HingeJoint hingeJoint = diorama.AddComponent<HingeJoint>();
		hingeJoint.anchor = new Vector3(0, 0, 0);
		hingeJoint.autoConfigureConnectedAnchor = false;
		var motor = hingeJoint.motor;
		motor.targetVelocity = 1;
		motor.force = 1000;
		motor.freeSpin = false;
		hingeJoint.motor = motor;
		hingeJoint.useMotor = true;
		hingeJoint.useLimits = false;
		hingeJoint.breakForce = Mathf.Infinity;
		hingeJoint.breakTorque = Mathf.Infinity;
		hingeJoint.enableCollision = false;
		hingeJoint.enablePreprocessing = true;
		hingeJoint.massScale = 1;
		hingeJoint.connectedMassScale = 1;
		hingeJoint.axis = new Vector3(0, 1, 0);
		hingeJoint.connectedAnchor = new Vector3(0, 0.994f, 0);
		hingeJoint.connectedBody = dioramaMoonFloor.GetComponent<Rigidbody>();
*/
	}

}

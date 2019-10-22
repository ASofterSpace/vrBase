/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


/**
 * The button controller controls all the objects - where an "object" is anything
 * that the user can actually take and (if it also implements ThrowableObject) throw
 * (not like an exception, more like a bowling ball ^^)
 */
public class ObjectCtrl {

	public const string OBJECT_IDENTIFIER = "obj-";

	private static MainCtrl mainCtrl;

	private static int nextObj = 0;

	private static Dictionary<string, TakeableObject> objects;


	public static void init(MainCtrl mainCtrl) {

		ObjectCtrl.mainCtrl = mainCtrl;

		ObjectCtrl.objects = new Dictionary<string, TakeableObject>();
	}

	public static void add(TakeableObject obj) {

		nextObj++;

		obj.setMainCtrl(mainCtrl);

		// buttons keep their name, as they are a special kind of object, basically
		if (!obj.getName().StartsWith(ButtonCtrl.BUTTON_IDENTIFIER)) {
			obj.setName(OBJECT_IDENTIFIER + nextObj);
		}

		objects.Add(obj.getName(), obj);

		if (obj is UpdateableCtrl) {
			mainCtrl.addUpdateableCtrl((UpdateableCtrl) obj);
		}
		if (obj is ResetteableCtrl) {
			mainCtrl.addResetteableCtrl((ResetteableCtrl) obj);
		}
	}

	public static TakeableObject get(string objectName) {

		return objects[objectName];
	}

	public static Dictionary<string, TakeableObject> getAll() {

		return objects;
	}

}

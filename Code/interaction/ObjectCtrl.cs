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

	private static int nextObj = 0;

	private static Dictionary<string, TakeableObject> objects;


	public static void init() {

		objects = new Dictionary<string, TakeableObject>();
	}

	public static void add(TakeableObject obj) {

		nextObj++;

		obj.setName(OBJECT_IDENTIFIER + nextObj);

		objects.Add(obj.getName(), obj);
	}

	public static TakeableObject get(string objectName) {

		return objects[objectName];
	}

}

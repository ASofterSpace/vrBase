using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class MaterialCtrl {

	private static Material[] materials;
	private static string[] textures;

	public const int BUILDING_FLOOR_CONCRETE = 0;
	public const int BUILDING_FLOOR_WOOD = 15;
	public const int SPACE_MOON_FLOOR = 1;
	public const int SPACE_MOON_SOUTH = 2;
	public const int SPACE_MOON_WEST = 3;
	public const int SPACE_MOON_NORTH = 4;
	public const int SPACE_MOON_EAST = 5;
	public const int SPACE_EARTH = 6;
	public const int SPACE_SUN = 7;
	public const int SPACE_STAR = 8;
	public const int PLASTIC_PURPLE = 9;
	public const int PLASTIC_WHITE = 10;
	public const int PLASTIC_GRAY = 11;
	public const int PLASTIC_RED = 21;
	public const int INTERACTION_TELEPORT_TARGET = 12;
	public const int INTERACTION_TELEPORT_RAY = 13;
	public const int INTERACTION_BUTTON_HOVER = 23;
	public const int FADEABLE_BLACK = 14;
	public const int OBJECTS_BOWLING_BALL_RED = 16;
	public const int OBJECTS_BOWLING_PIN_WHITE = 17;
	public const int OBJECTS_BOWLING_PIN_RED = 18;
	public const int OBJECTS_BLOBFLYER_BLACK = 19;
	public const int OBJECTS_NOSTALGICCONSOLE_GREEN = 20;
	public const int OBJECTS_NOSTALGICCONSOLE_SCREEN = 22;
	// do not add anything after the amount ;)
	public const int MATERIAL_AMOUNT = 24;

	private static Material standard;
	private static Material standardFade;
	private static Material unlitColor;
	private static Material unlitTexture;
	private static Material unlitTransparent;


	public static void init() {

		materials = new Material[MATERIAL_AMOUNT];

		textures = new string[MATERIAL_AMOUNT];

		textures[BUILDING_FLOOR_CONCRETE] = "Building/Floor/concrete";
		textures[BUILDING_FLOOR_WOOD] = "Building/Floor/woodenLengthwiseFloor";
		textures[OBJECTS_NOSTALGICCONSOLE_GREEN] = "Building/Wall/wallpaper_1_azure_continuous";
		textures[OBJECTS_NOSTALGICCONSOLE_SCREEN] = "Objects/Screens/legacy_screen_on_wallpaper_1_azure_continuous";
		textures[SPACE_MOON_FLOOR] = "Space/moonFloor";
		textures[SPACE_MOON_SOUTH] = "Space/moonNorth";
		textures[SPACE_MOON_WEST] = "Space/moonNorthMirrored";
		textures[SPACE_MOON_NORTH] = "Space/moonNorth";
		textures[SPACE_MOON_EAST] = "Space/moonNorthMirrored";
		textures[SPACE_EARTH] = "Space/earth";
		textures[SPACE_SUN] = "Space/sun";
		textures[INTERACTION_TELEPORT_TARGET] = "Interaction/teleportTarget";

		standard = GameObject.Find("/Shaders/standard").GetComponent<Renderer>().material;
		standardFade = GameObject.Find("/Shaders/standardFade").GetComponent<Renderer>().material;
		unlitColor = GameObject.Find("/Shaders/unlitColor").GetComponent<Renderer>().material;
		unlitTexture = GameObject.Find("/Shaders/unlitTexture").GetComponent<Renderer>().material;
		unlitTransparent = GameObject.Find("/Shaders/unlitTransparent").GetComponent<Renderer>().material;
	}

	/**
	 * Get a material by its number
	 */
	public static Material getMaterial(int materialNum) {

		Material result = materials[materialNum];

		if (result == null) {
			switch (materialNum) {
				case SPACE_MOON_FLOOR:
				case SPACE_MOON_SOUTH:
				case SPACE_MOON_WEST:
				case SPACE_MOON_NORTH:
				case SPACE_MOON_EAST:
				case SPACE_EARTH:
				case SPACE_SUN:
					result = new Material(unlitTexture);
					break;
				case SPACE_STAR:
				case INTERACTION_TELEPORT_RAY:
				case INTERACTION_BUTTON_HOVER:
					result = new Material(unlitColor);
					break;
				case INTERACTION_TELEPORT_TARGET:
					result = new Material(unlitTransparent);
					break;
				case FADEABLE_BLACK:
					result = new Material(standardFade);
					break;
				default:
					result = new Material(standard);
					break;
			}
			switch (materialNum) {
				case PLASTIC_WHITE:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					break;
				case PLASTIC_PURPLE:
					result.color = new Color(0.3542f, 0.0654f, 0.6603f, 1.0f);
					break;
				case PLASTIC_GRAY:
					result.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
					break;
				case PLASTIC_RED:
					result.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
					break;
				case SPACE_STAR:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					break;
				case INTERACTION_TELEPORT_RAY:
					result.color = new Color(0.8f, 0.1f, 0.9f, 1.0f);
					break;
				case INTERACTION_BUTTON_HOVER:
					result.color = new Color(1.0f, 0.9f, 1.0f, 1.0f);
					break;
				case FADEABLE_BLACK:
					result.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
					break;
				case OBJECTS_BOWLING_BALL_RED:
					result.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
					break;
				case OBJECTS_BOWLING_PIN_WHITE:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					break;
				case OBJECTS_BOWLING_PIN_RED:
					result.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
					break;
				case OBJECTS_BLOBFLYER_BLACK:
					result.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
					break;
				default:
					result.mainTexture = Resources.Load<Texture2D>("Textures/" + textures[materialNum]);
					break;
			}
			materials[materialNum] = result;
		}

		return result;
	}

	/**
	 * Assign a material to a gameobject by its number
	 */
	public static void setMaterial(GameObject obj, int materialNum) {
		obj.GetComponent<Renderer>().material = getMaterial(materialNum);
	}

	public static void setColor(int materialNum, Color newColor) {
		Material material = getMaterial(materialNum);
		material.color = newColor;
	}
}

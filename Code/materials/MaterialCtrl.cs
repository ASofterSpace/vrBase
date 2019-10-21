/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class MaterialCtrl {

	private static Material[] materials;
	private static string[] textures;

	public const int BUILDING_FLOOR_CONCRETE = 0;
	public const int BUILDING_FLOOR_WOOD = 15;
	public const int BUILDING_BEAM_WHITE = 24;
	public const int BUILDING_WALL = 57;
	public const int INTERACTION_TELEPORT_TARGET = 12;
	public const int INTERACTION_TELEPORT_RAY = 13;
	public const int INTERACTION_BUTTON_HOVER = 23;
	public const int FADEABLE_BLACK = 14;
	public const int OBJECTS_BOWLING_BALL_RED = 16;
	public const int OBJECTS_BOWLING_PIN_WHITE = 17;
	public const int OBJECTS_BOWLING_PIN_RED = 18;
	public const int OBJECTS_BLOBFLYER_BLACK = 19;
	public const int OBJECTS_FIREFIGHTING_BTN_GREEN = 52;
	public const int OBJECTS_FIREFIGHTING_BTN_RED = 53;
	public const int OBJECTS_FIREFIGHTING_LABEL_QUESTION = 58;
	public const int OBJECTS_FIREFIGHTING_LABEL_CORRECT_READY = 59;
	public const int OBJECTS_FIREFIGHTING_LABEL_CORRECT_NOT_READY = 60;
	public const int OBJECTS_FIREFIGHTING_LABEL_WRONG_READY = 61;
	public const int OBJECTS_FIREFIGHTING_LABEL_WRONG_NOT_READY = 62;
	public const int OBJECTS_FIREFIGHTING_OXYGEN_GREEN = 47;
	public const int OBJECTS_FIREFIGHTING_OXYGEN_YELLOW = 48;
	public const int OBJECTS_LOGOS_ASOFTERSPACE = 50;
	public const int OBJECTS_LOGOS_ASOFTERSPACE_DARK = 51;
	public const int OBJECTS_MATERIALS_PARTICLEBOARD = 30;
	public const int OBJECTS_MATERIALS_METAL_DARK = 31;
	public const int OBJECTS_MATERIALS_METAL_SHINY = 49;
	public const int OBJECTS_NOSTALGICCONSOLE_GREEN = 20;
	public const int OBJECTS_NOSTALGICCONSOLE_SCREEN = 22;
	public const int OBJECTS_POSTERS_FLIPPERQND = 35;
	public const int OBJECTS_POSTERS_SOFTWARE = 36;
	public const int OBJECTS_POSTERS_MARS = 37;
	public const int OBJECTS_POSTERS_VR = 38;
	public const int OBJECTS_POSTERS_PROCESS = 39;
	public const int OBJECTS_ROCKETLAUNCH_LAUNCHPAD = 55;
	public const int OBJECTS_ROCKETLAUNCH_YELLOW = 56;
	public const int OBJECTS_TICTACTOE_BLUE = 42;
	public const int OBJECTS_TICTACTOE_RED = 41;
	public const int OBJECTS_TICTACTOE_GRAY = 40;
	public const int OBJECTS_TICTACTOE_ROBOT = 43;
	public const int OBJECTS_TICTACTOE_ROBOT_GRAY = 44;
	public const int OBJECTS_TICTACTOE_LABELS_RESTART = 45;
	public const int OBJECTS_VRCADE_DIGITWHEEL = 34;
	public const int OBJECTS_VRCADE_FLIPPERQND_LAYOUT = 29;
	public const int OBJECTS_VRCADE_LABELS_BALLS = 28;
	public const int OBJECTS_VRCADE_LABELS_SCORE = 27;
	public const int OBJECTS_VRCADE_LABELS_START = 26;
	public const int OBJECTS_VRCADE_PINBALL_SILVER = 25;
	public const int OBJECTS_VRCADE_TARGET_WHITE = 32;
	public const int OBJECTS_VRCADE_TRIGGER_SILVER = 33;
	public const int PLASTIC_BLACK = 46;
	public const int PLASTIC_BLUE = 63;
	public const int PLASTIC_PURPLE = 9;
	public const int PLASTIC_WHITE = 10;
	public const int PLASTIC_GRAY = 11;
	public const int PLASTIC_RED = 21;
	public const int SPACE_MOON_FLOOR = 1;
	public const int SPACE_MOON_FLOOR_INNER = 54;
	public const int SPACE_MOON_SOUTH = 2;
	public const int SPACE_MOON_WEST = 3;
	public const int SPACE_MOON_NORTH = 4;
	public const int SPACE_MOON_EAST = 5;
	public const int SPACE_EARTH = 6;
	public const int SPACE_SUN = 7;
	public const int SPACE_STAR = 8;
	// do not add anything after the amount ;)
	public const int MATERIAL_AMOUNT = 64;

	private static Material standard;
	private static Material standardFade;
	private static Material unlitColor;
	private static Material unlitTexture;
	private static Material unlitTransparent;


	public static void init(MainCtrl mainCtrl) {

		materials = new Material[MATERIAL_AMOUNT];

		textures = new string[MATERIAL_AMOUNT];

		textures[BUILDING_FLOOR_CONCRETE] = "Building/Floor/concrete";
		textures[BUILDING_FLOOR_WOOD] = "Building/Floor/woodenLengthwiseFloor";
		textures[INTERACTION_TELEPORT_TARGET] = "Interaction/teleportTarget";
		textures[OBJECTS_FIREFIGHTING_LABEL_QUESTION] = "Objects/fireFighting/Labels/question";
		textures[OBJECTS_FIREFIGHTING_LABEL_CORRECT_READY] = "Objects/fireFighting/Labels/correct_ready";
		textures[OBJECTS_FIREFIGHTING_LABEL_CORRECT_NOT_READY] = "Objects/fireFighting/Labels/correct_not_ready";
		textures[OBJECTS_FIREFIGHTING_LABEL_WRONG_READY] = "Objects/fireFighting/Labels/wrong_ready";
		textures[OBJECTS_FIREFIGHTING_LABEL_WRONG_NOT_READY] = "Objects/fireFighting/Labels/wrong_not_ready";
		textures[OBJECTS_LOGOS_ASOFTERSPACE] = "Objects/Logos/asofterspace";
		textures[OBJECTS_LOGOS_ASOFTERSPACE_DARK] = "Objects/Logos/asofterspace_dark";
		textures[OBJECTS_MATERIALS_PARTICLEBOARD] = "Objects/Materials/particleboard_1_continuous_small";
		textures[OBJECTS_NOSTALGICCONSOLE_GREEN] = "Building/Wall/wallpaper_1_azure_continuous";
		textures[OBJECTS_NOSTALGICCONSOLE_SCREEN] = "Objects/Screens/legacy_screen_on_wallpaper_1_azure_continuous";
		textures[OBJECTS_POSTERS_FLIPPERQND] = "Objects/Posters/flipperQnD";
		textures[OBJECTS_POSTERS_SOFTWARE] = "Objects/Posters/flyer_software_en";
		textures[OBJECTS_POSTERS_MARS] = "Objects/Posters/flyer_mars_en";
		textures[OBJECTS_POSTERS_VR] = "Objects/Posters/flyer_vr_en";
		textures[OBJECTS_POSTERS_PROCESS] = "Objects/Posters/flyer_process_en";
		textures[OBJECTS_ROCKETLAUNCH_LAUNCHPAD] = "Objects/rocketLaunch/launchpad";
		textures[OBJECTS_TICTACTOE_ROBOT] = "Objects/Materials/bee_tape";
		textures[OBJECTS_TICTACTOE_LABELS_RESTART] = "Objects/TicTacToe/Labels/restart_dark";
		textures[OBJECTS_VRCADE_DIGITWHEEL] = "Objects/vrCade/digits";
		textures[OBJECTS_VRCADE_FLIPPERQND_LAYOUT] = "Objects/vrCade/FlipperQnD/layout";
		textures[OBJECTS_VRCADE_LABELS_BALLS] = "Objects/vrCade/Labels/balls";
		textures[OBJECTS_VRCADE_LABELS_SCORE] = "Objects/vrCade/Labels/score";
		textures[OBJECTS_VRCADE_LABELS_START] = "Objects/vrCade/Labels/start";
		textures[SPACE_MOON_FLOOR] = "Space/moonFloor";
		textures[SPACE_MOON_FLOOR_INNER] = "Space/moonFloorInner";
		textures[SPACE_MOON_SOUTH] = "Space/moonNorth";
		textures[SPACE_MOON_WEST] = "Space/moonNorthMirrored";
		textures[SPACE_MOON_NORTH] = "Space/moonNorth";
		textures[SPACE_MOON_EAST] = "Space/moonNorthMirrored";
		textures[SPACE_EARTH] = "Space/earth";
		textures[SPACE_SUN] = "Space/sun";

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
				case SPACE_MOON_FLOOR_INNER:
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
				case OBJECTS_FIREFIGHTING_LABEL_QUESTION:
				case OBJECTS_FIREFIGHTING_LABEL_CORRECT_READY:
				case OBJECTS_FIREFIGHTING_LABEL_CORRECT_NOT_READY:
				case OBJECTS_FIREFIGHTING_LABEL_WRONG_READY:
				case OBJECTS_FIREFIGHTING_LABEL_WRONG_NOT_READY:
				case OBJECTS_LOGOS_ASOFTERSPACE:
				case OBJECTS_LOGOS_ASOFTERSPACE_DARK:
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
				case BUILDING_BEAM_WHITE:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1);
					break;
				case BUILDING_WALL:
					result.color = new Color(0.6771f, 0.5327f, 0.83015f, 1);
					break;
				case PLASTIC_BLACK:
					result.color = new Color(0.0f, 0.0f, 0.0f, 1);
					break;
				case PLASTIC_WHITE:
					result.color = new Color(0.99f, 0.97f, 0.99f, 1);
					break;
				case PLASTIC_BLUE:
					result.color = new Color(0, 0, 1, 1);
					break;
				case PLASTIC_PURPLE:
					result.color = new Color(0.3542f, 0.0654f, 0.6603f, 1);
					break;
				case PLASTIC_GRAY:
					result.color = new Color(0.5f, 0.5f, 0.5f, 1);
					break;
				case PLASTIC_RED:
					result.color = new Color(1.0f, 0.0f, 0.0f, 1);
					break;
				case SPACE_STAR:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1);
					break;
				case INTERACTION_TELEPORT_RAY:
					result.color = new Color(0.8f, 0.1f, 0.9f, 1);
					break;
				case INTERACTION_BUTTON_HOVER:
					result.color = new Color(1.0f, 0.9f, 1.0f, 1);
					break;
				case FADEABLE_BLACK:
					result.color = new Color(0.0f, 0.0f, 0.0f, 1);
					break;
				case OBJECTS_BOWLING_BALL_RED:
					result.color = new Color(1.0f, 0.0f, 0.0f, 1);
					break;
				case OBJECTS_BOWLING_PIN_WHITE:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1);
					break;
				case OBJECTS_BOWLING_PIN_RED:
					result.color = new Color(1.0f, 0.0f, 0.0f, 1);
					break;
				case OBJECTS_BLOBFLYER_BLACK:
					result.color = new Color(0.0f, 0.0f, 0.0f, 1);
					break;
				case OBJECTS_MATERIALS_METAL_DARK:
					result.color = new Color(0.2f, 0.2f, 0.2f, 1);
					result.SetFloat("_Metallic", 0.3f);
					result.SetFloat("_Glossiness", 0.3f);
					break;
				case OBJECTS_MATERIALS_METAL_SHINY:
					result.color = new Color(0.8f, 0.8f, 0.8f, 1);
					result.SetFloat("_Metallic", 0.3f);
					result.SetFloat("_Glossiness", 0.3f);
					break;
				case OBJECTS_FIREFIGHTING_BTN_GREEN:
					result.color = new Color(0, 0.9f, 0, 1);
					break;
				case OBJECTS_FIREFIGHTING_BTN_RED:
					result.color = new Color(0.9f, 0, 0, 1);
					break;
				case OBJECTS_FIREFIGHTING_OXYGEN_GREEN:
					result.color = new Color(0, 0.8f, 0, 1);
					break;
				case OBJECTS_FIREFIGHTING_OXYGEN_YELLOW:
					result.color = new Color(0.8f, 0.9f, 0, 1);
					break;
				case OBJECTS_ROCKETLAUNCH_YELLOW:
					result.color = new Color(0.8f, 0.9f, 0, 1);
					break;
				case OBJECTS_TICTACTOE_BLUE:
					result.color = new Color(0, 0, 0.8f, 1);
					break;
				case OBJECTS_TICTACTOE_RED:
					result.color = new Color(0.8f, 0, 0, 1);
					break;
				case OBJECTS_TICTACTOE_GRAY:
					result.color = new Color(0.6f, 0.6f, 0.6f, 1);
					break;
				case OBJECTS_TICTACTOE_ROBOT_GRAY:
					result.color = new Color(0.4f, 0.4f, 0.4f, 1);
					break;
				case OBJECTS_VRCADE_TRIGGER_SILVER:
				case OBJECTS_VRCADE_PINBALL_SILVER:
					result.color = new Color(1, 1, 1, 1);
					// TODO :: set metallic to 1.0f
					// TODO :: set smoothness to 0.403f
					break;
				case OBJECTS_VRCADE_TARGET_WHITE:
					result.color = new Color(1, 1, 1, 1);
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

	/**
	 * Set the color of a material, immediately changing the appearance of all
	 * game objects that use this material
	 */
	public static void setColor(int materialNum, Color newColor) {
		Material material = getMaterial(materialNum);
		material.color = newColor;
	}

	/**
	 * Set the color of a game object - and only of that one - by creating a
	 * new material
	 */
	public static void setColor(GameObject obj, Color newColor) {
		Material material = new Material(standard);
		material.color = newColor;
		obj.GetComponent<Renderer>().material = material;
	}
}

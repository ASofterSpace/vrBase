using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class MaterialCtrl {

	private MainCtrl mainCtrl;

	private Material[] materials;
	private string[] textures;

	public const int BUILDING_FLOOR_CONCRETE = 0;
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
	// do not add anything after the amount ;)
	public const int MATERIAL_AMOUNT = 12;


	public MaterialCtrl(MainCtrl mainCtrl) {

		this.mainCtrl = mainCtrl;

		this.materials = new Material[MATERIAL_AMOUNT];

		this.textures = new string[MATERIAL_AMOUNT];

		textures[BUILDING_FLOOR_CONCRETE] = "Building/Floor/concrete";
		textures[SPACE_MOON_FLOOR] = "Space/moonFloor";
		textures[SPACE_MOON_SOUTH] = "Space/moonNorth";
		textures[SPACE_MOON_WEST] = "Space/moonNorthMirrored";
		textures[SPACE_MOON_NORTH] = "Space/moonNorth";
		textures[SPACE_MOON_EAST] = "Space/moonNorthMirrored";
		textures[SPACE_EARTH] = "Space/earth";
		textures[SPACE_SUN] = "Space/sun";
	}

	/**
	 * Get a material by its number
	 */
	public Material getMaterial(int materialNum) {

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
					result = new Material(Shader.Find("Unlit/Texture"));
					break;
				case SPACE_STAR:
					result = new Material(Shader.Find("Unlit/Color"));
					break;
				default:
					result = new Material(Shader.Find("Standard"));
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
				case SPACE_STAR:
					result.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
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
	public void setMaterial(GameObject obj, int materialNum) {
		obj.GetComponent<Renderer>().material = getMaterial(materialNum);
	}

	public void setColor(int materialNum, Color newColor) {
		Material material = getMaterial(materialNum);
		material.color = newColor;
	}
}

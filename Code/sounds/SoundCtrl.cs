/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class SoundCtrl {

	private static AudioClip[] sounds;
	private static string[] soundLocations;

	public const int DING_DING_METAL_1 = 1;
	public const int KLACK_1 = 2;
	public const int KLACK_11 = 3;
	public const int KLACK_WOOD_2 = 4;
	public const int KLACK_WOOD_3 = 5;
	public const int KLACK_WOOD_4 = 6;
	public const int KLACK_WOOD_5 = 7;
	public const int KLACK_WOOD_6 = 8;
	public const int KLACK_WOOD_7 = 9;
	public const int THUMP_METAL_1 = 10;
	public const int THUMP_METAL_2 = 11;
	public const int THUMP_METAL_3 = 12;
	public const int THUMP_METAL_4 = 13;
	public const int THUMP_METAL_5 = 14;
	public const int WHOOSH_5 = 15;
	// do not add anything after the amount ;)
	public const int SOUND_AMOUNT = 46;


	public static void init() {

		sounds = new AudioClip[SOUND_AMOUNT];
		soundLocations = new string[SOUND_AMOUNT];

		soundLocations[DING_DING_METAL_1] = "ding_ding_metal_1";
		soundLocations[KLACK_1] = "klack_1";
		soundLocations[KLACK_11] = "klack_11";
		soundLocations[KLACK_WOOD_2] = "klack_wood_2";
		soundLocations[KLACK_WOOD_3] = "klack_wood_3";
		soundLocations[KLACK_WOOD_4] = "klack_wood_4";
		soundLocations[KLACK_WOOD_5] = "klack_wood_5";
		soundLocations[KLACK_WOOD_6] = "klack_wood_6";
		soundLocations[KLACK_WOOD_7] = "klack_wood_7";
		soundLocations[THUMP_METAL_1] = "thump_metal_1";
		soundLocations[THUMP_METAL_2] = "thump_metal_2";
		soundLocations[THUMP_METAL_3] = "thump_metal_3";
		soundLocations[THUMP_METAL_4] = "thump_metal_4";
		soundLocations[THUMP_METAL_5] = "thump_metal_5";
		soundLocations[WHOOSH_5] = "whoosh_5";
	}

	/**
	 * Get a sound by its number
	 */
	public static AudioClip getSound(int soundNum) {

		AudioClip result = sounds[soundNum];

		if (result == null) {
			result = Resources.Load<AudioClip>("Sounds/" + soundLocations[soundNum]);
			sounds[soundNum] = result;
		}

		return result;
	}
}

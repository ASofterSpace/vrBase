/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class SoundCtrl {

	private static AudioSource mainCameraSoundSource;

	private static AudioClip[] sounds;
	private static string[] soundLocations;

	public const int DING_DING_METAL_1 = 1;
	public const int KLACK_1 = 2;
	public const int KLACK_9 = 17;
	public const int KLACK_11 = 3;
	public const int KLACK_KLACK_METAL_2 = 18;
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
	public const int WHOOSH_1 = 15;
	public const int WHOOSH_5 = 16;
	// do not add anything after the amount ;)
	public const int SOUND_AMOUNT = 19;


	public static void init(MainCtrl mainCtrl) {

		GameObject mainCamera = mainCtrl.getMainCamera();
		mainCameraSoundSource = mainCamera.AddComponent<AudioSource>();

		sounds = new AudioClip[SOUND_AMOUNT];
		soundLocations = new string[SOUND_AMOUNT];

		soundLocations[DING_DING_METAL_1] = "ding_ding_metal_1";
		soundLocations[KLACK_1] = "klack_1";
		soundLocations[KLACK_9] = "klack_9";
		soundLocations[KLACK_11] = "klack_11";
		soundLocations[KLACK_KLACK_METAL_2] = "klack_klack_metal_2";
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
		soundLocations[WHOOSH_1] = "whoosh_1";
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

	/**
	 * Play a sound at a certain game object...
	 * It might be a bit faster to cache the AudioSource, but this is quicker
	 * to use and we can do so until we actually run into speed problems, THEN
	 * optimize - not do so prematurely ;)
	 */
	public static void playSound(GameObject origin, int soundNum) {
		AudioSource source = origin.GetComponent<AudioSource>();
		if (source == null) {
			source = origin.AddComponent<AudioSource>();
		}
		source.PlayOneShot(getSound(soundNum), 1.0f);
	}

	/**
	 * I am hearing voices in my head!
	 * ... just like when I am being teleported ;)
	 */
	public static void playMainCamSound(int soundNum) {
		mainCameraSoundSource.PlayOneShot(getSound(soundNum), 1.0f);
	}
}

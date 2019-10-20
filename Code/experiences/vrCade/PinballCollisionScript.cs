/**
 * Unlicensed code created by A Softer Space, 2018
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;

using UnityEngine;


public class PinballCollisionScript : MonoBehaviour, UpdateableCtrl {

	private FlipperQnDCtrl flipperCtrl;

	private GameObject pinball;

	public AudioClip thump_metal_1;
	public AudioClip thump_metal_2;
	public AudioClip thump_metal_3;
	public AudioClip thump_metal_4;
	public AudioClip thump_metal_5;

	public AudioClip klack_wood_3;
	public AudioClip klack_wood_4;
	public AudioClip klack_wood_5;
	public AudioClip klack_wood_6;
	public AudioClip klack_wood_7;

	public AudioClip soundFlipper;

	private Rigidbody pinballRB;

	private bool speedUpNextFrame = false;

	private AudioSource pinballAudioSource;


	public void init(MainCtrl mainCtrl, GameObject pinball, FlipperQnDCtrl flipperCtrl) {

		mainCtrl.addUpdateableCtrl(this);

		this.flipperCtrl = flipperCtrl;

		this.pinball = pinball;

		this.pinballRB = pinball.GetComponent<Rigidbody>();

		// init audio
		this.pinballAudioSource = pinball.GetComponent<AudioSource>();

		this.soundFlipper = SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_2);
		this.klack_wood_3 = SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_3);
		this.klack_wood_4 = SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_4);
		this.klack_wood_5 = SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_5);
		this.klack_wood_6 = SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_6);
		this.klack_wood_7 = SoundCtrl.getSound(SoundCtrl.KLACK_WOOD_7);
		this.thump_metal_1 = SoundCtrl.getSound(SoundCtrl.THUMP_METAL_1);
		this.thump_metal_2 = SoundCtrl.getSound(SoundCtrl.THUMP_METAL_2);
		this.thump_metal_3 = SoundCtrl.getSound(SoundCtrl.THUMP_METAL_3);
		this.thump_metal_4 = SoundCtrl.getSound(SoundCtrl.THUMP_METAL_4);
		this.thump_metal_5 = SoundCtrl.getSound(SoundCtrl.THUMP_METAL_5);
	}

	void UpdateableCtrl.update(VrInput input) {

		if (speedUpNextFrame) {
			speedUpNextFrame = false;

			// pinballRB.AddForce(Vector3.Normalize(pinballRB.velocity));

			Vector3 normVelo = 1.5f * Vector3.Normalize(pinballRB.velocity);

			pinballRB.velocity = new Vector3(
				pinballRB.velocity.x + normVelo.x,
				pinballRB.velocity.y + normVelo.y,
				pinballRB.velocity.z + normVelo.z
			);
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.name == "targetShroom") {

			flipperCtrl.addToScore(5);

			speedUpNextFrame = true;

			float soundSelect = Random.Range(0.0f, 5.0f);

			if (soundSelect < 1) {
				pinballAudioSource.PlayOneShot(thump_metal_1, 1.0f);
			} else if (soundSelect < 2) {
				pinballAudioSource.PlayOneShot(thump_metal_2, 1.0f);
			} else if (soundSelect < 3) {
				pinballAudioSource.PlayOneShot(thump_metal_3, 1.0f);
			} else if (soundSelect < 4) {
				pinballAudioSource.PlayOneShot(thump_metal_4, 1.0f);
			} else {
				pinballAudioSource.PlayOneShot(thump_metal_5, 1.0f);
			}

		} else if (col.gameObject.name == "targetRotator") {

			flipperCtrl.addToScore((int) Random.Range(20.0f, 50.0f));

			pinballAudioSource.PlayOneShot(SoundCtrl.getSound(SoundCtrl.DING_DING_METAL_1), 1.0f);

		} else if (col.gameObject.name == "flipper") {

			// on collision with the flipper... uhm... play nothing!
			// pinballAudioSource.PlayOneShot(soundFlipper, 1.0f);

		} else {

			float volume = col.relativeVelocity.magnitude / 5;

			if (volume > 1.0f) {
				volume = 1.0f;
			}

			float soundSelect = Random.Range(0.0f, 5.0f);

			if (soundSelect < 1) {
				pinballAudioSource.PlayOneShot(klack_wood_3, volume);
			} else if (soundSelect < 2) {
				pinballAudioSource.PlayOneShot(klack_wood_4, volume);
			} else if (soundSelect < 3) {
				pinballAudioSource.PlayOneShot(klack_wood_5, volume);
			} else if (soundSelect < 4) {
				pinballAudioSource.PlayOneShot(klack_wood_6, volume);
			} else {
				pinballAudioSource.PlayOneShot(klack_wood_7, volume);
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public AudioMixer audioMixer;
	public AudioMixerSnapshot unPaused;
	public AudioMixerSnapshot paused;
	
	public AudioSource mainMusic = null;

	public AudioSource alert = null;
	public AudioSource waterDrop = null;
	public AudioSource waterDrop2 = null;
	public AudioSource waterDrop3 = null;


	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Instanciate(AudioSource audioSource)
	{
		audioSource.PlayOneShot(audioSource.clip);
	}
	
}

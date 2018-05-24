using UnityEngine;
using System.Collections;

public class churchAudio : MonoBehaviour {

	//reference to our audio
	AudioSource myAudio;
	//public GameObject myAudio;

	//variable for our volume
	public float volume;

	// Use this for initialization
	void Start () {
	
		//get the audio source from our game component
		myAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//volume = myAudio.GetComponent<moveLocaction>();
		//myAudio.Pause();
		myAudio.volume = 1;

	
	}
}

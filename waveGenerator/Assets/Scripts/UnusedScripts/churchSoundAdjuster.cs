﻿using UnityEngine;
using System.Collections;

public class churchSoundAdjuster : MonoBehaviour {

	//reference to our light setup
	public Light myLight;

	//Range variables
	public bool changeRange = false;
	public float rangeSpeed = 1.0f; 
	public float maxRange = 10.0f;

	//Intensity values
	public bool changeIntensity = false;
	public float intensitySpeed = 1.0f;
	public float maxIntensity = 10.0f;

	//Colour variables
	public bool changeColour = false;
	public float colourSpeed = 1.0f;
	public Color startColour;
	public Color endColour;

	float startTime;
	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light> ();
		startTime = Time.time;

	}

	// Update is called once per frame
	void Update () {

		if(changeRange)
		{
			//Lets use this to control the sound that is coming from the church too!
			myLight.range = Mathf.PingPong (Time.time * rangeSpeed, maxRange);

		}

		if(changeIntensity)
		{

			myLight.intensity = Mathf.PingPong (Time.time * intensitySpeed, maxIntensity);

		}

		if(changeColour)
		{

			float t = (Mathf.Sin (Time.time - startTime * colourSpeed));
			myLight.color = Color.Lerp (startColour, endColour, t);

		}
	}
}

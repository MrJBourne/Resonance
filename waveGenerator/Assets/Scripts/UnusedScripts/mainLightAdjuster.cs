using UnityEngine;
using System.Collections;

public class mainLightAdjuster : MonoBehaviour {

	//reference to our light setup
	private Light myLight;

	//So we can grab it's variables too, as these need to be in sync
	private GameObject Waves;

	//Our ray
	RaycastHit newHit;

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

	//use these variables for our duration
	float startTime;
	float endTime;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light> ();


	}

	// Update is called once per frame
	void Update () {

		//Waves = gameObject.GetComponent<distance[0]>();
		//maxIntensity = Waves;

		//this will reset the variables for each new wave
		if (Input.GetMouseButtonDown (0)) {

			startTime = Time.time;
			changeRange = false;
			changeIntensity = false;
			myLight.range = 20;
			//myLight.intensity = 20;

		}

		if (Input.GetMouseButtonUp(0)) {
			
			changeRange = true;
			changeIntensity = true;
			endTime = Time.time;
			myLight.intensity = (endTime - startTime)*3;


		}

		//changeIntensity();

		//Waves = gameObject.GetComponent<distance[0]> ();

		if(changeRange)
		{
			//Lets use this to control the sound that is coming from the church too!
			//myLight.range = Mathf.PingPong (Time.time * rangeSpeed, maxRange);
			//myLight.range += 0.5f;
		}

		if (changeIntensity) {
			
			//myLight.intensity = Mathf.PingPong (Time.time * intensitySpeed, maxIntensity);
			//myLight.intensity -= 0.2f;
			//myLight.intensity = (endTime - startTime)*10;

		} 

		//else {

			myLight.intensity -= 0.05f;

		//}

		if(changeColour)
		{

			float t = (Mathf.Sin (Time.time - startTime * colourSpeed));
			myLight.color = Color.Lerp (startColour, endColour, t);

		}
	}
}

//		void changeIntensity(){
//
//
//		}
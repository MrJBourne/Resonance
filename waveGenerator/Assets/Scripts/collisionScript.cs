//IF VERTICES OF PLANE < 0 THEN = 0
using UnityEngine;
using System.Collections; 

public class collisionScript : MonoBehaviour {

	//lets...
	public bool clickAllowwed;

	//We can have a single value but this wont allow multiple waves, so lets create arrays instead and loop through them each time the mouse is pressed 
	public bool touchPressed;
	public Vector2[] impactPos;
	public float[] waveAmplitude;
	public float[] myTime;
	public float[] newMag;
	public float[] distance;

	public float[] newScale;
	public float[] newFreq;
	public float[] newSpeed;

	private int waveNumber = 1;
	public float distanceX, distanceZ;

	public float magnitudeDivider;
	public float speedWaveSpread;
	public float newWave;
	public float freq;

	public float[] newTime;
	public bool[] startCount;

	//NOT MESSING TH?S Up

	//we'll create some variables that control the velocity and maginitude of our wave
	//private float velocity, magnitude = 10;

	//grab the attatched game object so we can get it's script's variables
	public GameObject lightLocObj;

	//setting the name of the script attatched to light so we can refer to it this way
	//private moveLocaction lightLoc;
	//private scriptOne tet;
	private float offsetX, offsetY;

	//lets make some values to store the duration of a mouse press
	public float currentTime;
	public 	float pressed;
	public 	float released;
	public 	float duration;
	//public float[] newDuration;
	public float maxDistance;

	public float newNewFreq;


	RaycastHit newHit;

	AudioSource myAudio;
	//																							public NavMeshAgent waterMesh;

	Mesh mesh;

	// Use this for initialization
	void Start () {

		//not sure where to place this
		//Application.targetFrameRate = 30;

		myAudio = GetComponent<AudioSource> ();

		mesh = GetComponent<MeshFilter>().mesh;
		waveAmplitude = new float[5];
		distance = new float[5];
		myTime = new float[5];
		impactPos = new Vector2[5];
		newMag = new float[5];

		newScale = new float[5];
		newFreq = new float[5];
		newSpeed = new float[5];

		//This variable will store the time the mouse is pressed down for each wave
		newTime = new float[5];
		startCount = new bool[5];

		//Get component attatched to script
		//lightLoc = lightLocObj.GetComponent<moveLocaction>();

		for (int i = 0; i < waveAmplitude.Length; i++) {
			newMag [i] = 10;
		}
	}

	// Update is called once per frame
	void Update () {

		//store the current time
		currentTime = Time.fixedTime;
		myAudio.volume = duration;

		//This will get the coordinates of the mouse position and convert it into world space
		Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);

		//loops through waves
		for (int i=0; i<waveAmplitude.Length; i++){

			//Debug.Log (duration);


			//distance will be controlled by the duration the mouse is being held down
			//Lets cap the distance at 20
			if (distance [i] < 20) {

				//gradually increase the distance to create a more organic looking ripple/echo
				distance [i] += speedWaveSpread;

			} else if (distance [i] > 20) {
				distance [i] = 20;
			}

			//Debug.Log(distance[i]);

			//}
			//grab the wave values so we can manipulate them and put them back
			waveAmplitude[i] = GetComponent<Renderer>().material.GetFloat("_Wave" + (i+1));
			//myTime[i] = GetComponent<Renderer>().material.GetFloat("_Time" + (i+1));

			//Now set the 
			if (waveAmplitude[i] > -1)

			{
				//time will slowlt manipulate vertices
				myTime [i] += 0.1f;

				GetComponent<Renderer>().material.SetFloat("_Distance" + (i+1), distance[i]);
				//GetComponent<Renderer>().material.SetFloat("_Distance" + (i+1), 5*speedWaveSpread);
				GetComponent<Renderer>().material.SetFloat("_Wave" + (i+1), waveAmplitude[i] * 0.95f);
				GetComponent<Renderer>().material.SetFloat("_Time" + (i+1), myTime[i]);

			}
			if (waveAmplitude[i] < 0.001)
			{
				//GetComponent<Renderer>().material.SetFloat("_Wave" + (i+1), 0);
				distance[i] = 0;
				myTime [i] = 0;
				//newDuration [i] = 0;
			}
		}
			
		if (Physics.Raycast (ray, out newHit, 100)) {

			if (Input.GetMouseButtonDown(0)) {

				//The time the mouse has been pressed
				pressed = currentTime;
				//Debug.Log (pressed);
			
			}
				
			newSpeed[waveNumber - 1] = speedWaveSpread;
		
			//MAKE FUNCTION
			//play the sound when the mouse has been released
			if(Input.GetMouseButtonUp(0)){

				//if the sonud is not already playing, play sonud again
				if (!myAudio.isPlaying) {
					myAudio.Play ();
					//alter the sound based on the length of our click
					myAudio.volume = duration*0.01f;
				}

			//if(Input.GetTouch(0)){
			//if(touchPressed = false){
			//if(Input.GetTouch(0).phase){

				//The time the mouse has been released
				released = currentTime;
				//Debug.Log (released);

				//lets subtract the duration of press from release to get the duration
				duration = (released - pressed);

				speedWaveSpread = duration*0.2f;

				//we want the max to be 3 seconds
				//if (duration > 3) {
				//	duration = 3;
				//}

				//we dont have a map function, so we'll clamp it
				//Mathf.Clamp(duration, 0, 10);
				//newNewFreq = Mathf.SmoothDamp(0.1f, 5f, ref duration, 0.3f);
				//Debug.Log (newNewFreq);


				distanceX = newHit.point.x;
				distanceZ = newHit.point.z;

			//loop through our array of waves
			//reset the values here
			waveNumber++;
			if (waveNumber > 5) {
				waveNumber = 1;

			}

			//set the origin of the ripple to the mouse position
			GetComponent<Renderer> ().material.SetFloat ("_xImpact" + waveNumber, newHit.point.x);
			GetComponent<Renderer> ().material.SetFloat ("_zImpact" + waveNumber, newHit.point.z);
			
			//set the new Radius of our waves
			GetComponent<Renderer> ().material.SetFloat ("_Scale" + waveNumber, duration*2);//*1.5f);//min = 0.5

			//GetComponent<Renderer> ().material.SetFloat ("_Scale" + waveNumber, newScale[waveNumber-1]);//min = 0.5
			//GetComponent<Renderer> ().material.SetFloat ("_Speed" + waveNumber, + duration*3);

			//dont need new freqs, just add here
			



				GetComponent<Renderer> ().material.SetFloat ("_Freq" + waveNumber,duration*0.5f);//*0.8f);//newFreq[waveNumber-1]);//+ duration*30);

			//set the origin of ripple to the mouse location
			GetComponent<Renderer> ().material.SetFloat ("_OffsetX" + waveNumber, distanceX / mesh.bounds.size.x);
			GetComponent<Renderer> ().material.SetFloat ("_OffsetZ" + waveNumber, distanceZ / mesh.bounds.size.z);

			
			//this is setting the wave variable in the materials variables to the velocity and magnitude the collider hits the rigidbody. We then multiply our number by a value from 0-1 to decrease the wave over time.

			GetComponent<Renderer> ().material.SetFloat ("_Wave" + waveNumber, 5 * magnitudeDivider);

			}
		}
	}

//	void OnCollisionEnter(Collision col){
//
//		Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//		RaycastHit newHit;
//
//		//if the ray has hit our navigation mesh
//		if (Physics.Raycast (ray, out newHit, 100)) 
//		{
//			//waterMesh.destination = newHit.point;
//			//impactPos[0].x = newHit.point.x;
//			//impactPos[0].y = newHit.point.z;
//
//			//}
//
//			//if (col.rigidbody) {
//			//loop through our array of waves
//			waveNumber++;
//			if (waveNumber > 2) {
//				waveNumber = 1;
//
//				//--
//			}
//			//waveAmplitude[waveNumber-1] = 0;
//			distance [waveNumber - 1] = 0;
//			myTime [waveNumber - 1] = 0;
//
//			//distanceX = this.transform.position.x - col.gameObject.transform.position.x;
//			//distanceZ = this.transform.position.z - col.gameObject.transform.position.z;
//
//			//we need to change these variables
//			//			impactPos[waveNumber - 1].x = col.transform.position.x;
//			//			impactPos[waveNumber - 1].y = col.transform.position.z;
//
//			//set the origin of the ripple to the mouse position
//			GetComponent<Renderer> ().material.SetFloat ("_xImpact", newHit.point.x);
//			GetComponent<Renderer> ().material.SetFloat ("_zImpact", newHit.point.z);
//
//			GetComponent<Renderer> ().material.SetFloat ("_OffsetX" + waveNumber, distanceX / mesh.bounds.size.x * 2.5f);
//			GetComponent<Renderer> ().material.SetFloat ("_OffsetZ" + waveNumber, distanceZ / mesh.bounds.size.z * 2.5f);
//
//			//we just need the size of the velocity
//			//this is setting the wave variable in the materials variables to the velocity and magnitude the collider hits the rigidbody. We then multiply our number by a value from 0-1 to decrease the wave over time.
//			GetComponent<Renderer> ().material.SetFloat ("_Wave" + waveNumber, col.rigidbody.velocity.magnitude * magnitudeDivider);
//			//GetComponent<Renderer> ().material.SetFloat ("_Wave" + waveNumber, magnitude * magnitudeDivider);
//			//We are making the wave by multiplying a grid of verticles. The way these numbers are stored in an array means when there is a 
//			//}
//		}
//
//
//
//
//	}

//	void OnMouseDown(){
//
//		//This will get the coordinates of the mouse position and convert it into world space
//		Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//		RaycastHit newHit;
//
//		//if the ray has hit our navigation mesh
//		if (Physics.Raycast (ray, out newHit, 100)) 
//		{
//
//		//lets increase the velocity the longer the mouse is held down
//		float tempV = 3f;
//		velocity += tempV;
//
//		//we want the velocity to be in the range ___, so we limit how large it can be
//		if (velocity > 4) {
//			
//			//velocity = 4;
//		}
//
//		//lets do the similar for the magnitude
//		float tempM = 3f;
//		magnitude += tempM;
//
//		//we want the velocity to be in the range ___, so we limit how large it can be
//		if (magnitude > 3) {
//
//			//velocity = 3;
//		}
//
//			Debug.Log ("mouseCLicked");
//		}
//	}

	//void OnMouse

	//doesnt work because it's reset every frame?
//	void OnMouseUp(){
//
//		//This will get the coordinates of the mouse position and convert it into world space
//		Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//		RaycastHit newHit;
//
//		//if the ray has hit our navigation mesh
//		if (Physics.Raycast (ray, out newHit, 100)) 
//		{
//			
//			//loop through our array of waves
//			waveNumber++;
//			if (waveNumber > 2) {
//				waveNumber = 1;
//			}
//			distance [waveNumber - 1] = 0;
//			myTime [waveNumber - 1] = 0;
//
//			//set the origin of the ripple to the mouse position
//			GetComponent<Renderer> ().material.SetFloat ("_xImpact", newHit.point.x);
//			GetComponent<Renderer> ().material.SetFloat ("_zImpact", newHit.point.z);
//
//			GetComponent<Renderer> ().material.SetFloat ("_OffsetX" + waveNumber, distanceX / mesh.bounds.size.x * 2.5f);
//			GetComponent<Renderer> ().material.SetFloat ("_OffsetZ" + waveNumber, distanceZ / mesh.bounds.size.z * 2.5f);
//
//			//this is setting the wave variable in the materials variables to the velocity and magnitude the collider hits the rigidbody. We then multiply our number by a value from 0-1 to decrease the wave over time.
////			GetComponent<Renderer> ().material.SetFloat ("_Wave" + waveNumber, col.rigidbody.velocity.magnitude * magnitudeDivider);
//			GetComponent<Renderer> ().material.SetFloat ("_Wave" + waveNumber, magnitude * magnitudeDivider);
//			//We are making the wave by multiplying a grid of verticles. The way these numbers are stored in an array means when there is a 
//			//}
//		}
//
//	    
//		//reset the variables for the next press
//		//magnitude = 0;
//		//velocity = 0;
//		//Debug.Log(magnitude);
//
//	}


}

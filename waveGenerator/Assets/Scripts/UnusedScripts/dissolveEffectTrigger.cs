using UnityEngine;
using System.Collections;

public class dissolveEffectTrigger : MonoBehaviour {

	public Material disolveMaterial;
	public float speed, max;
	public float currentY, startTime;


	void Start(){


	}

	private void Update()
	{
		startTime += Time.fixedTime;
		currentY = startTime/300;
		//Debug.Log (startTime);

		GetComponent<Renderer> ().material.SetFloat ("_DisolveY", currentY);
	//	disolveMaterial.SetFloat ("_DisolveY", currentY);

	}
}

//
//		//if this is during the animation 
//		if (currentY < max) 
//		{
//			disolveMaterial.SetFloat ("_DisolveY", currentY);
//			currentY += Time.deltaTime * speed;
//		}
//	
//		if (Input.GetKeyDown(KeyCode.E)) 
//
//			TriggerEffect();
//	}
//
//	//this will reset the disolve effect everytime the key "E" is presed
//	private void TriggerEffect()
//	{
//		startTime = Time.time;
//		currentY = 0;
//
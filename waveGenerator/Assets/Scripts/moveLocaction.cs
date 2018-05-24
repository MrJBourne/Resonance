using UnityEngine;
using System.Collections;

public class moveLocaction : MonoBehaviour {

	private NavMeshAgent meshAgent;
	//public float offsetX1, offsetZ1;
	Ray ray;

	// Use this for initialization
	void Start () {

		meshAgent = GetComponent<NavMeshAgent>();
	
	}

	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		//get the position of mouse in world space
		ray  = Camera.main.ScreenPointToRay(Input.mousePosition);


		//set the destination of our object to this position
		if (Physics.Raycast (ray, out hit, 100)) 
		{
			meshAgent.nextPosition = hit.point;

		}

	}

}

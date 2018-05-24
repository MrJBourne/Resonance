using UnityEngine;
using System.Collections;

public class lightClickLoc : MonoBehaviour {

	private NavMeshAgent mNavMeshAgent;
	//private bool canMove = true;
	Ray ray;
	//public float offsetX1, offsetZ1;

	// Use this for initialization
	void Start () {

		mNavMeshAgent = GetComponent<NavMeshAgent>();

	}

	// Update is called once per frame
	void Update () {

		RaycastHit hit;

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//set the destination of our object to the location of of mouse press in real world coordinates
		if (Physics.Raycast (ray, out hit, 100)&& (Input.GetMouseButtonDown(0))) 
		{
			mNavMeshAgent.nextPosition = hit.point;

		}

	}

}

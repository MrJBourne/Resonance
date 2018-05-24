using UnityEngine;
using System.Collections;

public class hoverMove : MonoBehaviour {

	private NavMeshAgent navMeshA;
	// Use this for initialization
	void Start () {

		navMeshA = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 100)){
			navMeshA.destination = hit.point;

		}
	}
}

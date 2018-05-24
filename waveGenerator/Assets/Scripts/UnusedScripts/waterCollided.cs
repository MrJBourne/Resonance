using UnityEngine;
using System.Collections;

public class waterCollided : MonoBehaviour {

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "mainChurch") {

			Destroy(col.gameObject);
			print("Points colliding: ");
		}

	}

	void OnTriggerEnter(){


	}
}

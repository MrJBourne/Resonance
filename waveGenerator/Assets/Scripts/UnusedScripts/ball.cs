using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour 
{

	void OnCollisionEnter(Collision col)
	{
		
		if (col.gameObject.name == "mainChurch")
		{

			Destroy(col.gameObject);
			//ContactPoint contact = col.contacts[0];

			print("Points colliding: ");// + col.contacts.Length);
			//print("First point that collided: " + col.contacts[0].point);


		}

	}

}

using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {

	public GameObject displayObject;

	private void OnTriggerEnter(){

		displayObject.SetActive(true);

	}
}

using UnityEngine;
using System.Collections;

public class frameRate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//QualitySettings.vSyncCount = 0;
		//Application.targetFrameRate = 30;
		Screen.SetResolution(640, 480, true, 30);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

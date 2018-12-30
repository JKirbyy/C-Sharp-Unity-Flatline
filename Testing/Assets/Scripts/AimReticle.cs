using UnityEngine;
using System.Collections;

public class AimReticle : MonoBehaviour {


	Vector3 worldPosition;

	void Start () 
	{
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		worldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		worldPosition.z = -5;

		transform.position = worldPosition;
		 
	}
}

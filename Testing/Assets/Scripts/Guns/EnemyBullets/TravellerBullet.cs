using UnityEngine;
using System.Collections;

public class TravellerBullet : MonoBehaviour {


	void Start () 
	{
		Destroy (gameObject, 7);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += (transform.up * DifficultyManager.travellerABulletSpeed * Time.deltaTime);
	}

}

using UnityEngine;
using System.Collections;

public class TravellerASpawner : MonoBehaviour 
{
	public GameObject enemy;
	public float spawnRate = 1.5f;

	void Start () 
	{
		InvokeRepeating ("EnemySpawn", 1f, spawnRate);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void EnemySpawn ()
	{
		Vector3 spawnPosition = new Vector3 (transform.position.x, transform.position.y, 0);
		Instantiate (enemy, spawnPosition, transform.rotation); 
	}


}

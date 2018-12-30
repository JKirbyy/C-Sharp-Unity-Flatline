using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject enemy;
	public float spawnRate = 1.5f;
	public float spawnRange = 5;

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
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnRange, spawnRange), transform.position.y, 0);
		Instantiate (enemy, spawnPosition, transform.rotation); 
	}


}

using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public float enemyBulletSpeed = 10;

	void Start () 
	{
		Destroy (gameObject, 7);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += (transform.forward * enemyBulletSpeed * Time.deltaTime);
	}

}

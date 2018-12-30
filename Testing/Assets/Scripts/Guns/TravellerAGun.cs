using UnityEngine;
using System.Collections;

public class TravellerAGun : MonoBehaviour 
{
	Vector3 worldPosition;
	Vector3 aimDirection;
	public Transform[] bulletSpawns;
	public GameObject enemyBullet;
	bool CanFire;
	public AudioClip shootSound;
	public AudioSource audioSource;

	void Start () 
	{
		CanFire = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
			
		foreach (Transform bulletSpawn in bulletSpawns)
		{
			if (CanFire == true)
			{
				audioSource.PlayOneShot (shootSound);
				Instantiate (enemyBullet, bulletSpawn.position, bulletSpawn.rotation);
				CanFire = false;
				StartCoroutine (FireCount ());
			}
		}
 	}

		
	IEnumerator FireCount ()
	{
		yield return new WaitForSeconds(DifficultyManager.travellerFireRate);
		CanFire = true;
	}
}

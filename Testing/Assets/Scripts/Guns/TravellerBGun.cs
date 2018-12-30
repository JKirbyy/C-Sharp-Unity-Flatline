using UnityEngine;
using System.Collections;

public class TravellerBGun : MonoBehaviour 
{
	Vector3 worldPosition;
	Vector3 aimDirection;
	public Transform[] bulletSpawns;
	public GameObject enemyBullet;
	public float fireRate;
	bool CanFire;
    private Transform player;

    void Start () 
	{
        player = GameObject.FindWithTag("Player").transform;
        CanFire = true;

	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.LookAt(player.position);

        foreach (Transform bulletSpawn in bulletSpawns)
		{
			if (CanFire == true)
			{
				Instantiate (enemyBullet, bulletSpawn.position, bulletSpawn.rotation);
				CanFire = false;
				StartCoroutine (FireCount ());
			}
		}
 	}

		
	IEnumerator FireCount ()
	{
		yield return new WaitForSeconds(fireRate);
		CanFire = true;
	}
}

using UnityEngine;
using System.Collections;

public class MoverGun : MonoBehaviour 
{
    public Vector3 directionVector;
    public Transform[] bulletSpawns;
	public GameObject enemyBullet;
	bool CanFire;
    private Transform player;
	public AudioClip shootSound;
	public AudioSource audioSource;

	void Start () 
	{
		CanFire = true;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            //transform.LookAt(player.position);
            directionVector = (player.position - transform.parent.position).normalized;
            transform.parent.up = directionVector;

            foreach (Transform bulletSpawn in bulletSpawns)
            {
                if (CanFire == true)
                {
					audioSource.PlayOneShot (shootSound);
					Instantiate(enemyBullet, bulletSpawn.position, bulletSpawn.rotation);
                    CanFire = false;
                    StartCoroutine(FireCount());
                }
            }
        }
    }
	
	IEnumerator FireCount ()
	{
		yield return new WaitForSeconds(DifficultyManager.moverFireRate);
		CanFire = true;
	}
}

using UnityEngine;
using System.Collections;

public class FloaterGun : MonoBehaviour 
{
    private Vector3 directionVector;
    public Transform[] bulletSpawns;
	public GameObject enemyBullet;
	bool CanFire;
	FloaterAI floaterAIScript;
    FloaterAI mainFloaterAIScript;
    private Transform player;
	public AudioSource audioSource;
	public AudioClip shootSound;

	void Start () 
	{
        //fireRate = GameObject.Find("Manager").GetComponent<DifficultyManager>().floaterFireRate;
        CanFire = true;
		floaterAIScript = transform.parent.GetComponent<FloaterAI>();
        mainFloaterAIScript = transform.root.GetComponent<FloaterAI>();
        Debug.Log(floaterAIScript);
        Debug.Log(mainFloaterAIScript);
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

            if (floaterAIScript)
            {
                if (floaterAIScript.notMoving == true)
                {
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
            
            else if(mainFloaterAIScript)
            {
                if (mainFloaterAIScript.notMoving == true)
                {
                    foreach (Transform bulletSpawn in bulletSpawns)
                    {
                        if (CanFire == true)
                        {
                            Instantiate(enemyBullet, bulletSpawn.position, bulletSpawn.rotation);
                            CanFire = false;
                            StartCoroutine(FireCount());
                        }
                    }
                }
            }
        }
    }

		
	IEnumerator FireCount ()
	{
		yield return new WaitForSeconds(DifficultyManager.floaterFireRate);
		CanFire = true;
	}
}

using UnityEngine;
using System.Collections;

public class FloaterSpawner : MonoBehaviour 
{
    public GameObject packFloater;
    public GameObject floater;
    public GameObject follower;
    private Vector3[] packSpawns = new Vector3[4];
    private Vector3 spawnPosition;
    public FloaterAI floaterScript;
    private Transform floaterSpawnedTransform;
    public FloaterGun packGun;
    public GameObject pack;
    public GameObject traveller;
    public GameObject mover;
    public float distance;
    public Transform player;


    void Start () 
	{
		//sets array of spawn positions for pack.
       spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
       packSpawns[0] = new Vector3(transform.position.x + 1.5f, transform.position.y, 0);
       packSpawns[1] = new Vector3(transform.position.x - 1.5f, transform.position.y, 0);
       packSpawns[2] = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
       packSpawns[3] = new Vector3(transform.position.x, transform.position.y - 1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
		if(player)
		distance = Vector3.Distance(transform.position, player.position);
    }
   
    public void followerSpawn()
    {
        Instantiate(follower, spawnPosition, transform.rotation);
    }

    public void moverSpawn()
    {
        Instantiate(mover, spawnPosition, transform.rotation);
    }

    public void travellerSpawn()
    {
        Instantiate(traveller, spawnPosition, transform.rotation);
    }
    #region floater
    public void floaterSpawn(string type)
    {
        if (type == "normal")
        {
            var floaterSpawned = Instantiate(floater, spawnPosition, transform.rotation);
            floaterScript = floaterSpawned.GetComponent<FloaterAI>();
            floaterScript.wait = false;

        }

        else if (type == "pack")
        {
            //spawns empty game object to control movement.
			var floaterSpawned = Instantiate(pack, spawnPosition, transform.rotation); 
            floaterSpawnedTransform = floaterSpawned.transform;
            floaterScript = floaterSpawned.GetComponent<FloaterAI>();

            for (int i = 0; i < 4; i++)
            {
                var packs = Instantiate(packFloater, packSpawns[i], transform.rotation);
                packs.transform.SetParent(floaterSpawnedTransform);
               

                //packScript = pack.GetComponent<packFloaterAI>();
                //packScript.spawnPosition = packSpawns[i];
                //packScript.mainFloater = floaterSpawned;
            }


        }
        #endregion
    }
}
    




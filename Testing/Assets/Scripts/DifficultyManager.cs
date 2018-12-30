using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static float powerUpWaitTime = 12; //12
    public GameObject traveller;

    public static float floaterFireRate;
    public static float floaterMinimumMovementRate;
    public static float floaterMaximumMovementRate;
    public static float moverFireRate;
    public static float travellerFireRate;
    public static float circlerTimeToNode;
    public static float followerInitialSpeed;
    public static float followerEndSpeed;
    public static float floaterBulletSpeed;
    public static float moverBulletSpeed;
    public static float travellerABulletSpeed;
    public static float travellerASpeed;

    public Transform[] travellerSpawns;
    public GameObject[] enemySpawns;
    private GameObject[] floaterArray;
    private GameObject[] moverArray;
    public int enemyAmount;
    public int floaterAmount;
    public int moverAmount;
    public int circlerAmount;
    public int followerAmount;
    public float spawnRate;
    private bool canCallState1;
    private bool canCallState2;
    private bool canCallState3;
    private bool canCallfollowerState;
    private int floaterMoverMaximum;
    private int floaterMoverCap = 15;

    private float floaterWaveTime;
    public float floaterSpawnRate;
    public int floaterSpawnMinimum;
    public int floaterSpawnMaximum;
    public int floaterMaximum;
	public int travellerSpawnCount;

    private float followerWaveTime;
    private float followerSpawnRate;
    private float travellerWaveTime;
    private float travellerSpawnRate;
    private int followerSpawnMinimum;
    private int followerSpawnMaximum;
    private int moverSpawnMinimum;
    private int moverSpawnMaximum;
    private float moverWaveTime;
    private float moverSpawnRate;
    private float circlerWaveTime;
    private float circlerSpawnRate;
    private int floaterIncrementRate = 4; //4 
    private int moverIncrementRate = 5; //5
    private int circlerIncrementRate = 6;
    private int travellerIncrementRate = 5;
    private int floaterSpawnMaxMax = 8;
    private int floaterSpawnMinMax = 5;
    private int moverSpawnMaxMax = 7;
    private int moverSpawnMinMax = 5;
    private float circlerRateMax = 12;
    private int travellerRateMax = 13;
    private int floaterMoverAmount;
    private int tempFloaterMaximum;
    private int tempFloaterSpawnMinimum;
    private int tempMoverMaximum;
    private int tempMoverMinimum;
    private float tempTravellerRate;

     
    private int moversSpawned;
    private int floatersSpawned;
    private int followersSpawned;
    private int followerX;
    private int x;
    private int y;
    private int moverY;
    private int moverX;
    private int moverZ;
    private int count = 1;
    private int moverCount = 1;
    private int z;
    private int floaterStateOneCount = 0;
    private GameObject spawn;
    FloaterSpawner floaterSpawnerScript;
    private int randomPack;
    private int travellerSpawnCountStateOne = 0;
    private int indexContainer;
    public bool fifthCheck = false;

    private bool randomStateCall = false; //false
    private float randomStateCount = 0;
    private bool followerStateCall = false;
    private bool travellerStateCall = false; //false
    private int randomChance = 5;
    private int whichState = 1;
    private int randomCount = 0;
    private bool stateTwoOver = false; //false

    private int floaterWaveCount = 0;
    private int moverWaveCount = 0;
    private int circlerWaveCount = 0;
    //private int followerWaveCount = 0;
    private int travellerWaveCount = 0;
    
    
	
	void Start ()
    {
        canCallState1 = true;
        canCallState2 = true;
        canCallState3 = true;
        canCallfollowerState = true;
		stateTwoOver = false;
		travellerStateCall = false;
		followerStateCall = false;
		randomStateCount = 0;
		randomStateCall = false;
		fifthCheck = false;
		count = 1;
		moverCount = 1;
		powerUpWaitTime = 12;
    }

    void Update()
    {
        floaterWaveTime += Time.deltaTime;
        followerWaveTime += Time.deltaTime;
        travellerWaveTime += Time.deltaTime;
        circlerWaveTime += Time.deltaTime;
        moverWaveTime += Time.deltaTime;

        floaterArray = GameObject.FindGameObjectsWithTag("Floater");
        floaterAmount = floaterArray.Length;

        moverArray = GameObject.FindGameObjectsWithTag("Mover");
        moverAmount = moverArray.Length;

        floaterMoverAmount = moverAmount + floaterAmount;


        #region StateOne
        if (Time.timeSinceLevelLoad < 15) //the state lasts 30 seconds.
        {
			if (canCallState1 == true) //this bool is set to false in StateOne() to stop it from beign called every frame.
            {
                StateOne();
            }

            if (floaterWaveTime >= floaterSpawnRate) //checks if wave should be spawned.
            {
                floaterWaveTime = 0;
                floaterStateOneCount = floaterStateOneCount + 1; //used to count amount of waves since last incrementation.

                //if (floaterStateOneCount == 2) //counts every 2 times spawn is called.
                //{
                // floaterSpawnMinimum = floaterSpawnMinimum + 1;
                // floaterStateOneCount = 0;
                //}

                if (floaterAmount > floaterMaximum) //checks if too many enemys on screen.
                {
                    floaterSpawnMaximum = floaterSpawnMaximum - 3;
                }

                floatersSpawned = Random.Range(floaterSpawnMinimum, floaterSpawnMaximum);
                y = 666; //because with the first iteration, y must not equal x,
                count = 0;

                for (int i = 0; i <= floatersSpawned; i++)
                {
                    if (y == x) //checks if the previous iteration produced the same spawn location.
                    {
                        count = count + 1;
                    }

                    if (count == (floatersSpawned - 1)) //if the same spawn was called for every spawn, the last one must be different.
                    {
                        z = (enemySpawns.Length - 1) - x;
                        //This works out the distance between the last spawn location and the current one.
                        //It rounds the x value to the furthest one away out of location 0 and the last location.
                        if (z > x)
                        {
                            spawn = enemySpawns[enemySpawns.Length - 1];
                            floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                            floaterSpawnerScript.floaterSpawn("normal");
                        }

                        else
                        {
                            spawn = enemySpawns[0];
                            floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                            floaterSpawnerScript.floaterSpawn("normal");
                        }
                    }

                    if (count < floatersSpawned) //this should be the normal spawning function.
                    {
                        x = Random.Range(0, (enemySpawns.Length - 1));
                        spawn = enemySpawns[x];
                        floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();

                        randomPack = Random.Range(1, 6); //this gives the pack a 1 in 5 chance of spawning

                        if (randomPack < 5)
                        {
                            floaterSpawnerScript.floaterSpawn("normal");
                        }

                        else
                        {
                            floaterSpawnerScript.floaterSpawn("pack");
                        }

                        y = x;
                    }

                }


            }

            if (followerWaveTime >= followerSpawnRate)
            {
                followerWaveTime = 0;
                followersSpawned = Random.Range(followerSpawnMinimum, followerSpawnMaximum);

                for (int i = 0; i < followersSpawned; i++)
                { 
                    float largestDistance = 0;
                    int indexCount = 0;
                    int index = 0;

                    foreach (GameObject spawns in enemySpawns)
                        {
                            floaterSpawnerScript = spawns.GetComponent<FloaterSpawner>();
							Debug.Log(floaterSpawnerScript.distance);
							Debug.Log(largestDistance);

                            if (floaterSpawnerScript.distance > largestDistance)
                            {
								largestDistance = floaterSpawnerScript.distance;
                                index = indexCount;
								//Debug.Log(indexCount);
								//Debug.Log(index);
                            }
                            
							indexCount += 1;
							Debug.Log(indexCount);
                        }
                    
					Debug.Log(index);
					spawn = enemySpawns[index];
                    floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                    floaterSpawnerScript.followerSpawn();
                }

            }


        }
        #endregion

        #region State Two
        if (Time.timeSinceLevelLoad > 15 && floaterAmount == 0) //can not be called until no enemies left on screen.
        {

            if (canCallState2 == true)
            {
                StateTwo();
            }

            if (travellerWaveTime >= travellerSpawnRate)
            {
                if (travellerSpawnCountStateOne == 0)
                {
                    Debug.Log("called horiz");
                    travellerWaveTime = 0;
                    HorizontalHell();
                    travellerSpawnCountStateOne += 1;
                }

                else if (travellerSpawnCountStateOne == 1)
                {
                    Debug.Log("called vert");
                    travellerWaveTime = 0;
                    VerticalHell();
                    travellerSpawnCountStateOne += 1;
                }

                else if (travellerSpawnCountStateOne == 2)
                {
                    Debug.Log("called one side");
                    travellerWaveTime = 0;
                    StartCoroutine(OneSidedHell());
                    travellerSpawnCountStateOne += 1;
                }

                else if (travellerSpawnCountStateOne == 3)
                {
                    Debug.Log("called Laser");
                    travellerWaveTime = 0;
                    LaserGrid();
                    travellerSpawnCountStateOne += 1;
                }

                else if (travellerSpawnCountStateOne == 4)
                {
                    travellerWaveTime = 4;
                    StartCoroutine(OneSidedHell());
                    travellerSpawnCountStateOne += 1;
                }

                else if (travellerSpawnCountStateOne == 5)
                {
                    fifthCheck = true;
                    travellerWaveTime = 0;
                    StartCoroutine(OneSidedHell());
                    travellerSpawnCountStateOne += 1;
                    stateTwoOver = true;
                    Debug.Log(Time.time);
                }
            }

        }
        #endregion

        #region State Three
		if (stateTwoOver == false && randomStateCall == false && Time.timeSinceLevelLoad > 30) //stateTwoOver == true //can be called once state two is over and an interlude isn't occuring.
        {
            randomStateCount += Time.deltaTime;

            if (canCallState3 == true)
            {
                StateThree();
            }

           
            #region Random State
            if (randomStateCount >= 45)
            {
                randomStateCount = 0;

                if (randomCount == 1) //decreases the chance of the state being called if it was called previosuly.
                {
                    randomChance = 6;
                    randomCount = 0;
                }

                else //increases the chance if it wasnt called on the previous iteration.
                {
                    randomChance = 5;                  
                }

                int r = Random.Range(1, randomChance);
				Debug.Log(r);
                if (r == 4)
                {
					randomCount = 1;
					whichState = Random.Range(1, 3); //3 is exclusive

                    if (whichState == 1)
                    {
                        followerStateCall = true;
                        whichState += 1;
                        randomStateCall = true;
                        canCallfollowerState = true; 
                    }

                  else
                    {
                        tempTravellerRate = travellerSpawnRate;
						travellerSpawnCount = 0;
                        travellerStateCall = true;
                        whichState = 0;
                        randomStateCall = true;
                        canCallState2 = true;
                    }
                }

            }
            #endregion

            #region FollowerSpawning
            if (followerWaveTime >= followerSpawnRate)
            {
                followerWaveTime = 0;
                followersSpawned = Random.Range(followerSpawnMinimum, followerSpawnMaximum);

                for (int i = 0; i < followersSpawned; i++)
                {
                    float largestDistance = 0;
                    int indexCount = 0;
                    int index = 0;

                    foreach (GameObject spawns in enemySpawns)
                    {
                        floaterSpawnerScript = spawns.GetComponent<FloaterSpawner>();
                        if (floaterSpawnerScript.distance > largestDistance)
                        {
                            largestDistance = floaterSpawnerScript.distance;
                            index = indexCount;
                        }
                        
						indexCount += 1;
                    }

                    spawn = enemySpawns[index];
                    floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                    floaterSpawnerScript.followerSpawn();
                }
            }
            #endregion



            #region Floater Spawning
            if (floaterWaveTime >= floaterSpawnRate) //initates spawn
            {
                floaterWaveTime = 0;
                floaterWaveCount += 1;

                tempFloaterMaximum = floaterSpawnMaximum;
                tempFloaterSpawnMinimum = floaterSpawnMinimum;

                if (floaterWaveCount == floaterIncrementRate)
                {
					Debug.Log("increment");
					floaterWaveCount = 0;

                    if (floaterSpawnMaximum < floaterSpawnMaxMax) //max max is the highest the variable can ever be set to.
                    {
						Debug.Log("incrementMax");
						tempFloaterMaximum += 1;
                        floaterSpawnRate += 1;
						Debug.Log(tempFloaterMaximum);
                    }

                    if (floaterSpawnMinimum < floaterSpawnMinMax)
                    {
						Debug.Log("Increment Min");
						tempFloaterSpawnMinimum += 1;
                    }
                }

                if (floaterMoverAmount >= floaterMoverMaximum)
                {                   
                    int h = Mathf.RoundToInt((floaterMoverCap - floaterMoverAmount) / 2); //this allows for the mover and floater spawn to share the spawn space allowed.
                    floaterSpawnMaximum = h;
                    
                }

                floatersSpawned = Random.Range(floaterSpawnMinimum, floaterSpawnMaximum);
                y = 666; //because with the first iteration, y must not equal x,
                count = 0;
                int packCount = 0;

                for (int i = 0; i <= floatersSpawned; i++)
                {
                    if (y == x) //checks if the previous iteration produced the same spawn location.
                    {
                        count = count + 1;
                    }

                    if (count == (floatersSpawned - 1)) //if the same spawn was called for every spawn, the last one must be different.
                    {
                        z = (enemySpawns.Length - 1) - y;
                        //This works out the distance between the last spawn location and the current one.
                        //It rounds the x value to the furthest one away out of location 0 and the last location.
                        if (z > y)
                        {
                            spawn = enemySpawns[enemySpawns.Length - 1];
                            floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                            floaterSpawnerScript.floaterSpawn("normal");
                        }

                        else
                        {
                            spawn = enemySpawns[0];
                            floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                            floaterSpawnerScript.floaterSpawn("normal");
                        }
                    }

                    if (count < floatersSpawned) //this should be the normal spawning function.
                    {                       
                        x = Random.Range(0, (enemySpawns.Length - 1));
                        spawn = enemySpawns[x];
                        floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();

                        randomPack = Random.Range(1, 6); //this gives the pack a 1 in 5 chance of spawning

                        if (packCount == 2) //overrides packCount if there have already been 2 packs.
                        {
                            randomPack = 1;
                        }

                        if (randomPack < 5)
                        {
                            floaterSpawnerScript.floaterSpawn("normal");
                        }

                        else
                        {
                            packCount += 1;
                            floaterSpawnerScript.floaterSpawn("pack");
                        }

                        y = x;
                        floaterSpawnMaximum = tempFloaterMaximum; //the incremented values are stored in temp to stop them being overidden. This overriding occurs to stop the maximum cap cahnge only affect one wave.
                        floaterSpawnMinimum = tempFloaterSpawnMinimum;
                    }

                }


            }
            #endregion

            #region Mover Spawning
            if (moverWaveTime >= moverSpawnRate) //initates spawn
            {
                moverWaveTime = 0;
                moverWaveCount += 1;

                tempMoverMaximum = moverSpawnMaximum;
                tempMoverMinimum = moverSpawnMinimum;

                if (floaterMoverAmount >= floaterMoverMaximum)
                {
                    int h = Mathf.RoundToInt((floaterMoverCap - floaterMoverAmount) / 2); //this allows for the mover and floater spawn to share the spawn space allowed.
                    moverSpawnMaximum -= h;
                }

                if (moverWaveCount == moverIncrementRate)
                {
                    moverWaveCount = 0;
                    
                    if (moverSpawnMaximum < moverSpawnMaxMax)
                    {
                        tempMoverMaximum += 1;
                        powerUpWaitTime -= 2;
                        moverSpawnRate += 1;
                    }

                    if (moverSpawnMinimum < moverSpawnMinMax)
                    {
                        tempMoverMinimum += 1;
                    }
                }

                moversSpawned = Random.Range(moverSpawnMinimum, moverSpawnMaximum);
                moverY = 666; //because with the first iteration, y must not equal x,
                moverCount = 0;

                for (int i = 0; i <= moversSpawned; i++)
                {
                    if (moverY == moverX) //checks if the previous iteration produced the same spawn location.
                    {
                        moverCount = moverCount + 1;
                    }

                    if (moverCount == (moversSpawned - 1)) //if the same spawn was called for every spawn, the last one must be different.
                    {
                        moverZ = (enemySpawns.Length - 1) - moverX;
                        //This works out the distance between the last spawn location and the current one.
                        //It rounds the x value to the furthest one away out of location 0 and the last location.
                        if (moverZ > moverX)
                        {
                            spawn = enemySpawns[enemySpawns.Length - 1];
                            floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                            floaterSpawnerScript.moverSpawn();
                        }

                        else
                        {
                            spawn = enemySpawns[0];
                            floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                            floaterSpawnerScript.moverSpawn();
                        }
                    }

                    if (moverCount < moversSpawned) //this should be the normal spawning function.
                    {
                        moverX = Random.Range(0, (enemySpawns.Length - 1));
                        spawn = enemySpawns[x];
                        floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                        floaterSpawnerScript.moverSpawn();
                        moverY = moverX;
                    }

                    moverSpawnMaximum = tempMoverMaximum;
                    moverSpawnMinimum = tempMoverMinimum;

                }


            }
            #endregion

            #region Traveller Spawning
            if (travellerWaveTime >= travellerSpawnRate)
            {
                Debug.Log("travellerSpawnTriggered");
                travellerWaveTime = 0;
                int patternIndex = Random.Range(0, 4);

                travellerWaveCount += 1;

                if (travellerWaveCount == travellerIncrementRate)
                {
                    travellerWaveCount = 0;

                    if (travellerSpawnRate < travellerRateMax)
                    {
                        travellerSpawnRate -= 1;
                    }
                }


                if (patternIndex == 0)
                {
                    HorizontalHell();
                }

                if (patternIndex == 1)
                {
                    VerticalHell();
                }

                if (patternIndex == 2)
                {
                    StartCoroutine(OneSidedHell());
                }

                if (patternIndex == 3)
                {
                    LaserGrid();
                }


            }
            #endregion

            #region Circler Spawning
            if(circlerWaveTime >= circlerSpawnRate)
            {
                circlerWaveTime = 0;

                circlerWaveCount += 1;

                if (circlerWaveCount == circlerIncrementRate)
                {
                    circlerWaveCount = 0;

                    if (circlerSpawnRate < circlerRateMax)
                    {
                        circlerSpawnRate -= 1;
                    }
                }

                foreach (GameObject circlerSpawns in enemySpawns)
                {
                    CirclerSpawn circlerSpawnScript = circlerSpawns.GetComponent<CirclerSpawn>();
                    circlerSpawnScript.spawn();
                }
            }
            #endregion
        }
        #endregion

        #region Traveller State
        if (travellerStateCall == true && floaterAmount == 0 && moverAmount == 0)
        {
			Debug.Log("Traveller State");

            if (canCallState2 == true)
            {
                StateTwo();
            }

			Debug.Log(travellerSpawnCount);

            if (travellerWaveTime >= travellerSpawnRate)
            {
                if (travellerSpawnCount == 0)
                {
					Debug.Log(travellerSpawnCount);
					Debug.Log("called horiz");
                    travellerWaveTime = 0;
                    HorizontalHell();
                    travellerSpawnCount += 1;
                }

                else if (travellerSpawnCount == 1)
                {
					Debug.Log(travellerSpawnCount);
					Debug.Log("called vert");
                    travellerWaveTime = 0;
                    VerticalHell();
                    travellerSpawnCount += 1;
                }

                else if (travellerSpawnCount == 2)
                {
					Debug.Log(travellerSpawnCount);
					Debug.Log("called one side");
                    travellerWaveTime = 0;
                    StartCoroutine(OneSidedHell());
                    travellerSpawnCount += 1;
                }

                else if (travellerSpawnCount == 3)
                {
					Debug.Log(travellerSpawnCount);
					Debug.Log("called Laser");
                    travellerWaveTime = 0;
                    LaserGrid();
                    travellerSpawnCount += 1;
                }

                else if (travellerSpawnCount == 4)
                {
					Debug.Log(travellerSpawnCount);
					travellerWaveTime = 4;
                    StartCoroutine(OneSidedHell());
                    travellerSpawnCount += 1;
                }

                else if (travellerSpawnCount == 5)
                {
					Debug.Log(travellerSpawnCount);
					fifthCheck = true;
                    travellerWaveTime = 0;
                    StartCoroutine(OneSidedHell());
                    travellerSpawnCount += 1;
                    Debug.Log(Time.time);
                }

                else if (travellerSpawnCount == 6)
                {
					Debug.Log(travellerSpawnCount);
					travellerStateCall = false;
                    randomStateCall = false;
                    travellerSpawnRate = tempTravellerRate;                   
                }
            }

        }
        #endregion

        #region Follower State
        if (followerStateCall == true && floaterAmount == 0 && moverAmount == 0)
        {
            StartCoroutine(FollowerTime());

            if (canCallfollowerState == true)
            {
                FollowerState();
            }

            if (followerWaveTime >= followerSpawnRate)
            {
                followerWaveTime = 0;
                followersSpawned = Random.Range(followerSpawnMinimum, followerSpawnMaximum);

                for (int i = 0; i <= followersSpawned; i++)
                {
                    x = Random.Range(0, (enemySpawns.Length - 1));
                    spawn = enemySpawns[x];
                    floaterSpawnerScript = spawn.GetComponent<FloaterSpawner>();
                    floaterSpawnerScript.followerSpawn();
                }
            }


        }
#endregion
    }

    void StateOne () //initialises state one variables
    {
        floaterMinimumMovementRate = 3;
        floaterMaximumMovementRate = 5;
        floaterFireRate = 0.9f;
        floaterWaveTime = 5; //counts in seconds.
        floaterSpawnRate = 4; 
        floaterSpawnMinimum = 4; //4 //the minimum amount of floaters that can spawn.
        floaterSpawnMaximum = 8;//8 //the maximum that can spawn.
        floaterMaximum = 18; //maximum allowed on screen.
        floaterBulletSpeed = 8; //6

        followerInitialSpeed = 2;
        followerEndSpeed = 5;
        followerWaveTime = 0;
        followerSpawnRate = 6;
        followerSpawnMinimum = 1;
        followerSpawnMaximum = 1;

        canCallState1 = false;

    }

    void StateTwo ()
    {        
        travellerABulletSpeed = 10;
        travellerASpeed = 12;
        travellerFireRate = 0.4f;
        travellerSpawnRate = 5;
        travellerWaveTime = 5;

        canCallState2 = false; 
    }

    void StateThree ()
    {
        floaterMinimumMovementRate = 3;
        floaterMaximumMovementRate = 5;
        floaterFireRate = 1; //1
        floaterWaveTime = 4;
        floaterSpawnRate = 4; //6 //8 //decreased from 8 but decereased floater rates
        floaterSpawnMinimum = 1; //1
        floaterSpawnMaximum = 5; //3 //exclusive ending to range. //4
        floaterMaximum = 10; //10
        floaterBulletSpeed = 6; //7

        floaterMoverMaximum = 13; 

        followerInitialSpeed = 3; //4
        followerEndSpeed = 4; //5
        followerWaveTime = 0;
        followerSpawnRate = 6; //6
        followerSpawnMinimum = 2; //1
        followerSpawnMaximum = 4; //2

        travellerABulletSpeed = 7;
        travellerASpeed = 10;
        travellerFireRate = 0.7f;
        travellerSpawnRate = 15; //10
        travellerWaveTime = 5;

        moverSpawnMinimum = 3; //4
        moverSpawnMaximum = 5; //6
        moverWaveTime = 6; //8
        moverSpawnRate = 6; //12 //15
        moverBulletSpeed = 6; //6
        moverFireRate = 2.7f; //1.3f

        circlerTimeToNode = 1;
        circlerSpawnRate = 20;
        circlerWaveTime = 10; //10

        canCallState3 = false;
    }

    void FollowerState()
    {
        followerInitialSpeed = 5;
        followerEndSpeed = 6;
        followerWaveTime = 0;
        followerSpawnRate = 3;
        followerSpawnMinimum = 3;
        followerSpawnMaximum = 5;

        canCallfollowerState = false;
    }

    IEnumerator FollowerTime()
    {
        yield return new WaitForSeconds(15);
        followerStateCall = false;
        moverWaveTime = 4;
        floaterWaveTime = 3;
        circlerWaveTime = 0;
        floaterWaveTime = 0;
        randomStateCall = false; //false
        FollowerReset();
    }

    public void FollowerReset()
    {
        followerInitialSpeed = 3; 
        followerEndSpeed = 4; 
        followerWaveTime = 0;
        followerSpawnRate = 6; 
        followerSpawnMinimum = 1; 
        followerSpawnMaximum = 2;
    }

    public void LaserGrid()
    {
        foreach (Transform travellerSpawn in travellerSpawns)
        {
            Debug.Log("Laser Grid");
            Instantiate(traveller, travellerSpawn.position, travellerSpawn.rotation);
        }
    }

    public void HorizontalHell()
    {
        Debug.Log("Horizontal Hell");
        Instantiate(traveller, travellerSpawns[1].position, travellerSpawns[1].rotation);
        Instantiate(traveller, travellerSpawns[2].position, travellerSpawns[2].rotation);
    }

    public void VerticalHell()
    {
        Debug.Log("Vertical Hell");
        Instantiate(traveller, travellerSpawns[0].position, travellerSpawns[0].rotation);
        Instantiate(traveller, travellerSpawns[3].position, travellerSpawns[3].rotation);
    }

    public IEnumerator OneSidedHell()
    {
        Debug.Log("One Sided Hell");
        int spawnIndex = Random.Range(0, 4);
        float travellerGap = Random.Range(0.5f, 1);

        if (fifthCheck == true)
        {
            if (indexContainer == 3)
            {
                spawnIndex = 2;
            }

            if (indexContainer == 0)
            {
                spawnIndex = 1;
            }

            else
            {
                spawnIndex = indexContainer + 1;
            }
        }

        Instantiate(traveller, travellerSpawns[spawnIndex].position, travellerSpawns[spawnIndex].rotation);
        yield return new WaitForSeconds(travellerGap);
        Instantiate(traveller, travellerSpawns[spawnIndex].position, travellerSpawns[spawnIndex].rotation);
        yield return new WaitForSeconds(travellerGap);
        Instantiate(traveller, travellerSpawns[spawnIndex].position, travellerSpawns[spawnIndex].rotation);
        yield return new WaitForSeconds(travellerGap);
        Instantiate(traveller, travellerSpawns[spawnIndex].position, travellerSpawns[spawnIndex].rotation);
        indexContainer = spawnIndex;
    }
}


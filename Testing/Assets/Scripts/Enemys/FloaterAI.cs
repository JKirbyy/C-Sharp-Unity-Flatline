using UnityEngine;
using System.Collections;

public class FloaterAI : MonoBehaviour
{
    public float movementSpeed;
	public float movementMinimum = 2;
    private GameObject player;
	float distance;
	private Vector3 targetPosition;
	private bool canCall;
    public bool notMoving = false;
    public bool wait = true;
	[HideInInspector]

	void Start () 
	{
        player = GameObject.FindGameObjectWithTag("Player");
        FindNewPosition();

		if (gameObject.GetComponent<Collider2D> () == null) 
		{
			Destroy (gameObject);
		}
    }


	void Update () 
	{
        if(player)
        { 
            distance = Vector3.Distance(targetPosition, transform.position);

			if (gameObject.GetComponent<Collider2D> () == null) 
			{
				Destroy (gameObject);
			}

            if (distance > 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            }

            else if (canCall == true)
            {
                canCall = false;
                StartCoroutine(FloaterMovement());
            }
        }

        if(transform.childCount == 0)
            Destroy(gameObject);	
	}

	IEnumerator FloaterMovement()
	{
        notMoving = true;
        Debug.Log("co routine");
		yield return new WaitForSeconds (Random.Range(DifficultyManager.floaterMinimumMovementRate, DifficultyManager.floaterMaximumMovementRate));
		FindNewPosition ();
	}

	void FindNewPosition()
	{
        if (player)
        {
            Debug.Log("finding position");
            notMoving = false;
            targetPosition = new Vector3(Random.Range((gameObject.transform.position.x + movementMinimum), (player.transform.position.x - 0.5f)), Random.Range(gameObject.transform.position.y, player.transform.position.y), 0);
            Debug.Log("Change Position");
            canCall = true;
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}


	//if statement to call ienumerator
	//Float around the screen firing at player
	//Go to a random position between the player and their spawn point
	//Then every 3 seconds go to a random position between the player and their current position
}
using UnityEngine;
using System.Collections;

public class MoverAI : MonoBehaviour
{
	public float movementSpeed = 1;
	public float movementRate = 3;
	public float movementMinimum = 2;
    private GameObject player;
	private Vector3 targetPosition;
	private bool canCall;
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

			if (gameObject.GetComponent<Collider2D> () == null) 
			{
				Destroy (gameObject);
			}
           
            if (canCall == true)
            {
                canCall = false;
                StartCoroutine(FloaterMovement());
            }
        }
	}

	IEnumerator FloaterMovement()
	{
        Debug.Log("co routine");
		yield return new WaitForSeconds (movementRate);
		FindNewPosition ();
	}

	void FindNewPosition()
	{
        if (player)
        {
            Debug.Log("finding position");
            targetPosition = new Vector3(Random.Range((gameObject.transform.position.x + movementMinimum), (player.transform.position.x - 0.2f)), Random.Range(gameObject.transform.position.y + movementMinimum, player.transform.position.y - 0.2f), 0);
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
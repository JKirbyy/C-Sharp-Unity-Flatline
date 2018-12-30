using UnityEngine;
using System.Collections;

public class FollowerAI : MonoBehaviour
{
    private Vector3 directionVector; //the direction the follower is looking in.
    public float initialSpeed; //the starting speed.
    public float endSpeed; //the full speed.
    public float followerSpeed; //the current speed.
    public float speedIncrease = 0.5f; //how much the speed interpolates by each frame.
    public Transform playerPosition; //the position of the player.

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 6);

		if (gameObject.GetComponent<Collider2D> () == null) 
		{
			Destroy (gameObject);
		}

    }


    void Update()
    {
		if (playerPosition) 
		{
			if (gameObject.GetComponent<Collider2D> () == null) {
				Destroy (gameObject);
			}

			followerSpeed = Mathf.Lerp (initialSpeed, endSpeed, speedIncrease);
			//transform.LookAt(playerPosition);

			directionVector = (playerPosition.position - transform.position).normalized;
			transform.up = directionVector;

			transform.position += transform.up * followerSpeed * Time.deltaTime;
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }

	}
}

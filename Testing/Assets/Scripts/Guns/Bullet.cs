using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 14;

	void Start () 
	{
		Destroy (gameObject, 7);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += (transform.up * bulletSpeed * Time.deltaTime); //changes the position of the bullet.
	}

	void OnTriggerEnter2D(Collider2D other) //called if the collider touches another collider.
	{
		if (other.tag == "Floater" || other.tag == "Follower" || other.tag == "Mover" || other.tag == "Circler" || other.tag == "Traveller")
		{
			DestroyObject (other.gameObject);
            GameManager.IncreaseScore(5);
            Destroy (gameObject);
		} 
	}
}

using UnityEngine;
using System.Collections;

public class HeavyBullet : MonoBehaviour
{
    public float bulletSpeed = 10;
	private int count;
	public GameObject player;

    void Start()
    {
        Destroy(gameObject, 7);
		count = 0;
		player = GameObject.Find ("Player New");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.up * bulletSpeed * Time.deltaTime);

		if (count == 4) 
		{
			count = 0;
			Player players = player.GetComponent<Player> ();
			players.HealthGain ();
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floater" || other.tag == "Follower" || other.tag == "Mover" || other.tag == "Circler" || other.tag == "Traveller")
        {
			if (other.tag != "Circler") 
			{
				count = count + 1;
			}
						
			DestroyObject(other.gameObject);
            GameManager.IncreaseScore(5);
            //Destroy(gameObject);
        }
    }

}

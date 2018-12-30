using UnityEngine;
using System.Collections;

public class shotgunBullet : MonoBehaviour
{

    public float bulletSpeed = 10;
    public float range;
    void Start()
    {
        Destroy(gameObject, range);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.up * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floater" || other.tag == "Follower" || other.tag == "Mover" || other.tag == "Circler" || other.tag == "Traveller")
        {
            DestroyObject(other.gameObject);
        }
    }
}

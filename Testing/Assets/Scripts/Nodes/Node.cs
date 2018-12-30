using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform player;
    private Vector3 playerVector;
    private Vector3 distance;
    public float x;
    public float y;

    void Start()
    {
        distance = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerVector = new Vector3(player.position.x, player.position.y, 0);
            transform.position = playerVector + distance;
        }
    }
}

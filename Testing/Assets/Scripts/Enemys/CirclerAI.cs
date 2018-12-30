using UnityEngine;
using System.Collections;

public class CirclerAI : MonoBehaviour
{
    private Vector3 directionVector; //the direction the circler goes in.
    public float circlerSpeed; //the speed of the circler.
    public float timeToHitNode; //the time taken for the circler to reach its target node.
    public Transform targetNode; //the target node for the circler.
    public string targetNodeString; //the name of the node passed from the spawn class.
    public bool hasTarget = false; //whether or not the spawn has passed a target.
    private float distance = 3; //distance between circler and node.
	private Transform player; //the player.
    private Vector2 targetNodeVector; //the position of the target node.
    private Vector2 currentPositionVector; //the position of the circler.
    private bool targetNodeSet = false; //whether or not the cicler has a target.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

		if (gameObject.GetComponent<Collider2D> () == null) 
		{
			Destroy (gameObject);
		}
    }

   
    void Update()
    {
		if (gameObject.GetComponent<Collider2D> () == null) 
		{
			Destroy (gameObject);
		}

		if (player)
        {
            if (targetNodeSet == true)
            {
                targetNodeVector = new Vector2(targetNode.position.x, targetNode.position.y);
                currentPositionVector = new Vector2(transform.position.x, transform.position.y);
                distance = Vector2.Distance(targetNodeVector, currentPositionVector);
                circlerSpeed = (distance / timeToHitNode) * 2;
            }          

            if (hasTarget == true)
            {
                findTarget();
                hasTarget = false;
            }

            if (distance < 2)
            {
                targetNode = player;
                circlerSpeed = circlerSpeed * 1.2f;
            }

            directionVector = (targetNode.position - transform.position).normalized;
            transform.up = directionVector;
            transform.position += transform.up * circlerSpeed * Time.deltaTime;


        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DestroyObject(gameObject);
        }
    }

    void findTarget ()
    {
        Debug.Log("finding target");
        targetNode = GameObject.Find(targetNodeString).transform;
        targetNodeSet = true;
    }
}

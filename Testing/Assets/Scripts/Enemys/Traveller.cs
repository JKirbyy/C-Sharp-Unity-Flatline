using UnityEngine;
using System.Collections;

public class Traveller : MonoBehaviour {

    public float speed;


	void Start () 
	{
		Destroy (gameObject, 7);

		if (gameObject.GetComponent<Collider2D> () == null) 
		{
			Destroy (gameObject);
		}
	}
	
    
	void Update () 
	{
		transform.position += transform.right * speed * Time.deltaTime;

	}


}


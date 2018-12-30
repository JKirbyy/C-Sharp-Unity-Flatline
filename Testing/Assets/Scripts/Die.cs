using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public bool pause;

    //this can be replaced by time.timescale being set to 0

    void Start()
    {
        pause = false;
    }
	
	
	void Update ()
    {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            pause = true;
            DestroyObject(gameObject);
        }
    }
}

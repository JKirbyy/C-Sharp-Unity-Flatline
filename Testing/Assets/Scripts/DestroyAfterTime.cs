using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	public float waitTime = 5f;

	void Start()
	{
		Destroy(gameObject, waitTime);
	}
}

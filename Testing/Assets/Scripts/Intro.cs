using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour 
{
	public UIManager mang;
	public GameObject events;
	public GameObject image;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (IntroCount ());
		mang = events.GetComponent<UIManager> ();
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			{
				mang.LoadScene("Main");
			}

		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			StartCoroutine (Skip ());
		}
	}

	IEnumerator IntroCount ()
	{
		yield return new WaitForSecondsRealtime(36);
		mang.LoadScene ("Main");

	}

	IEnumerator Skip ()
	{
		image.SetActive (true);
		yield return new WaitForSecondsRealtime (4);
		image.SetActive (false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
	public static float count = 0; //counts how long since the last bullet was fired.
    public static bool powerUpCheck = false; //checks is power up has been triggered.
    public static bool powerUpReady = false; //checks if power up is ready.
    public float powerUpTime = 15; //the amount the count needs to equal to make the power up active.
    public static float powerDownCount; 
    public Image powerDownBarFill;
    Color c;
	
	void Start ()
    {
       c = powerDownBarFill.color;
		count = 0;
		powerUpReady = false; //so on restart it won't be available;
    }
	
	
	void Update ()
    {

        if (powerUpCheck == false)  //makes sure the count won't happen during the power up.
        {
            count += Time.deltaTime;
        }

        if (powerUpReady == true) //holds the power up so it can be used if space is pressed.
        {
			count = 15;

			if (Input.GetKeyDown(KeyCode.Space))
            {
                powerUpReady = false;
                StartCoroutine(PowerUpProc());
            }
        }

        if (powerUpCheck == true)
        {          
            //powerDownCount -= Time.deltaTime * 10;          
            Time.timeScale = 0.1f;
			count = 0;

            if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Time.timeScale = 1;
				//powerDownCount -= Time.deltaTime * 10;  
            }
        }

        if (Input.GetButton("Fire1") || Input.GetButtonDown("Fire1"))
        {
			if (powerUpReady == false)
            {
				if (Gun.isPaused == false) 
				{
					//Debug.Log (count);
					count = 0;
				}
            }
        }
        
        if (count >= DifficultyManager.powerUpWaitTime)
        {
            powerUpReady = true;
        }        	
	}

    IEnumerator PowerUpProc ()
    {
        Debug.Log("calledPowerUp");
        powerDownCount = powerUpTime;
        c.a = 255;
        powerDownBarFill.color = c;
        Debug.Log(c.a);
        powerUpCheck = true;
		StartCoroutine (CountDown ());
		yield return new WaitForSecondsRealtime (powerUpTime);
		powerDownCount = 0;//real time because of time scale.
        c.a = 0;
        powerDownBarFill.color = c;
        powerUpCheck = false;
        Time.timeScale = 1;
        Gun.ResetRate();
        Player.ResetSpeed();
		count = 0;
    }

	IEnumerator CountDown ()
	{
		yield return new WaitForSecondsRealtime (2);
		powerDownCount = 13;
		yield return new WaitForSecondsRealtime (2);
		powerDownCount = 11;
		yield return new WaitForSecondsRealtime (2);
		powerDownCount = 9;
		yield return new WaitForSecondsRealtime (2);
		powerDownCount = 7;
		yield return new WaitForSecondsRealtime (2);
		powerDownCount = 5;
		yield return new WaitForSecondsRealtime (2);
		powerDownCount = 3;

	}

}

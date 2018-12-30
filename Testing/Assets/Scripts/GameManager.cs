using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score = 0;
    public static int Stage = 0;
	public static int highScore;


	void Start()
	{
		highScore = PlayerPrefs.GetInt ("HighScore", 0);
		Score = 0;
	}
		
    public static void IncreaseScore(int amount)
    {
        Score += amount;
    }

    public static void IncreaseStage(int amount)
    {
        Stage += amount;
    }

	public static void ScoreSave()
	{
		if (Score > highScore) 
		{
			PlayerPrefs.SetInt ("HighScore", Score);
			highScore = Score;
		}
	}

		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo 
{
    public static void SaveScore(int score)
    {
        PlayerPrefs.SetInt("score", score);
    }

    public static int GetScore()
    {
       return PlayerPrefs.GetInt("score");
    }
}

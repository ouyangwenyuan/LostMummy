using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Util
{
    public static void SetLevelToStorage(int level)
    {
        PlayerPrefs.SetInt("Level", level);
    }


    public static void SetScoreToStorage(int score)
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public static int GetScore()
    {
        return PlayerPrefs.GetInt("Score", 0); //Get level max from storage, default value =1
    }


    public static void SetCurrentToStorage(int current_level)
    {
        PlayerPrefs.SetInt("CurrentLevel", current_level);
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("Level", 1); //Get level max from storage, default value =1
    }

    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 1); //Get current level from storage, default value =1
    }

    public static int GetSoundState()
    {
        return PlayerPrefs.GetInt("Sound", 1); ///1 is Enable sound
    }

    public static void SetSoundState(int s)
    {
        PlayerPrefs.SetInt("Sound", s);
    }
}

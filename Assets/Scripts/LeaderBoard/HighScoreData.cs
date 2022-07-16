using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HighScoreData { 
    
    public int Score;
    public string PlayerId;
    public string PlayerName;



    public HighScoreData(int score)
    {
        Score = score;
        PlayerId = PlayerPrefs.GetString("CurrentPlayerId");
        PlayerName = PlayerPrefs.GetString("PlayerName");
    }

    public HighScoreData(int score, string playerId, string playerName)
    {
        Score = score;
        PlayerId = playerId;
        PlayerName = playerName;
    }
}

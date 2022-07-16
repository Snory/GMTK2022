using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : Singleton<LeaderBoard>
{

    private IRepository<HighScoreData> leaderBoardRepository;

    // Start is called before the first frame update
    void Start()
    {
        leaderBoardRepository = new LootLockerLeaderboardRepository();
        leaderBoardRepository.Load();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        leaderBoardRepository.Save();
    }
}

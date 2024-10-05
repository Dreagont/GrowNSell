using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
public class LeaderBoard : MonoBehaviour
{
    //private string PrivateLeaderBoardKey = "1150df401326607b735ea25d04ed8707c82c218a6e061b4ef410537107d79e5d93a85ce6aad78e32a07163a60e3ea5dfe19d6d340becbc5b0d5b1cfcf22bba485a33bb88833c63731e49c6c72f2eeb7edd04e6d6df7019f4367448a62045eb9d620eb998a25f228117f56a80fa2b1e12aa8a09473c874700a9510d55c5e1aaee";
    private string PublicLeaderBoardKey = "0ddd0da8a8e4a9f7b0a5447602b70589f263212a54c912c6b8fc4eb921c6f98d";
    public GameObject PlayerRankPrefab;
    public Transform RankParent;
    void Start()
    {
        GetLeaderBoard();   
    }
    void Update()
    {
        
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(PublicLeaderBoardKey, ((msg) =>
        {
            foreach (var item in msg)
            {
                GameObject boardPlayer =  Instantiate(PlayerRankPrefab, RankParent);
                PlayerRankUI playerRank = boardPlayer.GetComponent<PlayerRankUI>();

                playerRank.PlayerRankText.text = item.Rank.ToString();
                playerRank.PlayerNameText.text = item.Username;
                playerRank.PlayerGoldText.text = item.Score.ToString(); 
            }
        }));
    }

    public void SetLeaderBoard(string name, int gold)
    {
        LeaderboardCreator.UploadNewEntry(PublicLeaderBoardKey, name, gold, ((msg) =>
        {
            if (msg)
            {
                GetLeaderBoard();
            }
        }));
    }
}

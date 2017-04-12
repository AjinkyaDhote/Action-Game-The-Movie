using System.Collections.Generic;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;

public class AccessLeaderBoard : MonoBehaviour
{
    private bool hasLeaderBoardBeenAccessed;

    private void Awake()
    {
        hasLeaderBoardBeenAccessed = false;
        if (!GS.Authenticated)
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadData()
    {
        if (!GS.Authenticated)
        {
            Debug.Log("Cannot Access Leaderboard as GameSparks not available..");
            return;
        }
        if (hasLeaderBoardBeenAccessed) return;
        Text[] textslots = null;
        new LeaderboardDataRequest()
            .SetEntryCount(10)
            .SetIncludeFirst(10)
            .SetLeaderboardShortCode(GameManager.Instance.LeaderBoardShortCode)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    textslots =
                        transform.parent.parent.FindChild("LeaderboardCanvas")
                            .FindChild("ScoreSlots")
                            .GetComponentsInChildren<Text>();
                    Debug.Log("Found Leaderboard Data...");
                    int i = 0;
                    foreach (var entry in response.Data)
                    {
                        textslots[i].text = (entry.Rank ?? 0) + " : " + entry.UserName + "'s Score: " + entry.JSONData[GameManager.Instance.EventAttributeShortCodeHighScore].ToString();
                        i++;
                    }
                    GSData a = response.ScriptData;
                    textslots[10].text = GameManager.Instance.CurrentPlayerRank + " : " +
                                         GameManager.Instance.CurrentPlayerDisplay + "'s Score: " +
                                         GameManager.Instance.TotalScore;
                }
                else
                {
                    Debug.Log("Error Retrieving Leaderboard Data...");
                }
            });
        hasLeaderBoardBeenAccessed = true;
    }
}

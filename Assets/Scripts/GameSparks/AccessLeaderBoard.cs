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
    }

    public void LoadData()
    {
        if (!GS.Available)
        {
            Debug.Log("Cannot Access Leaderboard as GameSparks not available..");
            return;
        }
        if (!hasLeaderBoardBeenAccessed)
        {
            Text[] textslots = null;
            new LeaderboardDataRequest()
                .SetEntryCount(10)
                .SetIncludeFirst(10)
                .SetLeaderboardShortCode("wwlb")
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
                        foreach (LeaderboardDataResponse._LeaderboardData entry in response.Data)
                        {
                            textslots[i].text = (entry.Rank ?? 0) + " : " + entry.UserName + "'s Score: " + entry.JSONData["cpscore"].ToString();
                            i++;
                        }
                    }
                    else
                    {
                        Debug.Log("Error Retrieving Leaderboard Data...");
                    }
                });

            new LeaderboardsEntriesRequest()
            .SetLeaderboards(new List<string>() { "wwlb" })
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    if (textslots[10] != null)
                    {
                        if (response.ScriptData != null)
                        {
                            textslots[10].text = response.Results.GetString("rank").ToString() + " : " + response.Results.GetString("userName") + " Score: " + response.Results.GetString("cpscore").ToString();
                        }
                    }
                }
                else
                {
                    Debug.Log("Error Retrieving Leaderboard Data...");
                }
            });
            hasLeaderBoardBeenAccessed = true;
        }
    }
}

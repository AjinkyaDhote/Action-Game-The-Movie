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
                        foreach (LeaderboardDataResponse._LeaderboardData entry in response.Data)
                        {
                            textslots[i].text = (entry.Rank ?? 0) + " : " + entry.UserName + "'s Score: " + entry.JSONData[GameManager.Instance.EventAttributeShortCodeHighScore].ToString();
                            i++;
                        }
                    }
                    else
                    {
                        Debug.Log("Error Retrieving Leaderboard Data...");
                    }
                });

            new AccountDetailsRequest()
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    new GetLeaderboardEntriesRequest()
                        .SetPlayer(response.UserId)
                        .SetLeaderboards(new List<string>() { GameManager.Instance.LeaderBoardShortCode })
                        .Send((response2) =>
                        {
                            if (!response2.HasErrors)
                            {
                                if (textslots[10] != null)
                                {
                                    var number = response2.BaseData.GetGSData(GameManager.Instance.LeaderBoardShortCode).GetNumber(GameManager.Instance.EventAttributeShortCodeCurrentScore);
                                    if (number != null)
                                    {
                                        textslots[10].text = response.DisplayName + "'s Score: " + number;
                                    }                             
                                }
                            }
                            else
                            {
                                Debug.Log("Error Retrieving Leaderboard Data...");
                            }
                        });
                }
                else
                {
                    Debug.Log("Error Retrieving Current Player Data...");
                }
            });
            hasLeaderBoardBeenAccessed = true;
        }
    }
}

using System.Collections.Generic;
using System.Text;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;

public class AccessLeaderBoard : MonoBehaviour
{
    private bool hasLeaderBoardBeenAccessed;
    private static readonly Color32 CurrentPlayerColor = new Color32(128, 214, 255, 255);
    private static readonly Color32 CurrentPlayerColorInLeaderBoard = new Color32(0, 170, 255, 255);

    private void Awake()
    {
        hasLeaderBoardBeenAccessed = false;
        gameObject.SetActive(GS.Authenticated && !GameManager.Instance.isTutotialLevel);
    }

    public void LoadData()
    {
        if (!GS.Authenticated)
        {
            Debug.Log("Cannot Access Leaderboard as GameSparks not available..");
            return;
        }
        if (hasLeaderBoardBeenAccessed) return;
        Text[] rankPlayerNameSlots;
        Text[] scoreSlots;
        new LeaderboardDataRequest()
            .SetEntryCount(10)
            .SetIncludeFirst(10)
            .SetLeaderboardShortCode(GameManager.Instance.LeaderBoardShortCode)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    rankPlayerNameSlots =
                        transform.parent.parent.FindChild("LeaderboardCanvas")
                            .FindChild("RankPlayerNameSlots")
                            .GetComponentsInChildren<Text>();
                    scoreSlots =
                       transform.parent.parent.FindChild("LeaderboardCanvas")
                           .FindChild("ScoreSlots")
                           .GetComponentsInChildren<Text>();
                    Debug.Log("Found Leaderboard Data...");
                    int i = 0;
                    foreach (var entry in response.Data)
                    {
                        rankPlayerNameSlots[i].text = (entry.Rank ?? 0) + ". " + entry.UserName;
                        scoreSlots[i].text =
                            entry.JSONData[GameManager.Instance.EventAttributeShortCodeHighScore].ToString();

                        if (entry.UserName == GameManager.Instance.CurrentPlayerDisplay)
                        {
                            rankPlayerNameSlots[i].color = CurrentPlayerColorInLeaderBoard;
                            scoreSlots[i].color = CurrentPlayerColorInLeaderBoard;
                        }
                        i++;
                    }
                    rankPlayerNameSlots[10].text = GameManager.Instance.CurrentPlayerRank + ". " +
                                         GameManager.Instance.CurrentPlayerDisplay;
                    scoreSlots[10].text = GameManager.Instance.TotalScore.ToString();
                    rankPlayerNameSlots[10].color = CurrentPlayerColor;
                    scoreSlots[10].color = CurrentPlayerColor;
                }
                else
                {
                    Debug.Log("Error Retrieving Leaderboard Data...");
                }
            });
        hasLeaderBoardBeenAccessed = true;
    }
}

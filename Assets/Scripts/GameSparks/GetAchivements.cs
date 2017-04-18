using System.Collections.Generic;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;

public class GetAchivements : MonoBehaviour
{
    private bool areAchivementsDisplayed;

    private void Awake()
    {
        areAchivementsDisplayed = false;
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
        if (!areAchivementsDisplayed)
        {
            Text[] achivementTextSlots;
            Image[] achivementImageSlots;
            new ListAchievementsRequest()
                .Send((response) =>
                   {
                       if (!response.HasErrors)
                       {
                           achivementTextSlots =
                            transform.parent.parent.FindChild("AchievementsCanvas")
                                .FindChild("AchievementSlots")
                                .FindChild("AchivementTextSlots")
                                .GetComponentsInChildren<Text>();
                           achivementImageSlots =
                            transform.parent.parent.FindChild("AchievementsCanvas")
                                .FindChild("AchievementSlots")
                                .FindChild("AchievementImageSlots")
                                .GetComponentsInChildren<Image>();
                           int i = 0;
                           foreach (var achievement in response.Achievements)
                           {
                               //if (!(achievement.Earned ?? true)) continue;
                               achivementTextSlots[i].text = achievement.Name;

                               if (achievement.Earned ?? true)
                               {
                                   Sprite sprite;
                                   GameManager.BadgesAchieved.TryGetValue(achievement.ShortCode.GetHashCode(), out sprite);
                                   if (sprite != null) achivementImageSlots[i].sprite = sprite;
                               }
                               else
                               {
                                   Sprite sprite;
                                   GameManager.BadgesNotAchieved.TryGetValue(achievement.ShortCode.GetHashCode(), out sprite);
                                   if (sprite != null) achivementImageSlots[i].sprite = sprite;
                               }
                               i++;
                           }
                       }
                       else
                       {
                           Debug.Log("Error Retrieving Achievements Data...");
                       }
                   });
        }
    }
}

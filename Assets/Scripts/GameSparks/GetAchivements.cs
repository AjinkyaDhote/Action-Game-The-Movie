using System.Collections.Generic;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;

public class GetAchivements : MonoBehaviour
{
    private bool areAchivementsDisplayed;
    private Image imageScript;
    private Button buttonScript;
    private Text textScript;
    private AchievementCanvasBackButton _achievementCanvasBackButton;

    private void Awake()
    {
        areAchivementsDisplayed = false;
        imageScript = GetComponent<Image>();
        buttonScript = GetComponent<Button>();
        textScript = transform.GetChild(0).GetComponent<Text>();
        _achievementCanvasBackButton =
            transform.parent.parent.FindChild("AchievementsCanvas")
                .FindChild("ScoreBoardBt")
                .GetComponent<AchievementCanvasBackButton>();
    }

    private void Update()
    {
        imageScript.enabled = GS.Authenticated;
        buttonScript.enabled = GS.Authenticated;
        textScript.enabled = GS.Authenticated;
    }

    public void LoadData()
    {
        _achievementCanvasBackButton.Mount = transform.parent.GetChild(1);
        if (!GS.Authenticated)
        {
            Debug.Log("Cannot Access Leaderboard as GameSparks not available..");
            return;
        }
        if (areAchivementsDisplayed) return;
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
                          
                    foreach (var achievement in response.Achievements)
                    {
                        int i;
                        GameManager.AchivementPlaceMap.TryGetValue(achievement.ShortCode.GetHashCode(), out i);
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
                    }
                }
                else
                {
                    Debug.Log("Error Retrieving Achievements Data...");
                }
            });
    }
}

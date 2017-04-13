using UnityEngine;
using GameSparks.Core;
using GameSparks.Api.Requests;

public class EndStateScript : MonoBehaviour
{
    private Scoring score;
    private bool hasPayloadReached;
    private bool hasPlayerReached;
    private bool hasScoreAchievementsBeenUpdated;
    private bool hasPreviousHeadshotValueBeenReceived;
    private bool hasLevelCompletionAchivementBeenAwarded;
    private bool has100HeadShotsAchivementBeenAwarded;
    private bool hasHighScoreBeenPosted;
    private int? previousHeadshots = 0;

    private void Start()
    {
        score = GetComponent<Scoring>();
        hasPayloadReached = false;
        hasPlayerReached = false;
        hasScoreAchievementsBeenUpdated = false;
        hasPreviousHeadshotValueBeenReceived = false;
        hasLevelCompletionAchivementBeenAwarded = false;
        has100HeadShotsAchivementBeenAwarded = false;
        hasHighScoreBeenPosted = false;
    }

    private void GetPreviousNumberOfHeadshots()
    {
        if (/*!GS.Available && */!GS.Authenticated)
        {
            Debug.Log("Achivement not awarded as user not logged in");
            hasPreviousHeadshotValueBeenReceived = true;
            return;
        }
        new LogEventRequest()
            .SetEventKey("geths")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Received Player Data From GameSparks...");
                    GSData data = response.ScriptData.GetGSData("player_Data");
                    previousHeadshots = data != null ? data.GetInt("playerHeadshots") ?? 0 : 0;
                    hasPreviousHeadshotValueBeenReceived = true;
                }
                else
                {
                    Debug.Log("Error Loading Player Data...");
                }
            });

    }
    private void SetCurrentNumberOfHeadshotsAndAwardAchievement()
    {
        if (/*!GS.Available &&*/ !GS.Authenticated)
        {
            Debug.Log("Achivement not awarded as user not logged in");
            has100HeadShotsAchivementBeenAwarded = true;
            return;
        }
        int totalHeadShots = GameManager.Instance.headShots + (previousHeadshots ?? 0);
        new LogEventRequest()
            .SetEventKey("seths")
            .SetEventAttribute("nhs", totalHeadShots)
            .Send((response) =>
            {
                Debug.Log(!response.HasErrors ? "Player Saved To GameSparks..." : "Error Saving Player Data...");
            });
        if (totalHeadShots >= 100)
        {
            new LogEventRequest()
           .SetEventKey("aatop")
           .SetEventAttribute("acode", "hs100")
           .Send((response) =>
           {
               Debug.Log(!response.HasErrors ? "Achivement Awarded Successfully..." : "Error Awarding Achivement...");
               has100HeadShotsAchivementBeenAwarded = true;
           });
        }
    }
    private void AwardLevelCompletedAchievement()
    {
        if (/*!GS.Available &&*/ !GS.Authenticated)
        {
            Debug.Log("Achivement not awarded as user not logged in");
            hasLevelCompletionAchivementBeenAwarded = true;
            return;
        }
        if (!(GameManager.Instance.CurrentLevel > -1 && GameManager.Instance.CurrentLevel < 5)) return;
        new LogEventRequest()
            .SetEventKey("aatop")
            .SetEventAttribute("acode", GameManager.Instance.AchievementCode)
            .Send((response) =>
            {
                Debug.Log(!response.HasErrors ? "Achivement Awarded Successfully..." : "Error Awarding Achivement...");
                hasLevelCompletionAchivementBeenAwarded = true;
            });
    }

    private void SendHighScoreToGs()
    {
        if (/*!GS.Available &&*/ !GS.Authenticated)
        {
            Debug.Log("Score not updated on the server as user not logged in");
            hasHighScoreBeenPosted = true;
            return;
        }
        new LogEventRequest()
            .SetEventKey(GameManager.Instance.EventKeyShortCode)
            .SetEventAttribute(GameManager.Instance.EventAttributeShortCodeHighScore, GameManager.Instance.TotalScore)
            .SetEventAttribute(GameManager.Instance.EventAttributeShortCodeCurrentScore, GameManager.Instance.TotalScore)
            .Send((response) =>
            {
                Debug.Log(!response.HasErrors ? "High Score Posted Sucessfully..." : "Error Posting High Score...");
                GameManager.Instance.CurrentPlayerRank = response.ScriptData.GetLong("currentPlayerRank") ?? 0;
                GameManager.Instance.CurrentPlayerDisplay = response.ScriptData.GetString("currentPlayerDisplayName");
                hasHighScoreBeenPosted = true;
            });
    }

    private void Update()
    {
        if (!hasPlayerReached || !hasPayloadReached) return;
        if (!hasScoreAchievementsBeenUpdated)
        {
            score.Score();
            SendHighScoreToGs();
            AwardLevelCompletedAchievement();
            GetPreviousNumberOfHeadshots();
            hasScoreAchievementsBeenUpdated = true;
        }
        if (!hasPreviousHeadshotValueBeenReceived) return;
        SetCurrentNumberOfHeadshotsAndAwardAchievement();
        if (!has100HeadShotsAchivementBeenAwarded && !hasLevelCompletionAchivementBeenAwarded && !hasHighScoreBeenPosted) return;
        if (Popup.IsPopBeingDisplayed != 0) return;
        GameManager.Instance.win_Lose = true;
        GameManager.Instance.win_Lose_Message = "Target Reached!";
        GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
        GameManager.Instance.GoToWinLoseScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasPlayerReached = true;
        }

        if (other.gameObject.name == "WinTriggerDetectionCollider")
        {
            hasPayloadReached = true;
        }

    }
}

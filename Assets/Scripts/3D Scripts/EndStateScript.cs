using UnityEngine;
using GameSparks.Core;

public class EndStateScript : MonoBehaviour
{
    Scoring score;
    bool hasPayloadReached;
    bool hasPlayerReached;

    void Start()
    {
        score = GetComponent<Scoring>();
        hasPayloadReached = false;
        hasPlayerReached = false;
    }

    void ShowEndScreen()
    {
        score.Score();
        SendHighScoreToGS();
        GameManager.Instance.win_Lose = true;
        GameManager.Instance.win_Lose_Message = "Target Reached!";
        GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
        GameManager.Instance.GoToWinLoseScene();
    }

    void SendHighScoreToGS()
    {
        if (!GS.Available && !GS.Authenticated)
        {
            Debug.Log("Score not updated on the server as user not logged in");
            return;
        }
        Debug.Log("Posting High Score To Leaderboard...");
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey(GameManager.Instance.EventKeyShortCode)
            .SetEventAttribute(GameManager.Instance.EventAttributeShortCodeHighScore, GameManager.Instance.TotalScore)
            .Send((response) =>
            {
                Debug.Log(!response.HasErrors ? "High Score Posted Sucessfully..." : "Error Posting High Score...");        
            });
        Debug.Log("Posting Current Score To Leaderboard...");
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey(GameManager.Instance.EventKeyShortCode)
            .SetEventAttribute(GameManager.Instance.EventAttributeShortCodeCurrentScore, GameManager.Instance.TotalScore)
            .Send((response) =>
            {
                Debug.Log(!response.HasErrors ? "Current Score Posted Sucessfully..." : "Error Posting Current Score...");
            });
    }

    void Update()
    {
        if (hasPlayerReached && hasPayloadReached)
        {
            ShowEndScreen();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasPlayerReached = true;
        }

        if (other.gameObject.name == "WinTriggerDetectionCollider")
        {
            hasPayloadReached = true;
        }

    }
}

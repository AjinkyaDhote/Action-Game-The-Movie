using UnityEngine;
using System.Collections;

public class EndStateScript : MonoBehaviour
{    
    Scoring score;
    bool hasPayloadReached;
    bool hasPlayerReached;

	void Start ()
    {
        score = GetComponent<Scoring>();
        hasPayloadReached = false;
        hasPlayerReached = false;
	}

    void ShowEndScreen()
    {
        score.Score();
        GameManager.Instance.win_Lose = true;
        GameManager.Instance.win_Lose_Message = "Target Reached!";
        GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
        GameManager.Instance.GoToWinLoseScene();
    }


    void Update()
    {
        if(hasPlayerReached && hasPayloadReached)
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

        if(other.gameObject.name == "WinTriggerDetectionCollider")
        {
            hasPayloadReached = true;
        }

    }     
}

using UnityEngine;
using System.Collections;

public class EndStateScript : MonoBehaviour {

    public GameObject payLoad;

    Scoring score;	


	void Start ()
    {
        score = GetComponent<Scoring>();
	}

    void ShowEndScreen()
    {
        score.Score();
        GameManager.Instance.win_Lose = true;
        GameManager.Instance.win_Lose_Message = "Target Reached!";
        GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
        GameManager.Instance.GoToWinLoseScene();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NewPayload")
        {
            ShowEndScreen();
        }
    }
     
}

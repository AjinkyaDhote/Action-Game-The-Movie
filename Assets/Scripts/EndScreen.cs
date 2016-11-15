using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

    Scoring Score;

    // Use this for initialization
    void Start ()
    {
        Score = GetComponent<Scoring>();
    }

    public void ShowEndScreen()
    {
        Score.Score();
        GameManager.Instance.win_Lose = true;
        GameManager.Instance.win_Lose_Message = "Target Reached!";
        GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
        GameManager.Instance.GoToWinLoseScene();
    }
}


using UnityEngine;

public class WinTrigger : MonoBehaviour {

    //public Scoring m_scoring;
    public Scoring Score;
    [HideInInspector]
    public bool hasPlayerReached = false;
    [HideInInspector]
    public bool hasPayloadReached = false;

    void Start()
    {

        Score = GetComponent<Scoring>();
        //Debug.Log("WinTrigger POsition"+ gameObject.transform.position);
    }
    void Update()
    {

        if (hasPlayerReached  && hasPayloadReached)
        {
            Score.Score();
            GameManager.Instance.win_Lose = true;
            GameManager.Instance.win_Lose_Message = "Target Reached!";
            GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
            GameManager.Instance.GoToWinLoseScene();
        }
    }
}

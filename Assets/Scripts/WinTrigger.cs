
using UnityEngine;

public class WinTrigger : MonoBehaviour {

    //public Scoring m_scoring;
    public GameObject Score;


    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PayLoad"));
        {
            //Score.GetComponent<Scoring>().Score();

            GameManager.Instance.win_Lose = true;
            GameManager.Instance.win_Lose_Message = "Target Reached!";
            //GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
            GameManager.Instance.GoToWinLoseScene();
        }
    }

}


using UnityEngine;

public class WinTrigger : MonoBehaviour {

    //public Scoring m_scoring;
    public GameObject Score;
    byte didEveryoneReach = 0;

    void Start()
    {
        //Debug.Log("WinTrigger POsition"+ gameObject.transform.position);
    }
    void Update()
    {
        if (didEveryoneReach == 2)
        {
            //Score.GetComponent<Scoring>().Score();
            GameManager.Instance.win_Lose = true;
            GameManager.Instance.win_Lose_Message = "Target Reached!";
            //GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
            GameManager.Instance.GoToWinLoseScene();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NewPayload") || other.CompareTag("Player"))
        {
            didEveryoneReach++;
        }
    }

}

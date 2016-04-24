using UnityEngine;
using System.Collections;
using UnityEditor;

public class WinTrigger : MonoBehaviour {

    //public Scoring m_scoring;
    public GameObject Score;


    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Score.GetComponent<Scoring>().Score();

            GameManager.Instance.win_Lose = true;
            GameManager.Instance.win_Lose_Message = "You Win!";
            GameManager.Instance.GoToWinLoseScene();
        }
    }

}

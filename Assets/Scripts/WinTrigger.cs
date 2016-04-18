using UnityEngine;
using System.Collections;

public class WinTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.win_Lose = true;
            GameManager.Instance.win_Lose_Message = "You Win!";
            GameManager.Instance.GoToWinLoseScene();
        }
    }
}

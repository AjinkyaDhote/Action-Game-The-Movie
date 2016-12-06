using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

    public Transform endState;

    Scoring Score;
    GameObject payload;    
    public bool isDoorOpen;

    // Use this for initialization
    void Start ()
    {
        Score = GetComponent<Scoring>();
        payload = GameObject.FindGameObjectWithTag("NewPayload");
        isDoorOpen = false;
    }

    public void ShowEndScreen()
    {
        //Score.Score();
        //GameManager.Instance.win_Lose = true;
        //GameManager.Instance.win_Lose_Message = "Target Reached!";
        //GameManager.Instance.currentMenuState = GameManager.MenuState.SCORE_BOARD;
        //GameManager.Instance.GoToWinLoseScene();
    }

    void Update()
    {
        if(isDoorOpen)
        {
            payload.transform.forward = Vector3.Lerp(payload.transform.forward, (endState.transform.position - payload.transform.position), Time.deltaTime * 0.2f);
            //Debug.Log(Vector3.Angle(payload.transform.forward , (endState.transform.position - payload.transform.position)));
            payload.transform.Translate((endState.transform.position - payload.transform.position).normalized * 5f * Time.deltaTime, Space.World);
        }
    }  

    public void SetDoorOpen()
    {
        isDoorOpen = true;
    }
}

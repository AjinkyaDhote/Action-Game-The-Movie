using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogManager2DLevel1 : MonoBehaviour
{
    private Text robotText, batteryText;
    private float typeTime = 0.1f;
    private float dialoguePause = 0.5f;

    string[] robotString = { "Ramesh", "How are you ?" };
    string[] batterString = { "Suresh", "I am fine." };

    bool stringDisplayInProgress = false;
    bool batteryTurn = true;
    uint dialog = 2;
    uint totalDialogs = 2;
	
	void Start ()
    {
        //gameObject.SetActive(false);
        robotText = transform.FindChild("Background").transform.FindChild("Actor1Text").GetComponent<Text>();
        batteryText = transform.FindChild("Background").transform.FindChild("Actor2Text").GetComponent<Text>();

        //robotText.text = "Suresh";
        //batteryText.text = "Ramesh";


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!stringDisplayInProgress)
            {
                dialog = 0;
                stringDisplayInProgress = true;
                batteryTurn = true;
                StartCoroutine(playDialog(robotString[dialog], robotText));
            }
        }

        if ( dialog < totalDialogs)
        {
            if (!stringDisplayInProgress && batteryTurn)
            {
                stringDisplayInProgress = true;
                batteryTurn = false;
                StartCoroutine(playDialog(batterString[dialog], batteryText));
                dialog++;
            }
            else if (!stringDisplayInProgress && !batteryTurn)
            {
                stringDisplayInProgress = true;
                batteryTurn = true;
                StartCoroutine(playDialog(robotString[dialog], robotText));
            }
        }
    }

    private IEnumerator playDialog(string i_string, Text i_text)
    {
        int stringLen = i_string.Length;
        int currentLen = 0;

        i_text.text = "";

        while (currentLen < stringLen)
        {
            i_text.text += i_string[currentLen];
            currentLen++;

            if (currentLen < stringLen)
            {
                yield return new WaitForSeconds(typeTime);
            }
            else
            {
                break;
            }
        }

        yield return new WaitForSeconds(dialoguePause);
        stringDisplayInProgress = false;
    }
}
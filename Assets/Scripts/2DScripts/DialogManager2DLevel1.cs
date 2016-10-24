using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class DialogManager2DLevel1 : MonoBehaviour
{
    public GameObject tutorialManager;

    private Text robotText, batteryText;
    private Button nextBt;
    private float typeTime = 0.03f;
    private float dialoguePause = 0.5f;

    private List<string> robotString; 
    private List<string> batterString;

    private bool stringDisplayInProgress = false;
    private bool batteryTurn = true;
    int dialog;
    int totalDialogs;
    private uint conversationsCompleted = 0;
	
	void Awake ()
    {
        //gameObject.SetActive(false);
        robotText = transform.FindChild("Background").transform.FindChild("Actor1Text").GetComponent<Text>();
        batteryText = transform.FindChild("Background").transform.FindChild("Actor2Text").GetComponent<Text>();
        nextBt = transform.FindChild("Background").transform.FindChild("NextBt").GetComponent<Button>();
        nextBt.gameObject.SetActive(false);

        robotString = new List<string>();
        batterString = new List<string>();
        //playCutScene();
    }

    public void playCutScene0()
    {
        robotString.Clear();
        robotString.Add("Do you have any plans ?");
        robotString.Add("Excellent. I will try to plan a path. You stick to it.");
        robotString.Add("The same goes with me. Dont worry I will take care of that.");
        robotString.Add("Ok. Lets move.");

        batterString.Clear();
        batterString.Add("I have an overhead layout of the current area");
        batterString.Add("Sure!! Keep in mind that I am battery hungry.");
        batterString.Add("It appears there is a battery in that section. Lets pick that up.");
        batterString.Add("Stay close to me. You are also powered on my battery.");

        totalDialogs = batterString.Count;

        //if (!stringDisplayInProgress)
        {
            dialog = 0;
            stringDisplayInProgress = true;
            batteryTurn = true;
            nextBt.gameObject.SetActive(false);
            conversationsCompleted = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(playDialog(robotString[dialog], robotText));
        }
    }

    public void playCutScene1()
    {
        robotString.Clear();
        robotString.Add("Oh!! I see another one.");
        
        batterString.Clear();
        batterString.Add("Great. Lets grab it.");
        
        totalDialogs = batterString.Count;

        //if (!stringDisplayInProgress)
        {
            dialog = 0;
            stringDisplayInProgress = true;
            batteryTurn = true;
            nextBt.gameObject.SetActive(false);
            conversationsCompleted = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(playDialog(robotString[dialog], robotText));
        }
    }

    public void playCutScene2()
    {
        robotString.Clear();
        robotString.Add("Oh!! Did something go wrong?");
        robotString.Add("Ok. I will try to re-plan.");

        batterString.Clear();
        batterString.Add("My system tells me that I will run out of battery if we take this path.");
        batterString.Add("Cool....  Undo using RMB");

        totalDialogs = batterString.Count;

        //if (!stringDisplayInProgress)
        {
            dialog = 0;
            stringDisplayInProgress = true;
            batteryTurn = true;
            nextBt.gameObject.SetActive(false);
            conversationsCompleted = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(playDialog(robotString[dialog], robotText));
        }
    }

    public void playCutScene3()
    {
        robotString.Clear();
        robotString.Add("Hey I see some ammo.");
        robotString.Add("Thats awesome. I will remember to pick them up.");

        batterString.Clear();
        batterString.Add("Yes. My system tells me that the yellow boxes are near our planned path.");
        batterString.Add("Great.");

        totalDialogs = batterString.Count;

        //if (!stringDisplayInProgress)
        {
            dialog = 0;
            stringDisplayInProgress = true;
            batteryTurn = true;
            nextBt.gameObject.SetActive(false);
            conversationsCompleted = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(playDialog(robotString[dialog], robotText));
        }
    }

    public void playCutScene4()
    {
        robotString.Clear();
        robotString.Add("I see there are two options ahead. Which one should we take?");

        batterString.Clear();
        batterString.Add("We don't know the dangers ahead. Think and choose carefully.");

        totalDialogs = batterString.Count;

        //if (!stringDisplayInProgress)
        {
            dialog = 0;
            stringDisplayInProgress = true;
            batteryTurn = true;
            nextBt.gameObject.SetActive(false);
            conversationsCompleted = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(playDialog(robotString[dialog], robotText));
        }

    }

    public void playCutScene5()
    {
        robotString.Clear();
        robotString.Add("Hey!! We are at crossroads again.");
        robotString.Add("Should we take the other side then?");

        batterString.Clear();
        batterString.Add("My sensors are recording heavy meca activity in the violet zone.");
        batterString.Add("Did we take enough ammo. If not we should take the safer route.");

        totalDialogs = batterString.Count;

        //if (!stringDisplayInProgress)
        {
            dialog = 0;
            stringDisplayInProgress = true;
            batteryTurn = true;
            nextBt.gameObject.SetActive(false);
            conversationsCompleted = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(playDialog(robotString[dialog], robotText));
        }
    }

    public void playNextConversation()
    {
        conversationsCompleted = 0;
        nextBt.gameObject.SetActive(false);
        robotText.text = "";
        batteryText.text = "";

        if ( totalDialogs == dialog )
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            tutorialManager.GetComponent<TutorialManager2D>().Resume();
        }
    }

    void Update()
    {
        if (!stringDisplayInProgress && conversationsCompleted == 2)
        {
            nextBt.gameObject.SetActive(true);
        }

        if ( dialog < totalDialogs && conversationsCompleted < 2)
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
        conversationsCompleted++;
    }
}
using System.Collections;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public MenuManager menuManager;
    public MainMenuCamControl cameraControl;
    public Transform mainMount;
    private InputField userName;
    private Text messageText;
    private bool isCoroutineCalled;
    private void Start()
    {
        isCoroutineCalled = false;
        userName = transform.parent.FindChild("UserNameIF").GetComponent<InputField>();
        userName.readOnly = false;
        messageText = transform.parent.parent.FindChild("MessageT").GetChild(0).GetComponent<Text>();
        messageText.text = "";
        userName.text = PlayerPrefs.GetString("previousPlayerLoggedIn");
    }
    public void LoginButton()
    {
        if (!GS.Available)
        {
            messageText.text = "GameSparks is not available... \nCannot login at this time";
            return;
        }
        if (userName.text.Length == 0)
        {
            messageText.text = "The username cannot be empty";
            return;
        }
        PlayerPrefs.SetString("previousPlayerLoggedIn", userName.text);
        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(userName.text)
            .SetPassword("1")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    messageText.text = "Player Authenticated... \n User Name: " + response.DisplayName;
                    if (!isCoroutineCalled)
                    {
                        StartCoroutine(MoveCameraToStartCanvas());
                        isCoroutineCalled = true;
                        userName.readOnly = true;
                    }
                }
                else
                {
                    messageText.text = response.Errors.JSON.ToString();
                }
            });
    }

    private IEnumerator MoveCameraToStartCanvas()
    {
        yield return new WaitForSeconds(2);
        cameraControl.setMount(mainMount);
        menuManager.setMenuStateToMainMenu();
        isCoroutineCalled = false;
        messageText.text = "";
    }
}

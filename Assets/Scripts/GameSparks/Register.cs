using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    private InputField userName;
    private Text messageText;

    private void Start()
    {
        userName = transform.parent.FindChild("UserNameIF").GetComponent<InputField>();
        messageText = transform.parent.parent.FindChild("MessageT").GetChild(0).GetComponent<Text>();
        messageText.text = "";
    }
    public void RegisterButton()
    {
        if (!GS.Available)
        {
            messageText.text = "GameSparks is not available... \nCannot register at this time";
            return;
        }
        if (userName.text.Length == 0)
        {
            messageText.text = "The username cannot be empty";
            return;
        }
        new GameSparks.Api.Requests.RegistrationRequest()
            .SetDisplayName(userName.text)
            .SetUserName(userName.text)
            .SetPassword("1")
            .Send((response) =>
            {
                messageText.text = (!response.HasErrors) ? "Player Registered \n User Name: " + response.DisplayName : response.Errors.JSON.ToString();
            });
    }

}

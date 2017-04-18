using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Messages;

public class Popup : MonoBehaviour
{
    public static int IsPopBeingDisplayed { get; private set; }
    private GameObject popupPrefab;

    private void Awake()
    {
        popupPrefab = Resources.Load<GameObject>("PopupPrefab/Popup");
    }
    private void OnEnable()
    {
        AchievementEarnedMessage.Listener += AchievementMessageHandler;
    }

    private void OnDisable()
    {
        AchievementEarnedMessage.Listener -= AchievementMessageHandler;
    }

    private void AchievementMessageHandler(AchievementEarnedMessage message)
    {
        IsPopBeingDisplayed++;
        GameObject popupInstance = Instantiate(popupPrefab, transform.position, transform.rotation, transform);
        Image achivementImage = popupInstance.GetComponent<Image>();
        Text achivementText = popupInstance.transform.GetChild(0).GetComponent<Text>();
        Sprite sprite;
        GameManager.BadgesAchieved.TryGetValue(message.AchievementShortCode.GetHashCode(), out sprite);
        if (sprite != null) achivementImage.sprite = sprite;
        achivementText.text = message.AchievementName;
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3);
        IsPopBeingDisplayed--;
    }
}

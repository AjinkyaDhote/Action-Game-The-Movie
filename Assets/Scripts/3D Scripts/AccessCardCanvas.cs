using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AccessCardCanvas : MonoBehaviour
{
    private static Text numberOfCards;
    private static Text pickedMessage;
    private void Start()
    {
        if (!GameManager.Instance.EnableKeyCardCounter)
        {
            gameObject.SetActive(false);
        }
        if (!gameObject.activeSelf) return;
        numberOfCards = transform.GetChild(0).GetComponent<Text>();
        numberOfCards.text = "0";
        pickedMessage = transform.GetChild(1).GetComponent<Text>();
        pickedMessage.gameObject.SetActive(false);
    }

    public void ShowMessage()
    {
        pickedMessage.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }
    public static void UpdateNumberOfCards()
    {
        numberOfCards.text = LevelManager3D.accessCardCount.ToString();
    }
    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2);
        pickedMessage.gameObject.SetActive(false);
    }
}

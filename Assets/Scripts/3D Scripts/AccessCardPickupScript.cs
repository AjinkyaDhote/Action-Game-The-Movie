using UnityEngine;

public class AccessCardPickupScript : MonoBehaviour
{
    private AccessCardCanvas accessCardCanvas;

    private void Awake()
    {
        accessCardCanvas =
            GameObject.FindGameObjectWithTag("Player")
                .transform.GetChild(0)
                .GetChild(0)
                .FindChild("FPS UI Canvas")
                .FindChild("AccessCard")
                .GetComponent<AccessCardCanvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NewPayload")) return;
        LevelManager3D.accessCardCount++;
        AccessCardCanvas.UpdateNumberOfCards();
        accessCardCanvas.ShowMessage();
        gameObject.SetActive(false);
    }
}

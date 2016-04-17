using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void PlanGame()
    {
        GameManager.Instance.PlanGame();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}

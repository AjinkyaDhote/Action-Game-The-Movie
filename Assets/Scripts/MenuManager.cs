using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform MainMount, LevelMount, InGameMount;
    public Camera cam;

    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MainMenuCamControl mainMenuCamControl = cam.GetComponent<MainMenuCamControl>();

        if ( GameManager.Instance.currentMenuState == GameManager.MenuState.MAIN_MENU )
        {
            mainMenuCamControl.setMount( MainMount );
        }
        else if (GameManager.Instance.currentMenuState == GameManager.MenuState.LEVEL_MENU)
        {
            mainMenuCamControl.setMount( LevelMount );
        }
        else if (GameManager.Instance.currentMenuState == GameManager.MenuState.IN_GAME_MENU)
        {
            mainMenuCamControl.setMount(InGameMount);
        }
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

    public void setLevel( int level )
    {
        GameManager.Instance.setCurrentLevel(level);
    }

    public void setMenuStateToLevel()
    {
        GameManager.Instance.currentMenuState = GameManager.MenuState.LEVEL_MENU;
    }
}

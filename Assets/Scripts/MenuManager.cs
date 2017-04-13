using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Transform NetworkMount, MainMount, LevelMount, InGameMount, ScoreMount, LeaderboardMount, AchievementMount;
    public Camera cam;
    public Button playButton;
    public InputField loginInputField;

    public Text HeadShots, EnemiesKilled, Accuracy, Health, DistanceCoverd, Total;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.currentMenuState == GameManager.MenuState.IN_GAME_MENU)
            {
                cam.GetComponent<MainMenuCamControl>().setMount(LevelMount);
                setMenuStateToLevel();
            }
            else if (GameManager.Instance.currentMenuState == GameManager.MenuState.LEVEL_MENU)
            {
                cam.GetComponent<MainMenuCamControl>().setMount(NetworkMount);
                setMenuStateToMainMenu();
            }
        }
    }

    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = false;
        Cursor.visible = true;

        GameManager.Instance.countDownDone = false;

        MainMenuCamControl mainMenuCamControl = cam.GetComponent<MainMenuCamControl>();

        if (GameManager.Instance.currentMenuState == GameManager.MenuState.MAIN_MENU)
        {
            mainMenuCamControl.setMount(NetworkMount);
        }
        else if (GameManager.Instance.currentMenuState == GameManager.MenuState.LEVEL_MENU)
        {
            mainMenuCamControl.setMount(LevelMount);
        }
        else if (GameManager.Instance.currentMenuState == GameManager.MenuState.IN_GAME_MENU)
        {
            mainMenuCamControl.setMount(InGameMount);
        }
        else if (GameManager.Instance.currentMenuState == GameManager.MenuState.SCORE_BOARD)
        {
            HeadShots.text = GameManager.Instance.headShots.ToString();
            EnemiesKilled.text = GameManager.Instance.totalEnemiesKilled.ToString();
            Accuracy.text = GameManager.Instance.accuracy.ToString();
            Health.text = GameManager.Instance.remainingHealth.ToString();
            DistanceCoverd.text = GameManager.Instance.totalDistance.ToString();
            Total.text = GameManager.Instance.TotalScore.ToString();

            mainMenuCamControl.setMount(ScoreMount);
        }


        if (GameManager.Instance.playAvailable == true)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
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

    public void setLevel(int level)
    {
        GameManager.Instance.currentMenuState = GameManager.MenuState.IN_GAME_MENU;
        GameManager.Instance.setCurrentLevel(level);
        GameManager.Instance.playAvailable = false;
        playButton.interactable = false;

        if (GameManager.Instance.GOD_MODE && level == 1)
        {
            playButton.interactable = true;
        }
    }

    public void setMenuStateToLevel()
    {
        GameManager.Instance.currentMenuState = GameManager.MenuState.LEVEL_MENU;
    }

    public void setMenuStateToMainMenu()
    {
        GameManager.Instance.currentMenuState = GameManager.MenuState.MAIN_MENU;
        loginInputField.readOnly = false;
    }

    public void ShowLevelMenu()
    {
        GameManager.Instance.currentMenuState = GameManager.MenuState.LEVEL_MENU;
    }
}

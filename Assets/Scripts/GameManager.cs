using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public variables
    public List<Vector2> mapPoints; // these are image coordinates
    public List<int> distanceTravelled;
    //public List<Vector3> BatteryPos;
    public List<int> batteryUsedList;
    public List<int> batteryPickups; public List<int> ammoPickups;
    public List<int> batteryPickupsCount; public List<int> ammoPickupsCount;
    public List<GameObject> BatteriesHitList; public List<GameObject> ammosHitList;

    public List<Vector2> batteryPosList;
    public List<Vector2> ammoPosList;

    public int battery = 100;
    public int batteryDepletionRate = 5;
    public int headShots;
    public int totalEnemiesKilled;

    public float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    [HideInInspector]
    public bool win_Lose = false;
    [HideInInspector]
    public string win_Lose_Message = null;

    // private variables
    private static GameManager _instance = null;
    private enum Levels { MENU = 1, Scene2D_1, Scene3D_1, GameWinLose };
    private enum GameStates { MENU, PLAN_GAME, PLAY_GAME, GAME_OVER };
    private GameStates currentState = GameStates.MENU;
    int batteryCount;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentState = GameStates.MENU;
        SceneManager.LoadScene((int)Levels.MENU);
        batteryCount = 0;
    }

    public void PlayGame()
    {
        currentState = GameStates.PLAY_GAME;
        SceneManager.LoadScene((int)Levels.Scene3D_1);
    }

    public void PlanGame()
    {
        currentState = GameStates.PLAN_GAME;
        SceneManager.LoadScene((int)Levels.Scene2D_1);
    }

    public void GoToMenu()
    {
        currentState = GameStates.MENU;
        SceneManager.LoadScene((int)Levels.MENU);
    }

    public void GoToWinLoseScene()
    {
        currentState = GameStates.GAME_OVER;
        SceneManager.LoadScene((int)Levels.GameWinLose);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (currentState != GameStates.MENU && currentState != GameStates.PLAY_GAME)
            {
                currentState = GameStates.MENU;
                SceneManager.LoadScene((int)Levels.MENU);
                for (int i = 0; i < batteryPickupsCount.Count; i++)
                {
                    batteryCount += batteryPickupsCount[i];
                }
                Debug.Log(batteryCount);
            }
        }
    }
}
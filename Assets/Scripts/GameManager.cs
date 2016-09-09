using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentLevel;
    public enum MenuState { MAIN_MENU, LEVEL_MENU, IN_GAME_MENU, SCORE_BOARD };

    public bool playAvailable;

    public MenuState currentMenuState;

    // public variables
    public List<Vector2> mapPoints; // these are image coordinates
    public List<int> distanceTravelled;
    //public List<Vector3> BatteryPos;
    public List<int> batteryUsedList;
    public List<int> batteryPickups; //public List<int> ammoPickups;
    public List<int> batteryPickupsCount; //public List<int> ammoPickupsCount;
    public List<GameObject> BatteriesHitList; //public List<GameObject> ammosHitList;

    public List<Vector2> batteryPosList;
    public List<Vector2> ammoPosList;
    
    public int battery = 100;
    public int batteryDepletionRate = 5;

    //Score Board..................................................
    public int headShots;
    public int totalEnemiesKilled;
    public int remainingHealth;
    public int totalDistance;
    public int TotalScore;
    //........................................................
    public int batteryCount;

    public float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    [HideInInspector]
    public bool win_Lose = false;
    [HideInInspector]
    public string win_Lose_Message = null;
    [HideInInspector]
    //public int totalAmmoCollected;

    // private variables
    private static GameManager _instance = null;
    private enum Levels { MENU = 1, Scene2D_1, Scene3D_1, Scene2D_2, Scene3D_2, GameWinLose };
    private enum GameStates { MENU, PLAN_GAME, PLAY_GAME, GAME_OVER };
    private GameStates currentState = GameStates.MENU;
    

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
        headShots = 0;
        totalEnemiesKilled = 0;
        remainingHealth = 0;
        totalDistance = 0;
        TotalScore = 0;

        playAvailable = false;
        currentMenuState = MenuState.MAIN_MENU;

        DontDestroyOnLoad(gameObject);
        currentState = GameStates.MENU;     
        SceneManager.LoadScene((int)Levels.MENU);
        batteryCount = 0;
    }

    public void PlayGame()
    {
        currentState = GameStates.PLAY_GAME;
        //totalAmmoCollected = ammoPickups.Count * 10 + 75;

        if ( currentLevel == 1 )
        {
            SceneManager.LoadScene((int)Levels.Scene3D_1);
        }
        else if (currentLevel == 2)
        {
            SceneManager.LoadScene((int)Levels.Scene3D_2);
        }
    }

    public void PlanGame()
    {
        currentState = GameStates.PLAN_GAME;

        if ( currentLevel == 1 )
        {
            SceneManager.LoadScene((int)Levels.Scene2D_1);
        }
        else if ( currentLevel == 2 )
        {
            SceneManager.LoadScene((int)Levels.Scene2D_2);
        }
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

    public void setCurrentLevel( int level )
    {
        currentLevel = level;
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
            }
        }
    }
}
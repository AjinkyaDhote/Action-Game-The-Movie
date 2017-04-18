using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentLevel;
    public enum MenuState { MAIN_MENU, LEVEL_MENU, IN_GAME_MENU, SCORE_BOARD };

    public bool countDownDone = false;
    public bool infoDialogue = false;

    public bool playAvailable;

    public MenuState currentMenuState;

    // public variables
    public List<Vector2> mapPoints; // these are image coordinates
    public List<int> distanceTravelled;
    //public List<Vector3> BatteryPos;
    public List<int> batteryUsedList;
    public List<int> batteryPickups; //public List<int> ammoPickups;
    public List<int> batteryPickupsCount; //public List<int> ammoPickupsCount;
    public Stack<GameObject> BatteriesHitList; //public List<GameObject> ammosHitList;

    public List<Vector2> batteryPosList;
    public List<Vector2> ammoPosList;
    public List<Vector2> keyPosList;
    [HideInInspector]
    public int battery = 100;

    [HideInInspector]
    public int batteryDepletionRate = 5;

    //Score Board..................................................
    [HideInInspector]
    public int headShots;
    [HideInInspector]
    public int totalEnemiesKilled;
    [HideInInspector]
    public int accuracy;
    [HideInInspector]
    public int remainingHealth;
    [HideInInspector]
    public int totalDistance;
    [HideInInspector]
    public int TotalScore;
    //........................................................
    [HideInInspector]
    public int batteryCount;
    [HideInInspector]
    public float shotsFired;
    [HideInInspector]
    public float bodyShots;
    [HideInInspector]
    public float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    private const bool _GOD_MODE = true;

    public bool GOD_MODE
    {
        get
        {
            return _GOD_MODE;
        }
    }

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

    [HideInInspector]
    public bool win_Lose = false;
    [HideInInspector]
    public string win_Lose_Message = null;

    [HideInInspector]
    public bool EnableKeyCardCounter = false;

    // private variables
    private static GameManager _instance = null;
    private enum Levels { MENU = 1, Scene2D_1, Scene3D_1, Scene2D_2, Scene3D_2, GameWinLose, Scene2D_tut, Scene3D_tut, Scene2D_3, Scene3D_3 };
    private enum GameStates { MENU, PLAN_GAME, PLAY_GAME, GAME_OVER };
    private GameStates currentGameState = GameStates.MENU;
    [HideInInspector]
    public string AchievementCode = "";
    [HideInInspector]
    public string LeaderBoardShortCode = "";
    [HideInInspector]
    public string EventKeyShortCode = "";
    [HideInInspector]
    public string EventAttributeShortCodeHighScore = "";
    [HideInInspector]
    public string EventAttributeShortCodeCurrentScore = "";
    public long CurrentPlayerRank { get; set; }
    public string CurrentPlayerDisplay { get; set; }
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

    public static Dictionary<int, Sprite> BadgesAchieved;
    public static Dictionary<int, Sprite> BadgesNotAchieved;
    void Start()
    {
        BadgesAchieved = new Dictionary<int, Sprite>
        {
            {"lvl1c".GetHashCode(), Resources.Load<Sprite>("Sprites/Achievements/achievement_level1_completed")},
            {"lvl2c".GetHashCode(), Resources.Load<Sprite>("Sprites/Achievements/achievement_level2_completed")},
            {"lvl3c".GetHashCode(), Resources.Load<Sprite>("Sprites/Achievements/achievement_level3_completed")}
        };
        BadgesNotAchieved = new Dictionary<int, Sprite>
        {
        };

        headShots = 0;
        totalEnemiesKilled = 0;
        accuracy = 0;
        remainingHealth = 0;
        totalDistance = 0;
        TotalScore = 0;

        BatteriesHitList = new Stack<GameObject>();

        playAvailable = false;

        shotsFired = 0;
        bodyShots = 0;
        if (_GOD_MODE)
        {
            playAvailable = true;
            mapPoints.Add(new Vector2(-0.8f, -384.1f));
            mapPoints.Add(new Vector2(-390.6f, -354.1f));
            mapPoints.Add(new Vector2(-390.6f, -239.9f));
            mapPoints.Add(new Vector2(-232.4f, -121.7f));
            mapPoints.Add(new Vector2(-412.7f, 0.5f));

            mapPoints.Add(new Vector2(-252.4f, 56.6f));
            mapPoints.Add(new Vector2(10.0f, 58.6f));
            mapPoints.Add(new Vector2(8.0f, 208.8f));
            mapPoints.Add(new Vector2(-296.5f, 202.8f));
            mapPoints.Add(new Vector2(-298.5f, 411.2f));

            mapPoints.Add(new Vector2(36.1f, 421.2f));

            width2DPlane = 910.0f;
            height2DPlane = 912.0f;

            ammoPosList.Add(new Vector2(294.0f, 209.2f));
            ammoPosList.Add(new Vector2(382.6f, -365.6f));
            ammoPosList.Add(new Vector2(-268.8f, -233.6f));
            ammoPosList.Add(new Vector2(243.1f, -205.7f));
            ammoPosList.Add(new Vector2(-174.5f, 122.4f));
            ammoPosList.Add(new Vector2(-302.5f, 326.6f));
            ammoPosList.Add(new Vector2(283.4f, 367.8f));
            ammoPosList.Add(new Vector2(166.1f, 122.0f));
            ammoPosList.Add(new Vector2(409.2f, -62.2f));

            distanceTravelled.Add(10);
            distanceTravelled.Add(3);
            distanceTravelled.Add(5);
            distanceTravelled.Add(6);
            distanceTravelled.Add(5);
            distanceTravelled.Add(7);
            distanceTravelled.Add(4);
            distanceTravelled.Add(8);
            distanceTravelled.Add(6);
            distanceTravelled.Add(9);

            batteryUsedList.Add(50);
            batteryUsedList.Add(15);
            batteryUsedList.Add(25);
            batteryUsedList.Add(30);
            batteryUsedList.Add(25);
            batteryUsedList.Add(35);
            batteryUsedList.Add(20);
            batteryUsedList.Add(40);
            batteryUsedList.Add(30);
            batteryUsedList.Add(45);

            batteryPosList.Add(new Vector2(-392.8f, -357.6f));
            batteryPosList.Add(new Vector2(276.8f, -273.5f));
            batteryPosList.Add(new Vector2(-234.7f, -125.6f));
            batteryPosList.Add(new Vector2(422.5f, 286.3f));
            batteryPosList.Add(new Vector2(10.6f, 202.6f));
            batteryPosList.Add(new Vector2(5.8f, 55.1f));
            batteryPosList.Add(new Vector2(-297.1f, 402.3f));

            batteryPickupsCount.Add(1);
            batteryPickupsCount.Add(0);
            batteryPickupsCount.Add(1);
            batteryPickupsCount.Add(0);
            batteryPickupsCount.Add(0);
            batteryPickupsCount.Add(1);
            batteryPickupsCount.Add(1);
            batteryPickupsCount.Add(0);
            batteryPickupsCount.Add(1);
            batteryPickupsCount.Add(0);

            batteryPickups.Add(50);
            batteryPickups.Add(50);
            batteryPickups.Add(50);
            batteryPickups.Add(50);
            batteryPickups.Add(50);

            battery = 100;

        }

        currentMenuState = MenuState.MAIN_MENU;

        DontDestroyOnLoad(gameObject);
        currentGameState = GameStates.MENU;
        SceneManager.LoadScene((int)Levels.MENU);
        batteryCount = 0;
    }

    public void PlayGame()
    {
        currentGameState = GameStates.PLAY_GAME;

        // tutorial level
        if (currentLevel == 0)
        {
            SceneManager.LoadScene((int)Levels.Scene3D_tut);
        }
        // Level 1
        else if (currentLevel == 1)
        {
            SceneManager.LoadScene((int)Levels.Scene3D_1);
        }
        // Level 2
        else if (currentLevel == 2)
        {
            SceneManager.LoadScene((int)Levels.Scene3D_2);
        }
        // Level 2
        else if (currentLevel == 3)
        {
            SceneManager.LoadScene((int)Levels.Scene3D_3);
        }
    }

    public void PlanGame()
    {
        currentGameState = GameStates.PLAN_GAME;

        if (currentLevel == 0)
        {
            SceneManager.LoadScene((int)Levels.Scene2D_tut);
        }
        else if (currentLevel == 1)
        {
            SceneManager.LoadScene((int)Levels.Scene2D_1);
        }
        else if (currentLevel == 2)
        {
            SceneManager.LoadScene((int)Levels.Scene2D_2);
        }
        else if (currentLevel == 3)
        {
            SceneManager.LoadScene((int)Levels.Scene2D_3);
        }
    }

    public void GoToMenu()
    {
        currentGameState = GameStates.MENU;
        SceneManager.LoadScene((int)Levels.MENU);
    }

    public void GoToWinLoseScene()
    {
        currentGameState = GameStates.GAME_OVER;
        SceneManager.LoadScene((int)Levels.GameWinLose);
    }

    public void ExitGame()
    {
        if (!Application.isEditor)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        //Application.Quit();
    }

    public void setCurrentLevel(int level)
    {
        currentLevel = level;
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (currentGameState != GameStates.MENU && currentGameState != GameStates.PLAY_GAME)
            {
                currentGameState = GameStates.MENU;
                SceneManager.LoadScene((int)Levels.MENU);
                for (int i = 0; i < batteryPickupsCount.Count; i++)
                {
                    batteryCount += batteryPickupsCount[i];
                }
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        EnableKeyCardCounter = scene.buildIndex == (int)Levels.Scene3D_3;
        switch (scene.buildIndex)
        {
            case (int)Levels.Scene3D_tut:
                AchievementCode = "tlvlc";
                break;
            case (int)Levels.Scene3D_1:
                AchievementCode = "lvl1c";
                LeaderBoardShortCode = "wwlb1";
                EventKeyShortCode = "SubmitScore1";
                EventAttributeShortCodeHighScore = "cpscore1";
                EventAttributeShortCodeCurrentScore = "cpscore11";
                break;
            case (int)Levels.Scene3D_2:
                AchievementCode = "lvl2c";
                LeaderBoardShortCode = "wwlb2";
                EventKeyShortCode = "SubmitScore2";
                EventAttributeShortCodeHighScore = "cpscore2";
                EventAttributeShortCodeCurrentScore = "cpscore22";
                break;
            case (int)Levels.Scene3D_3:
                AchievementCode = "lvl3c";
                LeaderBoardShortCode = "wwlb3";
                EventKeyShortCode = "SubmitScore3";
                EventAttributeShortCodeHighScore = "cpscore3";
                EventAttributeShortCodeCurrentScore = "cpscore33";
                break;
            default:
                break;
        }
    }
}
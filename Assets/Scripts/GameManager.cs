﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public variables
    public List<Vector2> mapPoints; // these are image coordinates
    //public List<Vector3> BatteryPos;
    public List<int> batteryUsedList;
    public List<int> batteryPickups; public List<int> ammoPickups;
    public List<int> batteryPickupsCount; public List<int> ammoPickupsCount;
    public List<GameObject> BatteriesHitList; public List<GameObject> ammosHitList;

    public List<Vector2> batteryPosList;
    public List<Vector2> ammoPosList;

    public int battery = 100;
    public int batteryDepletionRate = 5;

    public float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    // private variables
    private static GameManager _instance = null;
    private enum Levels { MENU = 1, Scene2D_1, Scene3D_1 };
    private enum GameStates { MENU, PLAN_GAME, PLAY_GAME };
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
        DontDestroyOnLoad(gameObject);
        currentState = GameStates.MENU;
        SceneManager.LoadScene((int)Levels.MENU);
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

    public void goToMenu()
    {
        currentState = GameStates.MENU;
        SceneManager.LoadScene((int)Levels.MENU);
    }

    public void ExitGame()
    {
        Debug.Log("Exit the game.");
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (currentState != GameStates.MENU)
            {
                currentState = GameStates.MENU;
                SceneManager.LoadScene((int)Levels.MENU);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoring : MonoBehaviour {


    //private int EnemiesKilled;
    //private int healthRemaining;
    //private int batteryRemaining; 
    //private int totalDistance;
    //private int headshots;
    public Text playerHealthText;
    //public Text ammoText;

    public int totalBattery;
    public int totalBatteryValue;
    public int batteryUsed;
    

    //private int remainingHealth;
    private int initialBattery;
    //private int initialAmmo;//..................................................

    // Use this for initialization
    void Start ()
    {
        //totalDistance = 0;
        GameManager.Instance.totalDistance = 0;
        totalBattery = 0;
        batteryUsed = 0;
        initialBattery = GameManager.Instance.battery;
        totalBatteryValue = 0 + initialBattery;

        

        Debug.Log("initialBattery");
        Debug.Log(initialBattery);
    }



    public void Score()
    {
        Debug.Log("Distances");
        for (int i = 0; i < GameManager.Instance.distanceTravelled.Count; i++)
        {
            Debug.Log(GameManager.Instance.distanceTravelled[i]);
            GameManager.Instance.totalDistance += GameManager.Instance.distanceTravelled[i];      
        }

        /*Debug.Log("Batteries");
        for (int i = 0; i < GameManager.Instance.batteryPickupsCount.Count; i++)
        {
            totalBattery += GameManager.Instance.batteryPickupsCount[i];
        }
        Debug.Log(totalBattery);
        */

        Debug.Log(GameManager.Instance.batteryCount);

        Debug.Log("TotalBatteryValue");
        for (int i = 0; i < GameManager.Instance.batteryPickups.Count; i++)
        {
            totalBatteryValue += GameManager.Instance.batteryPickups[i];
        }
        Debug.Log(totalBatteryValue);

        Debug.Log("BatteryUsed");
        for (int i = 0; i < GameManager.Instance.batteryUsedList.Count; i++)
        {
            batteryUsed += GameManager.Instance.batteryUsedList[i];
        }
        Debug.Log(batteryUsed);


        Debug.Log("Headshots");
        Debug.Log(GameManager.Instance.headShots);

        Debug.Log("Killed");
        Debug.Log(GameManager.Instance.totalEnemiesKilled);

        Debug.Log("HealthRemaining");
        GameManager.Instance.remainingHealth = System.Int32.Parse(playerHealthText.text);
        Debug.Log(GameManager.Instance.remainingHealth);

        Debug.Log("Score");
        GameManager.Instance.TotalScore = (GameManager.Instance.headShots / 10)*500 + (GameManager.Instance.totalEnemiesKilled / 10)*500 + (GameManager.Instance.remainingHealth / 100)*100 - (GameManager.Instance.totalDistance / 100);
        Debug.Log(GameManager.Instance.TotalScore);


    }
        // Update is called once per frame
        void Update ()
    {
	    
	}
}

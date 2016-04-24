using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoring : MonoBehaviour {

    public int totalDistance;
    public int totalBattery;
    public int totalBatteryValue;
    public int batteryUsed;

    private int initialBattery;

    // Use this for initialization
    void Start ()
    {
        totalDistance = 0;
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
            totalDistance += GameManager.Instance.distanceTravelled[i];
        }

        Debug.Log("Batteries");
        for (int i = 0; i < GameManager.Instance.batteryPickupsCount.Count; i++)
        {
            totalBattery += GameManager.Instance.batteryPickupsCount[i];
        }
        Debug.Log(totalBattery);

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

    }
        // Update is called once per frame
        void Update ()
    {
	    
	}
}

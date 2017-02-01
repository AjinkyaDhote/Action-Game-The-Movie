using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoring : MonoBehaviour {

    

    public int totalBattery;
    public int totalBatteryValue;
    public int batteryUsed;
    
    
    private int initialBattery;

    // Use this for initialization
    void Start ()
    {
        GameManager.Instance.totalDistance = 0;
        totalBattery = 0;
        batteryUsed = 0;
        initialBattery = GameManager.Instance.battery;
        totalBatteryValue = 0 + initialBattery;
        
        
    }



    public void Score()
    {
        for (int i = 0; i < GameManager.Instance.distanceTravelled.Count; i++)
        {
            GameManager.Instance.totalDistance += GameManager.Instance.distanceTravelled[i];      
        }
        
        for (int i = 0; i < GameManager.Instance.batteryPickups.Count; i++)
        {
            totalBatteryValue += GameManager.Instance.batteryPickups[i];
        }
        
        for (int i = 0; i < GameManager.Instance.batteryUsedList.Count; i++)
        {
            batteryUsed += GameManager.Instance.batteryUsedList[i];
        }
        GameManager.Instance.remainingHealth = GameObject.FindGameObjectWithTag("NewPayload").transform.FindChild("PayLoadHealthBar").GetComponent<PayLoadHealthScript>().payLoadHealth;

        GameManager.Instance.accuracy = (int)(((float)GameManager.Instance.totalEnemiesKilled/ GameManager.Instance.shotsFired) * 100) + (int)(((float)GameManager.Instance.headShots/ GameManager.Instance.shotsFired) * 2 * 100);
        
        GameManager.Instance.TotalScore = (GameManager.Instance.headShots)*100 + (GameManager.Instance.totalEnemiesKilled)*100 + (GameManager.Instance.accuracy) * 10 + (GameManager.Instance.remainingHealth)*50 - (GameManager.Instance.totalDistance);
        
    }
}

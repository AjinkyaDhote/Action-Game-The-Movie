using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PayLoadMovementScript : MonoBehaviour
{

    public Camera mainCamera;
    public int payLoadSpeed;
    Vector3[] wayPoints3D;
    Rigidbody rigidBody;
    int wayPointNumber;
    PauseMenu pauseMenuScript;
    [HideInInspector]
    public static CountdownTimerScript countdownTimer;

    Text BatteryText;
    string batteryString;
    private int batteryCount = 100;
    private Battery _battery;
    private bool lastReached;
    private bool isMoving;
    private bool isRotating;

    float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = transform.position.y;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }

    void Start()
    {
        isMoving = true;

        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();

        width2DPlane = GameManager.Instance.width2DPlane;
        height2DPlane = GameManager.Instance.height2DPlane;
        width3DPlane = GameManager.Instance.width3DPlane;
        height3DPlane = GameManager.Instance.height3DPlane;

        wayPointNumber = 1;


        if (GameManager.Instance.mapPoints.Count == 1)
        {
            wayPointNumber = 0;
            lastReached = true;
        }
        else
        {
            lastReached = false;
        }

        rigidBody = GetComponent<Rigidbody>();

        wayPoints3D = new Vector3[GameManager.Instance.mapPoints.Count];

        for (int i = 0; i < wayPoints3D.Length; i++)
        {
            wayPoints3D[i] = convertPoint(GameManager.Instance.mapPoints[i]);
        }
        transform.position = wayPoints3D[0];
        transform.LookAt(wayPoints3D[wayPointNumber]);

        _battery = GameObject.Find("FPSPlayer").GetComponent<Battery>();
        BatteryText = GameObject.Find("FPS UI Canvas").GetComponent<Transform>().GetChild(4).GetComponent<Text>();
        BatteryText.color = Color.white;
        batteryString = " " + (batteryCount + _battery.batteryPickedUp);
        BatteryText.text = batteryString;
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, wayPoints3D[wayPointNumber]) < 0.5f)
        {
            isRotating = true;
            if (lastReached == false && wayPointNumber == wayPoints3D.Length - 1)
            {
                batteryCount -= GameManager.Instance.batteryUsedList[wayPointNumber - 1];
                batteryString = " " + (batteryCount + _battery.batteryPickedUp);
                BatteryText.text = batteryString;
                lastReached = true;
            }

            if (wayPointNumber < (wayPoints3D.Length - 1))
            {
                batteryCount -= GameManager.Instance.batteryUsedList[wayPointNumber - 1];
                batteryString = " " + (batteryCount + _battery.batteryPickedUp);
                BatteryText.text = batteryString;
                wayPointNumber++;
                //transform.LookAt(wayPoints3D[wayPointNumber]);
            }
        }
        else
        {
            rigidBody.MovePosition(transform.position + (wayPoints3D[wayPointNumber] - transform.position).normalized * payLoadSpeed * Time.deltaTime);
            batteryString = " " + (batteryCount + _battery.batteryPickedUp);
            BatteryText.text = batteryString;
        }
        if (isRotating)
        {         
           transform.forward = Vector3.Lerp(transform.forward, wayPoints3D[wayPointNumber] - wayPoints3D[wayPointNumber -1], Time.deltaTime * 0.2f);
        }
    }

    //void FixedUpdate()
    //{
    //    if (Vector3.Distance(transform.position, wayPoints3D[wayPointNumber]) < 0.5f)
    //    {
    //        if (lastReached == false && wayPointNumber == wayPoints3D.Length - 1)
    //        {
    //            batteryCount -= GameManager.Instance.batteryUsedList[wayPointNumber - 1];
    //            batteryString = " " + (batteryCount + _battery.batteryPickedUp);
    //            BatteryText.text = batteryString;
    //            lastReached = true;
    //        }

    //        if (wayPointNumber < (wayPoints3D.Length - 1))
    //        {
    //            batteryCount -= GameManager.Instance.batteryUsedList[wayPointNumber - 1];
    //            batteryString = " " + (batteryCount + _battery.batteryPickedUp);
    //            BatteryText.text = batteryString;
    //            wayPointNumber++;
    //        }
    //    }

    //    else
    //    {
    //        rigidBody.MovePosition(transform.position + (wayPoints3D[wayPointNumber] - transform.position).normalized * playerSpeed * Time.deltaTime);
    //        batteryString = " " + (batteryCount + _battery.batteryPickedUp);
    //        BatteryText.text = batteryString;
    //    }
    //}
}

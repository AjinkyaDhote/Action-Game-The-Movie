using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public Camera mainCamera;
	public int playerSpeed;
	Vector3[] wayPoints3D;
	Rigidbody rigidBody;
	int wayPointNumber;
	public MouseLook mouseLook;
	wasdMovement WASDmovement;
	PauseMenu pauseMenuScript;
	[HideInInspector]
	public static CountdownTimerScript countdownTimer;

	Text BatteryText;
	string batteryString;
	private int batteryCount = 100;
	private Battery _battery;
	private bool lastReached;

	float width2DPlane, width3DPlane, height2DPlane, height3DPlane;
	Vector3 convertPoint(Vector2 relativePoint)
	{
		Vector3 returnVal;
		returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
		returnVal.y = 1.0f;
		returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
		return returnVal;
	}

	void Start()
	{
		pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
		countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();

		width2DPlane = GameManager.Instance.width2DPlane;
		height2DPlane = GameManager.Instance.height2DPlane;
		width3DPlane = GameManager.Instance.width3DPlane;
		height3DPlane = GameManager.Instance.height3DPlane;

		WASDmovement = GetComponent<wasdMovement>();
		wayPointNumber = 1;
		rigidBody = GetComponent<Rigidbody>();
		if (!WASDmovement.enabled)
		{
			wayPoints3D = new Vector3[GameManager.Instance.mapPoints.Count];
			for (int i = 0; i < wayPoints3D.Length; i++)
			{
				wayPoints3D[i] = convertPoint(GameManager.Instance.mapPoints[i]);
			}
			transform.position = wayPoints3D[0];
		}
		mouseLook = new MouseLook();

		_battery = gameObject.GetComponent<Battery>();
		BatteryText = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("BatteryText").GetComponent<Text>();
		BatteryText.color = Color.white;
		batteryString = " " + (batteryCount + _battery.batteryPickedUp);
		BatteryText.text = batteryString;
		lastReached = false;
	}
	void Update()
	{
		if (!pauseMenuScript.isPaused)
		{
			mouseLook.LookRotation(transform, mainCamera.transform);
		}
	}

	void FixedUpdate()
	{
		if (!WASDmovement.enabled)
		{
			if (Vector3.Distance(transform.position, wayPoints3D[wayPointNumber]) < 0.5f)
			{
				if (wayPointNumber == wayPoints3D.Length - 1 && lastReached == false)
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
				}
			}
			else
			{
				rigidBody.MovePosition(transform.position + (wayPoints3D[wayPointNumber] - transform.position).normalized * playerSpeed * Time.deltaTime);
				batteryString = " " + (batteryCount + _battery.batteryPickedUp);
				BatteryText.text = batteryString;
			}
		}
	}
}
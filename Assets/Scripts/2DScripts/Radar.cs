using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour

{
    private float reciprocalSpeed;
    [SerializeField]
    private float _speed = 1000.0f;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            reciprocalSpeed = 1.0f / _speed;
        }
    }
    private const float RADIUS = 30.0f;
    private const float SCALE_MULTIPLIER = 2.0f;
    private const int LAYERMASK = 0x6000;
    private readonly Vector3 originalAmmoScale = new Vector3(0.25f, 0.25f, 0.25f);
    private readonly Vector3 originalBatteryScale = new Vector3(0.27f, 0.27f, 0.27f);
    private Transform[] ammos;
    private Transform[] batteries;
    private RaycastHit2D[] raycasts;
    private Vector3[] circlePoints;
    private uint it;
    private float elapsedTime, currentTime;
    private bool isElapsedTimeIniTialized;
    private LineRenderer lineRenderer;
    private void Start()
    {
        circlePoints = Utilities.GenerateCirclePoints(RADIUS);
        GameObject[] ammosGO = GameObject.FindGameObjectsWithTag("Ammo");
        GameObject[] batteriesGO = GameObject.FindGameObjectsWithTag("Battery");
        ammos = new Transform[ammosGO.Length];
        for (int i = 0; i < ammosGO.Length; i++)
        {
            ammos[i] = ammosGO[i].GetComponent<Transform>();
        }
        batteries = new Transform[batteriesGO.Length];
        for (int i = 0; i < batteriesGO.Length; i++)
        {
            batteries[i] = batteriesGO[i].GetComponent<Transform>();
        }
        it = 0;
        isElapsedTimeIniTialized = false;
        lineRenderer = GetComponent<LineRenderer>();
        Speed = Speed;
    }
    private void FixedUpdate()
    {
        if (isElapsedTimeIniTialized)
        {
            currentTime = Time.fixedTime;
            RescaleObjects();
        }
        else
        {
            currentTime = elapsedTime = Time.fixedTime;
            isElapsedTimeIniTialized = true;
        }
        if (currentTime - elapsedTime >= reciprocalSpeed)
        {
            elapsedTime = currentTime;
            it++;
            if (it == circlePoints.Length)
            {
                it = 0;
            }
            RescaleObjects();
        }
    }
    private void RescaleObjects()
    {
        lineRenderer.SetPosition(1, circlePoints[it]);
        raycasts = Physics2D.RaycastAll(Vector2.zero, circlePoints[it].normalized, RADIUS, LAYERMASK);
        for (int i = 0; i < ammos.Length; i++)
        {
            ammos[i].localScale = originalAmmoScale;
        }
        for (int i = 0; i < batteries.Length; i++)
        {
            batteries[i].localScale = originalBatteryScale;
        }
        for (int i = 0; i < raycasts.Length; i++)
        {
            raycasts[i].collider.gameObject.transform.localScale *= SCALE_MULTIPLIER;
        }
    }
}

using UnityEngine;

public class Player2D : MonoBehaviour
{
    public float speed;
    public MapScript mapScript;
    public Vector3 source, destination;

    public int currentPosIndex;

    void Awake()
    {
        mapScript.setPlayerInitialPos(transform.position);
    }

    void Start()
    {
        speed = 4f;
        currentPosIndex = 0;
        source = transform.position;
        destination = transform.position;
        
    }

    void Update()
    {
        Vector3 direction = (destination - source).normalized;
        float distance = Vector3.Distance(transform.position, destination);
        if (distance > 0.1f)
        {
            transform.Translate((direction * (speed * Time.deltaTime)));
        }
        else
        {
            transform.position = destination;
            source = destination;

            if (currentPosIndex < mapScript.playerPosList.Count-1)
            {
                currentPosIndex++;
                destination = mapScript.playerPosList[currentPosIndex];
            }
        }
    }
}
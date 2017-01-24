using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{

    public float droneSpeed;


    Vector3[] randomVectors;
    Vector3 initialPosition;    

    NavMeshAgent agent;
	
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = droneSpeed;

        randomVectors = new Vector3[8];

        randomVectors[0] = new Vector3(1.0f, 1.0f, 0.0f);
        randomVectors[1] = new Vector3(1.0f, 1.0f, 1.0f);
        randomVectors[2] = new Vector3(0.0f, 1.0f, 1.0f);
        randomVectors[3] = new Vector3(-1.0f, 1.0f, 1.0f);
        randomVectors[4] = new Vector3(-1.0f, 1.0f, 0.0f);
        randomVectors[5] = new Vector3(-1.0f, 1.0f, -1.0f);
        randomVectors[6] = new Vector3(0.0f, 1.0f, -1.0f);
        randomVectors[7] = new Vector3(1.0f, 1.0f, -1.0f);

        initialPosition = gameObject.transform.position;

        Patrol();
    }
	
	
	void Update ()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Patrol();
        }      		
	}

    Vector3 GetRandomVector()
    {
        bool hit = true;
        Vector3 randomPoint;
        Vector3 direction;
        int randomIndex = Random.Range(0, 8);
        int initialIndex = randomIndex;

        do
        {
            randomIndex = (randomIndex + 1) % 8;
            randomPoint = randomVectors[randomIndex];
            randomPoint *= 4;
            randomPoint += initialPosition;

            direction = randomPoint - gameObject.transform.position;
            hit = Physics.Raycast(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, direction.magnitude);

        } while (hit && initialIndex != randomIndex);

        return randomPoint;
    }

    void Patrol()
    {
        agent.destination = GetRandomVector();
    }
}

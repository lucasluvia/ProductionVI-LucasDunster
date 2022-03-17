using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolDetector : MonoBehaviour
{
    newPlayerBehaviour player;

    public bool pauseDetector;

    [SerializeField] Transform patrolPoint1;
    [SerializeField] Transform patrolPoint2;
    [SerializeField] Transform patrolPoint3;
    [SerializeField] Transform patrolPoint4;
    Transform currentDestination;

    int currentDestinationPoint = 1; 
    private Quaternion LookRotation;
    private Vector3 Direction;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<newPlayerBehaviour>();
        currentDestination = patrolPoint1;
        pauseDetector = false;
    }

    void Update()
    {
        if (!pauseDetector)
            transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, Time.deltaTime * 2);

        Direction = (currentDestination.position - transform.position).normalized;
        LookRotation = Quaternion.LookRotation(Direction);
        if (!pauseDetector)
            transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * 4);


        if (Vector3.Distance(transform.position, currentDestination.position) < 1.2f)
        {
            if (currentDestinationPoint > 4) currentDestinationPoint = 1;
            Debug.Log("Currently Patrolling to Point " + currentDestinationPoint);

            if (currentDestinationPoint == 1)
                currentDestination = patrolPoint1;
            if (currentDestinationPoint == 2)
                currentDestination = patrolPoint2;
            if (currentDestinationPoint == 3)
                currentDestination = patrolPoint3;
            if (currentDestinationPoint == 4)
                currentDestination = patrolPoint4;

            currentDestinationPoint++;
        }

    }
}

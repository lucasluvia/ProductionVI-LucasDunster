using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GravityDirection currentFloor;
    public GameObject playerReference;
    public GameObject environmentReference;

    private Vector3 newRotationDestination = new Vector3(0.0f, 0.0f, 0.0f);
    private bool isRotating = false;

    Vector3 environmentRotation;
    private float rotationUnits;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        playerReference = GameObject.Find("Player");
        environmentReference = GameObject.Find("[ENVIRONMENT]");
        environmentRotation = new Vector3(environmentReference.transform.rotation.x, environmentReference.transform.rotation.y, environmentReference.transform.rotation.z);
    }

    void Update()
    {
        if(isRotating)
        {
            environmentReference.transform.localRotation = Quaternion.Euler(newRotationDestination);
            //playerReference.transform.localRotation = Quaternion.Euler(-newRotationDestination);
            //playerReference.transform.localRotation = Quaternion.Euler(-newRotationDestination);
            isRotating = false;
            
        }
    }

    public void Rotate(GravityDirection newDirection)
    {
        switch (newDirection)
        {
            case GravityDirection.DOWN:
                newRotationDestination = (new Vector3(0.0f, 0.0f, 0.0f));
                if (currentFloor == GravityDirection.UP)
                    rotationUnits = 180.0f;
                else
                    rotationUnits = 90.0f;
                break;
            case GravityDirection.UP:
                 newRotationDestination = (new Vector3(0.0f, 0.0f, 180.0f));
                if (currentFloor == GravityDirection.DOWN)
                    rotationUnits = 180.0f;
                else
                    rotationUnits = 90.0f;
                break;                                  
            case GravityDirection.NORTH:
                newRotationDestination = (new Vector3(90.0f, 0.0f, 0.0f));
                if (currentFloor == GravityDirection.SOUTH)
                    rotationUnits = 180.0f;
                else
                    rotationUnits = 90.0f;
                break;                                   
            case GravityDirection.SOUTH:                 
                 newRotationDestination = (new Vector3(-90.0f, 0.0f, 0.0f));
                if (currentFloor == GravityDirection.NORTH)
                    rotationUnits = 180.0f;
                else
                    rotationUnits = 90.0f;
                break;                                  
            case GravityDirection.EAST:                
                 newRotationDestination = (new Vector3(0.0f, 0.0f, -90.0f));
                if (currentFloor == GravityDirection.WEST)
                    rotationUnits = 180.0f;
                else
                    rotationUnits = 90.0f;
                break;                                    
            case GravityDirection.WEST:                  
                 newRotationDestination = (new Vector3(0.0f, 0.0f, 90.0f));
                if (currentFloor == GravityDirection.EAST)
                    rotationUnits = 180.0f;
                else
                    rotationUnits = 90.0f;
                break;
        }
        isRotating = true;
        currentFloor = newDirection;
    }


}

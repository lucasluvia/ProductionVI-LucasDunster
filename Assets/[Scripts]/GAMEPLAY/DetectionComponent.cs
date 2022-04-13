using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    public DetectionType detectionType;
    public float DetectionStrength;
    public float DecreaseStrength = 5f;

    public bool isPlayerHere;


    DetectionController detectionController;
    newPlayerBehaviour player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<newPlayerBehaviour>();
        switch (detectionType)
        {
            case DetectionType.CAMERA_STATIC:
                DetectionStrength = 10f;
                break;
            case DetectionType.CAMERA_SCANNING:
                DetectionStrength = 10f;
                break;
            case DetectionType.PATROL:
                DetectionStrength = 20f;
                break;
        }
        detectionController = GameObject.FindWithTag("DetectionController").GetComponent<DetectionController>();
    }

    float elapsed = 0f;
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            if (player.inDetector)
            {
                if (isPlayerHere)
                {
                    detectionController.DetectionProgression += DetectionStrength;
                    Debug.Log("Adding +" + DetectionStrength + " to progression.");
                }
            }
            else
            {
                if (detectionController.DetectionProgression >= 1)
                    detectionController.DetectionProgression -= DecreaseStrength;
            }
        }
    }
}


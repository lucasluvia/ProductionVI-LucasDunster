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
                DetectionStrength = 20f;
                break;
            case DetectionType.PATROL:
                DetectionStrength = 35f;
                break;
        }
        detectionController = GameObject.FindWithTag("DetectionController").GetComponent<DetectionController>();
    }

    float elapsed = 0f;
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.5f)
        {
            elapsed = elapsed % 0.5f;
            if (player.inDetector)
            {
                if (isPlayerHere)
                {
                    detectionController.DetectionProgression += DetectionStrength;
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


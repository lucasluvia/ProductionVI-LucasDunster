using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    public DetectionType detectionType;
    public float DetectionStrength;
    public float DecreaseStrength = 1;

    DetectionController detectionController;
    newPlayerBehaviour player;

    void Start()
    {
        switch (detectionType)
        {
            case DetectionType.CAMERA_STATIC:
                DetectionStrength = 5.0f;
                break;
            case DetectionType.CAMERA_SCANNING:
                DetectionStrength = 5.0f;
                break;
            case DetectionType.PATROL:
                DetectionStrength = 10.0f;
                break;
        }
        player = GameObject.FindWithTag("Player").GetComponent<newPlayerBehaviour>();
        detectionController = GameObject.FindWithTag("DetectionController").GetComponent<DetectionController>();
    }

    float elapsed = 0f;
    void Update()
    {

        elapsed += Time.deltaTime;
        if(elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            if (player.inDetector)
            {
                detectionController.DetectionProgression += DetectionStrength;
            }
            else
            {
                if (detectionController.DetectionProgression >= 1)
                    detectionController.DetectionProgression -= DecreaseStrength;
            }
        }
    }

}

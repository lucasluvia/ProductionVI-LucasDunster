using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionController : MonoBehaviour
{
    private Slider detectionSlider;
    [Range(0,100)]
    public float DetectionProgression;

    void Start()
    {
        detectionSlider = GetComponent<Slider>();
    }

    void Update()
    {
        detectionSlider.value = DetectionProgression / 100;
    }
}

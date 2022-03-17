using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionController : MonoBehaviour
{
    private CanvasController canvasController;

    private Slider detectionSlider;
    [Range(0,100)]
    public float DetectionProgression;

    void Start()
    {
        canvasController = GameObject.FindWithTag("Canvas").GetComponent<CanvasController>();
        detectionSlider = GetComponent<Slider>();
    }

    void Update()
    {
        //detectionSlider.value = DetectionProgression / 100;
        detectionSlider.value = Mathf.Lerp(detectionSlider.value, DetectionProgression / 100, Time.deltaTime);
        if(detectionSlider.value >= 1)
        {
            canvasController.ShowDeathScreen();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPulse : MonoBehaviour
{
    [SerializeField] Image colourToPulse;
    [SerializeField] Color COLOUR1 = new Color(1,0,0,1);
    [SerializeField] Color COLOUR2 = new Color(1,0.5f,0.5f,1);

    void Update()
    {
        colourToPulse.color = Color.Lerp(COLOUR1, COLOUR2, Mathf.PingPong(Time.time, 1));
    }
}
